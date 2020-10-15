using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace AptechMenuBusiness
{
    class Program
    {
        static string mainMenu = "1. Nhập liệu" + 
                      "\n2. Thanh toán hóa đơn" +
                      "\n3. Liệt kê thông tin hóa đơn trong tháng" + // Câu 4 trong bài tập
                      "\n4. Tìm kiếm sản phẩm" +
                      "\nChọn lựa mục bạn cần thực hiện, hoặc nhấn nút bất kỳ ngoài danh sách để thoát chương trình!";
        static string menu1 = "1. Nhập sản phẩm vào kho" + // Câu 2 trong bài tập
                      "\n2. Nhập sản phẩm khách hàng đã mua" + // Câu 2 trong bài tập
                      "\nChọn lựa mục bạn cần thực hiện, hoặc nhấn nút bất kỳ để quay lại menu chính";
        static string menu2 = "1. Xác nhận thanh toán" +
                      "\n2. Hủy đơn" +
                      "\nChọn lựa mục bạn cần thực hiện, hoặc nhấn nút bất kỳ để quay lại menu chính";
        static string menu3 = "1. Sản phẩm có số tiền thanh toán lớn nhất của từng hóa đơn" + // Câu 5 trong bài tập
                      "\n2. Tìm thông tin tên sản phẩm theo từ khóa" + // Câu 6 trong bài tập
                      "\n3. Danh sách sản phẩm chưa được xuất kho" + // Câu 7 trong bài tập
                      "\nChọn lựa mục bạn cần thực hiện, hoặc nhấn nút bất kỳ để quay lại menu chính";
        static string menu4 = "1. Tổng tiền hóa đơn theo mã khách hàng" + // Câu 3 trong bài tập
                      "\n2. Chi tiết sản phẩm theo mã khách hàng" + // Câu 8 trong bài tập
                      "\nChọn lựa mục bạn cần thực hiện, hoặc nhấn nút bất kỳ để quay lại menu chính";

        static List<SanPham> Kho = new List<SanPham>();
        static List<KhachHang> ListKhachHang = new List<KhachHang>();
        static List<HoaDon> HoaDonDaThanhToan = new List<HoaDon>();
        static List<SanPham> SanPhamDaBan = new List<SanPham>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            title("Welcome and TIP:");
            Console.WriteLine("Chào mừng đến với chương trình quản lý hàng hóa");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Tip: chọn lựa các lựa chọn trên menu và nhấn Enter để thực hiện hoặc next");
            Console.ResetColor();
            Console.WriteLine("Nhấn phím Enter để bắt đầu chương trình");
            Console.ReadKey();

            // Working with main Menu
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = MainMenu();
            }
            Console.Clear();
            title("ĐÃ THOÁT CHƯƠNG TRÌNH");
        }
        private static bool MainMenu()
        {
            Console.Clear();
            title("DANH MỤC MENU:");
            Console.WriteLine(mainMenu);

            switch (Console.ReadLine())
            {
                case "1":
                    return DoOptionOne();
                case "2":
                    return DoOptionTwo();
                case "3":
                    return DoOptionThree();
                case "4":
                    return DoOptionFour();
                default:
                    return false;
            }
        }

        private static bool DoOptionOne()
        {
            Console.Clear();
            title("DANH MỤC MENU:");
            Console.WriteLine(menu1);
            switch (Console.ReadLine())
            {
                case "1":
                    // Nhập sản phẩm lưu kho
                    do
                    {
                        Console.Clear();
                        title($"Số lượng sản phẩm trong kho hiện tại là {Kho.Count}");
                        var sp = new SanPham();
                        string itemCode = $"sp{Kho.Count + 1}";
                        title($"Mã sản phẩm tự tạo là {itemCode}");
                        sp.Masp = itemCode;
                        Console.Write($"Nhập tên sản phẩm {itemCode}:");
                        sp.Tensp = Console.ReadLine();
                        Console.Write($"Nhập giá sản phẩm {itemCode} ($):");
                        sp.Giasp = Convert.ToDouble(Console.ReadLine());
                        Console.Write($"Nhập số lượng sản phẩm {itemCode}:");
                        sp.Soluong = Convert.ToInt32(Console.ReadLine());
                        Kho.Add(sp);
                        Console.Clear();
                        title($"Sản phẩm {itemCode} đã lưu kho, thực hiện:");
                        if (confirmation()) return true;
                    } while (true);
                case "2":
                    // Nhập sản phẩm cho từng khách hàng
                    Console.Clear();
                    if ((Kho != null) && (!Kho.Any()))
                    {
                        notify("Kho rỗng không thể thực hiện chức năng này");
                        return true;
                    }
                    Console.Write("Nhập mã khách hàng:");
                    string maKh = Console.ReadLine();
                    KhachHang guest = ListKhachHang.Where(kh => kh.Makh == maKh).FirstOrDefault();
                    KhachHang kh = new KhachHang()
                    {
                        DanhSachHoaDon = new List<HoaDon>()
                    };
                    if (guest != null)
                    {
                        kh = guest;
                    }
                    else
                    {
                        //Nhập dữ liệu khách hàng
                        title($"Khách hàng chưa tồn tại trên hệ thống, chương trình tự động tạo mã khách hàng là kh{ListKhachHang.Count + 1}");
                        kh.Makh = $"kh{ListKhachHang.Count + 1}";
                    }
                    Console.Write("Nhập tên khách hàng:");
                    kh.Hoten = Console.ReadLine();
                    //Nhập dữ liệu hóa đơn
                    var hd = new HoaDon()
                    {
                        Mahd = kh.DanhSachHoaDon != null ? $"hd{kh.DanhSachHoaDon.Count + 1}" : "hd1",
                        Tenhd = $"Hóa đơn mua hàng của khách hàng {kh.Hoten}",
                        Ngaylap = verifyDatetime("Ngày lập hóa đơn:"),
                        Danhsach = new List<SanPham>()
                    };
                    title($"Mã hóa đơn cho khách hàng {kh.Hoten} tự tạo là {hd.Mahd}");
                    title($"Tên hóa đơn: {hd.Tenhd}");
                    do
                    {
                        Console.Clear();
                        Console.Write("Nhập mã sản phẩm cần mua:");
                        string maSp = Console.ReadLine();
                        SanPham match = Kho.Where(sp => sp.Masp == maSp).FirstOrDefault();
                        if (match != null & match.Soluong > 0)
                        {
                            displayInStock(match);
                            int quantity = 0;
                            // Khởi tạo sản phẩm được mua
                            SanPham item = new SanPham()
                            {
                                Masp = match.Masp,
                                Tensp = match.Tensp,
                                Giasp = match.Giasp,
                                Soluong = 0
                            };
                            do
                            {
                                Console.Write("Nhập số lượng cần mua:");
                                if (Int32.TryParse(Console.ReadLine(), out quantity))
                                {
                                    if (quantity > match.Soluong)
                                    {
                                        notify("Số lượng vừa nhập lớn hơn số lượng sản phẩm đang có trong kho");
                                        continue;
                                    }
                                    // Trừ số lượng trong kho
                                    match.Soluong -= quantity;
                                    // Gán số lượng cho item
                                    item.Soluong = quantity;
                                    break;
                                }
                                else
                                {
                                    notify("Dữ liệu vừa nhập không phải dạng số nguyên");
                                }
                            } while (true);
                            //Lưu vào hóa đơn
                            if (hd.Danhsach != null & hd.Danhsach.Any(sp => sp.Masp == item.Masp))
                            {
                                // Kiểm tra nếu sản phẩm đã mua cùng tồn tại trong hóa đơn thì cộng dồn số lượng
                                SanPham items = hd.Danhsach.Where(sp => sp.Masp == match.Masp).FirstOrDefault();
                                items.Soluong += quantity;
                            }
                            else
                            {
                                // SP chưa xuất hiện trong hóa đơn thì thực hiện thêm mới
                                hd.Danhsach.Add(item);
                            }
                            Console.Clear();
                            title($"Đã lưu vào thông tin hóa đơn (Mã {hd.Mahd}) {hd.Danhsach.Count} sản phẩm");
                            Console.WriteLine("1. Nhập tiếp" +
                            "\n2. Nhấn nút bất kỳ để ghi nhận hóa đơn cho khách hàng");
                            if (Console.ReadLine() != "1")
                            {
                                break;
                            }
                        }
                        else
                        {
                            notify("Mã sản phẩm hết hàng hoặc không có trong kho");
                        }
                    } while (true);
                    //Ghi nhận hóa đơn cho khách hàng
                    kh.DanhSachHoaDon.Add(hd);
                    //Lưu khách hàng vào danh sách bán hàng
                    if (guest == null)
                    {
                        ListKhachHang.Add(kh);
                    }
                    title("Lưu hóa đơn thành công");
                    Console.ReadKey();
                    return true;
                default:
                    return true;
            }
        }

        private static bool DoOptionTwo()
        {
            string maKh = "";
            Console.Clear();
            title("DANH MỤC MENU:");
            Console.WriteLine(menu4);
            switch (Console.ReadLine())
            {
                case "1":
                    do
                    {
                        Console.Clear();
                        Console.Write("Nhập mã khách hàng:");
                        maKh = Console.ReadLine();
                        KhachHang match = ListKhachHang.Where(kh => kh.Makh == maKh).FirstOrDefault();
                        if (match != null)
                        {
                            if (match.DanhSachHoaDon.Count <= 0)
                            {
                                notify("Khách hàng chưa được nhập đơn hàng");
                                return true;
                            }
                            title($"Khách hàng đã mua {match.DanhSachHoaDon.Count} hóa đơn, chi tiết tổng tiền cụ thể như sau:");
                            match.DanhSachHoaDon.ForEach(hd =>
                            {
                                Console.WriteLine($"Tổng tiền mã hóa đơn {hd.Mahd} được lập vào ngày {hd.Ngaylap} là:");
                                title(hd.TongTien().ToString() + "($)");
                            });
                            Console.ReadLine();
                            return true;
                        }
                        else
                        {
                            notify("Mã khách hàng không tồn tại");
                        }
                    } while (true);
                case "2":
                    do
                    {
                        Console.Clear();
                        Console.Write("Nhập mã khách hàng:");
                        maKh = Console.ReadLine();
                        KhachHang guest = ListKhachHang.Where(kh => kh.Makh == maKh).FirstOrDefault();
                        if (guest != null)
                        {
                            if (guest.DanhSachHoaDon.Count <= 0)
                            {
                                notify("Khách hàng chưa được nhập đơn hàng");
                                return true;
                            }
                            guest.InDanhSachSanPham();
                            title("Nhấn Enter đế tiếp tục thực hiện");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine(menu2);
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    // Thể hiện thông báo thành công, lưu danh sách sản phẩm đã bán
                                    Console.Clear();
                                    title("Đã thanh toán đơn hàng thành công");
                                    guest.DanhSachHoaDon.ForEach(hd =>
                                    {
                                        hd.Danhsach.ForEach(sp =>
                                        {
                                            if (!SanPhamDaBan.Any(sold => sold.Masp == sp.Masp))
                                            {
                                                SanPhamDaBan.Add(sp);
                                            }

                                        });
                                        HoaDonDaThanhToan.Add(hd);
                                    });
                                    Console.ReadKey();
                                    return true;
                                case "2":
                                    // Delete hóa đơn
                                    guest.DanhSachHoaDon = new List<HoaDon>();
                                    title("Đã hủy đơn hàng");
                                    return true;
                                default:
                                    return true;
                            }
                        }
                        else
                        {
                            notify("Mã khách hàng không tồn tại");
                        }
                    } while (true);
                default:
                    return true;
            }
        }

        private static bool DoOptionThree()
        {
            int month = 0;
            int count = 0;
            do
            {
                Console.Clear();
                Console.Write("Nhập tháng cần truy xuất hóa đơn:");
                if (Int32.TryParse(Console.ReadLine(), out month))
                {
                    if (month < 1 || month > 12)
                    {
                        notify("Hãy nhập đúng số tháng trong năm");
                        continue;
                    }
                    Console.Clear();
                    title($"Thông tin các hóa đơn được bán trong tháng {month} là:");
                    HoaDonDaThanhToan.ForEach(hd =>
                    {
                        if ((hd.Ngaylap).Month == month)
                        {
                            hd.InDanhSachSanPham();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"Tổng tiền đã thanh toán là: {hd.TongTien()}");
                            Console.ResetColor();
                            Console.WriteLine("/##################################/");
                            count++;
                        }
                    });
                    Console.ReadKey();
                    if (count == 0)
                    {
                        Console.Clear();
                        notify($"Rất tiếc! Không có hóa đơn nào được xuất trong tháng {month}");
                    }
                    return true;
                }
                else
                {
                    notify("Dữ liệu vừa nhập không phải dạng số nguyên");
                }
            } while (true);
        }

        private static bool DoOptionFour()
        {
            Console.Clear();
            title("DANH MỤC MENU:");
            Console.WriteLine(menu3);
            switch (Console.ReadLine())
            {
                case "1":
                    // Tìm sản phẩm có giá tiền lớn nhất trong mỗi hóa đơn
                    Console.Clear();
                    if (HoaDonDaThanhToan.Count <= 0)
                    {
                        notify("Chưa có hóa đơn thanh toán. Không thực hiện được chức năng này");
                        return true;
                    }
                    HoaDonDaThanhToan.ForEach(hd =>
                    {
                        var item = hd.Danhsach.OrderByDescending(sp => sp.ThanhTien()).FirstOrDefault();
                        title($"Sản phẩm có giá thành tiền lớn nhất trong hóa đơn mã {hd.Mahd} là:");
                        Console.WriteLine(item);
                    });
                    Console.ReadKey();
                    return true;
                case "2":
                    // Tìm kiếm tên sản phẩm theo từ khóa
                    Console.Clear();
                    Console.WriteLine("Nhập từ khóa để tìm kiếm sản phẩm trong kho hàng theo tên:");
                    string keyword = Console.ReadLine();
                    var items = Kho.Where(sp => sp.Tensp.Contains(keyword)).ToList();
                    Console.Clear();
                    if (items.Count > 0)
                    {
                        title($"Danh sách sản phẩm có chứa từ khóa {keyword} là:");
                        foreach (var item in items)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else
                    {
                        notify($"Không có sản phẩm nào chứa từ khóa {keyword}");
                    }
                    Console.ReadKey();
                    return true;
                case "3":
                    // Tìm kiếm tên sản phẩm theo từ khóa
                    Console.Clear();
                    var result = SanPhamDaBan.Where(sp => Kho.All(item => item.Masp != sp.Masp)).ToList();
                    if (result.Count > 0)
                    {
                        title("Danh sách sản phẩm chưa được xuất kho:");
                        result.ForEach(sp =>
                        {
                            Console.WriteLine(sp);
                        });
                        Console.ReadKey();
                    }
                    else
                    {
                        notify("Tất cả các sản phẩm trong kho đều đã từng được bán");
                    }
                    return true;
                default:
                    return true;
            }
        }
        private static void displayInStock(SanPham sp)
        {
            Console.WriteLine("Thông tin sản phẩm hiện có trong kho:" +
                      $"\n1. Mã sản phẩm: {sp.Masp}" +
                      $"\n2. Tên sản phẩm: {sp.Tensp}" +
                      $"\n3. Giá sản phẩm: {sp.Giasp}($)" +
                      $"\n4. Số lượng trong kho: {sp.Soluong}");
        }

        private static bool confirmation()
        {
            Console.WriteLine("1. Nhập tiếp" +
                        "\n2. Nhấn nút bất kỳ để quay lại menu chính");
            if (Console.ReadLine() != "1")
            {
                return true;
            }
            return false;
        }

        private static void notify(String str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{str}, nhấn Enter để tiếp tục:");
            Console.ResetColor();
            Console.ReadKey();
        }

        private static DateTime verifyDatetime(string str)
        {
            DateTime userDateTime;
            do
            {
                Console.Write(str);
                if (DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", new CultureInfo("vi-VN"),
                       DateTimeStyles.None, out userDateTime))
                {
                    return userDateTime;
                }
                else
                {
                    notify("Nhập sai định dạng ngày tháng (dd-MM-yyyy)");
                }
            } while (true);
        }

        private static void title(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}
