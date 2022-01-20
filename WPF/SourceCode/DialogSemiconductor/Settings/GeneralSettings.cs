using System;
using System.Configuration;
using System.Windows;

namespace DialogSemiconductor.Settings
{
    /// <summary>
    /// Класс представления секции настроек программы
    /// </summary>
    [SettingsGroupName("General")]
    public sealed class GeneralSettings : ApplicationSettingsBase
    {
        /// <summary>
        /// Состояние главной формы
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("Normal")]
        public WindowState MFState
        {
            get
            { return (WindowState)this["MFState"]; }
            set
            { this["MFState"] = value; }
        }

        /// <summary>
        /// Положение левой грани главной формы относительно экрана
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("10")]
        public double MFLeft
        {
            get
            { return (double)this["MFLeft"]; }
            set
            { this["MFLeft"] = value; }
        }

        /// <summary>
        /// Высота главной формы
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("700")]
        public double MFHeight
        {
            get
            { return (double)this["MFHeight"]; }
            set
            { this["MFHeight"] = value; }
        }

        /// <summary>
        /// Положение верхней грани главной формы относительно экрана
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("10")]
        public double MFTop
        {
            get
            { return (double)this["MFTop"]; }
            set
            { this["MFTop"] = value; }
        }

        /// <summary>
        /// Ширина главной формы
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("950")]
        public double MFWidth
        {
            get
            { return (double)this["MFWidth"]; }
            set
            { this["MFWidth"] = value; }
        }

        /// <summary>
        /// Состояние главной формы
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("Normal")]
        public WindowState CFState
        {
            get
            { return (WindowState)this["CFState"]; }
            set
            { this["CFState"] = value; }
        }

        /// <summary>
        /// Положение левой грани главной формы относительно экрана
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("10")]
        public double CFLeft
        {
            get
            { return (double)this["CFLeft"]; }
            set
            { this["CFLeft"] = value; }
        }

        /// <summary>
        /// Высота главной формы
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("450")]
        public double CFHeight
        {
            get
            { return (double)this["CFHeight"]; }
            set
            { this["CFHeight"] = value; }
        }

        /// <summary>
        /// Положение верхней грани главной формы относительно экрана
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("10")]
        public double CFTop
        {
            get
            { return (double)this["CFTop"]; }
            set
            { this["CFTop"] = value; }
        }

        /// <summary>
        /// Ширина главной формы
        /// </summary>
        [UserScopedSetting]
        [DefaultSettingValue("800")]
        public double CFWidth
        {
            get
            { return (double)this["CFWidth"]; }
            set
            { this["CFWidth"] = value; }
        }
    }
}
