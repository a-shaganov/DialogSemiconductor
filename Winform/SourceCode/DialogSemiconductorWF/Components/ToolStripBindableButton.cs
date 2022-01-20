using System.Windows.Forms;

namespace DialogSemiconductorWF.Components
{
    /// <summary>
    /// Компонент для подвязки к свойствам информации
    /// </summary>
    public class ToolStripBindableButton : ToolStripButton, IBindableComponent
    {
        #region Fields
        private ControlBindingsCollection _DataBindings;
        /// <summary>
        /// Коллекция привязок
        /// </summary>
        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (_DataBindings == null)
                    _DataBindings = new ControlBindingsCollection(this);

                return _DataBindings;
            }
        }

        private BindingContext _BindingContext;
        /// <summary>
        /// Контекст привязок
        /// </summary>
        public BindingContext BindingContext
        {
            get
            {
                if (_BindingContext == null)
                    _BindingContext = new BindingContext();
                
                return _BindingContext;
            }
            set
            { _BindingContext = value; }
        }
        #endregion
    }
}