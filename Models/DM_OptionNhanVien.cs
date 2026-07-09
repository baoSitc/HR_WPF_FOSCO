using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HR_WPF_FOSCO.Models
{
    [Table("DM_OptionNhanVien")]
    public class DM_OptionNhanVien
    {
        [Key]
      public int ID { get; set; }
       public string? BaoHiemDacBiet { get; set; }
     public   decimal DVBHXH { get; set; } = 0;
        public decimal DVBHYT { get; set; } = 0;
        public decimal DVBHTN { get; set; } = 0;
        public decimal NVBHXH { get; set; } = 0;
        public decimal NVBHYT { get; set; }= 0;
        public  decimal NVBHTN { get; set; } = 0;
      
    }
}
