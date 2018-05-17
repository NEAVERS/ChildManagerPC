using System;
using System.Windows.Forms;
using YCF.Common;
using YCF.BLL;
using YCF.Model;
using YCF.BLL.cepingshi;
using ChildManager.UI.printrecord.cepingshi;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Data;
using YCF.BLL.Template;

namespace ChildManager.UI.cepingshi
{
    public partial class cp_gyqt_panel : UserControl
    {
        private cp_gyqt_tabbll bll = new cp_gyqt_tabbll();//儿童建档基本信息业务处理类
        tab_person_databll personbll = new tab_person_databll();
        cp_WomenInfo _cpwomeninfo = null;
        private bool _isShowTopPanel;
        IList<CP_GYQT_TAB> _list = null;
        private CP_GYQT_TAB _obj;
        string _hospital = globalInfoClass.Hospital;
        IList<shengmu> sm_list = new List<shengmu>();
        int sm_id = 1;
        IList<yunmu> ym_list = new List<yunmu>();
        int ym_id = 1;
        public List<DicObj> listszys = new List<DicObj>();
        public List<DicObj> listtest = new List<DicObj>();
        InputLanguage InputHuoDong = null;//当前输入法


        public cp_gyqt_panel(cp_WomenInfo cpwomeninfo)
        {
            InitializeComponent();
            _cpwomeninfo = cpwomeninfo;
            CommonHelper.SetAllControls(panel1);
            SetData(szyslist, listszys, "songzhen");
            SetData(testlist, listtest, "test");
        }

        public cp_gyqt_panel(cp_WomenInfo cpwomeninfo, bool isShowTopPanel) : this(cpwomeninfo)
        {
            _isShowTopPanel = isShowTopPanel;

            //panelEx1.Visible = _isShowTopPanel;
            if (!isShowTopPanel)
            {
                foreach (Control item in panelEx1.Controls)
                {
                    if (!item.Equals(update_time))
                    {
                        item.Visible = false;
                    }
                }
            }
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            RefreshRecordList();
        }

        private void RefreshRecordList()
        {
            _list = bll.GetList(_cpwomeninfo.cd_id);
            if (_list.Count > 0)
            {
                //update_time.DataSource = null;
                update_time.ValueMember = "id";
                update_time.DisplayMember = "update_time";
                update_time.DataSource = _list;
            }
            else
            {
                buttonX11.PerformClick();
                update_time.DataSource = _list;
                update_time.Text = "";
            }
        }

        private void update_time_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = (int)(update_time.SelectedValue ?? 0);
            RefreshCode(id);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            CP_GYQT_TAB obj = getObj();
            if (bll.SaveOrUpdate(obj))
            {
                MessageBox.Show("保存成功！");
                RefreshRecordList();
                update_time.SelectedIndex = _list.IndexOf(_list.FirstOrDefault(t => t.UPDATE_TIME == obj.UPDATE_TIME));
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
            Cursor.Current = Cursors.Default;
        }

        private CP_GYQT_TAB getObj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_GYQT_TAB obj = CommonHelper.GetObjMenzhen<CP_GYQT_TAB>(panel1.Controls, _cpwomeninfo.cd_id);
            obj.HOSPITAL = _hospital;
            obj.SHENGMU = GetSm();
            obj.YUNMU = GetYm();
            if (_obj != null)
            {
                obj.ID = _obj.ID;
                obj.OPERATE_CODE = _obj.OPERATE_CODE;
                obj.OPERATE_NAME = _obj.OPERATE_NAME;
                obj.OPERATE_TIME = _obj.OPERATE_TIME;
            }
            return obj;

        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode(int id)
        {
            Cursor.Current = Cursors.WaitCursor;
            _obj = bll.Get(id);
            if (_obj != null)
            {
                smtxt1.Text = "";
                sm_combox.Text = "→";
                smtxt2.Text = "";
                ymtxt1.Text = "";
                ym_combox.Text = "→";
                ymtxt2.Text = "";
                CommonHelper.setForm(_obj, panel1.Controls);
                string[] shengmulit = null;
                string[] yunmulit = null;
                sm_list = new List<shengmu>();
                 if (!string.IsNullOrEmpty(_obj.SHENGMU))
                {
                    shengmulit = _obj.SHENGMU.Split('〓');
                    for(int i=0; i<shengmulit.Length; i++)
                    {
                        shengmu sm_obj = new shengmu() { id = i + 1, count = shengmulit[i] };
                        sm_list.Add(sm_obj);
                    }
                    
                }
                RefreshSmList();
                ym_list = new List<yunmu>();
                if (!string.IsNullOrEmpty(_obj.YUNMU))
                {
                    yunmulit = _obj.YUNMU.Split('〓');
                    for (int i = 0; i < yunmulit.Length; i++)
                    {
                        yunmu ym_obj = new yunmu() { id = i + 1, count = yunmulit[i] };
                        ym_list.Add(ym_obj);
                    }
                }
                RefreshYmList();
            }
            else
            {
                SetDefault();
            }
            Cursor.Current = Cursors.Default;
        }

