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

namespace ChildManager.UI.jibenluru
{
    public partial class jibenxinxi_panel : UserControl
    {
        private tb_childbasebll jibenbll = new tb_childbasebll();//儿童建档基本信息业务处理类
        IDCardValidation validation = new IDCardValidation();
        WomenInfo _womeninfo = null;
        XmlDocument doc = new XmlDocument();
        HisPatientInfo _hisPatientInfo = null;
        public List<DicObj> listarea = new List<DicObj>();
        InputLanguage InputHuoDong = null;
        public jibenxinxi_panel(WomenInfo womeninfo)
        {
            InitializeComponent();
            doc.Load("省市区.xml");
            setCombValueAndText(new ComboBox(), "area.xml");
            _womeninfo = womeninfo;
            CommonHelper.SetAllControls(panel1);
        }
        public jibenxinxi_panel(WomenInfo cpwomeninfo, HisPatientInfo hisPatientInfo)
        {
            InitializeComponent();
            setCombValueAndText(new ComboBox(), "area.xml");
            
            _womeninfo = cpwomeninfo;
            _hisPatientInfo = hisPatientInfo;
            CommonHelper.SetAllControls(panel1);
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            if (_hisPatientInfo == null)
            {
                RefreshCode();
                childname.Focus();
            }
            else
            {
                jiuzhencardno.Text = _hisPatientInfo.VISIT_CARD_NO;
                childname.Text = _hisPatientInfo.PAT_NAME;
                childgender.Text = _hisPatientInfo.PHYSI_SEX_NAME;
                childbirthday.Text = _hisPatientInfo.DATE_BIRTH;
                telephone.Text = _hisPatientInfo.PHONE_NO;
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
                dt.Rows.Add(dicobj.id, dicobj.name);
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
            
            TB_CHILDBASE obj = getChildBaseInfoObj();
            if (_womeninfo.cd_id != -1)
            {
                obj.ID = _womeninfo.cd_id;
                b = jibenbll.Update(obj);
                
                if (b)
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    //new TbGaoweiBll().saveOrdeleteGaowei(textBoxX12.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));
                    _womeninfo.bindDataNowdayTreeView();
                    _womeninfo.treeView1_MouseDown(sender, new MouseEventArgs(MouseButtons.Left, 1, _womeninfo.treeView1.SelectedNode.Bounds.Location.X, _womeninfo.treeView1.SelectedNode.Bounds.Location.Y, 1));
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            else
            {
                //自动生成保健卡号
                obj.healthcardno = jibenbll.stateval().ToString();//保健卡号
                b = jibenbll.Add(obj);
                if (b)
                {
                    _womeninfo.cd_id = obj.ID;
                    this.healthcardno.Text = obj.healthcardno;
                    MessageBox.Show("保存成功！", "软件提示");
                    //if (!String.IsNullOrEmpty(textBoxX12.Text.Trim()))
                    //{
                    //    new TbGaoweiBll().saveOrdeleteGaowei(textBoxX12.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                    //}
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            //int[] age = CommonHelper.getAgeBytime(obj.childbirthday, DateTime.Now.ToString("yyyy-MM-dd"));
            _womeninfo.l_age.Text = CommonHelper.getAgeStr(obj.childbirthday);

        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private TB_CHILDBASE getChildBaseInfoObj()
        {
            TB_CHILDBASE obj = CommonHelper.GetObj<TB_CHILDBASE>(panel1.Controls);
            obj.status = "1";
            obj.childbuildday = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            obj.childbuildhospital = "重医大附属儿童医院";
            return obj;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode()
        {
            if (_womeninfo.cd_id != -1)
            {
                Cursor.Current = Cursors.WaitCursor;
                TB_CHILDBASE obj = jibenbll.Get(_womeninfo.cd_id);

                if (obj != null)
                {
                    CommonHelper.setForm(obj, panel1.Controls);
                    //_womeninfo.bindDataNowday();
                    //textBoxX12.Text = new TbGaoweiBll().getGaoweistr(_womeninfo.cd_id, "", "");
                }
            }
        }

        /// <summary>
        /// 本院信息导入
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode1(TB_CHILDBASE obj)
        {

            if (obj != null)
            {
                healthcardno.Text = "";//保健卡号
                string childname = obj.mothername;
                if (obj.childgender == "女")
                {
                    childname += "之女";
                }
                else
                {
                    childname += "之子";
                }

                this.childname.Text = childname;//姓名
                childgender.Text = obj.childgender;//性别
                //bloodtype.Text = obj.bloodtype;//血型
                DateTime dtbirth = DateTime.Now;
                DateTime.TryParse(obj.childbirthday, out dtbirth);
                childbirthday.Value = dtbirth;
                //childbuildday.Value = DateTime.Now;
                //childbuildhospital.Text = "重医大附属儿童医院";//建册医院
                //                                //存图片
                //                                //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
                //                                //父亲母亲
                //fathername.Text = obj.fathername;
                //fatherage.Text = obj.fatherage;
                fatherheight.Text = obj.fatherheight;
                fathereducation.Text = obj.fathereducation;
                //fatherjob.Text = obj.fatherjob;
                //mothername.Text = obj.mothername;
                //motherage.Text = obj.motherage;
                motherheight.Text = obj.motherheight;
                mothereducation.Text = obj.mothereducation;
                //motherjob.Text = obj.motherjob;
                telephone.Text = obj.telephone;
                province.Text = obj.province;
                //city.Text = obj.city;
                //area.Text = obj.area;
                //address.Text = obj.address;
                //nurseryinstitution.Text = obj.nurseryinstitution;

                //发证
                //obj.ImmuneUnit = cbx_immuneUnit.Text.Trim();
                //obj.ImmuneDay = dateTimeInput3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //obj.Community = comboBox5.Text.Trim();
                //obj.CensusRegister = comboBox4.Text.Trim();
                //出生史
                cs_fetus.Text = obj.cs_fetus;
                cs_produce.Text = obj.cs_produce;
                cs_week.Text = obj.cs_week;
                cs_day.Text = obj.cs_day;
                modedelivery.Text = obj.modedelivery;
                //perineumincision.Text = obj.perineumincision;
                //obj.FetusNumber = cbx_fetusNumber.Text = ;
                //neonatalcondition.Text = obj.neonatalcondition;
                birthweight.Text = obj.birthweight;
                birthheight.Text = obj.birthheight;
                //birthaddress.Text = obj.birthaddress;
                //母乳喂养
                //hospitalizedstates.Text = "纯母乳";
                //onemonth.Text = "纯母乳";
                //infourmonth.Text = "纯母乳";
                //fourtosixmonth.Text = "纯母乳";
                //obj.Yunqi = getcheckedValue(pnl_yunqi);
                //identityno.Text = obj.identityno;
            }
            else
            {
                healthcardno.Text = "";//保健卡号
                childname.Text = "";//姓名
                childgender.Text = "";//性别
                //bloodtype.Text = "";//血型
                childbirthday.Value = DateTime.Now;
                //childbuildday.Value = DateTime.Now;
                //childbuildhospital.Text = "重医大附属儿童医院";//建册医院
                //存图片
                //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
                //父亲母亲
                //fathername.Text = "";
                //fatherage.Text = "";
                fatherheight.Text = "";
                fathereducation.Text = "本科";
                //fatherjob.Text = "";
                //mothername.Text = "";
                //motherage.Text = "";
                motherheight.Text = "";
                mothereducation.Text = "本科";
                //motherjob.Text = "";
                telephone.Text = "";
                //address.Text = "";
                //nurseryinstitution.Text = "";

                //发证
                //obj.ImmuneUnit = cbx_immuneUnit.Text.Trim();
                //obj.ImmuneDay = dateTimeInput3.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //obj.Community = comboBox5.Text.Trim();
                //obj.CensusRegister = comboBox4.Text.Trim();
                //出生史
                cs_fetus.Text = "";
                cs_produce.Text = "";
                cs_week.Text = "";
                cs_day.Text = "";
                modedelivery.Text = "";
                //perineumincision.Text = "";
                //obj.FetusNumber = cbx_fetusNumber.Text = ;
                //neonatalcondition.Text = "";
                birthweight.Text = "";
                birthheight.Text = "";
                //birthaddress.Text = "";
                //母乳喂养
                //hospitalizedstates.Text = "纯母乳";
                //onemonth.Text = "纯母乳";
                //infourmonth.Text = "纯母乳";
                //fourtosixmonth.Text = "纯母乳";
                //obj.Yunqi = getcheckedValue(pnl_yunqi);
                //identityno.Text = "";
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            //打开查询窗体
            Paneltsb_searchycfInfo search = new Paneltsb_searchycfInfo();
            if(search.ShowDialog()==DialogResult.OK)
            {
                RefreshCode1(search.returnval);
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (_womeninfo.cd_id != -1)
            {
                if (MessageBox.Show("删除该儿童的记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (jibenbll.Delete(_womeninfo.cd_id))
                        {

                            MessageBox.Show("删除成功!", "软件提示");
                            _womeninfo.cd_id = -1;
                            RefreshCode();
                            //_womeninfo.bindDataNowday();
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

        private void all_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }
        

        private void childbirthday_ValueChanged_1(object sender, EventArgs e)
        {
            label_age.Text = CommonHelper.getAgeStr(childbirthday.Value.ToString("yyyy-MM-dd"));
        }


        private void childbirthday_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{RIGHT} ");
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

        private void province_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputHuoDong;
        }
    }
}
