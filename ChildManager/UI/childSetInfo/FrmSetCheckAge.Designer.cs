namespace ChildManager.UI.childSetInfo
{
    partial class FrmSetCheckAge
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSetCheckAgeList = new System.Windows.Forms.DataGridView();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNoCheck = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetCheckAgeList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSetCheckAgeList
            // 
            this.dgvSetCheckAgeList.AllowUserToAddRows = false;
            this.dgvSetCheckAgeList.AllowUserToDeleteRows = false;
            this.dgvSetCheckAgeList.AllowUserToResizeColumns = false;
            this.dgvSetCheckAgeList.AllowUserToResizeRows = false;
            this.dgvSetCheckAgeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvSetCheckAgeList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvSetCheckAgeList.BackgroundColor = System.Drawing.Color.White;
            this.dgvSetCheckAgeList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSetCheckAgeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSetCheckAgeList.ColumnHeadersHeight = 35;
            this.dgvSetCheckAgeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.Column11,
            this.Column10,
            this.Column2});
            this.dgvSetCheckAgeList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSetCheckAgeList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSetCheckAgeList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSetCheckAgeList.EnableHeadersVisualStyles = false;
            this.dgvSetCheckAgeList.Location = new System.Drawing.Point(1, 0);
            this.dgvSetCheckAgeList.MultiSelect = false;
            this.dgvSetCheckAgeList.Name = "dgvSetCheckAgeList";
            this.dgvSetCheckAgeList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSetCheckAgeList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSetCheckAgeList.RowHeadersVisible = false;
            this.dgvSetCheckAgeList.RowHeadersWidth = 40;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSetCheckAgeList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSetCheckAgeList.RowTemplate.Height = 23;
            this.dgvSetCheckAgeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSetCheckAgeList.Size = new System.Drawing.Size(305, 510);
            this.dgvSetCheckAgeList.TabIndex = 55;
            this.dgvSetCheckAgeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSetCheckAgeList_CellClick);
            // 
            // column1
            // 
            this.column1.HeaderText = "序号";
            this.column1.MinimumWidth = 10;
            this.column1.Name = "column1";
            this.column1.ReadOnly = true;
            this.column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.column1.Width = 60;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "体检月份";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 120;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "是否体检";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 120;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "id";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(312, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 56;
            this.button1.Text = "退  出";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNoCheck
            // 
            this.btnNoCheck.BackColor = System.Drawing.Color.White;
            this.btnNoCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNoCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNoCheck.Location = new System.Drawing.Point(312, 210);
            this.btnNoCheck.Name = "btnNoCheck";
            this.btnNoCheck.Size = new System.Drawing.Size(75, 30);
            this.btnNoCheck.TabIndex = 56;
            this.btnNoCheck.Text = "不体检";
            this.btnNoCheck.UseVisualStyleBackColor = false;
            this.btnNoCheck.Click += new System.EventHandler(this.btnNoCheck_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.White;
            this.btnCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCheck.Location = new System.Drawing.Point(312, 163);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(75, 30);
            this.btnCheck.TabIndex = 56;
            this.btnCheck.Text = "体  检";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // FrmSetCheckAge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(393, 514);
            this.Controls.Add(this.dgvSetCheckAgeList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnNoCheck);
            this.Controls.Add(this.btnCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmSetCheckAge";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置体检年龄";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSetCheckAge_FormClosing);
            this.Load += new System.EventHandler(this.FrmSetCheckAge_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetCheckAgeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvSetCheckAgeList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnNoCheck;
        private System.Windows.Forms.Button btnCheck;
    }
}