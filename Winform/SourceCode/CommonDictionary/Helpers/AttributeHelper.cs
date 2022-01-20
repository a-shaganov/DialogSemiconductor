using System;
using System.Linq;
using System.Reflection;

namespace CommonDictionary.Helpers
{
    /// <summary>
    /// Вспомогательный класс, предоставляющий методы работы с атрибутами
    /// </summary>
    public static class AttributeHelper
    {
        /// <summary>
        /// Получить атрибут, установленный для элемента перечисления
        /// </summary>
        /// <typeparam name="TAttribute"> Тип запрашиваемого атрибута </typeparam>
        /// <param name="enumValue"> Элемент перечисления </param>
        /// <returns> Атрибут. NULL - для элемента перечисления не установлен атрибут указанного типа </returns>
        public static TAttribute GetAttributeFromEnum<TAttribute>(object enumValue) where TAttribute : Attribute
        {
            ArgumentHelper.Null(enumValue, "enumValue");

            FieldInfo finfo = enumValue.GetType().GetField(enumValue.ToString());
            if (finfo != null)
                return (TAttribute)Attribute.GetCustomAttribute(finfo, typeof(TAttribute));
            return null;
        }

        /// <summary>
        /// Получить набор однотипых атрибутов, установленных для элемента перечисления
        /// </summary>
        /// <typeparam name="TAttribute"> Тип запрашиваемого атрибута </typeparam>
        /// <param name="enumValue"> Элемент перечисления </param>
        /// <returns> Набор атрибутов </returns>
        public static TAttribute[] GetAttributesFromEnum<TAttribute>(object enumValue) where TAttribute : Attribute
        {
            ArgumentHelper.Null(enumValue, "enumValue");

            FieldInfo finfo = enumValue.GetType().GetField(enumValue.ToString());
            if (finfo != null)
                return Attribute.GetCustomAttributes(finfo, typeof(TAttribute)).Cast<TAttribute>().ToArray();
            return new TAttribute[] { };
        }
    }
}