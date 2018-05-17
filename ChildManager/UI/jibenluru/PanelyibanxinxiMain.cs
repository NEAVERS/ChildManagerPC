using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using YCF.Common;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.Model;
using ChildManager.BLL;
using ChildManager.BLL.childSetInfo;
using System.Collections;
using ChildManager.Model.childSetInfo;
using login.UI;
using System.Xml;
using YCF.Model;

namespace ChildManager.UI.jibenluru
{
    public partial class PanelyibanxinxiMain : UserControl
    {
        private ChildBaseInfoBll childBaseInfobll = new ChildBaseInfoBll();//儿童建档基本信息业务处理类
        public ChildBaseInfoObj obj = new ChildBaseInfoObj();//儿童基本信息类
        WhoChildStandardDayObj bmistandbmiobj = null;
        WhoChildStandardDayObj tizhongstandobj = null;
        WhoChildStandardDayObj shengaostandobj = null;
        WhoChildStandardDayObj touweistandobj = null;

        Panel_gaowei_zhuanan gaoweizhuanan = null;
        Panel_goulou_zhuanan goulouzhuanan = null;
        Panel_pinxue_zhuanan pinxuezhuanan = null;
        Panel_yingyang_zhuanan yingyangzhuanan = null;

        IDCardValidation validation = new IDCardValidation();

        private ChildCheckObj _checkobj = new ChildCheckObj();

