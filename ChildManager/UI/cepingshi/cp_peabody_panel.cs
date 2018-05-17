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
    public partial class cp_peabody_panel : UserControl
    {
        private cp_peabody_tabbll bll = new cp_peabody_tabbll();//儿童建档基本信息业务处理类
        tab_person_databll personbll = new tab_person_databll();
        cp_WomenInfo _cpwomeninfo = null;
        private bool _isShowTopPanel;
        IList<CP_PEABODY_TAB> _list = null;
        private CP_PEABODY_TAB _obj;
        public TB_CHILDBASE _childobj = null;
        public tb_childbasebll childbll = new tb_childbasebll();
        string _hospital = globalInfoClass.Hospital;
        public List<DicObj> listszys = new List<DicObj>();
        public List<DicObj> listtest = new List<DicObj>();
        InputLanguage InputHuoDong = null;//当前输入法
        tab_Peabody_Numbll Abll = new tab_Peabody_Numbll(); 
        tab_ysf_xdnlbll xdnlbll = new tab_ysf_xdnlbll();
        tab_bzfzh_Numbll sandbzf = new tab_bzfzh_Numbll();
        public cp_peabody_panel(cp_WomenInfo cpwomeninfo)
        {
            InitializeComponent();
            _cpwomeninfo = cpwomeninfo;
            _childobj = childbll.Get(cpwomeninfo.cd_id);
            CommonHelper.SetAllControls(panel1);
            SetData(szyslist, listszys, "songzhen");
            SetData(testlist, listtest, "test");

        }

        public cp_peabody_panel(cp_WomenInfo cpwomeninfo, bool isShowTopPanel) : this(cpwomeninfo)
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
            CP_PEABODY_TAB obj = getObj();
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

        private CP_PEABODY_TAB getObj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_PEABODY_TAB obj = CommonHelper.GetObjMenzhen<CP_PEABODY_TAB>(panel1.Controls, _cpwomeninfo.cd_id);
            obj.HOSPITAL = _hospital;
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
                CommonHelper.setForm(_obj, panel1.Controls);
            }
            else
            {
                SetDefault();
            }
            Cursor.Current = Cursors.Default;
        }

        private void SetDefault()
        {
            CommonHelper.setForm(new CP_PEABODY_TAB(), panel1.Controls);
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
            CP_PEABODY_TAB obj = bll.Get(_obj?.ID ?? 0);
            if (obj == null)
            {
                MessageBox.Show("请保存后再预览打印！", "软件提示");
                return;
            }
            try
            {
                TB_CHILDBASE baseobj = new tb_childbasebll().Get(_cpwomeninfo.cd_id);
                cp_peabody_printer printer = new cp_peabody_printer(baseobj, obj);

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

        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (_obj != null)
            {
                var obj = new CP_PEABODY_TAB();
                obj.UPDATE_NAME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _list.Add(obj);
                update_time.DataSource = null;//数据源先置空，否则同一个对象不会刷新
                update_time.ValueMember = "id";
                update_time.DisplayMember = "update_time";
                update_time.DataSource = _list;
                update_time.SelectedIndex = _list.Count - 1;
            }
        }

        /// <summary>
        /// 控件离开公共事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GetQiuzhen(object sender, EventArgs e)
        {
            string qiuzhenTxt = (sender as MaskedTextBox).Text;
            string qzCon_Name = (sender as MaskedTextBox).Name;
            string resCon_Name = qzCon_Name.Replace("qiuzhen", "result");
            Control resultCon = CommonHelper.FindControl(resCon_Name, this);
            if (qiuzhenTxt != "")
            {
                SetResult(qiuzhenTxt, resultCon);

            }
        }

        /// <summary>
        /// 自动加载结果
        /// </summary>
        /// <param name="qiuzhen">丘疹/红晕</param>
        /// <param name="control">需要赋值的控件</param>
        public void SetResult(string qiuzhen, Control control)
        {
            if (!string.IsNullOrEmpty(qiuzhen))
            {
                string[] strQiuzhen = null;
                strQiuzhen = qiuzhen.Split('/');
                int qzNum = Convert.ToInt32(strQiuzhen[0]);
                if (qzNum >= 0 && qzNum <= 2)
                {
                    control.Text = "±";
                }
                else if (qzNum >= 3 && qzNum <= 5)
                {
                    control.Text = "+";
                }
                else if (qzNum >= 6 && qzNum <= 8)
                {
                    control.Text = "++";
                }
                else if (qzNum > 8)
                {
                    control.Text = "+++";
                }
            }
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

        private void fanshe_ysf_Layout(object sender, LayoutEventArgs e)
        {

        }

        /// <summary>
        /// 自动计算得分
        /// 2017-12-14 gtj
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="defen">得分</param>
        /// <param name="bfw">百分位</param>
        /// <param name="bzf">标准分1</param>
        /// <param name="bzf1">标准分2</param>

        public void SetTextNum_yuanshi(string type, string defen, Control bfw, Control bzf, Control bzf1)
        {
            int[] age = CommonHelper.getAgeBytime(_childobj.CHILDBIRTHDAY, DateTime.Now.ToString("yyyy-MM-dd"));
            if (age != null)
            {
                string tongling = "";
                int yueling = 0;
                //string _type = "ysf_";
                float num2 = Convert.ToSingle(defen);
                if (age[0] == 0 && age[1] == 0)
                {
                    yueling = age[2];
                    if (yueling >= 15 && yueling <= 30)
                    {
                        var fanseobj = Abll.GetList("15-30天");
                        if (fanseobj.Count > 0)
                        {

                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);

                        }

                    }

                }
                else
                {
                    yueling = age[0] * 12 + age[1];
                    string yue = "";
                    if (yueling <= 23)
                    {
                        yue = yueling.ToString() + "月";
                    }
                    else if (yueling >= 24 && yueling <= 26)
                    {
                        yue = "24-26月";
                    }
                    else if (yueling >= 27 && yueling <= 29)
                    {
                        yue = "27-29月";
                    }
                    else if (yueling >= 30 && yueling <= 32)
                    {
                        yue = "30-32月";
                    }
                    else if (yueling >= 33 && yueling <= 35)
                    {
                        yue = "33-35月";
                    }
                    else if (yueling >= 36 && yueling <= 38)
                    {
                        yue = "36-38月";
                    }
                    else if (yueling >= 39 && yueling <= 41)
                    {
                        yue = "39-41月";
                    }
                    else if (yueling >= 42 && yueling <= 44)
                    {
                        yue = "42-44月";
                    }
                    else if (yueling >= 45 && yueling <= 47)
                    {
                        yue = "45-47月";
                    }
                    else if (yueling >= 48 && yueling <= 50)
                    {
                        yue = "48-50月";
                    }
                    else if (yueling >= 51 && yueling <= 53)
                    {
                        yue = "51-53月";
                    }
                    else if (yueling >= 54 && yueling <= 59)
                    {
                        yue = "54-59月";
                    }
                    else if (yueling >= 60 && yueling <= 65)
                    {
                        yue = "60-65月";
                    }
                    else if (yueling >= 66 && yueling <= 71)
                    {
                        yue = "66-71月";
                    }
                    var fanseobj = Abll.GetList(yue);
                    if (fanseobj.Count > 0)
                    {
                        if (yueling <= 23)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);

                        }
                        else if (yueling >= 24 && yueling <= 26)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 27 && yueling <= 29)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 30 && yueling <= 32)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 33 && yueling <= 35)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 36 && yueling <= 38)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 39 && yueling <= 41)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 42 && yueling <= 44)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 45 && yueling <= 47)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 48 && yueling <= 50)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 51 && yueling <= 53)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 54 && yueling <= 59)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 60 && yueling <= 65)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                        else if (yueling >= 66 && yueling <= 71)
                        {
                            SetBfwBzf(type, num2, fanseobj, bfw, bzf, bzf1);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 自动计算得分
        /// 2017-12-14 gtj
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="defen">得分</param>
        /// <param name="bfw">相当年龄</param>
        public void SetTextNum_xiangdangnianling(string type, string defen, Control xiangdangnianling)
        {

            if (type == "反射")
            {
                xiangdangnianling.Text = xdnlbll.Getfs_xdnl(defen);
            }

            if (type == "姿势")
            {
                xiangdangnianling.Text = xdnlbll.Getzs_xdnl(defen);
            }
            if (type == "移动")
            {
                xiangdangnianling.Text = xdnlbll.Getyd_xdnl(defen);
            }
            if (type == "实物操作")
            {
                xiangdangnianling.Text = xdnlbll.Getswcz_xdnl(defen);
            }
            if (type == "抓握")
            {
                xiangdangnianling.Text = xdnlbll.Getzw_xdnl(defen);
            }
            if (type == "视觉-运动整合")
            {
                xiangdangnianling.Text = xdnlbll.Getsj_xdnl(defen);
            }

        }


        public void SetBfwBzf(string type, float num2, IList<TAB_PEABODY_NUM> defen, Control bfw, Control bzf, Control bzf1)
        {

            foreach (var item in defen)
            {
                if (type == "反射")
                {
                    if (!String.IsNullOrEmpty(item.YSF_FS))
                    {
                        if (item.YSF_FS.Contains("-"))
                        {
                            string[] objysf = item.YSF_FS.Split('-');
                            if (num2 >= Convert.ToSingle(objysf[0]) && num2 <= Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }

                        }
                        else if (item.YSF_FS.Contains(">"))
                        {
                            string[] objysf = item.YSF_FS.Split('>');
                            if (num2 > Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else if (item.YSF_FS.Contains("<"))
                        {
                            string[] objysf = item.YSF_FS.Split('<');
                            if (num2 < Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else
                        {
                            if (num2 == Convert.ToSingle(item.YSF_FS))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                    }
                }
                if (type == "姿势")
                {
                    if (!String.IsNullOrEmpty(item.YSF_ZS))
                    {
                        if (item.YSF_ZS.Contains("-"))
                        {
                            string[] objysf = item.YSF_ZS.Split('-');
                            if (num2 >= Convert.ToSingle(objysf[0]) && num2 <= Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }

                        }
                        else if (item.YSF_ZS.Contains(">"))
                        {
                            string[] objysf = item.YSF_ZS.Split('>');
                            if (num2 > Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else if (item.YSF_ZS.Contains("<"))
                        {
                            string[] objysf = item.YSF_ZS.Split('<');
                            if (num2 < Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else
                        {
                            if (num2 == Convert.ToSingle(item.YSF_ZS))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                    }
                }
                if (type == "移动")
                {
                    if (!String.IsNullOrEmpty(item.YSF_YD))
                    {
                        if (item.YSF_YD.Contains("-"))
                        {
                            string[] objysf = item.YSF_YD.Split('-');
                            if (num2 >= Convert.ToSingle(objysf[0]) && num2 <= Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }

                        }
                        else if (item.YSF_YD.Contains(">"))
                        {
                            string[] objysf = item.YSF_YD.Split('>');
                            if (num2 > Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else if (item.YSF_YD.Contains("<"))
                        {
                            string[] objysf = item.YSF_YD.Split('<');
                            if (num2 < Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else
                        {
                            if (num2 == Convert.ToSingle(item.YSF_YD))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                    }
                }
                if (type == "实物操作")
                {
                    if (!String.IsNullOrEmpty(item.YSF_SJCZ))
                    {
                        if (item.YSF_SJCZ.Contains("-"))
                        {
                            string[] objysf = item.YSF_SJCZ.Split('-');
                            if (num2 >= Convert.ToSingle(objysf[0]) && num2 <= Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }

                        }
                        else if (item.YSF_SJCZ.Contains(">"))
                        {
                            string[] objysf = item.YSF_SJCZ.Split('>');
                            if (num2 > Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else if (item.YSF_SJCZ.Contains("<"))
                        {
                            string[] objysf = item.YSF_SJCZ.Split('<');
                            if (num2 < Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else
                        {
                            if (num2 == Convert.ToSingle(item.YSF_SJCZ))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                    }
                }
                if (type == "抓握")
                {
                    if (!String.IsNullOrEmpty(item.YSF_ZW))
                    {
                        if (item.YSF_ZW.Contains("-"))
                        {
                            string[] objysf = item.YSF_ZW.Split('-');
                            if (num2 >= Convert.ToSingle(objysf[0]) && num2 <= Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }

                        }
                        else if (item.YSF_ZW.Contains(">"))
                        {
                            string[] objysf = item.YSF_ZW.Split('>');
                            if (num2 > Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else if (item.YSF_ZW.Contains("<"))
                        {
                            string[] objysf = item.YSF_ZW.Split('<');
                            if (num2 < Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else
                        {
                            if (num2 == Convert.ToSingle(item.YSF_ZW))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;
                            }
                        }
                    }
                }
                if (type == "视觉-运动整合")
                {
                    if (!String.IsNullOrEmpty(item.YSF_SJYD))
                    {
                        if (item.YSF_SJYD.Contains("-"))
                        {
                            string[] objysf = item.YSF_SJYD.Split('-');
                            if (num2 >= Convert.ToSingle(objysf[0]) && num2 <= Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }

                        }
                        else if (item.YSF_SJYD.Contains(">"))
                        {
                            string[] objysf = item.YSF_SJYD.Split('>');
                            if (num2 > Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else if (item.YSF_SJYD.Contains("<"))
                        {
                            string[] objysf = item.YSF_SJYD.Split('<');
                            if (num2 < Convert.ToSingle(objysf[1]))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                        else
                        {
                            if (num2 == Convert.ToSingle(item.YSF_SJYD))
                            {
                                bfw.Text = item.BFW;
                                bzf.Text = item.BZF;
                                bzf1.Text = item.BZF;

                            }
                        }
                    }
                }
            }





        }

        private void fanshe_ysf_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(fanshe_ysf.Text.Trim()))
            {
                if (CommonHelper.IsNumber(fanshe_ysf.Text.Trim()))
                {

                    SetTextNum_yuanshi("反射", fanshe_ysf.Text.Trim(), fanshe_bfw, fanshe_bzf1, fanshe_bzf2);
                    SetTextNum_xiangdangnianling("反射", fanshe_ysf.Text.Trim(), fanshe_xdnl);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    fanshe_ysf.Focus();
                }
            }
        }

        private void zishi_ysf_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(zishi_ysf.Text.Trim()))
            {
                if (CommonHelper.IsNumber(zishi_ysf.Text.Trim()))
                {

                    SetTextNum_yuanshi("姿势", zishi_ysf.Text.Trim(), zishi_bfw, zishi_bzf1, zishi_bzf2);
                    SetTextNum_xiangdangnianling("姿势", zishi_ysf.Text.Trim(), zishi_xdnl);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    zishi_ysf.Focus();
                }
            }
        }

        private void yidong_ysf_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(yidong_ysf.Text.Trim()))
            {
                if (CommonHelper.IsNumber(yidong_ysf.Text.Trim()))
                {
                    SetTextNum_yuanshi("移动", yidong_ysf.Text.Trim(), yidong_bfw, yidong_bzf1, yidong_bzf2);
                    SetTextNum_xiangdangnianling("移动", yidong_ysf.Text.Trim(), yidong_xdnl);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    yidong_ysf.Focus();
                }
            }
        }

        private void caozuo_ysf_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(caozuo_ysf.Text.Trim()))
            {
                if (CommonHelper.IsNumber(caozuo_ysf.Text.Trim()))
                {
                    SetTextNum_yuanshi("实物操作", caozuo_ysf.Text.Trim(), caozuo_bfw, caozuo_bzf1, caozuo_bzf2);
                    SetTextNum_xiangdangnianling("实物操作", caozuo_ysf.Text.Trim(), caozuo_xdnl);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    caozuo_ysf.Focus();
                }
            }
        }

        private void zhuawo_ysf_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(zhuawo_ysf.Text.Trim()))
            {
                if (CommonHelper.IsNumber(zhuawo_ysf.Text.Trim()))
                {
                    SetTextNum_yuanshi("抓握", zhuawo_ysf.Text.Trim(), zhuawo_bfw, zhuawo_bzf1, zhuawo_bzf2);
                    SetTextNum_xiangdangnianling("抓握", zhuawo_ysf.Text.Trim(), zhuawo_xdnl);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    zhuawo_ysf.Focus();
                }
            }
        }

        private void shijue_ysf_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(shijue_ysf.Text.Trim()))
            {
                if (CommonHelper.IsNumber(shijue_ysf.Text.Trim()))
                {
                    SetTextNum_yuanshi("视觉-运动整合", shijue_ysf.Text.Trim(), shijue_bfw, shijue_bzf1, shijue_bzf2);
                    SetTextNum_xiangdangnianling("视觉-运动整合", shijue_ysf.Text.Trim(), shijue_xdnl);
                }
                else
                {
                    MessageBox.Show("请输入正确值");
                    shijue_ysf.Focus();
                }
            }
        }


        public void ClearText(TextBox textBox, TextBox baifenwei, TextBox bzf, TextBox bzf1, TextBox xiangdangnianling)
        {
            if (string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                baifenwei.Text = "";
                bzf.Text = "";
                bzf1.Text = "";
                xiangdangnianling.Text = "";
            }
        }

        private void fanshe_ysf_TextChanged(object sender, EventArgs e)
        {
            ClearText(fanshe_ysf, fanshe_bfw, fanshe_bzf1, fanshe_bzf2, fanshe_xdnl);
        }

        private void zishi_ysf_TextChanged(object sender, EventArgs e)
        {
            ClearText(zishi_ysf, zishi_bfw, zishi_bzf1, zishi_bzf2, zishi_xdnl);
        }

        private void yidong_ysf_TextChanged(object sender, EventArgs e)
        {
            ClearText(yidong_ysf, yidong_bfw, yidong_bzf1, yidong_bzf2, yidong_xdnl);
        }

        private void caozuo_ysf_TextChanged(object sender, EventArgs e)
        {
            ClearText(caozuo_ysf, caozuo_bfw, caozuo_bzf1, caozuo_bzf2, caozuo_xdnl);
        }

        private void zhuawo_ysf_TextChanged(object sender, EventArgs e)
        {
            ClearText(zhuawo_ysf, zhuawo_bfw, zhuawo_bzf1, zhuawo_bzf2, zhuawo_xdnl);
        }

        private void shijue_ysf_TextChanged(object sender, EventArgs e)
        {
            ClearText(shijue_ysf, shijue_bfw, shijue_bzf1, shijue_bzf2, shijue_xdnl);
        }

        private void fanshe_bzf1_Leave(object sender, EventArgs e)
        {

        }

        private void zhuawo_bzf1_Leave(object sender, EventArgs e)
        {

        }

        private void fanshe_bzf2_Leave(object sender, EventArgs e)
        {

        }

        private void bzf_gmq_Leave(object sender, EventArgs e)
        {

        }

        public void SetTextNum_sandbzf(string type, string defen, Control fys_gmq, Control bfw_gmq)
        {
            string[] splitvals3 = null;

            if (type == "粗大运动")
            {
                if (Convert.ToSingle(defen) >= 3)
                {
                    splitvals3 = sandbzf.Getfs_sange(defen).Split(',');

                    fys_gmq.Text = splitvals3[0];
                    bfw_gmq.Text = splitvals3[1];
                }
                else
                {
                    fys_gmq.Text = "0";
                    bfw_gmq.Text = "0";
                }
            }
            if (type == "精细运动")
            {
                if (Convert.ToSingle(defen) >= 2)
                {

                    splitvals3 = sandbzf.Getjx_sange(defen).Split(',');

                    fys_gmq.Text = splitvals3[0];
                    bfw_gmq.Text = splitvals3[1];
                }
                else
                {
                    fys_gmq.Text = "0";
                    bfw_gmq.Text = "0";
                }
            }
            if (type == "总运动")
            {
                if (Convert.ToSingle(defen) >= 5)
                {

                    splitvals3 = sandbzf.Getz_sange(defen).Split(',');

                    fys_gmq.Text = splitvals3[0];
                    bfw_gmq.Text = splitvals3[1];
                }
                else
                {
                    fys_gmq.Text = "0";
                    bfw_gmq.Text = "0";
                }
            }



        }

        private void bzf_fmq_Leave(object sender, EventArgs e)
        {

        }

        private void bzf_tmq_Leave(object sender, EventArgs e)
        {

        }

        private void fanshe_bzf1_TextChanged(object sender, EventArgs e)
        {
            int fanshe_bzf = String.IsNullOrEmpty(fanshe_bzf1.Text.Trim()) ? 0 : Convert.ToInt32(fanshe_bzf1.Text.Trim());

            int zishi_bzf = String.IsNullOrEmpty(zishi_bzf1.Text.Trim()) ? 0 : Convert.ToInt32(zishi_bzf1.Text.Trim());

            int yidong_bzf = String.IsNullOrEmpty(yidong_bzf1.Text.Trim()) ? 0 : Convert.ToInt32(yidong_bzf1.Text.Trim());

            int caozuo_bzf = String.IsNullOrEmpty(caozuo_bzf1.Text.Trim()) ? 0 : Convert.ToInt32(caozuo_bzf1.Text.Trim());

            bzf_gmq.Text = (fanshe_bzf + zishi_bzf + yidong_bzf + caozuo_bzf).ToString();
        }

        private void zhuawo_bzf1_TextChanged(object sender, EventArgs e)
        {
            int zhuawo_bzf = String.IsNullOrEmpty(zhuawo_bzf1.Text.Trim()) ? 0 : Convert.ToInt32(zhuawo_bzf1.Text.Trim());
            int shijue_bzf = String.IsNullOrEmpty(shijue_bzf1.Text.Trim()) ? 0 : Convert.ToInt32(shijue_bzf1.Text.Trim());

            bzf_fmq.Text = (zhuawo_bzf + shijue_bzf).ToString();
        }

        private void fanshe_bzf2_TextChanged(object sender, EventArgs e)
        {
            int fanshe_bzf = String.IsNullOrEmpty(fanshe_bzf2.Text.Trim()) ? 0 : Convert.ToInt32(fanshe_bzf2.Text.Trim());

            int zishi_bzf = String.IsNullOrEmpty(zishi_bzf2.Text.Trim()) ? 0 : Convert.ToInt32(zishi_bzf2.Text.Trim());

            int yidong_bzf = String.IsNullOrEmpty(yidong_bzf2.Text.Trim()) ? 0 : Convert.ToInt32(yidong_bzf2.Text.Trim());

            int caozuo_bzf = String.IsNullOrEmpty(caozuo_bzf2.Text.Trim()) ? 0 : Convert.ToInt32(caozuo_bzf2.Text.Trim());
            int zhuawo_bzf = String.IsNullOrEmpty(zhuawo_bzf2.Text.Trim()) ? 0 : Convert.ToInt32(zhuawo_bzf2.Text.Trim());
            int shijue_bzf = String.IsNullOrEmpty(shijue_bzf2.Text.Trim()) ? 0 : Convert.ToInt32(shijue_bzf2.Text.Trim());

            bzf_tmq.Text = (fanshe_bzf + zishi_bzf + yidong_bzf + caozuo_bzf + zhuawo_bzf + shijue_bzf).ToString();

        }

        private void bzf_gmq_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bzf_gmq.Text.Trim()))
            {
                if (CommonHelper.IsNumber(bzf_gmq.Text.Trim()))
                {
                    SetTextNum_sandbzf("粗大运动", bzf_gmq.Text.Trim(), fys_gmq, bfw_gmq);

                }
                else
                {
                    MessageBox.Show("请输入正确值");

                }
            }
        }

        private void bzf_fmq_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bzf_fmq.Text.Trim()))
            {
                if (CommonHelper.IsNumber(bzf_fmq.Text.Trim()))
                {
                    SetTextNum_sandbzf("精细运动", bzf_fmq.Text.Trim(), fys_fmq, bfw_fmq);

                }
                else
                {
                    MessageBox.Show("请输入正确值");

                }
            }
        }

        private void bzf_tmq_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(bzf_tmq.Text.Trim()))
            {
                if (CommonHelper.IsNumber(bzf_tmq.Text.Trim()))
                {
                    SetTextNum_sandbzf("总运动", bzf_tmq.Text.Trim(), fys_tmq, bfw_tmq);

                }
                else
                {
                    MessageBox.Show("请输入正确值");

                }
            }
        }
    }
}
