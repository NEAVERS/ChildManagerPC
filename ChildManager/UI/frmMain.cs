using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Globalization;
using YCF.Common;
using ChildManager.UI.jibenluru;
using ChildManager.UI.tongji;
using YCF.BLL.sys;
using YCF.Model;
using System.Collections.Generic;
using System.IO;
using ChildManager.UI.cepingshi;
using ChildManager.UI.nursinfo;

namespace ChildManager.UI
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class frmMain : DevComponents.DotNetBar.RibbonForm
    {
        #region Private Vars
        private System.Windows.Forms.MdiClient mdiClient1;
        private System.ComponentModel.IContainer components;
        private DevComponents.DotNetBar.RibbonControl ribbonControl1;
        private DevComponents.DotNetBar.RibbonPanel ribbonPanel2;
        private DevComponents.DotNetBar.RibbonBar ribbonBar6;
        private DevComponents.DotNetBar.RibbonTabItemGroup ribbonTabItemGroup1;
        private DevComponents.DotNetBar.RibbonTabItem ribbonTabContext;
        private DevComponents.DotNetBar.ButtonItem buttonChangeStyle;
        private DevComponents.DotNetBar.ButtonItem buttonStyleOffice2007Blue;
        private DevComponents.DotNetBar.ButtonItem buttonStyleOffice2007Black;
        private DevComponents.DotNetBar.ButtonItem buttonItem47;
        private DevComponents.DotNetBar.ButtonItem buttonItem48;
        private DevComponents.DotNetBar.ButtonItem buttonItem49;
        private DevComponents.DotNetBar.ButtonItem buttonStyleOffice2007Silver;
        private DevComponents.DotNetBar.ColorPickerDropDown buttonStyleCustom;
        private Command AppCommandTheme;
        private Command AppCommandExit;
        private DevComponents.DotNetBar.ButtonItem buttonItem60;
        private StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonItem buttonStyleOffice2010Silver;
        private DevComponents.DotNetBar.ButtonItem buttonItem62;
        private ButtonItem buttonStyleOffice2010Blue;
        private ButtonItem buttonStyleOffice2010Black;
        private SwitchButtonItem switchButtonItem1;
        private Command RibbonStateCommand;
        private ButtonItem buttonStyleVS2010;
        private ButtonItem buttonStyleMetro;
        private ButtonItem buttonItem16;
        private SuperTabControl superTabControl1;
        private ButtonItem btn_close;
        private LabelItem labelItem1;
        private RibbonPanel ribbonPanel1;
        private RibbonBar ribbonBar1;
        private ButtonItem buttonItem6;
        private RibbonTabItem ribbonTabItem1;
        private ButtonItem btn_reset;
        private ButtonItem buttonItem4;
        private ButtonItem buttonItem17;
        #endregion

        #region Constructor/Dispose
        public frmMain()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            mdiClient1.ControlAdded += new ControlEventHandler(MdiClientControlAddRemove);
            mdiClient1.ControlRemoved += new ControlEventHandler(MdiClientControlAddRemove);
            AppCommandTheme_Executed(this.buttonStyleOffice2010Blue,null);
            RibbonStateCommand_Executed(null,null);
            labelItem1.Text += globalInfoClass.UserName;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mdiClient1 = new System.Windows.Forms.MdiClient();
            this.ribbonControl1 = new DevComponents.DotNetBar.RibbonControl();
            this.ribbonPanel2 = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonBar6 = new DevComponents.DotNetBar.RibbonBar();
            this.ribbonPanel1 = new DevComponents.DotNetBar.RibbonPanel();
            this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.ribbonTabContext = new DevComponents.DotNetBar.RibbonTabItem();
            this.ribbonTabItemGroup1 = new DevComponents.DotNetBar.RibbonTabItemGroup();
            this.ribbonTabItem1 = new DevComponents.DotNetBar.RibbonTabItem();
            this.buttonChangeStyle = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem17 = new DevComponents.DotNetBar.ButtonItem();
            this.AppCommandTheme = new DevComponents.DotNetBar.Command(this.components);
            this.buttonStyleMetro = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2010Blue = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2010Silver = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2010Black = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleVS2010 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem62 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2007Blue = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2007Black = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleOffice2007Silver = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem60 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonStyleCustom = new DevComponents.DotNetBar.ColorPickerDropDown();
            this.btn_reset = new DevComponents.DotNetBar.ButtonItem();
            this.btn_close = new DevComponents.DotNetBar.ButtonItem();
            this.switchButtonItem1 = new DevComponents.DotNetBar.SwitchButtonItem();
            this.RibbonStateCommand = new DevComponents.DotNetBar.Command(this.components);
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.buttonItem47 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem48 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem49 = new DevComponents.DotNetBar.ButtonItem();
            this.AppCommandExit = new DevComponents.DotNetBar.Command(this.components);
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.ribbonControl1.SuspendLayout();
            this.ribbonPanel2.SuspendLayout();
            this.ribbonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // mdiClient1
            // 
            this.mdiClient1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mdiClient1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdiClient1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mdiClient1.Location = new System.Drawing.Point(5, 122);
            this.mdiClient1.Name = "mdiClient1";
            this.mdiClient1.Size = new System.Drawing.Size(802, 376);
            this.mdiClient1.TabIndex = 5;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.BackColor = System.Drawing.SystemColors.Control;
            this.ribbonControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ribbonControl1.BackgroundImage")));
            // 
            // 
            // 
            this.ribbonControl1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonControl1.CaptionVisible = true;
            this.ribbonControl1.Controls.Add(this.ribbonPanel2);
            this.ribbonControl1.Controls.Add(this.ribbonPanel1);
            this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonControl1.ForeColor = System.Drawing.Color.Black;
            this.ribbonControl1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.ribbonTabContext,
            this.ribbonTabItem1,
            this.buttonChangeStyle,
            this.btn_reset,
            this.btn_close,
            this.switchButtonItem1});
            this.ribbonControl1.KeyTipsFont = new System.Drawing.Font("Symbol", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonControl1.Location = new System.Drawing.Point(5, 1);
            this.ribbonControl1.MdiSystemItemVisible = false;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.QuickToolbarItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1});
            this.ribbonControl1.RibbonStripFont = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonControl1.Size = new System.Drawing.Size(802, 121);
            this.ribbonControl1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonControl1.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.ribbonControl1.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.ribbonControl1.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.ribbonControl1.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.ribbonControl1.SystemText.QatDialogAddButton = "&Add >>";
            this.ribbonControl1.SystemText.QatDialogCancelButton = "Cancel";
            this.ribbonControl1.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.ribbonControl1.SystemText.QatDialogOkButton = "OK";
            this.ribbonControl1.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatDialogRemoveButton = "&Remove";
            this.ribbonControl1.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.ribbonControl1.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.ribbonControl1.TabGroupHeight = 14;
            this.ribbonControl1.TabGroups.AddRange(new DevComponents.DotNetBar.RibbonTabItemGroup[] {
            this.ribbonTabItemGroup1});
            this.ribbonControl1.TabGroupsVisible = true;
            this.ribbonControl1.TabIndex = 8;
            // 
            // ribbonPanel2
            // 
            this.ribbonPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel2.Controls.Add(this.ribbonBar6);
            this.ribbonPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel2.Location = new System.Drawing.Point(0, 56);
            this.ribbonPanel2.Name = "ribbonPanel2";
            this.ribbonPanel2.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.ribbonPanel2.Size = new System.Drawing.Size(802, 65);
            // 
            // 
            // 
            this.ribbonPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel2.TabIndex = 4;
            // 
            // ribbonBar6
            // 
            this.ribbonBar6.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar6.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar6.ContainerControlProcessDialogKey = true;
            this.ribbonBar6.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBar6.DragDropSupport = true;
            this.ribbonBar6.Location = new System.Drawing.Point(3, 0);
            this.ribbonBar6.Name = "ribbonBar6";
            this.ribbonBar6.Size = new System.Drawing.Size(793, 63);
            this.ribbonBar6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBar6.TabIndex = 0;
            // 
            // 
            // 
            this.ribbonBar6.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar6.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonPanel1.Controls.Add(this.ribbonBar1);
            this.ribbonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ribbonPanel1.Location = new System.Drawing.Point(0, 60);
            this.ribbonPanel1.Name = "ribbonPanel1";
            this.ribbonPanel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.ribbonPanel1.Size = new System.Drawing.Size(802, 61);
            // 
            // 
            // 
            this.ribbonPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonPanel1.TabIndex = 5;
            this.ribbonPanel1.Visible = false;
            // 
            // ribbonBar1
            // 
            this.ribbonBar1.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar1.ContainerControlProcessDialogKey = true;
            this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.ribbonBar1.DragDropSupport = true;
            this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem6,
            this.buttonItem4});
            this.ribbonBar1.Location = new System.Drawing.Point(3, 0);
            this.ribbonBar1.Name = "ribbonBar1";
            this.ribbonBar1.Size = new System.Drawing.Size(799, 59);
            this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBar1.TabIndex = 0;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // buttonItem6
            // 
            this.buttonItem6.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem6.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem6.Image")));
            this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.SubItemsExpandWidth = 14;
            this.buttonItem6.Text = "修改密码";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem4.Image")));
            this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.SubItemsExpandWidth = 14;
            this.buttonItem4.Text = "个人设置";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // ribbonTabContext
            // 
            this.ribbonTabContext.Checked = true;
            this.ribbonTabContext.ColorTable = DevComponents.DotNetBar.eRibbonTabColor.Orange;
            this.ribbonTabContext.Group = this.ribbonTabItemGroup1;
            this.ribbonTabContext.Name = "ribbonTabContext";
            this.ribbonTabContext.Panel = this.ribbonPanel2;
            this.ribbonTabContext.Text = "功能菜单";
            // 
            // ribbonTabItemGroup1
            // 
            this.ribbonTabItemGroup1.Color = DevComponents.DotNetBar.eRibbonTabGroupColor.Orange;
            this.ribbonTabItemGroup1.GroupTitle = "Tab Group";
            this.ribbonTabItemGroup1.Name = "ribbonTabItemGroup1";
            // 
            // 
            // 
            this.ribbonTabItemGroup1.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(158)))), ((int)(((byte)(159)))));
            this.ribbonTabItemGroup1.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(225)))), ((int)(((byte)(226)))));
            this.ribbonTabItemGroup1.Style.BackColorGradientAngle = 90;
            this.ribbonTabItemGroup1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup1.Style.BorderBottomWidth = 1;
            this.ribbonTabItemGroup1.Style.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(58)))), ((int)(((byte)(59)))));
            this.ribbonTabItemGroup1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup1.Style.BorderLeftWidth = 1;
            this.ribbonTabItemGroup1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup1.Style.BorderRightWidth = 1;
            this.ribbonTabItemGroup1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.ribbonTabItemGroup1.Style.BorderTopWidth = 1;
            this.ribbonTabItemGroup1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonTabItemGroup1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.ribbonTabItemGroup1.Style.TextColor = System.Drawing.Color.Black;
            this.ribbonTabItemGroup1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // ribbonTabItem1
            // 
            this.ribbonTabItem1.Name = "ribbonTabItem1";
            this.ribbonTabItem1.Panel = this.ribbonPanel1;
            this.ribbonTabItem1.Text = "系统设置";
            // 
            // buttonChangeStyle
            // 
            this.buttonChangeStyle.AutoExpandOnClick = true;
            this.buttonChangeStyle.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.buttonChangeStyle.Name = "buttonChangeStyle";
            this.buttonChangeStyle.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem17,
            this.buttonStyleMetro,
            this.buttonStyleOffice2010Blue,
            this.buttonStyleOffice2010Silver,
            this.buttonStyleOffice2010Black,
            this.buttonStyleVS2010,
            this.buttonItem62,
            this.buttonStyleOffice2007Blue,
            this.buttonStyleOffice2007Black,
            this.buttonStyleOffice2007Silver,
            this.buttonItem60,
            this.buttonItem16,
            this.buttonStyleCustom});
            this.buttonChangeStyle.Text = "Style";
            this.buttonChangeStyle.Visible = false;
            // 
            // buttonItem17
            // 
            this.buttonItem17.Command = this.AppCommandTheme;
            this.buttonItem17.CommandParameter = "Office2016";
            this.buttonItem17.Name = "buttonItem17";
            this.buttonItem17.OptionGroup = "style";
            this.buttonItem17.Text = "Office 2016";
            // 
            // AppCommandTheme
            // 
            this.AppCommandTheme.Name = "AppCommandTheme";
            this.AppCommandTheme.Executed += new System.EventHandler(this.AppCommandTheme_Executed);
            // 
            // buttonStyleMetro
            // 
            this.buttonStyleMetro.Command = this.AppCommandTheme;
            this.buttonStyleMetro.CommandParameter = "Metro";
            this.buttonStyleMetro.Name = "buttonStyleMetro";
            this.buttonStyleMetro.OptionGroup = "style";
            this.buttonStyleMetro.Text = "Metro/Office 2013";
            // 
            // buttonStyleOffice2010Blue
            // 
            this.buttonStyleOffice2010Blue.Checked = true;
            this.buttonStyleOffice2010Blue.Command = this.AppCommandTheme;
            this.buttonStyleOffice2010Blue.CommandParameter = "Office2010Blue";
            this.buttonStyleOffice2010Blue.Name = "buttonStyleOffice2010Blue";
            this.buttonStyleOffice2010Blue.OptionGroup = "style";
            this.buttonStyleOffice2010Blue.Text = "Office 2010 Blue";
            // 
            // buttonStyleOffice2010Silver
            // 
            this.buttonStyleOffice2010Silver.Command = this.AppCommandTheme;
            this.buttonStyleOffice2010Silver.CommandParameter = "Office2010Silver";
            this.buttonStyleOffice2010Silver.Name = "buttonStyleOffice2010Silver";
            this.buttonStyleOffice2010Silver.OptionGroup = "style";
            this.buttonStyleOffice2010Silver.Text = "Office 2010 <font color=\"Silver\"><b>Silver</b></font>";
            // 
            // buttonStyleOffice2010Black
            // 
            this.buttonStyleOffice2010Black.Command = this.AppCommandTheme;
            this.buttonStyleOffice2010Black.CommandParameter = "Office2010Black";
            this.buttonStyleOffice2010Black.Name = "buttonStyleOffice2010Black";
            this.buttonStyleOffice2010Black.OptionGroup = "style";
            this.buttonStyleOffice2010Black.Text = "Office 2010 Black";
            // 
            // buttonStyleVS2010
            // 
            this.buttonStyleVS2010.Command = this.AppCommandTheme;
            this.buttonStyleVS2010.CommandParameter = "VisualStudio2010Blue";
            this.buttonStyleVS2010.Name = "buttonStyleVS2010";
            this.buttonStyleVS2010.OptionGroup = "style";
            this.buttonStyleVS2010.Text = "Visual Studio 2010";
            // 
            // buttonItem62
            // 
            this.buttonItem62.Command = this.AppCommandTheme;
            this.buttonItem62.CommandParameter = "Windows7Blue";
            this.buttonItem62.Name = "buttonItem62";
            this.buttonItem62.OptionGroup = "style";
            this.buttonItem62.Text = "Windows 7";
            // 
            // buttonStyleOffice2007Blue
            // 
            this.buttonStyleOffice2007Blue.Command = this.AppCommandTheme;
            this.buttonStyleOffice2007Blue.CommandParameter = "Office2007Blue";
            this.buttonStyleOffice2007Blue.Name = "buttonStyleOffice2007Blue";
            this.buttonStyleOffice2007Blue.OptionGroup = "style";
            this.buttonStyleOffice2007Blue.Text = "Office 2007 <font color=\"Blue\"><b>Blue</b></font>";
            // 
            // buttonStyleOffice2007Black
            // 
            this.buttonStyleOffice2007Black.Command = this.AppCommandTheme;
            this.buttonStyleOffice2007Black.CommandParameter = "Office2007Black";
            this.buttonStyleOffice2007Black.Name = "buttonStyleOffice2007Black";
            this.buttonStyleOffice2007Black.OptionGroup = "style";
            this.buttonStyleOffice2007Black.Text = "Office 2007 <font color=\"black\"><b>Black</b></font>";
            // 
            // buttonStyleOffice2007Silver
            // 
            this.buttonStyleOffice2007Silver.Command = this.AppCommandTheme;
            this.buttonStyleOffice2007Silver.CommandParameter = "Office2007Silver";
            this.buttonStyleOffice2007Silver.Name = "buttonStyleOffice2007Silver";
            this.buttonStyleOffice2007Silver.OptionGroup = "style";
            this.buttonStyleOffice2007Silver.Text = "Office 2007 <font color=\"Silver\"><b>Silver</b></font>";
            // 
            // buttonItem60
            // 
            this.buttonItem60.Command = this.AppCommandTheme;
            this.buttonItem60.CommandParameter = "Office2007VistaGlass";
            this.buttonItem60.Name = "buttonItem60";
            this.buttonItem60.OptionGroup = "style";
            this.buttonItem60.Text = "Vista Glass";
            // 
            // buttonItem16
            // 
            this.buttonItem16.Command = this.AppCommandTheme;
            this.buttonItem16.CommandParameter = "VisualStudio2012Light";
            this.buttonItem16.Name = "buttonItem16";
            this.buttonItem16.OptionGroup = "style";
            this.buttonItem16.Text = "Visual Studio 2012 Light";
            // 
            // buttonStyleCustom
            // 
            this.buttonStyleCustom.BeginGroup = true;
            this.buttonStyleCustom.Command = this.AppCommandTheme;
            this.buttonStyleCustom.Name = "buttonStyleCustom";
            this.buttonStyleCustom.Text = "Custom scheme";
            this.buttonStyleCustom.Tooltip = "Custom color scheme is created based on currently selected color table. Try selec" +
    "ting Silver or Blue color table and then creating custom color scheme.";
            this.buttonStyleCustom.SelectedColorChanged += new System.EventHandler(this.buttonStyleCustom_SelectedColorChanged);
            this.buttonStyleCustom.ColorPreview += new DevComponents.DotNetBar.ColorPreviewEventHandler(this.buttonStyleCustom_ColorPreview);
            this.buttonStyleCustom.ExpandChange += new System.EventHandler(this.buttonStyleCustom_ExpandChange);
            // 
            // btn_reset
            // 
            this.btn_reset.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Text = "重新登录";
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_close
            // 
            this.btn_close.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.btn_close.Name = "btn_close";
            this.btn_close.Text = "退出系统";
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // switchButtonItem1
            // 
            this.switchButtonItem1.ButtonHeight = 20;
            this.switchButtonItem1.ButtonWidth = 62;
            this.switchButtonItem1.Command = this.RibbonStateCommand;
            this.switchButtonItem1.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.switchButtonItem1.Margin.Bottom = 2;
            this.switchButtonItem1.Margin.Left = 4;
            this.switchButtonItem1.Name = "switchButtonItem1";
            this.switchButtonItem1.OffText = "MAX";
            this.switchButtonItem1.OnText = "MIN";
            this.switchButtonItem1.SwitchBackColor = System.Drawing.Color.Firebrick;
            this.switchButtonItem1.Tooltip = "Minimizes/Maximizes the Ribbon";
            // 
            // RibbonStateCommand
            // 
            this.RibbonStateCommand.Name = "RibbonStateCommand";
            this.RibbonStateCommand.Executed += new System.EventHandler(this.RibbonStateCommand_Executed);
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "当前登录人：";
            // 
            // buttonItem47
            // 
            this.buttonItem47.BeginGroup = true;
            this.buttonItem47.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem47.Image")));
            this.buttonItem47.Name = "buttonItem47";
            this.buttonItem47.Text = "Search for Templates Online...";
            // 
            // buttonItem48
            // 
            this.buttonItem48.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem48.Image")));
            this.buttonItem48.Name = "buttonItem48";
            this.buttonItem48.Text = "Browse for Templates...";
            // 
            // buttonItem49
            // 
            this.buttonItem49.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem49.Image")));
            this.buttonItem49.Name = "buttonItem49";
            this.buttonItem49.Text = "Save Current Template...";
            // 
            // AppCommandExit
            // 
            this.AppCommandExit.Name = "AppCommandExit";
            this.AppCommandExit.Executed += new System.EventHandler(this.AppCommandExit_Executed);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2016;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))), System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199))))));
            // 
            // superTabControl1
            // 
            this.superTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.ForeColor = System.Drawing.Color.Black;
            this.superTabControl1.Location = new System.Drawing.Point(5, 122);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(802, 376);
            this.superTabControl1.TabFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControl1.TabIndex = 9;
            this.superTabControl1.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.WinMediaPlayer12;
            this.superTabControl1.Text = "superTabControl1";
            this.superTabControl1.TabStripMouseDoubleClick += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.superTabControl1_TabStripMouseDoubleClick);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(812, 500);
            this.Controls.Add(this.superTabControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.mdiClient1);
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "孕产妇保健管理系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ribbonControl1.ResumeLayout(false);
            this.ribbonControl1.PerformLayout();
            this.ribbonPanel2.ResumeLayout(false);
            this.ribbonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            this.Text = "儿童保健管理系统V" + OperatFile.GetIniFileString("DataBase", "version", "", Application.StartupPath + "\\version.ini");
            if(globalInfoClass.Ward_name=="儿保室")
            SetMdiForm("医生工作站", typeof(WomenInfo));
            else if (globalInfoClass.Ward_name == "护士站")
                SetMdiForm("护士工作站", typeof(hs_WomenInfo));
            else if (globalInfoClass.Ward_name == "测评室")
                SetMdiForm("测评工作站", typeof(cp_WomenInfo));
            else if (globalInfoClass.Ward_name == "训练室")
                SetMdiForm("训练工作站", typeof(WomenInfo));
            //if (!String.IsNullOrEmpty(globalInfoClass.Department.name))
            //{
            //    if (globalInfoClass.Department.name.Contains("产房") || globalInfoClass.Department.name.Contains("住院"))
            //    {
            //        buttonItem2_Click(sender, e);
            //    }
            //    else
            //    {
            //        buttonItem1_Click(sender, e);
            //    }
            //}
            //else
            //{
            //    buttonItem1_Click(sender, e);
            //}
            sysmenuBll menubll = new sysmenuBll();
            IList<SYS_MENUS> list = menubll.GetListBySql("系统设置",globalInfoClass.User_Role);
            foreach (SYS_MENUS obj in list)
            {
                ButtonItem btm = new ButtonItem();
                btm.ButtonStyle = eButtonStyle.ImageAndText;
                if (File.Exists(@Application.StartupPath + "\\" + obj.menu_image))
                {
                    btm.Image = Image.FromFile(Application.StartupPath + "\\" + obj.menu_image);
                }
                btm.ImagePosition = eImagePosition.Top;
                btm.Name = obj.menu_code;
                btm.SubItemsExpandWidth = 14;
                btm.Text = obj.menu_name;
                btm.Tag = obj;
                btm.Click += new System.EventHandler(this.btm_Click);
                this.ribbonBar1.Items.Add(btm);
            }
            
            IList<SYS_MENUS> listgn = menubll.GetListBySql("功能菜单", globalInfoClass.User_Role);
            foreach (SYS_MENUS obj in listgn)
            {
                ButtonItem btm = new ButtonItem();
                btm.ButtonStyle = eButtonStyle.ImageAndText;
                if (File.Exists(@Application.StartupPath + "\\" + obj.menu_image))
                {
                    btm.Image = Image.FromFile(Application.StartupPath + "\\" + obj.menu_image);
                }
                btm.ImagePosition = eImagePosition.Top;
                btm.Name = obj.menu_code;
                btm.SubItemsExpandWidth = 14;
                btm.Text = obj.menu_name;
                btm.Tag = obj;
                btm.Click += new System.EventHandler(this.btm_Click);
                this.ribbonBar6.Items.Add(btm);
            }
        }

        private void btm_Click(object sender, EventArgs e)
        {
            ButtonItem tsm = sender as ButtonItem;
            SYS_MENUS menuobj = tsm.Tag as SYS_MENUS;
            SetMdiForm(menuobj.MENU_NAME, Type.GetType(menuobj.MENU_URL));
        }

        private void MdiClientControlAddRemove(object sender, ControlEventArgs e)
        {
            if (this.MdiChildren.Length > 0)
            {
                if (!ribbonTabContext.Visible)
                {
                    ribbonTabContext.Visible = true;
                    ribbonControl1.RecalcLayout();
                }
            }
            else
            {
                if (ribbonTabContext.Visible)
                {
                    if (ribbonTabContext.Checked)
                    ribbonTabContext.Visible = false;
                    ribbonControl1.RecalcLayout();
                }
            }
        }

        #region Automatic Color Scheme creation based on the selected color table
        private bool m_ColorSelected = false;
        private eStyle m_BaseStyle = eStyle.Office2010Silver;
        private void buttonStyleCustom_ExpandChange(object sender, System.EventArgs e)
        {
            if (buttonStyleCustom.Expanded)
            {
                // Remember the starting color scheme to apply if no color is selected during live-preview
                m_ColorSelected = false;
                m_BaseStyle = StyleManager.Style;
            }
            else
            {
                if (!m_ColorSelected)
                {
                    if (StyleManager.IsMetro(StyleManager.Style))
                        StyleManager.MetroColorGeneratorParameters = DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters.Default;
                    else
                        StyleManager.ChangeStyle(m_BaseStyle, Color.Empty);
                }
            }
        }

        private void buttonStyleCustom_ColorPreview(object sender, DevComponents.DotNetBar.ColorPreviewEventArgs e)
        {
            if (StyleManager.IsMetro(StyleManager.Style))
            {
                Color baseColor = e.Color;
                StyleManager.MetroColorGeneratorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.White, baseColor);
            }
            else
                StyleManager.ColorTint = e.Color;
        }

        private void buttonStyleCustom_SelectedColorChanged(object sender, System.EventArgs e)
        {
            m_ColorSelected = true; // Indicate that color was selected for buttonStyleCustom_ExpandChange method
            buttonStyleCustom.CommandParameter = buttonStyleCustom.SelectedColor;
        }
        #endregion

        #region Commands Implementation

        private void AppCommandTheme_Executed(object sender, EventArgs e)
        {
            ICommandSource source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {
                eStyle style = (eStyle)Enum.Parse(typeof(eStyle), source.CommandParameter.ToString());
                // Using StyleManager change the style and color tinting
                if (StyleManager.IsMetro(style))
                {
                    // More customization is needed for Metro
                    // Capitalize App Button and tab
                    foreach (BaseItem item in RibbonControl.Items)
                    {
                        // Ribbon Control may contain items other than tabs so that needs to be taken in account
                        RibbonTabItem tab = item as RibbonTabItem;
                        if (tab != null)
                            tab.Text = tab.Text.ToUpper();
                    }
                    

                    ribbonControl1.RibbonStripFont = new System.Drawing.Font("Segoe UI", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    if (style == eStyle.Metro)
                        StyleManager.MetroColorGeneratorParameters = DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters.DarkBlue;

                    // Adjust size of switch button to match Metro styling
                    switchButtonItem1.SwitchWidth = 16;
                    switchButtonItem1.ButtonWidth = 48;
                    switchButtonItem1.ButtonHeight = 19;
                    

                    StyleManager.Style = style; // BOOM
                }
                else
                {
                    // If previous style was Metro we need to update other properties as well
                    if (StyleManager.IsMetro(StyleManager.Style))
                    {
                        ribbonControl1.RibbonStripFont = null;
                        // Fix capitalization App Button and tab
                        foreach (BaseItem item in RibbonControl.Items)
                        {
                            // Ribbon Control may contain items other than tabs so that needs to be taken in account
                            RibbonTabItem tab = item as RibbonTabItem;
                            if (tab != null)
                                tab.Text = ToTitleCase(tab.Text);
                        }
                        // Adjust size of switch button to match Office styling
                        switchButtonItem1.SwitchWidth = 28;
                        switchButtonItem1.ButtonWidth = 62;
                        switchButtonItem1.ButtonHeight = 20;
                    }
                    StyleManager.ChangeStyle(style, Color.Empty);
                }
            }
            else if (source.CommandParameter is Color)
            {
                if (StyleManager.IsMetro(StyleManager.Style))
                    StyleManager.MetroColorGeneratorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(Color.White, (Color)source.CommandParameter);
                else
                    StyleManager.ColorTint = (Color)source.CommandParameter;
            }
        }

        private string ToTitleCase(string text)
        {
            if (text.Contains("&"))
            {
                int ampPosition = text.IndexOf('&');
                text = text.Replace("&", "");
                text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
                if (ampPosition > 0)
                    text = text.Substring(0, ampPosition) + "&" + text.Substring(ampPosition);
                else
                    text = "&" + text;
                return text;
            }
            else
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
        }

        private void AppCommandExit_Executed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RibbonStateCommand_Executed(object sender, EventArgs e)
        {
            ribbonControl1.Expanded = RibbonStateCommand.Checked;
            RibbonStateCommand.Checked = !RibbonStateCommand.Checked;
        }
        #endregion

        /// <summary>
        /// 创建或者显示一个多文档界面页面
        /// </summary>
        /// <param name="caption">窗体标题</param>
        /// <param name="formType">窗体类型</param>
        public void SetMdiForm(string caption, Type formType, params object[] args)
        {
            bool IsOpened = false;

            //遍历现有的Tab页面，如果存在，那么设置为选中即可
            foreach (SuperTabItem tabitem in superTabControl1.Tabs)
            {
                if (tabitem.Name == caption)
                {
                    superTabControl1.SelectedTab = tabitem;
                    IsOpened = true;
                    break;
                }
            }

            //如果在现有Tab页面中没有找到，那么就要初始化了Tab页面了
            if (!IsOpened)
            {
                //为了方便管理，调用LoadMdiForm函数来创建一个新的窗体，并作为MDI的子窗体
                //然后分配给SuperTab控件，创建一个SuperTabItem并显示
                UserControl form = Activator.CreateInstance(formType, args) as UserControl;
                SuperTabItem tabItem = superTabControl1.CreateTab(caption);
                tabItem.Name = caption;
                tabItem.Text = caption;



                form.Visible = true;
                form.Dock = DockStyle.Fill;
                form.AutoScroll = true;
                //tabItem.Icon = form.Icon;
                tabItem.AttachedControl.Controls.Add(form);

                superTabControl1.SelectedTab = tabItem;
            }
        }

        /// <summary>
        /// 刷新界面页面
        /// </summary>
        /// <param name="caption">窗体标题</param>
        /// <param name="formType">窗体类型</param>
        public void updateMdiForm(string caption, Type formType,object para)
        {
            bool IsOpened = false;

            //遍历现有的Tab页面，如果存在，那么设置为选中即可
            foreach (SuperTabItem tabitem in superTabControl1.Tabs)
            {
                if (tabitem.Name == caption)
                {
                    superTabControl1.SelectedTab = tabitem;
                    //tabitem.Dispose();
                    IsOpened = true;
                    UserControl form;
                    if(para==null)
                    form= Activator.CreateInstance(formType) as UserControl;
                    else
                        form = Activator.CreateInstance(formType,new object[] { para}) as UserControl;
                    //form.Dispose();//控件使用完后Dispose。
                    tabitem.AttachedControl.Controls[0].Dispose();
                    tabitem.AttachedControl.Controls.Clear();

                    form.Visible = true;
                    form.Dock = DockStyle.Fill;
                    form.AutoScroll = true;
                    tabitem.AttachedControl.Controls.Add(form);

                    break;
                }
            }

            //如果在现有Tab页面中没有找到，那么就要初始化了Tab页面了
            if (!IsOpened)
            {
                //为了方便管理，调用LoadMdiForm函数来创建一个新的窗体，并作为MDI的子窗体
                //然后分配给SuperTab控件，创建一个SuperTabItem并显示
                UserControl form;
                if (para == null)
                    form = Activator.CreateInstance(formType) as UserControl;
                else
                    form = Activator.CreateInstance(formType, new object[] { para }) as UserControl;
                SuperTabItem tabItem = superTabControl1.CreateTab(caption);
                tabItem.Name = caption;
                tabItem.Text = caption;
                
                form.Visible = true;
                form.Dock = DockStyle.Fill;
                form.AutoScroll = true;
                //tabItem.Icon = form.Icon;
                tabItem.AttachedControl.Controls.Add(form);

                superTabControl1.SelectedTab = tabItem;
            }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            SetMdiForm("护士工作站", typeof(NursInfo));
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            SetMdiForm("医生工作站", typeof(WomenInfo));
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SetMdiForm("统计报表", typeof(tongji_MainInfo));
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("退出儿童保健管理系统？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            FrmPassWord changepassword = new FrmPassWord();
            if (changepassword.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void superTabControl1_TabStripMouseDoubleClick(object sender, MouseEventArgs e)
        {
            SuperTabItem tabitem = superTabControl1.SelectedTab;
            if(tabitem.Name!= " 首 页 ")
            tabitem.Close();
            GC.Collect();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            if (MessageBoxEx.Show("确定要重新登录吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Application.Restart();
        }

        private void buttonItem3_Click_1(object sender, EventArgs e)
        {
            SetMdiForm(" 首 页 ", typeof(firstpage));
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            FrmConfig config = new FrmConfig();
            if (config.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            SetMdiForm("测评室工作站", typeof(cp_WomenInfo));
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            SetMdiForm("训练室工作站", typeof(cp_WomenInfo));
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            SetMdiForm("高危专案", typeof(cp_WomenInfo));
        }
    }
}

