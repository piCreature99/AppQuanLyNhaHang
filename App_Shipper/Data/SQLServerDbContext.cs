using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Windows.Forms; // Để hiển thị MessageBox báo lỗi trực quan

namespace App_Shipper.Data
{
    public static class SQLServerDbContext
    {
        // Biến static lưu chuỗi kết nối sau khi đọc thành công
        private static string _connectionString;

        // Khối khởi tạo tĩnh (Static Constructor) - Tự động chạy 1 lần duy nhất khi ứng dụng đụng vào lớp này
        static SQLServerDbContext()
        {
            try
            {
                // Cấu hình bộ đọc file appsettings.json từ thư mục thực thi của ứng dụng
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration config = builder.Build();

                // Đọc chuỗi kết nối từ nút ConnectionStrings -> DefaultConnection
                _connectionString = config.GetConnectionString("DefaultConnection");

                // Nếu đọc ra chuỗi rỗng hoặc null, kích hoạt lỗi ngay
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new Exception("Không tìm thấy chuỗi kết nối mang tên 'DefaultConnection' trong file appsettings.json!");
                }
            }
            catch (Exception ex)
            {
                // Hiển thị hộp thoại cảnh báo lỗi chi tiết giúp bạn debug nhanh hơn
                MessageBox.Show(
                    $"[LỖI KHỞI TẠO HỆ THỐNG]\n\n" +
                    $"Chi tiết lỗi: {ex.Message}\n" +
                    $"Ngoại lệ nội bộ: {ex.InnerException?.Message}\n\n" +
                    $"Vui lòng kiểm tra lại file appsettings.json đã được Copy vào thư mục bin (Output) chưa!",
                    "Lỗi Kết Nối Cấu Hình DB",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // Vẫn giữ lại lệnh throw để hệ thống ghi nhận vết lỗi
                throw;
            }
        }

        /// <summary>
        /// Hàm lấy chuỗi kết nối phục vụ cho các file khác (DatabaseHelper, DAL...)
        /// </summary>
        /// <returns>Chuỗi connection string dạng string</returns>
        public static string GetConnectionString()
        {
            return _connectionString;
        }
    }
}