        private void SetDefault()
        {
            CommonHelper.setForm(new CP_GYQT_TAB(), panel1.Controls);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (_obj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (bll.Delete(_obj.ID))
                        {
                            MessageBox.Show("删除成功!", "软件提示");
                            RefreshRecordList();
                        }
                        else
                        {
                            MessageBox.Show("删除失败!", "请联系管理员");
                        }

                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            else
            {
                MessageBox.Show("该记录还未保存！");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            print(true);
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            print(false);
        }

        public void print(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            CP_GYQT_TAB obj = bll.Get(_obj?.ID ?? 0);
            if (obj == null)
            {
                MessageBox.Show("请保存后再预览打印！", "软件提示");
                return;
            }
            try
            {
                TB_CHILDBASE baseobj = new tb_childbasebll().Get(_cpwomeninfo.cd_id);
                cp_gyqt_printer printer = new cp_gyqt_printer(baseobj, obj);
                printer.Print(ispre);
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
        //新增体验记录
        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (_obj != null)
            {
                var obj = new CP_GYQT_TAB();
                obj.UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _list.Add(obj);
                update_time.DataSource = null;//数据源先置空，否则同一个对象不会刷新
                update_time.ValueMember = "id";
                update_time.DisplayMember = "update_time";
                update_time.DataSource = _list;
                update_time.SelectedIndex = _list.Count - 1;
            }
            ddst_grysh.Text = "正常";
            ddst_jxydnq.Text = "正常";
            ddst_yynq.Text = "正常";
            ddst_dydnq.Text = "正常";
            checkBox8.Checked = true;
            smtxt1.Text = "";
            sm_combox.Text = "→";
            smtxt2.Text = "";
            dataGridView_sm.Rows.Clear();
            sm_list = new List<shengmu>();
            ymtxt1.Text = "";
            ym_combox.Text = "→";
            ymtxt2.Text = "";
            dataGridView_ym.Rows.Clear();
            ym_list = new List<yunmu>();
        }

        /// <summary>
        /// 设置自动获取签名
        /// 2017-12-01 cc
        /// </summary>
        /// <param name="con">控件</param>
        /// <param name="diclist"></param>
        /// <param name="type">分类</param>
        public void SetData(ListBox con, List<DicObj> diclist, string type)
        {
            IList<TAB_PERSON_DATA> list = personbll.GetList(type);
            DataTable dt = new DataTable();
            dt.Columns.Add("code", typeof(string));
            dt.Columns.Add("name", typeof(string));

            foreach (TAB_PERSON_DATA obj in list)
            {
                DicObj dicobj = new DicObj();
                dicobj.name = obj.NAME;   //获取name属性值  
                dicobj.code = obj.CODE;   //获取name属性值 
                diclist.Add(dicobj);
                dt.Rows.Add(dicobj.code, dicobj.name);
            }
            con.DataSource = dt;
            con.DisplayMember = "name";
            con.ValueMember = "code";
        }

        #region 送诊医生操作
        private void szys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && szyslist.Visible == true)
            {
                DicObj info = szyslist.SelectedItem as DicObj;
                szys.Text = info.name;
                szyslist.Visible = false;
                //this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void szys_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void szys_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (szyslist.SelectedIndex > 0)
                    szyslist.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (szyslist.SelectedIndex < szyslist.Items.Count - 1)
                    szyslist.SelectedIndex++;
            }
            //回车
            else if (e.KeyCode == Keys.Enter)
            {
                //DicObj info = szyslist.SelectedItem as DicObj;
                //textBox1.Text = info.name;
                //szyslist.Visible = false;
            }
            else
            {
                szyslist.DataSource = null;

                string selpro = szys.Text.Trim();

                if (selpro != "")
                {
                    IList<DicObj> dataSource = listszys.FindAll(t => (t.code.Length >= selpro.Length && t.code.Substring(0, selpro.Length).ToUpper().Equals(selpro.ToUpper())) || (t.name.Length > selpro.Length && t.name.Substring(0, selpro.Length).Equals(selpro.ToUpper())));
                    if (dataSource.Count > 0)
                    {
                        szyslist.DataSource = dataSource;
                        szyslist.DisplayMember = "name";
                        szyslist.ValueMember = "code";
                        szyslist.Visible = true;
                    }
                    else
                        szyslist.Visible = false;
                }
                else
                {
                    szyslist.Visible = false;
                }
            }
            //textBox1.Focus();
            szys.Select(szys.Text.Length, 1); //光标定位到文本框最后
        }

        private void szys_Enter(object sender, EventArgs e)
        {
            InputHuoDong = InputLanguage.CurrentInputLanguage;
            foreach (InputLanguage Input in InputLanguage.InstalledInputLanguages)
            {
                if (Input.LayoutName.Contains("美式键盘"))
                {
                    InputLanguage.CurrentInputLanguage = Input;
                    break;
                }
            }
        }

        private void szys_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputHuoDong;
        }

