using ClosedXML.Excel;
using HR_WPF_FOSCO.Models;
using HR_WPF_FOSCO.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using Windows.UI;

namespace HR_WPF_FOSCO.ViewModels
{
    public class NhanSuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //quá trình lương
        public ObservableCollection<QuaTrinhLuong> QuaTrinhLuongs { get; set; }
            = new ObservableCollection<QuaTrinhLuong>();
        //cHỌN QUÁ TRÌNH LƯƠNG
        private QuaTrinhLuong _selectedQuaTrinhLuong;

        public QuaTrinhLuong SelectedQuaTrinhLuong
        {
            get => _selectedQuaTrinhLuong;
            set
            {
                _selectedQuaTrinhLuong = value;
                OnPropertyChanged(nameof(SelectedQuaTrinhLuong));


            }
        }
        //quá trình phụ cấp
        public ObservableCollection<QuaTrinhPhuCap> QuaTrinhPhuCaps { get; set; }
            = new ObservableCollection<QuaTrinhPhuCap>();
        //người phụ thuộc
        public ObservableCollection<NguoiPhuThuoc> NguoiPhuThuocs { get; set; }
            = new ObservableCollection<NguoiPhuThuoc>();
        //khai báo db context
        private readonly AppDbContext _context = new AppDbContext();
        private string _trangThaiFilter = "Tất cả";

        public string TrangThaiFilter
        {
            get => _trangThaiFilter;
            set
            {
                _trangThaiFilter = value;
                OnPropertyChanged(nameof(TrangThaiFilter));
                // reset combobox đơn vị
                if (_trangThaiFilter == "Tất cả nhân viên")
                {
                    SelectedDonViID = null;
                    TuKhoa = string.Empty;
                }

                SearchNhanSu();
            }
        }
        public List<string> TrangThaiList { get; set; }
            = new List<string>
        {
                 "Tất cả nhân viên",
            "Tất cả",
            "Đang làm việc",
            "Nghỉ việc"
        };

        public List<string> GenderList { get; set; }
                = new()
            {
                "Nam",
                "Nữ"
            };
        public string GioiTinhText
        {
            get
            {
                return SelectedNhanSu?.GioiTinh == true
                    ? "Nam"
                    : "Nữ";
            }
            set
            {
                if (SelectedNhanSu != null)
                {
                    SelectedNhanSu.GioiTinh =
                        value == "Nam";
                }

                OnPropertyChanged(nameof(GioiTinhText));
            }
        }
        // =====================================================
        // DANH SÁCH NHÂN SỰ
        // =====================================================

        public ObservableCollection<NhanSu> NhanSus { get; set; }
            = new ObservableCollection<NhanSu>();

        // =====================================================
        // DANH SÁCH ĐƠN VỊ
        // =====================================================

        public ObservableCollection<DonVi> DonVis { get; set; }
            = new ObservableCollection<DonVi>();
        // =====================================================
        // NHÂN SỰ ĐANG CHỌN
        // =====================================================

        private NhanSu _selectedNhanSu = new();

        public NhanSu SelectedNhanSu
        {
            get => _selectedNhanSu;
            set
            {
                _selectedNhanSu = value;
                OnPropertyChanged(nameof(SelectedNhanSu));
                OnPropertyChanged(nameof(GioiTinhText));
                LoadQuaTrinhLuong();
            }
        }
        // =====================================================
        // TỪ KHÓA TÌM KIẾM
        // =====================================================

        private string _tuKhoa = "";

        public string TuKhoa
        {
            get => _tuKhoa;
            set
            {
                _tuKhoa = value;
                OnPropertyChanged(nameof(TuKhoa));

                SearchNhanSu();
            }
        }
        // =====================================================
        // ĐƠN VỊ ĐANG CHỌN
        // =====================================================

        private int? _selectedDonViID;

        public int? SelectedDonViID
        {
            get => _selectedDonViID;
            set
            {
                _selectedDonViID = value;
                OnPropertyChanged(nameof(SelectedDonViID));

                SearchNhanSu();
            }
        }
        // =====================================================
        // COLLECTION VIEW
        // =====================================================

        public ICollectionView NhanSuView { get; set; }

        // =====================================================
        // CONSTRUCTOR
        // =====================================================
        public NhanSuViewModel()
        {
            LoadDonVis();

            LoadNhanSus();

            NhanSuView = CollectionViewSource.GetDefaultView(NhanSus);
        }
        // =====================================================
        // LOAD ĐƠN VỊ
        // =====================================================

        public void LoadDonVis()
        {
            using var db = new AppDbContext();

            var list = db.DonVis
                         .OrderBy(x => x.TenDonVi)
                         .ToList();

            DonVis.Clear();

            foreach (var item in list)
            {
                DonVis.Add(item);
            }
        }
        // =====================================================
        // LOAD NHÂN SỰ
        // =====================================================

        public void LoadNhanSus()
        {
            using var db = new AppDbContext();

            var list = db.NhanSus
                         .Include(x => x.DonVi)
                         .OrderBy(x => x.MaNhanSu)
                         .ToList();

            NhanSus.Clear();

            foreach (var item in list)
            {
                NhanSus.Add(item);
            }
        }
        // =====================================================
        // SEARCH
        // =====================================================

