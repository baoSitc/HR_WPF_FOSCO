using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_WPF_FOSCO.Models
{
    [Table("QuaTrinhLuong")]
    public class QuaTrinhLuong
    {
        // =====================================================
        // PRIMARY KEY
        // =====================================================

        [Key]
        public int ID { get; set; }

        // =====================================================
        // FOREIGN KEY NHÂN SỰ
        // =====================================================

        [StringLength(10)]
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
        // THỜI GIAN
        // =====================================================

        public DateTime? TuNgay { get; set; }

        public DateTime? DenNgay { get; set; }

        // =====================================================
        // THÔNG TIN LƯƠNG
        // =====================================================

        [StringLength(20)]
        public string? LoaiLuong { get; set; }

        [StringLength(5)]
        public string? TienTe { get; set; }

        public double? LuongThucTe { get; set; }

        public double? LuongDongBHXH { get; set; }

        // =====================================================
        // GHI CHÚ
        // =====================================================

        [StringLength(250)]
        public string? GhiChu { get; set; }

        // =====================================================
        // SYSTEM
        // =====================================================

        public DateTime? NgayTao { get; set; }
    }
}