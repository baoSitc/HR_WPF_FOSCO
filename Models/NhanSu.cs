using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_WPF_FOSCO.Models
{
    [Table("NhanSu")]
    public class NhanSu
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        [Key]
        [StringLength(10)]
        public string MaNhanSu { get; set; } = string.Empty;

        // =====================================================
        // FOREIGN KEY
        // =====================================================

        public int? ID_DonVi { get; set; }

        [ForeignKey(nameof(ID_DonVi))]
        public virtual DonVi? DonVi { get; set; }

        // =====================================================
        // THÔNG TIN CƠ BẢN
        // =====================================================

        [StringLength(150)]
        public string? HoTen { get; set; }

        public DateTime? NgaySinh { get; set; }

        public bool? GioiTinh { get; set; }

        [StringLength(20)]
        public string? CCCD { get; set; }

        [StringLength(30)]
        public string? MaSoThueCaNhan { get; set; }

        [StringLength(30)]
        public string? SoBHXH { get; set; }

        public bool? TrangThai { get; set; }
        public String? TrangThaiLamViec {
            get
            {
                if (TrangThai == null)
                    return "Không xác định";
                else if (TrangThai == true)
                    return "Đang làm việc";
                else
                    return "Đã nghỉ việc";
            }
        }

        // =====================================================
        // LIÊN HỆ
        // =====================================================

        [StringLength(50)]
        public string? DienThoaiDiDong { get; set; }

        [StringLength(50)]
        public string? DienThoaiCoQuan { get; set; }

        [StringLength(50)]
        public string? EmailCoQuan { get; set; }

        [StringLength(50)]
        public string? EmailCaNhan { get; set; }

        // =====================================================
        // NGUYÊN QUÁN
        // =====================================================

        [StringLength(255)]
        public string? NguyenQuan { get; set; }

        [StringLength(255)]
        public string? TinhThanhPhoNguyenQuan { get; set; }

        [StringLength(255)]
        public string? NoiSinh { get; set; }

        // =====================================================
        // THUẾ
        // =====================================================

        public DateTime? NgayCapMaSoThue { get; set; }

        // =====================================================
        // ĐỊA CHỈ HIỆN NAY
        // =====================================================

        [StringLength(255)]
        public string? ChoOHienNay { get; set; }

        [StringLength(255)]
        public string? XaPhuongHienNay { get; set; }

        [StringLength(255)]
        public string? QuanHuyenHienNay { get; set; }

        [StringLength(255)]
        public string? TinhThanhPhoHienNay { get; set; }

        [StringLength(255)]
        public string? SoNhaDuongHienNay { get; set; }

        [StringLength(255)]
        public string? QuocGiaHienNay { get; set; }

        // =====================================================
        // NGÂN HÀNG
        // =====================================================

        [StringLength(30)]
        public string? SoTaiKhoanNganHang { get; set; }

        [StringLength(255)]
        public string? NganHang { get; set; }

        [StringLength(255)]
        public string? ChiNhanhNganHang { get; set; }

        // =====================================================
        // BHXH
        // =====================================================

        [StringLength(50)]
        public string? SoSoBHXH { get; set; }

        [StringLength(50)]
        public string? MaSoBHXH { get; set; }

        // =====================================================
        // LOẠI NHÂN VIÊN
        // =====================================================

        [StringLength(5)]
        public string? LoaiNhanVien { get; set; }

        // =====================================================
        // NGHỈ VIỆC
        // =====================================================

        public DateTime? NgayNghiViec { get; set; }

        [StringLength(255)]
        public string? LyDoNghiViec { get; set; }

        // =====================================================
        // LIÊN HỆ KHẨN CẤP
        // =====================================================

        [StringLength(100)]
        public string? HoTenNguoiLienHeKhanCap { get; set; }

        [StringLength(100)]
        public string? QuanHeLienHeKhanCap { get; set; }

        [StringLength(50)]
        public string? DienThoaiLienHeKhanCap { get; set; }

        [StringLength(255)]
        public string? DiaChiLienHeKhanCap { get; set; }

        // =====================================================
        // GHI CHÚ
        // =====================================================

        public string? GhiChu { get; set; }

        // =====================================================
        // NGÀY TẠO
        // =====================================================

        public DateTime? NGAYTAO { get; set; }

        [NotMapped]
        public string Ten
        {
            get
            {
                if (string.IsNullOrWhiteSpace(HoTen))
                    return "";

                var arr = HoTen.Trim().Split(' ');

                return arr[arr.Length - 1];
            }
        }
        [NotMapped]
        public int? Stt { get; set; } = 1;
        // =====================================================
        // CHILD TABLES
        // =====================================================

        public virtual ICollection<NguoiPhuThuoc>? NguoiPhuThuocs { get; set; }

        public virtual ICollection<QuaTrinhLuong>? QuaTrinhLuongs { get; set; }

        public virtual ICollection<QuaTrinhPhuCap>? QuaTrinhPhuCaps { get; set; }

       // public virtual ICollection<HopDongLaoDong>? HopDongLaoDongs { get; set; }
    }
}