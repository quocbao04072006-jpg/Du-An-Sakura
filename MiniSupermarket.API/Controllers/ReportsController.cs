
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ReportsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET: api/reports/daily?date=2026-10-22
        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyReport(
            [FromQuery] DateTime? date)
        {
            try
            {
                DateTime targetDate = date ?? DateTime.Today;
                DateTime startDate = targetDate.Date;
                DateTime endDate = startDate.AddDays(1);

                // 1. Lay danh sach hoa don trong ngay
                var orders = await _context.Orders
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .Where(o => o.OrderDate >= startDate &&
                                o.OrderDate < endDate)
                    .ToListAsync();

                // 2. Tong so hoa don trong ngay
                int totalOrders = orders.Count;

                // 3. Tong doanh thu trong ngay
                decimal totalRevenue = orders.Sum(o => o.TotalAmount);

                // 4. Tim san pham ban chay nhat trong ngay
                var bestSellingProduct = orders
                    .SelectMany(o => o.OrderDetails)
                    .Where(od => od.Product != null &&
                                 !string.IsNullOrEmpty(
                                     od.Product.ProductName))
                    .GroupBy(od => od.Product!.ProductName)
                    .Select(g => new
                    {
                        ProductName = g.Key,
                        TotalQty = g.Sum(x => x.Quantity)
                    })
                    .OrderByDescending(x => x.TotalQty)
                    .FirstOrDefault()?.ProductName
                    ?? "Chua co du lieu";

                return Ok(new
                {
                    TotalOrders = totalOrders,
                    TotalRevenue = totalRevenue,
                    BestSellingProduct = bestSellingProduct
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Loi server khi lay bao cao: " + ex.Message
                });
            }
        }

        // GET: api/reports/dashboard-summary
        [HttpGet("dashboard-summary")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);

                // 1. Doanh thu hom nay
                decimal todayRevenue = await _context.Orders
                    .Where(o => o.OrderDate >= today &&
                                o.OrderDate < tomorrow)
                    .Select(o => (decimal?)o.TotalAmount)
                    .SumAsync() ?? 0m;

                // 2. Tong so hoa don
                int totalOrders = await _context.Orders
                    .CountAsync();

                // Cac chi so kho dung cung quy uoc voi man hinh Inventory:
                // sap het = ton > 0 va <= 10; het hang = ton <= 0.
                int totalProducts = await _context.Products.CountAsync();
                long totalStockQuantity = await _context.Products
                    .Select(p => (long?)p.StockQuantity)
                    .SumAsync() ?? 0L;
                int lowStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity > 0 && p.StockQuantity <= 10);
                int outOfStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity <= 0);

                return Ok(new
                {
                    TodayRevenue = todayRevenue,
                    TotalOrders = totalOrders,
                    TotalProducts = totalProducts,
                    TotalStockQuantity = totalStockQuantity,
                    LowStockProducts = lowStockProducts,
                    OutOfStockProducts = outOfStockProducts
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Loi khi lay Dashboard: " + ex.Message
                });
            }
        }

        // GET: api/reports/inventory-summary
        // Endpoint dung chung cho WinForms va trang web de cac KPI kho cung mot nguon du lieu.
        [HttpGet("inventory-summary")]
        public async Task<IActionResult> GetInventorySummary()
        {
            try
            {
                int totalProducts = await _context.Products.CountAsync();
                long totalStockQuantity = await _context.Products
                    .Select(p => (long?)p.StockQuantity)
                    .SumAsync() ?? 0L;
                int lowStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity > 0 && p.StockQuantity <= 10);
                int outOfStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity <= 0);

                return Ok(new
                {
                    TotalProducts = totalProducts,
                    TotalStockQuantity = totalStockQuantity,
                    LowStockProducts = lowStockProducts,
                    OutOfStockProducts = outOfStockProducts,
                    LowStockThreshold = 10
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Loi khi lay thong ke ton kho: " + ex.Message
                });
            }
        }

        // GET: api/reports/analytics?from=2026-10-01&to=2026-10-24
        // Nguồn dữ liệu dùng chung cho WinForms và website.
        [HttpGet("analytics")]
        public async Task<IActionResult> GetAnalytics(
            [FromQuery(Name = "from")] DateTime? fromDate,
            [FromQuery(Name = "to")] DateTime? toDate)
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime startDate = (fromDate ?? new DateTime(today.Year, today.Month, 1)).Date;
                DateTime finalDate = (toDate ?? today).Date;

                if (startDate > finalDate)
                {
                    return BadRequest(new
                    {
                        message = "Ngày bắt đầu không được lớn hơn ngày kết thúc."
                    });
                }

                DateTime endExclusive = finalDate.AddDays(1);

                var ordersQuery = _context.Orders
                    .AsNoTracking()
                    .Where(o => o.OrderDate >= startDate && o.OrderDate < endExclusive);

                decimal totalRevenue = await ordersQuery
                    .Select(o => (decimal?)o.TotalAmount)
                    .SumAsync() ?? 0m;

                int totalOrders = await ordersQuery.CountAsync();
                decimal averageOrderValue = totalOrders > 0
                    ? totalRevenue / totalOrders
                    : 0m;

                // Gộp doanh thu theo giờ trong ngày. Khi chọn nhiều ngày,
                // mỗi khung giờ sẽ là tổng của khung giờ đó trên toàn kỳ.
                var hourlyRows = await ordersQuery
                    .GroupBy(o => o.OrderDate.Hour)
                    .Select(g => new
                    {
                        Hour = g.Key,
                        Revenue = g.Sum(o => o.TotalAmount)
                    })
                    .ToListAsync();

                var revenueByHour = Enumerable.Range(0, 24)
                    .Select(hour => new
                    {
                        Hour = hour,
                        Revenue = hourlyRows
                            .Where(row => row.Hour == hour)
                            .Select(row => row.Revenue)
                            .FirstOrDefault()
                    })
                    .ToList();

                // Tỉ trọng doanh thu theo ngành hàng lấy từ dòng hàng đã bán.
                var categoryRows = await _context.OrderDetails
                    .AsNoTracking()
                    .Where(od =>
                        od.Order != null &&
                        od.Order.OrderDate >= startDate &&
                        od.Order.OrderDate < endExclusive &&
                        od.Product != null)
                    .GroupBy(od => od.Product!.Category != null
                        ? od.Product.Category.CategoryName
                        : "Chưa phân loại")
                    .Select(g => new
                    {
                        CategoryName = g.Key,
                        Quantity = g.Sum(od => od.Quantity),
                        Revenue = g.Sum(od => od.UnitPrice * od.Quantity)
                    })
                    .OrderByDescending(x => x.Revenue)
                    .ToListAsync();

                // Top sản phẩm: xếp theo số lượng bán, sau đó theo doanh thu.
                var topRows = await _context.OrderDetails
                    .AsNoTracking()
                    .Where(od =>
                        od.Order != null &&
                        od.Order.OrderDate >= startDate &&
                        od.Order.OrderDate < endExclusive &&
                        od.Product != null)
                    .GroupBy(od => new
                    {
                        od.ProductId,
                        ProductName = od.Product!.ProductName,
                        Barcode = od.Product.Barcode
                    })
                    .Select(g => new
                    {
                        g.Key.ProductId,
                        g.Key.ProductName,
                        g.Key.Barcode,
                        Quantity = g.Sum(od => od.Quantity),
                        Revenue = g.Sum(od => od.UnitPrice * od.Quantity)
                    })
                    .OrderByDescending(x => x.Quantity)
                    .ThenByDescending(x => x.Revenue)
                    .Take(10)
                    .ToListAsync();

                var topProducts = topRows
                    .Select((row, index) => new
                    {
                        Rank = index + 1,
                        row.ProductId,
                        row.Barcode,
                        row.ProductName,
                        Unit = "SP",
                        row.Quantity,
                        row.Revenue
                    })
                    .ToList();

                return Ok(new
                {
                    FromDate = startDate,
                    ToDate = finalDate,
                    TotalRevenue = totalRevenue,
                    TotalOrders = totalOrders,
                    AverageOrderValue = averageOrderValue,
                    RevenueByHour = revenueByHour,
                    RevenueByCategory = categoryRows,
                    TopProducts = topProducts
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Không thể lấy báo cáo phân tích doanh thu: " + ex.Message
                });
            }
        }

    }
}
