using System;
using System.Drawing;
using System.Windows.Forms;
using WindowPinnerApp.Services;

namespace WindowPinnerApp.UI
{
    internal class ToolbarManager
    {
        private readonly Form _mainForm;
        private NotifyIcon trayIcon;

        private ContextMenuStrip trayMenu;

        public ToolbarManager(Form mainForm)
        {
            _mainForm = mainForm;
            InitializeTray();
        }

        private void InitializeTray()
        {
            trayMenu = new ContextMenuStrip();
            trayMenu.Items.Add("Show Toolbar", null, ShowToolbar);
            trayMenu.Items.Add("Unpin All Windows", null, (s, e) => WindowManager.UnpinAllWindows());
            trayMenu.Items.Add("Exit", null, (s, e) => Application.Exit());

            trayIcon = new NotifyIcon()
            {
                Icon = SystemIcons.Application,
                ContextMenuStrip = trayMenu,
                Visible = true,
                Text = "Window Pinner App"
            };

            trayIcon.DoubleClick += ShowToolbar;
        }

        private void ShowToolbar(object sender, EventArgs e)
        {
            _mainForm.Show();
            _mainForm.WindowState = FormWindowState.Normal;
            _mainForm.Activate();
        }

        public void Dispose()
        {
            trayIcon.Dispose();
        }
    }
}
