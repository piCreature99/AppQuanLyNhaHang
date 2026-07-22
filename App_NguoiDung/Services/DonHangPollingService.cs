using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using App_NguoiDung.BLL;

namespace App_NguoiDung.Services
{
    public class DonHangPollingService
    {
        private readonly DonHangBLL _donHangBLL;
        private CancellationTokenSource _cts;
        private bool _isPolling = false;

        // Định nghĩa 2 Sự kiện tách biệt để Form lắng nghe
        public event Action<DataTable> OnShipperDataReceived;
        public event Action<Dictionary<string, Tuple<string, string>>> OnOrderStatusChanged; // Key: MaDonHang, Value: TrangThai

        public DonHangPollingService()
        {
            _donHangBLL = new DonHangBLL();
        }

        public void Start(string maNguoiDung, int intervalInMilliseconds = 2000)
        {
            if (_isPolling) return;

            _isPolling = true;
            _cts = new CancellationTokenSource();

            Task.Run(async () =>
            {
                while (_isPolling && !_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        // LUỒNG 1: QUÉT TRẠNG THÁI ĐƠN HÀNG (Chỉ lấy MaDonHang và TrangThaiDonHang hiện tại từ DB)
                        
                        Dictionary<string, Tuple<string, string>> dictTrangThaiDonHang = _donHangBLL.PollTrangThaiDonHangMoiNhat(maNguoiDung);
                        if (dictTrangThaiDonHang != null && OnOrderStatusChanged != null)
                        {
                            OnOrderStatusChanged.Invoke(dictTrangThaiDonHang);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[Polling Error]: {ex.Message}");
                    }

                    try
                    {
                        await Task.Delay(intervalInMilliseconds, _cts.Token);
                    }
                    catch (TaskCanceledException)
                    {
                        break; // Luồng bị hủy kích hoạt khi gọi Stop()
                    }
                }
            }, _cts.Token);
        }

        public void Stop()
        {
            _isPolling = false;
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }
    }
}