        public void SearchNhanSu()
        {
            using var db = new AppDbContext();
            var query = db.NhanSus
                                .Include(x => x.DonVi)
                                .AsQueryable();
            // SEARCH TEXT

            if (!string.IsNullOrWhiteSpace(TuKhoa))
            {
                query = query.Where(x =>

                    x.MaNhanSu.Contains(TuKhoa)

                    || x.HoTen.Contains(TuKhoa)

                    || x.CCCD.Contains(TuKhoa));
            }

            // FILTER ĐƠN VỊ

            if (SelectedDonViID != null && TrangThaiFilter != "Tất cả nhân viên")
            {
                query = query.Where(x =>
                    x.ID_DonVi == SelectedDonViID);
            }
            // FILTER TRẠNG THÁI
            if (TrangThaiFilter == "Tất cả nhân viên")
            {
                query = query.Where(x => x.TrangThai == true || x.TrangThai == false);
            }

            else if (TrangThaiFilter == "Tất cả")
            {
                query = query.Where(x => x.TrangThai == true || x.TrangThai == false);
            }
            else
                if (TrangThaiFilter == "Đang làm việc")
                {
                    query = query.Where(x => x.TrangThai == true);
                }
                else if (TrangThaiFilter == "Nghỉ việc")
                {
                    query = query.Where(x => x.TrangThai == false);
                }


            var list = query
                .AsEnumerable()
                        .OrderBy(x => x.Ten)
                        .ToList();

            NhanSus.Clear();
            int stt = 1;
            foreach (var item in list)
            {
                item.Stt = stt++;
                NhanSus.Add(item);
            }
        }
        // =====================================================
        // LOAD LƯƠNG
        // =====================================================

        public void LoadQuaTrinhLuong()
        {
            QuaTrinhLuongs.Clear();

            if (SelectedNhanSu == null)
                return;

            var list = _context.QuaTrinhLuongs
                               .Where(x =>
                                   x.MaNhanSu ==
                                   SelectedNhanSu.MaNhanSu
                                   && x.ID_DonVi == SelectedNhanSu.ID_DonVi)
                               .OrderByDescending(x => x.TuNgay)
                               .ToList();

            foreach (var item in list)
            {
                QuaTrinhLuongs.Add(item);
            }
        }

        // =====================================================
        // LOAD PHỤ CẤP
        // =====================================================

        public void LoadQuaTrinhPhuCap()
        {
            QuaTrinhPhuCaps.Clear();

            if (SelectedNhanSu == null)
                return;

            var list = _context.QuaTrinhPhuCaps
                               .Where(x =>
                                   x.MaNhanSu ==
                                   SelectedNhanSu.MaNhanSu)
                               .OrderByDescending(x =>
                                   x.HieuLucTuNgay)
                               .ToList();

            foreach (var item in list)
            {
                QuaTrinhPhuCaps.Add(item);
            }
        }

        // =====================================================
        // LOAD NGƯỜI PHỤ THUỘC
        // =====================================================

        public void LoadNguoiPhuThuoc()
        {
            NguoiPhuThuocs.Clear();

            if (SelectedNhanSu == null)
                return;

            var list = _context.NguoiPhuThuocs
                               .Where(x =>
                                   x.MaNhanSu ==
                                   SelectedNhanSu.MaNhanSu)
                               .ToList();

            foreach (var item in list)
            {
                NguoiPhuThuocs.Add(item);
            }
        }


        // =====================================================
        // CLEAR FORM
        // =====================================================

        public void ClearForm()
        {
            SelectedNhanSu = new NhanSu();
        }

        // =====================================================
        // ADD
        // =====================================================
        public void AddNhanSu()
        {
            using var db = new AppDbContext();

            db.NhanSus.Add(SelectedNhanSu);

            db.SaveChanges();

            LoadNhanSus();

            ClearForm();
        }
        // =====================================================
        // UPDATE
        // =====================================================

