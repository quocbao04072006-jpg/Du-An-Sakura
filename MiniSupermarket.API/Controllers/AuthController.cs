using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SupermarketDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthController(
        SupermarketDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    // POST: api/auth/login
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginModel model)
    {
        // 1. Kiểm tra dữ liệu đầu vào
        if (model == null ||
            string.IsNullOrWhiteSpace(model.Username) ||
            string.IsNullOrWhiteSpace(model.Password))
        {
            return BadRequest(new
            {
                success = false,
                message = "Vui lòng nhập tên đăng nhập và mật khẩu!"
            });
        }

        // 2. Tìm tài khoản theo tên đăng nhập
        var username = model.Username.Trim();

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        // 3. Không tồn tại tài khoản
        if (user == null)
        {
            return Unauthorized(new
            {
                success = false,
                message = "Tên đăng nhập hoặc mật khẩu không chính xác!"
            });
        }

        // 4. QUAN TRỌNG: Kiểm tra trạng thái tài khoản
        // IsActive = false thì tuyệt đối không cấp JWT
        if (!user.IsActive)
        {
            return Unauthorized(new
            {
                success = false,
                message = "Tài khoản đã bị khóa! Vui lòng liên hệ quản trị viên."
            });
        }

        // 5. Kiểm tra mật khẩu
        // Giữ nguyên cách lưu mật khẩu hiện tại của project
        if (!string.Equals(
                user.Password,
                model.Password,
                StringComparison.Ordinal))
        {
            return Unauthorized(new
            {
                success = false,
                message = "Tên đăng nhập hoặc mật khẩu không chính xác!"
            });
        }

        // 6. Lấy khóa bí mật dùng để ký JWT
        var secret = _configuration["JwtSettings:Secret"];

        if (string.IsNullOrWhiteSpace(secret))
        {
            return StatusCode(500, new
            {
                success = false,
                message = "Chưa cấu hình JwtSettings:Secret trong appsettings.json!"
            });
        }

        var key = Encoding.UTF8.GetBytes(secret);

        // 7. Tạo thông tin định danh và phân quyền
        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                user.Username),

            new Claim(
                ClaimTypes.Role,
                user.Role)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),

            Expires = DateTime.UtcNow.AddHours(8),

            Issuer = _configuration["JwtSettings:Issuer"],

            Audience = _configuration["JwtSettings:Audience"],

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        // 8. Chỉ cấp JWT sau khi tài khoản hợp lệ
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        // 9. Trả kết quả đăng nhập thành công
        return Ok(new
        {
            success = true,
            Token = tokenString,
            Username = user.Username,
            Role = user.Role,
            FullName = user.FullName
        });
    }
}

public class LoginModel
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}