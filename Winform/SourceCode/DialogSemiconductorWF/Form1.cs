using CommonData.Enums;
using CommonData.Slots;
using CommonData.Temperatures;
using CommonDictionary.Attributes;
using CommonDictionary.Helpers;
using DialogSemiconductorWF.Components;
using DialogSemiconductorWF.EventArguments;
using DialogSemiconductorWF.Helpers;
using DialogSemiconductorWF.Model;
using DialogSemiconductorWF.Properties;
using DialogSemiconductorWF.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialogSemiconductorWF
{
    public partial class mainForm : Form
    {
        #region Fields
        /// <summary>
        /// Контекствое меню для удаления настройки температуры
        /// </summary>
        private ContextMenuStrip collectionMIListBoxTemperatures;

        /// <summary>
        /// Модель для привязки к пользовательскому интерфейсу
        /// </summary>
        private DataBindingModel BindingModel;

        /// <summary>
        /// Первый выбранный слот
        /// </summary>
        private SlotInfo FirstSelectedSlot;

        /// <summary>
        /// Последний выбранный слот
        /// </summary>
        private SlotInfo LastSelectedSlot;
        #endregion

        #region Consturctor
        /// <summary>
        /// Конструктор
        /// </summary>
        public mainForm()
        {
            InitializeComponent();

            InitializeBindinngs();
            SetSettingsForUIComponents();

            ExecuteDrawingSlots(BindingModel.Slots);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Установка настроек формы
        /// </summary>
        private void SetSettingsForUIComponents()
        {
            deleteTemperature.ToolTipText = Resources.Btn_DeleteSelectedTemperature;
            upTemperature.ToolTipText = Resources.Btn_MoveUpSelectedTemperature;
            downTemperature.ToolTipText = Resources.Btn_MoveDownSelectedTemperature;
            selectAllSlots.ToolTipText = Resources.Btn_SelectAllSlots;
            deselectAllSlots.ToolTipText = Resources.Btn_DeselectAllSlots;
            executeProgramm.ToolTipText = Resources.Btn_ExecuteProgramm;
            returnToSettings.ToolTipText = Resources.Btn_BackToSettings;

            lblTemperatures.Text = Resources.Field_Temperature;
            lblAddTemperature.Text = Resources.Field_AddTemperature;

            btnAddTemperature.Text = Resources.Btn_AddTemperature;
            this.Text = Resources.Title_Programm;
        }

        /// <summary>
        /// Инициализация привязок
        /// </summary>
        private void InitializeBindinngs()
        {
            this.Height = (Int32)SettingsManager.Instance.General.MFHeight;
            this.Width = (Int32)SettingsManager.Instance.General.MFWidth;
            this.Top = (Int32)SettingsManager.Instance.General.MFTop;
            this.Left = (Int32)SettingsManager.Instance.General.MFLeft;
            this.WindowState = SettingsManager.Instance.General.MFState;

            BindingModel = new DataBindingModel();
            BindingModel.RestoreInformation();
            BindingModel.ChangedSlotsEventHandler += BindingModel_ChangedSlotsEventHandler;
            BindingModel.StartedOrEndExecuteCeck += BindingModel_StartedOrEndExecuteCeck;

            deleteTemperature.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            upTemperature.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            downTemperature.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            selectAllSlots.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            deselectAllSlots.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            executeProgramm.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            returnToSettings.DataBindings.Add("Visible", BindingModel, "ExecuteCheckSlots", false, DataSourceUpdateMode.OnPropertyChanged);
            returnToSettings.DataBindings.Add("Enabled", BindingModel, "ExecuteDontWorking", false, DataSourceUpdateMode.OnPropertyChanged);

            lblAddTemperature.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            pnlAddTemperature.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);

            typeSlotsChooser.ComboBox.DisplayMember = "Key";
            typeSlotsChooser.ComboBox.ValueMember = "Value";
            typeSlotsChooser.ComboBox.DataBindings.Add("DataSource", BindingModel, "SlotTypes", false, DataSourceUpdateMode.OnPropertyChanged);
            typeSlotsChooser.ComboBox.DataBindings.Add("SelectedIndex", BindingModel, "SelectedSlotIndex", false, DataSourceUpdateMode.OnPropertyChanged);
            typeSlotsChooser.ComboBox.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);

            sepTempAndTypeSlots.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            toolStripSeparator1.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);
            toolStripSeparator2.DataBindings.Add("Visible", BindingModel, "InvisibleSettings", false, DataSourceUpdateMode.OnPropertyChanged);

            lbTemperatures.DisplayMember = "Temperature";
            lbTemperatures.DataBindings.Add("DataSource", BindingModel, "Temperatures", false, DataSourceUpdateMode.OnPropertyChanged);
            lbTemperatures.DataBindings.Add("SelectedIndex", BindingModel, "SelectedTemperatureIndex", false, DataSourceUpdateMode.OnPropertyChanged);
            lbTemperatures.AllowDrop = true;

            ToolStripMenuItem miDeleteTemperature = new ToolStripMenuItem { Text = Resources.Btn_DeleteSelectedTemperature };
            miDeleteTemperature.Click += MiDeleteTemperature_Click;
            collectionMIListBoxTemperatures = new ContextMenuStrip();
            collectionMIListBoxTemperatures.Items.Add(miDeleteTemperature);
            lbTemperatures.MouseUp += LbTemperatures_MouseUp;

            tbAddTemperature.DataBindings.Add("Text", BindingModel, "Temperature", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        /// <summary>
        /// Событие дляот ображение контекстного меню при нажатии левой клавиши мышки
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void LbTemperatures_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            var index = lbTemperatures.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                BindingModel.SelectedTemperatureIndex = index;
                collectionMIListBoxTemperatures.Show(Cursor.Position);
                collectionMIListBoxTemperatures.Visible = true;
            }
            else
                collectionMIListBoxTemperatures.Visible = false;
        }

        /// <summary>
        /// Событие удаления температуры из контекстного меню
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void MiDeleteTemperature_Click(object sender, EventArgs e)
        {
            BindingModel.DeleteSelectedTemperature();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTemperature_Click(object sender, EventArgs e)
        {
            UInt16 temperature = 0;
            if (UInt16.TryParse(tbAddTemperature.Text, out temperature))
                BindingModel.AddTemperature(temperature);
        }

        /// <summary>
        /// Событие которое оповещает о начале или конце запуска проверки
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void BindingModel_StartedOrEndExecuteCeck(object sender, EventArgs e)
        {
            if (BindingModel.ExecuteCheckSlots)
            {
                lblTemperatures.Location = new Point(12, 33);
                pnlTemperatures.Location = new Point(5, 50);
                
            }
            else
            {
                lblTemperatures.Location = new Point(12, 141);
                pnlTemperatures.Location = new Point(5, 157);
            }
        }

        /// <summary>
        /// Событие изменения слота вызвоного из привязанной модели
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void BindingModel_ChangedSlotsEventHandler(object sender, ChangedMicroSlotsArg e)
        {
            ExecuteDrawingSlots(e.SelectedSlots);
        }

        /// <summary>
        /// Выполнить отрисовку слотов
        /// </summary>
        /// <param name="slots">Слоты</param>
        private void ExecuteDrawingSlots(MicroSlots slots)
        {
            slotsSelector.SuspendLayout();
            slotsSelector.Visible = false;

            if (slotsSelector.Controls.Count > 0)
            {
                foreach(Object obj in slotsSelector.Controls)
                {
                    if (!(obj is SlotPanel))
                        continue;

                    (obj as SlotPanel).MouseDown -= SlotPanel_MouseDown;
                    (obj as SlotPanel).MouseMove -= SlotPanel_MouseMove;
                    (obj as SlotPanel).MouseUp -= SlotPanel_MouseUp;
                }
            }

            slotsSelector.Controls.Clear();

            if (slots != null)
            {
                Int32 positionRow = 10;
                Int32 positionSlot = 10;
                foreach (MicroSlotRow row in slots.Rows)
                {
                    foreach (SlotInfo slot in row.Slots)
                    {
                        SlotPanel slotPanel = new SlotPanel(slot);
                        slotPanel.Location = new Point(positionSlot, positionRow);
                        slotPanel.MouseDown += SlotPanel_MouseDown;
                        slotPanel.MouseMove += SlotPanel_MouseMove;
                        slotPanel.MouseUp += SlotPanel_MouseUp;

                        slotsSelector.Controls.Add(slotPanel);

                        positionSlot += 22;
                    }

                    positionSlot = 10;
                    positionRow += 22;
                }
            }

            slotsSelector.Visible = true;
            slotsSelector.ResumeLayout();
        }

        /// <summary>
        /// Метод подготовки выбора первой панели со слотом
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void SlotPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (BindingModel.ExecuteCheckSlots)
                return;

            FirstSelectedSlot = (sender as SlotPanel).Slot;
            LastSelectedSlot = (sender as SlotPanel).Slot;
            if (!FirstSelectedSlot.IsSelected)
                FirstSelectedSlot.IsPrepared = true;
        }

        /// <summary>
        /// Метод оповещающий что на панелью слота передвинут курсор мышки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlotPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (BindingModel.ExecuteCheckSlots)
                return;

            if (FirstSelectedSlot == null)
                return;

            Control cntrl = SeekContolHelper.GetControlUnderCursor();
            if (cntrl == null)
                return;

            if ((cntrl is SlotPanel) && (FirstSelectedSlot != (cntrl as SlotPanel).Slot))
            {
                BindingModel.Slots.SelectSlotsOfIndexses(FirstSelectedSlot, (cntrl as SlotPanel).Slot);
                LastSelectedSlot = (cntrl as SlotPanel).Slot;
            }
        }

        /// <summary>
        /// Метод выбора панели(ей) со слотом
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void SlotPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (BindingModel.ExecuteCheckSlots)
                return;

            if ((FirstSelectedSlot == null) && (LastSelectedSlot == null))
                return;

            if ((FirstSelectedSlot == LastSelectedSlot) && FirstSelectedSlot.IsSelected)
                FirstSelectedSlot.IsSelected = false;

            BindingModel.Slots.SelectPreparedSlots();

            FirstSelectedSlot = null;
            LastSelectedSlot = null;
        }

        /// <summary>
        /// Событие кнопки отвечающей за выбор всех слотов
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void selectAllSlots_Click(object sender, EventArgs e)
        {
            BindingModel.Slots.SelectAllSlots(true);
        }

        /// <summary>
        /// Событие кнопки отвечающей за отмену выбора всех слотов
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void deselectAllSlots_Click(object sender, EventArgs e)
        {
            BindingModel.Slots.SelectAllSlots(false);
        }

        /// <summary>
        /// Событие изменения текста в компоненте
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void tbAddTemperature_TextChanged(object sender, EventArgs e)
        {
          btnAddTemperature.Enabled = true;

            if (String.IsNullOrEmpty(tbAddTemperature.Text))
                btnAddTemperature.Enabled = false;

            Regex regex = new Regex("[^0-9]+");
            if (regex.IsMatch(tbAddTemperature.Text))
                btnAddTemperature.Enabled = false;

            UInt16 temperature = 0;
            if (!UInt16.TryParse(tbAddTemperature.Text, out temperature))
                btnAddTemperature.Enabled = false;
        }

        /// <summary>
        /// Событие когда температура перетаскивается в другой объект
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void lbTemperatures_DragEnter(object sender, DragEventArgs e)
        {
            ListBox lst = sender as ListBox;

            if (!e.Data.GetDataPresent(typeof(DragItem)))
                e.Effect = DragDropEffects.None;
            else if ((e.AllowedEffect & DragDropEffects.Move) == 0)
                e.Effect = DragDropEffects.None;
            else
            {
                DragItem drag_item = (DragItem)e.Data.GetData(typeof(DragItem));
                if (drag_item.Client != lst)
                    e.Effect = DragDropEffects.None;
                else
                    e.Effect = DragDropEffects.Move;
            }
        }

        /// <summary>
        /// событие окончания перетаскивания объекта
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void lbTemperatures_DragDrop(object sender, DragEventArgs e)
        {
            ListBox lst = sender as ListBox;

            DragItem drag_item = (DragItem)e.Data.GetData(typeof(DragItem));

            int new_index = IndexFromScreenPoint(lst, new Point(e.X, e.Y));

            if (new_index == -1)
                new_index = lst.Items.Count - 1;

            if (new_index != drag_item.Index)
            {
                BindingModel.Temperatures.RemoveAt(drag_item.Index);
                BindingModel.Temperatures.Insert(new_index, drag_item.Item as TemperatureInfo);
                BindingModel.SelectedTemperatureIndex = new_index;
            }
        }

        /// <summary>
        /// Событие перетаскивания когда объект выходит з а пределы
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void lbTemperatures_DragOver(object sender, DragEventArgs e)
        {
            if (e.Effect != DragDropEffects.Move)
                return;

            ListBox lst = sender as ListBox;
            BindingModel.SelectedTemperatureIndex = IndexFromScreenPoint(lst, new Point(e.X, e.Y));
        }

        /// <summary>
        /// Получение объекта по точке локации
        /// </summary>
        /// <param name="lst">Компонент со списком</param>
        /// <param name="point">Точка</param>
        /// <returns>Индекс объекта</returns>
        public int IndexFromScreenPoint(ListBox lst, Point point)
        {
            point = lst.PointToClient(point);
            return lst.IndexFromPoint(point);
        }

        /// <summary>
        /// Событие при нажатии клавиши на объект в списке
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void lbTemperatures_MouseDown(object sender, MouseEventArgs e)
        {
            if (BindingModel.ExecuteCheckSlots)
                return;

            ListBox lst = sender as ListBox;

            if (e.Button != MouseButtons.Left)
                return;

            int index = lst.IndexFromPoint(e.Location);
            BindingModel.SelectedTemperatureIndex = index;

            if (index < 0)
                return;

            DragItem drag_item = new DragItem(lst, index, lst.Items[index]);
            lst.DoDragDrop(drag_item, DragDropEffects.Move);
        }

        /// <summary>
        /// Удалить выбраную температуру
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void deleteTemperature_Click(object sender, EventArgs e)
        {
            BindingModel.DeleteSelectedTemperature();
        }

        /// <summary>
        /// Событие кнопки для поднятия на верх температуры
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void upTemperature_Click(object sender, EventArgs e)
        {
            if (BindingModel.SelectedTemperatureIndex <= 0)
                return;

            TemperatureInfo info = BindingModel.SelectedTemperature;

            Int32 index = BindingModel.SelectedTemperatureIndex;

            BindingModel.Temperatures.RemoveAt(index);
            BindingModel.Temperatures.Insert(index - 1, info);
            BindingModel.SelectedTemperatureIndex = index - 1;
        }

        /// <summary>
        /// Событие кнопки для поднятия вниз температуры
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void downTemperature_Click(object sender, EventArgs e)
        {
            if (BindingModel.SelectedTemperatureIndex < 0)
                return;

            if (BindingModel.SelectedTemperatureIndex >= (BindingModel.Temperatures.Count - 1))
                return;

            TemperatureInfo info = BindingModel.SelectedTemperature;
            Int32 index = BindingModel.SelectedTemperatureIndex;

            BindingModel.Temperatures.RemoveAt(index);
            if ((index + 1) > (BindingModel.Temperatures.Count - 1))
                BindingModel.Temperatures.Add(info);
            else
                BindingModel.Temperatures.Insert(index + 1, info);

            BindingModel.SelectedTemperatureIndex = index + 1;
            lbTemperatures.SelectedIndex = index + 1;
        }

        /// <summary>
        /// Событие кнопки выполнения проверки слотов
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void executeProgramm_Click(object sender, EventArgs e)
        {
            BindingModel.ExecuteCheckingSlots();
        }

        /// <summary>
        /// Событие кнопки для возвращения к настройкам
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void returnToSettings_Click(object sender, EventArgs e)
        {
            BindingModel.ExecuteCheckingSlots();
        }

        /// <summary>
        /// Событие закрытия окна программы
        /// </summary>
        /// <param name="sender">Объект который вызвал событие</param>
        /// <param name="e">Параметры события</param>
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            BindingModel.SaveInformation();

            SettingsManager.Instance.General.MFHeight = this.Height;
            SettingsManager.Instance.General.MFWidth = this.Width;
            SettingsManager.Instance.General.MFTop = this.Top;
            SettingsManager.Instance.General.MFLeft = this.Left;
            SettingsManager.Instance.General.MFState = this.WindowState;
            
            SettingsManager.Instance.Save();
            Thread.Sleep(100);
        }
        #endregion
    }
}
