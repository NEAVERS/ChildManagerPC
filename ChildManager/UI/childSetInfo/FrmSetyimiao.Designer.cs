namespace ChildManager.UI.childSetInfo
{
    partial class FrmSetyimiao
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvyimiao = new System.Windows.Forms.DataGridView();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_yimiaoName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_pihao = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_guige = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_month = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.dtp_production = new System.Windows.Forms.DateTimePicker();
            this.cbx_jiliang = new System.Windows.Forms.ComboBox();
            this.cbx_factorName = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvyimiao)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvyimiao
            // 
            this.dgvyimiao.AllowUserToAddRows = false;
            this.dgvyimiao.AllowUserToDeleteRows = false;
            this.dgvyimiao.AllowUserToResizeColumns = false;
            this.dgvyimiao.AllowUserToResizeRows = false;
            this.dgvyimiao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvyimiao.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvyimiao.BackgroundColor = System.Drawing.Color.White;
            this.dgvyimiao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvyimiao.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvyimiao.ColumnHeadersHeight = 25;
            this.dgvyimiao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column2});
            this.dgvyimiao.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvyimiao.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvyimiao.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvyimiao.EnableHeadersVisualStyles = false;
            this.dgvyimiao.Location = new System.Drawing.Point(2, 1);
            this.dgvyimiao.MultiSelect = false;
            this.dgvyimiao.Name = "dgvyimiao";
            this.dgvyimiao.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvyimiao.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvyimiao.RowHeadersVisible = false;
            this.dgvyimiao.RowHeadersWidth = 40;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvyimiao.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvyimiao.RowTemplate.Height = 23;
            this.dgvyimiao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvyimiao.Size = new System.Drawing.Size(548, 453);
            this.dgvyimiao.TabIndex = 57;
            this.dgvyimiao.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvyimiao_CellClick);
            // 
            // column1
            // 
            this.column1.HeaderText = "疫苗名称";
            this.column1.MinimumWidth = 10;
            this.column1.Name = "column1";
            this.column1.ReadOnly = true;
            this.column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "生产日期";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "生产厂家";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "批号";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 60;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "规格";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 60;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "剂量单位";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 60;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "接种月份";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 60;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "id";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // txt_yimiaoName
            // 
            this.txt_yimiaoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_yimiaoName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_yimiaoName.Location = new System.Drawing.Point(626, 13);
            this.txt_yimiaoName.Name = "txt_yimiaoName";
            this.txt_yimiaoName.Size = new System.Drawing.Size(152, 23);
            this.txt_yimiaoName.TabIndex = 1;
            this.txt_yimiaoName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(555, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 58;
            this.label12.Text = "疫苗名称:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(554, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 58;
            this.label1.Text = "生产厂家:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(584, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 14);
            this.label2.TabIndex = 58;
            this.label2.Text = "批号:";
            // 
            // txt_pihao
            // 
            this.txt_pihao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_pihao.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_pihao.Location = new System.Drawing.Point(626, 145);
            this.txt_pihao.Name = "txt_pihao";
            this.txt_pihao.Size = new System.Drawing.Size(152, 23);
            this.txt_pihao.TabIndex = 4;
            this.txt_pihao.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(584, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 14);
            this.label3.TabIndex = 58;
            this.label3.Text = "规格:";
            // 
            // txt_guige
            // 
            this.txt_guige.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_guige.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_guige.Location = new System.Drawing.Point(626, 184);
            this.txt_guige.Name = "txt_guige";
            this.txt_guige.Size = new System.Drawing.Size(152, 23);
            this.txt_guige.TabIndex = 5;
            this.txt_guige.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(558, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 58;
            this.label4.Text = "剂量单位:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(558, 269);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 58;
            this.label5.Text = "接种月份:";
            // 
            // txt_month
            // 
            this.txt_month.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_month.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_month.Location = new System.Drawing.Point(630, 266);
            this.txt_month.Name = "txt_month";
            this.txt_month.Size = new System.Drawing.Size(147, 23);
            this.txt_month.TabIndex = 7;
            this.txt_month.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label26.Location = new System.Drawing.Point(555, 64);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(70, 14);
            this.label26.TabIndex = 85;
            this.label26.Text = "生产日期:";
            // 
            // dtp_production
            // 
            this.dtp_production.CalendarFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtp_production.CustomFormat = "yyyy-MM-dd";
            this.dtp_production.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dtp_production.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_production.Location = new System.Drawing.Point(626, 58);
            this.dtp_production.Name = "dtp_production";
            this.dtp_production.Size = new System.Drawing.Size(112, 26);
            this.dtp_production.TabIndex = 2;
            this.dtp_production.Enter += new System.EventHandler(this.all_Enter);
            this.dtp_production.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // cbx_jiliang
            // 
            this.cbx_jiliang.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_jiliang.FormattingEnabled = true;
            this.cbx_jiliang.Items.AddRange(new object[] {
            "支",
            "盒",
            "瓶",
            "粒"});
            this.cbx_jiliang.Location = new System.Drawing.Point(630, 226);
            this.cbx_jiliang.Name = "cbx_jiliang";
            this.cbx_jiliang.Size = new System.Drawing.Size(147, 22);
            this.cbx_jiliang.TabIndex = 6;
            this.cbx_jiliang.Enter += new System.EventHandler(this.all_Enter);
            this.cbx_jiliang.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // cbx_factorName
            // 
            this.cbx_factorName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbx_factorName.FormattingEnabled = true;
            this.cbx_factorName.Items.AddRange(new object[] {
            "重庆制药厂"});
            this.cbx_factorName.Location = new System.Drawing.Point(626, 100);
            this.cbx_factorName.Name = "cbx_factorName";
            this.cbx_factorName.Size = new System.Drawing.Size(152, 22);
            this.cbx_factorName.TabIndex = 3;
            this.cbx_factorName.Enter += new System.EventHandler(this.all_Enter);
            this.cbx_factorName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.all_KeyPress);
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(599, 370);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 89;
            this.btnDelete.Text = "删  除 ";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(685, 314);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 88;
            this.btnSave.Text = "保  存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(597, 314);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 87;
            this.btnAdd.Text = "添  加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(685, 370);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 89;
            this.btnClose.Text = "退  出";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmSetyimiao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(788, 456);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbx_factorName);
            this.Controls.Add(this.cbx_jiliang);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.dtp_production);
            this.Controls.Add(this.txt_guige);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_pihao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_month);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_yimiaoName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dgvyimiao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSetyimiao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "疫苗设置";
            this.Load += new System.EventHandler(this.FrmSetyimiao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvyimiao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvyimiao;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        public System.Windows.Forms.TextBox txt_yimiaoName;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txt_pihao;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txt_guige;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txt_month;
        public System.Windows.Forms.Label label26;
        public System.Windows.Forms.DateTimePicker dtp_production;
        public System.Windows.Forms.ComboBox cbx_jiliang;
        public System.Windows.Forms.ComboBox cbx_factorName;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
    }
}