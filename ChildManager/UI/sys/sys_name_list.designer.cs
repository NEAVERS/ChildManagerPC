using System.Windows.Forms;
namespace ChildManager.UI.sys
{
    partial class sys_name_list
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtnum = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.btnChange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInput = new System.Windows.Forms.Button();
            this.type = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnFound = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dgvConditionTreatRecordList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisposalItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditionTreatRecordList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::ChildManager.Properties.Resources.toolstrip04;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.txtnum);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(735, 140);
            this.panel2.TabIndex = 13;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(517, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 35);
            this.button1.TabIndex = 19;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtnum
            // 
            this.txtnum.AutoSize = true;
            this.txtnum.BackColor = System.Drawing.Color.Transparent;
            this.txtnum.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.txtnum.ForeColor = System.Drawing.Color.Red;
            this.txtnum.Location = new System.Drawing.Point(651, 102);
            this.txtnum.Name = "txtnum";
            this.txtnum.Size = new System.Drawing.Size(18, 18);
            this.txtnum.TabIndex = 18;
            this.txtnum.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(513, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 18);
            this.label5.TabIndex = 17;
            this.label5.Text = "当前页面行数:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.code);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.name);
            this.panel3.Controls.Add(this.btnChange);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btnInput);
            this.panel3.Controls.Add(this.type);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(486, 79);
            this.panel3.TabIndex = 16;
            // 
            // code
            // 
            this.code.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.code.Location = new System.Drawing.Point(241, 8);
            this.code.Margin = new System.Windows.Forms.Padding(4);
            this.code.Name = "code";
            this.code.Size = new System.Drawing.Size(103, 30);
            this.code.TabIndex = 69;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(190, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 68;
            this.label4.Text = "简拼:";
            // 
            // name
            // 
            this.name.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name.Location = new System.Drawing.Point(60, 8);
            this.name.Margin = new System.Windows.Forms.Padding(4);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(103, 30);
            this.name.TabIndex = 65;
            // 
            // btnChange
            // 
            this.btnChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChange.Location = new System.Drawing.Point(385, 40);
            this.btnChange.Margin = new System.Windows.Forms.Padding(4);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(83, 35);
            this.btnChange.TabIndex = 1;
            this.btnChange.Text = "修改";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 64;
            this.label1.Text = "姓名:";
            // 
            // btnInput
            // 
            this.btnInput.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInput.Location = new System.Drawing.Point(385, 3);
            this.btnInput.Margin = new System.Windows.Forms.Padding(4);
            this.btnInput.Name = "btnInput";
            this.btnInput.Size = new System.Drawing.Size(83, 35);
            this.btnInput.TabIndex = 0;
            this.btnInput.Text = "添加";
            this.btnInput.UseVisualStyleBackColor = true;
            this.btnInput.Click += new System.EventHandler(this.btnInput_Click);
            // 
            // type
            // 
            this.type.DropDownWidth = 100;
            this.type.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.type.FormattingEnabled = true;
            this.type.Items.AddRange(new object[] {
            "",
            "测验者",
            "送诊医生",
            "分诊医生"});
            this.type.Location = new System.Drawing.Point(60, 48);
            this.type.Margin = new System.Windows.Forms.Padding(4);
            this.type.Name = "type";
            this.type.Size = new System.Drawing.Size(104, 25);
            this.type.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(9, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 66;
            this.label2.Text = "类型:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.btnFound);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtType);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(3, 89);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(487, 44);
            this.panel1.TabIndex = 15;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtName.Location = new System.Drawing.Point(61, 8);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(103, 30);
            this.txtName.TabIndex = 51;
            // 
            // btnFound
            // 
            this.btnFound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFound.Location = new System.Drawing.Point(386, 4);
            this.btnFound.Margin = new System.Windows.Forms.Padding(4);
            this.btnFound.Name = "btnFound";
            this.btnFound.Size = new System.Drawing.Size(83, 35);
            this.btnFound.TabIndex = 64;
            this.btnFound.Text = "查询";
            this.btnFound.UseVisualStyleBackColor = true;
            this.btnFound.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("新宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(10, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 50;
            this.label3.Text = "姓名:";
            // 
            // txtType
            // 
            this.txtType.DropDownWidth = 100;
            this.txtType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtType.FormattingEnabled = true;
            this.txtType.Items.AddRange(new object[] {
            "",
            "测验者",
            "送诊医生",
            "分诊医生"});
            this.txtType.Location = new System.Drawing.Point(242, 12);
            this.txtType.Margin = new System.Windows.Forms.Padding(4);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(104, 25);
            this.txtType.TabIndex = 63;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(181, 15);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 62;
            this.label7.Text = "类型:";
            // 
            // btnDelete
            // 
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Location = new System.Drawing.Point(517, 48);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(83, 35);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.button2_Click);
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
            this.dgvConditionTreatRecordList.ColumnHeadersHeight = 30;
            this.dgvConditionTreatRecordList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDisposalItem,
            this.Column1,
            this.colCreator,
            this.Column4,
            this.Column3});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(104)))), ((int)(((byte)(152)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConditionTreatRecordList.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvConditionTreatRecordList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConditionTreatRecordList.EnableHeadersVisualStyles = false;
            this.dgvConditionTreatRecordList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.dgvConditionTreatRecordList.Location = new System.Drawing.Point(0, 140);
            this.dgvConditionTreatRecordList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvConditionTreatRecordList.Name = "dgvConditionTreatRecordList";
            this.dgvConditionTreatRecordList.RowHeadersVisible = false;
            this.dgvConditionTreatRecordList.RowTemplate.Height = 23;
            this.dgvConditionTreatRecordList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConditionTreatRecordList.Size = new System.Drawing.Size(735, 454);
            this.dgvConditionTreatRecordList.TabIndex = 14;
            this.dgvConditionTreatRecordList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvConditionTreatRecordList_CellClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
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
            // colDisposalItem
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colDisposalItem.DefaultCellStyle = dataGridViewCellStyle3;
            this.colDisposalItem.HeaderText = "序号";
            this.colDisposalItem.Name = "colDisposalItem";
            this.colDisposalItem.ReadOnly = true;
            this.colDisposalItem.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colDisposalItem.Width = 80;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.Width = 90;
            // 
            // colCreator
            // 
            this.colCreator.HeaderText = "姓名";
            this.colCreator.Name = "colCreator";
            this.colCreator.ReadOnly = true;
            this.colCreator.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colCreator.Width = 120;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "简拼";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 120;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "类型";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 120;
            // 
            // sys_name_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(735, 594);
            this.Controls.Add(this.dgvConditionTreatRecordList);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "sys_name_list";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "签名管理";
            this.Load += new System.EventHandler(this.yonghuliebiao_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConditionTreatRecordList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnInput;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnDelete;
        private DataGridView dgvConditionTreatRecordList;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private TextBox txtName;
        private Label label3;
        private Label label7;
        private Button btnFound;
        private System.Windows.Forms.ComboBox txtType;
        private Panel panel1;
        private Panel panel3;
        private TextBox code;
        private Label label4;
        private TextBox name;
        private Label label1;
        private ComboBox type;
        private Label label2;
        private Label txtnum;
        private Label label5;
        private Button button1;
        private DataGridViewTextBoxColumn colDisposalItem;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn colCreator;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column3;
    }
}