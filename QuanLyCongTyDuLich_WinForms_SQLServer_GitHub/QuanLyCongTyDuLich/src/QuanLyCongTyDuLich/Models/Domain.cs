using System;
using System.Collections.Generic;
namespace QuanLyCongTyDuLich.Models
{
    public sealed class BaoHiemNguoi { public string HoTen; public string GiayTo; public DateTime NgaySinh; }
    public sealed class DoanRequest
    {
        public string MaPhieuDoan, MaTour, MaKhachHang, TenCoQuan, DiaChiCoQuan, DienThoaiCoQuan, NguoiDaiDien, DiemDon;
        public int SoNguoiDi; public DateTime NgayDi; public decimal TienCoc; public bool CoBaoHiem;
        public List<BaoHiemNguoi> DanhSach = new List<BaoHiemNguoi>();
    }
    public sealed class LeRequest
    {
        public string MaVe, MaChuyen, MaKhachHang, MaDiemBan, DiemDon;
        public int SoNguoi;
    }
}
