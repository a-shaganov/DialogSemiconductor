using System;
using System.Globalization;
using System.Windows.Data;

namespace DialogSemiconductor.Views.Converters
{
    /// <summary>
    /// Конвертер, предоставляющий коллекцию значений типа перечисления, заданного параметром конвертера
    /// </summary>
    public sealed class EnumValuesEnumerator : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                Type ptype = parameter as Type;
                if (ptype == null)
                    ptype = parameter.GetType();
                if (ptype.IsEnum)
                    return Enum.GetValues(ptype);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
