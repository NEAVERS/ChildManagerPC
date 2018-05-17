namespace ChildManager.UI.jibenluru
{
    partial class Paneltsb_jiben_gaowei_projectadd
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
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxX1.Location = new System.Drawing.Point(74, 31);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(247, 23);
            this.textBoxX1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "高危项:";
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(197, 69);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(58, 30);
            this.btnSave.TabIndex = 108;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnclose
            // 
            this.btnclose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnclose.Location = new System.Drawing.Point(261, 69);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(58, 30);
            this.btnclose.TabIndex = 107;
            this.btnclose.Text = "取消";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // Paneltsb_jiben_gaowei_projectadd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(331, 121);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.label1);
            this.Name = "Paneltsb_jiben_gaowei_projectadd";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加项目";
            this.Load += new System.EventHandler(this.Paneltsb_jiben_gaowei_projectadd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnclose;
    }
}