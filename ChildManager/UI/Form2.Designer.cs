namespace ChildManager.UI
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.putT = new System.Windows.Forms.TextBox();
            this.putTId = new System.Windows.Forms.TextBox();
            this.fidtxt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(77, 177);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "获取职称记录";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // putT
            // 
            this.putT.Location = new System.Drawing.Point(13, 77);
            this.putT.Multiline = true;
            this.putT.Name = "putT";
            this.putT.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.putT.Size = new System.Drawing.Size(530, 94);
            this.putT.TabIndex = 1;
            // 
            // putTId
            // 
            this.putTId.Location = new System.Drawing.Point(12, 206);
            this.putTId.Multiline = true;
            this.putTId.Name = "putTId";
            this.putTId.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.putTId.Size = new System.Drawing.Size(530, 199);
            this.putTId.TabIndex = 2;
            // 
            // fidtxt
            // 
            this.fidtxt.Location = new System.Drawing.Point(12, 12);
            this.fidtxt.Name = "fidtxt";
            this.fidtxt.Size = new System.Drawing.Size(530, 21);
            this.fidtxt.TabIndex = 3;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 417);
            this.Controls.Add(this.fidtxt);
            this.Controls.Add(this.putTId);
            this.Controls.Add(this.putT);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox putT;
        private System.Windows.Forms.TextBox putTId;
        private System.Windows.Forms.TextBox fidtxt;
    }
}