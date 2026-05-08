using HR_WPF_FOSCO.Views;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

namespace HR_WPF_FOSCO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            // load màn đầu tiên
            OpenTab(
         "Quản lý đơn vị",
         "OfficeBuilding",
         new DonViView());
        }

        private void OpenTab(string title,
                      string iconKind,
                      UserControl control)
        {
            foreach (TabItem item in MainTabControl.Items)
            {
                StackPanel header = item.Header as StackPanel;

                if (header != null)
                {
                    TextBlock txt = header.Children[1] as TextBlock;

                    if (txt != null && txt.Text == title)
                    {
                        MainTabControl.SelectedItem = item;
                        return;
                    }
                }
            }

            TabItem tab = new TabItem();

            tab.Header = CreateTabHeader(title, iconKind);

            tab.Content = control;

            MainTabControl.Items.Add(tab);

            MainTabControl.SelectedItem = tab;
        }

        private void BtnDonVi_Click(object sender, RoutedEventArgs e)
        {
            OpenTab(
        "Quản lý đơn vị",
        "OfficeBuilding",
        new DonViView());
        }

        private void BtnNhanSu_Click(object sender, RoutedEventArgs e)
        {
            OpenTab("Quản lý nhân sự", "User", new Views.NhanSuView());
        }

        private void BtnHopDong_Click(object sender, RoutedEventArgs e)
        {
            OpenTab("Quản lý hợp đồng", "FileDocument", new Views.HopDongDichVuView());
        }

        private void BtnNguoiPT_Click(object sender, RoutedEventArgs e)
        {
            OpenTab("Quản lý người phụ thuộc", "User", new Views.NguoiPhuThuocView());
        }

        private void BtnLuong_Click(object sender, RoutedEventArgs e)
        {
            OpenTab("Quản lý bảng lương", "Cash", new Views.BangLuongView());
        }
        private StackPanel CreateTabHeader(string title, string iconKind)
        {
            StackPanel sp = new StackPanel();

            sp.Orientation = Orientation.Horizontal;

            var icon = new MaterialDesignThemes.Wpf.PackIcon();

            icon.Kind = (MaterialDesignThemes.Wpf.PackIconKind)
                Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), iconKind);

            icon.Width = 18;
            icon.Height = 18;
            icon.Margin = new Thickness(0, 0, 5, 0);

            TextBlock txt = new TextBlock();

            txt.Text = title;

            sp.Children.Add(icon);

            sp.Children.Add(txt);

            return sp;
        }
    }
}