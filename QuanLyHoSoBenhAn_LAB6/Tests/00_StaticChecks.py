"""Offline structural sanity checks. Does NOT compile .NET or execute SQL Server."""
from pathlib import Path
from xml.etree import ElementTree as ET
import re

root = Path(__file__).resolve().parents[1]
proj = root / 'src/QuanLyHoSoBenhAn/QuanLyHoSoBenhAn.csproj'
ns = '{http://schemas.microsoft.com/developer/msbuild/2003}'
project_files = [n.attrib['Include'].replace('\\', '/') for n in ET.parse(proj).iter(ns+'Compile')]
expected = ['BenhNhan','PhieuDangKy','PhieuThuLePhi','HoSoBenhAn',
            'PhieuKhamBenh','BacSi','KhoaDieuTri','ToaThuoc','ChiTietToaThuoc',
            'Thuoc','LoaiXetNghiem','PhieuXetNghiem','LichHen','NhanVienCSKH',
            'HoaDonVienPhi','TheoDoiDieuTri']
schema=(root/'Database/01_Schema.sql').read_text(encoding='utf-8')
procedures=(root/'Database/02_Procedures.sql').read_text(encoding='utf-8')
model=(root/'src/QuanLyHoSoBenhAn/Models/Entities.cs').read_text(encoding='utf-8')
forms=(root/'src/QuanLyHoSoBenhAn/Forms/FrmMain.cs').read_text(encoding='utf-8')
checks = {
  'solution exists': (root/'src/QuanLyHoSoBenhAn.sln').exists(),
  'project targets net472': '<TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>' in proj.read_text(),
  'all C# Compile Include paths exist': len(project_files)>=16 and all((proj.parent/p).exists() for p in project_files),
  '16 UML domain tables': all('CREATE TABLE dbo.'+name+' (' in schema for name in expected),
  '16 C# entity classes': all('class '+name+'\n' in model for name in expected),
  '14 SQL stored procedures': len(re.findall(r'CREATE OR ALTER PROCEDURE\s+dbo\.', procedures))==14,
  'SQL registration transaction': 'BEGIN TRAN;' in procedures and 'usp_DangKyLanDau' in procedures,
  'SQL filtered optional appointment uniqueness': 'WHERE MaLichHen IS NOT NULL' in schema,
  'quarterly filtered unique index': 'UX_PhieuKham_HoSo_Quy' in schema,
  'payment gate': "TrangThaiThanhToan='DA_THANH_TOAN'" in procedures,
  '8 WinForms tabs': forms.count('AddTab("')==8,
  '6 UML source diagrams': len(list((root/'UML').glob('*.puml')))==6,
  'SQL integration tests and traceability docs': (root/'Tests/01_TestCases.sql').exists() and (root/'Docs/TruyVetYeuCau.md').exists(),
  'original task document included': (root/'Docs/DeBaiGoc.docx').exists(),
}
for name, ok in checks.items():
    print(('PASS' if ok else 'FAIL')+' | '+name)
print('RESULT: '+str(sum(checks.values()))+'/'+str(len(checks))+' structural checks PASS')
if not all(checks.values()):
    raise SystemExit(1)
