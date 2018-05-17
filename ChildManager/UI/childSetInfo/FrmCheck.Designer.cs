namespace ChildManager.UI.childSetInfo
{
    partial class FrmCheck
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
            this.txt_diagnose = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_nuruzhidao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_diagnose
            // 
            this.txt_diagnose.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_diagnose.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_diagnose.Location = new System.Drawing.Point(88, 85);
            this.txt_diagnose.Multiline = true;
            this.txt_diagnose.Name = "txt_diagnose";
            this.txt_diagnose.Size = new System.Drawing.Size(334, 65);
            this.txt_diagnose.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(12, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 62;
            this.label3.Text = "体检诊断:";
            // 
            // txt_nuruzhidao
            // 
            this.txt_nuruzhidao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_nuruzhidao.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_nuruzhidao.Location = new System.Drawing.Point(88, 12);
            this.txt_nuruzhidao.Multiline = true;
            this.txt_nuruzhidao.Name = "txt_nuruzhidao";
            this.txt_nuruzhidao.Size = new System.Drawing.Size(334, 58);
            this.txt_nuruzhidao.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 61;
            this.label2.Text = "营养指导:";
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(220, 165);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 92;
            this.btnClose.Text = "退  出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(122, 165);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 91;
            this.btnSave.Text = "保  存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(434, 211);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txt_diagnose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_nuruzhidao);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改";
            this.Load += new System.EventHandler(this.FrmCheck_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txt_diagnose;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txt_nuruzhidao;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
    }
}