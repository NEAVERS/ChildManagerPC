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
using ChildManager.Model;
using System.Xml;
using System.Data;
using System.Text;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web.Security;
using YCF.Extension;
using System.Linq;
using YCF.Common.vo;
using YCF.Model.NotMaps;
using YCF.BLL.Template;

namespace ChildManager.UI.nursinfo
{
    public partial class hs_WomenInfo : UserControl
    {
        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_InitComm(int port);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_ExitComm(int port);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_InitComm_Baud(int port, int combaud);
        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_ResetMifare(int icdev, int _Mode);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_Config_Card(int icdev, char cardtype);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_Card_Hex(int icdev, int port, [Out]StringBuilder rbuff);


        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_InitType(int port, int stu);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern short IC_CpuReset_Hex(int icdev, ref byte rlen, StringBuilder rbuff);

        public int cd_id = -1;
        IDCardValidation validation = new IDCardValidation();
        tab_person_databll personbll = new tab_person_databll();
        tb_childbasebll basebll = new tb_childbasebll();
        tb_childcheckbll checkbll = new tb_childcheckbll();
        tb_childbase _jibenobj = null;
        HisPatientInfo _hisPatientobj = null;
        XmlDocument doc = new XmlDocument();
        string _hospital = globalInfoClass.Hospital;
        public List<DicObj> listarea = new List<DicObj>();
        public List<DicObj> listfenzen = new List<DicObj>();
        //当前使用输入法
        InputLanguage InputHuoDong = null;
        private string jzch = "";

        public hs_WomenInfo()
        {
            InitializeComponent();
            doc.Load("省市区.xml");
            XmlNode provinces = doc.SelectSingleNode("/ProvinceCity");
            foreach (XmlNode province in provinces.ChildNodes)
            {
                //this.province1.Items.Add(province.Name);
            }
            //this.province1.SelectedIndex = 0;

            //fathereducation.Items.Clear();
            //mothereducation.Items.Clear();
            //doc.Load("wenhua.xml");
            //provinces = doc.SelectSingleNode("/root");
            //foreach (XmlNode province in provinces.ChildNodes)
            //{
            //    this.fathereducation.Items.Add(province.Name);
            //    this.mothereducation.Items.Add(province.Name);
            //}
            //fathereducation.SelectedIndex = 0;
            //mothereducation.SelectedIndex = 0;

            refreshInfo();
            CommonHelper.SetAllControls(panel2);
            CommonHelper.SetAllControls(panel5);
            //设置就诊卡号，patient_id,保健卡号按回车事件
            SetControlsEvents();
            

            //CommonHelper.setCombValue(ck_fz, "fenzhen.xml");
            setCombValueAndText(new ComboBox(), "area.xml");
            SetData(fenzhenlist, listfenzen, "fenzhen");
            fenzhenlist.Parent = this;
            fenzhenlist.Left =  panel4.Left +ck_fz.Left;
            fenzhenlist.Top = panel4.Top + ck_fz.Top + ck_fz.Height * 2;
            fenzhenlist.BringToFront();
        }

        private void SetControlsEvents()
        {
            healthcardno.RemoveControlEvent("EventKeyPress");
            healthcardno.KeyPress += healthcardno_KeyPress;

            jiuzhencardno.RemoveControlEvent("EventKeyPress");
            jiuzhencardno.KeyPress += jiuzhencardno_KeyPress;

            patient_id.RemoveControlEvent("EventKeyPress");
            patient_id.KeyPress += patient_id_KeyPress;
 
            childbirthday.RemoveControlEvent("EventKeyDown");
            childbirthday.KeyDown += childbirthday_KeyDown;
            
        }

        public hs_WomenInfo(object obj)
        {
            InitializeComponent();
            ChildBaseInfoObj baseobj = obj as ChildBaseInfoObj;
            cd_id = baseobj.id;
            refreshInfo();
        }


