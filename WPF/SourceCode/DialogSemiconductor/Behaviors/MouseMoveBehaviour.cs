using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DialogSemiconductor.Behaviors
{
    public sealed class MouseMoveBehaviour
    {
        #region DependencyProperty
        /// <summary>
        /// Прикрепленное свойство комманды при выполнении события нажатия левой клавиши мышки
        /// </summary>
        public static readonly DependencyProperty CommandProperty;

        /// <summary>
        /// Прикрепленное свойство объекта
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        static MouseMoveBehaviour()
        {
            CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(MouseMoveBehaviour), new UIPropertyMetadata(null, OnPropertyChanged));
            CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(Object), typeof(MouseMoveBehaviour));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Метод получения выполняемой комманды
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Результат</returns>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        /// <summary>
        /// Метод установки выполняемой команды
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="value">Знрачение</param>
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Метод получения свойства прикрепленного объекта
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Результат</returns>
        public static Object GetCommandParameter(DependencyObject obj)
        {
            return (Object)obj.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// Метод установки свойства прикрепленного объекта
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="value">Знрачение</param>
        public static void SetCommandParameter(DependencyObject obj, Object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        /// <summary>
        /// Метод для привязки события при установке объекта
        /// </summary>
        /// <param name="d">Прикрепленный объект</param>
        /// <param name="e">Аргументы события от прикрепленного объекта</param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UIElement uie = (d as UIElement);
            if (uie != null)
            {
                if (e.NewValue != null)
                    uie.MouseMove += uie_MouseMove;
                else
                    uie.MouseMove -= uie_MouseMove;
            }
        }

        /// <summary>
        /// Выполнение свойства при нажатии на левую клавишу мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void uie_MouseMove(object sender, MouseEventArgs e)
        {
            ICommand command = GetCommand((DependencyObject)sender);
            if ((command != null) && (command.CanExecute(GetCommandParameter((DependencyObject)sender))))
                command.Execute(GetCommandParameter((DependencyObject)sender));
        }
        #endregion
    }
}
