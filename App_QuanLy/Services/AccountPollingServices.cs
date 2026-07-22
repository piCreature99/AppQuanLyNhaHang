using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using App_QuanLy.BLL;

namespace App_QuanLy.Services
{
    public class AccountPollingService
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;
        private CancellationTokenSource _cts;
        private bool _isPolling = false;

        // Định nghĩa một Event để Form đăng ký lắng nghe. 
        // Khi có dữ liệu mới, Event này sẽ tự động kích hoạt.
        public event Action<DataTable> OnShipperDataReceived;

        public AccountPollingService()
        {
            _taiKhoanBLL = new TaiKhoanBLL();
        }

        // Hàm kích hoạt luồng quét dữ liệu
        public void Start(int intervalInMilliseconds = 2000)
        {
            if (_isPolling) return; // Nếu đang chạy rồi thì không chạy thêm luồng nữa

            _isPolling = true;
            _cts = new CancellationTokenSource();

            // Đẩy toàn bộ vòng lặp vô hạn này xuống Background Thread (Luồng ngầm)
            Task.Run(async () =>
            {
                while (_isPolling && !_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        // 1. Gọi BLL lấy dữ liệu trạng thái Shipper mới nhất (Online, Hoạt động)
                        DataTable dtMoi = _taiKhoanBLL.LayDanhSachShipper("", ""); // Hoặc hàm chuyên dụng chỉ lấy cột cần thiết

                        // 2. Nếu có dữ liệu và có Form đang lắng nghe -> Phát sự kiện báo cho Form biết
                        if (dtMoi != null && OnShipperDataReceived != null)
                        {
                            OnShipperDataReceived.Invoke(dtMoi);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Polling Error]: {ex.Message}");
                    }

                    // Chờ 2 giây (bất đồng bộ, không gây nghẽn RAM)
                    await Task.Delay(intervalInMilliseconds, _cts.Token);
                }
            }, _cts.Token);
        }

        // Hàm dừng luồng quét khi đóng ứng dụng
        public void Stop()
        {
            _isPolling = false;
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }
    }
}
