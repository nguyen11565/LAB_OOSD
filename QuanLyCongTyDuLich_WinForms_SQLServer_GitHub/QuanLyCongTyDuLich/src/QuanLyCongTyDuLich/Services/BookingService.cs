using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using QuanLyCongTyDuLich.Data;
using QuanLyCongTyDuLich.Models;
namespace QuanLyCongTyDuLich.Services
{
    public sealed class BookingService
    {
        public void CreateTrip(string ma, string tour, DateTime depart, int capacity)
        {
            ma = Db.Required(ma, "Ma chuyen"); tour = Db.Required(tour, "Ma tour");
            if (capacity < 1) throw new ArgumentException("Suc chua phai tu 1 tro len.");
            var tourInfo = Db.Query("SELECT SoNgay,DonGia FROM dbo.TourDuLich WHERE MaTour=@id",Db.P("@id",tour));
            if (tourInfo.Rows.Count == 0) throw new ArgumentException("Ma tour khong ton tai.");
            int days = Convert.ToInt32(tourInfo.Rows[0]["SoNgay"]);
            decimal price = Convert.ToDecimal(tourInfo.Rows[0]["DonGia"]);
            DateTime end = depart.Date.AddDays(days - 1);
            if (depart.Date < DateTime.Today) throw new ArgumentException("Chuyen moi phai khoi hanh tu hom nay tro di.");
            Db.Execute(@"INSERT dbo.ChuyenKhachLe(MaChuyen,MaTour,DonGiaApDung,NgayDi,NgayVe,SucChua,SoLuongHienTai,TrangThai)
 VALUES(@id,@tour,@price,@di,@ve,@cap,0,N'DangMo')",Db.P("@id",ma),Db.P("@tour",tour),Db.P("@price",price),Db.P("@di",depart.Date),Db.P("@ve",end),Db.P("@cap",capacity));
        }
        public decimal SellTicket(LeRequest r)
        {
            r.MaVe = Db.Required(r.MaVe,"Ma ve"); r.MaChuyen = Db.Required(r.MaChuyen,"Ma chuyen");
            r.MaKhachHang = Db.Required(r.MaKhachHang,"Khach hang"); r.MaDiemBan = Db.Required(r.MaDiemBan,"Diem ban"); r.DiemDon = Db.Required(r.DiemDon,"Diem don");
            if (r.SoNguoi < 1 || r.SoNguoi > 11) throw new ArgumentException("Khach le: 1-11 nguoi (12 nguoi la doan).");
            using (var con = Db.Open())
            using (var tx = con.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    decimal price; DateTime start; string status;
                    using (var cmd = Db.Command(con,tx,@"SELECT c.DonGiaApDung,c.NgayDi,c.TrangThai
FROM dbo.ChuyenKhachLe c WITH(UPDLOCK,HOLDLOCK)
WHERE c.MaChuyen=@chuyen",Db.P("@chuyen",r.MaChuyen)))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) throw new ArgumentException("Chuyen khong ton tai.");
                        price = reader.GetDecimal(0); start = reader.GetDateTime(1); status = reader.GetString(2);
                    }
                    if (status != "DangMo" || start.Date < DateTime.Today) throw new ArgumentException("Chuyen khong con nhan khach.");
                    using (var update = Db.Command(con,tx,@"UPDATE dbo.ChuyenKhachLe SET SoLuongHienTai=SoLuongHienTai+@n
 WHERE MaChuyen=@chuyen AND TrangThai=N'DangMo' AND SoLuongHienTai+@n<=SucChua",Db.P("@n",r.SoNguoi),Db.P("@chuyen",r.MaChuyen)))
                        if (update.ExecuteNonQuery() != 1) throw new ArgumentException("Khong du cho trong tren chuyen.");
                    decimal total = checked(price * r.SoNguoi);
                    using (var cmd = Db.Command(con,tx,@"INSERT dbo.VeKhachLe(MaVe,MaChuyen,MaKhachHang,MaDiemBan,NgayDangKy,DiemDon,SoNguoi,TongTienThanhToan,TrangThai)
 VALUES(@id,@chuyen,@kh,@diemban,@ngay,@diemdon,@n,@tien,N'DaThanhToan')",
                        Db.P("@id",r.MaVe),Db.P("@chuyen",r.MaChuyen),Db.P("@kh",r.MaKhachHang),Db.P("@diemban",r.MaDiemBan),
                        Db.P("@ngay",DateTime.Today),Db.P("@diemdon",r.DiemDon),Db.P("@n",r.SoNguoi),Db.P("@tien",total))) cmd.ExecuteNonQuery();
                    tx.Commit(); return total;
                }
                catch { tx.Rollback(); throw; }
            }
        }
        public void CancelTicket(string id)
        {
            id = Db.Required(id,"Ma ve");
            using (var con = Db.Open())
            using (var tx = con.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    string trip; string state; int people; DateTime depart;
                    using (var cmd=Db.Command(con,tx,@"SELECT v.MaChuyen,v.SoNguoi,v.TrangThai,c.NgayDi
 FROM dbo.VeKhachLe v WITH(UPDLOCK,HOLDLOCK) JOIN dbo.ChuyenKhachLe c ON c.MaChuyen=v.MaChuyen WHERE v.MaVe=@id",Db.P("@id",id)))
                    using (var rd=cmd.ExecuteReader())
                    { if(!rd.Read()) throw new ArgumentException("Ma ve khong ton tai."); trip=rd.GetString(0);people=rd.GetInt32(1);state=rd.GetString(2);depart=rd.GetDateTime(3); }
                    if (state!="DaThanhToan") throw new ArgumentException("Ve da huy.");
                    if (depart.Date < DateTime.Today) throw new ArgumentException("Chuyen da khoi hanh, khong the huy ve.");
                    using (var cmd=Db.Command(con,tx,"UPDATE dbo.VeKhachLe SET TrangThai=N'DaHuy' WHERE MaVe=@id",Db.P("@id",id))) cmd.ExecuteNonQuery();
                    using (var cmd=Db.Command(con,tx,"UPDATE dbo.ChuyenKhachLe SET SoLuongHienTai=SoLuongHienTai-@n WHERE MaChuyen=@chuyen",Db.P("@n",people),Db.P("@chuyen",trip))) cmd.ExecuteNonQuery();
                    tx.Commit();
                } catch { tx.Rollback(); throw; }
            }
        }
        public void CompleteTrip(string id)
        {
            var changed=Db.Execute(@"UPDATE dbo.ChuyenKhachLe SET TrangThai=N'HoanThanh'
 WHERE MaChuyen=@id AND TrangThai=N'DangMo' AND NgayVe<=@today
 AND EXISTS(SELECT 1 FROM dbo.PhanCongHuongDan WHERE MaChuyen=@id)",Db.P("@id",Db.Required(id,"Ma chuyen")),Db.P("@today",DateTime.Today));
            if (changed!=1) throw new ArgumentException("Chuyen chua ket thuc, chua duoc phan cong HDV hoac da hoan thanh.");
        }
        public decimal CreateGroup(DoanRequest r)
        {
            r.MaPhieuDoan=Db.Required(r.MaPhieuDoan,"Ma phieu");r.MaTour=Db.Required(r.MaTour,"Ma tour");r.MaKhachHang=Db.Required(r.MaKhachHang,"Ma khach hang");
            r.TenCoQuan=Db.Required(r.TenCoQuan,"Ten co quan");r.DiaChiCoQuan=Db.Required(r.DiaChiCoQuan,"Dia chi");
            r.DienThoaiCoQuan=Db.Required(r.DienThoaiCoQuan,"Dien thoai");r.NguoiDaiDien=Db.Required(r.NguoiDaiDien,"Nguoi dai dien");r.DiemDon=Db.Required(r.DiemDon,"Diem don");
            if(r.SoNguoiDi<12)throw new ArgumentException("Doan phai co it nhat 12 nguoi.");
            if(r.NgayDi.Date<DateTime.Today)throw new ArgumentException("Ngay di doan phai tu hom nay tro di.");
            if(r.CoBaoHiem && (r.DanhSach==null || r.DanhSach.Count!=r.SoNguoiDi))throw new ArgumentException("Doan co bao hiem: nhap du danh sach tung nguoi di.");
            if(!r.CoBaoHiem && r.DanhSach != null && r.DanhSach.Count>0)throw new ArgumentException("Khong mua bao hiem: de trong danh sach bao hiem.");
            using(var con=Db.Open()) using(var tx=con.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    decimal price;
                    using(var cmd=Db.Command(con,tx,"SELECT DonGia FROM dbo.TourDuLich WHERE MaTour=@id",Db.P("@id",r.MaTour)))
                    {var p=cmd.ExecuteScalar();if(p==null)throw new ArgumentException("Tour khong ton tai.");price=(decimal)p;}
                    decimal total=checked(price*r.SoNguoiDi);
                    if(r.TienCoc<=0 || r.TienCoc>=total)throw new ArgumentException("Tien coc phai > 0 va nho hon tong tien (con lai thanh toan sau tour).");
                    using(var cmd=Db.Command(con,tx,@"INSERT dbo.PhieuDangKyDoan(MaPhieuDoan,MaTour,MaKhachHang,DonGiaApDung,TenCoQuan,DiaChiCoQuan,DienThoaiCoQuan,NguoiDaiDien,SoNguoiDi,NgayDiYeuCau,DiemDon,TienCoc,CoBaoHiem,TrangThai)
 VALUES(@id,@tour,@kh,@price,@ten,@diachi,@sdt,@dd,@n,@ngay,@don,@coc,@bh,N'DaDatCoc')",
                        Db.P("@id",r.MaPhieuDoan),Db.P("@tour",r.MaTour),Db.P("@kh",r.MaKhachHang),Db.P("@price",price),Db.P("@ten",r.TenCoQuan),Db.P("@diachi",r.DiaChiCoQuan),Db.P("@sdt",r.DienThoaiCoQuan),Db.P("@dd",r.NguoiDaiDien),Db.P("@n",r.SoNguoiDi),Db.P("@ngay",r.NgayDi.Date),Db.P("@don",r.DiemDon),Db.P("@coc",r.TienCoc),Db.P("@bh",r.CoBaoHiem)))cmd.ExecuteNonQuery();
                    if(r.CoBaoHiem)
                    foreach(var person in r.DanhSach)
                    {
                        if(person.NgaySinh.Date>DateTime.Today)throw new ArgumentException("Ngay sinh bao hiem khong hop le.");
                        using(var cmd=Db.Command(con,tx,@"INSERT dbo.DanhSachBaoHiem(MaSoBaoHiem,MaPhieuDoan,HoTen,CMNDPassport,NgaySinh) VALUES(@id,@phieu,@ten,@giay,@ns)",
                            Db.P("@id",Guid.NewGuid().ToString("N")),Db.P("@phieu",r.MaPhieuDoan),Db.P("@ten",Db.Required(person.HoTen,"Ho ten BH")),Db.P("@giay",Db.Required(person.GiayTo,"Giay to BH")),Db.P("@ns",person.NgaySinh.Date)))cmd.ExecuteNonQuery();
                    }
                    tx.Commit();return total;
                }catch{tx.Rollback();throw;}
            }
        }
        public void CancelGroup(string id)
        {
            id=Db.Required(id,"Ma phieu");
            using(var con=Db.Open())using(var tx=con.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    using(var cmd=Db.Command(con,tx,"UPDATE dbo.PhieuDangKyDoan SET TrangThai=N'DaHuy' WHERE MaPhieuDoan=@id AND TrangThai=N'DaDatCoc' AND NgayDiYeuCau>=@today",Db.P("@id",id),Db.P("@today",DateTime.Today)))
                        if(cmd.ExecuteNonQuery()!=1)throw new ArgumentException("Chi huy duoc doan da dat coc, chua khoi hanh; tien coc khong duoc hoan.");
                    // Release guides reserved for this canceled group.
                    using(var cmd=Db.Command(con,tx,"DELETE FROM dbo.PhanCongHuongDan WHERE MaPhieuDoan=@id",Db.P("@id",id)))cmd.ExecuteNonQuery();
                    tx.Commit();
                }catch{tx.Rollback();throw;}
            }
        }
        public void CompleteGroup(string id)
        {
            var rows=Db.Execute(@"UPDATE d SET TrangThai=N'DaHoanThanh' FROM dbo.PhieuDangKyDoan d JOIN dbo.TourDuLich t ON t.MaTour=d.MaTour
 WHERE d.MaPhieuDoan=@id AND d.TrangThai=N'DaDatCoc' AND DATEADD(day,t.SoNgay-1,d.NgayDiYeuCau)<=@today
 AND EXISTS(SELECT 1 FROM dbo.PhanCongHuongDan pc WHERE pc.MaPhieuDoan=d.MaPhieuDoan)",Db.P("@id",Db.Required(id,"Ma phieu")),Db.P("@today",DateTime.Today));
            if(rows!=1)throw new ArgumentException("Doan chua ve, chua duoc phan cong HDV hoac khong o trang thai dat coc.");
        }
        public decimal SettleGroup(string id)
        {
            id=Db.Required(id,"Ma phieu");
            using(var con=Db.Open())using(var tx=con.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    decimal due;
                    using(var cmd=Db.Command(con,tx,@"SELECT d.SoNguoiDi*d.DonGiaApDung-d.TienCoc FROM dbo.PhieuDangKyDoan d WITH(UPDLOCK,HOLDLOCK)
 WHERE d.MaPhieuDoan=@id AND d.TrangThai=N'DaHoanThanh'",Db.P("@id",id)))
                    {var v=cmd.ExecuteScalar();if(v==null)throw new ArgumentException("Phieu doan phai hoan thanh truoc khi tat toan.");due=(decimal)v;}
                    if(due<=0)throw new InvalidOperationException("Khong con so du can thanh toan.");
                    using(var cmd=Db.Command(con,tx,"INSERT dbo.ThanhToanDoan VALUES(@id,@phieu,@date,@sum)",Db.P("@id",Guid.NewGuid().ToString("N")),Db.P("@phieu",id),Db.P("@date",DateTime.Today),Db.P("@sum",due)))cmd.ExecuteNonQuery();
                    using(var cmd=Db.Command(con,tx,"UPDATE dbo.PhieuDangKyDoan SET TrangThai=N'DaThanhToan' WHERE MaPhieuDoan=@id",Db.P("@id",id)))cmd.ExecuteNonQuery();
                    tx.Commit();return due;
                }catch{tx.Rollback();throw;}
            }
        }
    }
}
