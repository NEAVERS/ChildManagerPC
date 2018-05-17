using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YCF.BLL.sys;
using YCF.Model;
using System.Reflection;
using System.IO;
using YCF.Common;

namespace ChildManager.UI.tongji
{
    public partial class tongji_MainInfo : UserControl
    {
        public tongji_MainInfo()
        {
            InitializeComponent();
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


        private void tongji_MainInfo_Load(object sender, EventArgs e)
        {
            try
            {
                sysmenuBll menubll = new sysmenuBll();
                IList<sys_menus> list = menubll.GetList("统计报表");
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
                //Type ty = Assembly.Load("login").GetType(string.Format("UI.xinxitongji.{0}", menuobj.menu_url));
                //Object obj = Activator.CreateInstance(ty);
                UserControl uc;
                if (!String.IsNullOrEmpty(menuobj.menu_para))
                {
                    object[] objpara = new object[] { menuobj.menu_para };
                    uc = Activator.CreateInstance(Type.GetType(menuobj.menu_url), objpara) as UserControl;
                }
                else
                {
                    uc = Activator.CreateInstance(Type.GetType(menuobj.menu_url)) as UserControl;
                }

                uc.Dock = DockStyle.Fill;
                this.pnlContent.Controls.Clear();
                this.pnlContent.Controls.Add(uc);
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
                string bllname = menuobj.menu_url.Substring(menuobj.menu_url.LastIndexOf(".")).Trim('.');
                Assembly ass;
                //Type type;
                object obj;
                if (File.Exists(Application.StartupPath + "\\reportmodel\\" + bllname + ".dll"))
                {
                    byte[] buffer = System.IO.File.ReadAllBytes(Application.StartupPath + "\\reportmodel\\" + bllname + ".dll");
                    //获取并加载DLL类库中的程序集
                    ass = Assembly.Load(buffer);

                    //获取类的类型：必须使用名称空间+类名称
                    //type = ass.GetType("login.UI.tempreport." + menuobj.menu_url);

                    //对获取的类进行创建实例。//必须使用名称空间+类名称
                    //UserControl uc = Activator.CreateInstance(Type.GetType("tongji_gaowei.tempreport.tongji_gaowei")) as UserControl;

                    if (!String.IsNullOrEmpty(menuobj.menu_para))
                    {
                        object[] objpara = new object[] { menuobj.menu_para };
                        obj = ass.CreateInstance(menuobj.menu_url, false, BindingFlags.Default, null, objpara, null, null);

                    }
                    else
                    {
                        obj = ass.CreateInstance(menuobj.menu_url);
                    }
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
                object[] args = null;
                if (!String.IsNullOrEmpty(menuobj.menu_para))
                {
                    args = new object[] { menuobj.menu_para };
                }

                UserControl uc = Activator.CreateInstance(Type.GetType(menuobj.menu_url), args) as UserControl;
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
