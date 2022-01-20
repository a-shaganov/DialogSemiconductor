using CommonDictionary.Notifies;

namespace DialogSemiconductor.ViewModels
{
    /// <summary>
    /// Абстрактный класс
    /// </summary>
    public abstract class AbstractViewModel : PropertyChangedNotify
    {
        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        protected AbstractViewModel()
        {
            InitializeCommands();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Инициализация команд
        /// </summary>
        protected abstract void InitializeCommands();
        #endregion
    }
}