        public void UpdateNhanSu()
        {
            using var db = new AppDbContext();

            db.NhanSus.Update(SelectedNhanSu);

            db.SaveChanges();

            LoadNhanSus();
        }
        public void DeleteNhanSu()
        {
            using var db = new AppDbContext();

            db.NhanSus.Remove(SelectedNhanSu);

            db.SaveChanges();

            LoadNhanSus();

            ClearForm();
        }
        //=====================================================
        // QUÁ TRÌNH LƯƠNG
        //=====================================================
        public void AddQuaTrinhLuong()
        {
            SelectedQuaTrinhLuong = new QuaTrinhLuong
            {
                TuNgay = DateTime.Today,
                DenNgay = DateTime.Today,
                TienTe = "D",
                MaNhanSu = SelectedNhanSu?.MaNhanSu,
                ID_DonVi = SelectedNhanSu?.ID_DonVi,
                DangSuDung = true,
                NgayTao = DateTime.Now

            };
            QuaTrinhLuongs.Add(SelectedQuaTrinhLuong);
            OnPropertyChanged(nameof(SelectedQuaTrinhLuong));

        }
        public void DeleteQuaTrinhLuong()
        {
            if (SelectedQuaTrinhLuong != null)
            {
                QuaTrinhLuongs.Remove(SelectedQuaTrinhLuong);
            }
        }
        public void SaveQuaTrinhLuong()
        {
            using var db = new AppDbContext();
            //kiểm tra xem có trường hợp  thêm mới nào không, nếu có thì thêm vào db
            bool them = false;
            foreach (var item in QuaTrinhLuongs)
            {
                if (item.ID == 0)
                {
                    them = true;
                    break;
                }
            }
            if (them)
            {
                foreach (var item in QuaTrinhLuongs)
                {
                    if (item.ID == 0)
                        db.QuaTrinhLuongs.Add(item);
                    else
                    {
                        item.DangSuDung = false;
                        db.QuaTrinhLuongs.Update(item);
                    }
                }
            }
            else
            {
                foreach (var item in QuaTrinhLuongs)
                {
                    if (SelectedQuaTrinhLuong.DangSuDung == true)
                        if (item.ID != SelectedQuaTrinhLuong.ID)
                            item.DangSuDung = false;


                    db.QuaTrinhLuongs.Update(item);
                }
            }


            db.SaveChanges();
            LoadQuaTrinhLuong();
        }
        public void ImportQuaTrinhLuong()
        {
            try
            {
                OpenFileDialog open = new OpenFileDialog();

                open.Filter = "Excel File|*.xlsx;*.xls";

                if (open.ShowDialog() != true)
                    return;

                using var db = new AppDbContext();

                using var workbook = new XLWorkbook(open.FileName);

                var ws = workbook.Worksheet(1);

                // Bỏ dòng tiêu đề
                var rows = ws.RowsUsed().Skip(1);

                int success = 0;

                List<string> errors = new();

                foreach (var row in rows)
                {
                    try
                    {
                        //-----------------------------------
                        // ĐỌC DỮ LIỆU
                        //-----------------------------------

                        string maNhanSu = row.Cell(1).GetString().Trim();
                        int idDonvi = int.Parse(row.Cell(2).GetString().Trim());

                        //string loaiLuong = row.Cell(3).GetString().Trim();

                        string tienTe = row.Cell(3).GetString().Trim();

                        double luongThucTe =
                            row.Cell(4).GetDouble();

                        double luongBHXH =
                            row.Cell(5).GetDouble();

                        DateTime tuNgay =
                            row.Cell(6).GetDateTime();

                        DateTime? denNgay = null;

                        if (!row.Cell(7).IsEmpty())
                        {
                            denNgay = row.Cell(7).GetDateTime();
                        }

                        string ghiChu =
                            row.Cell(8).GetString();

                        //-----------------------------------
                        // VALIDATE
                        //-----------------------------------

                        if (string.IsNullOrWhiteSpace(maNhanSu))
                        {
                            errors.Add($"Dòng {row.RowNumber()}: thiếu mã nhân sự");
                            continue;
                        }

                        var nhanSu = db.NhanSus
                            .FirstOrDefault(x => x.MaNhanSu == maNhanSu && x.ID_DonVi==idDonvi);

                        if (nhanSu == null)
                        {
                            errors.Add($"Dòng {row.RowNumber()}: không tồn tại nhân sự {maNhanSu}");
                            continue;
                        }

                        //-----------------------------------
                        // TẮT DÒNG ĐANG SỬ DỤNG
                        //-----------------------------------

                        var currentLuong = db.QuaTrinhLuongs
                            .Where(x => x.MaNhanSu == maNhanSu
                                     && x.DangSuDung == true)
                            .ToList();

                        foreach (var item in currentLuong)
                        {
                            item.DangSuDung = false;
                        }

                        //-----------------------------------
                        // THÊM LƯƠNG MỚI
                        //-----------------------------------

                        var newLuong = new QuaTrinhLuong
                        {
                            MaNhanSu = maNhanSu,

                            ID_DonVi = nhanSu.ID_DonVi,

                            TuNgay = tuNgay,

                            DenNgay = denNgay,                          

                            TienTe = tienTe,

                            LuongThucTe = luongThucTe,

                            LuongDongBHXH = luongBHXH,

                            GhiChu = ghiChu,

                            DangSuDung = true,

                            NgayTao = DateTime.Now
                        };

                        db.QuaTrinhLuongs.Add(newLuong);

                        success++;
                    }
                    catch (Exception exRow)
                    {
                        errors.Add($"Dòng {row.RowNumber()}: {exRow.Message}");
                    }
                }

                //-----------------------------------
                // SAVE
                //-----------------------------------

                db.SaveChanges();

                //-----------------------------------
                // THÔNG BÁO
                //-----------------------------------

                string message =
                    $"Import thành công: {success} dòng";

                if (errors.Count > 0)
                {
                    message +=
                        $"\nLỗi: {errors.Count} dòng\n\n";

                    message += string.Join("\n", errors);
                }

                MessageBox.Show(message,
                    "Import quá trình lương",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                //-----------------------------------
                // RELOAD
                //-----------------------------------

                LoadQuaTrinhLuong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Lỗi import",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
