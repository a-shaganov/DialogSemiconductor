using CommonDictionary.Helpers;
using System;

namespace CommonData.Attributes
{
    /// <summary>
    /// Дополнительный аттрибут для перечесления который указывает
    /// количество слотов
    /// </summary>
    public sealed class SizedSlotAttribute : Attribute
    {
        /// <summary>
        /// Количество линий
        /// </summary>
        public UInt16 Rows
        { get; private set; }

        /// <summary>
        /// Количество столбцов
        /// </summary>
        public UInt16 Columns
        { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rows">Количество линий</param>
        /// <param name="columns">Количество столбцов</param>
        public SizedSlotAttribute(UInt16 rows, UInt16 columns)
        {
            ArgumentHelper.NotSupported(() => rows <= 0, "Количество линий не может быть равна нулю");
            ArgumentHelper.NotSupported(() => columns <= 0, "Количество столбцов не может быть равна нулю");

            Rows = rows;
            Columns = columns;
        }
    }
}