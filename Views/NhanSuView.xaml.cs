using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HR_WPF_FOSCO.Views
{
    /// <summary>
    /// Interaction logic for NhanSuView.xaml
    /// </summary>
    public partial class NhanSuView : UserControl
    {
        public NhanSuView()
        {
            InitializeComponent();
            DataContext = new ViewModels.NhanSuViewModel();
        }

        public void BtnAddLuong_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.AddQuaTrinhLuong();
        }
        public void BtnSaveLuong_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.SaveQuaTrinhLuong();
        }
        public void BtnImportLuong_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.ImportQuaTrinhLuong();
        }
        public void BtnDeleteLuong_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.DeleteQuaTrinhLuong();
        }
        public void BtnAddPhuCap_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.AddQuaTrinhPhuCap();
        }
        public void BtnSavePhuCap_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.SaveQuaTrinhPhuCap();
        }
        public void BtnImportPhuCap_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.ImportQuaTrinhPhuCap();
        }
        public void BtnDeletePhuCap_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.DeleteQuaTrinhPhuCap();
        }
        public void BtnAddNguoiPhuThuoc_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.AddNguoiPhuThuoc();
        }
        public void BtnSaveNguoiPhuThuoc_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.SaveNguoiPhuThuoc();
        }
        public void BtnImportNguoiPhuThuoc_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.ImportNguoiPhuThuoc();
        }
            public void BtnDeleteNguoiPhuThuoc_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.NhanSuViewModel)?.DeleteNguoiPhuThuoc();
        }
    }
}
