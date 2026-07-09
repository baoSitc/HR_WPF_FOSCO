using HR_WPF_FOSCO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR_WPF_FOSCO.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<DonVi> DonVis { get; set; }
        public DbSet<HopDongDichVu> HopDongDichVus { get; set; }
        public DbSet<HopDongDinhKem> HopDongDinhKems { get; set; }      
        public DbSet<NhanSu> NhanSus { get; set; }
        public DbSet<QuaTrinhLuong> QuaTrinhLuongs   { get; set; }
        public DbSet<NguoiPhuThuoc> NguoiPhuThuocs { get; set; }
        public DbSet<QuaTrinhPhuCap> QuaTrinhPhuCaps { get; set; }        
        public DbSet<BangLuong> BangLuongs { get; set; }
        public DbSet<BangLuongChiTiet> BangLuongChiTiets { get; set; }
        public DbSet<DM_OptionNhanVien> DM_OptionNhanViens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer(
@"Data Source=115.79.61.102,1433\sqlexpress;
Initial Catalog=HR_WPF;
User Id=sa;
Password=Mclcnnbc@123Encovy;
TrustServerCertificate=True;");
        }
    }
}
