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
using YCF.Model.NotMaps;
using ChildManager.UI.cepingshi;

namespace ChildManager.UI.xunlianshi
{
    public partial class xl_WomenInfo : UserControl
    {
        public int cd_id = -1;
        tb_childbasebll basebll = new tb_childbasebll();
        HisPatientInfo _hisobj = null;
        public xl_WomenInfo()
        {
            InitializeComponent();
            refreshInfo();
        }

        public xl_WomenInfo(object obj)
        {
            InitializeComponent();
            ChildBaseInfoObj baseobj = obj as ChildBaseInfoObj;
            cd_id = baseobj.id;
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
            bindDataNowday();//绑定左边列表数据
            try
            {
                sysmenuBll menubll = new sysmenuBll();
                IList<SYS_MENUS> list = menubll.GetListBySql("训练室工作站",globalInfoClass.User_Role);
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
                dataGridView1_CellEnter(null, null);
                Cursor.Current = Cursors.Default;
            }
        }

        private void tsm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = sender as ToolStripMenuItem;
            SYS_MENUS menuobj = tsm.Tag as SYS_MENUS;
            if (this.cd_id == -1 && menuobj.MENU_CODE != "6001")
            {
                MessageBox.Show("请选择儿童！");
                return;
            }
            if (!tsm.Checked)
            {
                //Type ty = Assembly.Load("login").GetType(string.Format("UI.xinxitongji.{0}", menuobj.MENU_URL));
                //Object obj = Activator.CreateInstance(ty);
                CommonHelper.DisposeControls(pnlContent.Controls);
                UserControl uc = Activator.CreateInstance(Type.GetType(menuobj.MENU_URL), new object[] { this }) as UserControl;
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
            if (this.cd_id == -1 && menuobj.MENU_CODE != "6001")
            {
                MessageBox.Show("请选择儿童！");
                return;
            }
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
            if (this.cd_id == -1 && menuobj.MENU_CODE != "6001")
            {
                MessageBox.Show("请选择儿童！");
                return;
            }
            if (!tsb.Checked)
            {
                CommonHelper.DisposeControls(pnlContent.Controls);
                UserControl uc = Activator.CreateInstance(Type.GetType(menuobj.MENU_URL), new object[] { this }) as UserControl;
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
            if (this.cd_id == -1 && menuobj.MENU_CODE != "6001")
            {
                MessageBox.Show("请选择儿童！");
                return;
            }
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

        //刷新
        private void button1_Click(object sender, EventArgs e)
        {
            bindDataNowday();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int indexs = dataGridView1.RowCount;
            if (indexs <= 0)
            {
                return;
            }
            l_cardno.Text = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();//孕妇档案号
            l_name.Text = this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString();//孕妇姓名
            this.cd_id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[2].Value);//孕妇id
            l_sex.Text = this.dataGridView1.SelectedRows[0].Cells[3].Value.ToString();//孕妇姓名
            l_birth.Text = this.dataGridView1.SelectedRows[0].Cells[4].Value.ToString();//孕妇姓名
            int[] age = CommonHelper.getAgeBytime(l_birth.Text, DateTime.Now.ToString("yyyy-MM-dd"));
            l_age.Text = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");

            foreach (ToolStripItem tsdb in toolStrip2.Items)
            {
                SYS_MENUS menuobj = tsdb.Tag as SYS_MENUS;
                tsdb.Text = menuobj.MENU_NAME;
                tsdb.BackColor = System.Drawing.Color.Transparent;
                if (tsdb is ToolStripButton)
                {
                    if ((tsdb as ToolStripButton).Checked)
                    {
                        (tsdb as ToolStripButton).Checked = false;
                        tsdb.PerformClick();
                        break;
                    }

                }
                else if (tsdb is ToolStripDropDownButton)
                {
                    foreach (ToolStripMenuItem tsmi in (tsdb as ToolStripDropDownButton).DropDownItems)
                    {
                        if (tsmi.Checked)
                        {
                            tsmi.Checked = false;
                            tsmi.PerformClick();
                            break;
                        }
                    }
                }
            }
            Cursor.Current = Cursors.WaitCursor;

        }

        /// <summary>
        /// 绑定列表建档信息
        /// ywj
        /// 2016.8.29
        /// </summary>
        public void bindDataNowday()
        {
            dataGridView1.Rows.Clear();
            string checkday = DateTime.Now.ToString("yyyy-MM-dd");
            string isjiuzhen = CommonHelper.getcheckedValue(panel1);
            try
            {
                IList<TB_CHILDBASE> list = basebll.GetListByCheckDoc(checkday, isjiuzhen,cd_id);
                if(list!=null)
                {
                    foreach (TB_CHILDBASE obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, obj.HEALTHCARDNO, obj.CHILDNAME, obj.ID.ToString(), obj.CHILDGENDER, obj.CHILDBIRTHDAY);// 
                        dataGridView1.Rows.Add(row);
                        if(obj.ID==this.cd_id)
                        {
                            row.Selected = true;
                        }
                    }
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                if (dataGridView1.Rows.Count >= 1)
                {
                    if(dataGridView1.SelectedRows.Count<=0)
                    dataGridView1.Rows[0].Selected = true;
                }
            }
        }

        /// <summary>
        /// 检索已建档的信息
        /// ywj
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearcher_Click(object sender, EventArgs e)
        {
            Paneltsb_searchInfo frmsearcher = new Paneltsb_searchInfo();
            frmsearcher.ShowDialog();
            if (frmsearcher.DialogResult == DialogResult.OK)
            {
                TB_CHILDBASE jibenobj = frmsearcher.returnval;
                if (jibenobj != null)
                {
                    bool isinclude = true;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == jibenobj.HEALTHCARDNO)
                        {
                            dataGridView1.Rows[i].Selected = true;
                            isinclude = false;
                            break;
                        }
                    }
                    if (isinclude)
                    {
                        //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                        string[] rowmrn = new string[] { jibenobj.HEALTHCARDNO, jibenobj.CHILDNAME, jibenobj.ID.ToString(),jibenobj.CHILDGENDER,jibenobj.CHILDBIRTHDAY };
                        dataGridView1.Rows.Add(rowmrn);
                        this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                    }
                    dataGridView1_CellEnter(sender, null);

                }

            }
        }

