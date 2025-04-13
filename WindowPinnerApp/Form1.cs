
using System.Diagnostics;
using WindowPinnerApp.Services;
using WindowPinnerApp.UI;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;




namespace WindowPinnerApp
{
    public partial class Form1 : Form
    {

        private ToolbarManager toolbarManager;
        private HotkeyManager hotkeyManager;

        public Form1()
        {
            InitializeComponent();
        }

        /*private void Form1_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Debug.WriteLine("Right mouse button clicked");
                WindowManager.PinWindowUnderCursor();
            }
        }

        private void Form1_LeftClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Debug.WriteLine("Right mouse button clicked");
                WindowManager.UnpinWindowUnderCursor();
            }
        }*/


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            toolbarManager = new ToolbarManager(this);
            hotkeyManager = new HotkeyManager(this);
            //MouseHook.start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            hotkeyManager?.Dispose();
            toolbarManager?.Dispose();
            WindowManager.UnpinAllWindows();
            //MouseHook.stop();

            base.OnFormClosed(e);
        }
        protected override void WndProc(ref Message m)
        {
            hotkeyManager?.HandleHotkey(m);
            base.WndProc(ref m);
        }

        private void btnUnpnAll_Click(object sender, EventArgs e)
        {
            WindowManager.UnpinAllWindows();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide(); // Minimize to tray
            }
            else
            {
                toolbarManager?.Dispose(); // Proper cleanup
            }
            base.OnFormClosing(e);
        }

        private void ListWindowsButton_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); // assuming you added a ListBox named listBox1

            WinLister.EnumWindows((hWnd, lParam) =>
            {
                if (!WinLister.IsWindowVisible(hWnd)) return true;

                int length = WinLister.GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length + 1);
                WinLister.GetWindowText(hWnd, builder, builder.Capacity);

                string title = builder.ToString();
                if (!string.IsNullOrWhiteSpace(title))
                {
                    listBox1.Items.Add(title); // OR Console.WriteLine(title);
                }

                return true;
            }, IntPtr.Zero);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
