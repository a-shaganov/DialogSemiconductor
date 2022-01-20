using CommonData.Slots;
using CommonDictionary.Helpers;
using System;

namespace DialogSemiconductorWF.EventArguments
{
    /// <summary>
    /// Класс содержащий аргумент с измененным слотом
    /// </summary>
    public class ChangedMicroSlotsArg : EventArgs
    {
        #region Properties
        /// <summary>
        /// Выбранные слоты
        /// </summary>
        public MicroSlots SelectedSlots
        { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="slots">Слоты</param>
        public ChangedMicroSlotsArg(MicroSlots slots)
        {
            SelectedSlots = slots;
        }
        #endregion
    }
}