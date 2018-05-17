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

namespace ChildManager.UI.jibenluru
{
    public partial class WomenInfo : UserControl
    {
        public int cd_id = -1;
        tb_childbasebll basebll = new tb_childbasebll();
        HisPatientInfo _hisobj = null;
        public WomenInfo()
        {
            InitializeComponent();
            refreshInfo();

            //treeview失去焦点选中节点不变色
            treeView1.HideSelection = false;
            //自已绘制
            this.treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            //this.treeView1.DrawNode += new DrawTreeNodeEventHandler(treeView1_DrawNode);
        }

        public WomenInfo(object obj)
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
        public void refreshInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            //bindDataNowday();//绑定左边列表数据
            bindDataNowdayTreeView();
            try
            {
                sysmenuBll menubll = new sysmenuBll();
                IList<SYS_MENUS> list = menubll.GetListBySql("医生工作站", globalInfoClass.User_Role);
                foreach (SYS_MENUS obj in list)
                {
                    if (obj.MENU_LEVER == "1")
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
                    else if (obj.MENU_LEVER == "2")
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
                //dataGridView1_CellEnter(null, null);
                Cursor.Current = Cursors.Default;
            }
        }

        private void tsm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = sender as ToolStripMenuItem;
            SYS_MENUS menuobj = tsm.Tag as SYS_MENUS;
            if (this.cd_id == -1 && menuobj.menu_code != "1001")
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
            if (this.cd_id == -1 && menuobj.menu_code != "1001")
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
            if (this.cd_id == -1 && menuobj.menu_code != "1001")
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
            if (this.cd_id == -1 && menuobj.menu_code != "1001")
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
            //bindDataNowday();
            bindDataNowdayTreeView();

        }


