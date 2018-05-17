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
                sys_menus menuobj = tsdb.Tag as sys_menus;
                tsdb.Text = menuobj.menu_name;
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
                IList<sys_menus> list = menubll.GetListBySql("预约工作站",globalInfoClass.User_Role);
                foreach (sys_menus obj in list)
                {
                    if (obj.menu_lever == "1")
                    {
                        if (String.IsNullOrEmpty(obj.menu_url))
                        {
                            ToolStripDropDownButton tsdb = new ToolStripDropDownButton();
                            if (File.Exists(@Application.StartupPath + "\\" + obj.menu_image))
                            {
                                tsdb.Image = Image.FromFile(Application.StartupPath + "\\" + obj.menu_image);
                            }
                            tsdb.Name = obj.menu_code;
                            tsdb.Size = new System.Drawing.Size(85, 29);
                            tsdb.Text = obj.menu_name;
                            tsdb.Tag = obj;
                            toolStrip2.Items.Add(tsdb);
                        }
                        else
                        {
                            ToolStripButton tsdb = new ToolStripButton();
                            if (File.Exists(@Application.StartupPath + "\\" + obj.menu_image))
                            {
                                tsdb.Image = Image.FromFile(Application.StartupPath + "\\" + obj.menu_image);
                            }
                            tsdb.Name = obj.menu_code;
                            tsdb.Size = new System.Drawing.Size(85, 29);
                            tsdb.Text = obj.menu_name;
                            tsdb.Tag = obj;
                            if (obj.is_custom == "2")//如果不是自定义的页面，调用已有的页面
                            {
                                tsdb.Click += new System.EventHandler(this.tsdb_Click);
                            }
                            else//否则调用文书生成界面
                            {
                                tsdb.Click += new System.EventHandler(this.tsdb_temp_Click);
                            }
                            toolStrip2.Items.Add(tsdb);
                            if (obj.is_default == "1")
                            {
                                tsdb.PerformClick();
                            }
                        }

                    }
                    else if (obj.menu_lever == "2")
                    {

                        ToolStripMenuItem tsm = new ToolStripMenuItem();
                        if (File.Exists(@Application.StartupPath + "\\" + obj.menu_image))
                        {
                            tsm.Image = Image.FromFile(Application.StartupPath + "\\" + obj.menu_image);
                        }
                        tsm.Name = obj.menu_code;
                        tsm.Size = new System.Drawing.Size(220, 22);
                        tsm.Text = obj.menu_name;
                        tsm.Tag = obj;
                        if (obj.is_custom == "2")//如果不是自定义的页面，调用已有的页面
                        {
                            tsm.Click += new System.EventHandler(this.tsm_Click);
                        }
                        else//否则调用文书生成界面
                        {
                            tsm.Click += new System.EventHandler(this.tsm_temp_Click);
                        }
                        foreach (ToolStripItem tsdb in toolStrip2.Items)
                        {
                            if (tsdb.Name == obj.menu_parent)
                            {
                                (tsdb as ToolStripDropDownButton).DropDownItems.Add(tsm);
                                break;
                            }
                        }
                        if (obj.is_default == "1")
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
            sys_menus menuobj = tsm.Tag as sys_menus;
            if (!tsm.Checked)
            {
                if (Type.GetType(menuobj.menu_url) == null)
                {
                    return;
                }
                CommonHelper.DisposeControls(pnlContent.Controls);

                UserControl uc;
                //Type ty = Assembly.Load("login").GetType(string.Format("UI.xinxitongji.{0}", menuobj.menu_url));
                //Object obj = Activator.CreateInstance(ty);
                if (!String.IsNullOrEmpty(menuobj.menu_para))
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.menu_url), new object[] { menuobj.menu_para }) as UserControl;
                }
                else
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.menu_url)) as UserControl;
                }
                uc.Dock = DockStyle.Fill;
                this.pnlContent.Controls.Clear();
                this.pnlContent.Controls.Add(uc);
                uc.Select();
                SetDefaultToolStrip();
                tsm.Checked = true;
                tsm.BackColor = Color.FromArgb(255, 199, 142);
                ToolStripDropDownButton tsdb = tsm.OwnerItem as ToolStripDropDownButton;
                sys_menus tsdbmenuobj = tsdb.Tag as sys_menus;
                tsdb.Text = tsdbmenuobj.menu_name + "： " + menuobj.menu_name;
                tsdb.BackColor = Color.FromArgb(255, 199, 142);
            }
        }

        private void tsm_temp_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = sender as ToolStripMenuItem;
            sys_menus menuobj = tsm.Tag as sys_menus;
            if (!tsm.Checked)
            {
                CommonHelper.DisposeControls(pnlContent.Controls);
                string bllname = menuobj.menu_url.Substring(menuobj.menu_url.LastIndexOf(".")).Trim('.');
                Assembly ass;
                //Type type;
                object obj;
                if (File.Exists(Application.StartupPath + "\\filemanage\\" + bllname + ".dll"))
                {
                    byte[] buffer = System.IO.File.ReadAllBytes(Application.StartupPath + "\\filemanage\\" + bllname + ".dll");
                    //获取并加载DLL类库中的程序集
                    ass = Assembly.Load(buffer);

                    //获取类的类型：必须使用名称空间+类名称
                    //type = ass.GetType("login.UI.tempreport." + menuobj.menu_url);

                    //对获取的类进行创建实例。//必须使用名称空间+类名称
                    //UserControl uc = Activator.CreateInstance(Type.GetType("tongji_gaowei.tempreport.tongji_gaowei")) as UserControl;
                    obj = ass.CreateInstance(menuobj.menu_url, false, BindingFlags.Default, null, new object[] { "xxx" }, null, null);
                    UserControl uc = obj as UserControl;
                    uc.Dock = DockStyle.Fill;
                    this.pnlContent.Controls.Clear();
                    this.pnlContent.Controls.Add(uc);
                }
                SetDefaultToolStrip();
                tsm.Checked = true;
                tsm.BackColor = Color.FromArgb(255, 199, 142);
                ToolStripDropDownButton tsdb = tsm.OwnerItem as ToolStripDropDownButton;
                sys_menus tsdbmenuobj = tsdb.Tag as sys_menus;
                tsdb.Text = tsdbmenuobj.menu_name + "： " + menuobj.menu_name;
                tsdb.BackColor = Color.FromArgb(255, 199, 142);
            }
        }

        private void tsdb_Click(object sender, EventArgs e)
        {
            ToolStripButton tsb = sender as ToolStripButton;
            sys_menus menuobj = tsb.Tag as sys_menus;
            if (!tsb.Checked)
            {
                if (Type.GetType(menuobj.menu_url) == null)
                {
                    return;
                }
                CommonHelper.DisposeControls(pnlContent.Controls);
                UserControl uc;
                //Type ty = Assembly.Load("login").GetType(string.Format("UI.xinxitongji.{0}", menuobj.menu_url));
                //Object obj = Activator.CreateInstance(ty);
                if (!String.IsNullOrEmpty(menuobj.menu_para))
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.menu_url), new object[] { menuobj.menu_para }) as UserControl;
                }
                else
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.menu_url)) as UserControl;
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
            sys_menus menuobj = tsm.Tag as sys_menus;
            if (!tsm.Checked)
            {
                CommonHelper.DisposeControls(pnlContent.Controls);
                string bllname = menuobj.menu_url.Substring(menuobj.menu_url.LastIndexOf(".")).Trim('.');
                Assembly ass;
                //Type type;
                object obj;
                if (File.Exists(Application.StartupPath + "\\filemanage\\" + bllname + ".dll"))
                {
                    byte[] buffer = System.IO.File.ReadAllBytes(Application.StartupPath + "\\filemanage\\" + bllname + ".dll");
                    //获取并加载DLL类库中的程序集
                    ass = Assembly.Load(buffer);

                    //获取类的类型：必须使用名称空间+类名称
                    //type = ass.GetType("login.UI.tempreport." + menuobj.menu_url);

                    //对获取的类进行创建实例。//必须使用名称空间+类名称
                    //UserControl uc = Activator.CreateInstance(Type.GetType("tongji_gaowei.tempreport.tongji_gaowei")) as UserControl;
                    obj = ass.CreateInstance(menuobj.menu_url, false, BindingFlags.Default, null, new object[] { "xxx" }, null, null);
                    UserControl uc = obj as UserControl;
                    uc.Dock = DockStyle.Fill;
                    this.pnlContent.Controls.Clear();
                    this.pnlContent.Controls.Add(uc);
                }
                SetDefaultToolStrip();
                tsm.Checked = true;
                tsm.BackColor = Color.FromArgb(255, 199, 142);
                ToolStripDropDownButton tsdb = tsm.OwnerItem as ToolStripDropDownButton;
                sys_menus tsdbmenuobj = tsdb.Tag as sys_menus;
                tsdb.Text = tsdbmenuobj.menu_name + "： " + menuobj.menu_name;
                tsdb.BackColor = Color.FromArgb(255, 199, 142);
            }
        }

    }
}
