using System;
using System.Windows.Forms;

namespace QuanLyHoSoBenhAn.Forms
{
    public sealed class FrmDanhMuc : BaseForm
    {
        private readonly ComboBox loai;
        private readonly TextBox ten, giaTriPhu, dienThoai, moTa;
        private readonly NumericUpDown maKhoa, donGia;
        public FrmDanhMuc() : base("Danh mục bác sĩ / khoa / thuốc / xét nghiệm / CSKH",155)
        {
            loai = Choices("Danh mục","Khoa","BacSi","Thuoc","LoaiXetNghiem","NhanVienCSKH");
            ten = TextField("Tên / Họ tên",180);
            giaTriPhu = TextField("Chuyên môn / Hàm lượng",170);
            dienThoai = TextField("Điện thoại / Đơn vị tính",170);
            maKhoa = Number("Mã khoa (cho bác sĩ)",0,130);
            donGia = Number("Đơn giá (xét nghiệm)",0,140);
            moTa = TextField("Mô tả",160);
            ActionButton("Xem danh mục",RefreshList);
            ActionButton("Thêm dữ liệu",() =>
            {
                var category=(string)loai.SelectedItem;
                if (String.IsNullOrWhiteSpace(ten.Text)) throw new InvalidOperationException("Tên không được trống.");
                if (category=="Khoa") Catalog.ThemKhoa(ten.Text.Trim(),moTa.Text.Trim());
                else if(category=="BacSi") Catalog.ThemBacSi((int)maKhoa.Value,ten.Text.Trim(),giaTriPhu.Text.Trim(),dienThoai.Text.Trim());
                else if(category=="Thuoc") Catalog.ThemThuoc(ten.Text.Trim(),giaTriPhu.Text.Trim(),dienThoai.Text.Trim(),moTa.Text.Trim());
                else if(category=="LoaiXetNghiem") Catalog.ThemLoaiXN(ten.Text.Trim(),donGia.Value,moTa.Text.Trim());
                else Catalog.ThemNhanVien(ten.Text.Trim(),dienThoai.Text.Trim());
                RefreshList();Success("Đã thêm vào danh mục " + category);
            });
            loai.SelectedIndexChanged += (s,e) => Run(RefreshList);
            Run(RefreshList);
        }
        private void RefreshList()
        {
            switch((string)loai.SelectedItem)
            {
                case "Khoa": ShowTable(Catalog.Khoa()); break;
                case "BacSi": ShowTable(Catalog.BacSi()); break;
                case "Thuoc": ShowTable(Catalog.Thuoc()); break;
                case "LoaiXetNghiem": ShowTable(Catalog.LoaiXN()); break;
                default: ShowTable(Catalog.NhanVien()); break;
            }
        }
    }
}
