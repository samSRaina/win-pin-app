using System;
using System.Diagnostics;
using System.Runtime.InteropServices;



namespace WindowPinnerApp.Services
{
    internal static class MouseHook
    {
       


        private const int WH_MOUSE_LL = 14;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_LBUTTONDOWN = 0x0201;

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookId = IntPtr.Zero;


        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        public static void start()
        {
            _hookId = SetHook(_proc);
        }

        public static void stop()
        {
            UnhookWindowsHookEx(_hookId);
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
                    using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /*private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_RBUTTONDOWN)
            {
                WindowManager.PinWindowUnderCursor(); // Your pin logic
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }*/

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int wmMouse = wParam.ToInt32();

                if (wmMouse == WM_RBUTTONDOWN)
                {
                    Debug.WriteLine("Right-click detected: Pinning window.");
                    WindowManager.PinWindowUnderCursor();
                }
                else if (wmMouse == WM_LBUTTONDOWN)
                {
                    Debug.WriteLine("Left-click detected: Unpinning window.");
                    WindowManager.UnpinWindowUnderCursor();
                }
            }

            return CallNextHookEx(_hookId, nCode, wParam, lParam);
        }



    }
}