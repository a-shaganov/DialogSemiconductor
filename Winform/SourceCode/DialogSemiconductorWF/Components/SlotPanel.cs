using CommonData.Slots;
using CommonDictionary.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DialogSemiconductorWF.Components
{
    /// <summary>
    /// Класс компонента панели, это представления еденичного слота
    /// </summary>
    public sealed class SlotPanel : Panel
    {
        #region Fields
        private ToolTip toolTip1;
        #endregion

        #region Properties
        /// <summary>
        /// Единичный слот
        /// </summary>
        public SlotInfo Slot
        { get; private set; }

        private Boolean _IsPrepared;
        /// <summary>
        /// 
        /// </summary>
        public Boolean IsPrepared
        {
            get { return _IsPrepared; }
            set
            {
                _IsPrepared = value;
                SetColor();
            }
        }

        private Boolean _IsSelected;
        /// <summary>
        /// 
        /// </summary>
        public Boolean IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                SetColor();
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
                SetColor();
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
                SetColor();
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="slot">Единичный слот</param>
        public SlotPanel(SlotInfo slot)
        {
            this.Size = new Size(20, 20);
            this.BorderStyle = BorderStyle.FixedSingle;

            ArgumentHelper.Null(slot, "slot is NULL");
            Slot = slot;

            this.DataBindings.Add("IsPrepared", Slot, "IsPrepared", false, DataSourceUpdateMode.OnPropertyChanged);
            this.DataBindings.Add("IsSelected", Slot, "IsSelected", false, DataSourceUpdateMode.OnPropertyChanged);
            this.DataBindings.Add("HasError", Slot, "HasError", false, DataSourceUpdateMode.OnPropertyChanged);
            this.DataBindings.Add("CheckEnd", Slot, "CheckEnd", false, DataSourceUpdateMode.OnPropertyChanged);

            toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Установка цвета
        /// </summary>
        private void SetColor()
        {
            if (CheckEnd)
            {
                if (HasError)
                    this.BackColor = Color.Red;
                else
                    this.BackColor = Color.Green;

                this.Invoke((MethodInvoker)delegate
                {
                    toolTip1.SetToolTip(this, this.Slot.ErrorDescription);
                });
            }
            else if (_IsPrepared)
                this.BackColor = Color.LightGreen;
            else
            {
                if (_IsSelected)
                    this.BackColor = Color.White;
                else
                    this.BackColor = Color.LightGray;
            }
        }
        #endregion
    }
}
