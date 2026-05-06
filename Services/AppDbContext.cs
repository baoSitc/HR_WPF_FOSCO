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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer(
@"Data Source=192.0.0.251\sqlexpress;
Initial Catalog=HR_WPF;
User Id=sa;
Password=Mclcnnbc@123Encovy;
TrustServerCertificate=True;");
        }
    }
}
