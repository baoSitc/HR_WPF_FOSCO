using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_WPF_FOSCO.Models
{
    [Table("QuaTrinhPhuCap")]
    public class QuaTrinhPhuCap:INotifyPropertyChanged
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

        public decimal? TienThuong { get; set; }

        private decimal? _tienPhuCap;

        public decimal? TienPhuCap
        {
            get => _tienPhuCap;

            set
            {

                _tienPhuCap = value;              
                OnPropertyChanged(nameof(TienPhuCap));

                OnPropertyChanged(nameof(TienKhac_Thue));
            }
        }

        private decimal? _tienAn;

        public decimal? TienAn
        {
            get => _tienAn;

            set
            {
                _tienAn = value;               
                OnPropertyChanged(nameof(TienAn));
                OnPropertyChanged(nameof(TienKhac_Thue));
            }
        }
        private decimal? _tienDienThoai;

        public decimal? TienDienThoai
        {
            get => _tienDienThoai;

            set
            {
                _tienDienThoai = value;
                OnPropertyChanged(nameof(TienDienThoai));
                OnPropertyChanged(nameof(TienKhac_Thue));
            }
        }
        private decimal? _tienCongTac;
        public decimal? TienCongTac { get => _tienCongTac;
            set
            {
                _tienCongTac = value;
                OnPropertyChanged(nameof(TienCongTac));
                OnPropertyChanged(nameof(TienKhac_Thue));
            }
        }
        private decimal? _tienTrangPhuc;
        public decimal? TienTrangPhuc { get => _tienTrangPhuc;
            set
            {
                _tienTrangPhuc   = value;
                OnPropertyChanged(nameof(TienTrangPhuc));
                OnPropertyChanged(nameof(TienKhac_Thue));
            }
        }
        private decimal? _tienNha;
        public decimal? TienNha { get => _tienNha;
            set
            {
                _tienNha  = value;
                OnPropertyChanged(nameof(TienNha));
                OnPropertyChanged(nameof(TienKhac_Thue));
            }
        }

        public bool? HD_Nha { get; set; }

        private decimal? _tienKhac_Thue;

        public decimal? TienKhac_Thue
        {
            get
            {
                _tienKhac_Thue = (TienPhuCap ?? 0)
                    - (TienAn ?? 0)
                    - (TienDienThoai ?? 0)
                    - (TienCongTac ?? 0)
                    - (TienTrangPhuc ?? 0)
                    - (TienNha ?? 0)
                     - (TienKhac_KhongThue ?? 0);
                return _tienKhac_Thue;
                   
            }
            
        }
        private decimal? _tienKhac_KhongThue;
        public decimal? TienKhac_KhongThue { get => _tienKhac_KhongThue;
            set {
                _tienKhac_KhongThue = value;
                OnPropertyChanged(nameof(TienKhac_KhongThue));
                OnPropertyChanged(nameof(TienKhac_Thue));
            } }

        public decimal? TienNgoaiGio_Thue { get; set; }

        public decimal? TienNgoaiGio_KhongThue { get; set; }

        // =====================================================
        // BẢO HIỂM
        // =====================================================

        public decimal? PhuCapTinhBaoHiem { get; set; }

        // =====================================================
        // TRỢ CẤP
        // =====================================================

        public decimal? TroCapThoiViec { get; set; }

        // =====================================================
        // TRẠNG THÁI
        // =====================================================

        public bool? DangSuDung { get; set; }

        // =====================================================
        // GHI CHÚ
        // =====================================================

        [StringLength(250)]
        public string? GhiChu { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}