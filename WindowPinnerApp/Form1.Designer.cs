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
            button1 = new Button();
            listBox1 = new ListBox();
            SuspendLayout();
            // 
            // btnUnpnAll
            // 
            btnUnpnAll.Location = new Point(317, 52);
            btnUnpnAll.Name = "btnUnpnAll";
            btnUnpnAll.Size = new Size(142, 23);
            btnUnpnAll.TabIndex = 0;
            btnUnpnAll.Text = "Unpin All ";
            btnUnpnAll.UseVisualStyleBackColor = true;
            btnUnpnAll.Click += btnUnpnAll_Click;
            // 
            // button1
            // 
            button1.Location = new Point(62, 52);
            button1.Name = "button1";
            button1.Size = new Size(108, 23);
            button1.TabIndex = 1;
            button1.Text = "List Windows";
            button1.UseVisualStyleBackColor = true;
            button1.Click += ListWindowsButton_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(62, 99);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(210, 319);
            listBox1.TabIndex = 2;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(501, 530);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Controls.Add(btnUnpnAll);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnUnpnAll;
        private Button button1;
        private ListBox listBox1;
    }
}
