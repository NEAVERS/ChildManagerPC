namespace ChildManager.UI.tongji
{
    partial class tongji_MainInfo
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlContent;//定义面板
        private System.Windows.Forms.ToolStrip toolStrip2;//定义眉头按钮集合

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tongji_MainInfo));
            this.pnlContent = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlSplitter = new System.Windows.Forms.Panel();
            this.danganhao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Location = new System.Drawing.Point(0, 31);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1014, 589);
            this.pnlContent.TabIndex = 17;
            // 
            // toolStrip2
            // 
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.White;
            this.toolStrip2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip2.BackgroundImage")));
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(1014, 32);
            this.toolStrip2.TabIndex = 18;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 32);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(160, 628);
            this.panel3.TabIndex = 19;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // pnlSplitter
            // 
            this.pnlSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnlSplitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlSplitter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlSplitter.Location = new System.Drawing.Point(150, 7);
            this.pnlSplitter.Name = "pnlSplitter";
            this.pnlSplitter.Size = new System.Drawing.Size(12, 613);
            this.pnlSplitter.TabIndex = 27;
            // 
            // danganhao
            // 
            this.danganhao.Name = "danganhao";
            // 
            // name
            // 
            this.name.Name = "name";
            // 
            // tongji_MainInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlContent);
            this.Name = "tongji_MainInfo";
            this.Size = new System.Drawing.Size(1014, 620);
            this.Load += new System.EventHandler(this.tongji_MainInfo_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn danganhao;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.Panel pnlSplitter;
    }
}