        ChildCheckObj checkmobanobj = new ChildCheckObj();
        public int _childid = -1;
        private ChildCheckBll checkbll = new ChildCheckBll();
        private childSetCheckAgeBll ageBll = new childSetCheckAgeBll();
        XmlDocument doc = new XmlDocument();
        public PanelyibanxinxiMain()
        {
            InitializeComponent();
            doc.Load("省市区.xml");
            XmlNode provinces = doc.SelectSingleNode("/ProvinceCity");
            foreach (XmlNode province in provinces.ChildNodes)
            {
                comboBox31.Items.Add(province.Name);
            }
            comboBox31.SelectedIndex = 0;
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            RefreshCode();
            //dateTimeInput1.Value = Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd"));
            //dateTimeInput2.Value = DateTime.Now;
            //dateTimeInput3.Value = Convert.ToDateTime(DateTime.Today.ToString("yyyy-MM-dd"));

            //textBoxX12.Text = globalInfoClass.UserName;

            textBoxX1.Focus();
        }
        // 回车代替tab键盘
        private void all_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }

        }
        /// <summary>
        /// 下拉框自动弹开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_Enter(object sender, EventArgs e)
        {
            ComboBox bobox = (ComboBox)sender;
            if (bobox.Name == "comboBox1")
            {
                this.comboBox1.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox2")
            {
                this.comboBox2.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox6")
            {
                this.comboBox6.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox7")
            {
                this.comboBox7.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox8")
            {
                this.comboBox8.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox9")
            {
                this.comboBox9.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox3")
            {
                this.comboBox3.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox10")
            {
                this.comboBox10.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox11")
            {
                this.comboBox11.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox14")
            {
                this.comboBox14.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox15")
            {
                this.comboBox15.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox12")
            {
                this.comboBox12.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox13")
            {
                this.comboBox13.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox16")
            {
                this.comboBox16.DroppedDown = true;
            }
            else if (bobox.Name == "comboBox17")
            {
                this.comboBox17.DroppedDown = true;
            }
            else
            {
                return;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                bool b = false;
                if (String.IsNullOrEmpty(textBoxX1.Text.Trim()))
                {
                    MessageUtil.ShowTips("儿童姓名不能为空!");
                    textBoxX1.Focus();
                    return;
                }

                if (String.IsNullOrEmpty(comboBox1.Text.Trim()))
                {
                    MessageUtil.ShowTips("儿童性别不能为空!");
                    comboBox1.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(comboBox3.Text.Trim()))
                {
                    MessageUtil.ShowTips("建册医院不能为空!");
                    comboBox3.Focus();
                    return;
                }
                if (!String.IsNullOrEmpty(textBoxX9.Text.Trim()))
                {
                    if (!validation.Checkmobilephone(textBoxX9.Text.Trim()))
                    {
                        MessageBox.Show("手机号输入有误，请重输!", "系统提示");
                        textBoxX9.Focus();
                        return;
                    }
                }

                //查询是否存在记录
                //if (new ChildBaseInfoBll().isExist(this.textBoxX1.Text.Trim(), this.textBoxX28.Text))
                //{
                //    MessageUtil.ShowTips("该小孩存在建档记录！");
                //    textBoxX1.Focus();
                //    return;
                //}


                obj = getChildBaseInfoObj();
                if (globalInfoClass.Wm_Index != -1)
                {
                    b = childBaseInfobll.updateChildBaseInfo(obj);
                    if (b)
                    {
                        MessageBox.Show("保存成功！", "软件提示");
                        new TbGaoweiBll().saveOrdeleteGaowei(textBoxX12.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        MessageBox.Show("保存失败！", "软件提示");
                    }
                }
                else
                {
                    b = childBaseInfobll.insertChildBaseInfo(obj);
                    if (b)
                    {
                        globalInfoClass.Wm_Index = childBaseInfobll.getchild_id("select id from tb_childBase where healthCardNo='" + obj.HealthCardNo + "'");
                        obj.Id = globalInfoClass.Wm_Index;
                        this.textBoxX2.Text = obj.HealthCardNo;
                        MessageBox.Show("保存成功！", "软件提示");
                        if (!String.IsNullOrEmpty(textBoxX12.Text.Trim()))
                        {
                            new TbGaoweiBll().saveOrdeleteGaowei(textBoxX12.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));
                        }
                    }
                    else
                    {
                        MessageBox.Show("保存失败！", "软件提示");
                    }
                }
                
                //MessageUtil.ShowTips("保存成功");
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                saveChildCheck();
            }

            else if (tabControl1.SelectedTab == tabPage4)
            {
                gaoweizhuanan.savegaoweizhuanan();
            }

            else if (tabControl1.SelectedTab == tabPage5)
            {
                if (yingyangzhuanan != null)
                {
                    yingyangzhuanan.saveyingyangzhuanan();
                }
            }
            else if (tabControl1.SelectedTab == tabPage6)
            {
                if (pinxuezhuanan != null)
                {
                    pinxuezhuanan.savepinxuezhuanan();
                }
            }
            else if (tabControl1.SelectedTab == tabPage7)
            {
                if (goulouzhuanan != null)
                {
                    goulouzhuanan.savegoulouzhuanan();
                }
            }
        }

        /// <summary>
        /// 保存儿童体检项目信息
        /// </summary>
        public void saveChildCheck()
        {
            if (globalInfoClass.Wm_Index == -1)
            {
                MessageBox.Show("选择具体儿童后再保存!！", "软件提示");
                return;
            }
            if (String.IsNullOrEmpty(txt_checkHeight.Text.Trim()))
            {
                MessageBox.Show("请填写儿童身高后再保存", "系统提示");
                txt_checkHeight.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txt_checkWeight.Text.Trim()))
            {
                MessageBox.Show("请选择儿童体重后再保存", "系统提示");
                txt_checkWeight.Focus();
                return;
            }

            if (_checkobj == null)
            {
                string insertSql = this.getcheckinsertSql();
                if (checkbll.saverecord(insertSql))
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    new TbGaoweiBll().saveOrdeleteGaowei(textBoxX15.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));
                    Refreshcheckdatagridview();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            else
            {
                string updateSql = this.getcheckupdateSql();
                if (checkbll.updaterecord(updateSql) > 0)
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    new TbGaoweiBll().saveOrdeleteGaowei(textBoxX15.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));
                    Refreshcheckdatagridview();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private ChildBaseInfoObj getChildBaseInfoObj()
        {
            ChildBaseInfoObj obj1 = new ChildBaseInfoObj();
            obj1.Id = globalInfoClass.Wm_Index;
            //自动生成保健卡号
            obj1.HealthCardNo = childBaseInfobll.stateval("select max(healthCardNo)  from  tb_childBase").ToString();//保健卡号
            obj1.jiuzhenCardNo = textBoxX20.Text.Trim();//就诊卡号
            obj1.ChildName = textBoxX1.Text.Trim();//姓名
            obj1.ChildGender = comboBox1.Text.Trim();//性别
            obj1.BloodType = comboBox2.Text.Trim();//血型
            obj1.ChildBirthDay = dateTimeInput1.Value.ToString("yyyy-MM-dd");
            obj1.ChildBuildDay = dateTimeInput2.Value.ToString("yyyy-MM-dd HH:mm:ss");
            obj1.ChildBuildHospital = comboBox3.Text.Trim();//建册医院
            //存图片
            //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
            //父亲母亲
            obj1.FatherName = textBoxX4.Text.Trim();
            obj1.FatherAge = textBoxX3.Text.Trim();
            obj1.FatherHeight = textBoxX5.Text.Trim();
            obj1.FatherEducation = comboBox8.Text.Trim();
            obj1.FatherJob = comboBox7.Text.Trim();
            obj1.MotherName = textBoxX8.Text.Trim();
            obj1.MotherAge = textBoxX7.Text.Trim();
            obj1.MotherHeight = textBoxX6.Text.Trim();
            obj1.MotherEducation = comboBox9.Text.Trim();
            obj1.MotherJob = comboBox6.Text.Trim();
            obj1.Telephone = textBoxX9.Text.Trim();
            //telephone2
            obj1.Telephone2 = this.txt_Telephone2.Text.Trim();
            obj1.province = comboBox31.Text.Trim();
            obj1.city = comboBox29.Text.Trim();
            obj1.area = comboBox28.Text.Trim();
            obj1.address = textBoxX10.Text.Trim();
            obj1.NurseryInstitution = comboBox10.Text.Trim();

            //发证
            //obj1.ImmuneUnit = cbx_immuneUnit.Text.Trim();
            //obj1.ImmuneDay = dateTimeInput3.Value.ToString("yyyy-MM-dd HH:mm:ss");
            //obj1.Community = comboBox5.Text.Trim();
            //obj1.CensusRegister = comboBox4.Text.Trim();
            //出生史
            obj1.Cs_fetus = textBoxX18.Text.Trim();
            obj1.Cs_produce = textBoxX19.Text.Trim();
            obj1.Cs_week = textBoxX17.Text.Trim();
            obj1.Cs_day = textBoxX16.Text.Trim();
            obj1.ModeDelivery = comboBox14.Text.Trim();
            obj1.PerineumIncision = comboBox15.Text.Trim();
            //obj1.FetusNumber = cbx_fetusNumber.Text.Trim();
            obj1.NeonatalCondition = comboBox11.Text.Trim();
            obj1.BirthWeight = textBoxX14.Text.Trim();
            obj1.BirthHeight = textBoxX13.Text.Trim();
            obj1.Birthaddress = textBoxX11.Text.Trim();
            //母乳喂养
            obj1.HospitalizedStates = comboBox17.Text.Trim();
            obj1.OneMonth = comboBox12.Text.Trim();
            obj1.InFourMonth = comboBox13.Text.Trim();
            obj1.FourToSixMonth = comboBox16.Text.Trim();
            //obj1.Yunqi = getcheckedValue(pnl_yunqi);
            obj1.Identityno = textBoxX28.Text.Trim();
            obj1.jiuzhenCardNo = textBoxX20.Text.Trim();
            obj1.status = "1";
            obj1.linshi = getcheckedValue(panel4);
            //obj1.gerenshi = gerenshi.Text.Trim();
            //obj1.jiwangshi = jiwangshi.Text.Trim();
            //obj1.jiazushi = jiazushi.Text.Trim();
            return obj1;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode()
        {
            int id = globalInfoClass.Wm_Index;
            if (id != -1)
            {
                Cursor.Current = Cursors.WaitCursor;
                string sqls = "select * from  tb_childBase where id=" + id + "";
                obj = childBaseInfobll.getchildBaseobj(sqls);

                if (obj != null)
                {
                    textBoxX2.Text = obj.HealthCardNo;//保健卡号
                    textBoxX20.Text = obj.jiuzhenCardNo;//就诊卡号
                    textBoxX1.Text = obj.ChildName;//姓名
                    comboBox1.Text = obj.ChildGender;//性别
                    comboBox2.Text = obj.BloodType;//血型
                    dateTimeInput1.Value = Convert.ToDateTime(obj.ChildBirthDay);
                    dateTimeInput2.Value = Convert.ToDateTime(obj.ChildBuildDay);
                    comboBox3.Text = obj.ChildBuildHospital;//建册医院
                    //存图片
                    //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
                    //父亲母亲
                    textBoxX4.Text = obj.FatherName;
                    textBoxX3.Text = obj.FatherAge;
                    textBoxX5.Text = obj.FatherHeight;
                    comboBox8.Text = obj.FatherEducation;
                    comboBox7.Text = obj.FatherJob;
                    textBoxX8.Text = obj.MotherName;
                    textBoxX7.Text = obj.MotherAge;
                    textBoxX6.Text = obj.MotherHeight;
                    comboBox9.Text = obj.MotherEducation;
                    comboBox6.Text = obj.MotherJob;
                    textBoxX9.Text = obj.Telephone;
                    txt_Telephone2.Text = obj.Telephone2;
                    textBoxX10.Text = obj.address;
                    comboBox31.Text = obj.province;
                    comboBox29.Text = obj.city;
                    comboBox28.Text = obj.area;
                    comboBox10.Text = obj.NurseryInstitution;

                    //发证
                    //obj.ImmuneUnit = cbx_immuneUnit.Text.Trim();
                    //obj.ImmuneDay = dateTimeInput3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    //obj.Community = comboBox5.Text.Trim();
                    //obj.CensusRegister = comboBox4.Text.Trim();
                    //出生史
                    textBoxX18.Text = obj.Cs_fetus;
                    textBoxX19.Text = obj.Cs_produce;
                    textBoxX17.Text = obj.Cs_week;
                    textBoxX16.Text = obj.Cs_day;
                    comboBox14.Text = obj.ModeDelivery;
                    comboBox15.Text = obj.PerineumIncision;
                    //obj.FetusNumber = cbx_fetusNumber.Text = ;
                    comboBox11.Text = obj.NeonatalCondition;
                    textBoxX14.Text = obj.BirthWeight;
                    textBoxX13.Text = obj.BirthHeight;
                    textBoxX11.Text = obj.Birthaddress;
                    //母乳喂养
                    comboBox17.Text = obj.HospitalizedStates;
                    comboBox12.Text = obj.OneMonth;
                    comboBox13.Text = obj.InFourMonth;
                    comboBox16.Text = obj.FourToSixMonth;
                    //obj.Yunqi = getcheckedValue(pnl_yunqi);
                    textBoxX28.Text = obj.Identityno;
                    //gerenshi.Text = obj.gerenshi;
                    //jiwangshi.Text = obj.jiwangshi;
                    //jiazushi.Text = obj.jiazushi;
                    comboBox31.Text = obj.province;
                    comboBox29.Text = obj.city;
                    comboBox28.Text = obj.area;
                    textBoxX12.Text = new TbGaoweiBll().getGaoweistr(id, "","");
                    setcheckedValue(panel4,obj.linshi);
                }
            }
            else
            {
                textBoxX2.Text = "";//保健卡号
                textBoxX20.Text = "";//就诊卡号
                textBoxX1.Text = "";//姓名
                comboBox1.Text = "";//性别
                comboBox2.Text = "";//血型
                dateTimeInput1.Value = DateTime.Now;
                dateTimeInput2.Value = DateTime.Now;
                comboBox3.Text = CommonHelper.GetHospitalName();// "重医大附属儿童医院";//建册医院
                //存图片
                //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
                //父亲母亲
                textBoxX4.Text = "";
                textBoxX3.Text = "";
                textBoxX5.Text = "";
                comboBox8.Text = "";
                comboBox7.Text = "";
                textBoxX8.Text = "";
                textBoxX7.Text = "";
                textBoxX6.Text = "";
                comboBox9.Text = "";
                comboBox6.Text = "";
                textBoxX9.Text = "";
                textBoxX10.Text = "";
                comboBox10.Text = "";

                //发证
                //obj.ImmuneUnit = cbx_immuneUnit.Text.Trim();
                //obj.ImmuneDay = dateTimeInput3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //obj.Community = comboBox5.Text.Trim();
                //obj.CensusRegister = comboBox4.Text.Trim();
                //出生史
                textBoxX18.Text = "";
                textBoxX19.Text = "";
                textBoxX17.Text = "";
                textBoxX16.Text = "";
                comboBox14.Text = "";
                comboBox15.Text = "";
                //obj.FetusNumber = cbx_fetusNumber.Text = ;
                comboBox11.Text = "";
                textBoxX14.Text = "";
                textBoxX13.Text = "";
                textBoxX11.Text = "";
                //母乳喂养
                comboBox17.Text = "纯母乳";
                comboBox12.Text = "纯母乳";
                comboBox13.Text = "纯母乳";
                comboBox16.Text = "纯母乳";
                //obj.Yunqi = getcheckedValue(pnl_yunqi);
                textBoxX28.Text = "";
                textBoxX12.Text = "";
                //gerenshi.Text = "";
                //jiwangshi.Text = "";
                //jiazushi.Text = "";
                if (obj != null)
                {
                    textBoxX20.Text = obj.jiuzhenCardNo;//就诊卡号
                    textBoxX1.Text = obj.ChildName;//姓名
                    comboBox1.Text = obj.ChildGender;//性别
                    this.dateTimeInput1.Value =String.IsNullOrEmpty(obj.childBirthDay)?DateTime.Now: Convert.ToDateTime(obj.childBirthDay);
                }
            }
        }

        /// <summary>
        /// 本院信息导入
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode1()
        {
            
                if (obj != null)
                {
                    textBoxX2.Text = "";//保健卡号
                    string childname = obj.MotherName;
                    if (obj.ChildGender == "女")
                    {
                        childname += "之女";
                    }
                    else
                    {
                        childname += "之子";
                    }

                    textBoxX1.Text = childname;//姓名
                    comboBox1.Text = obj.ChildGender;//性别
                    comboBox2.Text = obj.BloodType;//血型
                    DateTime dtbirth = DateTime.Now;
                    DateTime.TryParse(obj.ChildBirthDay, out dtbirth);
                    dateTimeInput1.Value = dtbirth;
                    dateTimeInput2.Value = DateTime.Now;
                    comboBox3.Text = "重医大附属儿童医院";//建册医院
                    //存图片
                    //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
                    //父亲母亲
                    textBoxX4.Text = obj.FatherName;
                    textBoxX3.Text = obj.FatherAge;
                    textBoxX5.Text = obj.FatherHeight;
                    comboBox8.Text = obj.FatherEducation;
                    comboBox7.Text = obj.FatherJob;
                    textBoxX8.Text = obj.MotherName;
                    textBoxX7.Text = obj.MotherAge;
                    textBoxX6.Text = obj.MotherHeight;
                    comboBox9.Text = obj.MotherEducation;
                    comboBox6.Text = obj.MotherJob;
                    textBoxX9.Text = obj.Telephone;
                comboBox31.Text = obj.province;
                comboBox29.Text = obj.city;
                comboBox28.Text = obj.area;
                textBoxX10.Text = obj.address;
                    comboBox10.Text = obj.NurseryInstitution;

                    //发证
                    //obj.ImmuneUnit = cbx_immuneUnit.Text.Trim();
                    //obj.ImmuneDay = dateTimeInput3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    //obj.Community = comboBox5.Text.Trim();
                    //obj.CensusRegister = comboBox4.Text.Trim();
                    //出生史
                    textBoxX18.Text = obj.Cs_fetus;
                    textBoxX19.Text = obj.Cs_produce;
                    textBoxX17.Text = obj.Cs_week;
                    textBoxX16.Text = obj.Cs_day;
                    comboBox14.Text = obj.ModeDelivery;
                    comboBox15.Text = obj.PerineumIncision;
                    //obj.FetusNumber = cbx_fetusNumber.Text = ;
                    comboBox11.Text = obj.NeonatalCondition;
                    textBoxX14.Text = obj.BirthWeight;
                    textBoxX13.Text = obj.BirthHeight;
                    textBoxX11.Text = obj.Birthaddress;
                    //母乳喂养
                    comboBox17.Text = "纯母乳";
                    comboBox12.Text = "纯母乳";
                    comboBox13.Text = "纯母乳";
                    comboBox16.Text = "纯母乳";
                    //obj.Yunqi = getcheckedValue(pnl_yunqi);
                    textBoxX28.Text = obj.Identityno;
            }
            else
            {
                textBoxX2.Text = "";//保健卡号
                textBoxX1.Text = "";//姓名
                comboBox1.Text = "";//性别
                comboBox2.Text = "";//血型
                dateTimeInput1.Value = DateTime.Now;
                dateTimeInput2.Value = DateTime.Now;
                comboBox3.Text = "重医大附属儿童医院";//建册医院
                //存图片
                //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
                //父亲母亲
                textBoxX4.Text = "";
                textBoxX3.Text = "";
                textBoxX5.Text = "";
                comboBox8.Text = "";
                comboBox7.Text = "";
                textBoxX8.Text = "";
                textBoxX7.Text = "";
                textBoxX6.Text = "";
                comboBox9.Text = "";
                comboBox6.Text = "";
                textBoxX9.Text = "";
                textBoxX10.Text = "";
                comboBox10.Text = "";

                //发证
                //obj.ImmuneUnit = cbx_immuneUnit.Text.Trim();
                //obj.ImmuneDay = dateTimeInput3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //obj.Community = comboBox5.Text.Trim();
                //obj.CensusRegister = comboBox4.Text.Trim();
                //出生史
                textBoxX18.Text = "";
                textBoxX19.Text = "";
                textBoxX17.Text = "";
                textBoxX16.Text = "";
                comboBox14.Text = "";
                comboBox15.Text = "";
                //obj.FetusNumber = cbx_fetusNumber.Text = ;
                comboBox11.Text = "";
                textBoxX14.Text = "";
                textBoxX13.Text = "";
                textBoxX11.Text = "";
                //母乳喂养
                comboBox17.Text = "纯母乳";
                comboBox12.Text = "纯母乳";
                comboBox13.Text = "纯母乳";
                comboBox16.Text = "纯母乳";
                //obj.Yunqi = getcheckedValue(pnl_yunqi);
                textBoxX28.Text = "";
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            ////设定tabpages1为当前项
            ////tabControl1_Click(tabPage1, new EventArgs());
            //tabControl1.SelectedTab = tabPage1;
            ////打开查询窗体
            //Paneltsb_searchInfo search = new Paneltsb_searchInfo(this);
            //search.ShowDialog();
        }


        /// <summary>
        /// 体检保存
        /// </summary>
        /// <returns></returns>
        private string getcheckinsertSql()
        {
            StringBuilder builder = new StringBuilder();
            ChildCheckObj obj = new ChildCheckObj();
            obj.ChildId = globalInfoClass.Wm_Index;
            //obj.CheckAge = this.txt_checkAge.Text.Trim();
            //月份
            //obj.CheckMonth = dgvcheckMonthList.CurrentCell.Value.ToString();
            obj.CheckFactAge = this.txt_checkFactAge.Text.Trim();
            obj.CheckDay = this.dtp_checkDay.Value.ToString("yyyy-MM-dd");
            obj.BloodseSu = this.txt_bloodseSu.Text.Trim();
            obj.DoctorName = cbx_doctorName.Text.Trim();

            obj.YufangJiezhong = getcheckedValue(pnl_yufangJiezhong);
            //
            obj.CheckHeight = this.txt_checkHeight.Text.Trim();
            obj.CheckWeight = this.txt_checkWeight.Text.Trim();

            obj.CheckTouwei = this.txt_checkTouwei.Text.Trim();
            obj.CheckZuogao = this.txt_checkZuogao.Text.Trim();

            obj.zhushi = this.textBoxX25.Text.Trim();
            obj.fushi = this.textBoxX26.Text.Trim();

            obj.Vitd = this.cbx_vitd.Text.Trim();
            obj.OtherBingshi = this.txt_otherBingshi.Text.Trim();
            obj.shehui = this.comboBox5.Text.Trim();
            obj.WuGuan = this.cbx_wuGuan.Text.Trim();
            obj.Checkbi = this.cbx_checkbi.Text.Trim();
            obj.dongzuo = this.comboBox20.Text.Trim();
            obj.Skin = this.cbx_skin.Text.Trim();
            obj.JiZhu = this.cbx_jiZhu.Text.Trim();
            obj.Laguage = this.txt_laguage.Text.Trim();
            obj.Lingbaji = this.cbx_lingbajie.Text.Trim();
            obj.BigSport = this.cbx_bigSport.Text.Trim();
            obj.XiongBu = this.cbx_xiongBu.Text.Trim();
            obj.SiZhi = this.cbx_siZhi.Text.Trim();
            obj.CheckQianlu = this.txt_checkQianlu.Text.Trim();
            obj.XinZang = this.cbx_XinZang.Text.Trim();
            obj.toulu = this.comboBox30.Text.Trim();
            obj.FeiBu = this.cbx_FeiBu.Text.Trim();
            obj.KouQiang = this.cbx_kouQiang.Text.Trim();
            obj.YaciNumber = this.txt_yaciNumber.Text.Trim();
            obj.YuciNumber = this.txt_yuciNumber.Text.Trim();
            obj.Fubu = this.txt_fubu.Text.Trim();
            obj.GanZang = this.cbx_ganZang.Text.Trim();
            obj.MiNiaoQi = this.cbx_miNiaoQi.Text.Trim();

            obj.gouloubing = this.comboBox36.Text.Trim();

            obj.CheckContent = getcheckedValue(pnl_checkContent);
            //复诊日期
            obj.FuzenDay = this.dtp_fuzen.Value.ToString("yyyy-MM-dd");
            obj.checkdiagnose = this.txt_checkdiagonse.Text.Trim();

            obj.nuerzhidao = this.txtyingyangzhidao.Text.Trim();
            obj.zonghefazhan = this.txtzaoqifazhan.Text.Trim();

            obj.fuzhujiancha = getcheckedValue(panel3);
            obj.otherjiancha = getcheckedValue(panel5);

            obj.zhusu = this.textBox5.Text.Trim();
            obj.bingshi = this.textBox6.Text.Trim();
            obj.tijian = this.textBox7.Text.Trim();
            obj.zhenduan = this.textBox8.Text.Trim();
            obj.chuli = this.textBox9.Text.Trim();

            builder.Append("insert into tb_childcheck (");
            builder.Append("childId");
            builder.Append(",checkAge");
            builder.Append(",checkFactAge");
            builder.Append(",checkDay");
            builder.Append(",doctorName");
            builder.Append(",checkHeight");
            builder.Append(",checkWeight");
            builder.Append(",checkTouwei");
            builder.Append(",Fuwei");
            builder.Append(",checkZuogao");
            builder.Append(",checkTunwei");
            builder.Append(",checkQianlu");
            builder.Append(",checkIQ");
            builder.Append(",checkZiping");
            //builder.Append(",yuCeHeight");
            builder.Append(",leftyan");
            builder.Append(",rightyan");
            builder.Append(",leftyanshili");
            builder.Append(",rightyanshili");
            builder.Append(",xieshi");
            builder.Append(",lefter");
            builder.Append(",righter");
            builder.Append(",lefterlisten");
            builder.Append(",rightlisten");
            builder.Append(",checkbi");
            builder.Append(",kouQiang");
            builder.Append(",yaciNumber");
            builder.Append(",yuciNumber");
            builder.Append(",skin");
            builder.Append(",lingbajie");
            builder.Append(",bianTaoti");
            builder.Append(",xinZang");
            builder.Append(",feiBu");
            builder.Append(",ganZang");
            builder.Append(",piZang");
            builder.Append(",qiZhi");
            builder.Append(",siZhi");
            builder.Append(",xiongBu");
            builder.Append(",miNiaoQi");
            builder.Append(",wuGuan");
            builder.Append(",jiZhu");
            builder.Append(",pingPqiu");
            builder.Append(",checkContent");
            builder.Append(",yufangJiezhong");
            builder.Append(",bloodseSu");
            builder.Append(",shiWugouCheng");
            builder.Append(",vitd");
            builder.Append(",bigSport");
            builder.Append(",spirtSport");
            builder.Append(",laguage");
            builder.Append(",signSocial");
            builder.Append(",otherBingshi");
            builder.Append(",handle");
            builder.Append(",diagnose");
            builder.Append(",CheckMonth");
            builder.Append(",fuzenDay");
            builder.Append(",fubu");
            builder.Append(",nuerzhidao");
            builder.Append(",checkdiagnose");
            builder.Append(",zhushi");
            builder.Append(",fushi");
            builder.Append(",shehui");
            builder.Append(",dongzuo");
            builder.Append(",toulu");
            builder.Append(",gouloubing");
            builder.Append(",zonghefazhan");
            builder.Append(",fuzhujiancha");
            builder.Append(",otherjiancha");
            builder.Append(",zhusu");
            builder.Append(",bingshi");
            builder.Append(",tijian");
            builder.Append(",zhenduan");
            builder.Append(",chuli");
            builder.Append(" ) values ( ");

            builder.Append("'" + obj.ChildId + "'");
            builder.Append(",'" + obj.CheckAge + "'");
            builder.Append(",'" + obj.CheckFactAge + "'");
            builder.Append(",'" + obj.CheckDay + "'");
            builder.Append(",'" + obj.DoctorName + "'");
            builder.Append(",'" + obj.CheckHeight + "'");
            builder.Append(",'" + obj.CheckWeight + "'");
            builder.Append(",'" + obj.CheckTouwei + "'");
            builder.Append(",'" + obj.Fuwei + "'");
            builder.Append(",'" + obj.CheckZuogao + "'");
            builder.Append(",'" + obj.CheckTunwei + "'");
            builder.Append(",'" + obj.CheckQianlu + "'");
            builder.Append(",'" + obj.CheckIQ + "'");
            builder.Append(",'" + obj.CheckZiping + "'");
            //builder.Append(",'"+obj.YuCeHeight+"'");
            builder.Append(",'" + obj.Leftyan + "'");
            builder.Append(",'" + obj.Rightyan + "'");
            builder.Append(",'" + obj.Leftyanshili + "'");
            builder.Append(",'" + obj.Rightyanshili + "'");
            builder.Append(",'" + obj.Xieshi + "'");
            builder.Append(",'" + obj.Lefter + "'");
            builder.Append(",'" + obj.Righter + "'");
            builder.Append(",'" + obj.Lefterlisten + "'");
            builder.Append(",'" + obj.Rightlisten + "'");
            builder.Append(",'" + obj.Checkbi + "'");
            builder.Append(",'" + obj.KouQiang + "'");
            builder.Append(",'" + obj.YaciNumber + "'");
            builder.Append(",'" + obj.YuciNumber + "'");
            builder.Append(",'" + obj.Skin + "'");
            builder.Append(",'" + obj.Lingbaji + "'");
            builder.Append(",'" + obj.BianTaoti + "'");
            builder.Append(",'" + obj.XinZang + "'");
            builder.Append(",'" + obj.FeiBu + "'");
            builder.Append(",'" + obj.GanZang + "'");
            builder.Append(",'" + obj.PiZang + "'");
            builder.Append(",'" + obj.QiZhi + "'");
            builder.Append(",'" + obj.SiZhi + "'");
            builder.Append(",'" + obj.XiongBu + "'");
            builder.Append(",'" + obj.MiNiaoQi + "'");
            builder.Append(",'" + obj.WuGuan + "'");
            builder.Append(",'" + obj.JiZhu + "'");
            builder.Append(",'" + obj.PingPqiu + "'");
            builder.Append(",'" + obj.CheckContent + "'");
            builder.Append(",'" + obj.YufangJiezhong + "'");
            builder.Append(",'" + obj.BloodseSu + "'");
            builder.Append(",'" + obj.ShiWugouCheng + "'");
            builder.Append(",'" + obj.Vitd + "'");
            builder.Append(",'" + obj.BigSport + "'");
            builder.Append(",'" + obj.SpirtSport + "'");
            builder.Append(",'" + obj.Laguage + "'");
            builder.Append(",'" + obj.SignSocial + "'");
            builder.Append(",'" + obj.OtherBingshi + "'");
            builder.Append(",'" + obj.Handle + "'");
            builder.Append(",'" + obj.Diagnose + "'");
            builder.Append(",'" + obj.CheckMonth + "'");
            builder.Append(",'" + obj.FuzenDay + "'");
            builder.Append(",'" + obj.Fubu + "'");
            builder.Append(",'" + obj.nuerzhidao + "'");
            builder.Append(",'" + obj.checkdiagnose + "'");
            builder.Append(",'" + obj.zhushi + "'");
            builder.Append(",'" + obj.fushi + "'");
            builder.Append(",'" + obj.shehui + "'");
            builder.Append(",'" + obj.dongzuo + "'");
            builder.Append(",'" + obj.toulu + "'");
            builder.Append(",'" + obj.gouloubing + "'");
            builder.Append(",'" + obj.zonghefazhan + "'");
            builder.Append(",'" + obj.fuzhujiancha + "'");
            builder.Append(",'" + obj.otherjiancha + "'");
            builder.Append(",'" + obj.zhusu + "'");
            builder.Append(",'" + obj.bingshi + "'");
            builder.Append(",'" + obj.tijian + "'");
            builder.Append(",'" + obj.zhenduan + "'");
            builder.Append(",'" + obj.chuli + "'");
            builder.Append(" )");
            return builder.ToString();
        }

        /// <summary>
        /// 体检修改
        /// </summary>
        /// <returns></returns>
        private string getcheckupdateSql()
        {
            StringBuilder builder = new StringBuilder();
            ChildCheckObj obj = new ChildCheckObj();
            obj.ChildId = globalInfoClass.Wm_Index;
            //obj.CheckAge = this.txt_checkAge.Text.Trim();
            //月份
            obj.CheckMonth = dgvcheckMonthList.CurrentCell.Value.ToString();
            obj.CheckFactAge = this.txt_checkFactAge.Text.Trim();
            obj.CheckDay = this.dtp_checkDay.Value.ToString("yyyy-MM-dd");
            obj.BloodseSu = this.txt_bloodseSu.Text.Trim();
            obj.DoctorName = cbx_doctorName.Text.Trim();

            obj.YufangJiezhong = getcheckedValue(pnl_yufangJiezhong);
            //
            obj.CheckHeight = this.txt_checkHeight.Text.Trim();
            obj.CheckWeight = this.txt_checkWeight.Text.Trim();

            obj.CheckTouwei = this.txt_checkTouwei.Text.Trim();
            obj.CheckZuogao = this.txt_checkZuogao.Text.Trim();

            obj.zhushi = this.textBoxX25.Text.Trim();
            obj.fushi = this.textBoxX26.Text.Trim();

            obj.Vitd = this.cbx_vitd.Text.Trim();
            obj.OtherBingshi = this.txt_otherBingshi.Text.Trim();
            obj.shehui = this.comboBox5.Text.Trim();
            obj.WuGuan = this.cbx_wuGuan.Text.Trim();
            obj.Checkbi = this.cbx_checkbi.Text.Trim();
            obj.dongzuo = this.comboBox20.Text.Trim();
            obj.Skin = this.cbx_skin.Text.Trim();
            obj.JiZhu = this.cbx_jiZhu.Text.Trim();
            obj.Laguage = this.txt_laguage.Text.Trim();
            obj.Lingbaji = this.cbx_lingbajie.Text.Trim();
            obj.BigSport = this.cbx_bigSport.Text.Trim();
            obj.XiongBu = this.cbx_xiongBu.Text.Trim();
            obj.SiZhi = this.cbx_siZhi.Text.Trim();
            obj.CheckQianlu = this.txt_checkQianlu.Text.Trim();
            obj.XinZang = this.cbx_XinZang.Text.Trim();
            obj.toulu = this.comboBox30.Text.Trim();
            obj.FeiBu = this.cbx_FeiBu.Text.Trim();
            obj.KouQiang = this.cbx_kouQiang.Text.Trim();
            obj.YaciNumber = this.txt_yaciNumber.Text.Trim();
            obj.YuciNumber = this.txt_yuciNumber.Text.Trim();
            obj.Fubu = this.txt_fubu.Text.Trim();
            obj.GanZang = this.cbx_ganZang.Text.Trim();
            obj.MiNiaoQi = this.cbx_miNiaoQi.Text.Trim();

            obj.gouloubing = this.comboBox36.Text.Trim();

            obj.CheckContent = getcheckedValue(pnl_checkContent);
            //复诊日期
            obj.FuzenDay = this.dtp_fuzen.Value.ToString("yyyy-MM-dd");
            obj.checkdiagnose = this.txt_checkdiagonse.Text;

            obj.nuerzhidao = this.txtyingyangzhidao.Text.Trim();
            obj.zonghefazhan = this.txtzaoqifazhan.Text.Trim();
            obj.fuzhujiancha = getcheckedValue(panel3);
            obj.otherjiancha = getcheckedValue(panel5);
            obj.zhusu = this.textBox5.Text.Trim();
            obj.bingshi = this.textBox6.Text.Trim();
            obj.tijian = this.textBox7.Text.Trim();
            obj.zhenduan = this.textBox8.Text.Trim();
            obj.chuli = this.textBox9.Text.Trim();

            builder.Append("update  tb_childcheck set ");
            builder.Append("childId=" + obj.ChildId + "");
            builder.Append(",checkAge='" + obj.CheckAge + "'");
            builder.Append(",checkFactAge='" + obj.CheckFactAge + "'");
            builder.Append(",checkDay='" + obj.CheckDay + "'");
            builder.Append(",doctorName='" + obj.DoctorName + "'");
            builder.Append(",checkHeight='" + obj.CheckHeight + "'");
            builder.Append(",checkWeight='" + obj.CheckWeight + "'");
            builder.Append(",checkTouwei='" + obj.CheckTouwei + "'");
            builder.Append(",fuwei='" + obj.Fuwei + "'");
            builder.Append(",checkZuogao='" + obj.CheckZuogao + "'");
            builder.Append(",checkTunwei='" + obj.CheckTunwei + "'");
            builder.Append(",checkQianlu='" + obj.CheckQianlu + "'");
            builder.Append(",checkIQ='" + obj.CheckIQ + "'");
            builder.Append(",checkZiping='" + obj.CheckZiping + "'");
            //builder.Append(",yuCeHeight='" + obj.YuCeHeight + "'");
            builder.Append(",leftyan='" + obj.Leftyan + "'");
            builder.Append(",rightyan='" + obj.Rightyan + "'");
            builder.Append(",leftyanshili='" + obj.Leftyanshili + "'");
            builder.Append(",rightyanshili='" + obj.Rightyanshili + "'");
            builder.Append(",xieshi='" + obj.Xieshi + "'");
            builder.Append(",lefter='" + obj.Lefter + "'");
            builder.Append(",righter='" + obj.Righter + "'");
            builder.Append(",lefterlisten='" + obj.Lefterlisten + "'");
            builder.Append(",rightlisten='" + obj.Rightlisten + "'");
            builder.Append(",checkbi='" + obj.Checkbi + "'");
            builder.Append(",kouQiang='" + obj.KouQiang + "'");
            builder.Append(",yaciNumber='" + obj.YaciNumber + "'");
            builder.Append(",yuciNumber='" + obj.YuciNumber + "'");
            builder.Append(",skin='" + obj.Skin + "'");
            builder.Append(",lingbajie='" + obj.Lingbaji + "'");
            builder.Append(",bianTaoti='" + obj.BianTaoti + "'");
            builder.Append(",xinZang='" + obj.XinZang + "'");
            builder.Append(",feiBu='" + obj.FeiBu + "'");
            builder.Append(",ganZang='" + obj.GanZang + "'");
            builder.Append(",piZang='" + obj.PiZang + "'");
            builder.Append(",qiZhi='" + obj.QiZhi + "'");
            builder.Append(",siZhi='" + obj.SiZhi + "'");
            builder.Append(",xiongBu='" + obj.XiongBu + "'");
            builder.Append(",miNiaoQi='" + obj.MiNiaoQi + "'");
            builder.Append(",wuGuan='" + obj.WuGuan + "'");
            builder.Append(",jiZhu='" + obj.JiZhu + "'");
            builder.Append(",pingPqiu='" + obj.PingPqiu + "'");
            builder.Append(",checkContent='" + obj.CheckContent + "'");
            builder.Append(",yufangJiezhong='" + obj.YufangJiezhong + "'");
            builder.Append(",bloodseSu='" + obj.BloodseSu + "'");
            builder.Append(",shiWugouCheng='" + obj.ShiWugouCheng + "'");
            builder.Append(",vitd='" + obj.Vitd + "'");
            builder.Append(",bigSport='" + obj.BigSport + "'");
            builder.Append(",spirtSport='" + obj.SpirtSport + "'");
            builder.Append(",laguage='" + obj.Laguage + "'");
            builder.Append(",signSocial='" + obj.SignSocial + "'");
            builder.Append(",otherBingshi='" + obj.OtherBingshi + "'");
            builder.Append(",handle='" + obj.Handle + "'");
            builder.Append(",diagnose='" + obj.Diagnose + "'");
            builder.Append(",checkMonth='" + obj.CheckMonth + "'");
            builder.Append(",fuzenDay='" + obj.FuzenDay + "'");
            builder.Append(",fubu='" + obj.Fubu + "'");
            builder.Append(",nuerzhidao='" + obj.nuerzhidao + "'");
            builder.Append(",checkdiagnose='" + obj.checkdiagnose + "'");
            builder.Append(",zhushi = '" + obj.zhushi + "'");
            builder.Append(",fushi = '" + obj.fushi + "'");
            builder.Append(",shehui = '" + obj.shehui + "'");
            builder.Append(",dongzuo = '" + obj.dongzuo + "'");
            builder.Append(",toulu = '" + obj.toulu + "'");
            builder.Append(",gouloubing = '" + obj.gouloubing + "'");
            builder.Append(",zonghefazhan = '" + obj.zonghefazhan + "'");
            builder.Append(",fuzhujiancha = '" + obj.fuzhujiancha + "'");
            builder.Append(",otherjiancha='"+obj.otherjiancha+"'");
            builder.Append(",zhusu='" + obj.zhusu + "'");
            builder.Append(",bingshi='" + obj.bingshi + "'");
            builder.Append(",tijian='" + obj.tijian + "'");
            builder.Append(",zhenduan='" + obj.zhenduan + "'");
            builder.Append(",chuli='" + obj.chuli + "'");
            builder.Append(" where id = " + _checkobj.Id + "");
            return builder.ToString();
        }

        //BMI事件计算方法
        private void textBox28_Leave(object sender, EventArgs e)
        {
            getTigepingjia(txt_checkHeight.Text, txt_checkWeight.Text, txt_checkTouwei.Text);
        }

        //BMI事件计算方法
        private void getTigepingjia(string checkheight,string checkweight,string checktouwei)
        {
            if (!String.IsNullOrEmpty(checkheight) && !String.IsNullOrEmpty(checkweight))
            {
                Double high = 0;
                Double weight = 0;

                if (double.TryParse(checkheight, out high) && double.TryParse(checkweight, out weight))
                {
                    Double BMI = weight / ((high / 100) * (high / 100));
                    label52.Text = "BMI：" + BMI.ToString("f1");
                    label53.ForeColor = Color.Black;
                    label59.ForeColor = Color.Black;
                    label58.ForeColor = Color.Black;
                    label60.ForeColor = Color.Black;
                    if (bmistandbmiobj != null)
                    {
                        string bmicankao = "";
                        if (BMI < Convert.ToDouble(bmistandbmiobj.p3))
                        {
                            bmicankao = "<P3";
                            label53.ForeColor = Color.Red;
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p15))
                        {
                            bmicankao = "p3-P15之间";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p50))
                        {
                            bmicankao = "p15-P50之间";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p85))
                        {
                            bmicankao = "p50-P85之间";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p97))
                        {
                            bmicankao = "p85-P97之间";
                        }
                        else
                        {
                            bmicankao = ">P97";
                            label53.ForeColor = Color.Red;
                        }
                        label53.Text = "BMI评价：" + bmicankao;
                    }
                }
            }

            if (!String.IsNullOrEmpty(checkheight))
            {
                Double high = 0;

                if (double.TryParse(checkheight, out high))
                {
                    
                    if (shengaostandobj != null)
                    {
                        string highcankao = "";
                        if (high < Convert.ToDouble(shengaostandobj.p3))
                        {
                            highcankao = "<P3";
                            label59.ForeColor = Color.Red;
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p15))
                        {
                            highcankao = "p3-P15之间";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p50))
                        {
                            highcankao = "p15-P50之间";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p85))
                        {
                            highcankao = "p50-P85之间";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p97))
                        {
                            highcankao = "p85-P97之间";
                        }
                        else
                        {
                            highcankao = ">P97";
                            label59.ForeColor = Color.Red;
                        }
                        label59.Text = "身高评价：" + highcankao;

                    }
                }
            }

            if (!String.IsNullOrEmpty(checkweight))
            {
                Double weight = 0;

                if (double.TryParse(checkweight, out weight))
                {
                    if (tizhongstandobj != null)
                    {
                        string weightcankao = "";
                        if (weight < Convert.ToDouble(tizhongstandobj.p3))
                        {
                            weightcankao = "<P3";
                            label58.ForeColor = Color.Red;
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p15))
                        {
                            weightcankao = "p3-P15之间";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p50))
                        {
                            weightcankao = "p15-P50之间";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p85))
                        {
                            weightcankao = "p50-P85之间";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p97))
                        {
                            weightcankao = "p85-P97之间";
                        }
                        else
                        {
                            weightcankao = ">P97";
                            label58.ForeColor = Color.Red;
                        }
                        label58.Text = "体重评价：" + weightcankao;
                    }
                }
            }

            if (!String.IsNullOrEmpty(txt_checkTouwei.Text))
            {
                Double touwei = 0;
                if (double.TryParse(txt_checkTouwei.Text, out touwei))
                {
                    if (touweistandobj != null)
                    {
                        string touweicankao = "";
                        if (touwei < Convert.ToDouble(touweistandobj.p3))
                        {
                            touweicankao = "<P3";
                            label60.ForeColor = Color.Red;
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p15))
                        {
                            touweicankao = "p3-P15之间";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p50))
                        {
                            touweicankao = "p15-P50之间";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p85))
                        {
                            touweicankao = "p50-P85之间";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p97))
                        {
                            touweicankao = "p85-P97之间";
                        }
                        else
                        {
                            touweicankao = ">P97";
                            label60.ForeColor = Color.Red;
                        }
                        label60.Text = "头围评价：" + touweicankao;
                    }
                }
            }
        }
        //BMI标准
        private void getBmistand(string checkday)
        {
            string sqls_stand = "select * from who_childstand_day where sex='" + obj.ChildGender + "' and tian=datediff(day,'" + obj.ChildBirthDay + "','" + checkday + "')";
            ArrayList standlist = new ChildStandardBll().getwhoStandard_day_list(sqls_stand);
            if (standlist != null && standlist.Count > 0)
            {
                foreach(WhoChildStandardDayObj whostandobj in standlist)
                {
                    if (whostandobj.ptype == "BMI")
                        bmistandbmiobj = whostandobj;
                    else if (whostandobj.ptype == "体重" )
                        tizhongstandobj = whostandobj;
                    else if (whostandobj.ptype == "身高")
                        shengaostandobj = whostandobj;
                    else if (whostandobj.ptype == "头围")
                        touweistandobj = whostandobj;
                }
            }

            getTigepingjia(txt_checkHeight.Text, txt_checkWeight.Text, txt_checkTouwei.Text);
        }

        //指导等
        private void getzhidaostand(string checkday)
        {
            string sqls_zhidao = "select * from tb_moban where  yf<=datediff(month,'" + obj.ChildBirthDay + "','" + checkday + "') and yfend>=datediff(month,'" + obj.ChildBirthDay + "','" + checkday + "') ";
            ArrayList mobanlist = new MobanManageBll().getMobanList(sqls_zhidao);
            if (mobanlist != null && mobanlist.Count > 0)
            {
                foreach(mobanManageObj mobanobj in mobanlist)
                {
                    if (mobanobj.m_type == "营养指导")
                        txtyingyangzhidao.Text = mobanobj.m_content;
                    if (mobanobj.m_type == "行为习惯培养")
                        txt_checkdiagonse.Text = mobanobj.m_content;
                    if (mobanobj.m_type == "早期综合发展")
                        txtzaoqifazhan.Text = mobanobj.m_content;
                }
            }
        }
        /// <summary>
        /// 刷新体检界面
        /// </summary>
        private void RefreshcheckCode()
        {
            this.label49.Text = obj.HealthCardNo;
            this.label50.Text = obj.ChildName;
            this.label51.Text = obj.ChildGender;
            this.label57.Text = Convert.ToDateTime(obj.ChildBirthDay).ToString("yyyy-MM-dd");
            
            //string sqls = string.Format("select * from tb_childcheck where childId = " + globalInfoClass.Wm_Index + "");
            //_checkobj = checkbll.getChildCheckobj(sqls);
            if (_checkobj != null)
            {
                //_checkobj.ChildId = globalInfoClass.Wm_Index;
                //_checkobj.CheckAge = this.txt_checkAge.Text.Trim();
                //月份
                 this.txt_checkFactAge.Text = _checkobj.CheckFactAge;
                 this.dtp_checkDay.Value = Convert.ToDateTime(_checkobj.CheckDay);
                 this.txt_bloodseSu.Text = _checkobj.BloodseSu;
                 cbx_doctorName.Text = _checkobj.DoctorName;

                 setcheckedValue(pnl_yufangJiezhong,_checkobj.YufangJiezhong);
                //
                this.txt_checkHeight.Text = _checkobj.CheckHeight;
                 this.txt_checkWeight.Text = _checkobj.CheckWeight;

                 this.txt_checkTouwei.Text = _checkobj.CheckTouwei;
                 this.txt_checkZuogao.Text = _checkobj.CheckZuogao;

                 this.textBoxX25.Text = _checkobj.zhushi;
                this.textBoxX26.Text = _checkobj.fushi ;

                 this.cbx_vitd.Text = _checkobj.Vitd;
                 this.txt_otherBingshi.Text = _checkobj.OtherBingshi;
                 this.comboBox5.Text = _checkobj.shehui;
                 this.cbx_wuGuan.Text = _checkobj.WuGuan;
                 this.cbx_checkbi.Text = _checkobj.Checkbi;
                 this.comboBox20.Text = _checkobj.dongzuo;
                 this.cbx_skin.Text = _checkobj.Skin;
                 this.cbx_jiZhu.Text = _checkobj.JiZhu;
                 this.txt_laguage.Text = _checkobj.Laguage;
                 this.cbx_lingbajie.Text = _checkobj.Lingbaji;
                this.cbx_bigSport.Text =  _checkobj.BigSport;
                 this.cbx_xiongBu.Text = _checkobj.XiongBu;
                 this.cbx_siZhi.Text = _checkobj.SiZhi;
                 this.txt_checkQianlu.Text = _checkobj.CheckQianlu;
                 this.cbx_XinZang.Text = _checkobj.XinZang;
                 this.comboBox30.Text = _checkobj.toulu;
                 this.cbx_FeiBu.Text = _checkobj.FeiBu;
                 this.cbx_kouQiang.Text = _checkobj.KouQiang;
                 this.txt_yaciNumber.Text = _checkobj.YaciNumber;
                 this.txt_yuciNumber.Text = _checkobj.YuciNumber;
                 this.txt_fubu.Text = _checkobj.Fubu;
                 this.cbx_ganZang.Text = _checkobj.GanZang;
                 this.cbx_miNiaoQi.Text = _checkobj.MiNiaoQi;

                 this.comboBox36.Text = _checkobj.gouloubing;

                 setcheckedValue(pnl_checkContent,_checkobj.CheckContent);
        
                //复诊日期
                 this.dtp_fuzen.Value = Convert.ToDateTime(_checkobj.FuzenDay);
                 this.txt_checkdiagonse.Text = _checkobj.checkdiagnose;
                 this.txtyingyangzhidao.Text = _checkobj.nuerzhidao;
                 this.txtzaoqifazhan.Text = _checkobj.zonghefazhan;
                 setcheckedValue(panel3, _checkobj.fuzhujiancha);
                 setcheckedValue(panel5, _checkobj.otherjiancha);
                 textBoxX15.Text = new TbGaoweiBll().getGaoweistr(_checkobj.ChildId, "","");

                 this.textBox5.Text = _checkobj.zhusu;
                 this.textBox6.Text = _checkobj.bingshi;
                 this.textBox7.Text = _checkobj.tijian;
                 this.textBox8.Text = _checkobj.zhenduan;
                 this.textBox9.Text = _checkobj.chuli;

                 getBmistand(this.dtp_checkDay.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                label52.Text = "BMI：";
                label53.Text = "BMI评价：";
                label59.Text = "身高评价：";
                label58.Text = "体重评价：";
                label60.Text = "头围评价：";
                //_checkobj.ChildId = globalInfoClass.Wm_Index;
                //_checkobj.CheckAge = this.txt_checkAge.Text.Trim();
                //月份
                //DateTime dtbirth = Convert.ToDateTime(obj.ChildBirthDay);
                //DateTime dtnow = DateTime.Now;
                //TimeSpan timeSpan = dtnow - dtbirth;
                int[] age = getAgeBytime(obj.ChildBirthDay, this.dtp_checkDay.Value.ToString("yyyy-MM-dd"));
                string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
                this.txt_checkFactAge.Text = agestr;

                this.dtp_checkDay.Value = DateTime.Now;
                this.txt_bloodseSu.Text = "";
                //cbx_doctorName.Text = globalInfoClass.UserName;

                setcheckedValue(pnl_yufangJiezhong, "");
                //
                this.txt_checkHeight.Text = "";
                this.txt_checkWeight.Text = "";

                this.txt_checkTouwei.Text = "";
                this.txt_checkZuogao.Text = "";

                this.textBoxX25.Text = "";
                this.textBoxX26.Text = "";

                this.cbx_vitd.Text = "400IU/天";
                this.txt_otherBingshi.Text = "";
                this.comboBox5.Text = "正常";
                this.cbx_wuGuan.Text = "正常";
                this.cbx_checkbi.Text = "正常";
                this.comboBox20.Text = "正常";
                this.cbx_skin.Text = "正常";
                this.cbx_jiZhu.Text = "未见畸形";
                this.txt_laguage.Text = "正常";
                this.cbx_lingbajie.Text = "正常";
                this.cbx_bigSport.Text = "正常";
                this.cbx_xiongBu.Text = "正常";
                this.cbx_siZhi.Text = "正常";
                this.txt_checkQianlu.Text = "0.5";
                this.cbx_XinZang.Text = "正常";
                this.comboBox30.Text = "正常";
                this.cbx_FeiBu.Text = "正常";
                this.cbx_kouQiang.Text = "正常";
                this.txt_yaciNumber.Text = "";
                this.txt_yuciNumber.Text = "";
                this.txt_fubu.Text = "正常";
                this.cbx_ganZang.Text = "正常";
                this.cbx_miNiaoQi.Text = "正常";

                this.comboBox36.Text = "正常";

                //setcheckedValue(pnl_checkContent, _checkobj.CheckContent);
                //复诊日期
                //this.dtp_fuzen.Value = DateTime.Now;
                this.txt_checkdiagonse.Text = "";
                this.txtyingyangzhidao.Text = "";
                this.txtzaoqifazhan.Text = "";
                setcheckedValue(pnl_checkContent, "");
                setcheckedValue(panel3, "");

                this.textBox5.Text = "";
                this.textBox6.Text = "";
                this.textBox7.Text = "";
                this.textBox8.Text = "";
                this.textBox9.Text = "";

                textBoxX15.Text = new TbGaoweiBll().getGaoweistr(globalInfoClass.Wm_Index, "", "");

                getzhidaostand(this.dtp_checkDay.Value.ToString("yyyy-MM-dd"));
                
                int totalmonth = age[0] * 12 + age[1];
                getcheckmoban(totalmonth);

                getBmistand(this.dtp_checkDay.Value.ToString("yyyy-MM-dd"));
            }
            
        }

        /// <summary>
        /// 加载体检信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadPaneljibenChildCheckInfo()
        {
            if (globalInfoClass.Wm_Index == -1)
            {
                return;
            }
            //加载体检年龄列时间表
            RefreshcheckCodeList();
        }

        /// <summary>
        /// 刷新体检列表
        /// </summary>
        public void RefreshcheckCodeList()
        {
            if (globalInfoClass.Wm_Index != -1)
            {
                if (obj.HealthCardNo != label49.Text)
                {
                    Refreshcheckdatagridview();
                }
            }
            else
            {
                MessageBox.Show("请选择儿童信息");
                tabControl1.SelectedTab = tabPage1;
            }
        }

        /// <summary>
        /// 刷新体检列表
        /// </summary>
        public void Refreshcheckdatagridview()
        {
            dgvcheckMonthList.Rows.Clear();
            string sqls1 = "select * from tb_childcheck where childId=" + globalInfoClass.Wm_Index+" order by checkday ";
            ArrayList checkobjlist = checkbll.getChildCheckList(sqls1);
            if (checkobjlist != null && checkobjlist.Count > 0)
            {
                 foreach (ChildCheckObj checkobj in checkobjlist)
                 {
                     int[] age = getAgeBytime(obj.ChildBirthDay,checkobj.CheckDay);
                     string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "");// +(age[2] > 0 ? age[2].ToString() + "天" : "");
                     DataGridViewRow row = new DataGridViewRow();
                     row.CreateCells(dgvcheckMonthList, Convert.ToDateTime(checkobj.CheckDay).ToString("yyyy-MM-dd"), agestr);
                     row.Tag = checkobj;
                     dgvcheckMonthList.Rows.Add(row);
                     if (_checkobj != null && _checkobj.Id == checkobj.Id)
                     {
                         row.Selected = true;
                     }
                 }
                 if (dgvcheckMonthList.CurrentRow.Index == -1)
                 {
                      this.dgvcheckMonthList.Rows[checkobjlist.Count].Selected = true;//默认选中行

                 }
                 dgvcheckMonthList_CellClick(new object(), null);
             }
             else
             {
                 _checkobj = null;
                 RefreshcheckCode();
             }
        }

        //获取复选框中的值及其他备注项目的值
        private string getcheckedValue(Panel panels)
        {
            string returnval = "";
            foreach (Control c in panels.Controls)
            {
                if (c is CheckBox)
                {
                    if ((c as CheckBox).Checked)
                    {
                        returnval += (c.Text + ",");
                    }
                }
                else if (c is TextBox)
                {
                    if (!String.IsNullOrEmpty(c.Text))
                    {
                        returnval += (":" + c.Text + ",");
                    }
                }
                else if (c is ComboBox)
                {
                    if (!String.IsNullOrEmpty(c.Text))
                    {
                        returnval += (":" + c.Text + ",");
                    }
                }
            }
            if (returnval != "")
            {
                returnval = returnval.Substring(0, returnval.Length - 1);
            }
            return returnval;
        }
        //根据读取的值重新为复选框赋值
        private void setcheckedValue(Panel panels, string checkvals)
        {
            //假如读取到的值不为null
            if (!String.IsNullOrEmpty(checkvals))
            {
                //通过分割符拆分字符串为数组
                string[] splitvals = null;
                if (checkvals.Contains(","))
                {
                    splitvals = checkvals.Split(',');
                }
                else
                {
                    splitvals = new string[] { checkvals };
                }
                //循环panel自动为复选框赋值
                foreach (Control c in panels.Controls)
                {
                    if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = false;//默认不选中
                        foreach (string splitval in splitvals)
                        {
                            if (c.Text == splitval)
                            {
                                (c as CheckBox).Checked = true;
                                break;
                            }
                        }
                    }
                    else if (c is TextBox)
                    {
                        c.Text = "";
                        foreach (string splitval in splitvals)
                        {
                            if (splitval.Contains(":"))
                            {
                                c.Text = splitval.Replace(":", "");
                                break;
                            }
                        }
                    }
                    else if (c is ComboBox)
                    {
                        c.Text = "";
                        foreach (string splitval in splitvals)
                        {
                            if (splitval.Contains(":"))
                            {
                                c.Text = splitval.Replace(":", "");
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Control c in panels.Controls)
                {
                    if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = false;
                    }
                    else if (c is TextBox)
                    {
                        c.Text = "";
                    }
                }
            }

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                RefreshCode();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                RefreshcheckCodeList();
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                if (globalInfoClass.Wm_Index != -1)
                {
                    //tongji_BMI tjhight = new tongji_BMI(obj.ChildGender,obj.ChildBirthDay,obj.cs_week,obj.cs_day,obj.ispre,obj.birthWeight,obj.birthHeight);
                    //tabPage3.Controls.Clear();
                    //tabPage3.Controls.Add(tjhight);
                    //tjhight.Show();
                }
                else
                {
                    MessageBox.Show("请选择儿童信息");
                    tabControl1.SelectedTab = tabPage1;
                }
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                if (globalInfoClass.Wm_Index != -1)
                {
                    ArrayList gaoweilist = new TbGaoweiBll().getGaoweilist("select * from tb_gaowei where type='高危' and gaoweiyinsu !='' and gaoweiyinsu is not null and childid=" + globalInfoClass.Wm_Index);
                    if (gaoweilist!=null&&gaoweilist.Count > 0)
                    {
                        gaoweizhuanan = new Panel_gaowei_zhuanan(obj);
                        tabPage4.Controls.Clear();
                        tabPage4.Controls.Add(gaoweizhuanan);
                    }
                    else
                    {
                        MessageBox.Show("该儿童不属于高危");
                        tabControl1.SelectedTab = tabPage1;
                    }
                }
                else
                {
                    MessageBox.Show("请选择儿童信息");
                    tabControl1.SelectedTab = tabPage1;
                }
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                if (globalInfoClass.Wm_Index != -1)
                {
                    ArrayList gaoweilist = new TbGaoweiBll().getGaoweilist("select * from tb_gaowei where type='营养不良' and gaoweiyinsu !='' and gaoweiyinsu is not null and childid=" + globalInfoClass.Wm_Index);
                    if (gaoweilist!=null&&gaoweilist.Count > 0)
                    {
                        yingyangzhuanan = new Panel_yingyang_zhuanan(obj);
                        tabPage5.Controls.Clear();
                        tabPage5.Controls.Add(yingyangzhuanan);
                    }
                    else
                    {
                        MessageBox.Show("该儿童不属于营养不良");
                        tabControl1.SelectedTab = tabPage1;
                    }
                }
                else
                {
                    MessageBox.Show("请选择儿童信息");
                    tabControl1.SelectedTab = tabPage1;
                }
            }
            else if (tabControl1.SelectedTab == tabPage6)
            {
                if (globalInfoClass.Wm_Index != -1)
                {
                    ArrayList gaoweilist = new TbGaoweiBll().getGaoweilist("select * from tb_gaowei where type='营养不良' and gaoweiyinsu like '%贫血' and childid=" + globalInfoClass.Wm_Index);
                    if (gaoweilist != null && gaoweilist.Count > 0)
                    {
                        pinxuezhuanan = new Panel_pinxue_zhuanan(obj);
                        tabPage6.Controls.Clear();
                        tabPage6.Controls.Add(pinxuezhuanan);
                    }
                    else
                    {
                        MessageBox.Show("该儿童不属于贫血！");
                        tabControl1.SelectedTab = tabPage1;
                    }
                }
                else
                {
                    MessageBox.Show("请选择儿童信息");
                    tabControl1.SelectedTab = tabPage1;
                }
            }
            else if (tabControl1.SelectedTab == tabPage7)
            {
                if (globalInfoClass.Wm_Index != -1)
                {
                    ArrayList gaoweilist = new TbGaoweiBll().getGaoweilist("select * from tb_gaowei where type='营养不良' and gaoweiyinsu='佝偻病' and childid=" + globalInfoClass.Wm_Index);
                    if (gaoweilist != null && gaoweilist.Count > 0)
                    {
                        goulouzhuanan = new Panel_goulou_zhuanan(obj);
                        tabPage7.Controls.Clear();
                        tabPage7.Controls.Add(goulouzhuanan);
                    }
                    else
                    {
                        MessageBox.Show("该儿童不属于佝偻病！");
                        tabControl1.SelectedTab = tabPage1;
                    }
                }

                else
                {
                    MessageBox.Show("请选择儿童信息");
                    tabControl1.SelectedTab = tabPage1;
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrinter_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                printtijianjilu(false);
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                gaoweizhuanan.btnprint(false);
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                yingyangzhuanan.btnprint(false);
            }
            else if (tabControl1.SelectedTab == tabPage6)
            {
                pinxuezhuanan.btnprint(false);
            }
            else if (tabControl1.SelectedTab == tabPage7)
            {
                goulouzhuanan.btnprint(false);
            }
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPriview_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                printtijianjilu(true);
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                gaoweizhuanan.btnprint(true);
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                yingyangzhuanan.btnprint(true);
            }
            else if (tabControl1.SelectedTab == tabPage6)
            {
                pinxuezhuanan.btnprint(true);
            }
            else if (tabControl1.SelectedTab == tabPage7)
            {
                goulouzhuanan.btnprint(true);
            }

            
        }

        private void printtijianjilu(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (globalInfoClass.Wm_Index == -1)
            {
                MessageBox.Show("请保存儿童信息之后再预览打印！", "软件提示");
                return;
            }
            try
            {
                string sqls = "select * from tb_childBase where id=" + globalInfoClass.Wm_Index + "";
                ChildBaseInfoObj baseobj = childBaseInfobll.getchildBaseobj(sqls);
                if (baseobj == null)
                {
                    MessageBox.Show("请保存儿童信息之后再打印！", "软件提示");
                    return;
                }
                else
                {
                    string yf = "";
                    if (_checkobj == null)
                    {
                        MessageBox.Show("请选择儿童体检信息后再打印！", "软件提示");
                        return;
                    }
                    //else if (String.IsNullOrEmpty(checkInfo.getDqCheckMonth()))
                    //{

                    //    MessageBox.Show("请选择要打印的儿童体检月份！", "软件提示");
                    //    return;
                    //}
                    //yf = checkInfo.getDqCheckMonth();
                    //string sqls1 = string.Format("select * from tb_childcheck where childId=" + globalInfoClass.Wm_Index + " and checkMonth='" + yf + "'  order by checkDay desc");
                    //ChildCheckObj checkobj = childcheckbll.getChildCheckobj(sqls1);
                    //if (checkobj != null)
                    //{
                    string[] bmipingfen = new string[] { label52.Text + "  " + label53.Text + "  " + label59.Text , label58.Text + "  " + label60.Text };
                    //PaneljibenChildCheckPrinter childcheckPrinter = new PaneljibenChildCheckPrinter(baseobj, _checkobj, globalInfoClass.Wm_Index, bmipingfen);
                        //panelrecord.Patient = _patient;
                        //childcheckPrinter.Print(true);
                    //}
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            //设定tabpages1为当前项
            //tabControl1_Click(tabPage1, new EventArgs());
            tabControl1.SelectedTab = tabPage1;
            //打开查询窗体
            Paneltsb_searchycfInfo search = new Paneltsb_searchycfInfo();
            search.ShowDialog();
        }

        private void dateTimeInput1_ValueChanged(object sender, EventArgs e)
        {
            int[] age = getAgeBytime(dateTimeInput1.Value.ToString("yyyy-MM-dd"), DateTime.Now.ToString("yyyy-MM-dd"));
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            labelX2.Text = agestr;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (globalInfoClass.Wm_Index != -1)
                {
                    if (MessageBox.Show("删除该儿童的记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            string sqls = "update tb_childBase set status = '0'  where id=" + globalInfoClass.Wm_Index;
                            if (childBaseInfobll.updaterecord(sqls) > 0)
                            {

                                MessageBox.Show("删除成功!", "软件提示");
                                globalInfoClass.Wm_Index = -1;
                                RefreshCode();
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
                    MessageBox.Show("请选择要删除的儿童档案？");
                }
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                if (globalInfoClass.Wm_Index != -1)
                {
                    if (MessageBox.Show("删除该儿童本次体检记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            if (_checkobj != null)
                            {
                                string sqls = "delete from tb_childcheck  where id=" + _checkobj.Id;
                                if (checkbll.deleterecord(sqls) > 0)
                                {

                                    MessageBox.Show("删除成功!", "软件提示");
                                    _checkobj = null;
                                    Refreshcheckdatagridview();
                                }
                                else
                                {
                                    MessageBox.Show("删除失败!", "请联系管理员");
                                }
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
                    MessageBox.Show("请选择要删除的儿童档案？");
                }
            }
        }

        private void all_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");

        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            //Paneltsb_jiben_select_gaowei selectgaowei = new Paneltsb_jiben_select_gaowei();
            Paneltsb_jiben_gaowei selectgaowei = new Paneltsb_jiben_gaowei(textBoxX12.Text.Trim(),_childid);
            selectgaowei.ShowDialog();//将主窗体传递给子窗体
            if (selectgaowei.DialogResult == DialogResult.OK)
            {
                this.textBoxX12.Text = "";
                this.textBoxX12.Text = selectgaowei.returnval;
                selectgaowei.Close();
            }
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            //Paneltsb_jiben_select_gaowei selectgaowei = new Paneltsb_jiben_select_gaowei();
            Paneltsb_jiben_gaowei selectgaowei = new Paneltsb_jiben_gaowei(textBoxX15.Text.Trim(), _childid);
            selectgaowei.ShowDialog();//将主窗体传递给子窗体
            if (selectgaowei.DialogResult == DialogResult.OK)
            {
                this.textBoxX15.Text = "";
                this.textBoxX15.Text = selectgaowei.returnval;
                selectgaowei.Close();
            }
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            //设定tabpages1为当前项
            //tabControl1_Click(tabPage1, new EventArgs());
            tabControl1.SelectedTab = tabPage1;
            //打开查询窗体
            Paneltsb_readCard search = new Paneltsb_readCard(this);
            search.ShowDialog();
        }

        private void dtp_checkDay_Leave(object sender, EventArgs e)
        {
            getBmistand(this.dtp_checkDay.Value.ToString("yyyy-MM-dd"));
            getzhidaostand(this.dtp_checkDay.Value.ToString("yyyy-MM-dd"));
            

            int[] age = getAgeBytime(obj.ChildBirthDay, dtp_checkDay.Value.ToString("yyyy-MM-dd"));
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            txt_checkFactAge.Text = agestr;
            int totalmonth = age[0] * 12 + age[1];
            
            getcheckmoban(totalmonth);
        }

        private void allcombo_mousemove(object sender, MouseEventArgs e)
        {
            if (!(sender as ComboBox).Focused)
            {
                (sender as ComboBox).Focus();
            }
            else
            {
                (sender as ComboBox).DroppedDown = true;
            }
            //if (!(sender as ComboBox).DroppedDown)
            //{
            //    (sender as ComboBox).DroppedDown = true;
            //}
        }

        private void allcombo_mousehover(object sender, EventArgs e)
        {
            if (!(sender as ComboBox).Focused)
            {
                (sender as ComboBox).Focus();
            }
            else
            {
                (sender as ComboBox).DroppedDown = true;
            }
        }

        //private void textBoxX20_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)13)
        //    {
        //        //假如输入回车键后触发
        //        OracleDataReader sdr = null;
        //        HosDateLogic dg = new HosDateLogic();

        //        string sqls = string.Format("select * from tb_childBase where status='1' ");
        //        if (!String.IsNullOrEmpty(textBoxX20.Text))
        //        {
        //            sqls += " and jiuzhenCardNo = '" + textBoxX20.Text + "'";
        //        }
        //        else
        //        {
        //            return;
        //        }

        //        ArrayList list = childBaseInfobll.getchildBaseList(sqls);
        //        if (list != null && list.Count > 0)
        //        {
        //            ChildBaseInfoObj obj = list[0] as ChildBaseInfoObj;
        //            int id = obj.Id;
        //            globalInfoClass.Wm_Index = id;
        //            this.obj = obj;
        //            this.RefreshCode();
        //        }
        //        else
        //        {
        //            String usersql = "select visit_number,patient_name,regist_time,identityno,contact_phone,sex,age,address from v_yyt_registration where visit_number='" + textBoxX20.Text.Trim() + "' order by regist_time desc";
        //            try
        //            {

        //                sdr = dg.executequery(usersql);

        //                if (!sdr.HasRows)
        //                {
        //                    MessageBox.Show("检索不到病人就诊信息！", "系统提示");
        //                    textBoxX20.Focus();
        //                    return;

        //                }
        //                else
        //                {
        //                    sdr.Read();//读取第一行数据记录

        //                    this.textBoxX1.Text = sdr["patient_name"].ToString().Trim();//病人就诊号同时也是病历号
        //                    this.comboBox1.Text = sdr["sex"].ToString().Trim();//给病人姓名赋值
        //                    SendKeys.Send("{Tab}");

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //            finally
        //            {
        //                sdr.Close();
        //                dg.con_close();
        //            }
        //        }
        //    }
        //}

        private int[] getAgeBytime(string birthtime,string nowtime)
        {
            DateTime dtbirth = Convert.ToDateTime(birthtime);
            DateTime dtnow = Convert.ToDateTime(nowtime);
            int birthyear = dtbirth.Year;
            int birthmonth = dtbirth.Month;
            int birthday = dtbirth.Day;
            int nowyear = dtnow.Year;
            int nowmonth = dtnow.Month;
            int nowday = dtnow.Day;
            int yearcou = nowyear - birthyear;
            int monthcou = nowmonth - birthmonth;
            int daycou = nowday - birthday;

            if (yearcou < 0)
            {
                yearcou = 0;
            }
            if (monthcou <= 0)
            {
                monthcou = 12 + monthcou;
                yearcou = yearcou - 1;
            }
            if (daycou <= 0)
            {
                daycou = 30 + daycou;
                monthcou = monthcou - 1;
            }
            int[] age = new int[3];
            age[0] = yearcou;
            age[1] = monthcou;
            age[2] = daycou;
            return age;
        }

        private void buttonX7_Click_1(object sender, EventArgs e)
        {
            getzhidaostand(this.dtp_checkDay.Value.ToString("yyyy-MM-dd"));
        }

        private void getcheckmoban(int yf)
        {
            string sqls = "select * from tb_childcheck_moban where yfks<="+yf+" and  yfjs>="+yf;
            checkmobanobj = checkbll.getChildCheckobj(sqls);
            if (checkmobanobj != null)
            {
                this.comboBox5.Text = checkmobanobj.shehui; 
                this.comboBox20.Text = checkmobanobj.dongzuo;
                this.txt_laguage.Text = checkmobanobj.Laguage;
                this.cbx_bigSport.Text = checkmobanobj.BigSport;
            }
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ck = sender as CheckBox;
            if(ck.Text=="眼保健1")
                comboBox4.Enabled = ck.Checked;
            else if (ck.Text == "听性脑干反应")
                comboBox18.Enabled = ck.Checked;
            else if (ck.Text == "母乳分析")
                comboBox19.Enabled = ck.Checked;
            else if (ck.Text == "骨密度")
                comboBox21.Enabled = ck.Checked;
            else if (ck.Text == "运动评价")
                comboBox22.Enabled = ck.Checked;
            else if (ck.Text == "食物过敏")
                comboBox23.Enabled = ck.Checked;
            else if (ck.Text == "血常规")
                comboBox24.Enabled = ck.Checked;
            else if (ck.Text == "DDST")
                comboBox25.Enabled = ck.Checked;
            else if (ck.Text == "眼保健2")
                comboBox26.Enabled = ck.Checked;
            else if (ck.Text == "Gesell")
                comboBox27.Enabled = ck.Checked;
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            _checkobj = null;
            //dgvcheckMonthList.ClearSelection();
            RefreshcheckCode();
        }

        private void dtp_checkDay_ValueChanged(object sender, EventArgs e)
        {
            int[] age = getAgeBytime(obj.ChildBirthDay, dtp_checkDay.Value.ToString("yyyy-MM-dd"));
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            txt_checkFactAge.Text = agestr;
            int totalmonth = age[0] * 12 + age[1];
            DateTime dtfucha = DateTime.Now;
            if (totalmonth < 6)
            {
                dtfucha = this.dtp_checkDay.Value.AddMonths(1);

                this.dtp_fuzen.Value = outOfholiday(this.dtp_checkDay.Value.AddMonths(1));
            }
            else if (totalmonth < 12)
            {
                this.dtp_fuzen.Value = outOfholiday(this.dtp_checkDay.Value.AddMonths(2));
            }
            else if (totalmonth < 24)
            {
                this.dtp_fuzen.Value = outOfholiday(this.dtp_checkDay.Value.AddMonths(3));
            }
            else if (totalmonth < 36)
            {
                this.dtp_fuzen.Value = outOfholiday(this.dtp_checkDay.Value.AddMonths(6));
            }
            else
            {
                this.dtp_fuzen.Value = outOfholiday(this.dtp_checkDay.Value.AddMonths(12));
            }
        }

        private DateTime outOfholiday(DateTime dt)
        {
            //如果是星期六
            if (dt.DayOfWeek == DayOfWeek.Saturday)
            {
                dt.AddDays(2);
            }
            else if (dt.DayOfWeek == DayOfWeek.Sunday)//如果是星期天
            {
                dt.AddDays(1);
            }
            return dt;
        }

        private void dgvcheckMonthList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _checkobj = dgvcheckMonthList.SelectedRows[0].Tag as ChildCheckObj;
            RefreshcheckCode();
        }

        private void textBoxX25_TextChanged(object sender, EventArgs e)
        {
            if (_checkobj==null||String.IsNullOrEmpty(_checkobj.DoctorName))
            {
                if (String.IsNullOrEmpty(textBoxX25.Text.Trim()))
                {
                    cbx_doctorName.Text = "";
                }
                else
                {
                    cbx_doctorName.Text = globalInfoClass.UserName;
                }
            }
        }




        private void textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{Tab}");
            }
            if (e.KeyCode == Keys.Up)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("+{Tab}");
            }
        }

        private void groupPanel2_MouseClick(object sender, MouseEventArgs e)
        {
            this.groupPanel2.Focus();
        }

        private void comboBox31_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox29.Items.Clear();
            if(comboBox31.SelectedItem!=null)
            {
                string xpath = string.Format("/ProvinceCity/{0}/City", comboBox31.SelectedItem.ToString());
                XmlNodeList cities = doc.SelectNodes(xpath);
                foreach (XmlNode city in cities)
                {
                    comboBox29.Items.Add(city.Attributes["Name"].Value);
                }

                if (comboBox29.Items.Count >= 0)
                {
                    comboBox29.SelectedIndex = 0;
                }
            }
        }

        private void comboBox29_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox28.Items.Clear();
            if (comboBox29.SelectedItem != null)
            {
                string xpath = string.Format("/ProvinceCity/{0}/City[@Name='{1}']/CityArea",
                    comboBox31.SelectedItem.ToString(),
                    comboBox29.SelectedItem.ToString());

                XmlNodeList CityAreas = doc.SelectNodes(xpath);
                foreach (XmlNode area in CityAreas)
                {
                    comboBox28.Items.Add(area.Attributes["Name"].Value);
                }

                if (comboBox28.Items.Count > 0)
                {
                    comboBox28.SelectedIndex = 0;
                }
            }
        }

        private void buttonX12_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                tb_healthcheck_moban healthmoban = new tb_healthcheck_moban();
                healthmoban.zhusu = this.textBox5.Text.Trim();
                healthmoban.bingshi = this.textBox6.Text.Trim();
                healthmoban.tijian = this.textBox7.Text.Trim();
                healthmoban.zhenduan = this.textBox8.Text.Trim();
                healthmoban.chuli = this.textBox9.Text.Trim();
                Panel_moban_save mobansave = new Panel_moban_save(healthmoban);
                mobansave.ShowDialog();
            }
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                Panel_moban_list mobanlist = new Panel_moban_list();
                mobanlist.ShowDialog();
                if(mobanlist.DialogResult == DialogResult.OK)
                {
                    tb_healthcheck_moban healthmoban = mobanlist._healthcheckobj;
                    this.textBox5.Text = healthmoban.zhusu;
                    this.textBox6.Text = healthmoban.bingshi;
                    this.textBox7.Text = healthmoban.tijian;
                    this.textBox8.Text = healthmoban.zhenduan;
                    this.textBox9.Text = healthmoban.chuli;
                }
            }
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (globalInfoClass.Wm_Index == -1)
            {
                MessageBox.Show("请保存儿童信息之后再预览打印！", "软件提示");
                return;
            }
            try
            {
                string sqls = "select * from tb_childBase where id=" + globalInfoClass.Wm_Index + "";
                ChildBaseInfoObj baseobj = childBaseInfobll.getchildBaseobj(sqls);
                if (baseobj == null)
                {
                    MessageBox.Show("请保存儿童信息之后再打印！", "软件提示");
                    return;
                }
                else
                {
                    string yf = "";
                    if (_checkobj == null)
                    {
                        MessageBox.Show("请选择儿童体检信息后再打印！", "软件提示");
                        return;
                    }

                    //PaneljibenCheckBingliPrinter checkbingliPrinter = new PaneljibenCheckBingliPrinter(baseobj, _checkobj, globalInfoClass.Wm_Index);
                    //panelrecord.Patient = _patient;
                    //checkbingliPrinter.Print(true);
                    //}
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (globalInfoClass.Wm_Index == -1)
            {
                MessageBox.Show("请保存儿童信息之后再预览打印！", "软件提示");
                return;
            }
            try
            {
                string sqls = "select * from tb_childBase where id=" + globalInfoClass.Wm_Index + "";
                ChildBaseInfoObj baseobj = childBaseInfobll.getchildBaseobj(sqls);
                if (baseobj == null)
                {
                    MessageBox.Show("请保存儿童信息之后再打印！", "软件提示");
                    return;
                }
                else
                {
                    string yf = "";
                    if (_checkobj == null)
                    {
                        MessageBox.Show("请选择儿童体检信息后再打印！", "软件提示");
                        return;
                    }

                    //PaneljibenCheckBingliPrinter checkbingliPrinter = new PaneljibenCheckBingliPrinter(baseobj, _checkobj, globalInfoClass.Wm_Index);
                    //panelrecord.Patient = _patient;
                    //checkbingliPrinter.Print(false);
                    //}
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            if (checkmobanobj != null)
            {
                string shehui = comboBox5.Text.Trim();
                if (shehui != checkmobanobj.shehui)
                {
                    comboBox5.ForeColor = Color.Red;
                }
                else
                {
                    comboBox5.ForeColor = Color.Black;
                }
            }
        }

        private void comboBox20_TextChanged(object sender, EventArgs e)
        {
            if (checkmobanobj != null)
            {
                string dongzuo = comboBox20.Text.Trim();
                if (dongzuo != checkmobanobj.dongzuo)
                {
                    comboBox20.ForeColor = Color.Red;
                }
                else
                {
                    comboBox20.ForeColor = Color.Black;
                }
            }
        }

        private void txt_laguage_TextChanged(object sender, EventArgs e)
        {
            if (checkmobanobj != null)
            {
                string yuyan = txt_laguage.Text.Trim();
                if (yuyan != checkmobanobj.Laguage)
                {
                    txt_laguage.ForeColor = Color.Red;
                }
                else
                {
                    txt_laguage.ForeColor = Color.Black;
                }
            }
        }

        private void cbx_bigSport_TextChanged(object sender, EventArgs e)
        {
            string bigSport = cbx_bigSport.Text.Trim();
            if(checkmobanobj!=null)
            { 
                if (bigSport != checkmobanobj.BigSport)
                {
                    cbx_bigSport.ForeColor = Color.Red;
                }
                else
                {
                    cbx_bigSport.ForeColor = Color.Black;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                buttonX1.Visible = true;//保存
                buttonX2.Visible = true;//查找
                buttonX3.Visible = true;//删除
                buttonX6.Visible = true;//本院导入
                buttonX10.Visible = true;//就诊卡
                buttonX4.Visible = false;//预览
                buttonX5.Visible = false;//打印
                buttonX14.Visible = false;//病历预览
                buttonX15.Visible = false;//病历打印
                buttonX12.Visible = false;//保存模板
                buttonX13.Visible = false;//调用模板
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                buttonX1.Visible = true;//保存
                buttonX2.Visible = true;//查找
                buttonX3.Visible = true;//删除
                buttonX6.Visible = true;//本院导入
                buttonX10.Visible = true;//就诊卡
                buttonX4.Visible = true;//预览
                buttonX5.Visible = true;//打印
                buttonX14.Visible = true;//病历预览
                buttonX15.Visible = true;//病历打印
                buttonX12.Visible = true;//保存模板
                buttonX13.Visible = true;//调用模板
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {

                buttonX1.Visible = false;//保存
                buttonX2.Visible = true;//查找
                buttonX3.Visible = false;//删除
                buttonX6.Visible = true;//本院导入
                buttonX10.Visible = true;//就诊卡
                buttonX4.Visible = false;//预览
                buttonX5.Visible = false;//打印
                buttonX14.Visible = false;//病历预览
                buttonX15.Visible = false;//病历打印
                buttonX12.Visible = false;//保存模板
                buttonX13.Visible = false;//调用模板
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                buttonX1.Visible = true;//保存
                buttonX2.Visible = true;//查找
                buttonX3.Visible = true;//删除
                buttonX6.Visible = true;//本院导入
                buttonX10.Visible = true;//就诊卡
                buttonX4.Visible = true;//预览
                buttonX5.Visible = true;//打印
                buttonX14.Visible = false;//病历预览
                buttonX15.Visible = false;//病历打印
                buttonX12.Visible = false;//保存模板
                buttonX13.Visible = false;//调用模板
            }
            else if (tabControl1.SelectedTab == tabPage5)
            {
                buttonX1.Visible = true;//保存
                buttonX2.Visible = true;//查找
                buttonX3.Visible = true;//删除
                buttonX6.Visible = true;//本院导入
                buttonX10.Visible = true;//就诊卡
                buttonX4.Visible = true;//预览
                buttonX5.Visible = true;//打印
                buttonX14.Visible = false;//病历预览
                buttonX15.Visible = false;//病历打印
                buttonX12.Visible = false;//保存模板
                buttonX13.Visible = false;//调用模板

            }
            else if (tabControl1.SelectedTab == tabPage6)
            {
                buttonX1.Visible = true;//保存
                buttonX2.Visible = true;//查找
                buttonX3.Visible = true;//删除
                buttonX6.Visible = true;//本院导入
                buttonX10.Visible = true;//就诊卡
                buttonX4.Visible = true;//预览
                buttonX5.Visible = true;//打印
                buttonX14.Visible = false;//病历预览
                buttonX15.Visible = false;//病历打印
                buttonX12.Visible = false;//保存模板
                buttonX13.Visible = false;//调用模板
            }
            else if (tabControl1.SelectedTab == tabPage7)
            {
                buttonX1.Visible = true;//保存
                buttonX2.Visible = true;//查找
                buttonX3.Visible = true;//删除
                buttonX6.Visible = true;//本院导入
                buttonX10.Visible = true;//就诊卡
                buttonX4.Visible = true;//预览
                buttonX5.Visible = true;//打印
                buttonX14.Visible = false;//病历预览
                buttonX15.Visible = false;//病历打印
                buttonX12.Visible = false;//保存模板
                buttonX13.Visible = false;//调用模板
            }
        }
        
        private void buttonX16_Click(object sender, EventArgs e)
        {
            globalInfoClass.Wm_Index = -1;
            ChildMainForm mianform = this.ParentForm as ChildMainForm;
            //superTabControl1.Tabs.Clear();//默认清空所有tab
            mianform.updateMdiForm("儿保建卡", typeof(PanelyibanxinxiMain));
            //tijianluruToolStripMenuItem_Click(null, null);//默认点击新建档案
        }

    }
}
