## Hướng dẫn cài đặt và chạy dự án

### 1. Công nghệ sử dụng

- Backend: ASP.NET Core Web API trên .NET 8.
- Frontend: Windows Forms trên .NET Framework 4.7.2.
- Cơ sở dữ liệu: Microsoft SQL Server.
- Truy cập API: HTTP/HTTPS và dữ liệu JSON.
- Xác thực: JWT Bearer Token và phân quyền theo vai trò.

### 2. Yêu cầu môi trường

Cài đặt các công cụ cần thiết:

- Visual Studio có workload phát triển .NET Desktop và ASP.NET Core.
- SQL Server và công cụ quản lý cơ sở dữ liệu phù hợp.
- .NET 8 SDK và .NET Framework 4.7.2 Developer Pack.
- Git để quản lý mã nguồn.

### 3. Cấu hình cơ sở dữ liệu

1. Mở SQL Server và xác định instance đang sử dụng.
2. Kiểm tra chuỗi kết nối trong `MiniSupermarket.API/appsettings.json`.
3. Đảm bảo tên database trong chuỗi kết nối trùng với database của dự án.
4. Sao lưu database trước khi chạy migration hoặc thay đổi cấu trúc bảng.
5. Nếu dự án sử dụng EF Core Migrations, mở terminal tại thư mục `MiniSupermarket.API` và chạy:

```powershell
dotnet ef database update
```

Nếu máy chưa có công cụ EF Core CLI, cài phiên bản `dotnet-ef` tương thích với EF Core của dự án.

Lưu ý: Kiểm tra các script SQL bổ sung trước khi chạy để tránh thay đổi nhầm database.

### 4. Khởi chạy Backend API

1. Mở solution bằng Visual Studio.
2. Đặt `MiniSupermarket.API` làm Startup Project.
3. Kiểm tra cấu hình cổng trong `Properties/launchSettings.json`.
4. Chạy dự án bằng F5.
5. Mở Swagger nếu đã được bật trong môi trường phát triển để kiểm thử các endpoint.

Địa chỉ API phải trùng với địa chỉ được cấu hình trong `ApiClientService` của WinForms. Một cấu hình đã được sử dụng trong dự án là `https://localhost:7198`; hãy xác nhận cổng thực tế trên máy trước khi chạy.

### 5. Khởi chạy ứng dụng WinForms

1. Khôi phục các package NuGet cần thiết.
2. Đặt dự án WinForms làm Startup Project.
3. Kiểm tra `ApiClientService` đang trỏ tới địa chỉ API chính xác.
4. Chạy ứng dụng bằng F5.
5. Đăng nhập bằng tài khoản hợp lệ đã được tạo trong database.

Sau khi đăng nhập thành công, ứng dụng lưu thông tin phiên đăng nhập và token theo cơ chế `SessionManager` của dự án. Các request cần xác thực sử dụng Bearer Token.

### 6. Các API chính

| Chức năng | Phương thức | Endpoint |
|---|---|---|
| Lấy danh sách sản phẩm | GET | `/api/products` |
| Lấy danh sách khách hàng | GET | `/api/customers` |
| Tìm khách hàng | GET | `/api/customers/search?keyword=...` |
| Thêm khách hàng | POST | `/api/customers` |
| Cập nhật khách hàng | PUT | `/api/customers/{id}` |
| Xóa khách hàng | DELETE | `/api/customers/{id}` |
| Lấy danh sách hóa đơn | GET | `/api/orders` |
| Lịch sử mua hàng | GET | `/api/orders/customer/{customerId}` |
| Thanh toán POS | POST | `/api/orders/checkout` |
| Dashboard KPI | GET | `/api/reports/dashboard-summary` |
| Xuất danh mục sản phẩm | GET | `/api/reports/export/products.csv` |
| Xuất hóa đơn theo khoảng ngày | GET | `/api/reports/export/orders.csv?from=YYYY-MM-DD&to=YYYY-MM-DD` |

Các endpoint có thể yêu cầu JWT và vai trò phù hợp. Xem các controller trong source code để biết đầy đủ điều kiện request và cấu trúc response.

### 7. Kịch bản kiểm thử

- Đăng nhập bằng tài khoản hợp lệ và không hợp lệ.
- Kiểm tra menu hiển thị theo vai trò người dùng.
- Thêm, sửa, tìm kiếm sản phẩm và khách hàng.
- Kiểm tra cảnh báo sản phẩm có số lượng tồn kho không quá 10.
- Quét mã vạch, tăng/giảm số lượng, kiểm tra tổng tiền và tiền thừa.
- Thực hiện thanh toán thử, đối chiếu hóa đơn, chi tiết hóa đơn và tồn kho trong SQL Server.
- Kiểm tra đổi điểm thưởng với khách có điểm và khách không đủ điểm.
- Chọn khoảng thời gian báo cáo, đối chiếu biểu đồ doanh thu với dữ liệu hóa đơn.
- Xuất CSV danh mục sản phẩm và hóa đơn, kiểm tra nội dung tệp.
- Kiểm thử các trường hợp mất kết nối API hoặc gửi dữ liệu không hợp lệ.

Chỉ ghi kết quả Đạt sau khi đã thực hiện kiểm thử và lưu lại bằng chứng phù hợp.

### 8. Bảo mật và quản lý mã nguồn

- Không đưa mật khẩu database, JWT secret hoặc token thật vào README.
- Không commit dữ liệu nhạy cảm và các tệp đầu ra build.
- Kiểm tra `.gitignore` trước khi commit.
- Sử dụng các commit mô tả rõ nội dung thay đổi.
- Sao lưu database trước các thao tác migration hoặc điều chỉnh schema.

### 9. Giới hạn và hướng phát triển

Các tính năng cần thêm bảng dữ liệu, API hoặc tích hợp bên ngoài phải được ghi rõ là chưa triển khai. Không sử dụng dữ liệu minh họa như thể đó là số liệu vận hành thực tế.


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

### Đổi điểm thưởng tại quầy POS

- Khách hàng thành viên có thể sử dụng điểm thưởng để giảm trực tiếp số tiền cần thanh toán.
- Quy tắc hiện tại: 1 điểm thưởng tương ứng 1.000 VNĐ.
- Số điểm được đổi không vượt quá số điểm khách đang có và không làm số tiền giảm vượt quá tổng tiền hàng.
- Khi thanh toán thành công, hệ thống lưu tạm tính, khoản giảm giá, số điểm đã đổi và tổng tiền thanh toán vào hóa đơn.
- Điểm thưởng của khách hàng và tồn kho sản phẩm được cập nhật cùng giao dịch thanh toán trong backend.
- Nếu không sử dụng điểm, hệ thống áp dụng quy tắc cộng 10 điểm mỗi hóa đơn theo code hiện tại.
- WinForms gửi yêu cầu thanh toán tới `POST /api/orders/checkout`; API lấy giá bán từ database và kiểm tra lại điều kiện nghiệp vụ trước khi ghi dữ liệu.

Lưu ý: Quy tắc quy đổi điểm phải thống nhất giữa giao diện WinForms, backend API và tài liệu đồ án.


Họ tên sinh viên: Nguyen Quoc Bao


Mã sinh viên: 2124110087


Lớp học phần CCQ2411C