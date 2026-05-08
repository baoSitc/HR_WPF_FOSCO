using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace HR_WPF_FOSCO.Converters
{
    public class BooleanTrangThaiConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool trangThai)
            {
                return trangThai ? "Hoạt động" : "Không hoạt động";
            }
            return "Không xác định";
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string trangThai)
            {
                return trangThai == "Hoạt động";
            }
            return false;
        }
    }
}
