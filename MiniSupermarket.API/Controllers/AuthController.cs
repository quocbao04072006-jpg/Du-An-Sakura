using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthController(
        SupermarketDbContext context,
        IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
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

        // 2. Tìm tài khoản
        var username = model.Username.Trim();

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            return Unauthorized(new
            {
                success = false,
                message = "Tên đăng nhập hoặc mật khẩu không chính xác!"
            });
        }

        // 3. Tài khoản bị khóa thì không cấp JWT
        if (!user.IsActive)
        {
            return Unauthorized(new
            {
                success = false,
                message = "Tài khoản đã bị khóa! Vui lòng liên hệ quản trị viên."
            });
        }

        // 4. Kiểm tra mật khẩu đã mã hóa
        var verificationResult = VerifyPassword(
            user,
            model.Password);

        if (verificationResult == PasswordVerificationResult.Success ||
            verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
        {
            // Mật khẩu hợp lệ.
            // Nếu thuật toán hash cũ cần nâng cấp thì mã hóa lại.
            if (verificationResult ==
                PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.Password = _passwordHasher.HashPassword(
                    user,
                    model.Password);

                await _context.SaveChangesAsync();
            }
        }
        else
        {
            // Tương thích với tài khoản cũ đang lưu mật khẩu dạng văn bản.
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

            // Đăng nhập đúng bằng mật khẩu cũ:
            // chuyển sang lưu mật khẩu đã mã hóa.
            user.Password = _passwordHasher.HashPassword(
                user,
                model.Password);

            await _context.SaveChangesAsync();
        }

        // 5. Đọc cấu hình JWT
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

        // HS256 cần khóa bí mật có ít nhất 32 byte.
        if (key.Length < 32)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "JwtSettings:Secret phải có ít nhất 32 byte."
            });
        }

        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];

        // 6. Tạo claims cho JWT
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
                user.Role ?? string.Empty)
        };

        // 7. Tạo token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),

            Expires = DateTime.UtcNow.AddHours(8),

            Issuer = issuer,

            Audience = audience,

            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var tokenString = tokenHandler.WriteToken(token);

        // 8. Trả kết quả đăng nhập
        // Giữ lại các trường để hạn chế ảnh hưởng đến WinForms hiện tại.
        return Ok(new
        {
            success = true,
            Token = tokenString,
            Username = user.Username,
            Role = user.Role,
            FullName = user.FullName
        });
    }

    // Xác minh mật khẩu đã mã hóa.
    // Mật khẩu cũ dạng văn bản có thể không phải chuỗi hash hợp lệ.
    private PasswordVerificationResult VerifyPassword(
        User user,
        string enteredPassword)
    {
        try
        {
            return _passwordHasher.VerifyHashedPassword(
                user,
                user.Password ?? string.Empty,
                enteredPassword);
        }
        catch (FormatException)
        {
            return PasswordVerificationResult.Failed;
        }
    }
}

// Dữ liệu nhận từ màn hình đăng nhập
public class LoginModel
{
    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}