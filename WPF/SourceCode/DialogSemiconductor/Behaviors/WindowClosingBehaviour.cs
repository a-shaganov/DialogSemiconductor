using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DialogSemiconductor.Behaviors
{
    /// <summary>
    /// Класс поддержки событий, генерируемых при закрытии окна
    /// </summary>
    public sealed class WindowClosingBehaviour
    {
        static WindowClosingBehaviour()
        {
            ClosedProperty = DependencyProperty.RegisterAttached(
                "Closed", typeof(ICommand), typeof(WindowClosingBehaviour), new UIPropertyMetadata(ClosedChanged));
            ClosingProperty = DependencyProperty.RegisterAttached(
                "Closing", typeof(ICommand), typeof(WindowClosingBehaviour), new UIPropertyMetadata(ClosingChanged));
        }

        public static readonly DependencyProperty ClosedProperty;
        public static readonly DependencyProperty ClosingProperty;

        public static ICommand GetClosed(DependencyObject element)
        {
            return (ICommand)element.GetValue(ClosedProperty);
        }

        public static ICommand GetClosing(DependencyObject element)
        {
            return (ICommand)element.GetValue(ClosingProperty);
        }


        public static void SetClosed(DependencyObject element, ICommand value)
        {
            element.SetValue(ClosedProperty, value);
        }

        public static void SetClosing(DependencyObject element, ICommand value)
        {
            element.SetValue(ClosingProperty, value);
        }


        private static void ClosedChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            Window window = element as Window;
            if (window != null)
            {
                if (args.NewValue != null)
                    window.Closed += WindowClosed;
                else
                    window.Closed -= WindowClosed;
            }
        }

        private static void ClosingChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            Window window = element as Window;
            if (window != null)
            {
                if (args.NewValue != null)
                    window.Closing += WindowClosing;
                else
                    window.Closing -= WindowClosing;
            }
        }

        /// <summary>
        /// Работа с командой Closed при событии Closed сопровождаемого окна
        /// </summary>
        private static void WindowClosed(object sender, EventArgs args)
        {
            ICommand closedCommand = GetClosed((DependencyObject)sender);
            if (closedCommand != null)
                closedCommand.Execute(null);
        }

        /// <summary>
        /// Работа с командой Closing при событии Closing сопровождаемого окна
        /// </summary>
        private static void WindowClosing(object sender, CancelEventArgs args)
        {
            ICommand closingCommand = GetClosing((DependencyObject)sender);
            if (closingCommand != null)
            {
                if (closingCommand.CanExecute(null))
                    closingCommand.Execute(null);
                else
                    args.Cancel = true;
            }
        }
    }
}