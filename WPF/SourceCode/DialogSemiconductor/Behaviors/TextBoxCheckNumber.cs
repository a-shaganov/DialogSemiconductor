using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DialogSemiconductor.Behaviors
{
    /// <summary>
    /// Класс для проверки правильности ввода числовых значений в компонент TextBox
    /// </summary>
    public sealed class TextBoxCheckNumber
    {
        #region DependencyProperty
        /// <summary>
        /// Прикрепленное свойство комманды при выполнении события нажатия левой клавиши мышки
        /// </summary>
        public static readonly DependencyProperty CheckNumericProperty;

        /// <summary>
        /// Прикрепленное свойство комманды при выполнении события нажатия левой клавиши мышки
        /// </summary>
        public static readonly DependencyProperty CommandProperty;
        #endregion

        #region Constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        static TextBoxCheckNumber()
        {
            CheckNumericProperty = DependencyProperty.RegisterAttached("CheckNumeric", typeof(Boolean), typeof(TextBoxCheckNumber), new UIPropertyMetadata(false, OnPropertyChanged));
            CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(TextBoxCheckNumber), new UIPropertyMetadata(null));
        }
        #endregion

        #region Methods
        /// <summary>
        /// Метод получения флага проверки на число
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>Результат</returns>
        public static Boolean GetCheckNumeric(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(CheckNumericProperty);
        }

        /// <summary>
        /// Метод установки флага на проверку числа
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <param name="value">Знрачение</param>
        public static void SetCheckNumeric(DependencyObject obj, Boolean value)
        {
            obj.SetValue(CheckNumericProperty, value);
        }

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
        /// Метод для привязки события при установке объекта
        /// </summary>
        /// <param name="d">Прикрепленный объект</param>
        /// <param name="e">Аргументы события от прикрепленного объекта</param>
        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBox uie = (d as TextBox);
            if (uie != null)
            {
                if (e.NewValue != null)
                {
                    uie.PreviewTextInput += NumberValidationTextBox;
                    uie.TextChanged += Uie_TextChanged;
                }
                else
                {
                    uie.PreviewTextInput -= NumberValidationTextBox;
                    uie.TextChanged -= Uie_TextChanged;
                }
            }
        }

        /// <summary>
        /// Проверка на ввод текста
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private static void Uie_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICommand command = GetCommand((DependencyObject)sender);
            if ((command != null) && command.CanExecute(!String.IsNullOrEmpty((sender as TextBox).Text)))
                command.Execute(!String.IsNullOrEmpty((sender as TextBox).Text));
        }

        /// <summary>
        /// Проверка на правильность ввода числа
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private static void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        #endregion
    }
}
