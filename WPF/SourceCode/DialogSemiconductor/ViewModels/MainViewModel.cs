using CommonData.Attributes;
using CommonData.Enums;
using CommonData.Slots;
using CommonData.Temperatures;
using CommonDictionary.Commands;
using CommonDictionary.Helpers;
using DialogSemiconductor.Behaviors;
using DialogSemiconductor.Properties;
using DialogSemiconductor.Settings;
using DialogSemiconductor.Statics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Input;

namespace DialogSemiconductor.ViewModels
{
    /// <summary>
    /// Основной класс представления модели программы
    /// </summary>
    public sealed class MainViewModel : AbstractViewModel
    {
        #region Commands
        /// <summary>
        /// Добавить температуру
        /// </summary>
        public ICommand AddThemperatureCommand
        { get; private set; }

        /// <summary>
        /// Удалить температуру
        /// </summary>
        public ICommand DeleteThemperatureCommand
        { get; private set; }

        /// <summary>
        /// Удалить температуру из меню
        /// </summary>
        public ICommand DeleteThemperatureMenuCommand
        { get; private set; }

        /// <summary>
        /// Передвинуть температуру вверх
        /// </summary>
        public ICommand MoveUpTemperatureCommand
        { get; private set; }

        /// <summary>
        /// Передвинуть температуру вниз
        /// </summary>
        public ICommand MoveDownTemperatureCommand
        { get; private set; }

        /// <summary>
        /// Выполнить программу
        /// </summary>
        public ICommand ExecuteProgrammCommand
        { get; private set; }

        /// <summary>
        /// Произвести выбор первого слота когда
        /// левая кнопка мышки нажата
        /// </summary>
        public ICommand MouseDownCommand
        { get; private set; }

        /// <summary>
        /// Произвести выбор последнего слота когда
        /// левая кнопка мышки поднята
        /// </summary>
        public ICommand MouseUpCommand
        { get; private set; }

        /// <summary>
        /// Производить выбор всех слотов по которым проводится мышка,
        /// но только если была нажата левая клавиша мышки
        /// </summary>
        public ICommand MouseMoveCommand
        { get; private set; }

        /// <summary>
        /// Выбрать все слоты
        /// </summary>
        public ICommand SelectAllSlotsCommand
        { get; private set; }

        /// <summary>
        /// Передвинуть температуру мышкой
        /// </summary>
        public ICommand CatchDroppingTemperatureCommand
        { get; private set; }

        /// <summary>
        /// Проверка текста в компоненту с температурой
        /// </summary>
        public ICommand CheckTextCommand
        { get; private set; }

        /// <summary>
        /// Закрытие программы
        /// </summary>
        public ICommand ClosingProgrammCommand
        { get; private set; }
        #endregion

        #region Fields
        /// <summary>
        /// Первый выбранный слот
        /// </summary>
        private SlotInfo FirstSelectedSlot;

        /// <summary>
        /// Первый выбранный слот
        /// </summary>
        private SlotInfo LastSelectedSlot;

        /// <summary>
        /// Слоты которые используются для настройки
        /// </summary>
        public MicroSlots SlotsForSettings;

        /// <summary>
        /// Флаг разрешения добавления температуры
        /// </summary>
        public Boolean AcceptToaAddTemperature;
        #endregion

        #region Properties
        private MicroSlotTypes _MicroSlots;
        /// <summary>
        /// Тип слотов
        /// </summary>
        public MicroSlotTypes MicroSlots
        {
            get { return _MicroSlots; }
            set
            {
                _MicroSlots = value;
                RaisePropertyChanged("MicroSlots");

                SizedSlotAttribute size = AttributeHelper.GetAttributeFromEnum<SizedSlotAttribute>(_MicroSlots);
                if (size != null)
                    GenerateMicroSlots(size.Rows, size.Columns);
            }
        }

        /// <summary>
        /// Слоты
        /// </summary>
        public MicroSlots Slots
        { get; private set; }

        /// <summary>
        /// Список настроек температур
        /// </summary>
        public ObservableCollection<TemperatureInfo> Temperatures
        { get; private set; }

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

