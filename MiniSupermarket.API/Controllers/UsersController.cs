using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public UsersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .AsNoTracking()
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = user.FullName,
                    Role = user.Role,
                    IsActive = user.IsActive
                })
                .ToListAsync();

            return Ok(users);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Tên tài khoản và mật khẩu không được để trống.");
            }

            var username = request.Username.Trim();
            if (await _context.Users.AnyAsync(user => user.Username == username))
            {
                return Conflict("Tên tài khoản đã tồn tại.");
            }

            var user = new User
            {
                Username = username,
                Password = request.Password.Trim(),
                FullName = request.FullName?.Trim() ?? string.Empty,
                Role = string.IsNullOrWhiteSpace(request.Role) ? "Cashier" : request.Role.Trim(),
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive
            });
        }

        // PUT: api/users/{id}/active
        // Chỉ Admin được thay đổi trạng thái hoạt động của tài khoản.
        [HttpPut("{id:int}/active")]
        public async Task<IActionResult> UpdateUserActive(
            int id,
            [FromBody] UpdateUserActiveRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { message = "Không tìm thấy tài khoản cần cập nhật." });
            }

            user.IsActive = request.IsActive;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = user.Id,
                username = user.Username,
                isActive = user.IsActive,
                message = user.IsActive
                    ? "Đã kích hoạt tài khoản."
                    : "Đã khóa tài khoản."
            });
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class CreateUserRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Role { get; set; }
    }

    public class UpdateUserActiveRequest
    {
        public bool IsActive { get; set; }
    }
}