        //主窗体加载时自动打开基本信息的一般信息窗口
        public void refreshInfo()
        {
            Cursor.Current = Cursors.WaitCursor;
            bindDataNowday();//绑定左边列表数据

            dataGridView1_CellEnter(null, null);
            Cursor.Current = Cursors.Default;
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
            this.cd_id = Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[2].Value);//孕妇id
            //_jibenobj = basebll.GetByCardNo(jiuzhencardno.Text);
            _jibenobj = basebll.Get(this.cd_id);
            if (_jibenobj != null)
            {
                CommonHelper.setForm(_jibenobj, panel2.Controls);
                this.patient_id.Text = _jibenobj.patient_id;
                this.jiuzhencardno.Text = _jibenobj.jiuzhencardno;
                if (!string.IsNullOrEmpty(_jibenobj.patient_id) || !string.IsNullOrEmpty(_jibenobj.jiuzhencardno))
                {
                    _hisPatientobj = ReadCard.GetHisPatientInfo(_jibenobj.patient_id, _jibenobj.jiuzhencardno, true);
                    if (_hisPatientobj != null)
                    {
                        string[] strInfo = null;
                        string visit_time = "";
                        string type = "";
                        if (!string.IsNullOrEmpty(_hisPatientobj.SIGNAL_SOURCE_CODE))
                        {
                            string[] time = null;
                            strInfo = _hisPatientobj.SIGNAL_SOURCE_CODE.Split('_');
                            time = strInfo[0].Split('-');
                            visit_time = "20" + time[2] + "-" + time[1].Replace("? ", "") + "-" + time[0];
                            type = strInfo[1];
                        }
                        SIGNAL_SOURCE_CODE.Text = type;
                        if (type.Contains("("))
                        {
                            string[] typename = null;
                            typename = type.Split('(');
                            ck_fz.Text = typename[0];
                        }
                        if (string.IsNullOrEmpty(_jibenobj.jiuzhencardno))
                        {
                            this.jiuzhencardno.Text = _hisPatientobj.VISIT_CARD_NO;
                        }
                    }
                }
                else
                {
                    SIGNAL_SOURCE_CODE.Text = "_________________";
                }

                ClearControls(panel5);
                //this.childbirthday.Value =Convert.ToDateTime( _jibenobj.childbirthday);
            }
            RefreshCheckList();
            Cursor.Current = Cursors.WaitCursor;
            dataGridView2.ClearSelection();
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
                IList<tb_childbase> list = basebll.GetListByCheckHs(checkday, isjiuzhen, _hospital,cd_id);
                if (list != null)
                {
                    foreach (tb_childbase obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, obj.healthcardno, obj.childname, obj.id.ToString(), obj.childgender, obj.childbirthday);// 
                        dataGridView1.Rows.Add(row);
                        if (obj.id == this.cd_id)
                        {
                            row.Selected = true;
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
                if (dataGridView1.Rows.Count >= 1)
                {
                    if (dataGridView1.SelectedRows.Count <= 0)
                        dataGridView1.Rows[0].Selected = true;
                }
                else
                {
                    childbirthday.Value = DateTime.Now;
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
            Paneltsb_searchInfo frmsearcher = new Paneltsb_searchInfo(false);
            frmsearcher.ckb_check.Checked = false;
            frmsearcher.ShowDialog();
            if (frmsearcher.DialogResult == DialogResult.OK)
            {
                tb_childbase jibenobj = frmsearcher.returnval;
                if (jibenobj != null)
                {
                    SetInfo(sender,jibenobj);
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            bool b = false;
            if (String.IsNullOrEmpty(childname.Text.Trim()))
            {
                MessageUtil.ShowTips("儿童姓名不能为空!");
                childname.Focus();
                return;
            }

            if (String.IsNullOrEmpty(childgender.Text.Trim()))
            {
                MessageUtil.ShowTips("儿童性别不能为空!");
                childgender.Focus();
                return;
            }
            if (!String.IsNullOrEmpty(telephone.Text.Trim()))
            {
                if (!validation.Checkmobilephone(telephone.Text.Trim()))
                {
                    MessageBox.Show("手机号输入有误，请重输!", "系统提示");
                    telephone.Focus();
                    return;
                }
            }

            tb_childbase obj = getChildBaseInfoObj();
            if (_jibenobj != null)
            {
                obj.id = _jibenobj.id;
                obj.healthcardno = _jibenobj.healthcardno;
                obj.gaoweiyinsu = _jibenobj.gaoweiyinsu;
                b = basebll.Update(obj);
                if (b)
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    _jibenobj = obj;
                    cd_id = _jibenobj.id;
                    checkweight.Focus();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            else
            {
                //自动生成保健卡号
                obj.healthcardno = basebll.stateval().ToString();//保健卡号
                b = basebll.Add(obj);
                if (b)
                {
                    _jibenobj = obj;
                    this.healthcardno.Text = obj.healthcardno;
                    MessageBox.Show("保存成功！", "软件提示");
                    cd_id = _jibenobj.id;
                    checkweight.Focus();
                    dataGridView2.Rows.Clear();

                    string[] rowmrn = new string[] { _jibenobj.healthcardno, _jibenobj.childname, _jibenobj.id.ToString(), _jibenobj.childgender, _jibenobj.childbirthday };
                    dataGridView1.Rows.Add(rowmrn);
                    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true;//选中查询到的数据行
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }

            dataGridView2.ClearSelection();
            saveChildCheck(false);
            ClearControls(panel5);
            dataGridView2.Rows[0].Selected = true;
            //saveChildCheck(false);
            dataGridView2_CellContentClick(dataGridView2, new DataGridViewCellEventArgs(1, 0));

        }

        /// <summary>
        /// 清空控件中子控件内容
        /// </summary>
        /// <param name="control">panel或groupbox名称</param>
        private void ClearControls(Control control)
        {
            foreach (Control item in control.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Text = "";
                }
                else if (item is ComboBox)
                {
                    ComboBox cbx = item as ComboBox;
                    cbx.Text = "";
                }
                else if (item is DateTimePicker)
                {
                    DateTimePicker dtp = item as DateTimePicker;
                    dtp.Value = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private tb_childbase getChildBaseInfoObj()
        {
            tb_childbase obj = CommonHelper.GetObj<tb_childbase>(panel2.Controls);
            //obj.childbirthday = this.childbirthday.Text;
            obj.status = "1";
            obj.childbuildday = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            obj.childbuildhospital = "重医大附属儿童医院";
            return obj;
        }

        /// <summary>
        /// 初始化基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX4_Click(object sender, EventArgs e)
        {
            this.cd_id = -1;
            _jibenobj = null;

            healthcardno.Text = "";//保健卡号
            childname.Text = "";//姓名
            childgender.Text = "男";//性别
            childbirthday.Value = DateTime.Now;
            jiuzhencardno.Text = "";
            telephone2.Text = "";
            fatherheight.Text = "";
            fathereducation.Text = "本科";
            //fatherjob.Text = "";
            motherheight.Text = "";
            mothereducation.Text = "本科";
            //motherjob.Text = "";
            telephone.Text = "";
            //address.Text = "";
            this.patient_id.Text = "";
          
            //发证
            //obj.ImmuneUnit = cbx_immuneUnit.Text.Trim();
            //obj.ImmuneDay = dateTimeInput3.Value.ToString("yyyy-MM-dd HH:mm:ss");
            //obj.Community = comboBox5.Text.Trim();
            //obj.CensusRegister = comboBox4.Text.Trim();
            //出生史
            cs_fetus.Text = "1";
            cs_produce.Text = "1";
            sfzy.Text = "足月";
            cs_week.Text = "";
            cs_day.Text = "";
            modedelivery.Text = "剖腹产";
            //obj.FetusNumber = cbx_fetusNumber.Text = ;
            birthweight.Text = "";
            birthheight.Text = "";
            identityno.Text = "";
            sfzlb.Text = "";
            CommonHelper.setcheckedValue(sfst, "");
            jtzz_cx.Text = "城";
            province.Text = "";
            SIGNAL_SOURCE_CODE.Text = "_________________";
            healthcardno.Focus();
            ClearControls(panel5);
            dataGridView2.Rows.Clear();//清空列表
            healthcardno.Select();
        }

        /// <summary>
        /// 健康卡号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void healthcardno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(healthcardno.Text.Trim()))
                {
                    return;
                }
                SIGNAL_SOURCE_CODE.Text = "_________________";
                _jibenobj = basebll.GetByHealthNo(healthcardno.Text);
                if (_jibenobj != null)
                {
                    bool isinclude = true;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == _jibenobj.healthcardno)
                        {
                            dataGridView1.Rows[i].Selected = true;
                            isinclude = false;
                            break;
                        }
                    }
                    if (isinclude)
                    {
                        //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                        string[] rowmrn = new string[] { _jibenobj.healthcardno, _jibenobj.childname, _jibenobj.id.ToString(), _jibenobj.childgender, _jibenobj.childbirthday };
                        dataGridView1.Rows.Add(rowmrn);
                        this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                    }
                    dataGridView1_CellEnter(sender, null);
                    cd_id = _jibenobj.id;
                    CommonHelper.setForm(_jibenobj, panel2.Controls);
                    telephone2.Focus();
                }
                else
                {
                    MessageBox.Show("无儿童信息！");
                }
            }
        }

        /// <summary>
        /// 就诊卡回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jiuzhencardno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(jiuzhencardno.Text.Trim()))
                {
                    return;
                }
                SIGNAL_SOURCE_CODE.Text = "_________________";
                _jibenobj = basebll.GetByCardNo(jiuzhencardno.Text);
                if (_jibenobj != null)
                {
                    bool isinclude = true;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == _jibenobj.healthcardno)
                        {
                            dataGridView1.Rows[i].Selected = true;
                            isinclude = false;
                            break;
                        }
                    }
                    if (isinclude)
                    {
                        //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                        string[] rowmrn = new string[] { _jibenobj.healthcardno, _jibenobj.childname, _jibenobj.id.ToString(), _jibenobj.childgender, _jibenobj.childbirthday };
                        dataGridView1.Rows.Add(rowmrn);
                        this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                    }
                    dataGridView1_CellEnter(sender, null);
                    IList<HisPatientInfo> _hisVisiCardlist = ReadCard.GetListHisPatientInfo( null,jiuzhencardno.Text.Trim(),true);
                    if (_hisVisiCardlist != null)
                    {
                        if (_hisVisiCardlist.Count > 1)
                        {
                            CheckHisInfo(sender, _hisVisiCardlist);
                        }
                        else
                        {
                            foreach (HisPatientInfo _hisPatientobj in _hisVisiCardlist)
                            {
                                if (_hisPatientobj != null)
                                {
                                    string[] strInfo = null;
                                    string visit_time = "";
                                    string type = "";
                                    if (!string.IsNullOrEmpty(_hisPatientobj.SIGNAL_SOURCE_CODE))
                                    {
                                        string[] time = null;
                                        strInfo = _hisPatientobj.SIGNAL_SOURCE_CODE.Split('_');
                                        time = strInfo[0].Split('-');
                                        visit_time = "20" + time[2] + "-" + time[1].Replace("? ", "") + "-" + time[0];
                                        type = strInfo[1];
                                    }
                                    SIGNAL_SOURCE_CODE.Text = type;
                                    if (type.Contains("("))
                                    {
                                        string[] typename = null;
                                        typename = type.Split('(');
                                        ck_fz.Text = typename[0];
                                    }
                                }
                            }
                        }
                    }
                    
                    telephone2.Focus();
                }
                else
                {
                    IList<HisPatientInfo> _hisVisiCardlist = ReadCard.GetListHisPatientInfo(null, jiuzhencardno.Text.Trim(),true);
                    if (_hisVisiCardlist != null)
                    {
                        if (_hisVisiCardlist.Count > 1)
                        {
                            CheckHisInfo(sender, _hisVisiCardlist);
                        }
                        else
                        {
                            foreach (HisPatientInfo _hisPatientobj in _hisVisiCardlist)
                            {
                                if (_hisPatientobj != null)
                                {
                                    jiuzhencardno.Text = _hisPatientobj.VISIT_CARD_NO;
                                    jzch = _hisPatientobj.VISIT_CARD_NO;//记录当前就诊卡号
                                    jiuzhencardno.Text = jzch;
                                    childname.Text = _hisPatientobj.PAT_NAME;
                                    childgender.Text = _hisPatientobj.PHYSI_SEX_NAME;
                                    childbirthday.Value = Convert.ToDateTime(_hisPatientobj.DATE_BIRTH);
                                    telephone.Text = _hisPatientobj.PHONE_NO;
                                    this.patient_id.Text = _hisPatientobj.PAT_INDEX_NO;
                                    string[] strInfo = null;
                                    string visit_time = "";
                                    string type = "";
                                    if (!string.IsNullOrEmpty(_hisPatientobj.SIGNAL_SOURCE_CODE))
                                    {
                                        string[] time = null;
                                        strInfo = _hisPatientobj.SIGNAL_SOURCE_CODE.Split('_');
                                        time = strInfo[0].Split('-');
                                        visit_time = "20" + time[2] + "-" + time[1].Replace("? ", "") + "-" + time[0];
                                        type = strInfo[1];
                                    }
                                    SIGNAL_SOURCE_CODE.Text = type;
                                    if (type.Contains("("))
                                    {
                                        string[] typename = null;
                                        typename = type.Split('(');
                                        ck_fz.Text = typename[0];
                                    }

                                    if (!string.IsNullOrEmpty(patient_id.Text))
                                    {
                                        patient_id_KeyPress(sender, e);
                                    }
                                    province.Focus();
                                }
                                else
                                {
                                    MessageBox.Show("未找到挂号信息，请检查就诊号是否正确！", "提示");
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("该卡号无信息！");
                    }
                }
            }
        }

        /// <summary>
        /// 病人ID回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void patient_id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(patient_id.Text.Trim()))
                {
                    return;
                }
                SIGNAL_SOURCE_CODE.Text = "_________________";
                _jibenobj = basebll.GetByPatientId(patient_id.Text.Trim());
                if (_jibenobj != null)
                {

                    bool isinclude = true;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == _jibenobj.healthcardno)
                        {
                            dataGridView1.Rows[i].Selected = true;
                            isinclude = false;
                            break;
                        }
                    }
                    if (isinclude)
                    {
                        //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                        string[] rowmrn = new string[] { _jibenobj.healthcardno, _jibenobj.childname, _jibenobj.id.ToString(), _jibenobj.childgender, _jibenobj.childbirthday };
                        dataGridView1.Rows.Add(rowmrn);
                        this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                    }
                    dataGridView1_CellEnter(sender, null);
                    if (jzch != "")
                    {
                        this.jiuzhencardno.Text = jzch;
                        jzch = "";
                    }
                    IList<HisPatientInfo> _hisPatientlist = ReadCard.GetListHisPatientInfo(patient_id.Text.Trim(), null, true);
                    if (_hisPatientlist != null)
                    {
                        if (_hisPatientlist.Count > 1)
                        {
                            CheckHisInfo(sender, _hisPatientlist);
                        }
                        else
                        {
                            foreach (HisPatientInfo _hisPatientobj in _hisPatientlist)
                            {
                                if (_hisPatientobj != null)
                                {
                                    string[] strInfo = null;
                                    string visit_time = "";
                                    string type = "";
                                    if (!string.IsNullOrEmpty(_hisPatientobj.SIGNAL_SOURCE_CODE))
                                    {
                                        string[] time = null;
                                        strInfo = _hisPatientobj.SIGNAL_SOURCE_CODE.Split('_');
                                        time = strInfo[0].Split('-');
                                        visit_time = "20" + time[2] + "-" + time[1].Replace("? ", "") + "-" + time[0];
                                        type = strInfo[1];
                                    }
                                    SIGNAL_SOURCE_CODE.Text = type;
                                    if (type.Contains("("))
                                    {
                                        string[] typename = null;
                                        typename = type.Split('(');
                                        ck_fz.Text = typename[0];
                                    }
                                }
                            }
                        }
                    }
                    telephone2.Focus();
                }
                else
                {
                    IList<HisPatientInfo> _hisPatientlist = ReadCard.GetListHisPatientInfo(patient_id.Text.Trim(), null, true);
                    if (_hisPatientlist != null)
                    {
                        if (_hisPatientlist.Count > 1)
                        {
                            CheckHisInfo(sender, _hisPatientlist);
                        }
                        else
                        {
                            buttonX4.PerformClick();//清空信息

                            foreach (HisPatientInfo _hisPatientobj in _hisPatientlist)
                            {
                                if (_hisPatientobj != null)
                                {
                                    jiuzhencardno.Text = _hisPatientobj.VISIT_CARD_NO;//就诊卡号
                                    patient_id.Text = _hisPatientobj.PAT_INDEX_NO;//病人ID
                                    childname.Text = _hisPatientobj.PAT_NAME;//病人姓名
                                    childgender.Text = _hisPatientobj.PHYSI_SEX_NAME;//性别
                                    childbirthday.Value = Convert.ToDateTime(_hisPatientobj.DATE_BIRTH);//生日
                                    telephone.Text = _hisPatientobj.PHONE_NO;//电话号码

                                    string[] strInfo = null;
                                    string visit_time = "";
                                    string type = "";
                                    if (!string.IsNullOrEmpty(_hisPatientobj.SIGNAL_SOURCE_CODE))
                                    {
                                        string[] time = null;
                                        strInfo = _hisPatientobj.SIGNAL_SOURCE_CODE.Split('_');
                                        time = strInfo[0].Split('-');
                                        visit_time = "20" + time[2] + "-" + time[1].Replace("? ", "") + "-" + time[0];
                                        type = strInfo[1];
                                    }
                                    SIGNAL_SOURCE_CODE.Text = type;//挂号信息
                                    if (type.Contains("("))
                                    {
                                        string[] typename = null;
                                        typename = type.Split('(');
                                        ck_fz.Text = typename[0];//自动分诊
                                    }
                                    province.Focus();
                                }
                            }
                        }
                    }
                }
            }
        }
        private string GetMQRequest(string fid, string req)
        {
            long ret = 0;
            string cmsgid = "";
            string getcmsg = "";
            MQDLL.MQFuntion MQManagment = new MQDLL.MQFuntion();
            ret = MQManagment.connectMQ();
            ret = MQManagment.putMsg(fid, req, ref cmsgid);
            ret = MQManagment.getMsgById(fid, cmsgid, 60000, ref getcmsg);
            MQManagment.disconnectMQ();
            return getcmsg;
        }

