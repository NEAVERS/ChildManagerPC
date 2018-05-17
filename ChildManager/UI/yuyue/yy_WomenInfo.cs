using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using YCF.BLL.sys;
using YCF.Model;
using YCF.BLL;
using YCF.Common;
using ChildManager.Model.ChildBaseInfo;

namespace ChildManager.UI.yuyue
{
    public partial class yy_WomenInfo : UserControl
    {
        public yy_WomenInfo()
        {
            InitializeComponent();
            refreshInfo();
        }

        private void SetDefaultToolStrip()
        {
            foreach (ToolStripItem tsdb in toolStrip2.Items)
            {
                SYS_MENUS menuobj = tsdb.Tag as SYS_MENUS;
                tsdb.Text = menuobj.MENU_NAME;
                tsdb.BackColor = System.Drawing.Color.Transparent;
                if (tsdb is ToolStripDropDownButton)
                {
                    foreach (ToolStripMenuItem tsmi in (tsdb as ToolStripDropDownButton).DropDownItems)
                    {
                        tsmi.BackColor = System.Drawing.Color.Transparent;
                        tsmi.Checked = false;
                    }
                }
                else if (tsdb is ToolStripButton)
                {
                    (tsdb as ToolStripButton).Checked = false;
                }
            }
        }

        //主窗体加载时自动打开基本信息的一般信息窗口
        private void refreshInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                sysmenuBll menubll = new sysmenuBll();
                IList<SYS_MENUS> list = menubll.GetListBySql("预约工作站",globalInfoClass.User_Role);
                foreach (SYS_MENUS obj in list)
                {
                    if (obj.MENU_LEVER == "1")
                    {
                        if (String.IsNullOrEmpty(obj.MENU_URL))
                        {
                            ToolStripDropDownButton tsdb = new ToolStripDropDownButton();
                            if (File.Exists(@Application.StartupPath + "\\" + obj.MENU_IMAGE))
                            {
                                tsdb.Image = Image.FromFile(Application.StartupPath + "\\" + obj.MENU_IMAGE);
                            }
                            tsdb.Name = obj.MENU_CODE;
                            tsdb.Size = new System.Drawing.Size(85, 29);
                            tsdb.Text = obj.MENU_NAME;
                            tsdb.Tag = obj;
                            toolStrip2.Items.Add(tsdb);
                        }
                        else
                        {
                            ToolStripButton tsdb = new ToolStripButton();
                            if (File.Exists(@Application.StartupPath + "\\" + obj.MENU_IMAGE))
                            {
                                tsdb.Image = Image.FromFile(Application.StartupPath + "\\" + obj.MENU_IMAGE);
                            }
                            tsdb.Name = obj.MENU_CODE;
                            tsdb.Size = new System.Drawing.Size(85, 29);
                            tsdb.Text = obj.MENU_NAME;
                            tsdb.Tag = obj;
                            if (obj.IS_CUSTOM == "2")//如果不是自定义的页面，调用已有的页面
                            {
                                tsdb.Click += new System.EventHandler(this.tsdb_Click);
                            }
                            else//否则调用文书生成界面
                            {
                                tsdb.Click += new System.EventHandler(this.tsdb_temp_Click);
                            }
                            toolStrip2.Items.Add(tsdb);
                            if (obj.IS_DEFAULT == "1")
                            {
                                tsdb.PerformClick();
                            }
                        }

                    }
                    else if (obj.MENU_LEVER == "2")
                    {

                        ToolStripMenuItem tsm = new ToolStripMenuItem();
                        if (File.Exists(@Application.StartupPath + "\\" + obj.MENU_IMAGE))
                        {
                            tsm.Image = Image.FromFile(Application.StartupPath + "\\" + obj.MENU_IMAGE);
                        }
                        tsm.Name = obj.MENU_CODE;
                        tsm.Size = new System.Drawing.Size(220, 22);
                        tsm.Text = obj.MENU_NAME;
                        tsm.Tag = obj;
                        if (obj.IS_CUSTOM == "2")//如果不是自定义的页面，调用已有的页面
                        {
                            tsm.Click += new System.EventHandler(this.tsm_Click);
                        }
                        else//否则调用文书生成界面
                        {
                            tsm.Click += new System.EventHandler(this.tsm_temp_Click);
                        }
                        foreach (ToolStripItem tsdb in toolStrip2.Items)
                        {
                            if (tsdb.Name == obj.MENU_PARENT)
                            {
                                (tsdb as ToolStripDropDownButton).DropDownItems.Add(tsm);
                                break;
                            }
                        }
                        if (obj.IS_DEFAULT == "1")
                        {
                            tsm.PerformClick();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常，请联系管理员！");
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void tsm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = sender as ToolStripMenuItem;
            SYS_MENUS menuobj = tsm.Tag as SYS_MENUS;
            if (!tsm.Checked)
            {
                if (Type.GetType(menuobj.MENU_URL) == null)
                {
                    return;
                }
                CommonHelper.DisposeControls(pnlContent.Controls);

                UserControl uc;
                //Type ty = Assembly.Load("login").GetType(string.Format("UI.xinxitongji.{0}", menuobj.MENU_URL));
                //Object obj = Activator.CreateInstance(ty);
                if (!String.IsNullOrEmpty(menuobj.MENU_PARA))
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.MENU_URL), new object[] { menuobj.MENU_PARA }) as UserControl;
                }
                else
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.MENU_URL)) as UserControl;
                }
                uc.Dock = DockStyle.Fill;
                this.pnlContent.Controls.Clear();
                this.pnlContent.Controls.Add(uc);
                uc.Select();
                SetDefaultToolStrip();
                tsm.Checked = true;
                tsm.BackColor = Color.FromArgb(255, 199, 142);
                ToolStripDropDownButton tsdb = tsm.OwnerItem as ToolStripDropDownButton;
                SYS_MENUS tsdbmenuobj = tsdb.Tag as SYS_MENUS;
                tsdb.Text = tsdbmenuobj.MENU_NAME + "： " + menuobj.MENU_NAME;
                tsdb.BackColor = Color.FromArgb(255, 199, 142);
            }
        }

        private void tsm_temp_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = sender as ToolStripMenuItem;
            SYS_MENUS menuobj = tsm.Tag as SYS_MENUS;
            if (!tsm.Checked)
            {
                CommonHelper.DisposeControls(pnlContent.Controls);
                string bllname = menuobj.MENU_URL.Substring(menuobj.MENU_URL.LastIndexOf(".")).Trim('.');
                Assembly ass;
                //Type type;
                object obj;
                if (File.Exists(Application.StartupPath + "\\filemanage\\" + bllname + ".dll"))
                {
                    byte[] buffer = System.IO.File.ReadAllBytes(Application.StartupPath + "\\filemanage\\" + bllname + ".dll");
                    //获取并加载DLL类库中的程序集
                    ass = Assembly.Load(buffer);

                    //获取类的类型：必须使用名称空间+类名称
                    //type = ass.GetType("login.UI.tempreport." + menuobj.MENU_URL);

                    //对获取的类进行创建实例。//必须使用名称空间+类名称
                    //UserControl uc = Activator.CreateInstance(Type.GetType("tongji_gaowei.tempreport.tongji_gaowei")) as UserControl;
                    obj = ass.CreateInstance(menuobj.MENU_URL, false, BindingFlags.Default, null, new object[] { "xxx" }, null, null);
                    UserControl uc = obj as UserControl;
                    uc.Dock = DockStyle.Fill;
                    this.pnlContent.Controls.Clear();
                    this.pnlContent.Controls.Add(uc);
                }
                SetDefaultToolStrip();
                tsm.Checked = true;
                tsm.BackColor = Color.FromArgb(255, 199, 142);
                ToolStripDropDownButton tsdb = tsm.OwnerItem as ToolStripDropDownButton;
                SYS_MENUS tsdbmenuobj = tsdb.Tag as SYS_MENUS;
                tsdb.Text = tsdbmenuobj.MENU_NAME + "： " + menuobj.MENU_NAME;
                tsdb.BackColor = Color.FromArgb(255, 199, 142);
            }
        }

        private void tsdb_Click(object sender, EventArgs e)
        {
            ToolStripButton tsb = sender as ToolStripButton;
            SYS_MENUS menuobj = tsb.Tag as SYS_MENUS;
            if (!tsb.Checked)
            {
                if (Type.GetType(menuobj.MENU_URL) == null)
                {
                    return;
                }
                CommonHelper.DisposeControls(pnlContent.Controls);
                UserControl uc;
                //Type ty = Assembly.Load("login").GetType(string.Format("UI.xinxitongji.{0}", menuobj.MENU_URL));
                //Object obj = Activator.CreateInstance(ty);
                if (!String.IsNullOrEmpty(menuobj.MENU_PARA))
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.MENU_URL), new object[] { menuobj.MENU_PARA }) as UserControl;
                }
                else
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.MENU_URL)) as UserControl;
                }
                uc.Dock = DockStyle.Fill;
                this.pnlContent.Controls.Clear();
                this.pnlContent.Controls.Add(uc);
                SetDefaultToolStrip();
                tsb.Checked = true;
                tsb.BackColor = Color.FromArgb(255, 199, 142);
                uc.Select();
            }
        }

        private void tsdb_temp_Click(object sender, EventArgs e)
        {

            ToolStripButton tsm = sender as ToolStripButton;
            SYS_MENUS menuobj = tsm.Tag as SYS_MENUS;
            if (!tsm.Checked)
            {
                CommonHelper.DisposeControls(pnlContent.Controls);
                string bllname = menuobj.MENU_URL.Substring(menuobj.MENU_URL.LastIndexOf(".")).Trim('.');
                Assembly ass;
                //Type type;
                object obj;
                if (File.Exists(Application.StartupPath + "\\filemanage\\" + bllname + ".dll"))
                {
                    byte[] buffer = System.IO.File.ReadAllBytes(Application.StartupPath + "\\filemanage\\" + bllname + ".dll");
                    //获取并加载DLL类库中的程序集
                    ass = Assembly.Load(buffer);

                    //获取类的类型：必须使用名称空间+类名称
                    //type = ass.GetType("login.UI.tempreport." + menuobj.MENU_URL);

                    //对获取的类进行创建实例。//必须使用名称空间+类名称
                    //UserControl uc = Activator.CreateInstance(Type.GetType("tongji_gaowei.tempreport.tongji_gaowei")) as UserControl;
                    obj = ass.CreateInstance(menuobj.MENU_URL, false, BindingFlags.Default, null, new object[] { "xxx" }, null, null);
                    UserControl uc = obj as UserControl;
                    uc.Dock = DockStyle.Fill;
                    this.pnlContent.Controls.Clear();
                    this.pnlContent.Controls.Add(uc);
                }
                SetDefaultToolStrip();
                tsm.Checked = true;
                tsm.BackColor = Color.FromArgb(255, 199, 142);
                ToolStripDropDownButton tsdb = tsm.OwnerItem as ToolStripDropDownButton;
                SYS_MENUS tsdbmenuobj = tsdb.Tag as SYS_MENUS;
                tsdb.Text = tsdbmenuobj.MENU_NAME + "： " + menuobj.MENU_NAME;
                tsdb.BackColor = Color.FromArgb(255, 199, 142);
            }
        }

    }
}
