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
		public double THUONG { get; set; } = 0;
        public double LCV { get; set; } = 0;
        public double DLCV { get; set; } = 0;
        public double LTLD { get; set; } = 0;
		[StringLength(5)]
		public string? DVT { get; set; } = "D";
		public int? SOPT { get; set; } = 0;
        public double LuongCoBan { get; set; } = 0;
        public double TIENPC { get; set; } = 0;
		[StringLength(5)]
        public string? DVTPC { get; set; } = "D";
        public double AN { get; set; } = 0;
        public double DT { get; set; } = 0;
        public double CT { get; set; } = 0;
        public double TP { get; set; } = 0;
        public double KHAC { get; set; } = 0;
        public double KHAC_KTHUE { get; set; } = 0;
        public double PC_TIENNHA { get; set; } = 0;
        public double PC_TTKHNHA { get; set; } = 0;
        public double THUE_NHA { get; set; } = 0;
        public double PC_TBH { get; set; } = 0;
        public double PC_TTHUE { get; set; } = 0;
        public double TONGLUONG { get; set; } = 0;
        public double LGTNCN { get; set; } = 0;
        public double LGBHXH { get; set; } = 0;
        public double LGBHTN { get; set; } = 0;
        public double DV_BHXH { get; set;} = 0;
        public double DV_BHYT { get; set; } = 0;
        public double DV_BHTN { get; set; } = 0;
        public double NV_BHXH { get; set; } = 0;
        public double NV_BHYT { get; set; } = 0;
        public double NV_BHTN { get; set; } = 0;
        public double TC_NV { get; set; } = 0;
        public double Giam_TBT { get; set; } = 0;
        public double ST_NGPT { get;set; } = 0;
        public double TNCN { get;set; } = 0;
        public double TTN { get; set; } = 0;
        public double TIEN_CD { get; set; } = 0;
        public double DNVHG { get; set; } = 0;
        public double TIEN_DVP { get; set; } = 0;
        public string? GHICHU { get; set; } = null;
        public DateTime ? NgayCapNhat {  get; set; } = null;       
        public double TGLG { get; set; } = 0;
        public double TGBHXH { get; set; } = 0;
        public double TONGTIEN_DN { get; set; } = 0;
        [StringLength(2)]
        public string? NGUOISD { get; set; } = null;
        public double CACKHOANGIAMTRU { get; set; } = 0;     

 
    }
}