        private void childbirthday_ValueChanged(object sender, EventArgs e)
        {
            //if(!String.IsNullOrEmpty(childbirthday.Text))
            label_age.Text = CommonHelper.getAgeStr(childbirthday.Value.ToString("yyyy-MM-dd"));
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            ClearControls(panel5);
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            saveChildCheck(true);
            //调用体检数据新增
        }

        /// <summary>
        /// 保存儿童体检项目信息
        /// </summary>
        public void saveChildCheck(bool issave)
        {
            if (cd_id == -1)
            {
                MessageBox.Show("请先保存儿童基本信息", "系统提示");
                return;
            }
           
            tb_childcheck checkobj = GetObj();
            string fzinfo = ck_fz.Text.Trim();
            if (!issave)
            {
                if (SIGNAL_SOURCE_CODE.Text != "_________________")
                {
                    string[] typename = null;
                    typename = SIGNAL_SOURCE_CODE.Text.Split('(');
                    fzinfo = typename[0];
                    checkobj.ck_fz = fzinfo;
                }
            }
            tb_childcheck _checkobj = dataGridView2.SelectedRows.Count > 0 ? dataGridView2.SelectedRows[0].Tag as tb_childcheck : null;
            if (_checkobj == null)
            {
                if (checkbll.Add(checkobj))
                {
                    if (issave)
                    {
                        MessageBox.Show("保存成功！", "软件提示");
                    }
                    _checkobj = checkobj;
                    RefreshCheckList();
                    checkweight.Text = "";
                    checkheight.Text = "";
                    checkzuogao.Text = "";
                    checktouwei.Text = "";
                    ck_fz.Text = "";
                    dataGridView2.ClearSelection();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            else
            {
                checkobj.id = _checkobj.id;
                if (checkbll.UpdateNurse(checkobj))
                {
                    if(issave)
                    {
                        MessageBox.Show("保存成功！", "软件提示");
                    }
                    _checkobj = checkobj;
                    RefreshCheckList();
                    checkweight.Text = "";
                    checkheight.Text = "";
                    checkzuogao.Text = "";
                    checktouwei.Text = "";
                    ck_fz.Text = "";
                    dataGridView2.ClearSelection();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
        }

        /// <summary>
        /// 体检保存
        /// </summary>
        /// <returns></returns>
        private tb_childcheck GetObj()
        {
            tb_childcheck obj = CommonHelper.GetObj<tb_childcheck>(panel5.Controls);
            obj.hospital = _hospital;
            obj.childid = this.cd_id;
            return obj;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCheckList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataGridView2.Rows.Clear();
            if (this.cd_id != -1)
            {
                IList<tb_childcheck> checklist = checkbll.GetList(this.cd_id);
                
                if (checklist != null)
                {
                    checklist = checklist.OrderByDescending(x => x.checkday).ThenByDescending(x => x.id).ToList();
                    foreach (tb_childcheck checkobj in checklist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        //当前体检年龄
                        int [] age = CommonHelper.getAgeBytime(childbirthday.Value.ToString("yyyy-MM-dd"),checkobj.checkday);
                        string ageStr = age[0]+"岁"+age[1]+"月"+age[2]+"天";
                        row.CreateCells(dataGridView2, checkobj.checkday,ageStr,checkobj.checkweight, checkobj.checkheight, checkobj.checkzuogao, checkobj.checktouwei, checkobj.ck_fz);
                        row.Tag = checkobj;
                        dataGridView2.Rows.Add(row);
                        if (checkobj.checkday == checkday.Text)
                        {
                            row.Selected = true;
                        }
                    }
      
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的记录！");
                return;
            }
            tb_childcheck _checkobj = dataGridView2.SelectedRows[0].Tag as tb_childcheck;
            if (_checkobj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (checkbll.Delete(_checkobj.id))
                        {
                            MessageBox.Show("删除成功!", "软件提示");
                            RefreshCheckList();
                            RefreshCheckList();
                            checkweight.Text = "";
                            checkheight.Text = "";
                            checkzuogao.Text = "";
                            checktouwei.Text = "";
                            ck_fz.Text = "";
                            dataGridView2.ClearSelection();
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

        /// <summary>
        /// 加载dataGridView2选中行记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                tb_childcheck checkobj = dataGridView2.SelectedRows[0].Tag as tb_childcheck;
                CommonHelper.setForm(checkobj, panel5.Controls);
            }
        }

        public void setCombValueAndText(ComboBox cbx, string xmlname)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlname);    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            XmlNodeList personNodes = rootElem.GetElementsByTagName("person"); //获取person子节点集合  
            //cbx.Items.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));

            foreach (XmlNode node in personNodes)
            {
                DicObj dicobj = new DicObj();
                dicobj.name = ((XmlElement)node).GetAttribute("name");   //获取name属性值  
                dicobj.id = ((XmlElement)node).GetAttribute("id");   //获取name属性值 
                listarea.Add(dicobj);
                dt.Rows.Add(dicobj.id,dicobj.name);
                //cbx.Items.Add(dicobj.name);

            }
            //cbx.DataSource = listarea;
            //cbx.DisplayMember = "name";
            //cbx.ValueMember = "id";

            //添加到listbox
            diqulist.DataSource = dt;
            diqulist.DisplayMember = "name";
            diqulist.ValueMember = "id";
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
            IList<tab_person_data> list = personbll.GetList(type);
            DataTable dt = new DataTable();
            dt.Columns.Add("code", typeof(string));
            dt.Columns.Add("name", typeof(string));

            foreach (tab_person_data obj in list)
            {
                DicObj dicobj = new DicObj();
                dicobj.name = obj.name;   //获取name属性值  
                dicobj.code = obj.code;   //获取name属性值 
                diclist.Add(dicobj);
                dt.Rows.Add(dicobj.code, dicobj.name);
            }
            con.DataSource = dt;
            con.DisplayMember = "name";
            con.ValueMember = "code";
        }

        private void childbirthday_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{right}");
            }
        }
   
        private void province_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        //切换输入法为英文输入法
        private void province_Enter(object sender, EventArgs e)
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
            //InputLanguage CurrentInput = InputLanguage.CurrentInputLanguage;
        }

        //切换回原输入法
        private void province_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputHuoDong;
        }

