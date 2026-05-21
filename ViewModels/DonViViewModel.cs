using HR_WPF_FOSCO.Models;
using HR_WPF_FOSCO.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace HR_WPF_FOSCO.ViewModels
{
    public class DonViViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Models.DonVi> DonVis { get; set; }
        private Models.DonVi _selectedDonvi=new Models.DonVi();
        public DonVi SelectedDonvi
        {
            get { return _selectedDonvi; }
            set
            {
                _selectedDonvi = value;
                //   MessageBox.Show(_selectedDonvi?.TenDonVi);
                OnPropertyChanged(nameof(SelectedDonvi));
            }
        }
        private string _tuKhoa;

        public string TuKhoa
        {
            get => _tuKhoa;
            set
            {
                _tuKhoa = value;

                OnPropertyChanged(nameof(TuKhoa));

                TimKiem();
            }
        }
        public void TimKiem()
        {
            if (string.IsNullOrWhiteSpace(TuKhoa))
            {

                OnPropertyChanged(nameof(DonVi));
                return;
            }

            var data = new AppDbContext().DonVis
                .Where(x =>
                    x.TenDonVi.Contains(TuKhoa)
                ||
                x.MaDonVi.Contains(TuKhoa)
                ||
                x.MaSoThue.Contains(TuKhoa))
                .ToList();

            DonVis =
                new ObservableCollection<Models.DonVi>(data);
            foreach (var donvi in DonVis)
            {
                donvi.Stt = DonVis.IndexOf(donvi) + 1;

            }
            OnPropertyChanged(nameof(DonVis));
        }



        public DonViViewModel()
        {
            LoadDonVis();
            SelectedDonvi=new Models.DonVi();

        }
        void LoadDonVis()
        {
            using var db = new AppDbContext();
            DonVis = new ObservableCollection<Models.DonVi>(db.DonVis.OrderBy(d => d.MaDonVi).ToList());
            foreach (var donvi in DonVis)
            {
                donvi.Stt = DonVis.IndexOf(donvi) + 1;
                
            }

            OnPropertyChanged(nameof(DonVis));

        }
        public void AddDonVi()
        {
            using var db = new AppDbContext();
            SelectedDonvi.ID_DonVi = 0;
            db.DonVis.Add(SelectedDonvi);
            db.SaveChanges();
            LoadDonVis();
            MessageBox.Show("Thêm đơn vị thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        public void UpdateDonVi()
        {
            using var db = new AppDbContext();
            var donvi = db.DonVis.Find(SelectedDonvi.ID_DonVi);
            if (donvi != null)
            {
                donvi.TenDonVi = SelectedDonvi.TenDonVi;
                donvi.DiaChi = SelectedDonvi.DiaChi;
                donvi.MaDonVi = SelectedDonvi.MaDonVi;
                donvi.MaSoThue = SelectedDonvi.MaSoThue;
                donvi.GhiChu = SelectedDonvi.GhiChu;
                donvi.DichVuPhi = SelectedDonvi.DichVuPhi;
                donvi.Email = SelectedDonvi.Email;
                donvi.SoDienThoai = SelectedDonvi.SoDienThoai;
                donvi.LoaiLuong = SelectedDonvi.LoaiLuong;
                donvi.MaBHXH = SelectedDonvi.MaBHXH;
                donvi.MaBHXH_NN = SelectedDonvi.MaBHXH_NN;
                donvi.NganHangNopBHXH = SelectedDonvi.NganHangNopBHXH;
                donvi.NoiNopBHXH = SelectedDonvi.NoiNopBHXH;
                donvi.SoTaiKhoanNopBHXH = SelectedDonvi.SoTaiKhoanNopBHXH;
                donvi.NoiNopThue = SelectedDonvi.NoiNopThue;
                donvi.SoTaiKhoanNopThue = SelectedDonvi.SoTaiKhoanNopThue;
                donvi.NoiNopThue = SelectedDonvi.NoiNopThue;
                donvi.TrangThai = SelectedDonvi.TrangThai;
                db.SaveChanges();
                LoadDonVis();
                MessageBox.Show("Cập nhật đơn vị thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void DeleteDonVi()
        {
            using var db = new AppDbContext();
            var donvi = db.DonVis.Find(SelectedDonvi.ID_DonVi);
            if (donvi != null)
            {
                db.DonVis.Remove(donvi);
                db.SaveChanges();
                LoadDonVis();
                MessageBox.Show("Xóa đơn vị thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
