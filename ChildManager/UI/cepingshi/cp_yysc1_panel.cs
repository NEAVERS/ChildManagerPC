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
    public partial class cp_yysc1_panel : UserControl
    {
        tab_person_databll personbll = new tab_person_databll();
        private cp_cdi_tabbll cdibll = new cp_cdi_tabbll();
        private cp_cdi1_tabbll cdi1bll = new cp_cdi1_tabbll();//儿童建档基本信息业务处理类
        private cp_ddst_tabbll ddstbll = new cp_ddst_tabbll();//
        private cp_ppvt_tabbll ppvtbll = new cp_ppvt_tabbll();//
        cp_WomenInfo _cpwomeninfo = null;
        private bool _isShowTopPanel;
        IList<CP_CDI_TAB> _list = null;
        public List<DicObj> listszys = new List<DicObj>();
        public List<DicObj> listtest = new List<DicObj>();
        InputLanguage InputHuoDong = null;//当前输入法

        public string _type = "YYSC1";
        public string _hospital = globalInfoClass.Hospital;
        private CP_CDI_TAB _cdiobj;
        private CP_CDI1_TAB _cdi1obj;
        private CP_DDST_TAB _ddstobj;
        private CP_PPVT_TAB _zqobj;

        public cp_yysc1_panel(cp_WomenInfo cpwomeninfo)
        {
            InitializeComponent();
            _cpwomeninfo = cpwomeninfo;
            CommonHelper.SetAllControls(panel1);
            SetData(szyslist, listszys, "songzhen");
            SetData(testlist, listtest, "test");
        }

        public cp_yysc1_panel(cp_WomenInfo cpwomeninfo, bool isShowTopPanel) : this(cpwomeninfo)
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
            _list = cdibll.GetList(_cpwomeninfo.cd_id, _type);
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
                update_time.ValueMember = "id";
                update_time.DisplayMember = "update_time";
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
            CP_CDI_TAB cdiobj = getCdiObj();
            CP_CDI1_TAB cdi1obj = getCdi1Obj();
            CP_DDST_TAB ddstobj = getDdstObj();
            CP_PPVT_TAB ppvtobj = getPpvtObj();


            bool succ = false;
            if (cdibll.SaveOrUpdate(cdiobj))
            {
                succ = true;
                cdi1obj.EXTERNALID = cdiobj.ID;
                ddstobj.EXTERNALID = cdiobj.ID;
                ppvtobj.EXTERNALID = cdiobj.ID;
            }
            else
            {
                succ = false;
            }
            if (cdi1bll.SaveOrUpdate(cdi1obj))
            {
                succ = true;
            }
            else
            {
                succ = false;
            }
            if (ddstbll.SaveOrUpdate(ddstobj))
            {
                succ = true;
            }
            else
            {
                succ = false;
            }
            if (ppvtbll.SaveOrUpdate(ppvtobj))
            {
                succ = true;
            }
            else
            {
                succ = false;
            }

            if (succ)
            {
                MessageBox.Show("保存成功！");
                RefreshRecordList();
                update_time.SelectedIndex = _list.IndexOf(_list.FirstOrDefault(t => t.UPDATE_TIME == cdiobj.UPDATE_TIME));
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
            Cursor.Current = Cursors.Default;
        }
        //CDI短表（词汇及手势）测试结果
        private CP_CDI_TAB getCdiObj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_CDI_TAB cdiobj = CommonHelper.GetObjMenzhen<CP_CDI_TAB>(groupBox2.Controls, _cpwomeninfo.cd_id);
            cdiobj.TYPE = _type;
            cdiobj.HOSPITAL = _hospital;
            cdiobj.CHBD_DF = ss_chbd_df.Text;
            cdiobj.CHBD_P50 = ss_chbd_p50.Text;
            cdiobj.CHBD_P75 = ss_chbd_p75.Text;
            cdiobj.BSTGZ = ss_bstgz.Text;
            cdiobj.ETDYR = ss_etdyr.Text;
            cdiobj.CSZQM = cszqm.Text;
            cdiobj.CSRQ = csrq.Text;
            cdiobj.SZYS = szys.Text.Trim();
            if (_cdiobj != null)
            {
                cdiobj.ID = _cdiobj.ID;
                cdiobj.OPERATE_CODE = _cdiobj.OPERATE_CODE;
                cdiobj.OPERATE_NAME = _cdiobj.OPERATE_NAME;
                cdiobj.OPERATE_TIME = _cdiobj.OPERATE_TIME;
            }
            return cdiobj;

        }
        //CDI短表（词汇及句子）测试结果
        private CP_CDI1_TAB getCdi1Obj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_CDI1_TAB cdi1obj = CommonHelper.GetObjMenzhen<CP_CDI1_TAB>(groupBox3.Controls, _cpwomeninfo.cd_id);
            cdi1obj.TYPE = _type;
            cdi1obj.HOSPITAL = _hospital;
            cdi1obj.CSZQM = cszqm.Text.Trim();
            cdi1obj.CSRQ = csrq.Text.Trim();
            cdi1obj.SZYS = szys.Text.Trim();
            if (_cdi1obj != null)
            {
                cdi1obj.ID = _cdi1obj.ID;
                cdi1obj.OPERATE_CODE = _cdi1obj.OPERATE_CODE;
                cdi1obj.OPERATE_NAME = _cdi1obj.OPERATE_NAME;
                cdi1obj.OPERATE_TIME = _cdi1obj.OPERATE_TIME;
            }
            return cdi1obj;
        }
        //DDST结果
        private CP_DDST_TAB getDdstObj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_DDST_TAB ddstobj = CommonHelper.GetObjMenzhen<CP_DDST_TAB>(panel1.Controls, _cpwomeninfo.cd_id);
            ddstobj.TYPE = _type;
            ddstobj.HOSPITAL = _hospital;
            if (_ddstobj != null)
            {
                ddstobj.ID = _ddstobj.ID;
                ddstobj.OPERATE_CODE = _ddstobj.OPERATE_CODE;
                ddstobj.OPERATE_NAME = _ddstobj.OPERATE_NAME;
                ddstobj.OPERATE_TIME = _ddstobj.OPERATE_TIME;
            }
            return ddstobj;

        }
        //皮勃迪图片词汇测试（PPVT）
        private CP_PPVT_TAB getPpvtObj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_PPVT_TAB ppvtobj = CommonHelper.GetObjMenzhen<CP_PPVT_TAB>(panel1.Controls, _cpwomeninfo.cd_id);
            ppvtobj.TYPE = _type;
            ppvtobj.HOSPITAL = _hospital;
            if (_zqobj != null)
            {
                ppvtobj.ID = _zqobj.ID;
                ppvtobj.OPERATE_CODE = _zqobj.OPERATE_CODE;
                ppvtobj.OPERATE_NAME = _zqobj.OPERATE_NAME;
                ppvtobj.OPERATE_TIME = _zqobj.OPERATE_TIME;
            }
            return ppvtobj;

        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="ID"></param>
        public void RefreshCode(int id)
        {
            Cursor.Current = Cursors.WaitCursor;
            _cdiobj = cdibll.Get(id, _type);
            _cdi1obj = cdi1bll.Get(_cpwomeninfo.cd_id,  _type, _cdiobj?.ID ?? 0);
            _ddstobj = ddstbll.GetByType(_cpwomeninfo.cd_id, _type, _cdiobj?.ID ?? 0);
            _zqobj = ppvtbll.GetByType(_cpwomeninfo.cd_id, _type, _cdiobj?.ID ?? 0);
            if (_cdiobj != null)
            {
                CommonHelper.setForm(_cdiobj, panel1.Controls);
                ss_chbd_df.Text = _cdiobj.CHBD_DF;
                ss_chbd_p50.Text = _cdiobj.CHBD_P50;
                ss_chbd_p75.Text = _cdiobj.CHBD_P75;
                ss_bstgz.Text = _cdiobj.BSTGZ;
                ss_etdyr.Text = _cdiobj.ETDYR;
            }
            if (_cdi1obj != null)
            {
                CommonHelper.setForm(_cdi1obj, panel1.Controls);
            }
            if (_ddstobj != null)
            {
                CommonHelper.setForm(_ddstobj, panel1.Controls);
            }
            if (_zqobj != null)
            {
                CommonHelper.setForm(_zqobj, panel1.Controls);
            }
            if (_cdiobj == null && _cdi1obj == null && _ddstobj == null && _zqobj == null)
            {
                SetDefault();
            }
            Cursor.Current = Cursors.Default;
        }

        private void SetDefault()
        {
            CommonHelper.setForm(new CP_CDI_TAB(), panel1.Controls);
            CommonHelper.setForm(new CP_CDI1_TAB(), panel1.Controls);
            CommonHelper.setForm(new CP_DDST_TAB(), panel1.Controls);
            CommonHelper.setForm(new CP_PPVT_TAB(), panel1.Controls);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (_cdiobj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (cdibll.Delete(_cdiobj.ID) && cdi1bll.DeleteByExternalid(_cdiobj.ID) && ddstbll.DeleteByExternalid(_cdiobj.ID) && ppvtbll.DeleteByExternalid(_cdiobj.ID))
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
            if (_cdi1obj == null || _cdiobj == null || _ddstobj == null || _zqobj == null)
            {
                MessageBox.Show("请保存后再预览打印！", "软件提示");
                return;
            }
            try
            {
                TB_CHILDBASE baseobj = new tb_childbasebll().Get(_cpwomeninfo.cd_id);
                cp_yysc1_printer printer = new cp_yysc1_printer(baseobj, _cdiobj,  _ddstobj, _cdi1obj, _zqobj);
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

        void CheckBox4CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
            {
                foreach (CheckBox chk in (sender as CheckBox).Parent.Controls)
                {
                    if (chk != sender)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (_cdiobj != null)
            {
                var obj = new CP_CDI_TAB();
                obj.UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _list.Add(obj);
                update_time.DataSource = null;//数据源先置空，否则同一个对象不会刷新
                update_time.ValueMember = "id";
                update_time.DisplayMember = "update_time";
                update_time.DataSource = _list;
                update_time.SelectedIndex = _list.Count - 1;
            }
            ss_chbd_df.Text = "";
            ss_chbd_p50.Text = "";
            ss_chbd_p75.Text = "";
            ss_bstgz.Text = "";
            ss_etdyr.Text = "";

            ddst_grysh.SelectedIndex = 0;
            ddst_jxydnq.SelectedIndex = 0;
            ddst_yynq.SelectedIndex = 0;
            ddst_dydnq.SelectedIndex = 0;
            checkBox8.Checked = true;
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

        private void yylj_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(yylj_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(yylj_df.Text.Trim()))
                {
                    cp_yysc_panel.SetTextNum("CDI_LJ", yylj_df.Text.Trim(), yylj_p50, yylj_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    yylj_df.Focus();
                }
            }
        }

        private void ss_chbd_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ss_chbd_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(ss_chbd_df.Text.Trim()))
                {
                    cp_yysc_panel.SetTextNum("CDI_BD", ss_chbd_df.Text.Trim(), ss_chbd_p50, ss_chbd_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    ss_chbd_df.Focus();
                }
            }
        }

        private void chbd_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(chbd_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(chbd_df.Text.Trim()))
                {
                    cp_yysc_panel.SetTextNum("CDI1", chbd_df.Text.Trim(), chbd_p50, chbd_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    chbd_df.Focus();
                }
            }
        }
    }
}
