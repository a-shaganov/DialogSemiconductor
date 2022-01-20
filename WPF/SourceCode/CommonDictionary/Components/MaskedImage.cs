using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CommonDictionary.Components
{
    /// <summary>
    /// Компонент отображения рисунков с возможностью наложения маски при
    /// изменении состояния доступности компонента (enabled\disabled)
    /// </summary>
    public class MaskedImage : Image
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        static MaskedImage()
        {
            MaskPixelFormatProperty = DependencyProperty.Register(
                "MaskPixelFormat", typeof(PixelFormat), typeof(MaskedImage), new FrameworkPropertyMetadata(PixelFormats.Gray8), IsPixelFormatValid);

            IsEnabledProperty.OverrideMetadata(typeof(MaskedImage), new FrameworkPropertyMetadata(IsEnabledChange));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MaskedImage), new FrameworkPropertyMetadata(typeof(MaskedImage)));
        }

        public static readonly DependencyProperty MaskPixelFormatProperty;

        /// <summary>
        /// Формат пикселей, используемый при формировании маски рисунка
        /// </summary>
        public PixelFormat MaskPixelFormat
        {
            get
            { return (PixelFormat)GetValue(MaskPixelFormatProperty); }
            set
            { SetValue(MaskPixelFormatProperty, value); }
        }

        /// <summary>
        /// Изменение маски рисунка при изменении состояния доступности компонента
        /// </summary>
        private static void IsEnabledChange(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            MaskedImage image = obj as MaskedImage;
            if ((image == null) || (image.Source == null))
                return;

            if ((bool)args.NewValue)
            {
                if ((image.Source as FormatConvertedBitmap) != null)
                    image.Source = ((FormatConvertedBitmap)image.Source).Source;
                image.OpacityMask = null;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage(new Uri(image.Source.ToString()));
                image.Source = new FormatConvertedBitmap(bitmap, image.MaskPixelFormat, null, 100);
                image.OpacityMask = new ImageBrush(bitmap);
            }
        }

        /// <summary>
        /// Проверка корректности устанавливаемого значения PixelFormat
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsPixelFormatValid(object value)
        {
            return (value != null);
        }

        /// <summary>
        /// Установка контраста для растрового изображения
        /// </summary>
        /// <param name="sourceBitmap">Источник</param>
        /// <param name="threshold">Порог</param>
        /// <returns>Растровое изображение</returns>
        public static System.Drawing.Bitmap Contrast(System.Drawing.Bitmap sourceBitmap, int threshold)
        {
            System.Drawing.Imaging.BitmapData sourceData =
                sourceBitmap.LockBits(new System.Drawing.Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            System.Runtime.InteropServices.Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double contrastLevel = Math.Pow((100.0 + threshold) / 100.0, 2);
            double blue = 0;
            double green = 0;
            double red = 0;

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                blue = ((((pixelBuffer[k] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;
                green = ((((pixelBuffer[k + 1] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;
                red = ((((pixelBuffer[k + 2] / 255.0) - 0.5) * contrastLevel) + 0.5) * 255.0;

                if (blue > 255)
                    blue = 255;
                else if (blue < 0)
                    blue = 0;

                if (green > 255)
                    green = 255;
                else if (green < 0)
                    green = 0;

                if (red > 255)
                    red = 255;
                else if (red < 0)
                    red = 0;

                pixelBuffer[k] = (byte)blue;
                pixelBuffer[k + 1] = (byte)green;
                pixelBuffer[k + 2] = (byte)red;
            }


            System.Drawing.Bitmap resultBitmap = new System.Drawing.Bitmap(sourceBitmap.Width, sourceBitmap.Height);
            System.Drawing.Imaging.BitmapData resultData =
                resultBitmap.LockBits(new System.Drawing.Rectangle(0, 0, resultBitmap.Width, resultBitmap.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            System.Runtime.InteropServices.Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        /// <summary>
        /// Перевод источника растрового изображения в растровое изображение 2
        /// </summary>
        /// <param name="srs">Источник</param>
        /// <returns>Растровое изображение</returns>
        public static System.Drawing.Bitmap BitmapSourceToBitmap2(BitmapSource srs)
        {
            int width = srs.PixelWidth;
            int height = srs.PixelHeight;
            int stride = width * ((srs.Format.BitsPerPixel + 7) / 8);
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = System.Runtime.InteropServices.Marshal.AllocHGlobal(height * stride);
                srs.CopyPixels(new Int32Rect(0, 0, width, height), ptr, height * stride, stride);
                using (var btm = new System.Drawing.Bitmap(width, height, stride, System.Drawing.Imaging.PixelFormat.Format1bppIndexed, ptr))
                    return new System.Drawing.Bitmap(btm);
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                    System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
