using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniSupermarket.WinForms
{
    /// <summary>
    /// Lưu thông tin phiên đăng nhập dùng chung trong toàn bộ ứng dụng WinForms.
    /// </summary>
    public static class SessionManager
    {
        public static string JwtToken { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentFullName { get; set; } = string.Empty;
        public static string CurrentRole { get; set; } = string.Empty;

        /// <summary>
        /// Xóa toàn bộ thông tin đăng nhập và gỡ Bearer Token khỏi HttpClient dùng chung.
        /// </summary>
        public static void ClearSession()
        {
            JwtToken = string.Empty;
            CurrentUsername = string.Empty;
            CurrentFullName = string.Empty;
            CurrentRole = string.Empty;
            ApiClientService.SetAuthenticationToken();
        }
    }

    /// <summary>
    /// Dùng một HttpClient thống nhất để mọi Form tự động dùng chung BaseAddress và JWT.
    /// </summary>
    public static class ApiClientService
    {
        public static HttpClient Client { get; } = new HttpClient
        {
            // Phải khớp với HTTPS port trong MiniSupermarket.API/Properties/launchSettings.json.
            BaseAddress = new Uri("https://localhost:7198/api/"),
            Timeout = TimeSpan.FromSeconds(30)
        };

        public static string LastLoginError { get; private set; } = string.Empty;

        /// <summary>
        /// Đồng bộ JWT trong SessionManager vào HTTP Authorization header.
        /// Khi token rỗng, header được gỡ bỏ hoàn toàn.
        /// </summary>
        public static void SetAuthenticationToken()
        {
            if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                Client.DefaultRequestHeaders.Authorization = null;
            }
            else
            {
                Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken.Trim());
            }
        }

        /// <summary>
        /// Đăng nhập thông qua cùng HttpClient mà các màn hình nghiệp vụ sử dụng.
        /// Lấy token, username, họ tên, role rồi gắn Bearer Token cho các request tiếp theo.
        /// </summary>
        public static async Task<bool> LoginAsync(string username, string password)
        {
            LastLoginError = string.Empty;

            // Không để phiên cũ còn hiệu lực nếu lần đăng nhập mới thất bại.
            SessionManager.ClearSession();

            try
            {
                var loginObject = new
                {
                    Username = username,
                    Password = password
                };

                using (HttpResponseMessage response = await Client.PostAsJsonAsync("auth/login", loginObject))
                {
                    string jsonString = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        LastLoginError = TryReadMessage(jsonString);
                        if (string.IsNullOrWhiteSpace(LastLoginError))
                        {
                            LastLoginError = response.StatusCode == HttpStatusCode.Unauthorized
                                ? "Tên đăng nhập hoặc mật khẩu không chính xác!"
                                : "Đăng nhập thất bại. Mã phản hồi: " + (int)response.StatusCode;
                        }
                        return false;
                    }

                    using (JsonDocument document = JsonDocument.Parse(jsonString))
                    {
                        JsonElement root = document.RootElement;
                        SessionManager.JwtToken = ReadString(root, "token", "Token");
                        SessionManager.CurrentUsername = ReadString(root, "username", "Username");
                        SessionManager.CurrentFullName = ReadString(root, "fullName", "FullName");
                        SessionManager.CurrentRole = ReadString(root, "role", "Role");
                    }

                    if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
                    {
                        SessionManager.ClearSession();
                        LastLoginError = "Máy chủ đăng nhập thành công nhưng không trả về JWT Token hợp lệ.";
                        return false;
                    }

                    // Phòng trường hợp response không có username, giữ lại username đã nhập.
                    if (string.IsNullOrWhiteSpace(SessionManager.CurrentUsername))
                    {
                        SessionManager.CurrentUsername = username;
                    }

                    // DÒNG QUAN TRỌNG: tất cả Form gọi ApiClientService.Client sẽ gửi Bearer Token.
                    SetAuthenticationToken();
                    return true;
                }
            }
            catch
            {
                SessionManager.ClearSession();
                throw;
            }
        }

        /// <summary>
        /// Đọc giá trị JSON bất kể tên thuộc tính ở dạng camelCase hay PascalCase.
        /// </summary>
        private static string ReadString(JsonElement root, string camelName, string pascalName)
        {
            JsonElement value;
            if (root.TryGetProperty(camelName, out value) || root.TryGetProperty(pascalName, out value))
            {
                if (value.ValueKind == JsonValueKind.String)
                {
                    return value.GetString() ?? string.Empty;
                }
            }

            return string.Empty;
        }

        private static string TryReadMessage(string jsonString)
        {
            try
            {
                using (JsonDocument document = JsonDocument.Parse(jsonString))
                {
                    return ReadString(document.RootElement, "message", "Message");
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Ví dụ gọi API có bảo vệ JWT và nhận lỗi Unauthorized rõ ràng.
        /// </summary>
        public static async Task<string> GetDataWithTokenAsync(string endpoint)
        {
            SetAuthenticationToken();

            if (string.IsNullOrWhiteSpace(SessionManager.JwtToken))
            {
                throw new InvalidOperationException("Bạn chưa đăng nhập. Vui lòng đăng nhập trước khi gọi API.");
            }

            using (HttpResponseMessage response = await Client.GetAsync(endpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("JWT không hợp lệ hoặc phiên làm việc đã hết hạn. Vui lòng đăng nhập lại.");
                }

                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new UnauthorizedAccessException("Tài khoản không có quyền truy cập chức năng này.");
                }

                throw new HttpRequestException("Lỗi khi gọi API. Mã phản hồi: " + (int)response.StatusCode);
            }
        }
    }
}