        private TemperatureInfo _SelectedTemperature;
        /// <summary>
        /// Выбранная температура
        /// </summary>
        public TemperatureInfo SelectedTemperature
        {
            get { return _SelectedTemperature; }
            set
            {
                _SelectedTemperature = value;
                RaisePropertyChanged("SelectedTemperature");

                if (ExecuteCheckSlots)
                {
                    Slots = _SelectedTemperature.Slots;
                    RaisePropertyChanged("Slots");
                }
            }
        }

        private Boolean _ExecuteCheckSlots;
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

        private Boolean _ProgrammWorking;
        /// <summary>
        /// Программа выполняется
        /// </summary>
        public Boolean ProgrammWorking
        {
            get { return _ProgrammWorking; }
            set
            {
                _ProgrammWorking = value;
                RaisePropertyChanged("ProgrammWorking");
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        public MainViewModel()
        {
            MicroSlots = MicroSlotTypes.Small;
            ExecuteCheckSlots = false;
            AcceptToaAddTemperature = true;
            ProgrammWorking = false;
            Temperature = 100;

            Temperatures = new ObservableCollection<TemperatureInfo>();

            RestoreInformation();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Инициализация команд
        /// </summary>
        protected override void InitializeCommands()
        {
            MouseDownCommand = new RelayCommand(DoMouseDown, (obj) => (obj is SlotInfo) && !ExecuteCheckSlots);
            MouseMoveCommand = new RelayCommand(DoMouseMove, (obj) => (obj is SlotInfo) && !ExecuteCheckSlots);
            MouseUpCommand = new RelayCommand(DoMouseUp, (obj) =>  !ExecuteCheckSlots);
            SelectAllSlotsCommand = new RelayCommand(DoSelectAllSlots,
                (obj) => (Slots != null) && (obj != null) && !String.IsNullOrEmpty(obj.ToString()) && !ExecuteCheckSlots);
            AddThemperatureCommand = new RelayCommand(() => Temperatures.Add(new TemperatureInfo(Temperature)),
                () =>  !ExecuteCheckSlots && AcceptToaAddTemperature);
            DeleteThemperatureCommand = new RelayCommand(
                () => { Temperatures.Remove(SelectedTemperature); SelectedTemperature = null; },
                () => (SelectedTemperature != null) && Temperatures.Contains(SelectedTemperature) && !ExecuteCheckSlots);
            DeleteThemperatureMenuCommand = new RelayCommand(DoDeleteThemperatureMenu,
                (obj) => (obj != null) && (obj is TemperatureInfo) &&
                Temperatures.Contains(obj as TemperatureInfo) && !ExecuteCheckSlots);
            MoveUpTemperatureCommand = new RelayCommand(DoMoveUpTemperature,
                () => (SelectedTemperature != null) && Temperatures.Contains(SelectedTemperature) &&
                (Temperatures.IndexOf(SelectedTemperature) > 0) && !ExecuteCheckSlots);
            MoveDownTemperatureCommand = new RelayCommand(DoMoveDownTemperature,
                () => (SelectedTemperature != null) && Temperatures.Contains(SelectedTemperature) &&
                (Temperatures.IndexOf(SelectedTemperature) < (Temperatures.Count - 1)) && !ExecuteCheckSlots);
            CatchDroppingTemperatureCommand = new RelayCommand(DoCatchDroppingTemperature, CanDoCatchDroppingTemperature);
            ExecuteProgrammCommand = new RelayCommand(ExecuteProgramm,
                () => (Slots != null) && Slots.HasSelectedSlots && (Temperatures.Count > 0) && !ProgrammWorking);
            CheckTextCommand = new RelayCommand(DoCheckText, (obj) => (obj != null) && (obj is Boolean));
            ClosingProgrammCommand = new RelayCommand(() => { }, () => CanDoClosingProgramm() == true);
        }

        /// <summary>
        /// Проверка на ввод текста с возвратом флага который означает текст длля температуры установлен или нет
        /// </summary>
        /// <param name="obj">Флаг</param>
        private void DoCheckText(object obj)
        {
            AcceptToaAddTemperature = Boolean.Parse(obj.ToString());
        }

        /// <summary>
        /// Удалить температуру из меню
        /// </summary>
        /// <param name="obj">Температура</param>
        private void DoDeleteThemperatureMenu(object obj)
        {
            Temperatures.Remove(obj as TemperatureInfo);
        }

        /// <summary>
        /// Выполнение программы
        /// </summary>
        private async void ExecuteProgramm()
        {
            if (!ExecuteCheckSlots)
            {
                MessageBoxResult answer = MessageBox.Show(Resources.MsgQuestion_AreYouSureexEcuteCheck,
                    Resources.MsqBox_Question, MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.No)
                    return;

                ProgrammWorking = true;
                ExecuteCheckSlots = true;
                SlotsForSettings = Slots;
                Slots = null;

                foreach (TemperatureInfo temperature in Temperatures)
                {
                    temperature.SetSlots(SlotsForSettings.Clone() as MicroSlots);
                    SelectedTemperature = temperature;
                    Boolean res = await temperature.GenerateRandomResults();
                }

                MessageBox.Show(Resources.MsgInformation_CheckDone,
                    Resources.MsqBox_Information, MessageBoxButton.OK, MessageBoxImage.Information);

                ProgrammWorking = false;
            }
            else
            {
                MessageBoxResult answer = MessageBox.Show(Resources.MsgQuestion_GoToSettings,
                    Resources.MsqBox_Question, MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer == MessageBoxResult.No)
                    return;

                ExecuteCheckSlots = false;
                Slots = SlotsForSettings;
                RaisePropertyChanged("Slots");
            }

            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Генерация слотов для послкдующей настройки
        /// </summary>
        /// <param name="rows">Линий</param>
        /// <param name="columns">Столбцов</param>
        private void GenerateMicroSlots(UInt16 rows, UInt16 columns)
        {
            Slots = new MicroSlots(rows, columns);
            RaisePropertyChanged("Slots");
        }

        /// <summary>
        /// Выполнить выбор первого слота при нажатии на левую клавишу мышки
        /// </summary>
        /// <param name="obj">Слот в виде объекта</param>
        private void DoMouseDown(object obj)
        {
            if (ExecuteCheckSlots)
                return;

            FirstSelectedSlot = obj as SlotInfo;
            LastSelectedSlot = obj as SlotInfo;

            if (!FirstSelectedSlot.IsSelected)
                FirstSelectedSlot.IsPrepared = true;
        }

        /// <summary>
        /// Выполнить последовательно выбор слотов при передвижении мышки,
        /// при нажатой левой клавиши мышки
        /// </summary>
        /// <param name="obj">Слот в виде объекта</param>
        private void DoMouseMove(object obj)
        {
            if (ExecuteCheckSlots)
                return;

            LastSelectedSlot = obj as SlotInfo;
            Slots.SelectSlotsOfIndexses(FirstSelectedSlot, (obj as SlotInfo));
        }

        /// <summary>
        /// Выбрать последний слот при отпускании левой клавишу мышки
        /// </summary>
        /// <param name="obj">Слот в виде объекта</param>
        private void DoMouseUp(object obj)
        {
            if (ExecuteCheckSlots)
                return;

            if ((FirstSelectedSlot == null) && (LastSelectedSlot == null))
                return;

            if ((FirstSelectedSlot == LastSelectedSlot) && FirstSelectedSlot.IsSelected)
                FirstSelectedSlot.IsSelected = false;

            Slots.SelectPreparedSlots();

            FirstSelectedSlot = null;
            LastSelectedSlot = null;
        }

        /// <summary>
        /// Выполнить выбор или снятие выбора всех слотов
        /// </summary>
        /// <param name="obj">Флаг в виде строки</param>
        private void DoSelectAllSlots(object obj)
        {
            if (obj.ToString().Equals("true"))
                Slots.SelectAllSlots(true);
            else if (obj.ToString().Equals("false"))
                Slots.SelectAllSlots(false);
        }

        /// <summary>
        /// Переместить температуру вверх
        /// </summary>
        private void DoMoveUpTemperature()
        {
            Int32 index = Temperatures.IndexOf(SelectedTemperature);
            Temperatures.Move(index, index - 1);
        }

        /// <summary>
        /// Переместить температуру вниз
        /// </summary>
        private void DoMoveDownTemperature()
        {
            Int32 index = Temperatures.IndexOf(SelectedTemperature);
            Temperatures.Move(index, index + 1);
        }

        /// <summary>
        /// Передвинуть температуру
        /// </summary>
        /// <param name="obj">Данные температур которые нужно двигать</param>
        private void DoCatchDroppingTemperature(object obj)
        {
            TemperatureInfo destinationTemperature = (obj as DragAndDropData).DestinationObject as TemperatureInfo;
            TemperatureInfo sourceTemperature = (obj as DragAndDropData).SourceData as TemperatureInfo;

            if (sourceTemperature == null)
            {
                Object[] objects = (obj as DragAndDropData).SourceData as Object[];
                foreach (Object objData in objects)
                {
                    if ((objData is TemperatureInfo) && ((objData as TemperatureInfo) != destinationTemperature))
                    {
                        sourceTemperature = objData as TemperatureInfo;
                        break;
                    }
                }
            }

            if ((destinationTemperature != null) && (sourceTemperature != null))
            {
                Int32 indexDestination = Temperatures.IndexOf(destinationTemperature);
                Int32 indexSource = Temperatures.IndexOf(sourceTemperature);

                if ((indexDestination > -1) && (indexSource > -1))
                    Temperatures.Move(indexSource, indexDestination);
            }
        }

        /// <summary>
        /// Резрешение для того что бы передвинуть температуру
        /// </summary>
        /// <param name="arg">Аргументы для работы, данные для проверки изменения местами температур</param>
        /// <returns>Флаг разрешения</returns>
        private bool CanDoCatchDroppingTemperature(object arg)
        {
            if (ExecuteCheckSlots)
                return false;

            if (!(arg is DragAndDropData))
                return false;

            TemperatureInfo destinationTemperature = (arg as DragAndDropData).DestinationObject as TemperatureInfo;
            if (destinationTemperature == null)
                return false;

            if (!((arg as DragAndDropData).SourceData is TemperatureInfo))
            {
                Object[] objects = (arg as DragAndDropData).SourceData as Object[];
                if (objects == null)
                    return false;

                List<TemperatureInfo> differentTemperatures = new List<TemperatureInfo>();
                foreach (Object objData in objects)
                {
                    if ((objData is TemperatureInfo) && ((objData as TemperatureInfo) != destinationTemperature))
                        differentTemperatures.Add(objData as TemperatureInfo);
                }

                if ((differentTemperatures.Count == 0) || (differentTemperatures.Count > 1))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Закрытие программы
        /// </summary>
        /// <returns>Проверить возможность на закрытие программы</returns>
        private bool? CanDoClosingProgramm()
        {
            MessageBoxResult answer = MessageBox.Show(Resources.MsgQuestion_ClosingProgramm,
                Resources.MsqBox_Question, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (answer == MessageBoxResult.No)
                return null;

            if (File.Exists(CommonConstants.SERIALIZED_FILENAME))
                File.Delete(CommonConstants.SERIALIZED_FILENAME);

            List<Object> settings = new List<Object>();
            settings.Add(String.Format(CommonConstants.FRMT_PROPERTY_VARIABLE, CommonConstants.STR_TYPESLOT,
                MicroSlots.ToString()));


            if (Slots != null)
            {
                List<List<String>> rows = new List<List<String>>();
                foreach(MicroSlotRow row in Slots.Rows)
                {
                    List<String> setRow = new List<String>();
                    foreach (SlotInfo slot in row.Slots)
                        setRow.Add(slot.IsSelected.ToString());

                    rows.Add(setRow);
                }

                settings.Add(rows);
            }

            foreach(TemperatureInfo tmperature in Temperatures)
                settings.Add(String.Format(CommonConstants.FRMT_PROPERTY_VARIABLE, CommonConstants.STR_TEMPERATURE,
                    tmperature.Temperature));

            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(CommonConstants.SERIALIZED_FILENAME, FileMode.Create, FileAccess.Write, FileShare.None))
                binFormat.Serialize(fStream, settings);


            SettingsManager.Instance.Save();

            return true;
        }

        /// <summary>
        /// Восттановление информации
        /// </summary>
        private void RestoreInformation()
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
                foreach(Object obj in tmp)
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
                                MicroSlots = tmpType;
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
