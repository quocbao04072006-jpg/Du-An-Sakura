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
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Customer)
                .Include(o => o.OrderDetails)
                    .ThenInclude(detail => detail.Product)
                .AsNoTracking()
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return Ok(orders);
        }

        // =========================================================
        // LỊCH SỬ GIAO DỊCH THEO KHÁCH HÀNG
        // =========================================================
        // GET: api/orders/customer/{customerId}
        // Trả về các hóa đơn của đúng khách hàng, kèm chi tiết sản phẩm.
        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetCustomerTransactionHistory(int customerId)
        {
            var customerExists = await _context.Customers
                .AsNoTracking()
                .AnyAsync(c => c.CustomerId == customerId);

            if (!customerExists)
                return NotFound(new { message = "Không tìm thấy khách hàng." });

            var orders = await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .AsNoTracking()
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new CustomerTransactionDto
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    PaymentMethod = o.PaymentMethod,
                    Items = o.OrderDetails
                        .OrderBy(d => d.OrderDetailId)
                        .Select(d => new CustomerTransactionItemDto
                        {
                            ProductId = d.ProductId,
                            ProductName = d.Product != null
                                ? d.Product.ProductName
                                : "Sản phẩm không còn tồn tại",
                            Quantity = d.Quantity,
                            UnitPrice = d.UnitPrice,
                            LineTotal = d.UnitPrice * d.Quantity
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(orders);
        }

        // Ghi nhận hóa đơn POS. Tiền mặt được kiểm tra ở WinForms;
        // chuyển khoản/Thẻ POS cần nhân viên xác nhận đã nhận tiền ngoài hệ thống.
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request)
        {
            if (request == null || request.Items == null || request.Items.Count == 0)
                return BadRequest(new { message = "Giỏ hàng trống, không thể thanh toán." });

            string? paymentMethod = NormalizePaymentMethod(request.PaymentMethod);
            if (paymentMethod == null)
                return BadRequest(new { message = "Phương thức thanh toán không hợp lệ. Chọn Tiền mặt, Chuyển khoản hoặc Thẻ POS." });

            if (paymentMethod != "Tiền mặt" && !request.ExternalPaymentConfirmed)
                return BadRequest(new { message = "Chưa xác nhận giao dịch chuyển khoản/Thẻ POS bên ngoài hệ thống." });

            if (request.Items.Any(item => item == null || item.ProductId <= 0 || item.Quantity <= 0))
                return BadRequest(new { message = "Sản phẩm hoặc số lượng trong giỏ hàng không hợp lệ." });

            if (string.IsNullOrWhiteSpace(request.CashierUsername))
                return BadRequest(new { message = "Thiếu thông tin nhân viên thu ngân." });

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.CashierUsername);
            if (user == null)
                return BadRequest(new { message = "Không tìm thấy thông tin nhân viên thu ngân." });

            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Aggregate quantities per product so duplicate item lines cannot exceed stock.
                var requestedQuantities = request.Items
                    .GroupBy(item => item.ProductId)
                    .ToDictionary(group => group.Key, group => group.Sum(item => item.Quantity));
                var productIds = requestedQuantities.Keys.ToList();
                var products = await _context.Products
                    .Where(product => productIds.Contains(product.ProductId))
                    .ToDictionaryAsync(product => product.ProductId);

                foreach (var requested in requestedQuantities)
                {
                    if (!products.TryGetValue(requested.Key, out var product))
                        throw new InvalidOperationException($"Không tìm thấy sản phẩm có ID: {requested.Key}.");
                    if (product.StockQuantity < requested.Value)
                        throw new InvalidOperationException($"Sản phẩm '{product.ProductName}' không đủ tồn kho (còn {product.StockQuantity}).");
                }

                int? customerId = null;
                Customer? customer = null;
                if (!string.IsNullOrWhiteSpace(request.CustomerPhone))
                {
                    customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == request.CustomerPhone.Trim());
                    if (customer != null)
                        customerId = customer.CustomerId;
                }

                var order = new Order
                {
                    UserId = user.Id,
                    CustomerId = customerId,
                    OrderDate = DateTime.Now,
                    PaymentMethod = paymentMethod,
                    TotalAmount = 0m
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                decimal totalAmount = 0m;
                foreach (var item in request.Items)
                {
                    var product = products[item.ProductId];
                    product.StockQuantity -= item.Quantity;
                    decimal lineTotal = product.Price * item.Quantity;
                    totalAmount += lineTotal;
                    _context.OrderDetails.Add(new OrderDetail
                    {
                        OrderId = order.OrderId,
                        ProductId = product.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    });
                }

                order.TotalAmount = totalAmount;
                if (customer != null)
                    customer.RewardPoints += 10;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    message = "Đã ghi nhận hóa đơn thanh toán thành công.",
                    orderId = order.OrderId,
                    totalAmount,
                    paymentMethod
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(new { message = "Thanh toán thất bại: " + ex.Message });
            }
        }

        private static string? NormalizePaymentMethod(string? method)
        {
            if (string.IsNullOrWhiteSpace(method)) return "Tiền mặt";
            string value = method.Trim();
            if (value.Equals("Tiền mặt", StringComparison.OrdinalIgnoreCase)) return "Tiền mặt";
            if (value.Equals("Chuyển khoản", StringComparison.OrdinalIgnoreCase)) return "Chuyển khoản";
            if (value.Equals("Thẻ POS", StringComparison.OrdinalIgnoreCase) || value.Equals("POS", StringComparison.OrdinalIgnoreCase)) return "Thẻ POS";
            return null;
        }
    }

    public class CustomerTransactionDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = "Tiền mặt";
        public List<CustomerTransactionItemDto> Items { get; set; } =
            new List<CustomerTransactionItemDto>();
    }

    public class CustomerTransactionItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class CheckoutRequestDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public string? CustomerPhone { get; set; }
        public string PaymentMethod { get; set; } = "Tiền mặt";
        public bool ExternalPaymentConfirmed { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        // The API intentionally calculates the actual unit price from SQL Server;
        // this property exists only for compatibility with clients.
        public decimal UnitPrice { get; set; }
    }
}
