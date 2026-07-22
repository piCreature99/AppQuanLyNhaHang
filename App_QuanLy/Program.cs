using System;
using System.Windows.Forms;
using App_QuanLy.Views; // Khai báo phân vùng chứa các Form giao diện kính mờ

namespace App_QuanLy
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Cấu hình tối ưu hiển thị và font chữ cho Windows Forms hiện đại
            ApplicationConfiguration.Initialize();

            // Kích hoạt chạy màn hình Dashboard quản lý đầu tiên 
            //Application.Run(new FormQuanLyTaiKhoan());
            //Application.Run(new FormThucDon());
            //Application.Run(new FormKho());
            //Application.Run(new FormQuanLyHoaDon());
            //Application.Run(new FormDangNhap());
            //Application.Run(new FormQuenMatKhau());
            //Application.Run(new FormMain());
            Application.Run(new FormMaster());
        }
    }
}