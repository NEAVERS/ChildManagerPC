namespace ChildManager.UI
{
    partial class FrmConfig
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
            this.pre_max = new System.Windows.Forms.TextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pre_max
            // 
            this.pre_max.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pre_max.Location = new System.Drawing.Point(108, 30);
            this.pre_max.Name = "pre_max";
            this.pre_max.Size = new System.Drawing.Size(148, 23);
            this.pre_max.TabIndex = 1;
            this.pre_max.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label46.Location = new System.Drawing.Point(36, 32);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(70, 14);
            this.label46.TabIndex = 87;
            this.label46.Text = "预约数量:";
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOk.Location = new System.Drawing.Point(50, 78);
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
            this.btnclose.Location = new System.Drawing.Point(163, 78);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(73, 23);
            this.btnclose.TabIndex = 5;
            this.btnclose.Text = "取 消";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(299, 136);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pre_max);
            this.Controls.Add(this.label46);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmConfig";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "个人设置";
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox pre_max;
        public System.Windows.Forms.Label label46;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnclose;
    }
}