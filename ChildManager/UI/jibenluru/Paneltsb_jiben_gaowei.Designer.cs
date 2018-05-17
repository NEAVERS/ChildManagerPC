namespace ChildManager.UI.jibenluru
{
    partial class Paneltsb_jiben_gaowei
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Paneltsb_jiben_gaowei));
            this.tvInfo = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.cmsProject = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmscontent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsbupdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbdelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsProject.SuspendLayout();
            this.cmscontent.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvInfo
            // 
            this.tvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvInfo.CheckBoxes = true;
            this.tvInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tvInfo.Location = new System.Drawing.Point(1, 43);
            this.tvInfo.Name = "tvInfo";
            this.tvInfo.Size = new System.Drawing.Size(355, 490);
            this.tvInfo.TabIndex = 104;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 27);
            this.button1.TabIndex = 106;
            this.button1.Text = "导入并返回";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmsProject
            // 
            this.cmsProject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddProjectToolStripMenuItem});
            this.cmsProject.Name = "contextMenuStripRootNode";
            this.cmsProject.Size = new System.Drawing.Size(101, 26);
            // 
            // AddProjectToolStripMenuItem
            // 
            this.AddProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AddProjectToolStripMenuItem.Image")));
            this.AddProjectToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddProjectToolStripMenuItem.Name = "AddProjectToolStripMenuItem";
            this.AddProjectToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.AddProjectToolStripMenuItem.Text = "添加";
            this.AddProjectToolStripMenuItem.Click += new System.EventHandler(this.AddProjectToolStripMenuItem_Click);
            // 
            // cmscontent
            // 
            this.cmscontent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbupdate,
            this.toolStripSeparator4,
            this.tsbdelete,
            this.toolStripSeparator6});
            this.cmscontent.Name = "contextMenuStripRootNode";
            this.cmscontent.Size = new System.Drawing.Size(101, 60);
            // 
            // tsbupdate
            // 
            this.tsbupdate.Image = ((System.Drawing.Image)(resources.GetObject("tsbupdate.Image")));
            this.tsbupdate.Name = "tsbupdate";
            this.tsbupdate.Size = new System.Drawing.Size(152, 22);
            this.tsbupdate.Text = "修改";
            this.tsbupdate.Click += new System.EventHandler(this.tsbupdate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // tsbdelete
            // 
            this.tsbdelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbdelete.Image")));
            this.tsbdelete.Name = "tsbdelete";
            this.tsbdelete.Size = new System.Drawing.Size(152, 22);
            this.tsbdelete.Text = "删除";
            this.tsbdelete.Click += new System.EventHandler(this.tsbdelete_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // Paneltsb_jiben_gaowei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(361, 534);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tvInfo);
            this.MaximizeBox = false;
            this.Name = "Paneltsb_jiben_gaowei";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择高危并配置";
            this.Load += new System.EventHandler(this.Paneltsb_jiben_gaowei_Load);
            this.cmsProject.ResumeLayout(false);
            this.cmscontent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip cmsProject;
        private System.Windows.Forms.ToolStripMenuItem AddProjectToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmscontent;
        private System.Windows.Forms.ToolStripMenuItem tsbupdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsbdelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    }
}