using System;
using System.Windows;
using System.Windows.Input;

namespace DialogSemiconductor.Behaviors
{
    /// <summary>
    /// Класс поддержки технологии Drag&Drop
    /// </summary>
    public sealed class DragAndDropBehaviour
    {
        static DragAndDropBehaviour()
        {
            AttachedDataProperty = DependencyProperty.RegisterAttached(
                "AttachedData", typeof(object), typeof(DragAndDropBehaviour));
            CatchDroppingCommandProperty = DependencyProperty.RegisterAttached(
                "CatchDroppingCommand", typeof(ICommand), typeof(DragAndDropBehaviour));
            DestinationDragDropEffectProperty = DependencyProperty.RegisterAttached(
                "DestinationDragDropEffect", typeof(DragDropEffects), typeof(DragAndDropBehaviour), new UIPropertyMetadata(DragDropEffects.Link));
            IsDragSourceProperty = DependencyProperty.RegisterAttached(
                "IsDragSource", typeof(bool), typeof(DragAndDropBehaviour), new UIPropertyMetadata(OnIsDragSourceChanged));
            IsDropDestinationProperty = DependencyProperty.RegisterAttached(
                "IsDropDestination", typeof(bool), typeof(DragAndDropBehaviour), new UIPropertyMetadata(OnIsDropDestinationChanged));
            SourceDragDropEffectProperty = DependencyProperty.RegisterAttached(
                "SourceDragDropEffect", typeof(DragDropEffects), typeof(DragAndDropBehaviour), new UIPropertyMetadata(DragDropEffects.Link));
        }

        /// <summary>
        /// Ghbrht
        /// </summary>
        public static readonly DependencyProperty AttachedDataProperty;
        /// <summary>
        /// Команда приема передаваемого обьекта
        /// </summary>
        public static readonly DependencyProperty CatchDroppingCommandProperty;
        /// <summary>
        /// Признак, определяющий является ли компонент источником Drag события
        /// </summary>
        public static readonly DependencyProperty IsDragSourceProperty;
        /// <summary>
        /// Признак, определяющий является ли компонент приемником Drop события
        /// </summary>
        public static readonly DependencyProperty IsDropDestinationProperty;
        /// <summary>
        /// Тип отображаемой операции для приемника
        /// </summary>
        public static readonly DependencyProperty DestinationDragDropEffectProperty;
        /// <summary>
        /// Тип отображаемой операции для источника
        /// </summary>
        public static readonly DependencyProperty SourceDragDropEffectProperty;



        #region Get\Set methods of attached properties

        public static object GetAttachedData(DependencyObject obj)
        {
            return (object)obj.GetValue(AttachedDataProperty);
        }

        public static void SetAttachedData(DependencyObject obj, object value)
        {
            obj.SetValue(AttachedDataProperty, value);
        }

        public static ICommand GetCatchDroppingCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CatchDroppingCommandProperty);
        }

        public static void SetCatchDroppingCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CatchDroppingCommandProperty, value);
        }

        public static bool GetIsDragSource(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragSourceProperty);
        }

        public static void SetIsDragSource(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragSourceProperty, value);
        }

        public static bool GetIsDropDestination(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDropDestinationProperty);
        }

        public static void SetIsDropDestination(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDropDestinationProperty, value);
        }

        public static DragDropEffects GetDestinationDragDropEffect(DependencyObject obj)
        {
            return (DragDropEffects)obj.GetValue(DestinationDragDropEffectProperty);
        }

        public static void SetDestinationDragDropEffect(DependencyObject obj, DragDropEffects value)
        {
            obj.SetValue(DestinationDragDropEffectProperty, value);
        }

        public static DragDropEffects GetSourceDragDropEffect(DependencyObject obj)
        {
            return (DragDropEffects)obj.GetValue(SourceDragDropEffectProperty);
        }

        public static void SetSourceDragDropEffect(DependencyObject obj, DragDropEffects value)
        {
            obj.SetValue(SourceDragDropEffectProperty, value);
        }

        #endregion

        /// <summary>
        /// Извлечь данные, представленные обьектом IDataObject
        /// </summary>
        /// <param name="dataObject"> Представление данных </param>
        /// <returns> Данные </returns>
        private static object ExtractData(IDataObject dataObject)
        {
            if (dataObject != null)
            {
                String[] formats = dataObject.GetFormats();
                if (formats.Length == 1)
                    return dataObject.GetData(formats[0]);
            }

            return null;
        }

        /// <summary>
        /// Обработка изменения свойства IsDragSource
        /// </summary>
        private static void OnIsDragSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement visualElement = (FrameworkElement)d;

            if ((bool)args.OldValue)
                visualElement.MouseDown -= OnDragSourceMouseDown;
            if ((bool)args.NewValue)
                visualElement.MouseDown += OnDragSourceMouseDown;
        }

        /// <summary>
        /// Обработка изменения свойства IsDropDestination
        /// </summary>
        private static void OnIsDropDestinationChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            FrameworkElement visualElement = (FrameworkElement)d;

            if ((bool)args.OldValue)
            {
                visualElement.AllowDrop = false;
                visualElement.DragEnter -= OnDropDestinationDragEvents;
                visualElement.DragOver -= OnDropDestinationDragEvents;
                visualElement.Drop -= OnDropDestinationDrop;
            }

            if ((bool)args.NewValue)
            {
                visualElement.AllowDrop = true;
                visualElement.DragEnter += OnDropDestinationDragEvents;
                visualElement.DragOver += OnDropDestinationDragEvents;
                visualElement.Drop += OnDropDestinationDrop;
            }
        }

        /// <summary>
        /// Активация механизма Drag&Drop
        /// </summary>
        private static void OnDragSourceMouseDown(object sender, MouseButtonEventArgs args)
        {
            if (args.ChangedButton == MouseButton.Left)
            {
                FrameworkElement visualElement = sender as FrameworkElement;
                if ((visualElement == null) || (visualElement.DataContext == null))
                    return;

                DragDrop.DoDragDrop(visualElement, visualElement.DataContext, GetSourceDragDropEffect((FrameworkElement)sender));
            }
        }

        /// <summary>
        /// Проверка поддержки приемником передаваемого обьекта
        /// </summary>
        private static void OnDropDestinationDragEvents(object sender, DragEventArgs args)
        {
            FrameworkElement destVElement = (FrameworkElement)sender;

            ICommand command = GetCatchDroppingCommand(destVElement);
            object data = ExtractData(args.Data);

            object attachetData = GetAttachedData(destVElement);
            if (attachetData != null)
                data = new object[] { data, attachetData };

            if ((command != null) && (data != null) && command.CanExecute(new DragAndDropData(data, destVElement.DataContext)))
                args.Effects = GetDestinationDragDropEffect(destVElement);
            else
                args.Effects = DragDropEffects.None;
        }

        /// <summary>
        /// Прием передаваемого обьекта
        /// </summary>
        private static void OnDropDestinationDrop(object sender, DragEventArgs args)
        {
            FrameworkElement destVElement = (FrameworkElement)sender;

            ICommand command = GetCatchDroppingCommand(destVElement);

            object data = ExtractData(args.Data);

            object attachetData = GetAttachedData(destVElement);
            if (attachetData != null)
                data = new object[] { data, attachetData };

            if (command != null)
                command.Execute(new DragAndDropData(data, destVElement.DataContext));
        }
    }
}
