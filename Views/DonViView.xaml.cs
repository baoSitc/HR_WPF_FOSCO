using HR_WPF_FOSCO.ViewModels;
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
    /// Interaction logic for DonViView.xaml
    /// </summary>
    public partial class DonViView : UserControl
    {
        DonViViewModel vm = new DonViViewModel();
        public DonViView()
        {
            InitializeComponent();
            DataContext = vm;
           
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            vm.AddDonVi();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            vm.UpdateDonVi();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            vm.DeleteDonVi();
        }
      
    }
}
