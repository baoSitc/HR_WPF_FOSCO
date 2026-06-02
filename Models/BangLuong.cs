using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR_WPF_FOSCO.Models
{
    [Table("BangLuong")]
    public class BangLuong
    {
        [Key]
        public int ID { get; set; }
        public int Thang { get; set; }
        public int Nam { get; set; }
        [StringLength(20)]
        public string? SOGB { get; set; } = null;
        // =====================================================
        // FOREIGN KEY ĐƠN VỊ
        // =====================================================

        public int? ID_DonVi { get; set; }
        [ForeignKey(nameof(ID_DonVi))]
        public virtual DonVi? DonVi { get; set; }      
        public DateTime NgayTao { get; set; }= DateTime.Now;
        public DateTime? NgayIn { get; set; } 
        public string? GhiChu { get; set; }
        public int TrangThai { get; set; }= 0;
    }
}
