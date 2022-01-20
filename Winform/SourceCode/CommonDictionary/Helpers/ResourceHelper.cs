using System;
using System.Reflection;

namespace CommonDictionary.Helpers
{
    /// <summary>
    /// Вспомогательный класс, предоставляющий методы работы с ресурсами
    /// </summary>
    public static class ResourceHelper
    {
        /// <summary>
        /// Получить значение ресурса
        /// </summary>
        /// <param name="resourceClassType"> Тип класса ресурса </param>
        /// <param name="resourceName"> Название ресурса </param>
        /// <returns></returns>
        public static TValue GetResourceValue<TValue>(Type resourceClassType, String resourceName)
        {
            ArgumentHelper.Null(resourceClassType, "resourceClassType");
            ArgumentHelper.NotSupported(() => String.IsNullOrEmpty(resourceName), "Не определено название ресурса");

            PropertyInfo pinfo = resourceClassType.GetProperty(resourceName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            ArgumentHelper.NotSupported(() => pinfo == null, "Указано название несуществующего ресурса");
            return (TValue)pinfo.GetValue(null, null);
        }
    }
}