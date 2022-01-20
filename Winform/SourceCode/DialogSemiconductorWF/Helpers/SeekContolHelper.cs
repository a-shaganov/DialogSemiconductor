using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DialogSemiconductorWF.Helpers
{
    /// <summary>
    /// Класс для помощи поиска нужного компонента на форме
    /// </summary>
    public static class SeekContolHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pnt);

        /// <summary>
        /// Метод получения компонента над которым проводиться курсор мышки
        /// </summary>
        /// <returns></returns>
        public static Control GetControlUnderCursor()
        {
            var handle = WindowFromPoint(Control.MousePosition);
            if (handle != IntPtr.Zero)
                return Control.FromHandle(handle);

            return null;
        }
    }
}
