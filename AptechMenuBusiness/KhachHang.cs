using System;
using System.Collections.Generic;
using System.Text;

namespace AptechMenuBusiness
{
    class KhachHang
    {
        public string Makh { get; set; }
        public string Hoten { get; set; }
        public List<HoaDon> DanhSachHoaDon { get; set; }

        public void InDanhSachSanPham()
        {
            double tongtien = 0;
            DanhSachHoaDon.ForEach(hd =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Danh sách sản phẩm đã mua của mã hóa đơn {hd.Mahd} là:");
                Console.ResetColor();
                hd.Danhsach.ForEach(sp =>
                {
                    Console.WriteLine($"Tên sản phẩm: {sp.Tensp}");
                    Console.WriteLine($"Số lượng: {sp.Soluong}");
                    Console.WriteLine($"Đơn giá: {sp.Giasp}($)");
                    Console.WriteLine($"Thành tiền: {sp.ThanhTien()}($)");
                    Console.WriteLine("/##################################/");
                    tongtien += sp.ThanhTien();
                });
            });
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Số tiền quý khách cần thanh toán là:{tongtien}($)");
            Console.ResetColor();
        }
    }
}
