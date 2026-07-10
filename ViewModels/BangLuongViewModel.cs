using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using HR_WPF_FOSCO.Models;
using HR_WPF_FOSCO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HR_WPF_FOSCO.ViewModels
{
    class BangLuongViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BangLuong> BangLuongs { get; set; } =
            new ObservableCollection<BangLuong>();
        public ObservableCollection<BangLuongChiTiet> BangLuongChiTiets { get; set; } =
            new ObservableCollection<BangLuongChiTiet>();
        public ObservableCollection<DonVi> DonVis { get; set; } = new ObservableCollection<DonVi>();
        //khai báo ThangList
        public ObservableCollection<int> ThangList { get; set; } = new ObservableCollection<int>
        {
            1,2,3,4,5,6,7,8,9,10,11,12
        };
        public ObservableCollection<int> NamList { get; set; } = new ObservableCollection<int>
        {
            DateTime.Today.Year - 5,
            DateTime.Today.Year - 4,
            DateTime.Today.Year - 3,
            DateTime.Today.Year - 2,
            DateTime.Today.Year - 1,
            DateTime.Today.Year,
            DateTime.Today.Year + 1
        };
        private int _selectedThang = DateTime.Today.Month;

        public int SelectedThang
        {
            get => _selectedThang;
            set
            {
                _selectedThang = value;
                OnPropertyChanged(nameof(SelectedThang));
                LoadSoGiayBao();
            }
        }
        private int _selectedNam = DateTime.Today.Year;
        public int SelectedNam
        {
            get => _selectedNam;
            set
            {
                _selectedNam = value;
                OnPropertyChanged(nameof(SelectedNam));
                LoadSoGiayBao();
            }
        }
        public ObservableCollection<string> SoGiayBaos { get; } = new();
        private string? _selectedSoGiayBao;

        public string? SelectedSoGiayBao
        {
            get => _selectedSoGiayBao;
            set
            {
                OnPropertyChanged(nameof(SelectedSoGiayBao));
                LoadBangLuongChiTiet();
            }
        }
        private void LoadSoGiayBao()
        {
            SoGiayBaos.Clear();

            if (SelectedDonViID == null)
                return;

            using var db = new AppDbContext();

            var list = db.BangLuongs
                         .Where(x =>
                                x.Thang == SelectedThang &&
                                x.Nam == SelectedNam &&
                                x.ID_DonVi == SelectedDonViID)
                         .Select(x => x.SOGB)
                         .Distinct()
                         .OrderBy(x => x)
                         .ToList();

            foreach (var item in list)
                SoGiayBaos.Add(item);

            SelectedSoGiayBao = SoGiayBaos.FirstOrDefault();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        private readonly AppDbContext _context;
        public BangLuongViewModel()
        {
            _context = new AppDbContext();


            BangLuongChiTiets = new ObservableCollection<BangLuongChiTiet>();



            LoadDonVi();
            LoadBangLuong();
        }
       
        private int? _selectedDonViID;

        public int? SelectedDonViID
        {
            get => _selectedDonViID;
            set
            {
                _selectedDonViID = value;
                OnPropertyChanged(nameof(SelectedDonViID));
                
                LoadSoGiayBao();
            }
        }
        private BangLuong? _selectedBangLuong;

        public BangLuong? SelectedBangLuong
        {
            get => _selectedBangLuong;
            set
            {
                _selectedBangLuong = value;

                OnPropertyChanged(nameof(SelectedBangLuong));

                LoadBangLuongChiTiet();
            }
        }
        //======================================================
        //Tạo bảng lương
        //======================================================
        public void TaoBangLuong()
        {
            if (SelectedDonViID == null)
                return;
            //lấy tên đơn vị
            var donVi = _context.DonVis.Find(SelectedDonViID);

            var bangLuong = new BangLuong
            {
                Thang = SelectedThang,
                Nam = SelectedNam,
                SOGB = donVi.TenDonVi + "/" + SelectedThang + SelectedNam + "/01",

                ID_DonVi = SelectedDonViID,

                NgayTao = DateTime.Now,

                TrangThai = 0
            };

            _context.BangLuongs.Add(bangLuong);

            _context.SaveChanges();

            LoadBangLuong();
        }


        public void LoadBangLuongChiTiet()
        {
            BangLuongChiTiets.Clear();

            if (SelectedBangLuong == null)
                return;

            var list = _context.BangLuongChiTiets
                               .Where(x =>
                                   x.BangLuongID ==
                                   SelectedBangLuong.ID
                                   )
                               .OrderBy(x => x.HoTen)
                               .ToList();

            foreach (var item in list)
            {
                BangLuongChiTiets.Add(item);
            }
        }

        void LoadDonVi()
        {
            DonVis.Clear();
            var donVis = _context.DonVis;
            foreach (var donVi in donVis)
            {
                DonVis.Add(donVi);
            }
        }
        public void LoadBangLuong()
        {
            BangLuongs.Clear();

            var list = _context.BangLuongs
                               .OrderByDescending(x => x.Nam)
                               .ThenByDescending(x => x.Thang)
                               .ToList();

            foreach (var item in list)
            {
                BangLuongs.Add(item);
            }
        }
        //======================================================
        //Tạo bảng lương chi tiết
        //======================================================
        public void TaoBangLuongChiTiet()
        {
            int tgbh = 26065;
            int tglg = 26114;
            int maxBhxh = 46800000;
            int maxBhtn = 106200000;
            decimal? giamTruBanThan = 15500000;
            decimal? tienNguoiPT = 6200000;
            decimal? tileDVbhxh = 0.175m;
            decimal? tileDVbhyt = 0.08m;
            decimal? tileDVbhtn = 0.01m;
            decimal? tileNVbhxh = 0.085m;
            decimal? tileNVbhyt = 0.015m;
            decimal? tileNVbhtn = 0.01m;
            decimal? dongNhanVienHuong=0; decimal? tongLuong = 0; decimal? tongDonvi = 0;
            decimal? TNCN=0; decimal? TTN=0; 


            //if (SelectedBangLuong == null)
            //    return;
            //  var donVi = _context.DonVis.Find(SelectedDonViID);
            var donVi = _context.DonVis.Find(16);
            if (donVi == null)
                return;
            var nhanSus = _context.NhanSus
                                  .Where(x => x.ID_DonVi == donVi.ID_DonVi
                                  && x.TrangThai == true)
                                  .ToList();

            foreach (var nhanSu in nhanSus)
            {
                var qtl =
                _context.QuaTrinhLuongs
                    .Where(x =>
                x.MaNhanSu == nhanSu.MaNhanSu
                && x.DangSuDung == true
                && nhanSu.TrangThai == true)
            .FirstOrDefault();
                //tìm số người phụ thuộc
                var soNguoiPhuThuoc = _context.NguoiPhuThuocs
                    .Where(x => x.MaNhanSu == nhanSu.MaNhanSu
                    && x.DangSuDung=="Đang sử dụng")
                    .Count();
               
                //tìm phụ cấp
                var phuCap = _context.QuaTrinhPhuCaps
                    .Where(x => x.MaNhanSu == nhanSu.MaNhanSu
                    && x.DangSuDung == true)
                  .FirstOrDefault();

              
                decimal? pc_tbh = 0; decimal? pc_tthue = 0;
                if (phuCap != null) {
                    if ( phuCap.TienTe == "D" && phuCap.TienPhuCap > 0)
                    {
                        pc_tbh = phuCap.TienPhuCap - (phuCap.TienAn - phuCap.TienDienThoai - phuCap.TienCongTac
                            - phuCap.TienTrangPhuc - phuCap.TienKhac_Thue);

                        pc_tthue = phuCap.TienPhuCap - (phuCap.TienAn>730000? 730000 : phuCap.TienAn - phuCap.TienDienThoai - phuCap.TienCongTac
                            - phuCap.TienTrangPhuc>416000? 416000 : phuCap.TienTrangPhuc - phuCap.TienKhac_Thue);
                    }
                    else if ( phuCap.TienTe == "U" && phuCap.TienPhuCap > 0)
                    {
                        pc_tbh = (phuCap.TienPhuCap - (phuCap.TienAn - phuCap.TienDienThoai - phuCap.TienCongTac
                            - phuCap.TienTrangPhuc - phuCap.TienKhac_Thue))* tgbh;

                        pc_tthue = phuCap.TienPhuCap*tglg - (phuCap.TienAn*tglg>730000? 730000 : phuCap.TienAn*tglg - 
                            phuCap.TienDienThoai - phuCap.TienCongTac
                            - phuCap.TienTrangPhuc*tglg>416000? 416000 : phuCap.TienTrangPhuc*tglg - phuCap.TienKhac_Thue*tglg);
                    }
                }
                decimal? lcb = 0;
                if(qtl?.TienTe == "D")
                {
                    lcb = qtl.LuongThucTe==0 ? qtl.LuongDongBHXH : qtl.LuongThucTe;
                }
                else if (qtl?.TienTe == "U")
                {
                    lcb =Math.Round((qtl.LuongThucTe == 0 ? qtl.LuongDongBHXH??0 : qtl.LuongThucTe??0) * tglg,0);
                } 

                decimal? lgBHXH = 0; decimal? lgBHTN = 0;
                if (nhanSu.L_HDLD == "7" || nhanSu.L_HDLD == "4" || nhanSu.L_HDLD == "5" && nhanSu.LoaiNhanVien == null)
                { lgBHXH = lgBHTN = 0; }
                else
                {
                    if (qtl?.TienTe == "D")
                    {
                        lgBHXH = Math.Min(qtl?.LuongDongBHXH ?? 0+ pc_tbh ?? 0, maxBhxh);
                        lgBHTN = Math.Min(qtl?.LuongDongBHXH ?? 0+ pc_tbh ?? 0, maxBhtn);
                    }
                    else if (qtl?.TienTe == "U")
                    {
                        lgBHXH = Math.Min(qtl?.LuongDongBHXH ?? 0*tgbh + pc_tbh ?? 0, maxBhxh);
                        lgBHTN = Math.Min(qtl?.LuongDongBHXH ?? 0*tgbh + pc_tbh ?? 0, maxBhtn);
                    }
                }
                decimal? lgtNCN = 0;
                if (nhanSu.LoaiNhanVien != "N")
                    if (nhanSu.L_HDLD == "3") //loại nhân viên hưu trí loại 3
                    {
                        lgtNCN = Math.Round((lcb ?? 0) + (phuCap?.TienTe == "D" ? phuCap?.TienPhuCap ?? 0 : (phuCap?.TienPhuCap ?? 0) * tglg)
                           + ((lgBHXH ?? 0) * (tileDVbhxh ?? 0 + tileDVbhyt ?? 0 + tileDVbhtn ?? 0)), 0);
                    }
                    else
                        lgtNCN = Math.Round((lcb ?? 0) + (phuCap?.TienTe == "D" ? phuCap?.TienPhuCap ?? 0 :
                            (phuCap?.TienPhuCap ?? 0) * tglg), 0);
                //phần đóng bảo hiểm của Đơn vị
                var tyle = _context.DM_OptionNhanViens.Where(x => x.BaoHiemDacBiet == nhanSu.BaoHiemDacBiet).FirstOrDefault();
                    if(tyle != null)
                {
                    tileDVbhxh =tyle.DVBHXH;
                    tileDVbhyt = tyle.DVBHYT;
                    tileDVbhtn = tyle.DVBHTN;
                    tileNVbhxh = tyle.NVBHXH;
                    tileNVbhyt = tyle.NVBHYT;
                    tileNVbhtn = tyle.NVBHTN;
                }
                //Tính tiền giảm trừ bản thân               
               if(nhanSu.L_HDLD == "7" || nhanSu.L_HDLD == "2" || nhanSu.LoaiNhanVien == "N" )
                {
                    giamTruBanThan = 0;
                }
                //tính tiền thuế thu nhập cá nhân                
                if(nhanSu.L_HDLD == "7" && lgtNCN >= 2000000)
                {
                    TNCN = lgtNCN;
                }
                else if (nhanSu.L_HDLD == "7" || nhanSu.LoaiNhanVien == "N" || donVi.LoaiLuong == "S" || (nhanSu.LoaiNhanVien == "N" && nhanSu.L_HDLD == null) || (donVi.MaDonVi.StartsWith("B") && donVi.LoaiLuong != "K"))
                {
                    TNCN = 0;
                }
                else
                {
                    TNCN = lgtNCN-(lgBHXH*(tileNVbhxh+tileNVbhyt)+(lgBHTN * tileNVbhtn) +giamTruBanThan+tienNguoiPT*soNguoiPhuThuoc) ;
                    TNCN=Math.Round(TNCN ?? 0, 0);
                }
              
                //Tính tiền thuế thu nhập cá nhân
                if (donVi.LoaiLuong == "S" || nhanSu.L_HDLD == "7" || nhanSu.LoaiNhanVien == "N" 
                    || (nhanSu.LoaiNhanVien == "N" && nhanSu.L_HDLD == null) 
                    || (donVi.MaDonVi.StartsWith("B") && donVi.LoaiLuong != "K"))
                {
                    TTN = 0;
                }
                else
                {
                    if (TNCN <= 10000000)
                        TTN = TNCN * 0.05m;
                    else if (TNCN <= 30000000)
                        TTN = TNCN * 0.1m - 500000;
                    else if (TNCN <= 60000000)
                        TTN = TNCN * 0.2m - 3500000;
                    else if (TNCN <= 100000000)
                        TTN = TNCN * 0.3m - 9500000;
                    else
                        TTN = TNCN * 0.35m - 14500000;
                }
                TTN=TTN < 0 ? 0 : Math.Round(TTN ?? 0, 0);

                //Tính tiền cho nhân viên phụ thuộc vào loại tiền lương của đơn vị và loại nhân viên
                //DNVHG=ROUND((CASE WHEN LOAINV='N' AND INHD IS NULL THEN 0 ELSE TONGLUONG-TC_NV - PHI_CD  -(CASE WHEN RIGHT(LOAILG,1)='S' THEN TTN_TEMP ELSE (CASE WHEN TTN<0 THEN 0 ELSE TTN END )END) + (CASE WHEN L_HDLD='3' THEN TC_DN ELSE 0 END)END),0)  WHERE DV='" & sDonVi & "'"

                //sql = "update " & stentb & " set DNVHG=ROUND((CASE WHEN (LOAILG='D' OR LOAILG='K') AND L_HDLD='3' " _
                //& " THEN LGTNCN+(case when tienpc>0 then (CASE WHEN DVTPC='U' THEN TIENPC*TGLG-PC_TTHUE ELSE TIENPC-PC_TTHUE END)ELSE 0 END) -TTN-PHI_CD ELSE DNVHG END),0)  WHERE DV='" & sDonVi & "'"
                tongLuong = Math.Round(lcb ?? +(phuCap?.TienTe == "D" ? phuCap?.TienPhuCap ?? 0
                    : (phuCap?.TienPhuCap ?? 0) * tglg), 0);
                tongDonvi = Math.Round((lgBHXH ?? 0) * (tileDVbhxh ?? 0) + (lgBHXH ?? 0) * (tileDVbhyt ?? 0) + (lgBHTN ?? 0) * (tileDVbhtn ?? 0), 0);

                if (nhanSu.LoaiNhanVien == "N" && nhanSu.L_HDLD == null)
                {
                    dongNhanVienHuong = 0;
                }
                else if(nhanSu.L_HDLD == "3")
                {
                    dongNhanVienHuong = tongLuong - TTN;
                }


                var chiTiet = new BangLuongChiTiet
                {
                    BangLuongID = SelectedBangLuong.ID,
                    MaNhanSu = nhanSu.MaNhanSu,
                    HoTen = nhanSu.HoTen,
                    LOAINV = nhanSu.LoaiNhanVien,
                    L_HDLD = nhanSu.L_HDLD,
                    LOAILG = donVi.LoaiLuong,   
                    DVT = qtl?.TienTe ?? "D",
                    LCV = qtl?.LuongDongBHXH ?? 0,
                    LTLD = qtl?.LuongThucTe ?? 0,
                    DLCV= qtl?.TienTe == "D" ? qtl?.LuongDongBHXH ?? 0 : (qtl?.LuongDongBHXH ?? 0) * tgbh,
                    LuongCoBan = qtl?.TienTe == "D" ? qtl?.LuongThucTe ?? 0 : (qtl?.LuongThucTe ?? 0) * tglg,
                    SOPT = soNguoiPhuThuoc,
                    DVTPC = phuCap?.TienTe ?? "D",
                    TIENPC = phuCap?.TienPhuCap ?? 0,
                    AN = phuCap?.TienAn ?? 0,
                    DT = phuCap?.TienDienThoai ?? 0,
                    CT = phuCap?.TienCongTac ?? 0,
                    TP = phuCap?.TienTrangPhuc ?? 0,
                    KHAC = phuCap?.TienKhac_Thue ?? 0,
                    KHAC_KTHUE = phuCap?.TienKhac_KhongThue ?? 0,
                    PC_TBH =Math.Round( pc_tbh ?? 0,0),
                    PC_TTHUE =Math.Round( pc_tthue ?? 0,0),
                    TONGLUONG =tongLuong,
                    LGBHXH=Math.Round(lgBHXH ?? 0, 0),
                    LGBHTN = Math.Round(lgBHTN ?? 0, 0),
                    LGTNCN = lgtNCN ?? 0,
                    DV_BHXH = Math.Round((lgBHXH ?? 0) * (tileDVbhxh ?? 0), 0),
                    DV_BHYT = Math.Round((lgBHXH ?? 0) * (tileDVbhyt ?? 0), 0),
                    DV_BHTN = Math.Round((lgBHXH ?? 0) * (tileDVbhtn ?? 0), 0),
                    NV_BHXH = Math.Round((lgBHXH ?? 0) * (tileNVbhxh ?? 0), 0),
                    NV_BHYT = Math.Round((lgBHXH ?? 0) * (tileNVbhyt ?? 0), 0),
                    NV_BHTN = Math.Round((lgBHXH ?? 0) * (tileNVbhtn ?? 0), 0),
                    Giam_TBT = giamTruBanThan,
                    ST_NGPT = tienNguoiPT*soNguoiPhuThuoc,
                    TTN = TTN,
                    TNCN = TNCN,

                };
                _context.BangLuongChiTiets.Add(chiTiet);
            }
            _context.SaveChanges();
            LoadBangLuongChiTiet();
        }
    }
}
