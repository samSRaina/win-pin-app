using System.Runtime.InteropServices;
using System.Diagnostics;




namespace WindowPinnerApp.WinAPI
{
    internal partial class WindowManager
    {
        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point p);


        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);



        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const uint TOPMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE;


        //PIN_WINDOW_UNDER_CURSOR
         internal static void PinWindowUnderCursor()
        {
            System.Drawing.Point cursorPos = Cursor.Position;
            IntPtr hWnd = WindowFromPoint(cursorPos);

            if (hWnd != IntPtr.Zero)
            {
                bool success = SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                //MessageBox.Show(success ? "Window pinned" : "Failed to pin window");
                Console.WriteLine("RIGHT CLICK DETECTED");
            }
        }

        //UNPIN_WINDOW_UNDER_CURSOR
         internal static void UnPinWindowUnderCursor()
        {
            System.Drawing.Point cursorPos = Cursor.Position;
            IntPtr hWnd = WindowFromPoint(cursorPos);

            if (hWnd != IntPtr.Zero)
            {
                bool success = SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                MessageBox.Show(success ? "Window unpinned" : "Failed to unpin window");
            }

        }

        //



        
    }
}
