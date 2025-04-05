
using System.Diagnostics;
using WindowPinnerApp.Services;




namespace WindowPinnerApp
{
    public partial class Form1 : Form
    {

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


        //This makes sure 
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            MouseHook.start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            WindowManager.UnpinAllWindows();
            MouseHook.stop();

            base.OnFormClosed(e);
        }

        private void btnUnpnAll_Click(object sender, EventArgs e)
        {
            WindowManager.UnpinAllWindows();
        }
    }
}
