using System.Configuration;

namespace DialogSemiconductor.Settings
{
    /// <summary>
    /// Класс управления настройками программы
    /// </summary>
    public sealed class SettingsManager
    {
        private static SettingsManager _instance = null;
        /// <summary>
        /// Экземпляр класса
        /// </summary>
        public static SettingsManager Instance
        {
            get
            { return _instance ?? (_instance = new SettingsManager()); }
        }

        /// <summary>
        /// Общие настройки приложения
        /// </summary>
        public GeneralSettings General
        { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        private SettingsManager()
        {
            General = (GeneralSettings)SettingsBase.Synchronized(new GeneralSettings());
        }

        /// <summary>
        /// Сбросить все настройки приложения в значения по умолчанию
        /// </summary>
        public void Reset()
        {
            General.Reset();
        }

        /// <summary>
        /// Перезагрузить ранее сохраненные настройки приложения
        /// </summary>
        public void Reload()
        {
            General.Reload();
        }

        /// <summary>
        /// Сохранить настройки приложения
        /// </summary>
        public void Save()
        {
            General.Save();
        }
    }
}
