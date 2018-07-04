using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace AppResource.Themes.Converters
{
    /// <summary>
    /// string转ImageBrush
    /// </summary>
    public class IconSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                System.Windows.Media.ImageBrush ib = new System.Windows.Media.ImageBrush();
                if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
                {
                    ib.ImageSource = new BitmapImage(new Uri(@"C:\我的资源\Resource\Image\Icon\" + value.ToString() + ".png", UriKind.Absolute));
                    return ib;
                }
                string str = AppDomain.CurrentDomain.BaseDirectory + @"Image\Icon\" + value.ToString() + ".png";
                ib.ImageSource = new BitmapImage(new Uri(str, UriKind.Absolute));
                return ib;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
