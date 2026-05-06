using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Principal;
using System.Text;

namespace HR_WPF_FOSCO.Models
{
   public class HopDongDichVu
    {
        [Key]
        public int ID { get; set; }
        public int ID_DonVi { get; set; }
        public string? SoHopDong { get; set; }
        public string TenHopDong { get; set; } = string.Empty;

        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public string? LoaiLuong { get; set; }
        public string? DichVuPhi { get; set; }
        public string? DaiDienKhachHang { get; set; }
        public string? DaiDienCongTy { get; set; }
        public string? NhanSuTheoDoi { get; set; }
        public string? GhiChu { get; set; }
        
            }
}