        /// <summary>
        /// 若his查询出多条数据，可以进行选择加载
        /// 2017-11-22 cc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="_hisPatientlist">数据集合</param>
        public void CheckHisInfo(object sender, IList<HisPatientInfo> _hisPatientlist)
        {
            Panel_searchHisInfo frmsearcher = new Panel_searchHisInfo(_hisPatientlist);
            frmsearcher.ShowDialog();//打开查询界面
            if (frmsearcher.DialogResult == DialogResult.OK)//查询界面关闭
            {
                HisPatientInfo hisobj = frmsearcher.returnval;//查询界面返回obj
                if (hisobj != null)//如果obj不为空
                {
                    buttonX4.PerformClick();//清空界面

                    patient_id.Text = hisobj.PAT_INDEX_NO;//界面赋值查询的病人ID

                    tb_childbase jibenobj = basebll.GetByPatientId(hisobj.PAT_INDEX_NO);//通过病人ID查询本地数据库
                    if (jibenobj == null)//查询结果为null
                    {
                        //MessageBox.Show("（通过病人ID）本地无数据！");
                        jibenobj = basebll.GetByVisitNo(hisobj.VISIT_CARD_NO);//通过就诊卡号查询本地数据库
                        if (jibenobj == null)//查询结果为null
                        {
                            //MessageBox.Show("（通过就诊卡号）本地无数据！");
                            jiuzhencardno.Text = hisobj.VISIT_CARD_NO;
                            childname.Text = hisobj.PAT_NAME;
                            childgender.Text = hisobj.PHYSI_SEX_NAME;
                            patient_id.Text = hisobj.PAT_INDEX_NO;
                            telephone.Text = hisobj.PHONE_NO;
                            childbirthday.Text = hisobj.DATE_BIRTH;
                            SetHisInfo(hisobj);
                            telephone2.Focus();
                        }
                        else
                        {
                            SetHisInfo(hisobj);
                            SetInfo(sender, jibenobj);//加载本地数据
                        }
                    }
                    else
                    {
                        SetHisInfo(hisobj);
                        SetInfo(sender, jibenobj);//加载本地数据
                    }
                }

            }
        }

