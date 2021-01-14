using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace dmand
{
    [ValueConversion( typeof( object ), typeof( string ) )]
    public class TypeConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return value.GetType().Name;
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion( typeof( long ), typeof( long ) )]
    public class LengthConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is long )
            {
                var length = (long) value;
                return Math.Ceiling( (double) length / 1024 );
            }

            // Shouldn't happen, empty string is OK if it does
            return "";
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion( typeof( string ), typeof( string ) )]
    public class FileTypeConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is string )
            {
                return FileTypes.GetFileTypeDescription( value as string );
            }

            // Shouldn't happen, empty string is OK if it does
            return "";
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion( typeof( string ), typeof( BitmapSource ) )]
    public class IconConverter : IValueConverter
    {
        object IValueConverter.Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is string )
            {
                var icon = FileIcons.GetSmallIcon( value as string );

                return Imaging.CreateBitmapSourceFromHBitmap(
                        icon.ToBitmap().GetHbitmap(),
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions() );
            }

            return Binding.DoNothing;
        }

        object IValueConverter.ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
