namespace ChildManager.UI.cepingshi
{
    partial class cp_dst_panel
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
            this.testlist = new System.Windows.Forms.ListBox();
            this.cszqm = new System.Windows.Forms.TextBox();
            this.szyslist = new System.Windows.Forms.ListBox();
            this.label73 = new System.Windows.Forms.Label();
            this.szys = new System.Windows.Forms.TextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.csrq = new System.Windows.Forms.DateTimePicker();
            this.label46 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.dst_jg = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.dst_yszf = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dst_dq = new System.Windows.Forms.TextBox();
            this.panelEx1 = new System.Windows.Forms.Panel();
            this.buttonX11 = new DevComponents.DotNetBar.ButtonX();
            this.update_time = new System.Windows.Forms.ComboBox();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.dst_jg.SuspendLayout();
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
            this.panel1.Controls.Add(this.testlist);
            this.panel1.Controls.Add(this.cszqm);
            this.panel1.Controls.Add(this.szyslist);
            this.panel1.Controls.Add(this.label73);
            this.panel1.Controls.Add(this.szys);
            this.panel1.Controls.Add(this.label47);
            this.panel1.Controls.Add(this.csrq);
            this.panel1.Controls.Add(this.label46);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1416, 596);
            this.panel1.TabIndex = 0;
            // 
            // testlist
            // 
            this.testlist.Font = new System.Drawing.Font("宋体", 10.5F);
            this.testlist.FormattingEnabled = true;
            this.testlist.ItemHeight = 17;
            this.testlist.Location = new System.Drawing.Point(164, 125);
            this.testlist.Margin = new System.Windows.Forms.Padding(4);
            this.testlist.Name = "testlist";
            this.testlist.Size = new System.Drawing.Size(176, 89);
            this.testlist.TabIndex = 124;
            this.testlist.TabStop = false;
            this.testlist.Visible = false;
            // 
            // cszqm
            // 
            this.cszqm.BackColor = System.Drawing.Color.White;
            this.cszqm.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cszqm.ForeColor = System.Drawing.Color.Black;
            this.cszqm.Location = new System.Drawing.Point(164, 99);
            this.cszqm.Margin = new System.Windows.Forms.Padding(4);
            this.cszqm.Name = "cszqm";
            this.cszqm.Size = new System.Drawing.Size(176, 27);
            this.cszqm.TabIndex = 1;
            this.cszqm.Enter += new System.EventHandler(this.cszqm_Enter);
            this.cszqm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cszqm_KeyDown);
            this.cszqm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cszqm_KeyPress);
            this.cszqm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cszqm_KeyUp);
            this.cszqm.Leave += new System.EventHandler(this.cszqm_Leave);
            // 
            // szyslist
            // 
            this.szyslist.Font = new System.Drawing.Font("宋体", 10.5F);
            this.szyslist.FormattingEnabled = true;
            this.szyslist.ItemHeight = 17;
            this.szyslist.Location = new System.Drawing.Point(500, 124);
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
            this.label73.Location = new System.Drawing.Point(399, 101);
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
            this.szys.Location = new System.Drawing.Point(500, 98);
            this.szys.Margin = new System.Windows.Forms.Padding(4);
            this.szys.Name = "szys";
            this.szys.Size = new System.Drawing.Size(176, 27);
            this.szys.TabIndex = 2;
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
            this.label47.Location = new System.Drawing.Point(729, 102);
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
            this.csrq.Location = new System.Drawing.Point(831, 96);
            this.csrq.Margin = new System.Windows.Forms.Padding(4);
            this.csrq.Name = "csrq";
            this.csrq.Size = new System.Drawing.Size(176, 27);
            this.csrq.TabIndex = 3;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label46.Location = new System.Drawing.Point(44, 101);
            this.label46.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(107, 18);
            this.label46.TabIndex = 91;
            this.label46.Text = "测试者签名:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.dst_jg);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.dst_yszf);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dst_dq);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1130, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DST测试结果";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label19.Location = new System.Drawing.Point(576, 30);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 18);
            this.label19.TabIndex = 96;
            this.label19.Text = "结果:";
            // 
            // dst_jg
            // 
            this.dst_jg.Controls.Add(this.checkBox1);
            this.dst_jg.Controls.Add(this.checkBox7);
            this.dst_jg.Controls.Add(this.checkBox8);
            this.dst_jg.Location = new System.Drawing.Point(640, 21);
            this.dst_jg.Margin = new System.Windows.Forms.Padding(4);
            this.dst_jg.Name = "dst_jg";
            this.dst_jg.Size = new System.Drawing.Size(332, 34);
            this.dst_jg.TabIndex = 95;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox1.Location = new System.Drawing.Point(225, 5);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 22);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "异常";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox7.Location = new System.Drawing.Point(124, 5);
            this.checkBox7.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(66, 22);
            this.checkBox7.TabIndex = 1;
            this.checkBox7.TabStop = false;
            this.checkBox7.Text = "可疑";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Font = new System.Drawing.Font("宋体", 10.5F);
            this.checkBox8.Location = new System.Drawing.Point(15, 5);
            this.checkBox8.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(66, 22);
            this.checkBox8.TabIndex = 0;
            this.checkBox8.TabStop = false;
            this.checkBox8.Text = "正常";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.CheckBox4CheckedChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label21.Location = new System.Drawing.Point(256, 31);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(26, 18);
            this.label21.TabIndex = 94;
            this.label21.Text = "分";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label20.Location = new System.Drawing.Point(59, 31);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(89, 18);
            this.label20.TabIndex = 93;
            this.label20.Text = "原始总分:";
            // 
            // dst_yszf
            // 
            this.dst_yszf.BackColor = System.Drawing.Color.White;
            this.dst_yszf.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dst_yszf.ForeColor = System.Drawing.Color.Black;
            this.dst_yszf.Location = new System.Drawing.Point(160, 26);
            this.dst_yszf.Margin = new System.Windows.Forms.Padding(4);
            this.dst_yszf.Name = "dst_yszf";
            this.dst_yszf.Size = new System.Drawing.Size(85, 27);
            this.dst_yszf.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10.5F);
            this.label7.Location = new System.Drawing.Point(367, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 18);
            this.label7.TabIndex = 30;
            this.label7.Text = "DQ";
            // 
            // dst_dq
            // 
            this.dst_dq.BackColor = System.Drawing.Color.White;
            this.dst_dq.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dst_dq.ForeColor = System.Drawing.Color.Black;
            this.dst_dq.Location = new System.Drawing.Point(403, 26);
            this.dst_dq.Margin = new System.Windows.Forms.Padding(4);
            this.dst_dq.Name = "dst_dq";
            this.dst_dq.Size = new System.Drawing.Size(103, 27);
            this.dst_dq.TabIndex = 1;
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
            // cp_dst_panel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelEx1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "cp_dst_panel";
            this.Size = new System.Drawing.Size(1416, 636);
            this.Load += new System.EventHandler(this.PanelyibanxinxiMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.dst_jg.ResumeLayout(false);
            this.dst_jg.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEx1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox dst_dq;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.DateTimePicker csrq;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox dst_yszf;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel dst_jg;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX11;
        private System.Windows.Forms.ComboBox update_time;
        private System.Windows.Forms.ListBox szyslist;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox szys;
        private System.Windows.Forms.ListBox testlist;
        private System.Windows.Forms.TextBox cszqm;
    }
}
