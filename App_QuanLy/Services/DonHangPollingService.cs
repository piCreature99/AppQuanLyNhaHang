using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using App_QuanLy.BLL;

namespace App_QuanLy.Services
{
    public class DonHangPollingService
    {
        private readonly TaiKhoanBLL _taiKhoanBLL;
        private readonly DonHangBLL _donHangBLL;
        private CancellationTokenSource _cts;
        private bool _isPolling = false;

        // 🌟 Định nghĩa 2 Sự kiện tách biệt để Form lắng nghe
        public event Action<DataTable> OnShipperDataReceived;
        public event Action<Dictionary<string, string>> OnOrderStatusChanged; // Key: MaDonHang, Value: TrangThai
        public event Action<int> OnOrderCountChanged;

        public DonHangPollingService()
        {
            _taiKhoanBLL = new TaiKhoanBLL();
            _donHangBLL = new DonHangBLL();
        }

        public void Start(int intervalInMilliseconds = 2000)
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
                        // LUỒNG 1: QUÉT TRẠNG THÁI SHIPPER
                        DataTable dtShipper = _taiKhoanBLL.PollDanhSachShipperSS();
                        if (dtShipper != null && OnShipperDataReceived != null)
                        {
                            OnShipperDataReceived.Invoke(dtShipper);
                        }

                        // LUỒNG 2: QUÉT TRẠNG THÁI ĐƠN HÀNG (Chỉ lấy MaDonHang và TrangThaiDonHang hiện tại từ DB)
                        // Hàm này bạn viết trong DonHangBLL, trả về Dictionary<string, string> cho nhẹ
                        Dictionary<string, string> dictTrangThaiDonHang = _donHangBLL.PollTrangThaiDonHangMoiNhat();
                        if (dictTrangThaiDonHang != null && OnOrderStatusChanged != null)
                        {
                            OnOrderStatusChanged.Invoke(dictTrangThaiDonHang);
                        }

                        int soLuongHoaDonMoi = _donHangBLL.LayTongSoHoaDon();
                        if (soLuongHoaDonMoi >= 0 && OnOrderCountChanged != null)
                        {
                            OnOrderCountChanged.Invoke(soLuongHoaDonMoi);
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