        #endregion

        #region 测试者签名操作
        private void cszqm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && testlist.Visible == true)
            {
                DicObj info = testlist.SelectedItem as DicObj;
                cszqm.Text = info.name;
                testlist.Visible = false;
                //this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void cszqm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void cszqm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (testlist.SelectedIndex > 0)
                    testlist.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (testlist.SelectedIndex < testlist.Items.Count - 1)
                    testlist.SelectedIndex++;
            }
            //回车
            else if (e.KeyCode == Keys.Enter)
            {
                //DicObj info = testlist.SelectedItem as DicObj;
                //textBox1.Text = info.name;
                //testlist.Visible = false;
            }
            else
            {
                testlist.DataSource = null;

                string selpro = cszqm.Text.Trim();

                if (selpro != "")
                {
                    IList<DicObj> dataSource = listtest.FindAll(t => (t.code.Length >= selpro.Length && t.code.Substring(0, selpro.Length).ToUpper().Equals(selpro.ToUpper())) || (t.name.Length > selpro.Length && t.name.Substring(0, selpro.Length).Equals(selpro.ToUpper())));
                    if (dataSource.Count > 0)
                    {
                        testlist.DataSource = dataSource;
                        testlist.DisplayMember = "name";
                        testlist.ValueMember = "code";
                        testlist.Visible = true;
                    }
                    else
                        testlist.Visible = false;
                }
                else
                {
                    testlist.Visible = false;
                }
            }
            //textBox1.Focus();
            cszqm.Select(cszqm.Text.Length, 1); //光标定位到文本框最后
        }

        private void cszqm_Enter(object sender, EventArgs e)
        {
            InputHuoDong = InputLanguage.CurrentInputLanguage;
            foreach (InputLanguage Input in InputLanguage.InstalledInputLanguages)
            {
                if (Input.LayoutName.Contains("美式键盘"))
                {
                    InputLanguage.CurrentInputLanguage = Input;
                    break;
                }
            }
        }

        private void cszqm_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputHuoDong;
        }

        #endregion

        #region 声母操作

        //获取声母字段
        public string GetSm()
        {
            string shengmu="";
            if (sm_list != null)
            {
                foreach (shengmu obj in sm_list)
                {
                    shengmu += obj.count + "〓";
                }
                shengmu = shengmu.Trim('〓');
            }
            return shengmu;
        }
        //新增声母表
        private void sm_add_Click(object sender, EventArgs e)
        {
            shengmu smobj = new shengmu();
            smobj.id = sm_id;
            smobj.count = smtxt1.Text.Trim() + "&" + sm_combox.Text.Trim() + "&" + smtxt2.Text.Trim();
            sm_list.Add(smobj);
            RefreshSmList();
            sm_id++;
            smtxt1.Focus();
        }
        //刷新声母表
        public void RefreshSmList()
        {
            dataGridView_sm.Rows.Clear();//清空列表
            if (sm_list != null)
            {
                int i = 1;
                foreach (shengmu obj in sm_list)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    string count = obj.count.Replace("&", "");
                    row.CreateCells(dataGridView_sm, i, count);
                    row.Tag = obj;
                    dataGridView_sm.Rows.Add(row);
                    i++;
                }
                dataGridView_sm.ClearSelection();
                smtxt1.Text = "";
                sm_combox.Text = "→";
                smtxt2.Text = "";
            }
        }
        //删除声母表选中行
        private void sm_del_Click(object sender, EventArgs e)
        {
            if (dataGridView_sm.SelectedRows.Count > 0)
            {
                shengmu smobj = dataGridView_sm.SelectedRows[0].Tag as shengmu;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if (sm_list.Remove(smobj))
                    {
                        RefreshSmList();
                    }
                    else
                    {
                        MessageBox.Show("删除失败!");
                    }

                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }

            }
            else
            {
                MessageBox.Show("请选择要删除的行！", "系统提示");
            }
        }

        #endregion

        #region 韵母操作

        //获取韵母字段
        public string GetYm()
        {
            string yunmu = "";
            if (ym_list != null)
            {
                foreach (yunmu obj in ym_list)
                {
                    yunmu += obj.count + "〓";
                }
                yunmu = yunmu.Trim('〓');
            }
            return yunmu;
        }
        //新增韵母表
        private void ym_add_Click(object sender, EventArgs e)
        {
            yunmu ymobj = new yunmu();
            ymobj.id = ym_id;
            ymobj.count = ymtxt1.Text.Trim() + "&" + ym_combox.Text.Trim() + "&" + ymtxt2.Text.Trim();
            ym_list.Add(ymobj);
            RefreshYmList();
            ym_id++;
            ymtxt1.Focus();
        }
        //刷新韵母表
        public void RefreshYmList()
        {
            dataGridView_ym.Rows.Clear();//清空列表
            if (ym_list != null)
            {
                int i = 1;
                foreach (yunmu obj in ym_list)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    string count = obj.count.Replace("&", "");
                    row.CreateCells(dataGridView_ym, i, count);
                    row.Tag = obj;
                    dataGridView_ym.Rows.Add(row);
                    i++;
                }
                dataGridView_ym.ClearSelection();
                ymtxt1.Text = "";
                ym_combox.Text = "→";
                ymtxt2.Text = "";
            }
        }
        //删除韵母表选中行
        private void ym_del_Click(object sender, EventArgs e)
        {
            if (dataGridView_ym.SelectedRows.Count > 0)
            {
                yunmu ymobj = dataGridView_ym.SelectedRows[0].Tag as yunmu;
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    if (ym_list.Remove(ymobj))
                    {
                        RefreshYmList();
                    }
                    else
                    {
                        MessageBox.Show("删除失败!");
                    }

                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }

            }
            else
            {
                MessageBox.Show("请选择要删除的行！", "系统提示");
            }
        }

        #endregion

        private void ymtxt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V)
            {
                (sender as Control).Text = (sender as Control).Text.Replace('v', 'ü');
                (sender as TextBoxBase).SelectionStart = (sender as TextBoxBase).Text.Length;
            }
        }
    }
}
