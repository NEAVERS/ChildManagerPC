namespace ChildManager.UI.nursinfo
{
    partial class Panel_searchHisInfo
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Panel_searchHisInfo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtp_endtime = new System.Windows.Forms.DateTimePicker();
            this.label8 = new DevComponents.DotNetBar.LabelX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label6 = new DevComponents.DotNetBar.LabelX();
            this.txtPatientId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label5 = new DevComponents.DotNetBar.LabelX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_NewOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_BuyTicket = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NoOrderCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtSex = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dtp_starttime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker4 = new System.Windows.Forms.DateTimePicker();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.chkUseDate = new System.Windows.Forms.CheckBox();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.dataGridView1 = new RowMergeView();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtp_endtime
            // 
            this.dtp_endtime.Enabled = false;
            this.dtp_endtime.Font = new System.Drawing.Font("宋体", 12F);
            this.dtp_endtime.Location = new System.Drawing.Point(341, 52);
            this.dtp_endtime.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_endtime.Name = "dtp_endtime";
            this.dtp_endtime.Size = new System.Drawing.Size(187, 30);
            this.dtp_endtime.TabIndex = 14;
            this.dtp_endtime.Leave += new System.EventHandler(this.dtp_endtime_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            // 
            // 
            // 
            this.label8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(303, 59);
            this.label8.Margin = new System.Windows.Forms.Padding(4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 28);
            this.label8.TabIndex = 11;
            this.label8.Text = "至";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(931, 51);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(104, 30);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtName.DisabledBackColor = System.Drawing.Color.White;
            this.txtName.Font = new System.Drawing.Font("宋体", 12F);
            this.txtName.ForeColor = System.Drawing.Color.Black;
            this.txtName.Location = new System.Drawing.Point(316, 8);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(144, 30);
            this.txtName.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            // 
            // 
            // 
            this.label6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(260, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 28);
            this.label6.TabIndex = 0;
            this.label6.Text = "姓名";
            // 
            // txtPatientId
            // 
            this.txtPatientId.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.txtPatientId.Border.Class = "TextBoxBorder";
            this.txtPatientId.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPatientId.DisabledBackColor = System.Drawing.Color.White;
            this.txtPatientId.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPatientId.ForeColor = System.Drawing.Color.Black;
            this.txtPatientId.Location = new System.Drawing.Point(91, 8);
            this.txtPatientId.Margin = new System.Windows.Forms.Padding(4);
            this.txtPatientId.Name = "txtPatientId";
            this.txtPatientId.Size = new System.Drawing.Size(161, 30);
            this.txtPatientId.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            // 
            // 
            // 
            this.label5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(11, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 28);
            this.label5.TabIndex = 0;
            this.label5.Text = "病人ID";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_NewOrder,
            this.menu_BuyTicket,
            this.menu_NoOrderCustomer});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(203, 76);
            // 
            // menu_NewOrder
            // 
            this.menu_NewOrder.Name = "menu_NewOrder";
            this.menu_NewOrder.Size = new System.Drawing.Size(202, 24);
            this.menu_NewOrder.Text = "新建订单(&O)";
            // 
            // menu_BuyTicket
            // 
            this.menu_BuyTicket.Name = "menu_BuyTicket";
            this.menu_BuyTicket.Size = new System.Drawing.Size(202, 24);
            this.menu_BuyTicket.Text = "购买水票(&T)";
            // 
            // menu_NoOrderCustomer
            // 
            this.menu_NoOrderCustomer.Name = "menu_NoOrderCustomer";
            this.menu_NoOrderCustomer.Size = new System.Drawing.Size(202, 24);
            this.menu_NoOrderCustomer.Text = "显示未订货客户(&Y)";
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelX5);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.txtSex);
            this.panelEx1.Controls.Add(this.checkBox1);
            this.panelEx1.Controls.Add(this.dtp_starttime);
            this.panelEx1.Controls.Add(this.dateTimePicker4);
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.chkUseDate);
            this.panelEx1.Controls.Add(this.dtp_endtime);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.txtPatientId);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.txtName);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1288, 102);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 6;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelX5.ForeColor = System.Drawing.Color.Red;
            this.labelX5.Location = new System.Drawing.Point(811, 55);
            this.labelX5.Margin = new System.Windows.Forms.Padding(4);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(100, 29);
            this.labelX5.TabIndex = 24;
            this.labelX5.Text = "0";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.labelX2.Location = new System.Drawing.Point(644, 58);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(159, 29);
            this.labelX2.TabIndex = 23;
            this.labelX2.Text = "当前页面行数:";
            // 
            // txtSex
            // 
            this.txtSex.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.txtSex.FormattingEnabled = true;
            this.txtSex.Items.AddRange(new object[] {
            "",
            "男",
            "女",
            "性别不明"});
            this.txtSex.Location = new System.Drawing.Point(561, 9);
            this.txtSex.Margin = new System.Windows.Forms.Padding(4);
            this.txtSex.Name = "txtSex";
            this.txtSex.Size = new System.Drawing.Size(129, 28);
            this.txtSex.TabIndex = 22;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.checkBox1.Location = new System.Drawing.Point(542, 57);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(91, 24);
            this.checkBox1.TabIndex = 17;
            this.checkBox1.Text = "仅当天";
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // dtp_starttime
            // 
            this.dtp_starttime.Enabled = false;
            this.dtp_starttime.Font = new System.Drawing.Font("宋体", 12F);
            this.dtp_starttime.Location = new System.Drawing.Point(107, 52);
            this.dtp_starttime.Margin = new System.Windows.Forms.Padding(4);
            this.dtp_starttime.Name = "dtp_starttime";
            this.dtp_starttime.Size = new System.Drawing.Size(188, 30);
            this.dtp_starttime.TabIndex = 20;
            this.dtp_starttime.Leave += new System.EventHandler(this.dtp_starttime_ValueChanged);
            // 
            // dateTimePicker4
            // 
            this.dateTimePicker4.Enabled = false;
            this.dateTimePicker4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePicker4.Location = new System.Drawing.Point(798, 9);
            this.dateTimePicker4.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePicker4.Name = "dateTimePicker4";
            this.dateTimePicker4.Size = new System.Drawing.Size(160, 27);
            this.dateTimePicker4.TabIndex = 20;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("宋体", 12F);
            this.labelX4.Location = new System.Drawing.Point(6, 56);
            this.labelX4.Margin = new System.Windows.Forms.Padding(4);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(92, 28);
            this.labelX4.TabIndex = 19;
            this.labelX4.Text = "检查日期";
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("宋体", 12F);
            this.labelX3.Location = new System.Drawing.Point(698, 10);
            this.labelX3.Margin = new System.Windows.Forms.Padding(4);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(92, 28);
            this.labelX3.TabIndex = 19;
            this.labelX3.Text = "出生日期";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 12F);
            this.labelX1.Location = new System.Drawing.Point(472, 10);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(92, 28);
            this.labelX1.TabIndex = 16;
            this.labelX1.Text = "儿童性别";
            // 
            // chkUseDate
            // 
            this.chkUseDate.AutoSize = true;
            this.chkUseDate.Font = new System.Drawing.Font("宋体", 12F);
            this.chkUseDate.Location = new System.Drawing.Point(966, 10);
            this.chkUseDate.Margin = new System.Windows.Forms.Padding(4);
            this.chkUseDate.Name = "chkUseDate";
            this.chkUseDate.Size = new System.Drawing.Size(71, 24);
            this.chkUseDate.TabIndex = 8;
            this.chkUseDate.Text = "启用";
            this.chkUseDate.CheckedChanged += new System.EventHandler(this.chkUseDate_CheckedChanged);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.dataGridView1);
            this.panelEx2.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx2.Location = new System.Drawing.Point(0, 100);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(4);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1288, 718);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(235)))), ((int)(((byte)(248)))));
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 11;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle17;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(104)))), ((int)(((byte)(152)))));
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1,
            this.dataGridViewTextBoxColumn3,
            this.Column11,
            this.Column10,
            this.Column12,
            this.Column15,
            this.Column3});
            this.dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle19;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.MergeColumnHeaderBackColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.MergeColumnNames = ((System.Collections.Generic.List<string>)(resources.GetObject("dataGridView1.MergeColumnNames")));
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("宋体", 12F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 20;
            this.dataGridView1.RowTemplate.Height = 55;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1288, 718);
            this.dataGridView1.TabIndex = 44;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX2_CellDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGridView1_KeyPress);
            // 
            // bar1
            // 
            this.bar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bar1.Font = new System.Drawing.Font("宋体", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Location = new System.Drawing.Point(0, 825);
            this.bar1.Margin = new System.Windows.Forms.Padding(4);
            this.bar1.Name = "bar1";
            this.bar1.RoundCorners = false;
            this.bar1.Size = new System.Drawing.Size(1287, 25);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.bar1.TabIndex = 18;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "病人ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 140;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "出生日期";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 180;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "病人性别";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "就诊时间";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 180;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "挂号类别";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "病人ID";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 120;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "就诊卡号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "病人姓名";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 110;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "出生日期";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 130;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "性别";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 70;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "就诊时间";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 130;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "挂号类别";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "科室";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Panel_searchHisInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 850);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.panelEx2);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Panel_searchHisInfo";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "儿保科挂号查询";
            this.Load += new System.EventHandler(this.Paneltsb_searchInfo_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.LabelX label6;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPatientId;
        private DevComponents.DotNetBar.LabelX label5;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_NewOrder;
        private System.Windows.Forms.ToolStripMenuItem menu_BuyTicket;
        private System.Windows.Forms.DateTimePicker dtp_endtime;
        private DevComponents.DotNetBar.LabelX label8;
        private System.Windows.Forms.ToolStripMenuItem menu_NoOrderCustomer;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.CheckBox chkUseDate;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.ComboBox txtSex;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Bar bar1;
        public RowMergeView dataGridView1;
        private System.Windows.Forms.DateTimePicker dtp_starttime;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}
