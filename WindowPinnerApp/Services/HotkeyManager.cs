using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowPinnerApp.Services
{
	internal class HotkeyManager : IDisposable
	{
		private const int WM_HOTKEY = 0x0312;
        private const int HOTKEY_ID_TOGGLE = 9000;

        private readonly IntPtr _wHnd;
        private bool _isPinned = false;

        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_ALT = 0x0001;
        private const uint VK_T = 0x50; //ASCII for P


        [DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);


        public HotkeyManager(Form form)
        {
            _wHnd = form.Handle;

            try
            {
                RegisterHotKeys();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Hotkey registration failed:\n{ex.Message}",
                    "Hotkey Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void RegisterHotKeys()
        {
            bool success = RegisterHotKey(_wHnd, HOTKEY_ID_TOGGLE, MOD_CONTROL | MOD_ALT, VK_T);
            if (!success)
            {
                throw new InvalidOperationException("Failed to register hotkey.");
            }
        }

        private void UnregisterHotKeys()
		{
			try
			{
				UnregisterHotKey(_wHnd, HOTKEY_ID_TOGGLE);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error unregistering hotkey: {ex.Message}");
			}
		}

        public void HandleHotkey(Message m)
        {
            if (m.Msg != WM_HOTKEY) return;

            int id = m.WParam.ToInt32();

            if (id == HOTKEY_ID_TOGGLE)
            {
                try
                {
                    if (_isPinned)
                    {
                        WindowManager.UnpinWindowUnderCursor();
                    }
                    else
                    {
                        WindowManager.PinWindowUnderCursor();
                    }

                    _isPinned = !_isPinned;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hotkey action failed: " + ex.Message);
                }
            }
        }

        public void Dispose()
        {
            UnregisterHotKeys();
        }
    }
}
