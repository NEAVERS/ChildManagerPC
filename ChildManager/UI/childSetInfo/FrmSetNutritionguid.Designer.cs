namespace ChildManager.UI.childSetInfo
{
    partial class FrmSetNutritionguid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvNutrition = new System.Windows.Forms.DataGridView();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txt_shiyingxing = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txt_big = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNutrition)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvNutrition
            // 
            this.dgvNutrition.AllowUserToAddRows = false;
            this.dgvNutrition.AllowUserToDeleteRows = false;
            this.dgvNutrition.AllowUserToResizeColumns = false;
            this.dgvNutrition.AllowUserToResizeRows = false;
            this.dgvNutrition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvNutrition.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvNutrition.BackgroundColor = System.Drawing.Color.White;
            this.dgvNutrition.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNutrition.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvNutrition.ColumnHeadersHeight = 25;
            this.dgvNutrition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.Column2});
            this.dgvNutrition.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNutrition.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvNutrition.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvNutrition.EnableHeadersVisualStyles = false;
            this.dgvNutrition.Location = new System.Drawing.Point(-1, 0);
            this.dgvNutrition.MultiSelect = false;
            this.dgvNutrition.Name = "dgvNutrition";
            this.dgvNutrition.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("宋体", 9F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNutrition.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvNutrition.RowHeadersVisible = false;
            this.dgvNutrition.RowHeadersWidth = 40;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNutrition.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvNutrition.RowTemplate.Height = 23;
            this.dgvNutrition.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNutrition.Size = new System.Drawing.Size(109, 453);
            this.dgvNutrition.TabIndex = 56;
            this.dgvNutrition.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNutrition_CellClick);
            // 
            // column1
            // 
            this.column1.HeaderText = "年龄段";
            this.column1.MinimumWidth = 10;
            this.column1.Name = "column1";
            this.column1.ReadOnly = true;
            this.column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "id";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabControl1.Location = new System.Drawing.Point(110, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(609, 414);
            this.tabControl1.TabIndex = 57;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage1.Controls.Add(this.txt_shiyingxing);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(601, 388);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "适应性及语言";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txt_shiyingxing
            // 
            this.txt_shiyingxing.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_shiyingxing.Location = new System.Drawing.Point(2, 3);
            this.txt_shiyingxing.Multiline = true;
            this.txt_shiyingxing.Name = "txt_shiyingxing";
            this.txt_shiyingxing.Size = new System.Drawing.Size(598, 379);
            this.txt_shiyingxing.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabPage2.Controls.Add(this.txt_big);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(601, 388);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "大动作";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txt_big
            // 
            this.txt_big.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_big.Location = new System.Drawing.Point(4, 2);
            this.txt_big.Multiline = true;
            this.txt_big.Name = "txt_big";
            this.txt_big.Size = new System.Drawing.Size(598, 380);
            this.txt_big.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(292, 416);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 58;
            this.btnSave.Text = "保  存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(496, 416);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 58;
            this.btnDelete.Text = "删  除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(395, 416);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 30);
            this.btnClose.TabIndex = 58;
            this.btnClose.Text = "关  闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmSetNutritionguid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(720, 450);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dgvNutrition);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmSetNutritionguid";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "营养指导设置";
            this.Load += new System.EventHandler(this.FrmSetNutritionguid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNutrition)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvNutrition;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txt_shiyingxing;
        private System.Windows.Forms.TextBox txt_big;
    }
}