using CommonDictionary.Helpers;

namespace DialogSemiconductor.Behaviors
{
    /// <summary>
    /// Класс представления данных механизма Drag&Drop
    /// </summary>
    public sealed class DragAndDropData
    {
        /// <summary>
        /// Обьект приемник
        /// </summary>
        public object DestinationObject
        { get; private set; }

        /// <summary>
        /// Данные обьекта передатчика
        /// </summary>
        public object SourceData
        { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sdata"> Данные обьекта передатчика </param>
        /// <param name="dobj"> Обьект приемник </param>
        public DragAndDropData(object sdata, object dobj)
        {
            ArgumentHelper.Null(sdata, "sdata");

            DestinationObject = dobj;
            SourceData = sdata;
        }
    }
}
