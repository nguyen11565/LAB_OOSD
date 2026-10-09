using System;
using System.Data;
using QuanLyCongTyDuLich.Data;
namespace QuanLyCongTyDuLich.Services
{
    public sealed class SurveyService
    {
        public void Create(string survey, string customer, string trip, string group, int score, string message)
        {
            survey=Db.Required(survey,"Ma khao sat");customer=Db.Required(customer,"Khach hang");message=Db.Required(message,"Gop y");
            if(score<1||score>5)throw new ArgumentException("Diem danh gia tu 1-5.");
            if(string.IsNullOrWhiteSpace(trip)==string.IsNullOrWhiteSpace(group))throw new ArgumentException("Chon mot chuyen hoac mot doan.");
            object match = !string.IsNullOrWhiteSpace(trip)
                ? Db.Scalar(@"SELECT COUNT(*) FROM dbo.ChuyenKhachLe c JOIN dbo.VeKhachLe v ON c.MaChuyen=v.MaChuyen
 WHERE c.MaChuyen=@ref AND v.MaKhachHang=@kh AND v.TrangThai=N'DaThanhToan' AND c.TrangThai=N'HoanThanh'",Db.P("@ref",trip),Db.P("@kh",customer))
                : Db.Scalar(@"SELECT COUNT(*) FROM dbo.PhieuDangKyDoan
 WHERE MaPhieuDoan=@ref AND MaKhachHang=@kh AND TrangThai IN (N'DaHoanThanh',N'DaThanhToan')",Db.P("@ref",group),Db.P("@kh",customer));
            if((int)match==0)throw new ArgumentException("Khach chua tham gia hoac tour chua hoan thanh.");
            Db.Execute(@"INSERT dbo.PhieuKhaoSat(MaKhaoSat,MaKhachHang,MaChuyen,MaPhieuDoan,NgayKhaoSat,NoiDungGopY,DiemDanhGia)
 VALUES(@id,@kh,@trip,@grp,@date,@msg,@score)",Db.P("@id",survey),Db.P("@kh",customer),
            Db.P("@trip",string.IsNullOrWhiteSpace(trip)?(object)DBNull.Value:trip),Db.P("@grp",string.IsNullOrWhiteSpace(group)?(object)DBNull.Value:group),
            Db.P("@date",DateTime.Today),Db.P("@msg",message),Db.P("@score",score));
        }
    }
}
