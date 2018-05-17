namespace ChildManager.UI.jibenluru
{
    partial class WomenInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public System.Windows.Forms.Panel pnlContent;//定义面板//定义眉头按钮集合

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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("未就诊");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("已就诊");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WomenInfo));
            this.pnlContent = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.l_patientid = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.l_csqk = new System.Windows.Forms.TextBox();
            this.l_yunzhou = new System.Windows.Forms.TextBox();
            this.l_gp = new System.Windows.Forms.TextBox();
            this.l_weight = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.l_sex = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.l_age = new System.Windows.Forms.TextBox();
            this.l_birth = new System.Windows.Forms.TextBox();
            this.l_cardno = new System.Windows.Forms.TextBox();
            this.l_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.butRead = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.pnlSplitter = new System.Windows.Forms.Panel();
            this.btnSearcher = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.Location = new System.Drawing.Point(163, 101);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(850, 515);
            this.pnlContent.TabIndex = 17;
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
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Location = new System.Drawing.Point(160, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(850, 50);
            this.panel2.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.l_patientid);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.l_csqk);
            this.groupBox1.Controls.Add(this.l_yunzhou);
            this.groupBox1.Controls.Add(this.l_gp);
            this.groupBox1.Controls.Add(this.l_weight);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.l_sex);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.l_age);
            this.groupBox1.Controls.Add(this.l_birth);
            this.groupBox1.Controls.Add(this.l_cardno);
            this.groupBox1.Controls.Add(this.l_name);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(165, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(849, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基本信息";
            // 
            // l_patientid
            // 
            this.l_patientid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_patientid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_patientid.Location = new System.Drawing.Point(688, 44);
            this.l_patientid.Name = "l_patientid";
            this.l_patientid.ReadOnly = true;
            this.l_patientid.Size = new System.Drawing.Size(108, 19);
            this.l_patientid.TabIndex = 21;
            this.l_patientid.Text = "       ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(632, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 14);
            this.label13.TabIndex = 20;
            this.label13.Text = "病人ID:";
            // 
            // l_csqk
            // 
            this.l_csqk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_csqk.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_csqk.Location = new System.Drawing.Point(539, 44);
            this.l_csqk.Name = "l_csqk";
            this.l_csqk.ReadOnly = true;
            this.l_csqk.Size = new System.Drawing.Size(82, 19);
            this.l_csqk.TabIndex = 19;
            this.l_csqk.Text = "        ";
            // 
            // l_yunzhou
            // 
            this.l_yunzhou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_yunzhou.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_yunzhou.Location = new System.Drawing.Point(374, 44);
            this.l_yunzhou.Name = "l_yunzhou";
            this.l_yunzhou.ReadOnly = true;
            this.l_yunzhou.Size = new System.Drawing.Size(77, 19);
            this.l_yunzhou.TabIndex = 18;
            this.l_yunzhou.Text = "        ";
            // 
            // l_gp
            // 
            this.l_gp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_gp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_gp.Location = new System.Drawing.Point(226, 45);
            this.l_gp.Name = "l_gp";
            this.l_gp.ReadOnly = true;
            this.l_gp.Size = new System.Drawing.Size(78, 19);
            this.l_gp.TabIndex = 17;
            this.l_gp.Text = "        ";
            // 
            // l_weight
            // 
            this.l_weight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_weight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_weight.Location = new System.Drawing.Point(72, 45);
            this.l_weight.Name = "l_weight";
            this.l_weight.ReadOnly = true;
            this.l_weight.Size = new System.Drawing.Size(77, 19);
            this.l_weight.TabIndex = 15;
            this.l_weight.Text = "        ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(471, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 14);
            this.label10.TabIndex = 14;
            this.label10.Text = "产时情况:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(332, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 14);
            this.label9.TabIndex = 13;
            this.label9.Text = "孕周:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(186, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "胎次:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(5, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 10;
            this.label6.Text = "出生体重:";
            // 
            // l_sex
            // 
            this.l_sex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_sex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_sex.Location = new System.Drawing.Point(372, 21);
            this.l_sex.Name = "l_sex";
            this.l_sex.ReadOnly = true;
            this.l_sex.Size = new System.Drawing.Size(77, 19);
            this.l_sex.TabIndex = 9;
            this.l_sex.Text = "    ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(332, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 14);
            this.label11.TabIndex = 8;
            this.label11.Text = "性别:";
            // 
            // l_age
            // 
            this.l_age.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_age.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_age.Location = new System.Drawing.Point(688, 21);
            this.l_age.Name = "l_age";
            this.l_age.ReadOnly = true;
            this.l_age.Size = new System.Drawing.Size(109, 19);
            this.l_age.TabIndex = 7;
            this.l_age.Text = "       ";
            // 
            // l_birth
            // 
            this.l_birth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_birth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_birth.Location = new System.Drawing.Point(540, 21);
            this.l_birth.Name = "l_birth";
            this.l_birth.ReadOnly = true;
            this.l_birth.Size = new System.Drawing.Size(96, 19);
            this.l_birth.TabIndex = 6;
            this.l_birth.Text = "   ";
            // 
            // l_cardno
            // 
            this.l_cardno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_cardno.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_cardno.Location = new System.Drawing.Point(226, 21);
            this.l_cardno.Name = "l_cardno";
            this.l_cardno.ReadOnly = true;
            this.l_cardno.Size = new System.Drawing.Size(78, 19);
            this.l_cardno.TabIndex = 5;
            this.l_cardno.Text = "    ";
            // 
            // l_name
            // 
            this.l_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.l_name.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.l_name.Location = new System.Drawing.Point(72, 21);
            this.l_name.Name = "l_name";
            this.l_name.ReadOnly = true;
            this.l_name.Size = new System.Drawing.Size(77, 19);
            this.l_name.TabIndex = 4;
            this.l_name.Text = "        ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(646, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 14);
            this.label5.TabIndex = 3;
            this.label5.Text = "年龄:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(471, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 14);
            this.label4.TabIndex = 2;
            this.label4.Text = "出生日期:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(172, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 14);
            this.label3.TabIndex = 1;
            this.label3.Text = "档案号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(31, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "姓名：";
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(160, 628);
            this.panel3.TabIndex = 19;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.butRead);
            this.groupBox2.Controls.Add(this.treeView1);
            this.groupBox2.Controls.Add(this.pnlSplitter);
            this.groupBox2.Controls.Add(this.btnSearcher);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(7, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 614);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // butRead
            // 
            this.butRead.BackColor = System.Drawing.Color.LavenderBlush;
            this.butRead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRead.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.butRead.Location = new System.Drawing.Point(6, 43);
            this.butRead.Name = "butRead";
            this.butRead.Size = new System.Drawing.Size(138, 32);
            this.butRead.TabIndex = 115;
            this.butRead.Text = "读卡";
            this.butRead.UseVisualStyleBackColor = false;
            this.butRead.Click += new System.EventHandler(this.butRead_Click);
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView1.ItemHeight = 20;
            this.treeView1.Location = new System.Drawing.Point(0, 81);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "root1";
            treeNode1.Text = "未就诊";
            treeNode2.Name = "root2";
            treeNode2.Text = "已就诊";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeView1.Size = new System.Drawing.Size(151, 527);
            this.treeView1.TabIndex = 114;
            this.treeView1.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView1_DrawNode);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // pnlSplitter
            // 
            this.pnlSplitter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.pnlSplitter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlSplitter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlSplitter.BackgroundImage")));
            this.pnlSplitter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlSplitter.Location = new System.Drawing.Point(150, -5);
            this.pnlSplitter.Name = "pnlSplitter";
            this.pnlSplitter.Size = new System.Drawing.Size(10, 619);
            this.pnlSplitter.TabIndex = 27;
            // 
            // btnSearcher
            // 
            this.btnSearcher.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnSearcher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearcher.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearcher.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearcher.Location = new System.Drawing.Point(82, 9);
            this.btnSearcher.Name = "btnSearcher";
            this.btnSearcher.Size = new System.Drawing.Size(65, 32);
            this.btnSearcher.TabIndex = 0;
            this.btnSearcher.Text = "查 询";
            this.btnSearcher.UseVisualStyleBackColor = false;
            this.btnSearcher.Click += new System.EventHandler(this.btnSearcher_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.LavenderBlush;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(6, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "刷 新";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip2.AutoSize = false;
            this.toolStrip2.BackColor = System.Drawing.Color.White;
            this.toolStrip2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip2.BackgroundImage")));
            this.toolStrip2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Location = new System.Drawing.Point(165, 74);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(849, 28);
            this.toolStrip2.TabIndex = 802;
            this.toolStrip2.Text = "toolStrip2";
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
            // WomenInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlContent);
            this.Name = "WomenInfo";
            this.Size = new System.Drawing.Size(1014, 620);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.TextBox l_name;
        private System.Windows.Forms.Panel pnlSplitter;
        public System.Windows.Forms.TextBox l_age;
        public System.Windows.Forms.TextBox l_birth;
        public System.Windows.Forms.TextBox l_cardno;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        public System.Windows.Forms.Button btnSearcher;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        public System.Windows.Forms.TextBox l_sex;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox l_weight;
        public System.Windows.Forms.TextBox l_gp;
        public System.Windows.Forms.TextBox l_csqk;
        public System.Windows.Forms.TextBox l_yunzhou;
        public System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.TextBox l_patientid;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.Button butRead;
    }
}