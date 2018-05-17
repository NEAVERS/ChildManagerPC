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
using System.Text;

namespace ChildManager.UI.cepingshi
{
    public partial class cp_yysc_panel : UserControl
    {
        tab_person_databll personbll = new tab_person_databll();
        private cp_cdi_tabbll cdibll = new cp_cdi_tabbll();//儿童建档基本信息业务处理类
        private cp_cdi1_tabbll cdi1bll = new cp_cdi1_tabbll();
        private cp_ddst_tabbll ddstbll = new cp_ddst_tabbll();//
        private cp_zqyyfyjc_tabbll zqbll = new cp_zqyyfyjc_tabbll();//
        public static tab_yysc_Numbll Abll = new tab_yysc_Numbll();
        cp_WomenInfo _cpwomeninfo = null;
        public TB_CHILDBASE _childobj = null;
        public tb_childbasebll childbll = new tb_childbasebll();
        IList<CP_CDI_TAB> _list = null;
        private bool _isShowTopPanel;
        private CP_CDI_TAB _cdiobj;
        private CP_CDI1_TAB _cdi1obj;
        private CP_DDST_TAB _ddstobj;
        private CP_ZQYYFYJC_TAB _zqobj;
        public string _type = "YYSC";
        string _hospital = globalInfoClass.Hospital;
        public List<DicObj> listszys = new List<DicObj>();
        public List<DicObj> listtest = new List<DicObj>();
        InputLanguage InputHuoDong = null;//当前输入法
        public cp_yysc_panel(cp_WomenInfo cpwomeninfo)
        {
            InitializeComponent();
            _cpwomeninfo = cpwomeninfo;
            _childobj = childbll.Get(cpwomeninfo.cd_id);
            CommonHelper.SetAllControls(panel1);
            SetData(szyslist, listszys, "songzhen");
            SetData(testlist, listtest, "test");
        }

