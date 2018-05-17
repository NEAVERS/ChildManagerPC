using System;
using System.Windows.Forms;
using YCF.Common;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL.ChildBaseInfo;
using System.Xml;
using YCF.Common;
using YCF.BLL;
using YCF.Model;
using YCF.Model.NotMaps;
using System.Data;
using System.Collections.Generic;

namespace ChildManager.UI.cepingshi
{
    public partial class xl_jibenxinxi_edit : Form
    {
        private tb_childbasebll jibenbll = new tb_childbasebll();//儿童建档基本信息业务处理类
        IDCardValidation validation = new IDCardValidation();
        XmlDocument doc = new XmlDocument();
        public List<DicObj> listarea = new List<DicObj>();
        HisPatientInfo _hisPatientInfo = null;
        InputLanguage InputHuoDong = null;

        public xl_jibenxinxi_edit( HisPatientInfo hisPatientInfo)
        {
            InitializeComponent();
            _hisPatientInfo = hisPatientInfo;
            CommonHelper.SetAllControls(panel1);
        }
        public xl_jibenxinxi_edit()
        {
            InitializeComponent();
            doc.Load("省市区.xml");
        }

        public void setCombValueAndText(ListBox cbx, string xmlname)
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
                dt.Rows.Add(dicobj.id, dicobj.name);
            }
            cbx.DataSource = dt;
            cbx.DisplayMember = "name";
            cbx.ValueMember = "id";
        }

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
        }

        private void diqu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void diqu_KeyUp(object sender, KeyEventArgs e)
        {
            if (globalInfoClass.Zhiwu == "护士")
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
            else
            {
                //上下左右
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
                {
                    if (diqulist_doc.SelectedIndex > 0)
                        diqulist_doc.SelectedIndex--;
                }
                else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
                {
                    if (diqulist_doc.SelectedIndex < diqulist_doc.Items.Count - 1)
                        diqulist_doc.SelectedIndex++;
                }
                else
                {
                    diqulist_doc.DataSource = null;

                    string selpro = province_doc.Text.Trim();

                    if (selpro != "")
                    {
                        IList<DicObj> dataSource = listarea.FindAll(t => (t.id.Length >= selpro.Length && t.id.Substring(0, selpro.Length).ToUpper().Equals(selpro.ToUpper())) || (t.name.Length > selpro.Length && t.name.Substring(0, selpro.Length).Equals(selpro.ToUpper())));
                        if (dataSource.Count > 0)
                        {
                            diqulist_doc.DataSource = dataSource;
                            diqulist_doc.DisplayMember = "name";
                            diqulist_doc.ValueMember = "id";
                            diqulist_doc.Visible = true;
                        }
                        else
                            diqulist_doc.Visible = false;
                    }
                    else
                    {
                        diqulist_doc.Visible = false;
                    }
                }
                province_doc.Select(province_doc.Text.Length, 1); //光标定位到文本框最后
            }
               
        }

        private void diqu_KeyDown(object sender, KeyEventArgs e)
        {
            if (globalInfoClass.Zhiwu == "护士")
            {
                if (e.KeyCode == Keys.Enter && diqulist.Visible == true)
                {
                    DicObj info = diqulist.SelectedItem as DicObj;
                    province.Text = info.name;
                    diqulist.Visible = false;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter && diqulist_doc.Visible == true)
                {
                    DicObj info = diqulist_doc.SelectedItem as DicObj;
                    province_doc.Text = info.name;
                    diqulist_doc.Visible = false;
                }
            }

        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            if (globalInfoClass.Zhiwu == "护士")
            {
                groupBox1.Visible = false;
                panel1.Height = 296;
                setCombValueAndText(diqulist, "area.xml");
                RefreshCode(_hisPatientInfo);
            }
            else
            {
                groupPanel1.Visible = false;
                groupBox1.Top = 8;
                panel1.Height = 296;
                setCombValueAndText(diqulist_doc, "area.xml");
                RefreshCode(_hisPatientInfo);
            }       
        }
        // 回车代替tab键盘
        private void all_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            bool b = false;
            if (globalInfoClass.Zhiwu == "护士")
            {
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
            }
            else
            {
                if (String.IsNullOrEmpty(childname_doc.Text.Trim()))
                {
                    MessageUtil.ShowTips("儿童姓名不能为空!");
                    childname_doc.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(childgender_doc.Text.Trim()))
                {
                    MessageUtil.ShowTips("儿童性别不能为空!");
                    childgender_doc.Focus();
                    return;
                }
                if (!String.IsNullOrEmpty(telephone_doc.Text.Trim()))
                {
                    if (!validation.Checkmobilephone(telephone_doc.Text.Trim()))
                    {
                        MessageBox.Show("手机号输入有误，请重输!", "系统提示");
                        telephone_doc.Focus();
                        return;
                    }
                }
            }

            TB_CHILDBASE obj = getChildBaseInfoObj();

            //自动生成保健卡号
            obj.HEALTHCARDNO = jibenbll.stateval().ToString();//保健卡号
            b = jibenbll.Add(obj);
            if (b)
            {
                this.healthcardno.Text = obj.HEALTHCARDNO;
                if (MessageBox.Show("保存成功！退出？", "软件提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }  
            }
            else
            {
                MessageBox.Show("保存失败！", "软件提示");
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private TB_CHILDBASE getChildBaseInfoObj()
        {
            TB_CHILDBASE obj = new TB_CHILDBASE();
            if (globalInfoClass.Zhiwu == "护士")
            {
                obj = CommonHelper.GetObj<TB_CHILDBASE>(groupPanel1.Controls);
                obj.STATUS = "1";
                obj.CHILDBUILDDAY = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                obj.CHILDBUILDHOSPITAL = "重医大附属儿童医院";
                return obj;
            }
            else
            {
                obj.PATIENT_ID = patient_id_doc.Text.Trim();
                obj.JIUZHENCARDNO = jiuzhencardno_doc.Text.Trim();
                obj.CHILDNAME = childname_doc.Text.Trim();
                obj.IDENTITYNO = identityno_doc.Text.Trim();
                obj.SFZLB = sfzlb_doc.Text.Trim();
                obj.CHILDGENDER = childgender_doc.Text.Trim();
                obj.CHILDBIRTHDAY = childbirthday_doc.Text;
                obj.PROVINCE = province_doc.Text.Trim();
                obj.JTZZ_CX = jtzz_cx_doc.Text.Trim();
                obj.TELEPHONE = telephone_doc.Text.Trim();
                obj.TELEPHONE2 = telephone2_doc.Text.Trim();

                return obj;
            }
        }

        /// <summary>
        /// HIS信息插入
        /// 2017-12-05 cc
        /// </summary>
        /// <param name="hisPatientInfo">His查询信息</param>
        public void RefreshCode(HisPatientInfo hisPatientInfo)
        {
            if (globalInfoClass.Zhiwu == "护士")
            {
                if (hisPatientInfo != null)
                {

                    healthcardno.Text = "";//保健卡号
                    patient_id.Text = hisPatientInfo.PAT_INDEX_NO;//病人ID
                    jiuzhencardno.Text = hisPatientInfo.VISIT_CARD_NO;//就诊卡号
                    this.childname.Text = hisPatientInfo.PAT_NAME;//姓名
                    childgender.Text = hisPatientInfo.PHYSI_SEX_NAME;//性别
                    childbirthday.Value=Convert.ToDateTime(hisPatientInfo.DATE_BIRTH);//出生日期
                    telephone.Text=hisPatientInfo.PHONE_NO;//电话号码
                }
                else
                {
                    healthcardno.Text = "";//保健卡号
                    childname.Text = "";//姓名
                    childgender.Text = "";//性别
                    childbirthday.Value = DateTime.Now;
                    fatherheight.Text = "";
                    fathereducation.Text = "本科";
                    motherheight.Text = "";
                    mothereducation.Text = "本科";
                    telephone.Text = "";
                    cs_fetus.Text = "";
                    cs_produce.Text = "";
                    cs_week.Text = "";
                    cs_day.Text = "";
                    modedelivery.Text = "";
                    birthweight.Text = "";
                    birthheight.Text = "";
                }
            }
            else
            {
                if (hisPatientInfo != null)
                {

                    healthcardno.Text = "";//保健卡号
                    patient_id_doc.Text = hisPatientInfo.PAT_INDEX_NO;//病人ID
                    jiuzhencardno_doc.Text = hisPatientInfo.VISIT_CARD_NO;//就诊卡号

                    this.childname_doc.Text = hisPatientInfo.PAT_NAME;//姓名
                    childgender_doc.Text = hisPatientInfo.PHYSI_SEX_NAME;//性别
                    childbirthday_doc.Value = Convert.ToDateTime(hisPatientInfo.DATE_BIRTH);//出生日期
                    telephone_doc.Text = hisPatientInfo.PHONE_NO;//电话号码
                }
                else
                {
                    healthcardno_doc.Text = "";//保健卡号
                    childname_doc.Text = "";//姓名
                    childgender_doc.Text = "";//性别
                    childbirthday_doc.Value = DateTime.Now;
                    telephone_doc.Text = "";
                }
            }
        }

        private void childbirthday_ValueChanged(object sender, EventArgs e)
        {
            label_age.Text = CommonHelper.getAgeStr(childbirthday.Value.ToString("yyyy-MM-dd"));
        }

        private void childbirthday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{RIGHT}");
            }
        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            if (this.ActiveControl is DateTimePicker && m.Msg == 0x100 && (int)m.WParam == 13)
            {
                m.WParam = (IntPtr)0x27;
            }
            return base.ProcessKeyPreview(ref m);
        }

        private void childbirthday_doc_ValueChanged(object sender, EventArgs e)
        {
            label_age_doc.Text = CommonHelper.getAgeStr(childbirthday_doc.Value.ToString("yyyy-MM-dd"));
        }

        private void childbirthday_doc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{RIGHT}");
            }
        }

        private void province_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputHuoDong;
        }
    }
}
