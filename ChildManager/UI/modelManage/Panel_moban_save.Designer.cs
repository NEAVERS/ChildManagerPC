namespace ChildManager.UI
{
    partial class Panel_moban_save
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
            this.txtNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new DevComponents.DotNetBar.LabelX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_NewOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_BuyTicket = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_NoOrderCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox30 = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1.SuspendLayout();
            this.panelEx1.SuspendLayout();
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
            this.panelEx1.Controls.Add(this.buttonX1);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.panel4);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Controls.Add(this.txtNo);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(330, 126);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 6;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonX1.Location = new System.Drawing.Point(119, 88);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 26);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 114;
            this.buttonX1.Text = "保存";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
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
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBoxs_CheckedChanged);
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
            this.checkBox30.CheckedChanged += new System.EventHandler(this.checkBoxs_CheckedChanged);
            // 
            // Panel_moban_save
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 126);
            this.Controls.Add(this.panelEx1);
            this.MaximizeBox = false;
            this.Name = "Panel_moban_save";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保存模板";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
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
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}
