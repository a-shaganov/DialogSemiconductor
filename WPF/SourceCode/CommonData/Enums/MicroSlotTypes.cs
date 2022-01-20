using CommonData.Attributes;
using CommonData.Properties;
using CommonDictionary.Attributes;

namespace CommonData.Enums
{
    /// <summary>
    /// Типы слотов
    /// </summary>
    public enum MicroSlotTypes
    {
        /// <summary>
        /// Маленький (30х40)
        /// </summary>
        [SizedSlot(30, 40)]
        [LocalizedDescription(typeof(Resources), "MicroSlotTypes_Small")]
        Small,
        /// <summary>
        /// Середній (25х25)
        /// </summary>
        [SizedSlot(25, 25)]
        [LocalizedDescription(typeof(Resources), "MicroSlotTypes_Middle")]
        Middle,
        /// <summary>
        /// Великий (30х40)
        /// </summary>
        [SizedSlot(20, 15)]
        [LocalizedDescription(typeof(Resources), "MicroSlotTypes_Large")]
        Large
    }
}