        //设置复选框单选
        private void checkBoxs_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                foreach (Control c in (sender as Control).Parent.Controls)
                {
                    if (c is CheckBox && !c.Equals(sender))
                    {
                        (c as CheckBox).Checked = false;
                    }
                }
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            bindDataNowday(); 
        }

        private void butRead_Click(object sender, EventArgs e)
        {
            HisChipInfo _chipobj = ReadCard.GetChipObj();
            if (_chipobj != null)
            {
                TB_CHILDBASE _childobj = basebll.GetByPatientId(_chipobj.PAT_INDEX_NO);
                if (_childobj != null)
                {
                    SetChildInfo(_childobj, sender);
                }
                else
                {
                    _childobj = basebll.GetByPatientId(_chipobj.CARD_NO);
                    if (_childobj != null)
                    {
                        SetChildInfo(_childobj, sender);
                    }
                    else
                    {
                        if (_chipobj.PAT_INDEX_NO == null && _chipobj.CARD_NO == null)
                        {
                            MessageBox.Show("读卡错误!");
                            return;
                        }
                        else
                        {
                            IList<HisPatientInfo> _hisPatientlist = ReadCard.GetListHisPatientInfo(_chipobj.PAT_INDEX_NO, _chipobj.CARD_NO, false);
                            if (_hisPatientlist.Count > 0)
                            {

                                _hisobj = _hisPatientlist[0];//取查询第一条数据

                                if (_hisobj != null)
                                {
                                    SetHisPatientInfo(sender, _hisobj);
                                }
                            }
                            else
                            {
                                MessageBox.Show("信息不存在!");
                            }

                        }
                    }
                }
            }
        }


        /// <summary>
        /// 加载本地基本信息
        /// 2017-12-13 cc
        /// </summary>
        /// <param name="childbase">病人信息</param>
        private void SetChildInfo(TB_CHILDBASE childbase, object sender)
        {
            if (childbase != null)
            {
                bool isinclude = true;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == childbase.HEALTHCARDNO)
                    {
                        dataGridView1.Rows[i].Selected = true;
                        isinclude = false;
                        break;
                    }
                }
                if (isinclude)
                {
                    //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                    string[] rowmrn = new string[] { childbase.HEALTHCARDNO, childbase.CHILDNAME, childbase.ID.ToString(), childbase.CHILDGENDER, childbase.CHILDBIRTHDAY };
                    dataGridView1.Rows.Add(rowmrn);
                    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                }
                dataGridView1_CellEnter(sender, null);
            }
        }

        /// <summary>
        /// 本地无数据情况下，加载HIS查询信息
        /// 2017-12-13 cc
        /// </summary>
        /// <param name="hisPatientInfo"></param>
        public void SetHisPatientInfo(object sender, HisPatientInfo hisPatientInfo)
        {
            xl_jibenxinxi_edit xl_Jibenxinxi_Edit = new xl_jibenxinxi_edit(hisPatientInfo);
            xl_Jibenxinxi_Edit.ShowDialog();
            if (xl_Jibenxinxi_Edit.DialogResult == DialogResult.OK)
            {
                TB_CHILDBASE _Childbase = basebll.GetByPatientId(hisPatientInfo.PAT_INDEX_NO);
                if (_Childbase != null)
                {
                    SetInfo(_Childbase, sender);
                }
                else
                {
                    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true;
                    dataGridView1_CellEnter(sender, null);
                }
            }
        }

        /// <summary>
        ///  加载基本信息
        ///  2017-12-13 cc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="jibenobj">基本信息</param>
        public void SetInfo(TB_CHILDBASE jibenobj, object sender)
        {
            bool isinclude = true;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == jibenobj.HEALTHCARDNO)
                {
                    dataGridView1.Rows[i].Selected = true;
                    isinclude = false;
                    break;
                }
            }
            if (isinclude)
            {
                string[] rowmrn = new string[] { jibenobj.HEALTHCARDNO, jibenobj.CHILDNAME, jibenobj.ID.ToString(), jibenobj.CHILDGENDER, jibenobj.CHILDBIRTHDAY };
                dataGridView1.Rows.Add(rowmrn);
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
            }
            dataGridView1_CellEnter(sender, null);
        }
    }
}
