
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
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public CategoriesController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        // Moi tai khoan da dang nhap deu co the xem danh muc.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.CategoryId)
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/categories/search?keyword=...
        // Tim theo ID, ten danh muc hoac mo ta.
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Category>>> SearchCategories(
            [FromQuery] string? keyword = null)
        {
            IQueryable<Category> query =
                _context.Categories.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string normalized = keyword.Trim();

                if (int.TryParse(normalized, out int searchId))
                {
                    query = query.Where(c =>
                        c.CategoryId == searchId ||
                        c.CategoryName.Contains(normalized) ||
                        (c.Description != null &&
                         c.Description.Contains(normalized)));
                }
                else
                {
                    query = query.Where(c =>
                        c.CategoryName.Contains(normalized) ||
                        (c.Description != null &&
                         c.Description.Contains(normalized)));
                }
            }

            var categories = await query
                .OrderBy(c => c.CategoryId)
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/categories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy danh mục."
                });
            }

            return Ok(category);
        }

        // POST: api/categories
        // Chi Admin va Warehouse duoc them danh muc.
        [Authorize(Roles = "Admin,Warehouse")]
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(
            [FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu danh mục không hợp lệ."
                });
            }

            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return BadRequest(new
                {
                    message = "Tên danh mục không được để trống."
                });
            }

            category.CategoryName = category.CategoryName.Trim();

            if (category.CategoryName.Length > 100)
            {
                return BadRequest(new
                {
                    message = "Tên danh mục không được vượt quá 100 ký tự."
                });
            }

            if (!string.IsNullOrWhiteSpace(category.Description))
            {
                category.Description = category.Description.Trim();
            }

            bool nameExists = await _context.Categories
                .AnyAsync(c => c.CategoryName == category.CategoryName);

            if (nameExists)
            {
                return Conflict(new
                {
                    message = "Tên danh mục đã tồn tại."
                });
            }

            // Khong nhan ID tu client khi tao moi.
            category.CategoryId = 0;

            _context.Categories.Add(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message = "Không thể thêm danh mục. Hãy kiểm tra lại dữ liệu."
                });
            }

            return CreatedAtAction(
                nameof(GetCategory),
                new { id = category.CategoryId },
                category);
        }

        // PUT: api/categories/5
        // Chi Admin va Warehouse duoc sua danh muc.
        [Authorize(Roles = "Admin,Warehouse")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(
            int id,
            [FromBody] Category category)
        {
            if (category == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu danh mục không hợp lệ."
                });
            }

            if (id != category.CategoryId)
            {
                return BadRequest(new
                {
                    message = "Mã danh mục không trùng khớp."
                });
            }

            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return BadRequest(new
                {
                    message = "Tên danh mục không được để trống."
                });
            }

            category.CategoryName = category.CategoryName.Trim();

            if (category.CategoryName.Length > 100)
            {
                return BadRequest(new
                {
                    message = "Tên danh mục không được vượt quá 100 ký tự."
                });
            }

            if (!string.IsNullOrWhiteSpace(category.Description))
            {
                category.Description = category.Description.Trim();
            }

            var existing = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (existing == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy danh mục cần cập nhật."
                });
            }

            bool nameExists = await _context.Categories
                .AnyAsync(c =>
                    c.CategoryId != id &&
                    c.CategoryName == category.CategoryName);

            if (nameExists)
            {
                return Conflict(new
                {
                    message = "Tên danh mục đã được sử dụng."
                });
            }

            existing.CategoryName = category.CategoryName;
            existing.Description = category.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Categories.AnyAsync(
                        c => c.CategoryId == id))
                {
                    return NotFound(new
                    {
                        message = "Danh mục không còn tồn tại."
                    });
                }

                throw;
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message = "Không thể cập nhật danh mục. Hãy kiểm tra dữ liệu."
                });
            }

            return NoContent();
        }

        // DELETE: api/categories/5
        // Chi Admin va Warehouse duoc xoa danh muc.
        [Authorize(Roles = "Admin,Warehouse")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy danh mục."
                });
            }

            // Khong xoa danh muc dang duoc san pham su dung.
            bool hasProducts = await _context.Products
                .AnyAsync(p => p.CategoryId == id);

            if (hasProducts)
            {
                return Conflict(new
                {
                    message = "Không thể xóa danh mục vì vẫn còn sản phẩm thuộc danh mục này."
                });
            }

            _context.Categories.Remove(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message = "Không thể xóa danh mục vì dữ liệu đang được sử dụng."
                });
            }

            return NoContent();
        }
    }
}
