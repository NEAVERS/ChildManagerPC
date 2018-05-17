namespace ChildManager.UI.sys
{
    partial class sys_rolemenu
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("医生工作站");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("测评室工作站");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("训练室工作站");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("统计报表");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("系统设置");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("功能菜单");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("预约工作站");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("护士工作站");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sys_rolemenu));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ckbAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.tvInfo = new System.Windows.Forms.TreeView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.dgvrole = new System.Windows.Forms.DataGridView();
            this.danganhao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvrole)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.tvInfo);
            this.panel1.Location = new System.Drawing.Point(252, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 599);
            this.panel1.TabIndex = 24;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BackgroundImage = global::ChildManager.Properties.Resources.toolstrip04;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.ckbAll);
            this.panel3.Controls.Add(this.btnAdd);
            this.panel3.Location = new System.Drawing.Point(1, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(635, 36);
            this.panel3.TabIndex = 15;
            // 
            // ckbAll
            // 
            this.ckbAll.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ckbAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ckbAll.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbAll.Location = new System.Drawing.Point(4, 2);
            this.ckbAll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ckbAll.Name = "ckbAll";
            this.ckbAll.Size = new System.Drawing.Size(81, 29);
            this.ckbAll.TabIndex = 111;
            this.ckbAll.Text = "全选";
            this.ckbAll.Click += new System.EventHandler(this.ckbAll_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.MagentaWithBackground;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAdd.Location = new System.Drawing.Point(120, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 29);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2003;
            this.btnAdd.TabIndex = 110;
            this.btnAdd.Text = "保  存";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tvInfo
            // 
            this.tvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.tvInfo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tvInfo.Location = new System.Drawing.Point(0, 38);
            this.tvInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tvInfo.Name = "tvInfo";
            treeNode1.Name = "节点0";
            treeNode1.Text = "医生工作站";
            treeNode2.Name = "节点1";
            treeNode2.Text = "测评室工作站";
            treeNode3.Name = "节点2";
            treeNode3.Text = "训练室工作站";
            treeNode4.Name = "节点0";
            treeNode4.Text = "统计报表";
            treeNode5.Name = "节点1";
            treeNode5.Text = "系统设置";
            treeNode6.Name = "节点2";
            treeNode6.Text = "功能菜单";
            treeNode7.Name = "Node1";
            treeNode7.Text = "预约工作站";
            treeNode8.Name = "节点0";
            treeNode8.Text = "护士工作站";
            this.tvInfo.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            this.tvInfo.Size = new System.Drawing.Size(636, 558);
            this.tvInfo.TabIndex = 106;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "角色编码";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "id";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            this.dataGridViewTextBoxColumn3.Width = 10;
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.BackgroundImage = global::ChildManager.Properties.Resources.toolstrip04;
            this.bar1.BackgroundImagePosition = DevComponents.DotNetBar.eBackgroundImagePosition.Tile;
            this.bar1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.bar1.IsMaximized = false;
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem3});
            this.bar1.Location = new System.Drawing.Point(5, 2);
            this.bar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(243, 31);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 25;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem1.Image")));
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "新增";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem2.Image")));
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "修改";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem3.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem3.Image")));
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "删除";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // dgvrole
            // 
            this.dgvrole.AllowUserToAddRows = false;
            this.dgvrole.AllowUserToResizeColumns = false;
            this.dgvrole.AllowUserToResizeRows = false;
            this.dgvrole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvrole.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.dgvrole.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvrole.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvrole.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.danganhao,
            this.name,
            this.Column1});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvrole.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvrole.GridColor = System.Drawing.Color.LightSteelBlue;
            this.dgvrole.Location = new System.Drawing.Point(9, 39);
            this.dgvrole.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvrole.MultiSelect = false;
            this.dgvrole.Name = "dgvrole";
            this.dgvrole.RowHeadersVisible = false;
            this.dgvrole.RowTemplate.Height = 23;
            this.dgvrole.RowTemplate.ReadOnly = true;
            this.dgvrole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvrole.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvrole.Size = new System.Drawing.Size(239, 558);
            this.dgvrole.TabIndex = 26;
            this.dgvrole.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvrole_CellBeginEdit);
            this.dgvrole.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvrole_CellClick);
            this.dgvrole.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvrole_CellDoubleClick);
            this.dgvrole.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvrole_CellEndEdit);
            // 
            // danganhao
            // 
            this.danganhao.DataPropertyName = "mrn";
            this.danganhao.FillWeight = 65F;
            this.danganhao.HeaderText = "角色名称";
            this.danganhao.Name = "danganhao";
            this.danganhao.Width = 90;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.FillWeight = 110F;
            this.name.HeaderText = "角色编码";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 110;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "id";
            this.Column1.Name = "Column1";
            this.Column1.Visible = false;
            // 
            // sys_rolemenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(247)))), ((int)(((byte)(254)))));
            this.Controls.Add(this.dgvrole);
            this.Controls.Add(this.bar1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "sys_rolemenu";
            this.Size = new System.Drawing.Size(897, 600);
            this.Load += new System.EventHandler(this.sysrolemenu_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvrole)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvInfo;
        private DevComponents.DotNetBar.Controls.CheckBoxX ckbAll;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        public System.Windows.Forms.DataGridView dgvrole;
        private System.Windows.Forms.DataGridViewTextBoxColumn danganhao;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}