        public cp_yysc_panel(cp_WomenInfo cpwomeninfo, bool isShowTopPanel) : this(cpwomeninfo)
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
            CP_ZQYYFYJC_TAB zqobj = getZqObj();
            bool succ = false;
            if (cdibll.SaveOrUpdate(cdiobj))
            {
                succ = true;
                cdi1obj.EXTERNALID = cdiobj.ID;
                ddstobj.EXTERNALID = cdiobj.ID;
                zqobj.EXTERNALID = cdiobj.ID;
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
            if (zqbll.SaveOrUpdate(zqobj))
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

        private CP_CDI_TAB getCdiObj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_CDI_TAB cdiobj = CommonHelper.GetObjMenzhen<CP_CDI_TAB>(groupBox3.Controls, _cpwomeninfo.cd_id);
            cdiobj.TYPE = _type;
            cdiobj.HOSPITAL = _hospital;
            cdiobj.CSZQM = cszqm.Text.Trim();
            cdiobj.CSRQ = csrq.Text.Trim();
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
        private CP_CDI1_TAB getCdi1Obj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_CDI1_TAB cdi1obj = CommonHelper.GetObjMenzhen<CP_CDI1_TAB>(groupBox2.Controls, _cpwomeninfo.cd_id);
            cdi1obj.TYPE = _type;
            cdi1obj.HOSPITAL = _hospital;
            cdi1obj.CSZQM = cszqm.Text.Trim();
            cdi1obj.CSRQ = csrq.Text.Trim();
            cdi1obj.CHBD_DF = jz_chbd_df.Text.Trim();
            cdi1obj.CHBD_P50 = jz_chbd_p50.Text.Trim();
            cdi1obj.CHBD_P75 = jz_chbd_p75.Text.Trim();
            cdi1obj.BSTGZ = jz_bstgz.Text.Trim();
            cdi1obj.ETDYR = jz_etdyr.Text.Trim();
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

        private CP_ZQYYFYJC_TAB getZqObj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_ZQYYFYJC_TAB zqobj = CommonHelper.GetObjMenzhen<CP_ZQYYFYJC_TAB>(panel1.Controls, _cpwomeninfo.cd_id);
            zqobj.TYPE = _type;
            zqobj.HOSPITAL = _hospital;
            if (_zqobj != null)
            {
                zqobj.ID = _zqobj.ID;
                zqobj.OPERATE_CODE = _zqobj.OPERATE_CODE;
                zqobj.OPERATE_NAME = _zqobj.OPERATE_NAME;
                zqobj.OPERATE_TIME = _zqobj.OPERATE_TIME;

            }
            return zqobj;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode(int id)
        {
            Cursor.Current = Cursors.WaitCursor;
            _cdiobj = cdibll.Get(id, _type);
            _cdi1obj = cdi1bll.Get(_cpwomeninfo.cd_id, _type, (int)(_cdiobj?.ID ?? 0));
            _ddstobj = ddstbll.GetByCdId(_cpwomeninfo.cd_id, _type, (int)(_cdiobj?.ID ?? 0));
            _zqobj = zqbll.GetByCdId(_cpwomeninfo.cd_id, _type, (int)(_cdiobj?.ID ?? 0));
            if (_cdiobj != null)
            {
                CommonHelper.setForm(_cdiobj, panel1.Controls);
            }
            if (_cdi1obj != null)
            {
                jz_chbd_df.Text = _cdi1obj.CHBD_DF;
                jz_chbd_p50.Text = _cdi1obj.CHBD_P50;
                jz_chbd_p75.Text = _cdi1obj.CHBD_P75;
                jz_bstgz.Text = _cdi1obj.BSTGZ;
                jz_etdyr.Text = _cdi1obj.ETDYR;
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
            CommonHelper.setForm(new CP_ZQYYFYJC_TAB(), panel1.Controls);
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
                        if (cdibll.Delete(_cdiobj.ID) && cdi1bll.DeleteByExternalid((int)_cdiobj.ID) && ddstbll.DeleteByExternalid((int)_cdiobj.ID) && zqbll.DeleteByExternalid((int)_cdiobj.ID))
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
            if (_cdiobj == null || _cdi1obj == null || _ddstobj == null || _zqobj == null)
            {
                MessageBox.Show("请保存后再预览打印！", "软件提示");
                return;
            }

            try
            {
                TB_CHILDBASE baseobj = new tb_childbasebll().Get(_cpwomeninfo.cd_id);
                cp_yysc_printer printer = new cp_yysc_printer(baseobj, _ddstobj, _cdiobj, _cdi1obj, _zqobj);
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
            jz_chbd_df.Text = "";
            jz_chbd_p50.Text = "";
            jz_chbd_p75.Text = "";
            jz_bstgz.Text = "";
            jz_etdyr.Text = "";

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

        private void yybd_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(yybd_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(yybd_df.Text.Trim()))
                {
                    SetTextNum("A", yybd_df.Text.Trim(), yybd_yl, yybd_p50, yybd_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    yybd_df.Focus();
                }
            }
        }

        private void tjnl_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tjnl_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(tjnl_df.Text.Trim()))
                {
                    SetTextNum("B", tjnl_df.Text.Trim(), tjnl_yl, tjnl_p50, tjnl_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    tjnl_df.Focus();
                }
            }
        }
        private void sjxg_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sjxg_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(sjxg_df.Text.Trim()))
                {
                    SetTextNum("C", sjxg_df.Text.Trim(), sjxg_yl, sjxg_p50, sjxg_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    sjxg_df.Focus();
                }
            }
        }
        private void qlb_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(qlb_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(qlb_df.Text.Trim()))
                {
                    SetTextNum("全量表", qlb_df.Text.Trim(), qlb_yl, qlb_p50, qlb_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    qlb_df.Focus();
                }
            }
        }

        /// <summary>
        /// 自动计算得分
        /// 2017-11-20 cc
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="defen">得分</param>
        /// <param name="Contongling">相当于同龄</param>
        /// <param name="ConP50">相当于P50</param>
        /// <param name="ConP75">相当于P75</param>
        
        public void SetTextNum(string type, string defen, Control Contongling, Control ConP50, Control ConP75)
        {
            int[] age = CommonHelper.getAgeBytime(_childobj.CHILDBIRTHDAY, DateTime.Now.ToString("yyyy-MM-dd"));
            if (age != null)
            {
                string tongling = "";
                float num2 = Convert.ToSingle(defen);
                int yueling = age[0] * 12 + age[1];
                TAB_YYSC_NUM maxobj = Abll.GetMaxYueling(type);
                if (yueling > maxobj.YUELING)
                {
                    float p10 = Convert.ToSingle(maxobj.P10);
                    if (num2 < p10)
                    {
                        tongling = "<P10";
                    }
                }
                else
                {
                    var aobj = Abll.GetObj(type, yueling);
                    if (aobj != null)
                    {
                        float p10 = Convert.ToSingle(aobj.P10);
                        float p25 = Convert.ToSingle(aobj.P25);
                        float p50 = Convert.ToSingle(aobj.P50);
                        float p75 = Convert.ToSingle(aobj.P75);
                        float p90 = Convert.ToSingle(aobj.P90);
                        if (num2 < p10)
                        {
                            tongling = "<P10";
                        }
                        else if (num2 == p10)
                        {
                            tongling = "P10";
                            if (num2 == p90)
                            {
                                tongling += "-P90";
                            }
                            else if (num2 == p75)
                            {
                                tongling += "-P75";
                            }
                            else if (num2 == p50)
                            {
                                tongling += "-P50";
                            }
                            else if (num2 == p25)
                            {
                                tongling += "-P25";
                            }
                        }
                        else if (num2 < p25)
                        {
                            tongling = "P10-P25";
                        }
                        else if (num2 == p25)
                        {
                            tongling = "P25";
                            if (num2 == p90)
                            {
                                tongling += "-P90";
                            }
                            else if (num2 == p75)
                            {
                                tongling += "-P75";
                            }
                            else if (num2 == p50)
                            {
                                tongling += "-P50";
                            }
                        }
                        else if (num2 < p50)
                        {
                            tongling = "P25-P50";
                        }
                        else if (num2 == p50)
                        {
                            tongling = "P50";
                            if (num2 == p90)
                            {
                                tongling += "-P90";
                            }
                            else if (num2 == p75)
                            {
                                tongling += "-P75";
                            }
                        }
                        else if (num2 < p75)
                        {
                            tongling = "P50-P75";
                        }
                        else if (num2 == p75)
                        {
                            tongling = "P75";
                            if (num2 == p90)
                            {
                                tongling += "-P90";
                            }
                        }
                        else if (num2 < p90)
                        {
                            tongling = "P75-P90";
                        }
                        else if (num2 == p90)
                        {
                            tongling = "P90";
                        }
                        else if (num2 > p90)
                        {
                            tongling = ">P90";
                        }
                    }
                }
                Contongling.Text = tongling;
            }
            SetTextNum(type, defen, ConP50, ConP75);
        }
        private void yylj_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(yylj_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(yylj_df.Text.Trim()))
                {
                    SetTextNum("CDI_LJ", yylj_df.Text.Trim(), yylj_p50, yylj_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    yylj_df.Focus();
                }
            }
        }
        private void chbd_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(chbd_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(chbd_df.Text.Trim()))
                {
                    SetTextNum("CDI_BD", chbd_df.Text.Trim(), chbd_p50, chbd_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    chbd_df.Focus();
                }
            }
        }
        private void jz_chbd_df_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(jz_chbd_df.Text.Trim()))
            {
                if (CommonHelper.IsNumber(jz_chbd_df.Text.Trim()))
                {
                    SetTextNum("CDI1", jz_chbd_df.Text.Trim(), jz_chbd_p50, jz_chbd_p75);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    jz_chbd_df.Focus();
                }
            }
        }

        /// <summary>
        /// 自动计算得分
        /// 2017-11-21 cc
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="defen">得分</param>
        /// <param name="ConP50">相当于P50</param>
        /// <param name="ConP75">相当于P75</param>
        public static void SetTextNum(string type, string defen, Control ConP50, Control ConP75)
        {
            ConP50.Text = Abll.GetResultP50(type, defen);
            ConP75.Text = Abll.GetResultP75(type, defen);
        }

        public void ClearText(TextBox textBox, TextBox tongling, TextBox P50, TextBox P75)
        {
            if (string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                tongling.Text = "";
                P50.Text = "";
                P75.Text = "";
            } 
        }

        private void yylj_df_TextChanged(object sender, EventArgs e)
        {
            ClearText(yylj_df, null, yylj_p50, yylj_p75);
        }

        private void chbd_df_TextChanged(object sender, EventArgs e)
        {
            ClearText(chbd_df, null, chbd_p50, chbd_p75);
        }

        private void jz_chbd_df_TextChanged(object sender, EventArgs e)
        {
            ClearText(jz_chbd_df, null, jz_chbd_p50, jz_chbd_p75);
        }

        private void yybd_df_TextChanged(object sender, EventArgs e)
        {
            ClearText(yybd_df, yybd_yl, yybd_p50, yybd_p75);
        }

        private void tjnl_df_TextChanged(object sender, EventArgs e)
        {
            ClearText(tjnl_df, tjnl_yl, tjnl_p50, tjnl_p75);
        }

        private void sjxg_df_TextChanged(object sender, EventArgs e)
        {
            ClearText(sjxg_df, sjxg_yl, sjxg_p50, sjxg_p75);
        }

        private void qlb_df_TextChanged(object sender, EventArgs e)
        {
            ClearText(qlb_df, qlb_yl, qlb_p50, qlb_p75);
        }
    }

}
