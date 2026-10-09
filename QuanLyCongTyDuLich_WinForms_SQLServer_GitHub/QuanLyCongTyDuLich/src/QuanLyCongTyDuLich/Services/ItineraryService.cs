using System;
using System.Data;
using QuanLyCongTyDuLich.Data;
namespace QuanLyCongTyDuLich.Services
{
    public sealed class ItineraryService
    {
        public DataTable Attractions() { return Db.Query(@"SELECT td.MaTour,t.TenTour,td.ThuTu,td.MaDiemThamQuan,d.TenDiemThamQuan
 FROM dbo.TourDiemThamQuan td JOIN dbo.TourDuLich t ON t.MaTour=td.MaTour JOIN dbo.DiemThamQuan d ON d.MaDiemThamQuan=td.MaDiemThamQuan ORDER BY td.MaTour,td.ThuTu"); }
        public DataTable Stops() { return Db.Query(@"SELECT ts.MaTour,t.TenTour,ts.ThuTu,ts.MaNoiDungChan,n.TenNoiDungChan,
 ts.MaPhuongTien,p.TenPhuongTien,n.DoiPhuongTien,n.CoNoiAn,n.CoKhachSan,n.LoaiKhachSan
 FROM dbo.TourNoiDungChan ts JOIN dbo.TourDuLich t ON t.MaTour=ts.MaTour
 JOIN dbo.NoiDungChan n ON n.MaNoiDungChan=ts.MaNoiDungChan
 JOIN dbo.PhuongTien p ON p.MaPhuongTien=ts.MaPhuongTien ORDER BY ts.MaTour,ts.ThuTu"); }
        public void AddAttraction(string tour,string point,int rank)
        {if(rank<1)throw new ArgumentException("Thu tu phai >= 1.");Db.Execute("INSERT dbo.TourDiemThamQuan VALUES(@t,@p,@o)",Db.P("@t",tour),Db.P("@p",point),Db.P("@o",rank));}
        public void RemoveAttraction(string tour,string point)
        {Db.Execute("DELETE FROM dbo.TourDiemThamQuan WHERE MaTour=@t AND MaDiemThamQuan=@p",Db.P("@t",tour),Db.P("@p",point));}
        public void AddStop(string tour,int rank,string stop,string vehicle)
        {if(rank<1)throw new ArgumentException("Thu tu phai >= 1.");Db.Execute("INSERT dbo.TourNoiDungChan VALUES(@t,@o,@s,@v)",Db.P("@t",tour),Db.P("@o",rank),Db.P("@s",stop),Db.P("@v",vehicle));}
        public void RemoveStop(string tour,int rank)
        {Db.Execute("DELETE FROM dbo.TourNoiDungChan WHERE MaTour=@t AND ThuTu=@o",Db.P("@t",tour),Db.P("@o",rank));}
    }
}
