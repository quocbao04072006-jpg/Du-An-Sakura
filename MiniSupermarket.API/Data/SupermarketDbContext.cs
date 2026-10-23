
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Data
{
    public class SupermarketDbContext : DbContext
    {
        public SupermarketDbContext(
            DbContextOptions<SupermarketDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Danh muc
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "My pham va cham soc da Nhat", Description = "Sua rua mat, kem chong nang, toner Nhat Ban" },
                new Category { CategoryId = 2, CategoryName = "Banh keo va thuc pham Nhat", Description = "Banh keo, mi an lien va thuc pham dong goi" },
                new Category { CategoryId = 3, CategoryName = "Thuc pham chuc nang Nhat", Description = "Vitamin, collagen va san pham bo sung" },
                new Category { CategoryId = 4, CategoryName = "Do gia dung Nhat Ban", Description = "Vat dung gia dinh va san pham tieu dung" }
            );

            // 2. San pham
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Barcode = "4901872444915", ProductName = "Sua rua mat Senka Perfect Whip 120g", Price = 105000m, StockQuantity = 100, CategoryId = 1 },
                new Product { ProductId = 2, Barcode = "4901872460793", ProductName = "Kem chong nang Anessa Perfect UV 60ml", Price = 515000m, StockQuantity = 50, CategoryId = 1 },
                new Product { ProductId = 3, Barcode = "4909978201234", ProductName = "Nuoc hoa hong Hada Labo", Price = 145000m, StockQuantity = 70, CategoryId = 1 },
                new Product { ProductId = 4, Barcode = "4901330578237", ProductName = "Banh que Pocky Matcha", Price = 38000m, StockQuantity = 200, CategoryId = 2 },
                new Product { ProductId = 5, Barcode = "4902105022987", ProductName = "Mi ly Nissin vi hai san", Price = 32000m, StockQuantity = 150, CategoryId = 2 },
                new Product { ProductId = 6, Barcode = "4901360353603", ProductName = "Banh gion vi rong bien", Price = 42000m, StockQuantity = 80, CategoryId = 2 },
                new Product { ProductId = 7, Barcode = "4511413404141", ProductName = "Vitamin C DHC 60 ngay", Price = 160000m, StockQuantity = 120, CategoryId = 3 },
                new Product { ProductId = 8, Barcode = "4511413404127", ProductName = "Vitamin B DHC", Price = 135000m, StockQuantity = 90, CategoryId = 3 },
                new Product { ProductId = 9, Barcode = "4903301123456", ProductName = "Nuoc rua chen gia dinh", Price = 65000m, StockQuantity = 45, CategoryId = 4 },
                new Product { ProductId = 10, Barcode = "4901609001234", ProductName = "Hop dung thuc pham Nhat", Price = 55000m, StockQuantity = 60, CategoryId = 4 }
            );

            // 3. Khach hang
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Nguyen Van An", PhoneNumber = "0901122334", Address = "TP Ho Chi Minh", RewardPoints = 150, MembershipRank = "Vang" },
                new Customer { CustomerId = 2, CustomerName = "Tran Thi Bich", PhoneNumber = "0918877665", Address = "Ben Tre", RewardPoints = 50, MembershipRank = "Bac" },
                new Customer { CustomerId = 3, CustomerName = "Le Van Cuong", PhoneNumber = "0983344556", Address = "Can Tho", RewardPoints = 10, MembershipRank = "Chuan" },
                new Customer { CustomerId = 4, CustomerName = "Pham Minh Duc", PhoneNumber = "0902233445", Address = "Binh Duong", RewardPoints = 80, MembershipRank = "Bac" },
                new Customer { CustomerId = 5, CustomerName = "Vo Ngoc Mai", PhoneNumber = "0935566778", Address = "Dong Nai", RewardPoints = 250, MembershipRank = "Vang" }
            );

            // 4. Tai khoan
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin01",
                    Password = "123456",
                    FullName = "Quan tri vien Sakura",
                    Role = "Admin",
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    Username = "cashier01",
                    Password = "123456",
                    FullName = "Thu ngan Sakura",
                    Role = "Cashier",
                    IsActive = true
                }
            );
        }
    }
}
