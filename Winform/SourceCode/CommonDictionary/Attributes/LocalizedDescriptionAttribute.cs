using CommonDictionary.Helpers;
using System;
using System.ComponentModel;

namespace CommonDictionary.Attributes
{
    /// <summary>
    /// Атрибут поддержки локализированного описания
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        /// <summary>
        /// Описание
        /// </summary>
        public override String Description
        { get { return DescriptionValue; } }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="resourceClassType"> Тип класса ресурса </param>
        /// <param name="resourceName"> Название ресурса </param>
        public LocalizedDescriptionAttribute(Type resourceClassType, String resourceName) : base(resourceName)
        {
            DescriptionValue = ResourceHelper.GetResourceValue<String>(resourceClassType, resourceName);
        }
    }
}
