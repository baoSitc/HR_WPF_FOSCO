using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Security.Principal;
using System.Text;

namespace HR_WPF_FOSCO.Models
{
    [Table("HopDongDichVu")]
    
   public class HopDongDichVu
    {
        [Key]
        public int ID_HopDong { get; set; }
        public int ID_DonVi { get; set; }
        public string? SoHopDong { get; set; }
        public string TenHopDong { get; set; } = string.Empty;
        public DateTime? NgayKy { get; set; }
        public DateTime? TuNgay { get; set; }
        public DateTime? DenNgay { get; set; }
        public string? LoaiLuong { get; set; } //Net hay Gross
        public string? DichVuPhi { get; set; }
        public string? DaiDienKhachHang { get; set; }
        public string? DaiDienCongTy { get; set; }
        public string? NhanSuTheoDoi { get; set; }
        public string? GhiChu { get; set; }
        public int TrangThai { get; set; } = 1;
        public DateTime? NgayTao { get; set; } = DateTime.Now;
        //navigation property
        // FOREIGN KEY
        [ForeignKey("ID_DonVi")]
        public DonVi DonVi { get; set; }

    }
}

