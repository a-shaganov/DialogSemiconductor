using CommonDictionary.Helpers;
using System;
using System.Collections.ObjectModel;

namespace CommonData.Slots
{
    /// <summary>
    /// Слоты для микроконтроллера
    /// </summary>
    public class MicroSlots : AbstractInfo
    {
        #region Properties
        /// <summary>
        /// Количество линий
        /// </summary>
        public UInt16 RowCount
        { get; private set; }

        /// <summary>
        /// Количество слотов в линии
        /// </summary>
        public UInt16 ColumnCount
        { get; private set; }

        /// <summary>
        /// Слоты указанные в линии
        /// </summary>
        public ObservableCollection<MicroSlotRow> Rows
        { get; private set; }

        /// <summary>
        /// Присутствуют выбранные слоты
        /// </summary>
        public Boolean HasSelectedSlots
        {
            get
            {
                Boolean res = false;
                foreach (MicroSlotRow row in Rows)
                {
                    foreach (SlotInfo slot in row.Slots)
                        res = res || slot.IsSelected;
                }

                return res;
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        public MicroSlots()
        {
            Rows = new ObservableCollection<MicroSlotRow>();
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rows">Количество линий</param>
        /// <param name="columns">Количество столбцов</param>
        public MicroSlots(UInt16 rows, UInt16 columns) : this()
        {
            ArgumentHelper.NotSupported(() => rows <= 0, "rows must be > 0");
            ArgumentHelper.NotSupported(() => columns <= 0, "columns must be > 0");

            RowCount = rows;
            ColumnCount = columns;

            for (UInt16 idx = 0; idx < rows; idx++)
                Rows.Add(new MicroSlotRow(columns));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Метод клонирования объекта
        /// </summary>
        /// <returns>Копия объекта</returns>
        public override AbstractInfo Clone()
        {
            MicroSlots slot = new MicroSlots();
            foreach (MicroSlotRow info in Rows)
                slot.Rows.Add(info.Clone() as MicroSlotRow);

            return slot;
        }

        /// <summary>
        /// Получить индекс слота
        /// </summary>
        /// <param name="slot">Слот, индекс которого ищется</param>
        /// <returns></returns>
        private Tuple<Int32, Int32> GetIndexOfSlot(SlotInfo slot)
        {
            if (slot == null)
                return new Tuple<Int32, Int32>(-1, -1);

            Int32 rowIndex = 0;
            foreach (MicroSlotRow row in Rows)
            {
                Int32 index = row.Slots.IndexOf(slot);
                if (index > -1)
                    return new Tuple<Int32, Int32>(rowIndex, index);

                rowIndex++;
            }

            return new Tuple<Int32, Int32>(-1, -1);
        }

        /// <summary>
        /// Подготовить слоты к выбору имее первый и последний индексы
        /// </summary>
        /// <param name="firstIndex"></param>
        /// <param name="lastIndex"></param>
        public void SelectSlotsOfIndexses(SlotInfo first, SlotInfo last)
        {
            if ((first == null) || (last == null))
                return;

            Tuple<Int32, Int32> firstSlot = GetIndexOfSlot(first);
            Tuple<Int32, Int32> lastSlot = GetIndexOfSlot(last);

            if ((firstSlot.Item2 < 0) || (firstSlot.Item2 < 0))
                return;

            Int32 firstRow = firstSlot.Item1;
            Int32 lastRow = lastSlot.Item1;
            if (firstSlot.Item1 > lastSlot.Item1)
            {
                firstRow = lastSlot.Item1;
                lastRow = firstSlot.Item1;
            }

            Int32 firstIndex = firstSlot.Item2;
            Int32 lastIndex = lastSlot.Item2;
            if (firstSlot.Item2 > lastSlot.Item2)
            {
                firstIndex = lastSlot.Item2;
                lastIndex = firstSlot.Item2;
            }

            for (Int32 row = 0; row < Rows.Count; row++)
            {
                for (Int32 idx = 0; idx < Rows[row].Slots.Count; idx++)
                    Rows[row].Slots[idx].IsPrepared = (row >= firstRow) && (row <= lastRow) && (idx >= firstIndex) && (idx <= lastIndex);
            }
        }

        /// <summary>
        /// Выбрать подготовленные слоты
        /// </summary>
        public void SelectPreparedSlots()
        {
            foreach (MicroSlotRow row in Rows)
            {
                foreach (SlotInfo slot in row.Slots)
                {
                    if (!slot.IsPrepared)
                        continue;

                    slot.IsPrepared = false;
                    slot.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// Произвести действия с выбором всех слотов
        /// </summary>
        /// <param name="select">Флаг выбора слотов</param>
        public void SelectAllSlots(Boolean select)
        {
            foreach (MicroSlotRow row in Rows)
            {
                foreach (SlotInfo slot in row.Slots)
                {
                    slot.IsPrepared = false;
                    slot.IsSelected = select;
                }
            }
        }
        #endregion
    }
}