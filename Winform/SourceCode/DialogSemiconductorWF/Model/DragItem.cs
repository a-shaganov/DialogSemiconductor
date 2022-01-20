using System.Windows.Forms;

namespace DialogSemiconductorWF.Model
{
    /// <summary>
    /// Класс содержащий объект и его индекс, а так же родителя
    /// </summary>
    public sealed class DragItem
    {
        #region Fields
        /// <summary>
        /// Родитель
        /// </summary>
        public ListBox Client;

        /// <summary>
        /// Индекс объекта
        /// </summary>
        public int Index;

        /// <summary>
        /// Объект
        /// </summary>
        public object Item;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="client">Родитель</param>
        /// <param name="index">Индекс объекта</param>
        /// <param name="item">Объект</param>
        public DragItem(ListBox client, int index, object item)
        {
            Client = client;
            Index = index;
            Item = item;
        }
        #endregion
    }
}
