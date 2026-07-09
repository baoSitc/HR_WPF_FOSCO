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
    /// Interaction logic for BangLuongView.xaml
    /// </summary>
    public partial class BangLuongView : UserControl
    {
        public BangLuongView()
        {
            InitializeComponent();
            DataContext = new ViewModels.BangLuongViewModel();
        }
        void btnXemChiTietBangLuong_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as ViewModels.BangLuongViewModel;
            (DataContext as ViewModels.BangLuongViewModel)?.TaoBangLuongChiTiet();
        }
    }
}
