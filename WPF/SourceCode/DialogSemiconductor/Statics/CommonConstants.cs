using System;

namespace DialogSemiconductor.Statics
{
    /// <summary>
    /// Класс содержащий общие константы
    /// </summary>
    public static class CommonConstants
    {
        /// <summary>
        /// Формат строки для последовательной генерации
        /// </summary>
        public const String FRMT_PROPERTY_VARIABLE = "{0}:{1}";

        /// <summary>
        /// Текстовая константа неустановленого значения
        /// </summary>
        public const String STR_NULL = "null";

        /// <summary>
        /// Текстовая константа температуры
        /// </summary>
        public const String STR_TEMPERATURE = "temperature";

        /// <summary>
        /// Текстовая константа типа слота
        /// </summary>
        public const String STR_TYPESLOT = "typeslot";

        /// <summary>
        /// Название файла с сериализированными данными светильников
        /// </summary>
        public const String SERIALIZED_FILENAME = "information.dat";
    }
}
