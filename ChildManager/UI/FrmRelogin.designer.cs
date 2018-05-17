namespace ChildManager.UI
{
    partial class FrmRelogin
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(108, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 23);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label46.Location = new System.Drawing.Point(49, 32);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(63, 14);
            this.label46.TabIndex = 87;
            this.label46.Text = "用户名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(60, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 14);
            this.label1.TabIndex = 87;
            this.label1.Text = "密码:";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(108, 71);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(148, 23);
            this.textBox2.TabIndex = 2;
            this.textBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(64, 111);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(73, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "登录";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnclose
            // 
            this.btnclose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnclose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnclose.Location = new System.Drawing.Point(160, 111);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(73, 23);
            this.btnclose.TabIndex = 5;
            this.btnclose.Text = "取 消";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // FrmRelogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(296, 157);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label46);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmRelogin";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label label46;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnclose;
    }
}