using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<OrdersController> _logger;

        // Quy tắc quy đổi: 1 điểm = 1.000 đồng.
        // Nếu cửa hàng áp dụng tỷ lệ khác, đổi hằng số này
        // đồng thời với hằng số trong FormPOS.cs.
        private const decimal RewardPointValue = 1000m;

        // Quy tắc hiện tại: mua hàng không đổi điểm sẽ được cộng 10 điểm.
        private const int PointsEarnedPerOrder = 10;

        public OrdersController(
            SupermarketDbContext context,
            ILogger<OrdersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // =====================================================
        // 1. LẤY DANH SÁCH HÓA ĐƠN
        // GET: api/orders
        // =====================================================

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orders = await _context.Orders
                    .AsNoTracking()
                    .OrderByDescending(o => o.OrderDate)
                    .Select(o => new
                    {
                        o.OrderId,
                        o.OrderDate,
                        o.TotalAmount,
                        o.SubtotalAmount,
                        o.DiscountAmount,
                        o.RewardPointsRedeemed,
                        o.PaymentMethod,
                        o.UserId,
                        o.CustomerId,

                        User = o.User == null
                            ? null
                            : new
                            {
                                o.User.Id,
                                o.User.Username,
                                o.User.FullName,
                                o.User.Role,
                                o.User.IsActive
                            },

                        Customer = o.Customer == null
                            ? null
                            : new
                            {
                                o.Customer.CustomerId,
                                o.Customer.CustomerName,
                                o.Customer.PhoneNumber,
                                o.Customer.Address,
                                o.Customer.RewardPoints,
                                o.Customer.MembershipRank
                            },

                        OrderDetails = o.OrderDetails
                            .OrderBy(d => d.OrderDetailId)
                            .Select(d => new
                            {
                                d.OrderDetailId,
                                d.OrderId,
                                d.ProductId,
                                d.Quantity,
                                d.UnitPrice,

                                Product = d.Product == null
                                    ? null
                                    : new
                                    {
                                        d.Product.ProductId,
                                        d.Product.Barcode,
                                        d.Product.ProductName,
                                        d.Product.Price,
                                        d.Product.StockQuantity,
                                        d.Product.CategoryId
                                    }
                            })
                            .ToList()
                    })
                    .ToListAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Lỗi lấy danh sách hóa đơn.");

                return StatusCode(500, new
                {
                    message = "Không thể tải danh sách hóa đơn."
                });
            }
        }

        // =====================================================
        // 2. LỊCH SỬ MUA HÀNG CỦA KHÁCH
        // GET: api/orders/customer/1
        // =====================================================

        [HttpGet("customer/{customerId:int}")]
        public async Task<IActionResult> GetCustomerTransactionHistory(
            int customerId)
        {
            try
            {
                bool customerExists = await _context.Customers
                    .AsNoTracking()
                    .AnyAsync(c => c.CustomerId == customerId);

                if (!customerExists)
                {
                    return NotFound(new
                    {
                        message = "Không tìm thấy khách hàng."
                    });
                }

                var orders = await _context.Orders
                    .AsNoTracking()
                    .Where(o => o.CustomerId == customerId)
                    .OrderByDescending(o => o.OrderDate)
                    .Select(o => new CustomerTransactionDto
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        TotalAmount = o.TotalAmount,
                        SubtotalAmount = o.SubtotalAmount,
                        DiscountAmount = o.DiscountAmount,
                        RewardPointsRedeemed = o.RewardPointsRedeemed,
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
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Lỗi lấy lịch sử khách hàng ID {CustomerId}.",
                    customerId);

                return StatusCode(500, new
                {
                    message = "Không thể tải lịch sử mua hàng."
                });
            }
        }

        // =====================================================
        // 3. THANH TOÁN POS
        // POST: api/orders/checkout
        //
        // - Giá bán lấy từ database.
        // - Kiểm tra tồn kho ở phía API.
        // - Đổi điểm linh hoạt theo số điểm hiện có.
        // - Mức giảm không vượt quá tổng tiền hàng.
        // - Đổi điểm: trừ số điểm đã sử dụng.
        // - Không đổi điểm: cộng 10 điểm cho khách thành viên.
        // - Lưu hóa đơn, chi tiết, tồn kho và điểm trong transaction.
        // =====================================================

        [HttpPost("checkout")]
        [Authorize(Roles = "Admin,Cashier")]
        public async Task<IActionResult> Checkout(
            [FromBody] CheckoutRequestDto request)
        {
            // A. Kiểm tra dữ liệu gửi lên

            if (request == null ||
                request.Items == null ||
                request.Items.Count == 0)
            {
                return BadRequest(new
                {
                    message = "Giỏ hàng trống, không thể thanh toán."
                });
            }

            if (request.Items.Count > 200)
            {
                return BadRequest(new
                {
                    message =
                        "Mỗi hóa đơn không được vượt quá 200 dòng sản phẩm."
                });
            }

            if (request.Items.Any(item =>
                    item == null ||
                    item.ProductId <= 0 ||
                    item.Quantity <= 0 ||
                    item.Quantity > 10000))
            {
                return BadRequest(new
                {
                    message =
                        "Mã sản phẩm hoặc số lượng trong giỏ không hợp lệ."
                });
            }

            string? paymentMethod =
                NormalizePaymentMethod(request.PaymentMethod);

            if (paymentMethod == null)
            {
                return BadRequest(new
                {
                    message =
                        "Phương thức thanh toán không hợp lệ. " +
                        "Chọn Tiền mặt, Chuyển khoản hoặc Thẻ POS."
                });
            }

            if (paymentMethod != "Tiền mặt" &&
                !request.ExternalPaymentConfirmed)
            {
                return BadRequest(new
                {
                    message =
                        "Chưa xác nhận giao dịch chuyển khoản/Thẻ POS."
                });
            }

            // B. Lấy danh tính thu ngân từ JWT

            string? userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new
                {
                    message =
                        "Token không có thông tin nhân viên hợp lệ. " +
                        "Vui lòng đăng nhập lại."
                });
            }

            try
            {
                var cashier = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (cashier == null || !cashier.IsActive)
                {
                    return Unauthorized(new
                    {
                        message =
                            "Tài khoản không tồn tại hoặc đã bị khóa."
                    });
                }

                bool canCheckout =
                    string.Equals(
                        cashier.Role,
                        "Admin",
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    string.Equals(
                        cashier.Role,
                        "Cashier",
                        StringComparison.OrdinalIgnoreCase);

                if (!canCheckout)
                {
                    return Forbid();
                }

                // C. Gộp các dòng sản phẩm trùng nhau

                Dictionary<int, int> requestedQuantities;

                try
                {
                    var grouped = request.Items
                        .GroupBy(item => item.ProductId)
                        .ToDictionary(
                            group => group.Key,
                            group => group.Sum(
                                item => (long)item.Quantity));

                    if (grouped.Any(item =>
                            item.Value > int.MaxValue))
                    {
                        return BadRequest(new
                        {
                            message =
                                "Tổng số lượng sản phẩm vượt giới hạn."
                        });
                    }

                    requestedQuantities = grouped.ToDictionary(
                        item => item.Key,
                        item => (int)item.Value);
                }
                catch (OverflowException)
                {
                    return BadRequest(new
                    {
                        message =
                            "Tổng số lượng sản phẩm vượt giới hạn."
                    });
                }

                List<int> productIds =
                    requestedQuantities.Keys.ToList();

                // D. Bắt đầu transaction
                // Serializable giúp hạn chế xung đột tồn kho
                // khi nhiều quầy bán hàng cùng lúc.

                await using var transaction =
                    await _context.Database.BeginTransactionAsync(
                        IsolationLevel.Serializable);

                try
                {
                    // E. Lấy sản phẩm từ database

                    var products = await _context.Products
                        .Where(p => productIds.Contains(p.ProductId))
                        .ToDictionaryAsync(p => p.ProductId);

                    decimal subtotalAmount = 0m;

                    foreach (var requested in requestedQuantities)
                    {
                        if (!products.TryGetValue(
                                requested.Key,
                                out var product))
                        {
                            throw new CheckoutValidationException(
                                "Không tìm thấy sản phẩm có ID: " +
                                requested.Key + ".");
                        }

                        if (product.StockQuantity < requested.Value)
                        {
                            throw new CheckoutValidationException(
                                "Sản phẩm '" + product.ProductName +
                                "' không đủ tồn kho. Hiện còn " +
                                product.StockQuantity + ".");
                        }

                        if (product.Price <= 0m)
                        {
                            throw new CheckoutValidationException(
                                "Giá bán của sản phẩm '" +
                                product.ProductName +
                                "' không hợp lệ.");
                        }

                        subtotalAmount +=
                            product.Price * requested.Value;
                    }

                    // F. Tìm khách hàng theo số điện thoại

                    Customer? customer = null;

                    string customerPhone =
                        (request.CustomerPhone ?? string.Empty).Trim();

                    if (!string.IsNullOrWhiteSpace(customerPhone))
                    {
                        customer = await _context.Customers
                            .FirstOrDefaultAsync(
                                c => c.PhoneNumber == customerPhone);
                    }

                    // G. Tính điểm đổi và khoản giảm giá

                    decimal discountAmount = 0m;
                    int redeemedPoints = 0;

                    if (request.RedeemRewardPoints)
                    {
                        // Không cho đổi điểm nếu chưa tìm thấy khách.
                        if (customer == null)
                        {
                            throw new CheckoutValidationException(
                                "Vui lòng nhập đúng số điện thoại thành viên " +
                                "trước khi đổi điểm.");
                        }

                        // Không cho đổi nếu khách không có điểm.
                        if (customer.RewardPoints <= 0)
                        {
                            throw new CheckoutValidationException(
                                "Khách hàng hiện không có điểm thưởng để đổi.");
                        }

                        // Ví dụ:
                        // Tổng tiền 50.000đ, tỷ lệ 1.000đ/điểm
                        // => tối đa 50 điểm được dùng.
                        decimal pointsAllowedBySubtotalDecimal =
                            Math.Floor(
                                subtotalAmount / RewardPointValue);

                        int pointsAllowedBySubtotal =
                            pointsAllowedBySubtotalDecimal >= int.MaxValue
                                ? int.MaxValue
                                : (int)pointsAllowedBySubtotalDecimal;

                        // Số điểm thực tế dùng bằng số nhỏ hơn giữa:
                        // - Điểm khách đang có.
                        // - Điểm tối đa theo tổng tiền hàng.
                        redeemedPoints = Math.Min(
                            customer.RewardPoints,
                            pointsAllowedBySubtotal);

                        if (redeemedPoints <= 0)
                        {
                            throw new CheckoutValidationException(
                                "Tổng tiền hàng phải từ " +
                                RewardPointValue.ToString("N0") +
                                "đ trở lên mới có thể đổi điểm.");
                        }

                        discountAmount =
                            redeemedPoints * RewardPointValue;
                    }

                    decimal finalAmount =
                        subtotalAmount - discountAmount;

                    if (finalAmount < 0m)
                    {
                        throw new CheckoutValidationException(
                            "Tổng tiền thanh toán không hợp lệ.");
                    }

                    // H. Tạo hóa đơn
                    // Model Order và dbo.Orders phải có:
                    // SubtotalAmount, DiscountAmount,
                    // RewardPointsRedeemed.

                    var order = new Order
                    {
                        UserId = cashier.Id,

                        CustomerId = customer == null
                            ? (int?)null
                            : customer.CustomerId,

                        OrderDate = DateTime.Now,
                        PaymentMethod = paymentMethod,

                        SubtotalAmount = subtotalAmount,
                        DiscountAmount = discountAmount,
                        RewardPointsRedeemed = redeemedPoints,
                        TotalAmount = finalAmount
                    };

                    _context.Orders.Add(order);

                    // Lưu trước để lấy OrderId.
                    // Transaction vẫn chưa commit nên có thể rollback.
                    await _context.SaveChangesAsync();

                    // I. Tạo chi tiết hóa đơn và trừ kho

                    foreach (var requested in requestedQuantities)
                    {
                        var product = products[requested.Key];
                        int quantity = requested.Value;

                        // Kiểm tra lại trước khi trừ kho.
                        if (product.StockQuantity < quantity)
                        {
                            throw new CheckoutValidationException(
                                "Sản phẩm '" + product.ProductName +
                                "' không còn đủ tồn kho.");
                        }

                        product.StockQuantity -= quantity;

                        _context.OrderDetails.Add(new OrderDetail
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ProductId,
                            Quantity = quantity,
                            UnitPrice = product.Price
                        });
                    }

                    // J. Cập nhật điểm thưởng
                    //
                    // Có đổi điểm: trừ đúng số điểm đã đổi.
                    // Không đổi điểm: cộng 10 điểm theo quy tắc hiện tại.
                    //
                    // Tránh vừa đổi vừa cộng điểm trên cùng hóa đơn.

                    if (customer != null)
                    {
                        if (redeemedPoints > 0)
                        {
                            customer.RewardPoints -= redeemedPoints;
                        }
                        else
                        {
                            customer.RewardPoints +=
                                PointsEarnedPerOrder;
                        }
                    }

                    // Lưu chi tiết, tồn kho và điểm thưởng.
                    await _context.SaveChangesAsync();

                    // Chỉ commit sau khi mọi thao tác thành công.
                    await transaction.CommitAsync();

                    return Ok(new
                    {
                        message =
                            "Đã ghi nhận hóa đơn thanh toán thành công.",

                        orderId = order.OrderId,

                        subtotalAmount = order.SubtotalAmount,
                        discountAmount = order.DiscountAmount,
                        rewardPointsRedeemed =
                            order.RewardPointsRedeemed,

                        rewardPointsBalance = customer == null
                            ? (int?)null
                            : customer.RewardPoints,

                        totalAmount = order.TotalAmount,
                        paymentMethod = order.PaymentMethod,
                        cashierUsername = cashier.Username
                    });
                }
                catch (CheckoutValidationException ex)
                {
                    await TryRollbackAsync(transaction);

                    return BadRequest(new
                    {
                        message = ex.Message
                    });
                }
                catch (DbUpdateException ex)
                {
                    await TryRollbackAsync(transaction);

                    _logger.LogError(
                        ex,
                        "Lỗi lưu database khi checkout. UserId={UserId}. " +
                        "Inner error: {InnerError}",
                        userId,
                        ex.GetBaseException().Message);

                    return StatusCode(500, new
                    {
                        message = "Lỗi database khi thanh toán.",
                        detail = ex.GetBaseException().Message,
                        traceId = HttpContext.TraceIdentifier
                    });
                }
                catch (Exception ex)
                {
                    await TryRollbackAsync(transaction);

                    _logger.LogError(
                        ex,
                        "Lỗi checkout. UserId={UserId}. " +
                        "Inner error: {InnerError}",
                        userId,
                        ex.GetBaseException().Message);

                    return StatusCode(500, new
                    {
                        message = "Có lỗi hệ thống khi thanh toán.",
                        detail = ex.GetBaseException().Message,
                        traceId = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Không thể khởi tạo hoặc xử lý checkout. UserId={UserId}.",
                    userId);

                return StatusCode(500, new
                {
                    message =
                        "Không thể khởi tạo giao dịch thanh toán.",
                    detail = ex.GetBaseException().Message,
                    traceId = HttpContext.TraceIdentifier
                });
            }
        }

        // =====================================================
        // 4. ROLLBACK AN TOÀN
        // =====================================================

        private async Task TryRollbackAsync(
            IDbContextTransaction transaction)
        {
            try
            {
                await transaction.RollbackAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Không thể rollback transaction.");
            }
        }

        // =====================================================
        // 5. CHUẨN HÓA PHƯƠNG THỨC THANH TOÁN
        // =====================================================

        private static string? NormalizePaymentMethod(string? method)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                return "Tiền mặt";
            }

            string value = method.Trim();

            if (value.Equals(
                    "Tiền mặt",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Tiền mặt";
            }

            if (value.Equals(
                    "Chuyển khoản",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Chuyển khoản";
            }

            if (value.Equals(
                    "Thẻ POS",
                    StringComparison.OrdinalIgnoreCase)
                ||
                value.Equals(
                    "POS",
                    StringComparison.OrdinalIgnoreCase))
            {
                return "Thẻ POS";
            }

            return null;
        }

        // =====================================================
        // 6. LỖI NGHIỆP VỤ
        // =====================================================

        private sealed class CheckoutValidationException : Exception
        {
            public CheckoutValidationException(string message)
                : base(message)
            {
            }
        }
    }

    // =========================================================
    // DTO LỊCH SỬ GIAO DỊCH
    // =========================================================

    public class CustomerTransactionDto
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal SubtotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public int RewardPointsRedeemed { get; set; }

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

    // =========================================================
    // DTO YÊU CẦU THANH TOÁN
    // =========================================================

    public class CheckoutRequestDto
    {
        // Giữ để tương thích client cũ.
        // API lấy danh tính thu ngân thực tế từ JWT.
        public string CashierUsername { get; set; } = string.Empty;

        public string? CustomerPhone { get; set; }

        public string PaymentMethod { get; set; } = "Tiền mặt";

        public bool ExternalPaymentConfirmed { get; set; }

        // POS gửi true khi người dùng tích checkbox đổi điểm.
        public bool RedeemRewardPoints { get; set; }

        public List<CartItemDto> Items { get; set; } =
            new List<CartItemDto>();
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Chỉ giữ để tương thích client.
        // API không sử dụng giá này để tính tiền.
        public decimal UnitPrice { get; set; }
    }
}