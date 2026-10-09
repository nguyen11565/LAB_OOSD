using System;
using System.Collections.Generic;
using System.Data;
using QuanLyCongTyDuLich.Data;
namespace QuanLyCongTyDuLich.Services
{
    public sealed class FieldSpec
    {
        public string Name, Caption; public Type Kind; public bool Nullable;
        public FieldSpec(string name, string caption, Type kind, bool nullable = false)
        { Name = name; Caption = caption; Kind = kind; Nullable = nullable; }
    }
    public sealed class CatalogSpec
    {
        public string Title, TableName; public FieldSpec[] Fields;
        public CatalogSpec(string title, string table, params FieldSpec[] fields)
        { Title = title; TableName = table; Fields = fields; }
    }
    public sealed class CatalogService
    {
        private static FieldSpec S(string column, string caption) { return new FieldSpec(column, caption, typeof(string)); }
        private static FieldSpec I(string column, string caption, bool optional = false) { return new FieldSpec(column, caption, typeof(int), optional); }
        private static FieldSpec M(string column, string caption) { return new FieldSpec(column, caption, typeof(decimal)); }
        private static FieldSpec B(string column, string caption) { return new FieldSpec(column, caption, typeof(bool)); }
        public static readonly CatalogSpec[] Entities = {
            new CatalogSpec("Tour du lich", "TourDuLich", S("MaTour","Ma tour"), S("TenTour","Ten tour"), I("SoNgay","So ngay"), I("SoDem","So dem"), M("DonGia","Don gia VND")),
            new CatalogSpec("Diem tham quan", "DiemThamQuan", S("MaDiemThamQuan","Ma diem"), S("TenDiemThamQuan","Ten diem"), S("DiaDiem","Dia diem"), S("NoiDungYNghia","Noi dung va y nghia")),
            new CatalogSpec("Noi dung chan", "NoiDungChan", S("MaNoiDungChan","Ma dung chan"), S("TenNoiDungChan","Ten noi dung chan"), B("DoiPhuongTien","Doi phuong tien"), B("CoNoiAn","Co noi an"), B("CoKhachSan","Co khach san"), I("LoaiKhachSan","So sao (2-5)",true)),
            new CatalogSpec("Phuong tien", "PhuongTien", S("MaPhuongTien","Ma phuong tien"), S("TenPhuongTien","Ten phuong tien"), S("LoaiPhuongTien","Loai phuong tien")),
            new CatalogSpec("Diem ban ve", "DiemBanVe", S("MaDiemBan","Ma diem ban"), S("TenDiemBan","Ten diem ban"), S("DiaChi","Dia chi"), S("SoDienThoai","Dien thoai")),
            new CatalogSpec("Khach hang", "KhachHang", S("MaKhachHang","Ma khach hang"), S("HoTen","Ho ten"), S("SoDienThoai","Dien thoai"), S("DiaChi","Dia chi"), new FieldSpec("Email","Email",typeof(string),true)),
            new CatalogSpec("Nhan vien HD", "NhanVienHD", S("MaNhanVien","Ma nhan vien"), S("HoTen","Ho ten"), S("SoDienThoai","Dien thoai"), M("LuongCoBan","Luong co ban VND"))
        };
        private readonly CatalogRepository repo = new CatalogRepository();
        public DataTable List(CatalogSpec spec) { return repo.List(spec); }
        public void Save(CatalogSpec spec, Dictionary<string, object> values, bool update)
        {
            foreach (var field in spec.Fields)
            {
                object raw = values[field.Name];
                if (!field.Nullable && (raw == null || raw == DBNull.Value || (raw is string && string.IsNullOrWhiteSpace((string)raw))))
                    throw new ArgumentException("Nhap " + field.Caption);
            }
            if (spec.TableName == "TourDuLich")
            {
                // Existing bookings must retain their originally published duration.
                if (update)
                {
                    var old = repo.List(spec).Select("MaTour = '" + Convert.ToString(values["MaTour"]).Replace("'", "''") + "'");
                    if (old.Length == 1 && ((int)old[0]["SoNgay"] != (int)values["SoNgay"] || (int)old[0]["SoDem"] != (int)values["SoDem"]))
                    {
                        var used = Db.Scalar(@"SELECT (SELECT COUNT(*) FROM dbo.ChuyenKhachLe WHERE MaTour=@id)
 + (SELECT COUNT(*) FROM dbo.PhieuDangKyDoan WHERE MaTour=@id)",Db.P("@id",values["MaTour"]));
                        if ((int)used > 0) throw new ArgumentException("Tour da co lich chuyen/doan; khong duoc sua so ngay/dem.");
                    }
                }
                if ((int)values["SoNgay"] <= 0 || (int)values["SoDem"] < 0 || (int)values["SoDem"] > (int)values["SoNgay"] || (decimal)values["DonGia"] <= 0)
                    throw new ArgumentException("So ngay/dem/don gia khong hop le.");
            }
            if (spec.TableName == "NhanVienHD" && (decimal)values["LuongCoBan"] < 0)
                throw new ArgumentException("Luong co ban khong duoc am.");
            if (spec.TableName == "NoiDungChan")
            {
                bool hotel = (bool)values["CoKhachSan"];
                object star = values["LoaiKhachSan"];
                if ((hotel && (star == null || star == DBNull.Value || (int)star < 2 || (int)star > 5)) || (!hotel && star != null && star != DBNull.Value))
                    throw new ArgumentException("Co khach san: phai co 2-5 sao; khong co: de trong so sao.");
            }
            repo.Save(spec, values, update);
        }
        public void Delete(CatalogSpec spec, string key) { repo.Delete(spec, Db.Required(key, "Ma ban ghi")); }
    }
}
