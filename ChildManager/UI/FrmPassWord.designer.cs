namespace ChildManager.UI
{
    partial class FrmPassWord
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
            this.txt_oldPwd = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_newPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_okNewPwd = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_oldPwd
            // 
            this.txt_oldPwd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_oldPwd.Location = new System.Drawing.Point(108, 30);
            this.txt_oldPwd.Name = "txt_oldPwd";
            this.txt_oldPwd.PasswordChar = '*';
            this.txt_oldPwd.Size = new System.Drawing.Size(148, 23);
            this.txt_oldPwd.TabIndex = 1;
            this.txt_oldPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label46.Location = new System.Drawing.Point(49, 32);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(56, 14);
            this.label46.TabIndex = 87;
            this.label46.Text = "原密码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(49, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 87;
            this.label1.Text = "新密码:";
            // 
            // txt_newPwd
            // 
            this.txt_newPwd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_newPwd.Location = new System.Drawing.Point(108, 71);
            this.txt_newPwd.Name = "txt_newPwd";
            this.txt_newPwd.PasswordChar = '*';
            this.txt_newPwd.Size = new System.Drawing.Size(148, 23);
            this.txt_newPwd.TabIndex = 2;
            this.txt_newPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(37, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 87;
            this.label2.Text = "确认密码:";
            // 
            // txt_okNewPwd
            // 
            this.txt_okNewPwd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_okNewPwd.Location = new System.Drawing.Point(108, 111);
            this.txt_okNewPwd.Name = "txt_okNewPwd";
            this.txt_okNewPwd.PasswordChar = '*';
            this.txt_okNewPwd.Size = new System.Drawing.Size(148, 23);
            this.txt_okNewPwd.TabIndex = 3;
            this.txt_okNewPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(52, 159);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(73, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确 定";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnclose
            // 
            this.btnclose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnclose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnclose.Location = new System.Drawing.Point(165, 159);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(73, 23);
            this.btnclose.TabIndex = 5;
            this.btnclose.Text = "取 消";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // FrmPassWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(296, 213);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txt_okNewPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_newPwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_oldPwd);
            this.Controls.Add(this.label46);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPassWord";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改密码";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txt_oldPwd;
        public System.Windows.Forms.Label label46;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txt_newPwd;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txt_okNewPwd;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnclose;
    }
}