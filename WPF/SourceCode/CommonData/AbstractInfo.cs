using CommonDictionary.Notifies;

namespace CommonData
{
    /// <summary>
    /// Абстрактный класс для слотов, линий со слотами и микроконтроллера
    /// </summary>
    public abstract class AbstractInfo : PropertyChangedNotify
    {
        /// <summary>
        /// Метод клонирования объекта
        /// </summary>
        /// <returns>Копия объекта</returns>
        public abstract AbstractInfo Clone();
    }
}
