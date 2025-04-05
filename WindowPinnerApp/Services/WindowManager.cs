using System.Runtime.InteropServices;
using System.Diagnostics;


namespace WindowPinnerApp.Services
{
    internal static class WindowManager
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const uint TOPMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE;

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point p);


        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);



        private static readonly List<IntPtr> pinnedWindows = new(); //To track pinned windows

        //PIN_WINDOW_UNDER_CURSOR
        internal static void PinWindowUnderCursor()
        {
            System.Drawing.Point cursorPos = Cursor.Position;
            IntPtr hWnd = WindowFromPoint(cursorPos);

            if (hWnd != IntPtr.Zero)
            {
                bool success = SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                //MessageBox.Show(success ? "Window pinned" : "Failed to pin window");

                if (success && !pinnedWindows.Contains(hWnd)) 
                {
                    pinnedWindows.Add(hWnd);
                }
                Console.WriteLine("RIGHT CLICK DETECTED");
            }
        }

        //UNPIN_WINDOW_UNDER_CURSOR
         internal static void UnpinWindowUnderCursor()
        {
            System.Drawing.Point cursorPos = Cursor.Position;
            IntPtr hWnd = WindowFromPoint(cursorPos);

            if (hWnd != IntPtr.Zero)
            {
                bool success = SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                Debug.WriteLine("LEFT CLICK DETECTED");
                /*MessageBox.Show(success ? "Window unpinned" : "Failed to unpin window");*/
            }

        }

        //UNPIN_ALL_WINDOWS
        internal static void UnpinAllWindows()
        {
            foreach (var hWnd in pinnedWindows)
            {
                SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            }

            pinnedWindows.Clear(); // Reset list
        }



    }
}
