using System.Runtime.InteropServices;
using System.Diagnostics;

using WindowPinnerApp.WinAPI;




namespace WindowPinnerApp
{
    public partial class Form1 : Form
    {
        
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
                WindowManager.PinWindowUnderCursor();
            }
        }
    }
}
