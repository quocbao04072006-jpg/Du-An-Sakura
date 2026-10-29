using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        public int? CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer? Customer { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        // Thành tiền thực thu sau khi giảm giá.
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        // Tổng tiền hàng trước giảm giá.
        [Column(TypeName = "decimal(18,2)")]
        public decimal SubtotalAmount { get; set; }

        // Số tiền giảm từ việc đổi điểm thưởng.
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; }

        // Số điểm thưởng đã sử dụng.
        public int RewardPointsRedeemed { get; set; }

        public string PaymentMethod { get; set; } = "Tiền mặt";

        public ICollection<OrderDetail> OrderDetails { get; set; }
            = new List<OrderDetail>();
    }
}