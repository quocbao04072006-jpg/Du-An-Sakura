
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        // Moi tai khoan da dang nhap deu co the xem san pham.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .OrderBy(p => p.ProductId)
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/products/paged?keyword=senka&pageNumber=1&pageSize=5&categoryId=1
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResultDto<ProductListItemDto>>>
            GetPagedProducts(
                [FromQuery] string keyword = "",
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 5,
                [FromQuery] int? categoryId = null)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, 50);

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
                query = query.Where(
                    p => p.CategoryId == categoryId.Value);
            }

            int totalItems = await query.CountAsync();

            int totalPages = (int)Math.Ceiling(
                totalItems / (double)pageSize);

            pageNumber = totalPages == 0
                ? 1
                : Math.Min(pageNumber, totalPages);

            List<Product> products = await query
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = products.Select(p => new ProductListItemDto
            {
                ProductId = p.ProductId,
                Barcode = p.Barcode,
                ProductName = p.ProductName,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null
                    ? p.Category.CategoryName
                    : string.Empty
            }).ToList();

            return Ok(new PagedResultDto<ProductListItemDto>
            {
                CurrentPage = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Items = items
            });
        }

        // GET: api/products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm."
                });
            }

            return Ok(product);
        }

        // GET: api/products/barcode/SAKURA10010
        [HttpGet("barcode/{barcode}")]
        public async Task<ActionResult<ProductListItemDto>> GetByBarcode(
            string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
            {
                return BadRequest(new
                {
                    message = "Mã vạch không được để trống."
                });
            }

            string normalized = barcode.Trim().ToUpperInvariant();

            var product = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(
                    p => p.Barcode.ToUpper() == normalized);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy mã vạch."
                });
            }

            return Ok(new ProductListItemDto
            {
                ProductId = product.ProductId,
                Barcode = product.Barcode,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.CategoryName
                    ?? string.Empty
            });
        }

        // GET: api/products/next-barcode
        // Chi Admin va Warehouse can ma vach moi de them san pham.
        [Authorize(Roles = "Admin,Warehouse")]
        [HttpGet("next-barcode")]
        public async Task<IActionResult> GetNextBarcode()
        {
            const int firstNumber = 10010;
            const int lastNumber = 99999;
            const string prefix = "SAKURA";

            var existingBarcodes = await _context.Products
                .AsNoTracking()
                .Select(p => p.Barcode)
                .ToListAsync();

            var usedNumbers = new HashSet<int>();

            foreach (string barcode in existingBarcodes)
            {
                if (string.IsNullOrWhiteSpace(barcode))
                {
                    continue;
                }

                string normalized = barcode.Trim().ToUpperInvariant();

                if (normalized.Length == 11 &&
                    normalized.StartsWith(
                        prefix,
                        StringComparison.Ordinal) &&
                    int.TryParse(
                        normalized.Substring(prefix.Length),
                        NumberStyles.None,
                        CultureInfo.InvariantCulture,
                        out int number) &&
                    number <= lastNumber)
                {
                    usedNumbers.Add(number);
                }
            }

            for (int number = firstNumber;
                 number <= lastNumber;
                 number++)
            {
                string candidate =
                    prefix + number.ToString(
                        "D5",
                        CultureInfo.InvariantCulture);

                if (!usedNumbers.Contains(number) &&
                    !await _context.Products.AnyAsync(
                        p => p.Barcode == candidate))
                {
                    return Ok(new { barcode = candidate });
                }
            }

            for (int number = 0;
                 number < firstNumber;
                 number++)
            {
                string candidate =
                    prefix + number.ToString(
                        "D5",
                        CultureInfo.InvariantCulture);

                if (!usedNumbers.Contains(number) &&
                    !await _context.Products.AnyAsync(
                        p => p.Barcode == candidate))
                {
                    return Ok(new { barcode = candidate });
                }
            }

            return Conflict(new
            {
                message = "Đã hết dải mã barcode SAKURA + 5 chữ số."
            });
        }

        private static bool IsValidBarcode(string? barcode)
        {
            return !string.IsNullOrWhiteSpace(barcode) &&
                   Regex.IsMatch(
                       barcode.Trim(),
                       @"^SAKURA\d{5}$",
                       RegexOptions.IgnoreCase);
        }

        private async Task<IActionResult?> ValidateProductAsync(
            Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Barcode) ||
                string.IsNullOrWhiteSpace(product.ProductName))
            {
                return BadRequest(new
                {
                    message =
                        "Mã vạch và tên sản phẩm không được để trống."
                });
            }

            product.Barcode =
                product.Barcode.Trim().ToUpperInvariant();

            product.ProductName = product.ProductName.Trim();

            if (!IsValidBarcode(product.Barcode))
            {
                return BadRequest(new
                {
                    message =
                        "Barcode phải có định dạng SAKURA + 5 chữ số, " +
                        "ví dụ: SAKURA10010."
                });
            }

            if (product.ProductName.Length > 150)
            {
                return BadRequest(new
                {
                    message =
                        "Tên sản phẩm không được vượt quá 150 ký tự."
                });
            }

            if (product.Price <= 0)
            {
                return BadRequest(new
                {
                    message = "Đơn giá phải lớn hơn 0."
                });
            }

            if (product.StockQuantity < 0)
            {
                return BadRequest(new
                {
                    message = "Tồn kho không được âm."
                });
            }

            if (product.CategoryId <= 0 ||
                !await _context.Categories.AnyAsync(
                    c => c.CategoryId == product.CategoryId))
            {
                return BadRequest(new
                {
                    message =
                        "Danh mục không tồn tại hoặc không hợp lệ."
                });
            }

            return null;
        }

        // POST: api/products
        // Chi Admin va Warehouse duoc them san pham.
        [Authorize(Roles = "Admin,Warehouse")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(
            [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu sản phẩm không hợp lệ."
                });
            }

            var validation = await ValidateProductAsync(product);

            if (validation != null)
            {
                return validation;
            }

            if (await _context.Products.AnyAsync(
                    p => p.Barcode.ToUpper() == product.Barcode))
            {
                return Conflict(new
                {
                    message =
                        $"Mã vạch '{product.Barcode}' đã tồn tại. " +
                        "Vui lòng dùng mã khác."
                });
            }

            // Khong nhan khoa chinh tu client khi them moi.
            product.ProductId = 0;
            product.Category = null;

            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message =
                        "Không thể thêm sản phẩm. " +
                        "Hãy kiểm tra mã vạch đã tồn tại hay chưa."
                });
            }

            return CreatedAtAction(
                nameof(GetProduct),
                new { id = product.ProductId },
                product);
        }

        // PUT: api/products/5
        // Admin va Warehouse duoc cap nhat thong tin va ton kho.
        [Authorize(Roles = "Admin,Warehouse")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(
            int id,
            [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new
                {
                    message = "Dữ liệu sản phẩm không hợp lệ."
                });
            }

            if (id != product.ProductId)
            {
                return BadRequest(new
                {
                    message = "Mã sản phẩm không trùng khớp."
                });
            }

            var existing = await _context.Products
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (existing == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm cần cập nhật."
                });
            }

            var validation = await ValidateProductAsync(product);

            if (validation != null)
            {
                return validation;
            }

            if (await _context.Products.AnyAsync(
                    p => p.ProductId != id &&
                         p.Barcode.ToUpper() == product.Barcode))
            {
                return Conflict(new
                {
                    message =
                        $"Mã vạch '{product.Barcode}' đã được sử dụng " +
                        "bởi sản phẩm khác."
                });
            }

            existing.Barcode = product.Barcode;
            existing.ProductName = product.ProductName;
            existing.Price = product.Price;
            existing.StockQuantity = product.StockQuantity;
            existing.CategoryId = product.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message =
                        "Không thể cập nhật sản phẩm. " +
                        "Hãy kiểm tra mã vạch và dữ liệu liên quan."
                });
            }

            // 204 tuong thich voi WinForms dang kiem tra IsSuccessStatusCode.
            return NoContent();
        }

        // DELETE: api/products/5
        // Chi Admin va Warehouse duoc xoa san pham.
        [Authorize(Roles = "Admin,Warehouse")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm."
                });
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return Conflict(new
                {
                    message =
                        "Không thể xóa sản phẩm vì sản phẩm " +
                        "có thể đã được sử dụng trong đơn hàng."
                });
            }

            return NoContent();
        }
    }
}
