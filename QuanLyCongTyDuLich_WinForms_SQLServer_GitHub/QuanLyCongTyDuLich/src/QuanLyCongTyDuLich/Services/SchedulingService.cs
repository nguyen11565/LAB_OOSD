using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongTyDuLich.Data;
namespace QuanLyCongTyDuLich.Services
{
    public sealed class SchedulingService
    {
        public void Assign(string employee, string trip, string group, decimal fee)
        {
            employee=Db.Required(employee,"Nhan vien");
            if ((string.IsNullOrWhiteSpace(trip) && string.IsNullOrWhiteSpace(group)) ||
                (!string.IsNullOrWhiteSpace(trip) && !string.IsNullOrWhiteSpace(group)))
                throw new ArgumentException("Chi chon 1 trong 2: chuyen khach le hoac doan.");
            if (fee <= 0) throw new ArgumentException("Tien cong phai > 0.");
            using (var con=Db.Open()) using (var tx=con.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    // Serialise assignments for each employee to block concurrent overlapping schedules.
                    using (var cmd=Db.Command(con,tx,"SELECT MaNhanVien FROM dbo.NhanVienHD WITH(UPDLOCK,HOLDLOCK) WHERE MaNhanVien=@nv",Db.P("@nv",employee)))
                        if(cmd.ExecuteScalar()==null)throw new ArgumentException("Khong ton tai nhan vien.");
                    DateTime start,end;
                    if(!string.IsNullOrWhiteSpace(trip))
                    {
                        using(var cmd=Db.Command(con,tx,"SELECT NgayDi,NgayVe FROM dbo.ChuyenKhachLe WHERE MaChuyen=@id AND TrangThai=N'DangMo'",Db.P("@id",trip)))
                        using(var rd=cmd.ExecuteReader())
                        {if(!rd.Read())throw new ArgumentException("Chuyen khong ton tai hoac da hoan thanh.");start=rd.GetDateTime(0);end=rd.GetDateTime(1);}
                        using(var cmd=Db.Command(con,tx,"SELECT COUNT(*) FROM dbo.PhanCongHuongDan WHERE MaChuyen=@id",Db.P("@id",trip)))
                            if((int)cmd.ExecuteScalar()!=0)throw new ArgumentException("Moi chuyen chi duoc mot HDV.");
                    }
                    else
                    {
                        using(var cmd=Db.Command(con,tx,@"SELECT d.NgayDiYeuCau,DATEADD(day,t.SoNgay-1,d.NgayDiYeuCau)
 FROM dbo.PhieuDangKyDoan d JOIN dbo.TourDuLich t ON t.MaTour=d.MaTour WHERE d.MaPhieuDoan=@id AND d.TrangThai=N'DaDatCoc'",Db.P("@id",group)))
                        using(var rd=cmd.ExecuteReader())
                        {if(!rd.Read())throw new ArgumentException("Doan khong ton tai hoac da ket thuc/huy.");start=rd.GetDateTime(0);end=rd.GetDateTime(1);}
                    }
                    using(var cmd=Db.Command(con,tx,@"SELECT COUNT(*) FROM dbo.PhanCongHuongDan p
 LEFT JOIN dbo.ChuyenKhachLe c ON c.MaChuyen=p.MaChuyen
 LEFT JOIN dbo.PhieuDangKyDoan d ON d.MaPhieuDoan=p.MaPhieuDoan
 LEFT JOIN dbo.TourDuLich t ON t.MaTour=d.MaTour
 WHERE p.MaNhanVien=@nv AND COALESCE(c.NgayDi,d.NgayDiYeuCau)<=@end
 AND COALESCE(c.NgayVe,DATEADD(day,t.SoNgay-1,d.NgayDiYeuCau))>=@start",Db.P("@nv",employee),Db.P("@start",start),Db.P("@end",end)))
                        if((int)cmd.ExecuteScalar()!=0)throw new ArgumentException("Nhan vien bi trung lich voi chuyen/doan khac.");
                    using(var cmd=Db.Command(con,tx,"INSERT dbo.PhanCongHuongDan VALUES(@id,@nv,@trip,@grp,@fee)",
                        Db.P("@id",Guid.NewGuid().ToString("N")),Db.P("@nv",employee),Db.P("@trip",string.IsNullOrWhiteSpace(trip)?(object)DBNull.Value:trip.Trim()),
                        Db.P("@grp",string.IsNullOrWhiteSpace(group)?(object)DBNull.Value:group.Trim()),Db.P("@fee",fee)))cmd.ExecuteNonQuery();
                    tx.Commit();
                }catch{tx.Rollback();throw;}
            }
        }
        public DataTable List()
        { return Db.Query(@"SELECT p.MaPhanCong,n.HoTen,p.MaChuyen,p.MaPhieuDoan,p.TienCong FROM dbo.PhanCongHuongDan p
 JOIN dbo.NhanVienHD n ON n.MaNhanVien=p.MaNhanVien ORDER BY n.HoTen,p.MaPhanCong"); }
    }
}
