namespace ChildManager.UI
{
    partial class Panel_moban_list
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new DevComponents.DotNetBar.LabelX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_NewOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_BuyTicket = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NoOrderCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox30 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNo
            // 
            // 
            // 
            // 
            this.txtNo.Border.Class = "TextBoxBorder";
            this.txtNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtNo.Location = new System.Drawing.Point(92, 12);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(180, 23);
            this.txtNo.TabIndex = 0;
            this.txtNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            // 
            // 
            // 
            this.label1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "模板名称:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_NewOrder,
            this.menu_BuyTicket,
            this.menu_NoOrderCustomer});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(176, 70);
            // 
            // menu_NewOrder
            // 
            this.menu_NewOrder.Name = "menu_NewOrder";
            this.menu_NewOrder.Size = new System.Drawing.Size(175, 22);
            this.menu_NewOrder.Text = "新建订单(&O)";
            // 
            // menu_BuyTicket
            // 
            this.menu_BuyTicket.Name = "menu_BuyTicket";
            this.menu_BuyTicket.Size = new System.Drawing.Size(175, 22);
            this.menu_BuyTicket.Text = "购买水票(&T)";
            // 
            // menu_NoOrderCustomer
            // 
            this.menu_NoOrderCustomer.Name = "menu_NoOrderCustomer";
            this.menu_NoOrderCustomer.Size = new System.Drawing.Size(175, 22);
            this.menu_NoOrderCustomer.Text = "显示未订货客户(&Y)";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dataGridView1);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.panel4);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.txtNo);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(310, 311);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 6;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(233)))), ((int)(((byte)(248)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(26, 77);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(258, 222);
            this.dataGridView1.TabIndex = 114;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "模板列表";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(14, 51);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(72, 20);
            this.labelX1.TabIndex = 113;
            this.labelX1.Text = "模板类型:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.checkBox30);
            this.panel4.Location = new System.Drawing.Point(92, 47);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(180, 24);
            this.panel4.TabIndex = 112;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(79, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 18);
            this.checkBox1.TabIndex = 111;
            this.checkBox1.Text = "科室";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox30
            // 
            this.checkBox30.AutoSize = true;
            this.checkBox30.Checked = true;
            this.checkBox30.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox30.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox30.Location = new System.Drawing.Point(3, 3);
            this.checkBox30.Name = "checkBox30";
            this.checkBox30.Size = new System.Drawing.Size(54, 18);
            this.checkBox30.TabIndex = 110;
            this.checkBox30.Text = "个人";
            this.checkBox30.UseVisualStyleBackColor = true;
            this.checkBox30.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Panel_moban_list
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 311);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.Name = "Panel_moban_list";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保存模板";
            this.Load += new System.EventHandler(this.Panel_moban_list_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtNo;
        private DevComponents.DotNetBar.LabelX label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_NewOrder;
        private System.Windows.Forms.ToolStripMenuItem menu_BuyTicket;
        private System.Windows.Forms.ToolStripMenuItem menu_NoOrderCustomer;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox30;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}
