using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR_WPF_FOSCO.Models
{
    [Table("DonVi")]
    public class DonVi
    {
        [Key]
        public int ID_DonVi { get; set; }
        public string MaDonVi { get; set; }=string.Empty;
        public string TenDonVi { get; set; } = string.Empty;
		public string? MaSoThue { get; set; }
        public string? DiaChi   {            get; set;        }
        public string? SoDienThoai        {            get; set;        }
		public	string? Email { get; set; }
        public bool? TrangThai { get; set; }=true;
        public DateTime? NgayTao { get; set; }=DateTime.Now;
        public String? NoiNopBHXH { get; set; } = null!;
        public String? NganHangNopBHXH { get; set; } = null!;
        public String? SoTaiKhoanNopBHXH { get; set; }
        public string? NgayCapMaSoThue { get; set; }
        public string? MaBHXH { get; set; }
        public string? MaBHXH_NN { get; set; }
        public string? NgayNghiHangTuan { get; set; }
        public string? LoaiLuong { get; set; }
        public string? GiayPhepDonVi { get; set; }
        public string? NgayCapGiayPhepDonVi { get; set; }
        public string? CoQuanCapGiayPhepDonVi { get; set; }
        public string? DichVuPhi { get; set; }
        public string? HoTenNguoiLienHeKhanCap { get; set; }
        public string? QuanHeLienHeKhanCap { get; set; }
        public string? DienThoaiDiDongLienHeKhanCap { get; set; }
        public string? EmailLienHeKhanCap { get; set; }
        public string? DiaChiLienHeKhanCap { get; set; }
        public string? GhiChu { get; set; }
        public DateTime? NgayKyHopDong { get; set; }
        public DateTime? NgayKetThucHopDong { get; set; }
        public DateTime? NgayVao   { get; set; }
        public String? NoiNopThue { get; set; } = null!;
        public String? NganHangNopThue { get; set; } = null!; 
        public String? SoTaiKhoanNopThue { get;set; }
        public int?Stt { get; set; }
        [NotMapped]
        public string TenHienThi
        {
            get
            {
                return $"{MaDonVi} - {TenDonVi}";
            }
        }




    }
}
