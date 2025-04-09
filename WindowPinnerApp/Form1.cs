
using System.Diagnostics;
using WindowPinnerApp.Services;
using WindowPinnerApp.UI;




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

    }
}
