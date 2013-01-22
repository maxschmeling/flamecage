using System;
using System.Windows;
using System.Windows.Data;

namespace GrayHills.FlameCage.Client.Views.Converters
{
  public class HasValueVisibilityConverter : IValueConverter
  {
    #region IValueConverter Members

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value == null)
      {
        return Visibility.Visible;
      }

      var type = Type.GetTypeCode(value.GetType());

      switch (type)
      {
        case TypeCode.String:
          if (string.IsNullOrEmpty(value as string))
            return Visibility.Visible;
          break;
      }

      return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
