namespace ChildManager.UI.tongji
{
    partial class tongji_publicmodel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.dgvDiscourseList = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscourseList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::ChildManager.Properties.Resources.toolstrip04;
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.btnQuery);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1014, 37);
            this.panel2.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(797, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 30);
            this.button3.TabIndex = 85;
            this.button3.Text = "导出excel";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuery.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnQuery.Location = new System.Drawing.Point(716, 3);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 30);
            this.btnQuery.TabIndex = 24;
            this.btnQuery.Text = "统计";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // dgvDiscourseList
            // 
            this.dgvDiscourseList.AllowUserToAddRows = false;
            this.dgvDiscourseList.AllowUserToDeleteRows = false;
            this.dgvDiscourseList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.dgvDiscourseList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDiscourseList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiscourseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDiscourseList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDiscourseList.Location = new System.Drawing.Point(0, 37);
            this.dgvDiscourseList.MultiSelect = false;
            this.dgvDiscourseList.Name = "dgvDiscourseList";
            this.dgvDiscourseList.ReadOnly = true;
            this.dgvDiscourseList.RowTemplate.Height = 23;
            this.dgvDiscourseList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDiscourseList.Size = new System.Drawing.Size(1014, 487);
            this.dgvDiscourseList.TabIndex = 1;
            // 
            // tongji_publicmodel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvDiscourseList);
            this.Controls.Add(this.panel2);
            this.Name = "tongji_publicmodel";
            this.Size = new System.Drawing.Size(1014, 524);
            this.Load += new System.EventHandler(this.tongji_putong_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiscourseList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dgvDiscourseList;
    }
}
