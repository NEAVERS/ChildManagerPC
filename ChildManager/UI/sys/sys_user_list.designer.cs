using System.Windows.Forms;
namespace ChildManager.UI.sys
{
    partial class sys_user_list
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnInput = new System.Windows.Forms.Button();
            this.dgvConditionTreatRecordList = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDisposalItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditionTreatRecordList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::ChildManager.Properties.Resources.toolstrip04;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button6);
            this.panel2.Controls.Add(this.button5);
            this.panel2.Controls.Add(this.comboBox2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnInput);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1344, 42);
            this.panel2.TabIndex = 13;
            // 
            // button6
            // 
            this.button6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button6.Location = new System.Drawing.Point(1132, 2);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(83, 35);
            this.button6.TabIndex = 65;
            this.button6.Text = "签名管理";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button5.Location = new System.Drawing.Point(539, 4);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(83, 35);
            this.button5.TabIndex = 64;
            this.button5.Text = "查询";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownWidth = 100;
            this.comboBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(400, 6);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(104, 25);
            this.comboBox2.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(337, 11);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 62;
            this.label7.Text = "角色:";
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(225, 4);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(103, 30);
            this.textBox2.TabIndex = 53;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(175, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 52;
            this.label1.Text = "账户:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(56, 5);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(103, 30);
            this.textBox1.TabIndex = 51;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(5, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 50;
            this.label3.Text = "姓名:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1004, 2);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 35);
            this.button4.TabIndex = 4;
            this.button4.Text = "批量修改角色";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(909, 2);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 35);
            this.button3.TabIndex = 3;
            this.button3.Text = "重置密码";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(815, 2);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 35);
            this.button2.TabIndex = 2;
            this.button2.Text = "删除用户";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(723, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "修改用户";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInput
            // 
            this.btnInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInput.Location = new System.Drawing.Point(629, 2);
            this.btnInput.Margin = new System.Windows.Forms.Padding(4);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(83, 35);
            this.btnInput.TabIndex = 0;
            this.btnInput.Text = "添加用户";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // dgvConditionTreatRecordList
            // 
            this.dgvConditionTreatRecordList.AllowUserToAddRows = false;
            this.dgvConditionTreatRecordList.AllowUserToDeleteRows = false;
            this.dgvConditionTreatRecordList.AllowUserToResizeColumns = false;
            this.dgvConditionTreatRecordList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.dgvConditionTreatRecordList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvConditionTreatRecordList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvConditionTreatRecordList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.dgvConditionTreatRecordList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(104)))), ((int)(((byte)(152)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConditionTreatRecordList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvConditionTreatRecordList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.colDisposalItem,
            this.colContent,
            this.colCreator,
            this.Column4,
            this.Column3,
            this.colCreateTime,
            this.Column1,
            this.Column2});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(104)))), ((int)(((byte)(152)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConditionTreatRecordList.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvConditionTreatRecordList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConditionTreatRecordList.EnableHeadersVisualStyles = false;
            this.dgvConditionTreatRecordList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.dgvConditionTreatRecordList.Location = new System.Drawing.Point(0, 42);
            this.dgvConditionTreatRecordList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvConditionTreatRecordList.Name = "dgvConditionTreatRecordList";
            this.dgvConditionTreatRecordList.RowHeadersVisible = false;
            this.dgvConditionTreatRecordList.RowTemplate.Height = 23;
            this.dgvConditionTreatRecordList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConditionTreatRecordList.Size = new System.Drawing.Size(1344, 552);
            this.dgvConditionTreatRecordList.TabIndex = 14;
            this.dgvConditionTreatRecordList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConditionTreatRecordList_CellDoubleClick_1);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column6.Width = 30;
            // 
            // colDisposalItem
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.colDisposalItem.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDisposalItem.HeaderText = "序号";
            this.colDisposalItem.Name = "colDisposalItem";
            this.colDisposalItem.ReadOnly = true;
            this.colDisposalItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDisposalItem.Width = 40;
            // 
            // colContent
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.colContent.DefaultCellStyle = dataGridViewCellStyle4;
            this.colContent.HeaderText = "账户";
            this.colContent.Name = "colContent";
            this.colContent.ReadOnly = true;
            this.colContent.Width = 80;
            // 
            // colCreator
            // 
            this.colCreator.HeaderText = "姓名";
            this.colCreator.Name = "colCreator";
            this.colCreator.ReadOnly = true;
            this.colCreator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCreator.Width = 80;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "科室名称";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "科室代码";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // colCreateTime
            // 
            this.colCreateTime.HeaderText = "电话";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.ReadOnly = true;
            this.colCreateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "角色";
            this.Column1.Name = "Column1";
            this.Column1.Width = 115;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "地址";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn2.HeaderText = "账户";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 40;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "姓名";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "电话";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 110;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "权限";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "地址";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 150;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "科室代码";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 60;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "科室名称";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 155;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "职位";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 80;
            // 
            // sys_user_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvConditionTreatRecordList);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "sys_user_list";
            this.Size = new System.Drawing.Size(1344, 594);
            this.Load += new System.EventHandler(this.yonghuliebiao_Load_1);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditionTreatRecordList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Button button3;
        private Button button4;
        public DataGridView dgvConditionTreatRecordList;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private TextBox textBox2;
        private Label label1;
        private TextBox textBox1;
        private Label label3;
        private Label label7;
        private Button button5;
        private DataGridViewCheckBoxColumn Column6;
        private DataGridViewTextBoxColumn colDisposalItem;
        private DataGridViewTextBoxColumn colContent;
        private DataGridViewTextBoxColumn colCreator;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn colCreateTime;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.ComboBox comboBox2;
        private Button button6;
    }
}