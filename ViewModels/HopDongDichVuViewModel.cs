using HR_WPF_FOSCO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Data;
using Windows.Data;

namespace HR_WPF_FOSCO.ViewModels
{
    public class HopDongDichVuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //tìm kiếm hợp đồng dịch vụ
        private ICollectionView _hopDongDichVuView;
        public ICollectionView HopDongDichVuView
        {
            get { return _hopDongDichVuView; }
            set
            {
                _hopDongDichVuView = value;
                OnPropertyChanged(nameof(HopDongDichVuView));
            }
        }

        //Danh sách hợp đồng dịch vụ
        private ObservableCollection<Models.HopDongDichVu> _hopDongDichVus;
        public ObservableCollection<Models.HopDongDichVu> HopDongDichVus
        {
            get { return _hopDongDichVus; }
            set
            {
                _hopDongDichVus = value;
                OnPropertyChanged(nameof(HopDongDichVus));
            }
        }
        //Dòng hợp đồng dịch vụ đang được chọn
        private Models.HopDongDichVu _selectedHopDongDichVu;
        public Models.HopDongDichVu SelectedHopDongDichVu
        {
            get { return _selectedHopDongDichVu; }
            set
            {
                _selectedHopDongDichVu = value;
                OnPropertyChanged(nameof(SelectedHopDongDichVu));
            }

        }
        //Danh sách đơn vị
        private ObservableCollection<Models.DonVi> _donVis;
        public ObservableCollection<Models.DonVi> DonVis
        {
            get { return _donVis; }
            set
            {
                _donVis = value;
                OnPropertyChanged(nameof(DonVis));
            }
        }

        //Constructor
        public HopDongDichVuViewModel()
        {
            LoadDonVis();
            LoadHopDong();
            SelectedHopDongDichVu = new Models.HopDongDichVu();
        }

        private void LoadDonVis()
        {
            using var db = new Services.AppDbContext();
            DonVis = new ObservableCollection<Models.DonVi>(db.DonVis.OrderBy(d => d.MaDonVi).ToList());
        }
        //Load hợp đồng dịch vụ
        private void LoadHopDong()
        {
            using var db = new Services.AppDbContext();
            HopDongDichVus = new ObservableCollection<Models.HopDongDichVu>(db.HopDongDichVus.OrderBy(h => h.ID_HopDong).Include(h => h.DonVi).ToList());
            HopDongDichVuView = CollectionViewSource.GetDefaultView(HopDongDichVus);
        }
        //Thêm hợp đồng dịch vụ
        public void AddHopDongDichVu()
        {
            using var db = new Services.AppDbContext();
            SelectedHopDongDichVu.ID_HopDong = 0;
            db.HopDongDichVus.Add(SelectedHopDongDichVu);
            db.SaveChanges();
            LoadHopDong();
            SelectedHopDongDichVu = new Models.HopDongDichVu();
            OnPropertyChanged(nameof(SelectedHopDongDichVu));

        }
        //Cập nhật hợp đồng dịch vụ
        public void UpdateHopDongDichVu()
        {
            using var db = new Services.AppDbContext();
            var hopdong = db.HopDongDichVus.Find(SelectedHopDongDichVu.ID_HopDong);
            if (hopdong != null)
            {
                hopdong.SoHopDong = SelectedHopDongDichVu.SoHopDong;
                hopdong.TenHopDong = SelectedHopDongDichVu.TenHopDong;
                hopdong.NgayKy = SelectedHopDongDichVu.NgayKy;
                hopdong.TuNgay = SelectedHopDongDichVu.TuNgay;
                hopdong.DenNgay = SelectedHopDongDichVu.DenNgay;
                hopdong.LoaiLuong = SelectedHopDongDichVu.LoaiLuong;
                hopdong.DaiDienCongTy = SelectedHopDongDichVu.DaiDienCongTy;
                hopdong.DaiDienKhachHang = SelectedHopDongDichVu.DaiDienKhachHang;
                hopdong.NhanSuTheoDoi = SelectedHopDongDichVu.NhanSuTheoDoi;
                hopdong.GhiChu = SelectedHopDongDichVu.GhiChu;
                hopdong.ID_DonVi = SelectedHopDongDichVu.ID_DonVi;
                hopdong.TrangThai = SelectedHopDongDichVu.TrangThai;
                db.SaveChanges();
                LoadHopDong();
            }
        }
        //Xóa hợp đồng dịch vụ
        public void DeleteHopDongDichVu()
        {
            using var db = new Services.AppDbContext();
            var hopdong = db.HopDongDichVus.Find(SelectedHopDongDichVu.ID_HopDong);
            if (hopdong != null)
            {
                db.HopDongDichVus.Remove(hopdong);
                db.SaveChanges();
                LoadHopDong();
                SelectedHopDongDichVu = new Models.HopDongDichVu();
                OnPropertyChanged(nameof(SelectedHopDongDichVu));
            }
        }
        //làm mới form
        public void ClearForm()
        {
            SelectedHopDongDichVu = new Models.HopDongDichVu();
            OnPropertyChanged(nameof(SelectedHopDongDichVu));
        }
        public void SearchHopDongDichVu(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                HopDongDichVuView.Filter = null;
            }
            else
            {
                HopDongDichVuView.Filter = obj =>
                {
                    if (obj is Models.HopDongDichVu hopdong)
                    {
                        return hopdong.SoHopDong.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                               hopdong.TenHopDong.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                               hopdong.DonVi.TenDonVi.Contains(keyword, StringComparison.OrdinalIgnoreCase);
                    }
                    return false;
                };
            }
        }
    }
}
