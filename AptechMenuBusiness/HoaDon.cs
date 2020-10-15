using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AptechMenuBusiness
{
    class HoaDon
    {
        public string Mahd { get; set; }
        public string Tenhd { get; set; }
        public DateTime Ngaylap { get; set; }
        public List<SanPham> Danhsach { get; set; }
        public double TongTien()
        {
            return Danhsach.Select(sp => sp.ThanhTien()).Sum();
        }
        public void InDanhSachSanPham()
        {
            Danhsach.ForEach(sp =>
            {
                Console.WriteLine($"Tên sản phẩm: {sp.Tensp}");
                Console.WriteLine($"Số lượng: {sp.Soluong}");
                Console.WriteLine($"Đơn giá: {sp.Giasp}($)");
                Console.WriteLine($"Thành tiền: {sp.ThanhTien()}");
                Console.WriteLine("/##################################/");
            });
        }
    }
}
