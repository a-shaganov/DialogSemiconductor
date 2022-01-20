using System;

namespace CommonData.Slots
{
    /// <summary>
    /// Класс отвечающий за слот
    /// </summary>
    public sealed class SlotInfo : AbstractInfo
    {
        #region Properties
        private Boolean _IsPrepared;
        /// <summary>
        /// Флаг означающий что слот выбран
        /// </summary>
        public Boolean IsPrepared
        {
            get { return _IsPrepared; }
            set
            {
                _IsPrepared = value;
                RaisePropertyChanged("IsPrepared");
            }
        }

        private Boolean _IsSelected;
        /// <summary>
        /// Флаг означающий что слот выбран
        /// </summary>
        public Boolean IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        private Boolean _HasError;
        /// <summary>
        /// Присуствует ошибка
        /// </summary>
        public Boolean HasError
        {
            get { return _HasError; }
            set
            {
                _HasError = value;
                RaisePropertyChanged("HasError");
            }
        }

        private String _ErrorDescription;
        /// <summary>
        /// Описание ошибки
        /// </summary>
        public String ErrorDescription
        {
            get { return _ErrorDescription; }
            set
            {
                _ErrorDescription = value;
                RaisePropertyChanged("ErrorDescription");
            }
        }

        private Boolean _CheckEnd;
        /// <summary>
        /// Выполнена проверка
        /// </summary>
        public Boolean CheckEnd
        {
            get { return _CheckEnd; }
            set
            {
                _CheckEnd = value;
                RaisePropertyChanged("CheckEnd");
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        public SlotInfo()
        {
            IsSelected = false;
            HasError = false;
            CheckEnd = false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Метод клонирования объекта
        /// </summary>
        /// <returns>Копия объекта</returns>
        public override AbstractInfo Clone()
        {
            return new SlotInfo()
            {
                IsSelected = this.IsSelected,
                HasError = this.HasError,
                CheckEnd = this.CheckEnd
            };
        }
        #endregion
    }
}