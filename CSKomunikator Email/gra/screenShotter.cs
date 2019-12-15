using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace gra
{
    static partial class Program
    {
        [DllImport("User32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("gdi32.dll", EntryPoint = "BitBlt", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt([In] IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, [In] IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rectangle lpRect);
        public static void WNDtoPNG(IntPtr hwnd, string fName)
        {
            IntPtr wndDC = GetDC(hwnd);
            Graphics g1 = Graphics.FromHdc(wndDC);
            Rectangle rect;
            GetWindowRect(hwnd, out rect);
            int x = rect.Width - rect.X, y = rect.Height - rect.Y;
            Image myImage = new Bitmap(x, y, g1);
            Graphics g2 = Graphics.FromImage(myImage);
            IntPtr dc1 = g1.GetHdc();
            IntPtr dc2 = g2.GetHdc();
            BitBlt(dc2, 0, 0, x, y, dc1, 0, 0, 13369376);
            g1.ReleaseHdc(dc1);
            g2.ReleaseHdc(dc2);
            myImage.Save(fName, ImageFormat.Png);
        }
        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();
        public static void ScreenToPNG(string fName)
        {
            WNDtoPNG(GetDesktopWindow(), fName);
        }

    }
}
