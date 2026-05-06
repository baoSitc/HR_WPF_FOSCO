using HR_WPF_FOSCO.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HR_WPF_FOSCO.ViewModels
{
  public   class MainViewModel
    {
        public ObservableCollection<DonVi> DonVis { get; set; } 
        public MainViewModel()
        {
            // Khởi tạo các ViewModel khác nếu cần
            LoadData();
        }
        void LoadData()
        {
            using (var context = new Services.AppDbContext())
            {
                DonVis = new ObservableCollection<DonVi>(context.DonVis.ToList());
            }
        }
    }
}
