using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
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

        // GET: api/reports/daily?date=2026-10-29
        [HttpGet("daily")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> GetDailyReport(
            [FromQuery] DateTime? date)
        {
            try
            {
                DateTime targetDate = (date ?? DateTime.Today).Date;
                DateTime endDate = targetDate.AddDays(1);

                var orders = await _context.Orders
                    .AsNoTracking()
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .Where(o => o.OrderDate >= targetDate
                             && o.OrderDate < endDate)
                    .ToListAsync();

                decimal totalRevenue = orders.Sum(o => o.TotalAmount);
                int totalOrders = orders.Count;

                var bestSellingProduct = orders
                    .SelectMany(o => o.OrderDetails)
                    .Where(od => od.Product != null
                              && !string.IsNullOrEmpty(
                                  od.Product.ProductName))
                    .GroupBy(od => od.Product!.ProductName)
                    .Select(g => new
                    {
                        ProductName = g.Key,
                        TotalQty = g.Sum(x => x.Quantity)
                    })
                    .OrderByDescending(x => x.TotalQty)
                    .FirstOrDefault()?.ProductName
                    ?? "Chưa có dữ liệu";

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
                    message = "Lỗi khi lấy báo cáo ngày: " + ex.Message
                });
            }
        }

        // GET: api/reports/dashboard-summary
        [HttpGet("dashboard-summary")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            try
            {
                DateTime today = DateTime.Today;
                DateTime tomorrow = today.AddDays(1);
                DateTime monthStart =
                    new DateTime(today.Year, today.Month, 1);
                DateTime nextMonth = monthStart.AddMonths(1);

                decimal todayRevenue = await _context.Orders
                    .AsNoTracking()
                    .Where(o => o.OrderDate >= today
                             && o.OrderDate < tomorrow)
                    .Select(o => (decimal?)o.TotalAmount)
                    .SumAsync() ?? 0m;

                int totalOrders =
                    await _context.Orders.CountAsync();

                int totalProducts =
                    await _context.Products.CountAsync();

                long totalStockQuantity = await _context.Products
                    .Select(p => (long?)p.StockQuantity)
                    .SumAsync() ?? 0L;

                int newCustomersToday = await _context.Customers
                    .CountAsync(c => c.CreatedAt.HasValue
                                  && c.CreatedAt.Value >= today
                                  && c.CreatedAt.Value < tomorrow);

                int newCustomersThisMonth = await _context.Customers
                    .CountAsync(c => c.CreatedAt.HasValue
                                  && c.CreatedAt.Value >= monthStart
                                  && c.CreatedAt.Value < nextMonth);

                int lowStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity > 0
                                  && p.StockQuantity <= 10);

                int outOfStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity <= 0);

                int itemsLowStock = await _context.Products
                    .CountAsync(p => p.StockQuantity <= 10);

                return Ok(new
                {
                    TodayRevenue = todayRevenue,
                    TotalOrders = totalOrders,
                    TotalProducts = totalProducts,
                    TotalStockQuantity = totalStockQuantity,
                    LowStockProducts = lowStockProducts,
                    OutOfStockProducts = outOfStockProducts,
                    NewCustomersToday = newCustomersToday,
                    NewCustomersThisMonth = newCustomersThisMonth,
                    ItemsLowStock = itemsLowStock
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Không thể lấy dữ liệu Dashboard: "
                              + ex.Message
                });
            }
        }

        // GET: api/reports/inventory-summary
        [HttpGet("inventory-summary")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> GetInventorySummary()
        {
            try
            {
                int totalProducts =
                    await _context.Products.CountAsync();

                long totalStockQuantity = await _context.Products
                    .Select(p => (long?)p.StockQuantity)
                    .SumAsync() ?? 0L;

                int lowStockProducts = await _context.Products
                    .CountAsync(p => p.StockQuantity > 0
                                  && p.StockQuantity <= 10);

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
                    message = "Lỗi khi lấy thống kê tồn kho: "
                              + ex.Message
                });
            }
        }

        // GET: api/reports/analytics?from=2026-10-01&to=2026-10-29
        [HttpGet("analytics")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> GetAnalytics(
            [FromQuery(Name = "from")] DateTime? fromDate,
            [FromQuery(Name = "to")] DateTime? toDate)
        {
            try
            {
                DateTime today = DateTime.Today;

                DateTime startDate =
                    (fromDate ?? new DateTime(
                        today.Year, today.Month, 1)).Date;

                DateTime finalDate = (toDate ?? today).Date;

                if (startDate > finalDate)
                {
                    return BadRequest(new
                    {
                        message =
                            "Ngày bắt đầu không được lớn hơn ngày kết thúc."
                    });
                }

                int dayCount =
                    (finalDate - startDate).Days + 1;

                if (dayCount > 366)
                {
                    return BadRequest(new
                    {
                        message =
                            "Khoảng thời gian báo cáo không được vượt quá 366 ngày."
                    });
                }

                DateTime endExclusive = finalDate.AddDays(1);

                var ordersQuery = _context.Orders
                    .AsNoTracking()
                    .Where(o => o.OrderDate >= startDate
                             && o.OrderDate < endExclusive);

                decimal totalRevenue = await ordersQuery
                    .Select(o => (decimal?)o.TotalAmount)
                    .SumAsync() ?? 0m;

                int totalOrders = await ordersQuery.CountAsync();

                decimal averageOrderValue = totalOrders > 0
                    ? totalRevenue / totalOrders
                    : 0m;

                // Doanh thu tổng hợp theo giờ.
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

                // Quan trọng:
                // Nhóm theo Year/Month/Day thay vì OrderDate.Date.
                // Cách này ổn định hơn khi dịch truy vấn sang SQL.
                var dailyRows = await ordersQuery
                    .GroupBy(o => new
                    {
                        Year = o.OrderDate.Year,
                        Month = o.OrderDate.Month,
                        Day = o.OrderDate.Day
                    })
                    .Select(g => new
                    {
                        g.Key.Year,
                        g.Key.Month,
                        g.Key.Day,
                        Revenue = g.Sum(o => o.TotalAmount),
                        OrderCount = g.LongCount()
                    })
                    .ToListAsync();

                var dailyLookup = dailyRows.ToDictionary(
                    row => new DateTime(
                        row.Year, row.Month, row.Day),
                    row => row);

                // Trả đủ từng ngày trong khoảng chọn.
                // Ngày không có hóa đơn có doanh thu bằng 0.
                var revenueByDay = Enumerable.Range(0, dayCount)
                    .Select(offset =>
                    {
                        DateTime currentDate =
                            startDate.AddDays(offset).Date;

                        bool found = dailyLookup.TryGetValue(
                            currentDate, out var row);

                        return new
                        {
                            Date = currentDate,
                            Revenue = found ? row!.Revenue : 0m,
                            OrderCount = found
                                ? row!.OrderCount
                                : 0L
                        };
                    })
                    .ToList();

                // Doanh thu theo danh mục.
                var categoryRows = await _context.OrderDetails
                    .AsNoTracking()
                    .Where(od =>
                        od.Order != null
                        && od.Order.OrderDate >= startDate
                        && od.Order.OrderDate < endExclusive
                        && od.Product != null)
                    .GroupBy(od =>
                        od.Product!.Category != null
                            ? od.Product.Category.CategoryName
                            : "Chưa phân loại")
                    .Select(g => new
                    {
                        CategoryName = g.Key,
                        Quantity = g.Sum(od => od.Quantity),
                        Revenue = g.Sum(
                            od => od.UnitPrice * od.Quantity)
                    })
                    .OrderByDescending(x => x.Revenue)
                    .ToListAsync();

                // Top 5 sản phẩm bán chạy bằng LINQ.
                var topRows = await _context.OrderDetails
                    .AsNoTracking()
                    .Where(od =>
                        od.Order != null
                        && od.Order.OrderDate >= startDate
                        && od.Order.OrderDate < endExclusive
                        && od.Product != null)
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
                        Revenue = g.Sum(
                            od => od.UnitPrice * od.Quantity)
                    })
                    .OrderByDescending(x => x.Quantity)
                    .ThenByDescending(x => x.Revenue)
                    .Take(5)
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
                    RevenueByDay = revenueByDay,
                    RevenueByCategory = categoryRows,
                    TopProducts = topProducts
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message =
                        "Không thể lấy báo cáo phân tích doanh thu: "
                        + ex.Message
                });
            }
        }

        // GET:
        // api/reports/revenue-by-day?from=2026-10-01&to=2026-10-29
        // Ưu tiên Stored Procedure. Nếu lỗi hoặc dữ liệu không khớp,
        // tự động dùng EF Core làm phương án dự phòng.
        [HttpGet("revenue-by-day")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> GetRevenueByDay(
            [FromQuery(Name = "from")] DateTime? fromDate,
            [FromQuery(Name = "to")] DateTime? toDate)
        {
            DateTime today = DateTime.Today;

            DateTime startDate =
                (fromDate ?? new DateTime(
                    today.Year, today.Month, 1)).Date;

            DateTime endDate = (toDate ?? today).Date;

            if (startDate > endDate)
            {
                return BadRequest(new
                {
                    message =
                        "Ngày bắt đầu không được lớn hơn ngày kết thúc."
                });
            }

            int dayCount = (endDate - startDate).Days + 1;

            if (dayCount > 366)
            {
                return BadRequest(new
                {
                    message =
                        "Khoảng thời gian báo cáo không được vượt quá 366 ngày."
                });
            }

            try
            {
                var rows = new List<DailyRevenueAdoRow>();
                bool storedProcedureWorked = false;

                // Thử lấy dữ liệu bằng ADO.NET và Stored Procedure.
                try
                {
                    string? connectionString =
                        _context.Database.GetConnectionString();

                    if (!string.IsNullOrWhiteSpace(connectionString))
                    {
                        await using var connection =
                            new SqlConnection(connectionString);

                        await using var command = new SqlCommand(
                            "dbo.sp_GetRevenueByDay", connection)
                        {
                            CommandType =
                                CommandType.StoredProcedure
                        };

                        command.Parameters.Add(
                            new SqlParameter(
                                "@FromDate", SqlDbType.Date)
                            {
                                Value = startDate
                            });

                        command.Parameters.Add(
                            new SqlParameter(
                                "@ToDate", SqlDbType.Date)
                            {
                                Value = endDate
                            });

                        await connection.OpenAsync();

                        await using var reader =
                            await command.ExecuteReaderAsync();

                        int dateOrdinal =
                            reader.GetOrdinal("SalesDate");

                        int revenueOrdinal =
                            reader.GetOrdinal("Revenue");

                        int countOrdinal =
                            reader.GetOrdinal("OrderCount");

                        while (await reader.ReadAsync())
                        {
                            rows.Add(new DailyRevenueAdoRow
                            {
                                Date = reader
                                    .GetDateTime(dateOrdinal).Date,

                                Revenue = reader
                                    .GetDecimal(revenueOrdinal),

                                OrderCount = reader
                                    .GetInt64(countOrdinal)
                            });
                        }

                        storedProcedureWorked = true;
                    }
                }
                catch (Exception)
                {
                    // Nếu Stored Procedure lỗi, chuyển sang EF Core.
                    storedProcedureWorked = false;
                }

                // Tính tổng doanh thu thật trong bảng Orders để đối chiếu.
                decimal databaseTotal = await _context.Orders
                    .AsNoTracking()
                    .Where(o =>
                        o.OrderDate >= startDate
                        && o.OrderDate < endDate.AddDays(1))
                    .Select(o => (decimal?)o.TotalAmount)
                    .SumAsync() ?? 0m;

                decimal storedProcedureTotal =
                    rows.Sum(row => row.Revenue);

                bool storedProcedureDataValid =
                    storedProcedureWorked
                    && (rows.Count > 0 || databaseTotal == 0m)
                    && Math.Abs(
                        storedProcedureTotal - databaseTotal) < 0.01m;

                if (!storedProcedureDataValid)
                {
                    List<DailyRevenueResult> fallbackRows =
                        await BuildRevenueByDayFromEfAsync(
                            startDate, endDate);

                    return Ok(fallbackRows);
                }

                var byDate = rows
                    .GroupBy(row => row.Date.Date)
                    .ToDictionary(
                        group => group.Key,
                        group => new
                        {
                            Revenue = group.Sum(
                                row => row.Revenue),

                            OrderCount = group.Sum(
                                row => row.OrderCount)
                        });

                var result = Enumerable.Range(0, dayCount)
                    .Select(offset =>
                    {
                        DateTime currentDate =
                            startDate.AddDays(offset).Date;

                        bool found = byDate.TryGetValue(
                            currentDate, out var row);

                        return new DailyRevenueResult
                        {
                            Date = currentDate,
                            Revenue = found
                                ? row!.Revenue
                                : 0m,
                            OrderCount = found
                                ? row!.OrderCount
                                : 0L
                        };
                    })
                    .ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message =
                        "Không thể lấy doanh thu theo ngày bằng "
                        + "Stored Procedure hoặc EF Core.",
                    detail = ex.Message
                });
            }
        }

        // GET: api/reports/export/products.csv
        [HttpGet("export/products.csv")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> ExportProductsCsv()
        {
            var products = await _context.Products
                .AsNoTracking()
                .Include(p => p.Category)
                .OrderBy(p => p.ProductName)
                .ToListAsync();

            var lines = new List<string>
            {
                "MaSP,MaVach,TenSanPham,DanhMuc,DonGia,TonKho"
            };

            lines.AddRange(products.Select(p => CsvLine(
                p.ProductId.ToString(),
                p.Barcode,
                p.ProductName,
                p.Category?.CategoryName ?? "",
                p.Price.ToString(
                    "0.##",
                    System.Globalization.CultureInfo.InvariantCulture),
                p.StockQuantity.ToString())));

            return CsvDownload(
                lines, "danh-muc-san-pham.csv");
        }

        // GET:
        // api/reports/export/orders.csv?from=2026-10-01&to=2026-10-29
        [HttpGet("export/orders.csv")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> ExportOrdersCsv(
            [FromQuery(Name = "from")] DateTime? fromDate,
            [FromQuery(Name = "to")] DateTime? toDate)
        {
            DateTime startDate =
                (fromDate ?? DateTime.Today.AddDays(-30)).Date;

            DateTime endDate =
                (toDate ?? DateTime.Today).Date;

            if (startDate > endDate)
            {
                return BadRequest(new
                {
                    message =
                        "Ngày bắt đầu không được lớn hơn ngày kết thúc."
                });
            }

            DateTime endExclusive = endDate.AddDays(1);

            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Customer)
                .Include(o => o.User)
                .Where(o =>
                    o.OrderDate >= startDate
                    && o.OrderDate < endExclusive)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var lines = new List<string>
            {
                "MaHoaDon,NgayLap,ThuNgan,KhachHang,"
                + "TamTinh,GiamGia,DiemDaDoi,ThanhTien,PhuongThuc"
            };

            lines.AddRange(orders.Select(o => CsvLine(
                o.OrderId.ToString(),
                o.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"),
                o.User?.Username ?? "",
                o.Customer?.CustomerName ?? "Khách vãng lai",
                o.SubtotalAmount.ToString(
                    "0.##",
                    System.Globalization.CultureInfo.InvariantCulture),
                o.DiscountAmount.ToString(
                    "0.##",
                    System.Globalization.CultureInfo.InvariantCulture),
                o.RewardPointsRedeemed.ToString(),
                o.TotalAmount.ToString(
                    "0.##",
                    System.Globalization.CultureInfo.InvariantCulture),
                o.PaymentMethod)));

            return CsvDownload(
                lines, "bao-cao-hoa-don.csv");
        }

        private IActionResult CsvDownload(
            IEnumerable<string> lines,
            string fileName)
        {
            // BOM UTF-8 giúp Excel hiển thị đúng tiếng Việt.
            byte[] content = Encoding.UTF8.GetPreamble()
                .Concat(
                    Encoding.UTF8.GetBytes(
                        string.Join("\r\n", lines)))
                .ToArray();

            return File(
                content,
                "text/csv; charset=utf-8",
                fileName);
        }

        private static string CsvLine(
            params string[] values)
        {
            return string.Join(",", values.Select(value =>
            {
                string safe = value ?? string.Empty;
                return "\"" + safe.Replace("\"", "\"\"") + "\"";
            }));
        }

        // Dự phòng bằng EF Core, luôn trả đủ các ngày trong kỳ.
        private async Task<List<DailyRevenueResult>>
            BuildRevenueByDayFromEfAsync(
                DateTime startDate,
                DateTime endDate)
        {
            DateTime endExclusive = endDate.AddDays(1);

            var groupedRows = await _context.Orders
                .AsNoTracking()
                .Where(o =>
                    o.OrderDate >= startDate
                    && o.OrderDate < endExclusive)
                .GroupBy(o => new
                {
                    Year = o.OrderDate.Year,
                    Month = o.OrderDate.Month,
                    Day = o.OrderDate.Day
                })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    g.Key.Day,
                    Revenue = g.Sum(o => o.TotalAmount),
                    OrderCount = g.LongCount()
                })
                .ToListAsync();

            var byDate = groupedRows.ToDictionary(
                row => new DateTime(
                    row.Year,
                    row.Month,
                    row.Day),
                row => row);

            int dayCount =
                (endDate.Date - startDate.Date).Days + 1;

            return Enumerable.Range(0, dayCount)
                .Select(offset =>
                {
                    DateTime currentDate =
                        startDate.Date.AddDays(offset);

                    bool found = byDate.TryGetValue(
                        currentDate, out var row);

                    return new DailyRevenueResult
                    {
                        Date = currentDate,
                        Revenue = found
                            ? row!.Revenue
                            : 0m,
                        OrderCount = found
                            ? row!.OrderCount
                            : 0L
                    };
                })
                .ToList();
        }

        private sealed class DailyRevenueResult
        {
            public DateTime Date { get; set; }
            public decimal Revenue { get; set; }
            public long OrderCount { get; set; }
        }

        private sealed class DailyRevenueAdoRow
        {
            public DateTime Date { get; set; }
            public decimal Revenue { get; set; }
            public long OrderCount { get; set; }
        }
    }
}