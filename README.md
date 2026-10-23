Họ tên sinh viên: Nguyen Quoc Bao


Mã sinh viên: 2124110087


Lớp học phần CCQ2411C


## MiniMart UI refresh (2026-10)

### Chức năng được bổ sung
- Sidebar thống nhất với Dashboard, POS, Danh mục, Sản phẩm, Kho & tồn kho, Khách hàng, Báo cáo, Quản trị tài khoản và Cài đặt hệ thống.
- Màn hình Kho & tồn kho đọc dữ liệu thật từ `GET /api/products`, hỗ trợ tìm kiếm, lọc hàng sắp hết/hết hàng và điều chỉnh tồn kho có kiểm tra không âm qua `POST /api/products/{id}/stock-adjustment`.
- Dashboard hiển thị doanh thu hôm nay, tổng hóa đơn, sản phẩm sắp hết và tổng khách hàng.
- Báo cáo ngày bổ sung doanh thu theo giờ, doanh thu theo ngành hàng, top 10 sản phẩm, doanh thu tháng và giá trị hóa đơn trung bình.
- Màn hình Khách hàng có KPI tổng khách, thành viên mới trong tháng và tổng điểm tích lũy.
- Cài đặt hệ thống cho phép xem phiên đăng nhập và kiểm tra API; không lưu các cấu hình cửa hàng giả khi database chưa có bảng cấu hình.

### Cập nhật cơ sở dữ liệu
Model khách hàng có thêm `CreatedAt` để thống kê thành viên mới. Sau khi sao lưu database, chạy từ thư mục `MiniSupermarket.API`:

```powershell
dotnet ef database update
```

Nếu lệnh EF chưa được cài, cài `dotnet-ef` phiên bản tương thích EF Core 8 và kiểm tra `appsettings.json` trỏ đúng SQL Server. Các khách hàng đã có trước migration sẽ có `CreatedAt = NULL` vì database không có ngày tạo lịch sử; KPI thành viên mới chỉ đếm chính xác các hồ sơ được tạo từ sau khi cập nhật. API cần chạy ở `https://localhost:7198` hoặc cập nhật `ApiClientService` trong WinForms để trùng với cổng thực tế.

### Giới hạn dữ liệu được giữ minh bạch
Schema hiện có chưa lưu giá vốn, đơn vị tính, nhà cung cấp, hạn sử dụng, vị trí kệ, ảnh sản phẩm hay bảng voucher. Vì vậy UI không hiển thị số liệu giả cho các trường này. Các tính năng nhập hàng theo nhà cung cấp, đổi voucher, lợi nhuận và báo cáo giá vốn cần bổ sung model/API/database riêng trước khi bật.
