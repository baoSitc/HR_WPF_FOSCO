using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR_WPF_FOSCO.Models
{
    public class HopDongDinhKem
    {
        [Key]
        public int ID_File { get; set; }

        public int ID_HopDong { get; set; }

        public string TenFile { get; set; }
        public string LoaiFile { get; set; }

        public string DuongDanFile { get; set; }
        public string NguoiTao { get; set; }
        public string GhiChu { get; set; }

        public DateTime? NgayTao { get; set; }

        [ForeignKey("ID_HopDong")]
        public HopDongDichVu HopDongDichVu { get; set; }
    }
}
