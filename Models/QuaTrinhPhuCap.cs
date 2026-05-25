using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_WPF_FOSCO.Models
{
    [Table("QuaTrinhPhuCap")]
    public class QuaTrinhPhuCap
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
        // THÔNG TIN CHUNG
        // =====================================================

        [StringLength(5)]
        public string? TienTe { get; set; }

        public DateTime? NgayTao { get; set; }

        public DateTime? HieuLucTuNgay { get; set; }

        public DateTime? HieuLucDenNgay { get; set; }

        [StringLength(5)]
        public string? LoaiLuong { get; set; }

        // =====================================================
        // PHỤ CẤP - THU NHẬP
        // =====================================================

        public double? TienThuong { get; set; }

        public double? TienPhuCap { get; set; }

        public double? TienAn { get; set; }

        public double? TienDienThoai { get; set; }

        public double? TienCongTac { get; set; }

        public double? TienTrangPhuc { get; set; }

        public double? TienNha { get; set; }

        public bool? HD_Nha { get; set; }

        public double? TienKhac_Thue { get; set; }

        public double? TienKhac_KhongThue { get; set; }

        public double? TienNgoaiGio_Thue { get; set; }

        public double? TienNgoaiGio_KhongThue { get; set; }

        // =====================================================
        // BẢO HIỂM
        // =====================================================

        public double? PhuCapTinhBaoHiem { get; set; }

        // =====================================================
        // TRỢ CẤP
        // =====================================================

        public double? TroCapThoiViec { get; set; }

        // =====================================================
        // TRẠNG THÁI
        // =====================================================

        public bool? DangSuDung { get; set; }

        // =====================================================
        // GHI CHÚ
        // =====================================================

        [StringLength(250)]
        public string? GhiChu { get; set; }
    }
}