        /// <summary>
        ///读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReadCard_Click(object sender, EventArgs e)
        {
            string chipnum=ReadCard.GetChipNum();
            ChipRequest(chipnum);//测试芯片号
        }

        /// <summary>
        /// 根据芯片号查询
        /// 2017-11-07 cc
        /// </summary>
        /// <param name="chipnum">芯片号</param>
        public void ChipRequest(string chipnum)
        {
            YHRequest YHReq = new YHRequest(YHRequest.fid_get_hischip);
            YHReq.addQuery(YHRequest.item_chip, YHRequest.compy_equals, "'" + chipnum + "'", YHRequest.splice_and);
            var result = YHMQUtil<HisChipInfo>.get(YHReq);
            HisChipInfo hisobj = result.data;
            //MessageBox.Show("卡号："+hisobj.CARD_NO+"  芯片号："+ hisobj.CARD_CHIP_NO + "  类型：" + hisobj.CARD_TYPE + "  病人ID：" + hisobj.PAT_INDEX_NO);
            if (result.success)
            {
                string PAT_INDEX_NO = hisobj.PAT_INDEX_NO;
                string CARD_NO = hisobj.CARD_NO;

                if (!string.IsNullOrEmpty(PAT_INDEX_NO)|| !string.IsNullOrEmpty(CARD_NO))
                {
                    if (string.IsNullOrEmpty(CARD_NO))
                    {
                        patient_id.Text = PAT_INDEX_NO;
                        SetChildBase(PAT_INDEX_NO, CARD_NO);
                    }
                    else if (string.IsNullOrEmpty(PAT_INDEX_NO))
                    {
                        jiuzhencardno.Text = CARD_NO;
                        SetChildBase(PAT_INDEX_NO, CARD_NO);
                    }
                    else
                    {
                        patient_id.Text = PAT_INDEX_NO;
                        jiuzhencardno.Text = CARD_NO;
                        SetChildBase(PAT_INDEX_NO, CARD_NO);
                    }
                }
            }
            else
            {
                MessageBox.Show("未找到挂号信息，请检查就诊卡是否正确！", "提示");
                return;
            }
        }

