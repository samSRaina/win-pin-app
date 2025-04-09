using System.Runtime.InteropServices;
using System.Diagnostics;
using System;
using System.Linq;
using System.Drawing;


namespace WindowPinnerApp.Services
{
    internal static class WindowManager
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint TOPMOST_FLAGS = SWP_NOSIZE | SWP_NOMOVE;

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point p);


        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        /*[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
*/

        private static readonly Stack<IntPtr> pinnedWindows = new(); //To track pinned windows

        internal static void PinWindowUnderCursor()
        {
            try
            {
                System.Drawing.Point cursorPos = Cursor.Position;
                IntPtr hWnd = WindowFromPoint(cursorPos);

                if (hWnd != IntPtr.Zero && !pinnedWindows.Contains(hWnd))
                {
                    bool success = SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                    //Debug.WriteLine(success ? "Window pinned" : "Failed to pin window");
                    if (success)
                    {
                        pinnedWindows.Push(hWnd);
                        RefreshWindowOrder();
                        Debug.WriteLine("Window Pinned");
                    }
                    else
                    {
                        Debug.WriteLine("Failed to PinWindow");
                    }

                }
            } catch(Exception ex) {
                Debug.WriteLine($"Error in PinWindowUnderCursor: {ex.Message}");
            }
            
        }

        internal static void UnpinWindowUnderCursor()
        {
            Point cursorPos = Cursor.Position;
            IntPtr hWnd = WindowFromPoint(cursorPos);

            if (hWnd != IntPtr.Zero && pinnedWindows.Contains(hWnd))
            {
                SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

                // Remove specific window by rebuilding the stack
                var updatedStack = new Stack<IntPtr>(pinnedWindows.Reverse().Where(w => w != hWnd));
                pinnedWindows.Clear();
                foreach (var win in updatedStack.Reverse()) pinnedWindows.Push(win);

                RefreshWindowOrder();
                Debug.WriteLine("Unpinned window");
            }
        }


        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        internal static void UnpinAllWindows()
        {
            Console.WriteLine("Unpinning all windows...");
            EnumWindows((hWnd, lParam) =>
            {
                SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                return true;
            }, IntPtr.Zero);

            Console.WriteLine("Unpinned all windows!");
        }



        internal static void ToggleWindowPinUnderCursor()
        {
            try
            {
                Point cursorPos = Cursor.Position;
                IntPtr hWnd = WindowFromPoint(cursorPos);

                if (hWnd == IntPtr.Zero)
                {
                    Debug.WriteLine("No window found under cursor");
                    return;
                }
                
               
                    if (pinnedWindows.Contains(hWnd))
                    {
                        // Unpin
                        SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);

                        // Remove from stack
                        var updatedStack = new Stack<IntPtr>(pinnedWindows.Reverse().Where(w => w != hWnd));
                        pinnedWindows.Clear();
                        foreach (var win in updatedStack.Reverse()) pinnedWindows.Push(win);

                        RefreshWindowOrder();
                        Debug.WriteLine("Unpinned window");
                    }
                    else
                    {
                        // Pin
                        bool success = SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
                        if (success)
                        {
                            pinnedWindows.Push(hWnd);
                            RefreshWindowOrder();
                            Debug.WriteLine("Pinned new window");
                        }
                        else
                        {
                            Debug.WriteLine("Failed to pin new window");
                        }
                    }
                

                
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ToggleWindowPinUnderCursor error: {ex.Message}");
            }
        }


        private static void RefreshWindowOrder()
        {
            foreach (var hWnd in pinnedWindows.Reverse())
            {
                SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            }
        }



    }
}
