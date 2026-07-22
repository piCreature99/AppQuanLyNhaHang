using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace App_Shipper.Helpers
{
    public class HelperFunctions
    {
        public Image LoadImage(string path)
        {
            if (!File.Exists(path)) return null;

            try
            {
                byte[] imageBytes = File.ReadAllBytes(path);
                MemoryStream ms = new MemoryStream(imageBytes);
                return Image.FromStream(ms);
            }
            catch
            {
                return null;
            }
        }

        public void DefaultImgInitializer()
        {
            string thuMucImagesShipper = Path.Combine(Application.StartupPath, "Images\\shipper");
            if (!Directory.Exists(thuMucImagesShipper))
            {
                Directory.CreateDirectory(thuMucImagesShipper);
            }
            string duongDanAnhShipper = Path.Combine(thuMucImagesShipper, "default.png");
            if (!File.Exists(duongDanAnhShipper))
            {
                TaoAnhChuMacDinh(duongDanAnhShipper);
            }

            string thuMucImagesNguoiDung = Path.Combine(Application.StartupPath, "Images\\client");
            if (!Directory.Exists(thuMucImagesNguoiDung))
            {
                Directory.CreateDirectory(thuMucImagesNguoiDung);
            }
            string duongDanAnhNguoiDung = Path.Combine(thuMucImagesNguoiDung, "default.png");
            if (!File.Exists(duongDanAnhNguoiDung))
            {
                TaoAnhChuMacDinh(duongDanAnhNguoiDung);
            }

            string thuMucImagesAdmin = Path.Combine(Application.StartupPath, "Images\\admin");
            if (!Directory.Exists(thuMucImagesAdmin))
            {
                Directory.CreateDirectory(thuMucImagesAdmin);
            }
            string duongDanAnhAdmin = Path.Combine(thuMucImagesAdmin, "default.png");
            if (!File.Exists(duongDanAnhAdmin))
            {
                TaoAnhChuMacDinh(duongDanAnhAdmin);
            }

            string thuMucImagesMonAn = Path.Combine(Application.StartupPath, "Images\\FoodItems");
            if (!Directory.Exists(thuMucImagesMonAn))
            {
                Directory.CreateDirectory(thuMucImagesMonAn);
            }
            string duongDanAnhMonAn = Path.Combine(thuMucImagesMonAn, "default.png");
            if (!File.Exists(duongDanAnhMonAn))
            {
                TaoAnhChuMacDinh(duongDanAnhMonAn);
            }
        }

        public string KhoiTaoFolderDichTheoVaiTro(string vaiTro)
        {
            return Path.Combine(Application.StartupPath, $"Images\\{vaiTro}");
        }

        private void TaoAnhChuMacDinh(string duongDanLuuFile)
        {
            // 1. Tạo khung ảnh kích thước 300x300
            using (Bitmap bmp = new Bitmap(300, 300))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Màu nền xám đậm
                    g.Clear(Color.FromArgb(64, 0, 0));

                    // 2. Cấu hình chữ muốn viết lên ảnh
                    string text = "NO IMAGE";
                    Font font = new Font("Arial", 16, FontStyle.Bold);
                    Brush brush = Brushes.White;

                    // 3. Căn chữ nằm chính giữa bức ảnh
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    // 4. Vẽ chữ lên bề mặt bức ảnh
                    // RectangleF(0, 0, 300, 300) nghĩa là căn chữ theo toàn bộ khung ảnh
                    g.DrawString(text, font, brush, new RectangleF(0, 0, 300, 300), sf);
                }

                // 5. Lưu lại thành file vật lý
                bmp.Save(duongDanLuuFile, ImageFormat.Png);
            }
        }
    }
}
