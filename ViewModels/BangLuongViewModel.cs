using DocumentFormat.OpenXml.Math;
using HR_WPF_FOSCO.Models;
using HR_WPF_FOSCO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;

namespace HR_WPF_FOSCO.ViewModels
{
    class BangLuongViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BangLuong> BangLuongs { get; set; }=
            new ObservableCollection<BangLuong>();
        public ObservableCollection<BangLuongChiTiet> BangLuongChiTiets { get; set; }= 
            new ObservableCollection<BangLuongChiTiet>();
        public ObservableCollection<DonVi> DonVis { get; set; } = new ObservableCollection<DonVi>();

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
        private int _thang = DateTime.Today.Month;

        public int Thang
        {
            get => _thang;
            set
            {
                _thang = value;
                OnPropertyChanged(nameof(Thang));
            }
        }
        private int _nam = DateTime.Today.Year;
        public int Nam
        {
            get => _nam;
            set
            {
                _nam = value;
                OnPropertyChanged(nameof(Nam));
            }
        }
        private int? _selectedDonViID;

        public int? SelectedDonViID
        {
            get => _selectedDonViID;
            set
            {
                _selectedDonViID = value;
                OnPropertyChanged(nameof(SelectedDonViID));
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
                Thang = Thang,
                Nam = Nam,
                SOGB = donVi.TenDonVi + "/" + Thang + Nam+"/01",

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
                                   SelectedBangLuong.ID)
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
            if (SelectedBangLuong == null)
                return;
            var donViID = SelectedBangLuong.ID_DonVi;
            if (donViID == null)
                return;
            var nhanSus = _context.NhanSus
                                  .Where(x => x.ID_DonVi == donViID
                                  && x.TrangThai == true)
                                  .ToList();

            foreach (var nhanSu in nhanSus)
            {
                var qtl =
                _context.QuaTrinhLuongs
                    .Where(x =>
                x.MaNhanSu == nhanSu.MaNhanSu
                && x.DangSuDung==true)         
            .FirstOrDefault();
                string? loaiNV = nhanSu.LoaiNhanVien;
                string? L_HDLD = nhanSu.L_;
                double luongThucTe =
                         qtl?.LuongThucTe ?? 0;

                double luongBHXH =
                    qtl?.LuongDongBHXH ?? 0;


                var chiTiet = new BangLuongChiTiet
                {
                    BangLuongID = SelectedBangLuong.ID,
                    MaNhanSu = nhanSu.MaNhanSu,
                    HoTen = nhanSu.HoTen,
                    LOAINV = "NV",
                    L_HDLD = "HDLĐ",
                    LOAILG = "LG1",
                    DVT = "D",
                    DVTPC = "D"
                };
                _context.BangLuongChiTiets.Add(chiTiet);
            }
            _context.SaveChanges();
            LoadBangLuongChiTiet();
        }
}
