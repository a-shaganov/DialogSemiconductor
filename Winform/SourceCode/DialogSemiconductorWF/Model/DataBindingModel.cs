using CommonData.Attributes;
using CommonData.Enums;
using CommonData.Slots;
using CommonData.Temperatures;
using CommonDictionary.Attributes;
using CommonDictionary.Helpers;
using CommonDictionary.Notifies;
using DialogSemiconductorWF.EventArguments;
using DialogSemiconductorWF.Properties;
using DialogSemiconductorWF.Statics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DialogSemiconductorWF.Model
{
    /// <summary>
    /// Класс содержащий всю логику для работы с моделями
    /// </summary>
    public class DataBindingModel : PropertyChangedNotify
    {
        #region Fields
        /// <summary>
        /// Слоты которые используются для настройки
        /// </summary>
        public MicroSlots SlotsForSettings;
        #endregion

        #region Properties
        /// <summary>
        /// Типы слотов
        /// </summary>
        public BindingList<KeyValuePair<String, MicroSlotTypes>> SlotTypes
        { get; private set; }

        /// <summary>
        /// Список настроек температуры
        /// </summary>
        public BindingList<TemperatureInfo> Temperatures
        { get; private set; }

        /// <summary>
        /// Слоты
        /// </summary>
        public MicroSlots Slots
        { get; private set; }

        private Int32 _SelectedSlotIndex;
        /// <summary>
        /// 
        /// </summary>
        public Int32 SelectedSlotIndex
        {
            get { return _SelectedSlotIndex; }
            set
            {
                if ((_SelectedSlotIndex == value) || ExecuteCheckSlots)
                    return;

                _SelectedSlotIndex = value;
                RaisePropertyChanged("SelectedSlotIndex");

                Slots = null;
                if ((_SelectedSlotIndex > -1) && (_SelectedSlotIndex < SlotTypes.Count))
                {
                    SizedSlotAttribute size = AttributeHelper.GetAttributeFromEnum<SizedSlotAttribute>(SlotTypes[_SelectedSlotIndex].Value);
                    Slots = new MicroSlots(size.Rows, size.Columns);
                }

                RaisePropertyChanged("Slots");

                if (ChangedSlotsEventHandler != null)
                    ChangedSlotsEventHandler(this, new ChangedMicroSlotsArg(Slots));
            }
        }

        /// <summary>
        /// Выбранная температура
        /// </summary>
        public TemperatureInfo SelectedTemperature
        { get; private set; }

        private Int32 _SelectedTemperatureIndex;
        /// <summary>
        /// 
        /// </summary>
        public Int32 SelectedTemperatureIndex
        {
            get { return _SelectedTemperatureIndex; }
            set
            {
                _SelectedTemperatureIndex = value;
                RaisePropertyChanged("SelectedTemperatureIndex");

                SelectedTemperature = null;
                if ((_SelectedTemperatureIndex > -1) && (_SelectedTemperatureIndex < Temperatures.Count))
                    SelectedTemperature = Temperatures[_SelectedTemperatureIndex];

                RaisePropertyChanged("SelectedTemperature");

                if (ExecuteCheckSlots && (SelectedTemperature.Slots != null) && (ChangedSlotsEventHandler != null))
                {
                    Slots = SelectedTemperature.Slots;
                    ChangedSlotsEventHandler(this, new ChangedMicroSlotsArg(Slots));
                }
            }
        }

        private UInt16 _Temperature;
        /// <summary>
        /// Новая температура
        /// </summary>
        public UInt16 Temperature
        {
            get { return _Temperature; }
            set
            {
                _Temperature = value;
                RaisePropertyChanged("Temperature");
            }
        }

        public Boolean _ExecuteCheckSlots;
        /// <summary>
        /// Флаг выполнения проверки слотов
        /// </summary>
        public Boolean ExecuteCheckSlots
        {
            get { return _ExecuteCheckSlots; }
            set
            {
                _ExecuteCheckSlots = value;
                RaisePropertyChanged("ExecuteCheckSlots");
            }
        }

        public Boolean _InvisibleSettings;
        /// <summary>
        /// Флаг для видимости настроек
        /// </summary>
        public Boolean InvisibleSettings
        {
            get { return _InvisibleSettings; }
            set
            {
                _InvisibleSettings = value;
                RaisePropertyChanged("InvisibleSettings");
            }
        }

        public Boolean _ExecuteDontWorking;
        /// <summary>
        /// Флаг означающий что программа не выполняет проверку
        /// </summary>
        public Boolean ExecuteDontWorking
        {
            get { return _ExecuteDontWorking; }
            set
            {
                _ExecuteDontWorking = value;
                RaisePropertyChanged("ExecuteDontWorking");
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Событие оповещающее что слот был изменен
        /// </summary>
        public event EventHandler<ChangedMicroSlotsArg> ChangedSlotsEventHandler;

        /// <summary>
        /// Началось выполнение проверки
        /// </summary>
        public event EventHandler StartedOrEndExecuteCeck;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        public DataBindingModel()
        {
            Temperatures = new BindingList<TemperatureInfo>();

            SlotTypes = new BindingList<KeyValuePair<String, MicroSlotTypes>>();
            foreach (MicroSlotTypes type in Enum.GetValues(typeof(MicroSlotTypes)))
                SlotTypes.Add(new KeyValuePair<string, MicroSlotTypes>(AttributeHelper.GetAttributeFromEnum<LocalizedDescriptionAttribute>(type).Description, type));

            if (SlotTypes.Count > 0)
                SelectedSlotIndex = 1;

            if (Temperatures.Count > 0)
                SelectedTemperatureIndex = 0;

            Temperature = 120;
            ExecuteCheckSlots = false;
            InvisibleSettings = true;
            ExecuteDontWorking = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Метод удаления выбранной температуры
        /// </summary>
        public void DeleteSelectedTemperature()
        {
            if ((SelectedTemperatureIndex < 0) || (SelectedTemperature == null))
                return;

            if (Temperatures.Contains(SelectedTemperature))
                Temperatures.Remove(SelectedTemperature);

            SelectedTemperatureIndex = -1;
        }

        /// <summary>
        /// Додати температуру
        /// </summary>
        /// <param name="temperature">Температура</param>
        public void AddTemperature(UInt16 temperature)
        {
            ArgumentHelper.NotSupported(() => temperature <= 0, "Temperature to small");
            Temperatures.Add(new TemperatureInfo(temperature));
        }

        /// <summary>
        /// Выполнить проверку слотов
        /// </summary>
        public async void ExecuteCheckingSlots()
        {
            if (!ExecuteCheckSlots)
            {
                DialogResult answer = MessageBox.Show(Resources.MsgQuestion_AreYouSureexEcuteCheck,
                    Resources.MsqBox_Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer == DialogResult.No)
                    return;

                ExecuteDontWorking = false;
                ExecuteCheckSlots = true;
                SlotsForSettings = Slots;
                Slots = null;

                if (StartedOrEndExecuteCeck != null)
                    StartedOrEndExecuteCeck(this, new EventArgs());

                InvisibleSettings = !ExecuteCheckSlots;
                foreach (TemperatureInfo temperature in Temperatures)
                {
                    temperature.SetSlots(SlotsForSettings.Clone() as MicroSlots);
                    if (SelectedTemperatureIndex != Temperatures.IndexOf(temperature))
                        SelectedTemperatureIndex = Temperatures.IndexOf(temperature);

                    Boolean res = await temperature.GenerateRandomResults();
                }

                MessageBox.Show(Resources.MsgInformation_CheckDone,
                    Resources.MsqBox_Information, MessageBoxButtons.OK, MessageBoxIcon.Information);

                ExecuteDontWorking = true;
            }
            else
            {
                DialogResult answer = MessageBox.Show(Resources.MsgQuestion_GoToSettings,
                    Resources.MsqBox_Question, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer == DialogResult.No)
                    return;

                ExecuteCheckSlots = false;
                InvisibleSettings = !ExecuteCheckSlots;

                if (StartedOrEndExecuteCeck != null)
                    StartedOrEndExecuteCeck(this, new EventArgs());

                Slots = SlotsForSettings;
                if (ChangedSlotsEventHandler != null)
                    ChangedSlotsEventHandler(this, new ChangedMicroSlotsArg(Slots));
            }
        }

        /// <summary>
        /// Метод сохранения информации
        /// </summary>
        public void SaveInformation()
        {
            if (File.Exists(CommonConstants.SERIALIZED_FILENAME))
                File.Delete(CommonConstants.SERIALIZED_FILENAME);

            List<Object> settings = new List<Object>();
            settings.Add(String.Format(CommonConstants.FRMT_PROPERTY_VARIABLE, CommonConstants.STR_TYPESLOT,
                SlotTypes[_SelectedSlotIndex].Value.ToString()));

            if (Slots != null)
            {
                List<List<String>> rows = new List<List<String>>();
                foreach (MicroSlotRow row in Slots.Rows)
                {
                    List<String> setRow = new List<String>();
                    foreach (SlotInfo slot in row.Slots)
                        setRow.Add(slot.IsSelected.ToString());

                    rows.Add(setRow);
                }

                settings.Add(rows);
            }

            foreach (TemperatureInfo tmperature in Temperatures)
                settings.Add(String.Format(CommonConstants.FRMT_PROPERTY_VARIABLE, CommonConstants.STR_TEMPERATURE,
                    tmperature.Temperature));

            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(CommonConstants.SERIALIZED_FILENAME, FileMode.Create, FileAccess.Write, FileShare.None))
                binFormat.Serialize(fStream, settings);

        }

        /// <summary>
        /// Метод восстановления информации
        /// </summary>
        public void RestoreInformation()
        {
            List<Object> tmp = null;

            if (File.Exists(CommonConstants.SERIALIZED_FILENAME))
            {
                BinaryFormatter binFormat = new BinaryFormatter();
                using (Stream fStream = File.OpenRead(CommonConstants.SERIALIZED_FILENAME))
                    tmp = (List<Object>)binFormat.Deserialize(fStream);
            }

            if ((tmp != null) && (tmp.Count > 0))
            {
                foreach (Object obj in tmp)
                {
                    if (obj is String)
                    {
                        String setting = obj.ToString();
                        String[] data = setting.Split(':');
                        if (data.Length < 2)
                            continue;

                        if (setting.Contains(CommonConstants.STR_TYPESLOT))
                        {
                            MicroSlotTypes tmpType = MicroSlotTypes.Small;
                            if (Enum.TryParse<MicroSlotTypes>(data[1], out tmpType))
                                SelectedSlotIndex = SlotTypes.IndexOf(SlotTypes.FirstOrDefault(item => item.Value == tmpType));
                        }
                        else if (setting.Contains(CommonConstants.STR_TEMPERATURE))
                        {
                            UInt16 temperature = 0;
                            if (UInt16.TryParse(data[1], out temperature))
                                Temperatures.Add(new TemperatureInfo(temperature));
                        }
                    }
                    else if (obj is List<List<String>>)
                    {
                        List<List<String>> settings = obj as List<List<String>>;
                        if (Slots.Rows.Count != settings.Count)
                            continue;

                        Int32 indexRow = 0;
                        foreach (MicroSlotRow row in Slots.Rows)
                        {
                            Int32 index = 0;
                            foreach (SlotInfo slot in row.Slots)
                            {
                                Boolean res = false;
                                if (Boolean.TryParse(settings[indexRow][index], out res))
                                    slot.IsSelected = res;
                                index++;
                            }

                            indexRow++;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
