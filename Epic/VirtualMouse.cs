using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;

namespace Epic
{
    public static class VirtualMouse
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }
        //public static void MoveTo(int x, int y)
        //{
        //    float min = 0;
        //    float max = 65535;

        //    int mappedX = (int)Remap(x, 0.0f, 2880.0f, min, max);
        //    int mappedY = (int)Remap(y, 0.0f, 1800.0f, min, max);

        //    mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, mappedX, mappedY, 0, 0);
        //}
        public static (float, float) map(float x, float y)
        {
            float dpiX, dpiY;
            using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
            {
                dpiX = graphics.DpiX;
                dpiY = graphics.DpiY;
            }

            var mappedX = (x - 0.0f) / (SystemParameters.PrimaryScreenWidth / 96 * dpiX - 0.0f) * (ushort.MaxValue - ushort.MinValue) + ushort.MinValue;
            var mappedY = (y - 0.0f) / (SystemParameters.PrimaryScreenHeight / 96 * dpiY - 0.0f) * (ushort.MaxValue - ushort.MinValue) + ushort.MinValue;

            return ((float)mappedX, (float)mappedY);
        }
        public static void MoveTo(float x, float y)
        {
            var mapped = map(x, y);
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, (int)mapped.Item1, (int)mapped.Item2, 0, 0);
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, cursorX, cursorY, 0, 0);
        }

        public static void LeftDown(int x, int y)
        {
            var mapped = map(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, (int)mapped.Item1, (int)mapped.Item2, 0, 0);
        }
        public static void LeftUp(int x, int y)
        {
            var mapped = map(x, y);
            mouse_event(MOUSEEVENTF_LEFTUP, (int)mapped.Item1, (int)mapped.Item2, 0, 0);
        }

        public static void LeftClick(int x, int y)
        {
            var mapped = map(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, (int)mapped.Item1, (int)mapped.Item2, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, (int)mapped.Item1, (int)mapped.Item2, 0, 0);
        }

        //public static void LeftUp()
        //{
        //    mouse_event(MOUSEEVENTF_LEFTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        //}

        //public static void RightClick()
        //{
        //    mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        //    mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        //}

        //public static void RightDown()
        //{
        //    mouse_event(MOUSEEVENTF_RIGHTDOWN, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        //}

        //public static void RightUp()
        //{
        //    mouse_event(MOUSEEVENTF_RIGHTUP, System.Windows.Forms.Control.MousePosition.X, System.Windows.Forms.Control.MousePosition.Y, 0, 0);
        //}
    }

}
