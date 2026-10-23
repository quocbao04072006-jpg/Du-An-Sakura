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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        // GET: api/categories/search?keyword=... (Hỗ trợ tìm cả theo ID chính xác hoặc theo tên/mô tả gần đúng)
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Category>>> SearchCategories([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return await _context.Categories.AsNoTracking().ToListAsync();
            }

            keyword = keyword.Trim();

            // Kiểm tra xem từ khóa người dùng nhập vào có phải là một số nguyên (ID) hay không
            if (int.TryParse(keyword, out int searchId))
            {
                var categoriesById = await _context.Categories
                    .AsNoTracking()
                    .Where(c => c.CategoryId == searchId || c.CategoryName.Contains(keyword) || (c.Description != null && c.Description.Contains(keyword)))
                    .ToListAsync();

                return Ok(categoriesById);
            }

            // Nếu là chữ, tìm kiếm gần đúng theo Tên danh mục hoặc Mô tả
            var categoriesByName = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryName.Contains(keyword) || (c.Description != null && c.Description.Contains(keyword)))
                .ToListAsync();

            return Ok(categoriesByName);
        }

        // GET: api/categories/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category == null ? NotFound() : Ok(category);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                return BadRequest(new { message = "Tên danh mục không được để trống." });
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.CategoryId }, category);
        }

        // PUT: api/categories/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest(new { message = "Mã danh mục không trùng khớp." });
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Categories.AnyAsync(c => c.CategoryId == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/categories/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}