        /// <summary>
        /// 通过病人ID，就诊卡号查询本地数据
        /// 2017-11-20 cc
        /// </summary>
        /// <param name="patient_id">病人ID</param>
        /// <param name="jiuzhencardno">就诊卡号</param>
        public void SetChildBase(string patient_id, string jiuzhencardno)
        {
            if (!string.IsNullOrEmpty(patient_id))
            {
                _jibenobj = basebll.GetByPatientId(patient_id);
                if (_jibenobj != null)
                {
                    bool isinclude = true;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == _jibenobj.healthcardno)
                        {
                            dataGridView1.Rows[i].Selected = true;
                            isinclude = false;
                            break;
                        }
                    }
                    if (isinclude)
                    {
                        //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                        string[] rowmrn = new string[] { _jibenobj.healthcardno, _jibenobj.childname, _jibenobj.id.ToString(), _jibenobj.childgender, _jibenobj.childbirthday };
                        dataGridView1.Rows.Add(rowmrn);
                        this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
                    }
                    dataGridView1_CellEnter(null, null);
                    //CommonHelper.setForm(_jibenobj, panel2.Controls);
                    //HisPatientInfo _hisPatientobj=VisitandPatientRequestOne(_jibenobj.patient_id, null);
                    //SetHisInfo(_hisPatientobj);
                    //if (string.IsNullOrEmpty(_jibenobj.jiuzhencardno))
                    //{
                    //    this.jiuzhencardno.Text = _hisPatientobj.VISIT_CARD_NO;
                    //}
                    telephone2.Focus();
                }
                else
                {
                    buttonX4.PerformClick();//清空界面
                    if (!string.IsNullOrEmpty(patient_id))
                    {
                        this.patient_id.Text = patient_id;
                        this.jiuzhencardno.Text = jiuzhencardno;
                        this.patient_id.Focus();
                        SendKeys.Send("{Enter}");
                    }
                }
            }
            else
            {
                _jibenobj = basebll.GetByCardNo(jiuzhencardno);
                if (_jibenobj != null)
                {
                    bool isinclude = true;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() == _jibenobj.healthcardno)
                        {
                            dataGridView1.Rows[i].Selected = true;
                            isinclude = false;
                            break;
                        }
                    }
                    if (isinclude)
                    {
                        //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                        string[] rowmrn = new string[] { _jibenobj.healthcardno, _jibenobj.childname, _jibenobj.id.ToString(), _jibenobj.childgender, _jibenobj.childbirthday };
                        dataGridView1.Rows.Add(rowmrn);
                        this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据
                    }
                    CommonHelper.setForm(_jibenobj, panel2.Controls);
                    telephone2.Focus();
                }
                else
                {
                    buttonX4.PerformClick();//清空界面
                    if (!string.IsNullOrEmpty(jiuzhencardno))
                    {
                        this.patient_id.Text = patient_id;
                        this.jiuzhencardno.Text = jiuzhencardno;
                        this.jiuzhencardno.Focus();
                        SendKeys.Send("{Enter}");
                    }
                }
            }
        }

        /// <summary>
        /// 加载挂号信息
        /// 2017-11-23 cc
        /// </summary>
        /// <param name="hisobj">挂号信息对象</param>
        public void SetHisInfo(HisPatientInfo hisobj)
        {
            string[] strInfo = null;
            string visit_time = "";
            string type = "";
            if (!string.IsNullOrEmpty(hisobj.SIGNAL_SOURCE_CODE))
            {
                string[] time = null;
                strInfo = hisobj.SIGNAL_SOURCE_CODE.Split('_');
                time = strInfo[0].Split('-');
                visit_time = "20" + time[2] + "-" + time[1].Replace("? ", "") + "-" + time[0];
                type = strInfo[1];
            }
            SIGNAL_SOURCE_CODE.Text = type;//挂号信息
        }

        /// <summary>
        /// 加载基本信息
        /// 2017-11-16 cc
        /// </summary>
        /// <param name="sender">控件</param>
        /// <param name="jibenobj">基本信息</param>
        public void SetInfo(object sender, tb_childbase jibenobj)
        {
            bool isinclude = true;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == jibenobj.healthcardno)
                {
                    dataGridView1.Rows[i].Selected = true;
                    isinclude = false;
                    break;
                }
            }
            if (isinclude)
            {
                //bindDataNowday(jibenobj.wm_mrn);//档案号查数据
                string[] rowmrn = new string[] { jibenobj.healthcardno, jibenobj.childname, jibenobj.id.ToString(), jibenobj.childgender, jibenobj.childbirthday };
                dataGridView1.Rows.Add(rowmrn);
                this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true; ;//选中查询到的数据行
            }
            dataGridView1_CellEnter(sender, null);
            telephone2.Focus();
        }

        private void diqu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }
        
        private void diqu_KeyUp(object sender, KeyEventArgs e)
        {
            
            //上下左右
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (diqulist.SelectedIndex > 0)
                    diqulist.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (diqulist.SelectedIndex < diqulist.Items.Count - 1)
                    diqulist.SelectedIndex++;
            }
            //回车
            else if (e.KeyCode == Keys.Enter)
            {
                //DicObj info = diqulist.SelectedItem as DicObj;
                //textBox1.Text = info.name;
                //diqulist.Visible = false;
            }
            else
            {
                diqulist.DataSource = null;

                string selpro = province.Text.Trim();

                if (selpro != "")
                {
                    IList<DicObj> dataSource = listarea.FindAll(t => (t.id.Length >= selpro.Length && t.id.Substring(0, selpro.Length).ToUpper().Equals(selpro.ToUpper())) || (t.name.Length > selpro.Length && t.name.Substring(0, selpro.Length).Equals(selpro.ToUpper())));
                    if (dataSource.Count > 0)
                    {
                        diqulist.DataSource = dataSource;
                        diqulist.DisplayMember = "name";
                        diqulist.ValueMember = "id";
                        diqulist.Visible = true;
                    }
                    else
                        diqulist.Visible = false;
                }
                else
                {
                    diqulist.Visible = false;
                }
            }
            //textBox1.Focus();
            province.Select(province.Text.Length, 1); //光标定位到文本框最后
        }

        private void diqu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && diqulist.Visible == true)
            {
                DicObj info = diqulist.SelectedItem as DicObj;
                province.Text = info.name;
                diqulist.Visible = false;
                //this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void fenzhen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && fenzhenlist.Visible == true )
            {
                DicObj info = fenzhenlist.SelectedItem as DicObj;
                ck_fz.Text = info.name;
                fenzhenlist.Visible = false;
                //this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void fenzhen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (fenzhenlist.SelectedIndex > 0)
                    fenzhenlist.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (fenzhenlist.SelectedIndex < fenzhenlist.Items.Count - 1)
                    fenzhenlist.SelectedIndex++;
            }
            //回车
            else if (e.KeyCode == Keys.Enter)
            {
                //DicObj info = diqulist.SelectedItem as DicObj;
                //textBox1.Text = info.name;
                //diqulist.Visible = false;
            }
            else
            {
                fenzhenlist.DataSource = null;

                string selpro = ck_fz.Text.Trim();

                if (selpro != "")
                {
                    IList<DicObj> dataSource = listfenzen.FindAll(t => (t.code.Length >= selpro.Length && t.code.Substring(0, selpro.Length).ToUpper().Equals(selpro.ToUpper())) || (t.name.Length > selpro.Length && t.name.Substring(0, selpro.Length).Equals(selpro.ToUpper())));
                    if (dataSource.Count > 0)
                    {
                        fenzhenlist.DataSource = dataSource;
                        fenzhenlist.DisplayMember = "name";
                        fenzhenlist.ValueMember = "code";
                        fenzhenlist.Visible = true;
                    }
                    else
                    {
                        fenzhenlist.Visible = false;
                    }
                }
                else
                {
                    fenzhenlist.Visible = false;
                }
            }
            //textBox1.Focus();
            ck_fz.Select(ck_fz.Text.Length, 1); //光标定位到文本框最后
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            Panel_searchHisInfo frmsearcher = new Panel_searchHisInfo(false);
            frmsearcher.ShowDialog();//打开查询界面
            if (frmsearcher.DialogResult == DialogResult.OK)//查询界面关闭
            {
                HisPatientInfo hisobj = frmsearcher.returnval;//查询界面返回obj
                if (hisobj != null)//如果obj不为空
                {
                    buttonX4.PerformClick();//清空界面
                    patient_id.Text = hisobj.PAT_INDEX_NO;//界面赋值查询的病人ID

                    tb_childbase jibenobj = basebll.GetByPatientId(hisobj.PAT_INDEX_NO);//通过病人ID查询本地数据库
                    if (jibenobj == null)//查询结果为null
                    {
                        //MessageBox.Show("（通过病人ID）本地无数据！");
                        jibenobj = basebll.GetByVisitNo(hisobj.VISIT_CARD_NO);//通过就诊卡号查询本地数据库
                        if (jibenobj == null)//查询结果为null
                        {
                            //MessageBox.Show("（通过就诊卡号）本地无数据！");
                            jiuzhencardno.Text = hisobj.VISIT_CARD_NO;
                            childname.Text = hisobj.PAT_NAME;
                            childgender.Text = hisobj.PHYSI_SEX_NAME;
                            patient_id.Text = hisobj.PAT_INDEX_NO;
                            telephone.Text = hisobj.PHONE_NO;
                            childbirthday.Text = hisobj.DATE_BIRTH;
                            SetHisInfo(hisobj);
                            province.Focus();
                        }
                        else
                        {
                            SetHisInfo(hisobj);
                            SetInfo(sender,jibenobj);//加载本地数据
                        }
                    }
                    else
                    {
                        SetHisInfo(hisobj);
                        SetInfo(sender,jibenobj);//加载本地数据
                    }
                }

            }
        }

        /// <summary>
        /// 保存基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            bool b = false;
            if (String.IsNullOrEmpty(childname.Text.Trim()))
            {
                MessageUtil.ShowTips("儿童姓名不能为空!");
                childname.Focus();
                return;
            }

            if (String.IsNullOrEmpty(childgender.Text.Trim()))
            {
                MessageUtil.ShowTips("儿童性别不能为空!");
                childgender.Focus();
                return;
            }
            if (!String.IsNullOrEmpty(telephone.Text.Trim()))
            {
                if (!validation.Checkmobilephone(telephone.Text.Trim()))
                {
                    MessageBox.Show("手机号输入有误，请重输!", "系统提示");
                    telephone.Focus();
                    return;
                }
            }

            tb_childbase obj = getChildBaseInfoObj();
            if (_jibenobj != null)
            {
                obj.id = _jibenobj.id;
                obj.healthcardno = _jibenobj.healthcardno;
                obj.gaoweiyinsu = _jibenobj.gaoweiyinsu;
                b = basebll.Update(obj);
                if (b)
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    _jibenobj = obj;
                    checkweight.Focus();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            else
            {
                //自动生成保健卡号
                obj.healthcardno = basebll.stateval().ToString();//保健卡号
                b = basebll.Add(obj);
                if (b)
                {
                    _jibenobj = obj;
                    this.healthcardno.Text = obj.healthcardno;
                    MessageBox.Show("保存成功！", "软件提示");
                    cd_id = _jibenobj.id;
                    checkweight.Focus();
                    dataGridView2.Rows.Clear();

                    string[] rowmrn = new string[] { _jibenobj.healthcardno, _jibenobj.childname, _jibenobj.id.ToString(), _jibenobj.childgender, _jibenobj.childbirthday };
                    dataGridView1.Rows.Add(rowmrn);
                    this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Selected = true;//选中查询到的数据行
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
        }
    }
}
