using System;
using System.Collections.ObjectModel;

namespace CommonData.Slots
{
    /// <summary>
    /// Класс отвечающий за линию в контроллере
    /// </summary>
    public class MicroSlotRow : AbstractInfo
    {
        #region Properties
        /// <summary>
        /// Слоты указанные в линии
        /// </summary>
        public ObservableCollection<SlotInfo> Slots
        { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        public MicroSlotRow()
        {
            Slots = new ObservableCollection<SlotInfo>();
        }

        /// <summary>
        /// Конструтор
        /// </summary>
        /// <param name="columns">Количество столбцов</param>
        public MicroSlotRow(UInt16 columns) : this()
        {
            for (UInt16 idx = 0; idx < columns; idx++)
                Slots.Add(new SlotInfo());
        }
        #endregion

        #region Methods
        /// <summary>
        /// Метод клонирования объекта
        /// </summary>
        /// <returns>Копия объекта</returns>
        public override AbstractInfo Clone()
        {
            MicroSlotRow row = new MicroSlotRow();
            foreach (SlotInfo info in Slots)
                row.Slots.Add(info.Clone() as SlotInfo);
            return row;
        }
        #endregion
    }
}