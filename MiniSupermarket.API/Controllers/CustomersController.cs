using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
                return NotFound("Không tìm thấy khách hàng.");
            return customer;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Customer>>> SearchCustomers([FromQuery] string keyword)
        {
            IQueryable<Customer> query = _context.Customers.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string normalized = keyword.Trim();
                query = query.Where(c =>
                    c.CustomerName.Contains(normalized) ||
                    c.PhoneNumber.Contains(normalized) ||
                    (c.Address != null && c.Address.Contains(normalized)));
            }
            return await query.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.CustomerName) || string.IsNullOrWhiteSpace(customer.PhoneNumber))
                return BadRequest("Tên khách hàng và số điện thoại không được để trống.");

            string phone = customer.PhoneNumber.Trim();
            bool phoneExists = await _context.Customers.AnyAsync(c => c.PhoneNumber == phone);
            if (phoneExists)
                return BadRequest("Số điện thoại này đã tồn tại cho một khách hàng khác.");

            customer.CustomerName = customer.CustomerName.Trim();
            customer.PhoneNumber = phone;
            customer.Address = string.IsNullOrWhiteSpace(customer.Address) ? null : customer.Address.Trim();
            customer.MembershipRank = string.IsNullOrWhiteSpace(customer.MembershipRank) ? "Chuẩn" : customer.MembershipRank.Trim();
            if (customer.RewardPoints < 0)
                return BadRequest("Điểm tích lũy không được âm.");
            customer.CreatedAt = customer.CreatedAt ?? DateTime.Now;

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Customer>> PutCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
                return BadRequest("Mã khách hàng không trùng khớp.");
            if (string.IsNullOrWhiteSpace(customer.CustomerName) || string.IsNullOrWhiteSpace(customer.PhoneNumber))
                return BadRequest("Tên khách hàng và số điện thoại không được để trống.");
            if (customer.RewardPoints < 0)
                return BadRequest("Điểm tích lũy không được âm.");

            string phone = customer.PhoneNumber.Trim();
            bool phoneExists = await _context.Customers.AnyAsync(c => c.CustomerId != id && c.PhoneNumber == phone);
            if (phoneExists)
                return BadRequest("Số điện thoại này đã được sử dụng bởi khách hàng khác.");

            // Update the tracked database row explicitly, including CreatedAt.
            var existing = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
            if (existing == null)
                return NotFound("Không tìm thấy khách hàng.");

            existing.CustomerName = customer.CustomerName.Trim();
            existing.PhoneNumber = phone;
            existing.Address = string.IsNullOrWhiteSpace(customer.Address) ? null : customer.Address.Trim();
            existing.RewardPoints = customer.RewardPoints;
            existing.MembershipRank = string.IsNullOrWhiteSpace(customer.MembershipRank)
                ? (existing.MembershipRank ?? "Chuẩn")
                : customer.MembershipRank.Trim();
            existing.CreatedAt = customer.CreatedAt;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound("Không tìm thấy khách hàng.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
