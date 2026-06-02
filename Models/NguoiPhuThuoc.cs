using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_WPF_FOSCO.Models
{
    [Table("NguoiPhuThuoc")]
    public class NguoiPhuThuoc
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        [Key]
        public int ID { get; set; }

        // =====================================================
        // FOREIGN KEY NHÂN SỰ
        // =====================================================

        [StringLength(20)]
        public string? MaNhanSu { get; set; }

        [ForeignKey(nameof(MaNhanSu))]
        public virtual NhanSu? NhanSu { get; set; }

        // =====================================================
        // FOREIGN KEY ĐƠN VỊ
        // =====================================================

        public int? ID_DonVi { get; set; }

        [ForeignKey(nameof(ID_DonVi))]
        public virtual DonVi? DonVi { get; set; }

        // =====================================================
        // THÔNG TIN NGƯỜI PHỤ THUỘC
        // =====================================================

        [StringLength(150)]
        public string? HoTenNguoiPhuThuoc { get; set; }

        public DateTime? NgaySinh { get; set; }

        [StringLength(50)]
        public string? QuanHe { get; set; }

        [StringLength(20)]
        public string? SoCMND { get; set; }

        [StringLength(30)]
        public string? MaSoThue { get; set; }

        // =====================================================
        // THỜI GIAN GIẢM TRỪ
        // =====================================================

        public DateTime? TuNgay { get; set; }

        public DateTime? DenNgay { get; set; }

        // =====================================================
        // GIẤY TỜ
        // =====================================================

        [StringLength(50)]
        public string? LoaiGiayTo { get; set; }

        // =====================================================
        // GIẢM TRỪ GIA CẢNH
        // =====================================================

        public double? GiamTru { get; set; }
        public String? DangSuDung { get; set; } = "Đang sử dụng";
        public String? HoTenNhanSu { get; set; }
        public String? MaSoThueNhanSu { get; set; }
    }
}