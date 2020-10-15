using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AptechMenuBusiness
{
    class SanPham
    {
        public string Masp { get; set; }
        public string Tensp { get; set; }
        public double Giasp { get; set; }
        public int Soluong { get; set; }
        public double ThanhTien()
        {
            return Giasp * Soluong;
        }
        public override string ToString()
        {
            return $"Mã sản phẩm: {Masp}" +
                   $"\nTên sản phẩm: {Tensp}" +
                   $"\nGiá sản phẩm: {Giasp}" +
                   $"\nSố lượng: {Soluong}" +
                   $"\nThành tiền: {this.ThanhTien()}" +
                   "/##################################/";
        }
    }
}
