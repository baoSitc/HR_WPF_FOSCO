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
        //chọn quá trình phụ cấp
        private QuaTrinhPhuCap _selectedQuaTrinhPhuCap;
        public QuaTrinhPhuCap SelectedQuaTrinhPhuCap
        {
            get => _selectedQuaTrinhPhuCap;
            set
            {
                _selectedQuaTrinhPhuCap = value;
                OnPropertyChanged(nameof(SelectedQuaTrinhPhuCap));
            }
        }


        //người phụ thuộc
        public ObservableCollection<NguoiPhuThuoc> NguoiPhuThuocs { get; set; }
            = new ObservableCollection<NguoiPhuThuoc>();
        private NguoiPhuThuoc _selectedNguoiPhuThuoc;
        public NguoiPhuThuoc SelectedNguoiPhuThuoc
        {
            get => _selectedNguoiPhuThuoc;
            set
            {
                _selectedNguoiPhuThuoc = value;
                OnPropertyChanged(nameof(SelectedNguoiPhuThuoc));
            }
        }

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
                LoadQuaTrinhPhuCap();
                LoadNguoiPhuThuoc();
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
                            .AsNoTracking()
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
            OnPropertyChanged(nameof(QuaTrinhLuongs));
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
                                   SelectedNhanSu.MaNhanSu
                                   && x.ID_DonVi == SelectedNhanSu.ID_DonVi)
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
            OnPropertyChanged(nameof(NguoiPhuThuocs));
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
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa quá trình phụ cấp này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                if (SelectedQuaTrinhLuong != null)
                {
                    QuaTrinhLuongs.Remove(SelectedQuaTrinhLuong);
                }
        }
        public void SaveQuaTrinhLuong()
        {
            if (SelectedQuaTrinhLuong == null)
                return;

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
                            .FirstOrDefault(x => x.MaNhanSu == maNhanSu && x.ID_DonVi == idDonvi);

                        if (nhanSu == null)
                        {
                            errors.Add($"Dòng {row.RowNumber()}: không tồn tại nhân sự {maNhanSu}");
                            continue;
                        }
                        //kiểm tra xem đã tồn tại quá trình lương nào có cùng mã nhân sự, đơn vị và từ ngày chưa, nếu có thì cập nhật lại, nếu không thì thêm mới
                        var existing = db.QuaTrinhLuongs
                        .FirstOrDefault(x =>
                        x.MaNhanSu == maNhanSu
                        && x.ID_DonVi == idDonvi
                        && x.TuNgay == tuNgay);
                        if (existing != null)
                        {


                            existing.TienTe = tienTe;

                            existing.LuongThucTe =(decimal?) luongThucTe;

                            existing.LuongDongBHXH = (decimal?) luongBHXH;

                            existing.DenNgay = denNgay;

                            existing.GhiChu = ghiChu;

                            existing.DangSuDung = true;
                            db.QuaTrinhLuongs.Update(existing);
                        }
                        else
                        // THÊM MỚI
                        {


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

                                LuongThucTe = (decimal?) luongThucTe,

                                LuongDongBHXH = (decimal?) luongBHXH,

                                GhiChu = ghiChu,

                                DangSuDung = true,

                                NgayTao = DateTime.Now
                            };

                            db.QuaTrinhLuongs.Add(newLuong);

                            success++;
                        }
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
        //=====================================================
        // QUÁ TRÌNH PHỤ CẤP
        //=====================================================
        public void AddQuaTrinhPhuCap()
        {
            SelectedQuaTrinhPhuCap = new QuaTrinhPhuCap
            {
                HieuLucTuNgay = DateTime.Today,
                HieuLucDenNgay = DateTime.Today,
                MaNhanSu = SelectedNhanSu?.MaNhanSu,
                ID_DonVi = SelectedNhanSu?.ID_DonVi,
                DangSuDung = true,
                NgayTao = DateTime.Now,
                TienTe = "D"

            };
            QuaTrinhPhuCaps.Add(SelectedQuaTrinhPhuCap);
            OnPropertyChanged(nameof(SelectedQuaTrinhPhuCap));

        }
        public void DeleteQuaTrinhPhuCap()
        {
            //thống báo người dùng có chắc chắn muốn xóa không, nếu có thì mới xóa
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa quá trình phụ cấp này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (SelectedQuaTrinhPhuCap != null)
                {
                    QuaTrinhPhuCaps.Remove(SelectedQuaTrinhPhuCap);
                    OnPropertyChanged(nameof(SelectedQuaTrinhPhuCap));
                }
            }
        }
        public void SaveQuaTrinhPhuCap()
        {
            if (SelectedQuaTrinhPhuCap == null)
                return;

            using var db = new AppDbContext();
            //kiểm tra xem có trường hợp  thêm mới nào không, nếu có thì thêm vào db
            bool them = false;
            foreach (var item in QuaTrinhPhuCaps)
            {
                if (item.ID == 0)
                {
                    them = true;
                    break;
                }
            }
            if (them)
            {
                foreach (var item in QuaTrinhPhuCaps)
                {
                    if (item.ID == 0)
                        db.QuaTrinhPhuCaps.Add(item);
                    else
                    {
                        item.DangSuDung = false;
                        db.QuaTrinhPhuCaps.Update(item);
                    }
                }
            }
            else
            {
                foreach (var item in QuaTrinhPhuCaps)
                {
                    if (SelectedQuaTrinhPhuCap.DangSuDung == true)
                        if (item.ID != SelectedQuaTrinhPhuCap.ID)
                            item.DangSuDung = false;
                }
            }

            db.SaveChanges();
            LoadQuaTrinhPhuCap();
        }
        public void ImportQuaTrinhPhuCap()
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
                        DateTime tuNgay =
                           row.Cell(4).GetDateTime();

                        DateTime? denNgay = null;

                        if (!row.Cell(5).IsEmpty())
                        {
                            denNgay = row.Cell(5).GetDateTime();
                        }

                        double tienphucap =
                            row.Cell(6).GetDouble();

                        double tienan =
                            row.Cell(7).GetDouble();

                        double tiendienthoai =
                           row.Cell(8).GetDouble();
                        double tiencongtac =
                           row.Cell(9).GetDouble();

                        double tientrangphuc =
                            row.Cell(10).GetDouble();
                        double tienkhac_khongthue =
                           row.Cell(11).GetDouble();

                        double tienkhac_thue =
                           row.Cell(12).GetDouble();



                        string ghiChu =
                            row.Cell(13).GetString();

                        //-----------------------------------
                        // VALIDATE
                        //-----------------------------------

                        if (string.IsNullOrWhiteSpace(maNhanSu))
                        {
                            errors.Add($"Dòng {row.RowNumber()}: thiếu mã nhân sự");
                            continue;
                        }

                        var nhanSu = db.NhanSus
                            .FirstOrDefault(x => x.MaNhanSu == maNhanSu && x.ID_DonVi == idDonvi);

                        if (nhanSu == null)
                        {
                            errors.Add($"Dòng {row.RowNumber()}: không tồn tại nhân sự {maNhanSu}");
                            continue;
                        }
                        //kiểm tra xem đã tồn tại quá trình lương nào có cùng mã nhân sự, đơn vị và từ ngày chưa, nếu có thì cập nhật lại, nếu không thì thêm mới
                        var existing = db.QuaTrinhPhuCaps
                        .FirstOrDefault(x =>
                        x.MaNhanSu == maNhanSu
                        && x.ID_DonVi == idDonvi
                        && x.HieuLucTuNgay == tuNgay);
                        if (existing != null)
                        {


                            existing.TienTe = tienTe;

                            existing.TienPhuCap = (decimal?) tienphucap;

                            existing.TienAn = (decimal?) tienan;

                            existing.TienDienThoai = (decimal?) tiendienthoai;

                            existing.TienCongTac = (decimal?) tiencongtac;

                            existing.TienTrangPhuc = (decimal?) tientrangphuc;

                            //existing.TienKhac_Thue = tienkhac_thue;

                            existing.TienKhac_KhongThue = (decimal?)    tienkhac_khongthue;

                            existing.HieuLucDenNgay = denNgay;

                            existing.GhiChu = ghiChu;

                            existing.DangSuDung = true;
                            db.QuaTrinhPhuCaps.Update(existing);
                        }
                        else
                        // THÊM MỚI
                        {


                            //-----------------------------------
                            // TẮT DÒNG ĐANG SỬ DỤNG
                            //-----------------------------------


                            var currentLuong = db.QuaTrinhPhuCaps
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

                            var newphucap = new QuaTrinhPhuCap
                            {
                                MaNhanSu = maNhanSu,

                                ID_DonVi = nhanSu.ID_DonVi,

                                HieuLucTuNgay = tuNgay,

                                HieuLucDenNgay = denNgay,

                                TienTe = tienTe,



                                TienPhuCap = (decimal?) tienphucap,

                                TienAn = (decimal?) tienan,

                                TienDienThoai = (decimal?)          tiendienthoai,

                                TienCongTac = (decimal?) tiencongtac,

                                TienTrangPhuc = (decimal?) tientrangphuc,


                                TienKhac_KhongThue = (decimal?)    tienkhac_khongthue,

                                GhiChu = ghiChu,

                                DangSuDung = true,

                                NgayTao = DateTime.Now
                            };

                            db.QuaTrinhPhuCaps.Add(newphucap);

                            success++;
                        }
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
                    "Import quá trình phụ cấp",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                //-----------------------------------
                // RELOAD
                //-----------------------------------

                LoadQuaTrinhPhuCap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Lỗi import",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        //=====================================================
        // NGƯỜI PHỤ THUỘC
        //=====================================================
        public void AddNguoiPhuThuoc()
        {
            SelectedNguoiPhuThuoc = new NguoiPhuThuoc
            {
                MaNhanSu = SelectedNhanSu?.MaNhanSu,
                ID_DonVi = SelectedNhanSu?.ID_DonVi,
                DangSuDung = "Đang sử dụng",
                TuNgay = DateTime.Today,
                DenNgay = DateTime.Today
            };
            NguoiPhuThuocs.Add(SelectedNguoiPhuThuoc);
            OnPropertyChanged(nameof(SelectedNguoiPhuThuoc));
        }

        public void DeleteNguoiPhuThuoc()
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa người phụ thuộc này không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                if (SelectedNguoiPhuThuoc != null)
                {
                    NguoiPhuThuocs.Remove(SelectedNguoiPhuThuoc);
                }
            OnPropertyChanged(nameof(SelectedNguoiPhuThuoc));
        }
        public void SaveNguoiPhuThuoc()
        {
            if (SelectedNguoiPhuThuoc == null)
                return;
            using var db = new AppDbContext();
            //kiểm tra xem có trường hợp  thêm mới nào không, nếu có thì thêm vào db
            bool them = false;
            foreach (var item in NguoiPhuThuocs)
            {
                if (item.ID == 0)
                {
                    them = true;
                    break;
                }
            }
            if (them)
            {
                foreach (var item in NguoiPhuThuocs)
                {
                    if (item.ID == 0)
                        db.NguoiPhuThuocs.Add(item);
                    else
                    {
                        item.DangSuDung = "Đang sử dụng";
                        db.NguoiPhuThuocs.Update(item);
                    }
                }
            }
            else
            {
                foreach (var item in NguoiPhuThuocs)
                {
                    if (SelectedNguoiPhuThuoc.DangSuDung == "Đang sử dụng")
                        if (item.ID != SelectedNguoiPhuThuoc.ID)
                            item.DangSuDung = "Không sử dụng";

                }
            }
            db.SaveChanges();
            LoadNguoiPhuThuoc();
        }

        public void ImportNguoiPhuThuoc()
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

                // Bỏ 4 dòng  tiêu đề
                var rows = ws.RowsUsed().Skip(4);

                int success = 0;

                List<string> errors = new();

                foreach (var row in rows)
                {
                    try
                    {
                        //-----------------------------------
                        // ĐỌC DỮ LIỆU
                        //-----------------------------------

                        string maNhanSu = row.Cell(2).GetString().Trim();
                        int idDonvi = int.Parse(row.Cell(3).GetString().Trim());

                        //string loaiLuong = row.Cell(3).GetString().Trim();

                        string hotenNguoiNopThue = row.Cell(4).GetString().Trim();
                        string mstNguoiNopThue = row.Cell(5).GetString().Trim();
                        string hotenNguoiPhuThuoc = row.Cell(6).GetString().Trim();
                        DateTime ngaysinhNguoiPhuThuoc = row.Cell(7).GetDateTime();
                        string mstNguoiPhuThuoc = row.Cell(8).GetString().Trim();
                        string loaiGiayTo = row.Cell(9).GetString().Trim();
                        string soGiayTo = row.Cell(10).GetString().Trim();
                        string quanheVoiNguoiNopThue = row.Cell(11).GetString().Trim();

                        DateTime tuthang =
                           row.Cell(12).GetDateTime();

                        DateTime? denthang = null;

                        if (!row.Cell(13).IsEmpty())
                        {
                            denthang = row.Cell(13).GetDateTime();
                        }

                       

                        //-----------------------------------
                        // VALIDATE
                        //-----------------------------------

                        if (string.IsNullOrWhiteSpace(maNhanSu))
                        {
                            errors.Add($"Dòng {row.RowNumber()}: thiếu mã nhân sự");
                            continue;
                        }

                        var nhanSu = db.NhanSus
                            .FirstOrDefault(x => x.MaNhanSu == maNhanSu && x.ID_DonVi == idDonvi);

                        if (nhanSu == null)
                        {
                            errors.Add($"Dòng {row.RowNumber()}: không tồn tại nhân sự {maNhanSu}");
                            continue;
                        }
                        //kiểm tra xem đã tồn tại quá trình lương nào có cùng mã nhân sự, đơn vị và từ ngày chưa, nếu có thì cập nhật lại, nếu không thì thêm mới
                        var existing = db.NguoiPhuThuocs
                        .FirstOrDefault(x =>
                        x.MaNhanSu == maNhanSu
                        && x.ID_DonVi == idDonvi
                        && x.TuNgay == tuthang);
                        if (existing != null)
                        {


                            existing.MaNhanSu = maNhanSu;

                            existing.ID_DonVi = idDonvi ;

                            existing.HoTenNhanSu = hotenNguoiNopThue;

                            existing.MaSoThueNhanSu = mstNguoiNopThue;

                            existing.HoTenNguoiPhuThuoc = hotenNguoiPhuThuoc;

                            existing.NgaySinh = ngaysinhNguoiPhuThuoc;

                            //existing.TienKhac_Thue = tienkhac_thue;

                            existing.MaSoThueNhanSu = mstNguoiPhuThuoc;

                            existing.LoaiGiayTo = loaiGiayTo;

                            existing.SoCMND = soGiayTo;
                            existing.QuanHe = quanheVoiNguoiNopThue;
                            existing.TuNgay = tuthang;
                            existing.DenNgay = denthang;
                            
                            existing.DangSuDung = "Đang sử dụng";
                            db.NguoiPhuThuocs.Update(existing);
                        }
                        else
                        // THÊM MỚI
                        {
                               

                            //-----------------------------------
                            // THÊM NGƯỜI PHỤ THUỘC MỚI
                            //-----------------------------------

                            var newphucap = new NguoiPhuThuoc
                            {
                                MaNhanSu = maNhanSu,

                              ID_DonVi = idDonvi,

                               HoTenNhanSu = hotenNguoiNopThue,

                               MaSoThueNhanSu = mstNguoiNopThue,

                               HoTenNguoiPhuThuoc = hotenNguoiPhuThuoc,
                               

                               NgaySinh = ngaysinhNguoiPhuThuoc,   
                                LoaiGiayTo = loaiGiayTo,
                                SoCMND = soGiayTo,
                                MaSoThue = mstNguoiPhuThuoc,
                                QuanHe = quanheVoiNguoiNopThue,
                               TuNgay = tuthang,
                              DenNgay = denthang,
                               DangSuDung = "Đang sử dụng",
                            };

                            db.NguoiPhuThuocs.Add(newphucap);

                            success++;
                        }
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
                    "Import NGƯỜI PHỤ THUỘC",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                //-----------------------------------
                // RELOAD
                //-----------------------------------

                LoadQuaTrinhPhuCap();
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
