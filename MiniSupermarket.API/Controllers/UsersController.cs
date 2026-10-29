
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly PasswordHasher<User> _passwordHasher;

        private static readonly string[] AllowedRoles =
        {
            "Admin",
            "Cashier",
            "Warehouse"
        };

        public UsersController(SupermarketDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        // GET: api/users
        // Khong tra mat khau trong response.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    Role = u.Role,
                    IsActive = u.IsActive
                })
                .ToListAsync();

            return Ok(users);
        }

        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser(
            [FromBody] CreateUserRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu tài khoản không hợp lệ."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Username))
            {
                return BadRequest(new
                {
                    message = "Tên tài khoản không được để trống."
                });
            }

            if (string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    message = "Mật khẩu không được để trống."
                });
            }

            string username = request.Username.Trim();
            string password = request.Password;

            if (username.Length > 100)
            {
                return BadRequest(new
                {
                    message = "Tên tài khoản không được vượt quá 100 ký tự."
                });
            }

            if (password.Length < 8 || password.Length > 256)
            {
                return BadRequest(new
                {
                    message = "Mật khẩu phải dài từ 8 đến 256 ký tự."
                });
            }

            if (await _context.Users.AnyAsync(
                    u => u.Username == username))
            {
                return Conflict(new
                {
                    message = "Tên tài khoản đã tồn tại."
                });
            }

            string role = "Cashier";

            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                string requestedRole = request.Role.Trim();

                string? matchedRole = AllowedRoles.FirstOrDefault(
                    r => r.Equals(
                        requestedRole,
                        StringComparison.OrdinalIgnoreCase));

                if (matchedRole == null)
                {
                    return BadRequest(new
                    {
                        message = "Vai trò không hợp lệ. " +
                                  "Chỉ chấp nhận Admin, Cashier hoặc Warehouse."
                    });
                }

                role = matchedRole;
            }

            var user = new User
            {
                Username = username,
                FullName = request.FullName?.Trim() ?? string.Empty,
                Role = role,
                IsActive = true
            };

            // Ma hoa mat khau truoc khi luu SQL Server.
            user.Password = _passwordHasher.HashPassword(
                user,
                password);

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message = "Không thể tạo tài khoản. " +
                              "Hãy kiểm tra tên tài khoản đã tồn tại hay chưa."
                });
            }

            return Ok(new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FullName = user.FullName,
                Role = user.Role,
                IsActive = user.IsActive
            });
        }

        // PUT: api/users/5/active
        [HttpPut("{id:int}/active")]
        public async Task<IActionResult> UpdateUserActive(
            int id,
            [FromBody] UpdateUserActiveRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu trạng thái không hợp lệ."
                });
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản cần cập nhật."
                });
            }

            // Khong cho Admin tu khoa tai khoan dang dang nhap.
            var currentUserId =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!request.IsActive &&
                int.TryParse(currentUserId, out int actorId) &&
                actorId == id)
            {
                return BadRequest(new
                {
                    message = "Bạn không thể tự khóa tài khoản đang đăng nhập."
                });
            }

            // Khong cho khoa Admin cuoi cung dang hoat dong.
            if (!request.IsActive &&
                user.IsActive &&
                string.Equals(
                    user.Role,
                    "Admin",
                    StringComparison.OrdinalIgnoreCase))
            {
                int activeAdminCount = await _context.Users
                    .CountAsync(u => u.IsActive && u.Role == "Admin");

                if (activeAdminCount <= 1)
                {
                    return Conflict(new
                    {
                        message = "Không thể khóa Admin cuối cùng đang hoạt động."
                    });
                }
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
