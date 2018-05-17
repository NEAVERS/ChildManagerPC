namespace ChildManager.UI.cepingshi
{
    partial class cp_sm_panel
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
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.szyslist = new System.Windows.Forms.ListBox();
            this.label73 = new System.Windows.Forms.Label();
            this.szys = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.csrq = new System.Windows.Forms.DateTimePicker();
            this.label46 = new System.Windows.Forms.Label();
            this.cszqm = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bz_visible = new System.Windows.Forms.Panel();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bz = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.jl = new System.Windows.Forms.Panel();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.bzf = new System.Windows.Forms.TextBox();
            this.panelEx1 = new System.Windows.Forms.Panel();
            this.buttonX11 = new DevComponents.DotNetBar.ButtonX();
            this.update_time = new System.Windows.Forms.ComboBox();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.bz_visible.SuspendLayout();
            this.jl.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 82F;
            this.dataGridViewTextBoxColumn1.HeaderText = "体检日期";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 82;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "年龄";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 65;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.szyslist);
            this.panel1.Controls.Add(this.label73);
            this.panel1.Controls.Add(this.szys);
            this.panel1.Controls.Add(this.label47);
            this.panel1.Controls.Add(this.csrq);
            this.panel1.Controls.Add(this.label46);
            this.panel1.Controls.Add(this.cszqm);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1416, 596);
            this.panel1.TabIndex = 22;
            // 
            // szyslist
            // 
            this.szyslist.Font = new System.Drawing.Font("宋体", 10.5F);
            this.szyslist.FormattingEnabled = true;
            this.szyslist.ItemHeight = 17;
            this.szyslist.Location = new System.Drawing.Point(489, 239);
            this.szyslist.Margin = new System.Windows.Forms.Padding(4);
            this.szyslist.Name = "szyslist";
            this.szyslist.Size = new System.Drawing.Size(176, 89);
            this.szyslist.TabIndex = 120;
            this.szyslist.TabStop = false;
            this.szyslist.Visible = false;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label73.Location = new System.Drawing.Point(388, 214);
            this.label73.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(89, 18);
            this.label73.TabIndex = 122;
            this.label73.Text = "送诊医生:";
            // 
            // szys
            // 
            this.szys.BackColor = System.Drawing.Color.White;
            this.szys.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.szys.ForeColor = System.Drawing.Color.Black;
            this.szys.Location = new System.Drawing.Point(489, 210);
            this.szys.Margin = new System.Windows.Forms.Padding(4);
            this.szys.Name = "szys";
            this.szys.Size = new System.Drawing.Size(176, 27);
            this.szys.TabIndex = 91;
            this.szys.Enter += new System.EventHandler(this.szys_Enter);
            this.szys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.szys_KeyDown);
            this.szys.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.szys_KeyPress);
            this.szys.KeyUp += new System.Windows.Forms.KeyEventHandler(this.szys_KeyUp);
            this.szys.Leave += new System.EventHandler(this.szys_Leave);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label47.Location = new System.Drawing.Point(824, 214);
            this.label47.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(89, 18);
            this.label47.TabIndex = 93;
            this.label47.Text = "测试日期:";
            // 
            // csrq
            // 
            this.csrq.CustomFormat = "yyyy-MM-dd";
            this.csrq.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.csrq.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.csrq.Location = new System.Drawing.Point(925, 209);
            this.csrq.Margin = new System.Windows.Forms.Padding(4);
            this.csrq.Name = "csrq";
            this.csrq.Size = new System.Drawing.Size(160, 27);
            this.csrq.TabIndex = 92;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label46.Location = new System.Drawing.Point(25, 214);
            this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(107, 18);
            this.label46.TabIndex = 91;
            this.label46.Text = "测试者签名:";
            // 
            // cszqm
            // 
            this.cszqm.BackColor = System.Drawing.Color.White;
            this.cszqm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cszqm.ForeColor = System.Drawing.Color.Black;
            this.cszqm.Location = new System.Drawing.Point(143, 209);
            this.cszqm.Margin = new System.Windows.Forms.Padding(4);
            this.cszqm.Name = "cszqm";
            this.cszqm.Size = new System.Drawing.Size(160, 27);
            this.cszqm.TabIndex = 90;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.bz_visible);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bz);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.jl);
            this.groupBox1.Controls.Add(this.bzf);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(4, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1130, 194);
            this.groupBox1.TabIndex = 87;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SM测试结果";
            // 
            // bz_visible
            // 
            this.bz_visible.Controls.Add(this.checkBox8);
            this.bz_visible.Controls.Add(this.checkBox7);
            this.bz_visible.Location = new System.Drawing.Point(1003, 88);
            this.bz_visible.Name = "bz_visible";
            this.bz_visible.Size = new System.Drawing.Size(120, 80);
            this.bz_visible.TabIndex = 94;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox8.Location = new System.Drawing.Point(9, 45);
            this.checkBox8.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(102, 22);
            this.checkBox8.TabIndex = 2;
            this.checkBox8.TabStop = false;
            this.checkBox8.Text = "病人可见";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Checked = true;
            this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox7.Location = new System.Drawing.Point(9, 15);
            this.checkBox7.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(102, 22);
            this.checkBox7.TabIndex = 1;
            this.checkBox7.TabStop = false;
            this.checkBox7.Text = "医生可见";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label1.Location = new System.Drawing.Point(44, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 93;
            this.label1.Text = "备注:";
            // 
            // bz
            // 
            this.bz.BackColor = System.Drawing.Color.White;
            this.bz.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bz.ForeColor = System.Drawing.Color.Black;
            this.bz.Location = new System.Drawing.Point(108, 71);
            this.bz.Margin = new System.Windows.Forms.Padding(4);
            this.bz.Multiline = true;
            this.bz.Name = "bz";
            this.bz.Size = new System.Drawing.Size(888, 106);
            this.bz.TabIndex = 92;
            this.bz.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label8.Location = new System.Drawing.Point(244, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 18);
            this.label8.TabIndex = 56;
            this.label8.Text = "结论:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label2.Location = new System.Drawing.Point(191, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 18);
            this.label2.TabIndex = 32;
            this.label2.Text = ")";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label7.Location = new System.Drawing.Point(25, 30);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "标准分(";
            // 
            // jl
            // 
            this.jl.Controls.Add(this.checkBox6);
            this.jl.Controls.Add(this.checkBox5);
            this.jl.Controls.Add(this.checkBox2);
            this.jl.Controls.Add(this.checkBox1);
            this.jl.Controls.Add(this.checkBox3);
            this.jl.Controls.Add(this.checkBox4);
            this.jl.Location = new System.Drawing.Point(308, 20);
            this.jl.Margin = new System.Windows.Forms.Padding(4);
            this.jl.Name = "jl";
            this.jl.Size = new System.Drawing.Size(775, 34);
            this.jl.TabIndex = 28;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox6.Location = new System.Drawing.Point(637, 6);
            this.checkBox6.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(120, 22);
            this.checkBox6.TabIndex = 5;
            this.checkBox6.TabStop = false;
            this.checkBox6.Text = "极重度异常";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox5.Location = new System.Drawing.Point(503, 6);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(102, 22);
            this.checkBox5.TabIndex = 4;
            this.checkBox5.TabStop = false;
            this.checkBox5.Text = "重度异常";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox2.Location = new System.Drawing.Point(369, 6);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(102, 22);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.TabStop = false;
            this.checkBox2.Text = "中度异常";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox1.Location = new System.Drawing.Point(228, 6);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 22);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "轻度异常";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox3.Location = new System.Drawing.Point(124, 6);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(66, 22);
            this.checkBox3.TabIndex = 1;
            this.checkBox3.TabStop = false;
            this.checkBox3.Text = "可疑";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox4.Location = new System.Drawing.Point(15, 6);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(66, 22);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.TabStop = false;
            this.checkBox4.Text = "正常";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // bzf
            // 
            this.bzf.BackColor = System.Drawing.Color.White;
            this.bzf.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bzf.ForeColor = System.Drawing.Color.Black;
            this.bzf.Location = new System.Drawing.Point(108, 25);
            this.bzf.Margin = new System.Windows.Forms.Padding(4);
            this.bzf.Name = "bzf";
            this.bzf.Size = new System.Drawing.Size(73, 27);
            this.bzf.TabIndex = 26;
            this.bzf.TextChanged += new System.EventHandler(this.BzfTextChanged);
            // 
            // panelEx1
            // 
            this.panelEx1.BackgroundImage = global::ChildManager.Properties.Resources.toolstrip04;
            this.panelEx1.Controls.Add(this.buttonX11);
            this.panelEx1.Controls.Add(this.update_time);
            this.panelEx1.Controls.Add(this.buttonX4);
            this.panelEx1.Controls.Add(this.buttonX2);
            this.panelEx1.Controls.Add(this.buttonX3);
            this.panelEx1.Controls.Add(this.buttonX1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1416, 40);
            this.panelEx1.TabIndex = 14;
            // 
            // buttonX11
            // 
            this.buttonX11.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.buttonX11.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonX11.Location = new System.Drawing.Point(237, 4);
            this.buttonX11.Margin = new System.Windows.Forms.Padding(4);
            this.buttonX11.Name = "buttonX11";
            this.buttonX11.Size = new System.Drawing.Size(197, 32);
            this.buttonX11.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX11.TabIndex = 119;
            this.buttonX11.Text = "新增测评记录";
            this.buttonX11.Click += new System.EventHandler(this.buttonX11_Click);
            // 
            // update_time
            // 
            this.update_time.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.update_time.FormattingEnabled = true;
            this.update_time.Location = new System.Drawing.Point(12, 5);
            this.update_time.Margin = new System.Windows.Forms.Padding(4);
            this.update_time.Name = "update_time";
            this.update_time.Size = new System.Drawing.Size(213, 25);
            this.update_time.TabIndex = 118;
            this.update_time.SelectedIndexChanged += new System.EventHandler(this.update_time_SelectedIndexChanged);
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonX4.Location = new System.Drawing.Point(771, 4);
            this.buttonX4.Margin = new System.Windows.Forms.Padding(4);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(100, 32);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 4;
            this.buttonX4.Text = "打印";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonX2.Location = new System.Drawing.Point(659, 4);
            this.buttonX2.Margin = new System.Windows.Forms.Padding(4);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(100, 32);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 3;
            this.buttonX2.Text = "预览";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonX3.Location = new System.Drawing.Point(551, 4);
            this.buttonX3.Margin = new System.Windows.Forms.Padding(4);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(100, 32);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 2;
            this.buttonX3.Text = "删除";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonX1.Location = new System.Drawing.Point(443, 4);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(4);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(100, 32);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 0;
            this.buttonX1.Text = "保存";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // cp_sm_panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelEx1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "cp_sm_panel";
            this.Size = new System.Drawing.Size(1416, 636);
            this.Load += new System.EventHandler(this.PanelyibanxinxiMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.bz_visible.ResumeLayout(false);
            this.bz_visible.PerformLayout();
            this.jl.ResumeLayout(false);
            this.jl.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEx1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        public DevComponents.DotNetBar.ButtonX buttonX1;
        public DevComponents.DotNetBar.ButtonX buttonX3;
        public DevComponents.DotNetBar.ButtonX buttonX4;
        public DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel jl;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox bzf;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.DateTimePicker csrq;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox cszqm;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox bz;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevComponents.DotNetBar.ButtonX buttonX11;
        private System.Windows.Forms.ComboBox update_time;
        private System.Windows.Forms.ListBox szyslist;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox szys;
        private System.Windows.Forms.Panel bz_visible;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox7;
    }
}
