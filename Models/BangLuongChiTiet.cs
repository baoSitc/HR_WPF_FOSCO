using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR_WPF_FOSCO.Models
{
    [Table("BangLuongChiTiet")]
    public class BangLuongChiTiet
    {
        [Key]
        public int ID { get; set; }
        // =====================================================
        // FOREIGN KEY Bảng Lương
        // =====================================================
        public int? BangLuongID { get; set; }
        [ForeignKey(nameof(BangLuongID))]
        public virtual BangLuong? BangLuong { get; set; } = null;
        // =====================================================
        // FOREIGN KEY NHÂN SỰ
        // =====================================================
        [StringLength(20)]
        public string? MaNhanSu { get; set; }
        [ForeignKey(nameof(MaNhanSu))]
        public virtual NhanSu? NhanSu { get; set; }		
		[StringLength(50)]
        public string? HoTen { get; set; }
		[StringLength(5)]
        public string? LOAINV { get; set; }
		[ StringLength(5)]
		public string? L_HDLD { get; set; }
        [StringLength(5)]
        public string? LOAILG { get; set; }
		public decimal? THUONG { get; set; } = 0;
        public decimal? LCV { get; set; } = 0;
        public decimal? DLCV { get; set; } = 0;
        public decimal? LTLD { get; set; } = 0;
		[StringLength(5)]
		public string? DVT { get; set; } = "D";
		public int? SOPT { get; set; } = 0;
        public decimal? LuongCoBan { get; set; } = 0;
        public decimal? TIENPC { get; set; } = 0;
		[StringLength(5)]
        public string? DVTPC { get; set; } = "D";
        public decimal? AN { get; set; } = 0;
        public decimal? DT { get; set; } = 0;
        public decimal? CT { get; set; } = 0;
        public decimal? TP { get; set; } = 0;
        public decimal? KHAC { get; set; } = 0;
        public decimal? KHAC_KTHUE { get; set; } = 0;
        public decimal? PC_TIENNHA { get; set; } = 0;
        public decimal? PC_TTKHNHA { get; set; } = 0;
        public decimal? THUE_NHA { get; set; } = 0;
        public decimal? PC_TBH { get; set; } = 0;
        public decimal? PC_TTHUE { get; set; } = 0;
        public decimal? TONGLUONG { get; set; } = 0;
        public decimal? LGTNCN { get; set; } = 0;
        public decimal? LGBHXH { get; set; } = 0;
        public decimal? LGBHTN { get; set; } = 0;
        public decimal? DV_BHXH { get; set;} = 0;
        public decimal? DV_BHYT { get; set; } = 0;
        public decimal? DV_BHTN { get; set; } = 0;
        public decimal? TC_DV
        {
            get 
            { return (DV_BHXH ?? 0) + (DV_BHYT ?? 0) + (DV_BHTN ?? 0) ;
            }
        }
        public decimal? NV_BHXH { get; set; } = 0;
        public decimal? NV_BHYT { get; set; } = 0;
        public decimal? NV_BHTN { get; set; } = 0;
        public decimal? TC_NV {
            // TỔNG CÁC KHOẢN NHÂN VIÊN PHẢI TRẢ
            get
            {
                return (NV_BHXH ?? 0) + (NV_BHYT ?? 0) + (NV_BHTN ?? 0);
            }

                 } 
        public decimal? Giam_TBT { get; set; } = 0;
        public decimal? ST_NGPT { get;set; } = 0;
        public decimal? TNCN { get;set; } = 0;
        public decimal? TTN { get; set; } = 0;
        public decimal? TIEN_CD { get; set; } = 0;
        public decimal? DNVHG { get; set; } = 0;
        public decimal? TIEN_DVP { get; set; } = 0;
        public string? GHICHU { get; set; } = null;
        public DateTime ? NgayCapNhat {  get; set; } = null;       
        public decimal? TGLG { get; set; } = 0;
        public decimal? TGBHXH { get; set; } = 0;
        public decimal? TONGTIEN_DN { get; set; } = 0;
        [StringLength(2)]
        public string? NGUOISD { get; set; } = null;
        public decimal? CACKHOANGIAMTRU { get; set; } = 0;     

 
    }
}
