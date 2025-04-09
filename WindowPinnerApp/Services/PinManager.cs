using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowPinnerApp.Services
{
    internal class PinManager
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_SHOWWINDOW = 0x0040;


        private List<IntPtr> pinnedWindows = new List<IntPtr>();

        public void TogglePinUnderCursor()
        {
            IntPtr hWnd = GetWindowUnderCursor();
            if(hWnd == IntPtr.Zero)
            {
                Debug.WriteLine("No Window Under Cursor");
                return;
            }

            if(pinnedWindows.Contains(hWnd))
            {
                Debug.WriteLine($" Unpinning Window: { GetWindowTitle(hWnd)}");
                UnpinWindow(hWnd);
                pinnedWindows.Remove(hWnd);
            }
            else
            {
                Debug.WriteLine($" Pinning Window: {GetWindowTitle(hWnd)}");
                pinnedWindows.RemoveAll(h => !IsWindow(h));
                pinnedWindows.Remove(hWnd);
                pinnedWindows.Add(hWnd);
                ApplyZOrder();
            }        
        }

        private void ApplyZOrder()
        {
            for (int i = 0; i < pinnedWindows.Count; i++)
            {
                IntPtr HwND = pinnedWindows[i];
                IntPtr insertAfter = (i == 0) ? HWND_TOPMOST : pinnedWindows[i - 1];
                bool result = SetWindowPos(HwND, insertAfter, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE);

                if(!result)
                {
                    Debug.WriteLine($"Failed to set Z-order for window: {GetWindowTitle(HwND)}");
                }
                else
                {
                    Debug.WriteLine($"Set Z-order for window: {GetWindowTitle(HwND)}");
                }
            }
            Debug.WriteLine($"Pinned order: {string.Join(", ", pinnedWindows.Select(GetWindowTitle))}");

        }


        private IntPtr GetWindowUnderCursor()
        {
            if (!GetCursorPos(out POINT pt))
                return IntPtr.Zero;

            return WindowFromPoint(pt);
        }

        private void UnpinWindow(IntPtr hwnd)
        {
            bool result = SetWindowPos(hwnd, HWND_NOTOPMOST, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
            if (!result)
            {
                Debug.WriteLine($"Failed to unpin window: {GetWindowTitle(hwnd)}");
            }
            else
            {
                Debug.WriteLine($"Unpinned window: {GetWindowTitle(hwnd)}");
            }
        }

        private static string GetWindowTitle(IntPtr hwnd)
        {
            int length = GetWindowTextLength(hwnd);
            if (length == 0) return "[Untitled]";
            StringBuilder sb = new(length + 1);
            GetWindowText(hwnd, sb, sb.Capacity);
            return sb.ToString().Trim();
        }



        [DllImport("user32.dll")]
        private static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(POINT pt);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        private struct POINT
        {
            public int X;
            public int Y;
        }
    }
}