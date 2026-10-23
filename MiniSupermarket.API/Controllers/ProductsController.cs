using System.Globalization;
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
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        // Giữ endpoint cũ để các màn hình hiện tại như POS vẫn hoạt động.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .ToListAsync();
        }

        // GET: api/products/paged?keyword=senka&pageNumber=1&pageSize=5&categoryId=1
        // Phân trang tại SQL Server, tìm theo tên/mã vạch và lọc theo danh mục.
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResultDto<ProductListItemDto>>> GetPagedProducts(
            [FromQuery] string keyword = "",
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] int? categoryId = null)
        {
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            // Chỉ cho phép kích thước trang từ 1 đến 50 để tránh tải quá nhiều dữ liệu.
            if (pageSize < 1 || pageSize > 50)
            {
                pageSize = 5;
            }

            // Eager Loading lấy kèm tên danh mục; AsNoTracking tối ưu truy vấn chỉ đọc.
            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim();
                query = query.Where(p =>
                    p.ProductName.Contains(search) ||
                    p.Barcode.Contains(search));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            // Đếm tổng số bản ghi sau khi áp dụng điều kiện tìm kiếm/lọc.
            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // Nếu xóa bản ghi ở trang cuối, tự điều chỉnh về trang cuối còn tồn tại.
            if (totalPages > 0 && pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            if (totalPages == 0)
            {
                pageNumber = 1;
            }

            // Skip/Take được EF Core chuyển thành phân trang ở SQL Server.
            List<Product> products = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Chuyển Entity sang DTO trước khi trả JSON.
            List<ProductListItemDto> items = products.Select(p => new ProductListItemDto
            {
                ProductId = p.ProductId,
                Barcode = p.Barcode,
                ProductName = p.ProductName,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
            }).ToList();

            var result = new PagedResultDto<ProductListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            };

            return Ok(result);
        }

        // GET: api/products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            return product == null ? NotFound() : Ok(product);
        }

        // GET: api/products/barcode/123456
        [HttpGet("barcode/{barcode}")]
        public async Task<ActionResult<Product>> GetByBarcode(string barcode)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Barcode == barcode);

            return product == null ? NotFound() : Ok(product);
        }

        // GET: api/products/next-barcode
        // Sinh mã vạch 13 chữ số bắt đầu bằng 893. Mã mẫu đầu tiên theo yêu cầu là
        // 8934567890128; những mã đang có sẽ được kiểm tra để không tạo trùng.
        [HttpGet("next-barcode")]
        public async Task<ActionResult<string>> GetNextBarcode()
        {
            const long firstBarcode = 8934567890128L;
            const long lastBarcode = 8939999999999L;

            List<string> existingBarcodes = await _context.Products
                .AsNoTracking()
                .Select(p => p.Barcode)
                .ToListAsync();

            long maxExisting = firstBarcode - 1;

            foreach (string barcode in existingBarcodes)
            {
                if (string.IsNullOrEmpty(barcode) ||
                    barcode.Length != 13 ||
                    !barcode.StartsWith("893", StringComparison.Ordinal))
                {
                    continue;
                }

                long numericBarcode;
                if (long.TryParse(barcode, NumberStyles.None, CultureInfo.InvariantCulture, out numericBarcode)
                    && numericBarcode > maxExisting)
                {
                    maxExisting = numericBarcode;
                }
            }

            long candidate = maxExisting + 1;

            // Kiểm tra lại mã ứng viên trước khi trả về.
            while (candidate <= lastBarcode &&
                   await _context.Products.AnyAsync(
                       p => p.Barcode == candidate.ToString(CultureInfo.InvariantCulture)))
            {
                candidate++;
            }

            // Trường hợp đã hết dải mã lớn nhất, tìm mã trống từ đầu dải.
            if (candidate > lastBarcode)
            {
                candidate = firstBarcode;
                while (candidate <= lastBarcode &&
                       await _context.Products.AnyAsync(
                           p => p.Barcode == candidate.ToString(CultureInfo.InvariantCulture)))
                {
                    candidate++;
                }
            }

            if (candidate > lastBarcode)
            {
                return Conflict(new { message = "Đã hết dải mã vạch khả dụng bắt đầu bằng 893." });
            }

            return Ok(new { barcode = candidate.ToString(CultureInfo.InvariantCulture) });
        }

        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Barcode) || string.IsNullOrWhiteSpace(product.ProductName))
            {
                return BadRequest(new { message = "Mã vạch và tên sản phẩm không được để trống." });
            }

            product.Barcode = product.Barcode.Trim();

            bool barcodeExists = await _context.Products
                .AnyAsync(p => p.Barcode == product.Barcode);

            if (barcodeExists)
            {
                return Conflict(new
                {
                    message = "Mã vạch '" + product.Barcode + "' đã tồn tại. Vui lòng dùng mã khác."
                });
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        // PUT: api/products/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest(new { message = "Mã sản phẩm không trùng khớp." });
            }

            if (string.IsNullOrWhiteSpace(product.Barcode) || string.IsNullOrWhiteSpace(product.ProductName))
            {
                return BadRequest(new { message = "Mã vạch và tên sản phẩm không được để trống." });
            }

            product.Barcode = product.Barcode.Trim();

            Product? existing = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (existing == null)
            {
                return NotFound(new { message = "Không tìm thấy sản phẩm cần cập nhật." });
            }

            bool barcodeUsedByAnotherProduct = await _context.Products
                .AnyAsync(p => p.ProductId != id && p.Barcode == product.Barcode);

            if (barcodeUsedByAnotherProduct)
            {
                return Conflict(new
                {
                    message = "Mã vạch '" + product.Barcode + "' đã được sử dụng bởi sản phẩm khác."
                });
            }

            // Cập nhật trên entity đang được EF Core theo dõi.
            existing.Barcode = product.Barcode;
            existing.ProductName = product.ProductName.Trim();
            existing.Price = product.Price;
            existing.StockQuantity = product.StockQuantity;
            existing.CategoryId = product.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Products.AnyAsync(p => p.ProductId == id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // DELETE: api/products/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
