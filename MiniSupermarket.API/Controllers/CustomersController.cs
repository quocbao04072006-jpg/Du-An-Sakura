using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Cashier")]
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        private static readonly string[] AllowedMembershipRanks =
        {
            "Chuẩn",
            "Bạc",
            "Vàng",
            "VIP"
        };

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // 1. LẤY DANH SÁCH KHÁCH HÀNG
        // GET: api/customers
        // =====================================================

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customers = await _context.Customers
                    .AsNoTracking()
                    .OrderBy(c => c.CustomerId)
                    .ToListAsync();

                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Không thể tải danh sách khách hàng."
                });
            }
        }

        // =====================================================
        // 2. LẤY CHI TIẾT KHÁCH HÀNG
        // GET: api/customers/1
        // =====================================================

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var customer = await _context.Customers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.CustomerId == id);

                if (customer == null)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy khách hàng."
                    });
                }

                return Ok(customer);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Không thể tải thông tin khách hàng."
                });
            }
        }

        // =====================================================
        // 3. TÌM KIẾM KHÁCH HÀNG
        // GET: api/customers/search?keyword=nguyen
        // =====================================================

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomers(
            [FromQuery] string? keyword = null)
        {
            try
            {
                IQueryable<Customer> query =
                    _context.Customers.AsNoTracking();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    string normalized = keyword.Trim();

                    query = query.Where(c =>
                        c.CustomerName.Contains(normalized) ||
                        c.PhoneNumber.Contains(normalized) ||
                        (c.Address != null &&
                         c.Address.Contains(normalized)));
                }

                var customers = await query
                    .OrderBy(c => c.CustomerId)
                    .ToListAsync();

                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Không thể tìm kiếm khách hàng."
                });
            }
        }

        // =====================================================
        // 4. THÊM KHÁCH HÀNG
        // POST: api/customers
        //
        // Theo quy tắc hiện tại:
        // - Khách mới bắt đầu với 0 điểm.
        // - Hạng ban đầu là Chuẩn.
        // - Ngày tham gia do server ghi nhận.
        // =====================================================

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(
            [FromBody] CreateCustomerRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu khách hàng không hợp lệ."
                });
            }

            if (string.IsNullOrWhiteSpace(request.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống."
                });
            }

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống."
                });
            }

            string name = request.CustomerName.Trim();
            string phone = request.PhoneNumber.Trim();

            if (name.Length > 100)
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được vượt quá 100 ký tự."
                });
            }

            if (phone.Length > 15)
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được vượt quá 15 ký tự."
                });
            }

            if (request.Address?.Length > 200)
            {
                return BadRequest(new
                {
                    message = "Địa chỉ không được vượt quá 200 ký tự."
                });
            }

            bool phoneExists = await _context.Customers
                .AnyAsync(c => c.PhoneNumber == phone);

            if (phoneExists)
            {
                return Conflict(new
                {
                    message =
                        "Số điện thoại này đã tồn tại cho một khách hàng khác."
                });
            }

            var customer = new Customer
            {
                CustomerName = name,
                PhoneNumber = phone,

                Address = string.IsNullOrWhiteSpace(request.Address)
                    ? null
                    : request.Address.Trim(),

                RewardPoints = 0,
                MembershipRank = "Chuẩn",
                CreatedAt = DateTime.Now
            };

            _context.Customers.Add(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message =
                        "Không thể thêm khách hàng. " +
                        "Hãy kiểm tra số điện thoại đã tồn tại hay chưa."
                });
            }

            return CreatedAtAction(
                nameof(GetCustomer),
                new { id = customer.CustomerId },
                customer);
        }

        // =====================================================
        // 5. CẬP NHẬT KHÁCH HÀNG
        // PUT: api/customers/1
        //
        // ĐÃ SỬA:
        // - Cập nhật RewardPoints.
        // - Cập nhật MembershipRank.
        // - Giữ nguyên CreatedAt.
        // - Không cho điểm âm.
        // - Kiểm tra số điện thoại trùng.
        // =====================================================

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Customer>> PutCustomer(
            int id,
            [FromBody] UpdateCustomerRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu khách hàng không hợp lệ."
                });
            }

            if (id <= 0)
            {
                return BadRequest(new
                {
                    message = "Mã khách hàng không hợp lệ."
                });
            }

            if (string.IsNullOrWhiteSpace(request.CustomerName))
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được để trống."
                });
            }

            if (string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được để trống."
                });
            }

            string name = request.CustomerName.Trim();
            string phone = request.PhoneNumber.Trim();

            if (name.Length > 100)
            {
                return BadRequest(new
                {
                    message = "Tên khách hàng không được vượt quá 100 ký tự."
                });
            }

            if (phone.Length > 15)
            {
                return BadRequest(new
                {
                    message = "Số điện thoại không được vượt quá 15 ký tự."
                });
            }

            if (request.Address?.Length > 200)
            {
                return BadRequest(new
                {
                    message = "Địa chỉ không được vượt quá 200 ký tự."
                });
            }

            // Điểm thưởng không được âm.
            if (request.RewardPoints.HasValue &&
                request.RewardPoints.Value < 0)
            {
                return BadRequest(new
                {
                    message = "Điểm thưởng không được nhỏ hơn 0."
                });
            }

            // Nếu client gửi hạng mới thì phải thuộc danh sách hợp lệ.
            string? requestedRank = null;

            if (!string.IsNullOrWhiteSpace(request.MembershipRank))
            {
                string rank = request.MembershipRank.Trim();

                requestedRank = AllowedMembershipRanks.FirstOrDefault(
                    r => string.Equals(
                        r,
                        rank,
                        StringComparison.OrdinalIgnoreCase));

                if (requestedRank == null)
                {
                    return BadRequest(new
                    {
                        message =
                            "Hạng thành viên không hợp lệ. " +
                            "Các hạng được phép: Chuẩn, Bạc, Vàng, VIP."
                    });
                }
            }

            var existing = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (existing == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng."
                });
            }

            bool phoneExists = await _context.Customers
                .AnyAsync(c =>
                    c.CustomerId != id &&
                    c.PhoneNumber == phone);

            if (phoneExists)
            {
                return Conflict(new
                {
                    message =
                        "Số điện thoại này đã được sử dụng bởi khách hàng khác."
                });
            }

            // Cập nhật thông tin liên hệ.
            existing.CustomerName = name;
            existing.PhoneNumber = phone;

            existing.Address =
                string.IsNullOrWhiteSpace(request.Address)
                    ? null
                    : request.Address.Trim();

            // =================================================
            // SỬA LỖI KHÔNG CẬP NHẬT ĐIỂM
            // =================================================

            // Nếu client có gửi điểm mới, lưu đúng điểm đó.
            // Nếu client cũ không gửi trường này, giữ nguyên điểm.
            if (request.RewardPoints.HasValue)
            {
                existing.RewardPoints =
                    request.RewardPoints.Value;
            }

            // Nếu client có gửi hạng mới, lưu đúng hạng đã chọn.
            // Nếu không gửi hạng, giữ nguyên hạng đang có.
            if (requestedRank != null)
            {
                existing.MembershipRank = requestedRank;
            }

            // CreatedAt tiếp tục do server quản lý và không thay đổi
            // khi cập nhật thông tin khách hàng.

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message =
                        "Không thể cập nhật khách hàng. " +
                        "Hãy kiểm tra lại dữ liệu."
                });
            }

            // Trả về dữ liệu đã lưu để WinForms kiểm tra.
            return Ok(new
            {
                message = "Cập nhật khách hàng thành công.",
                existing.CustomerId,
                existing.CustomerName,
                existing.PhoneNumber,
                existing.Address,
                existing.RewardPoints,
                existing.MembershipRank,
                existing.CreatedAt
            });
        }

        // =====================================================
        // 6. XÓA KHÁCH HÀNG
        // DELETE: api/customers/1
        // Chỉ Admin được xóa.
        // =====================================================

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng."
                });
            }

            bool hasOrders = await _context.Orders
                .AnyAsync(o => o.CustomerId == id);

            if (hasOrders)
            {
                return Conflict(new
                {
                    message =
                        "Khách hàng đã có lịch sử mua hàng, " +
                        "không thể xóa để tránh ảnh hưởng dữ liệu hóa đơn."
                });
            }

            _context.Customers.Remove(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message =
                        "Không thể xóa khách hàng vì dữ liệu đang được sử dụng."
                });
            }

            return NoContent();
        }
    }

    // =========================================================
    // DTO THÊM KHÁCH HÀNG
    // =========================================================

    public class CreateCustomerRequest
    {
        public string CustomerName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? Address { get; set; }
    }

    // =========================================================
    // DTO CẬP NHẬT KHÁCH HÀNG
    //
    // RewardPoints là int? để client cũ không gửi điểm
    // thì API vẫn giữ nguyên điểm hiện có.
    // =========================================================

    public class UpdateCustomerRequest
    {
        public string CustomerName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? Address { get; set; }

        public int? RewardPoints { get; set; }

        public string? MembershipRank { get; set; }
    }
}