        /// <summary>
        /// 绑定列表建档信息
        /// ywj
        /// 2016.8.29
        /// </summary>
        //public void bindDataNowday()
        //{
        //    dataGridView1.Rows.Clear();
        //    string checkday = DateTime.Now.ToString("yyyy-MM-dd");
        //    string isjiuzhen = CommonHelper.getcheckedValue(panel1);
        //    try
        //    {
        //        IList<TB_CHILDBASE> list = basebll.GetListByCheckDoc(checkday, isjiuzhen,cd_id);
        //        if(list!=null)
        //        {
        //            foreach (TB_CHILDBASE obj in list)
        //            {
        //                if (obj == null)
        //                    continue;
        //                DataGridViewRow row = new DataGridViewRow();
        //                row.CreateCells(dataGridView1, obj.healthcardno, obj.childname, obj.ID.ToString(), obj.childgender, obj.childbirthday);// 
        //                dataGridView1.Rows.Add(row);
        //                if(obj.ID==this.cd_id)
        //                {
        //                    row.Selected = true;
        //                }
        //            }
        //        }
                
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        Cursor.Current = Cursors.Default;
        //        if (dataGridView1.Rows.Count >= 1)
        //        {
        //            if(dataGridView1.SelectedRows.Count<=0)
        //            dataGridView1.Rows[0].Selected = true;
        //        }
        //    }
        //}

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
                    for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                    {
                        if ((treeView1.Nodes[0].Nodes[i].Tag as TB_CHILDBASE).healthcardno == jibenobj.healthcardno)
                        {
                            treeView1.Nodes[0].Nodes[i].Checked = true;
                            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[i];
                            isinclude = false;
                            break;
                        }
                    }
                    for (int i = 0; i < treeView1.Nodes[1].Nodes.Count; i++)
                    {
                        if ((treeView1.Nodes[1].Nodes[i].Tag as TB_CHILDBASE).healthcardno == jibenobj.healthcardno)
                        {
                            treeView1.Nodes[1].Nodes[i].Checked = true;
                            treeView1.SelectedNode = treeView1.Nodes[1].Nodes[i];
                            isinclude = false;
                            break;
                        }
                    }
                    if (isinclude)
                    {
                        //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                        //string[] rowmrn = new string[] { jibenobj.healthcardno, jibenobj.childname, jibenobj.id.ToString(),jibenobj.childgender,jibenobj.childbirthday };
                        //dataGridView1.Rows.Add(rowmrn);
                        //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                        //treeview节点添加
                        TreeNode tn = new TreeNode();
                        tn.Text = jibenobj.childname;
                        tn.Tag = jibenobj;
                        treeView1.Nodes[1].Nodes.Add(tn);
                        tn.Checked = true;
                        treeView1.SelectedNode = tn;
                    }
                    //dataGridView1_CellEnter(sender, null);
                    treeView1_MouseDown(sender, new MouseEventArgs(MouseButtons.Left, 1, treeView1.SelectedNode.Bounds.Location.X, treeView1.SelectedNode.Bounds.Location.Y, 1));
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
            //bindDataNowday();
            bindDataNowdayTreeView();
        }


        //treeview1 节点
        public void bindDataNowdayTreeView()
        {            
            string checkday = DateTime.Now.ToString("yyyy-MM-dd");
            string isjiuzhen = null;
            try
            {
                TreeNode tn;
                isjiuzhen = "未就诊";
                IList<TB_CHILDBASE> list = basebll.GetListByCheckDoc(checkday, isjiuzhen, -1);
                
                if (list != null)
                {
                    treeView1.Nodes[0].Nodes.Clear();
                    foreach (TB_CHILDBASE obj in list)
                    {
                        if (obj == null)
                            continue;
                        tn = new TreeNode();
                        //DataGridViewRow row = new DataGridViewRow();
                        //row.CreateCells(dataGridView1, obj.healthcardno, obj.childname, obj.ID.ToString(), obj.childgender, obj.childbirthday);// 
                        //dataGridView1.Rows.Add(row);
                        tn.Text=obj.childname;
                        tn.Tag = obj;
                        treeView1.Nodes[0].Nodes.Add(tn);
                        if (obj.ID == this.cd_id)
                        {
                            tn.Checked = true;
                            treeView1.SelectedNode = tn;
                            treeView1.Focus();
                        }
                    }
                }
                isjiuzhen = "已就诊";

                list = basebll.GetListByCheckDoc(checkday, isjiuzhen, -1);
                if (list != null)
                {
                    treeView1.Nodes[1].Nodes.Clear();
                    foreach (TB_CHILDBASE obj in list)
                    {
                        if (obj == null)
                            continue;
                        tn = new TreeNode();
                        //DataGridViewRow row = new DataGridViewRow();
                        //row.CreateCells(dataGridView1, obj.healthcardno, obj.childname, obj.ID.ToString(), obj.childgender, obj.childbirthday);// 
                        //dataGridView1.Rows.Add(row);
                        tn.Text = obj.childname;
                        tn.Tag = obj;
                        treeView1.Nodes[1].Nodes.Add(tn);
                        if (obj.ID == this.cd_id )
                        {
                            tn.Checked = true;
                            treeView1.SelectedNode = tn;
                            treeView1.Focus();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                treeView1.Nodes[0].Expand();
                treeView1.Nodes[1].Expand();
                //if (treeView1.SelectedNode==null&& treeView1.Nodes[0].Nodes.Count>0)
                //{
                //    treeView1.Nodes[0].Nodes[0].Checked = true;
                //    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
                //    treeView1.Focus();
                //}
            }
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //演示为绿底白字
                e.Graphics.FillRectangle(Brushes.DarkBlue, e.Node.Bounds);
                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }


            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }
        }

        public void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = treeView1.GetNodeAt(e.X,e.Y);
            if (node==null || node.Tag==null)
            {
                return;

            }

            int indexs = treeView1.Nodes[0].Nodes.Count + treeView1.Nodes[1].Nodes.Count;
            if (indexs <= 0)
            {
                return;
            }
            node.Checked = true;
            
            treeView1.SelectedNode = node;
            
            TB_CHILDBASE tbc = (TB_CHILDBASE)this.treeView1.SelectedNode.Tag;
            l_cardno.Text = tbc.healthcardno;//档案号
            l_name.Text = tbc.childname;
            this.cd_id = tbc.id;
            l_sex.Text = tbc.childgender;
            l_birth.Text = tbc.childbirthday;
            //int[] age = CommonHelper.getAgeBytime(l_birth.Text, DateTime.Now.ToString("yyyy-MM-dd"));
            l_age.Text = CommonHelper.getAgeStr(l_birth.Text);
            l_patientid.Text = tbc.patient_id;
            l_weight.Text = tbc.birthweight + "kg";
            l_gp.Text = "G" + tbc.cs_fetus + "P" + tbc.cs_produce;
            l_yunzhou.Text = tbc.cs_week==null||tbc.cs_week.Trim().Equals("") ? tbc.sfzy : tbc.cs_day.Trim().Equals("") ? tbc.cs_week + "周" : tbc.cs_week + "+" + tbc.cs_day + "周";
            l_csqk.Text = tbc.modedelivery;
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
        /// 读卡
        /// 2017-12-06 cc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butRead_Click(object sender, EventArgs e)
        {
            HisChipInfo _chipobj = ReadCard.GetChipObj();
            if (_chipobj != null)
            {

                // MessageBox.Show("病人ID：" + _chipobj.PAT_INDEX_NO + "\r\n就诊卡号：" + _chipobj.CARD_NO);

                TB_CHILDBASE _childobj = basebll.GetByPatientId(_chipobj.PAT_INDEX_NO);
                if (_childobj != null)
                {
                    MessageBox.Show("本地病人ID查询有数据");
                    SetChildInfo(_childobj, sender);
                }
                else
                {
                    _childobj = basebll.GetByPatientId(_chipobj.CARD_NO);
                    if (_childobj != null)
                    {
                        MessageBox.Show("本地就诊卡号查询有数据");
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
        /// 2017-12-06 cc
        /// </summary>
        /// <param name="childbase">病人信息</param>
        public void SetChildInfo(TB_CHILDBASE childbase, object sender)
        {
            if (childbase != null)
            {
                if (childbase != null)
                {

                    bool isinclude = true;
                    for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
                    {
                        if ((treeView1.Nodes[0].Nodes[i].Tag as TB_CHILDBASE).healthcardno == childbase.healthcardno)
                        {
                            treeView1.Nodes[0].Nodes[i].Checked = true;
                            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[i];
                            isinclude = false;
                            break;
                        }
                    }
                    for (int i = 0; i < treeView1.Nodes[1].Nodes.Count; i++)
                    {
                        if ((treeView1.Nodes[1].Nodes[i].Tag as TB_CHILDBASE).healthcardno == childbase.healthcardno)
                        {
                            treeView1.Nodes[1].Nodes[i].Checked = true;
                            treeView1.SelectedNode = treeView1.Nodes[1].Nodes[i];
                            isinclude = false;
                            break;
                        }
                    }
                    if (isinclude)
                    {
                        //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                        //string[] rowmrn = new string[] { jibenobj.healthcardno, jibenobj.childname, jibenobj.id.ToString(),jibenobj.childgender,jibenobj.childbirthday };
                        //dataGridView1.Rows.Add(rowmrn);
                        //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                        //treeview节点添加
                        TreeNode tn = new TreeNode();
                        tn.Text = childbase.childname;
                        tn.Tag = childbase;
                        treeView1.Nodes[1].Nodes.Add(tn);
                        tn.Checked = true;
                        treeView1.SelectedNode = tn;
                    }
                    treeView1_MouseDown(sender, new MouseEventArgs(MouseButtons.Left, 1, treeView1.SelectedNode.Bounds.Location.X, treeView1.SelectedNode.Bounds.Location.Y, 1));
                }
            }
        }

        /// <summary>
        /// 本地无数据情况下，加载HIS查询信息
        /// 2017-11-29 cc
        /// </summary>
        /// <param name="hisPatientInfo"></param>
        public void SetHisPatientInfo(object sender, HisPatientInfo hisPatientInfo)
        {
            //l_cardno.Text = "";//孕妇档案号
            //l_name.Text = hisPatientInfo.PAT_NAME;//孕妇姓名
            //this.cd_id = -1;//孕妇id
            //l_sex.Text = hisPatientInfo.PHYSI_SEX_NAME;//孕妇性别
            //l_birth.Text = hisPatientInfo.DATE_BIRTH;//出生日期
            //int[] age = CommonHelper.getAgeBytime(l_birth.Text, DateTime.Now.ToString("yyyy-MM-dd"));
            //l_age.Text = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            //MessageBox.Show("未保存的信息，请先保存！");

            doc_jibenxinxi_edit doc_Jibenxinxi_Edit = new doc_jibenxinxi_edit(hisPatientInfo);
            doc_Jibenxinxi_Edit.ShowDialog();
            if (doc_Jibenxinxi_Edit.DialogResult == DialogResult.OK)
            {
                TB_CHILDBASE _Childbase = basebll.GetByPatientId(hisPatientInfo.PAT_INDEX_NO);
                if (_Childbase != null)
                {
                    SetInfo(sender, _Childbase);
                }
                else
                {
                    treeView1_MouseDown(sender, new MouseEventArgs(MouseButtons.Left, 1, treeView1.SelectedNode.Bounds.Location.X, treeView1.SelectedNode.Bounds.Location.Y, 1));
                }
            }

        }

        /// <summary>
        /// 加载基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="jibenobj"></param>
        public void SetInfo(object sender, TB_CHILDBASE jibenobj)
        {
            bool isinclude = true;
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count; i++)
            {
                if ((treeView1.Nodes[0].Nodes[i].Tag as TB_CHILDBASE).healthcardno == jibenobj.healthcardno)
                {
                    treeView1.Nodes[0].Nodes[i].Checked = true;
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[i];
                    isinclude = false;
                    break;
                }
            }
            for (int i = 0; i < treeView1.Nodes[1].Nodes.Count; i++)
            {
                if ((treeView1.Nodes[1].Nodes[i].Tag as TB_CHILDBASE).healthcardno == jibenobj.healthcardno)
                {
                    treeView1.Nodes[1].Nodes[i].Checked = true;
                    treeView1.SelectedNode = treeView1.Nodes[1].Nodes[i];
                    isinclude = false;
                    break;
                }
            }
            if (isinclude)
            {
                //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                //string[] rowmrn = new string[] { jibenobj.healthcardno, jibenobj.childname, jibenobj.id.ToString(),jibenobj.childgender,jibenobj.childbirthday };
                //dataGridView1.Rows.Add(rowmrn);
                //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                //treeview节点添加
                TreeNode tn = new TreeNode();
                tn.Text = jibenobj.childname;
                tn.Tag = jibenobj;
                treeView1.Nodes[1].Nodes.Add(tn);
                tn.Checked = true;
                treeView1.SelectedNode = tn;
            }
            treeView1_MouseDown(sender, new MouseEventArgs(MouseButtons.Left, 1, treeView1.SelectedNode.Bounds.Location.X, treeView1.SelectedNode.Bounds.Location.Y, 1));
        }
    }
}
