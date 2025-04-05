namespace WindowPinnerApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnUnpnAll = new Button();
            SuspendLayout();
            // 
            // btnUnpnAll
            // 
            btnUnpnAll.Location = new Point(43, 35);
            btnUnpnAll.Name = "btnUnpnAll";
            btnUnpnAll.Size = new Size(142, 23);
            btnUnpnAll.TabIndex = 0;
            btnUnpnAll.Text = "Unpin All ";
            btnUnpnAll.UseVisualStyleBackColor = true;
            btnUnpnAll.Click += btnUnpnAll_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 182);
            Controls.Add(btnUnpnAll);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnUnpnAll;
    }
}
