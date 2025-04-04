using System.Runtime.InteropServices;
using System.Diagnostics;




namespace WindowPinnerApp
{
    public partial class Form1 : Form
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
        private void PinWindowUnderCursor()
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
        private void UnPinWindowUnderCursor()
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



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MouseDown += new MouseEventHandler(Form1_RightClick);
            Debug.WriteLine("MouseDown event handler added");
        }
        private void Form1_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Debug.WriteLine("Right mouse button clicked");
                PinWindowUnderCursor();
            }
        }





    }
}
