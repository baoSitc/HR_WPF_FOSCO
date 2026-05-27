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
    }
}
