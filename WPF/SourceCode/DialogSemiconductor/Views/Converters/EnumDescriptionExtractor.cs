using CommonDictionary.Attributes;
using CommonDictionary.Helpers;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace DialogSemiconductor.Views.Converters
{
    /// <summary>
    /// Конвертер, представляющий значения перечисления описанием, заданным для него
    /// атрибутом Description или LocalizedDescription
    /// </summary>
    public sealed class EnumDescriptionExtractor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String result = String.Empty;
            if (value != null)
            {
                LocalizedDescriptionAttribute ldescAttr = AttributeHelper.GetAttributeFromEnum<LocalizedDescriptionAttribute>(value);
                if (ldescAttr != null)
                    result = ldescAttr.Description;
                else
                {
                    DescriptionAttribute descAttr = AttributeHelper.GetAttributeFromEnum<DescriptionAttribute>(value);
                    if (descAttr != null)
                        result = descAttr.Description;
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
