namespace ChildManager.UI.jibenluru
{
    partial class panel_ceping
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.Panel pnlCeping;//定义面板//定义眉头按钮集合

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlCeping = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.danganhao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCeping
            // 
            this.pnlCeping.BackColor = System.Drawing.Color.Transparent;
            this.pnlCeping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCeping.Location = new System.Drawing.Point(184, 0);
            this.pnlCeping.Name = "pnlCeping";
            this.pnlCeping.Size = new System.Drawing.Size(830, 620);
            this.pnlCeping.TabIndex = 17;
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "mrn";
            this.dataGridViewTextBoxColumn1.FillWeight = 65F;
            this.dataGridViewTextBoxColumn1.HeaderText = "档案号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 65;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn2.FillWeight = 110F;
            this.dataGridViewTextBoxColumn2.HeaderText = "孕妇姓名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 110;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "id";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToResizeColumns = false;
            this.dgv1.AllowUserToResizeRows = false;
            this.dgv1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.dgv1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.danganhao});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgv1.GridColor = System.Drawing.Color.LightSteelBlue;
            this.dgv1.Location = new System.Drawing.Point(0, 0);
            this.dgv1.MultiSelect = false;
            this.dgv1.Name = "dgv1";
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.RowTemplate.ReadOnly = true;
            this.dgv1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv1.Size = new System.Drawing.Size(184, 620);
            this.dgv1.TabIndex = 18;
            this.dgv1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellClick);
            this.dgv1.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellEnter);
            this.dgv1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv1_DataBindingComplete);
            // 
            // danganhao
            // 
            this.danganhao.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.danganhao.DataPropertyName = "cepingDisplay";
            this.danganhao.FillWeight = 65F;
            this.danganhao.HeaderText = "检查项目";
            this.danganhao.Name = "danganhao";
            this.danganhao.ReadOnly = true;
            this.danganhao.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // panel_ceping
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.pnlCeping);
            this.Controls.Add(this.dgv1);
            this.Name = "panel_ceping";
            this.Size = new System.Drawing.Size(1014, 620);
            this.Load += new System.EventHandler(this.panel_ceping_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        public System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.DataGridViewTextBoxColumn danganhao;
    }
}