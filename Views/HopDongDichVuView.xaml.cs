using HR_WPF_FOSCO.Models;
using HR_WPF_FOSCO.Services;
using HR_WPF_FOSCO.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace HR_WPF_FOSCO.Views
{
    /// <summary>
    /// Interaction logic for HopDongDichVuView.xaml
    /// </summary>
    public partial class HopDongDichVuView : UserControl
    {
        HopDongDichVuViewModel vm = new HopDongDichVuViewModel();
        public HopDongDichVuView()
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            vm.AddHopDongDichVu();
        }
        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            vm.UpdateHopDongDichVu();
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            vm.DeleteHopDongDichVu();
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            vm.ClearForm();
            //xoa trang vung pdfviewer
            PdfViewer.Navigate("about:blank");
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            vm.SearchHopDongDichVu(txtSearch.Text);
        }
        private void BtnUploadPDF_Click(object sender,
                                 RoutedEventArgs e)
        {
            if (vm.SelectedHopDongDichVu == null
                || vm.SelectedHopDongDichVu.ID_HopDong == 0)
            {
                MessageBox.Show("Vui lòng lưu hợp đồng trước.");

                return;
            }

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "All Files|*.*";

            if (dlg.ShowDialog() == true)
            {
                string folder =
                    Path.Combine(
                        AppDomain.CurrentDomain.BaseDirectory,
                        "Contracts");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                string fileName =
                    Path.GetFileName(dlg.FileName);

                string destPath =
                    Path.Combine(folder, fileName);

                File.Copy(dlg.FileName,
                          destPath,
                          true);

                using var db = new AppDbContext();

                HopDongDinhKem file =
                    new HopDongDinhKem();

                file.ID_HopDong =
                    vm.SelectedHopDongDichVu.ID_HopDong;

                file.TenFile = fileName;

                file.DuongDanFile = destPath;
                file.LoaiFile = Path.GetExtension(fileName);


                db.HopDongDinhKems.Add(file);


                db.SaveChanges();

                MessageBox.Show("Upload thành công.");

                PdfViewer.Navigate(destPath);
            }
        }
        private void BtnOpenPDF_Click(object sender,
                              RoutedEventArgs e)
        {
            if (vm.SelectedHopDongDichVu == null)
                return;

            using var db = new AppDbContext();

            var file = db.HopDongDinhKems
                         .Where(x =>
                             x.ID_HopDong ==
                             vm.SelectedHopDongDichVu.ID_HopDong)
                         .OrderByDescending(x => x.ID_File)

                         .FirstOrDefault();

            if (file == null)
            {
                MessageBox.Show("Không có file.");

                return;
            }

            if (File.Exists(file.DuongDanFile))
            {
                PdfViewer.Navigate(file.DuongDanFile);
            }
            else
            {
                MessageBox.Show("File không tồn tại.");
            }
        }
        private void LoadFiles()
        {
            using var db = new AppDbContext();

            var files = db.HopDongDinhKems
                .Where(x => x.ID_HopDong ==
                            vm.SelectedHopDongDichVu.ID_HopDong)
                .ToList();

            cbFiles.ItemsSource = files;
        }
        private void dgHopDong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadFiles();
        }
        private void cbFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFiles.SelectedItem is HopDongDinhKem selectedFile)
            {
                if (File.Exists(selectedFile.DuongDanFile))
                {
                    PdfViewer.Navigate(selectedFile.DuongDanFile);
                }
                else
                {
                    MessageBox.Show("File không tồn tại.");
                }
            }
        }
        private void BtnDeleteFile_Click(object sender, RoutedEventArgs e)
        {
            if (cbFiles.SelectedItem is HopDongDinhKem selectedFile)
            {
                using var db = new AppDbContext();
                var file = db.HopDongDinhKems.Find(selectedFile.ID_File);
                if (file != null)
                {
                    db.HopDongDinhKems.Remove(file);
                    db.SaveChanges();
                    if (File.Exists(file.DuongDanFile))
                    {
                        File.Delete(file.DuongDanFile);
                    }
                    MessageBox.Show("Xóa file thành công.");
                    LoadFiles();
                    PdfViewer.Navigate("about:blank");
                }
            }
        }
        private void BtnDownloadFile_Click(object sender, RoutedEventArgs e)
        {
            if (cbFiles.SelectedItem is HopDongDinhKem selectedFile)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = selectedFile.TenFile;
                dlg.Filter = "All Files|*.*";
                if (dlg.ShowDialog() == true)
                {
                    File.Copy(selectedFile.DuongDanFile, dlg.FileName, true);
                    MessageBox.Show("Download thành công.");
                }
            }
        }
    }
}
