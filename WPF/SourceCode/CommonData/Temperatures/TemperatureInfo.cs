using CommonData.Properties;
using CommonData.Slots;
using CommonDictionary.Notifies;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommonData.Temperatures
{
    /// <summary>
    /// Класс содержащий настройку температуры и результатов по слотам
    /// </summary>
    public sealed class TemperatureInfo : PropertyChangedNotify
    {
        #region Properties
        /// <summary>
        /// Слоты
        /// </summary>
        public MicroSlots Slots
        { get; private set; }

        /// <summary>
        /// Температурв
        /// </summary>
        public UInt16 Temperature
        { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        public TemperatureInfo(UInt16 temperature)
        {
            Temperature = temperature;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Установить слот, желательно это делать до тестирования
        /// </summary>
        /// <param name="slots">Слоты</param>
        public void SetSlots(MicroSlots slots)
        {
            Slots = slots;
            RaisePropertyChanged("Slots");
        }

        /// <summary>
        /// Генерация рандомных результатов для тестирования отображения
        /// </summary>
        public Task<Boolean> GenerateRandomResults()
        {
            return Task<Boolean>.Factory.StartNew(() =>
            {
                Random random = new Random();
                foreach (MicroSlotRow row in Slots.Rows)
                {
                    foreach (SlotInfo slot in row.Slots)
                    {
                        if (slot.IsSelected)
                        {
                            Thread.Sleep(100);
                            Int32 num = random.Next(0, 5);
                            slot.CheckEnd = true;
                            slot.HasError = num > 2;
                            slot.ErrorDescription = Resources.Msg_Norma;
                            if (num > 2)
                                slot.ErrorDescription = String.Format(Resources.Msg_Error, num);
                        }
                    }
                }

                return true;
            });
        }
        #endregion
    }
}