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
using ChildManager.UI.printrecord;
using login.UI;
using System.Xml;
using YCF.Model;
using System.Collections.Generic;
using YCF.BLL;
using YCF.BLL.Template;
using YCF.Extension;

namespace ChildManager.UI.jibenluru
{
    public partial class tijian_panel : UserControl
    {
        private tb_childcheckbll checkbll = new tb_childcheckbll();//儿童体检业务处理类
        private tb_childbasebll basebll = new tb_childbasebll();//儿童建档基本信息业务处理类
        private WhoChildStandardDayObj bmistandbmiobj = null;
        private WhoChildStandardDayObj tizhongstandobj = null;
        private WhoChildStandardDayObj shengaostandobj = null;
        private WhoChildStandardDayObj touweistandobj = null;

        IList<tb_childcheck> _listcheck = null;

        private tb_childcheck _checkobj = null;
        private tb_childcheck checkobjForRef = null;
        private tb_childbase baseobj = null;
        private ChildCheckObj checkmobanobj = new ChildCheckObj();
        string _hospital = globalInfoClass.Hospital;
        private WomenInfo _womeninfo;
        //评价分等级  上、中上、中、中下、下
        // updownindex[0] BMI
        // updownindex[1] 身高
        // updownindex[2] 头围
        // updownindex[3] 体重
        // updownindex[4] 体重/身高
        private string[] updownindex = new string[5];
        public tijian_panel(WomenInfo womeninfo)
        {
            InitializeComponent();
            _womeninfo = womeninfo;
            baseobj = basebll.Get(womeninfo.cd_id);
            CommonHelper.SetAllControls(panel2);

            //移除病史、体检、诊断、处理的按回车跳转下一个控件事件
            bingshi.RemoveControlEvent("EventKeyPress");
            tijian.RemoveControlEvent("EventKeyPress");
            zhenduan.RemoveControlEvent("EventKeyPress");
            chuli.RemoveControlEvent("EventKeyPress");
            //移除病史、体检、诊断、处理左右键事件
            bingshi.RemoveControlEvent("EventKeyDown");
            tijian.RemoveControlEvent("EventKeyDown");
            zhenduan.RemoveControlEvent("EventKeyDown");
            chuli.RemoveControlEvent("EventKeyDown");
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
            saveChildCheck();
        }

        /// <summary>
        /// 保存儿童体检项目信息
        /// </summary>
        public bool saveChildCheck()
        {
            //if (String.IsNullOrEmpty(checkheight.Text.Trim()))
            //{
            //    MessageBox.Show("请填写儿童身高后再保存", "系统提示");
            //    checkheight.Focus();
            //    return;
            //}
            //if (String.IsNullOrEmpty(checkweight.Text.Trim()))
            //{
            //    MessageBox.Show("请选择儿童体重后再保存", "系统提示");
            //    checkweight.Focus();
            //    return;
            //}

            tb_childcheck checkboj = GetObj();
            
            //int precout = checkbll.countpre(checkboj.fuzenday, globalInfoClass.UserCode);
            //if (precout >= globalInfoClass.Pre_Max)
            //{
            //    MessageBox.Show(checkboj.fuzenday + "已预约" + precout + "人", "软件提示");
            //}

            if (string.IsNullOrEmpty(zhenduan.Text.Trim()))
            {
                this.zhenduan.Text = tijian.Text.Trim();
            }

            if (_checkobj == null)
            {
                checkboj.ck_fz = globalInfoClass.UserName;
                checkboj.hospital = _hospital;
                if (checkbll.Add(checkboj))
                {
                    checkobjForRef = checkboj;

                    //MessageBox.Show("保存成功！", "软件提示");
                    //new TbGaoweiBll().saveOrdeleteGaowei(textBoxX15.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));

                    RefreshcheckList();
                    //comboBox1_SelectedIndexChanged(comboBox1, new EventArgs());
                    

                    return true;
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            else
            {
                checkboj.ck_fz = _checkobj.ck_fz;
                checkboj.id = _checkobj.id;
                checkboj.hospital = _checkobj.hospital;
                if (checkbll.Update(checkboj))
                {
                    checkobjForRef = checkboj;
                    //MessageBox.Show("保存成功！", "软件提示");
                    //new TbGaoweiBll().saveOrdeleteGaowei(textBoxX15.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd"));
                    RefreshcheckList();
                    _womeninfo.bindDataNowdayTreeView();
                    return true;
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            

            return false;
        }

        /// <summary>
        /// 体检保存
        /// </summary>
        /// <returns></returns>
        private tb_childcheck GetObj()
        {
            StringBuilder builder = new StringBuilder();
            tb_childcheck obj = CommonHelper.GetObj<tb_childcheck>(panel2.Controls);
            if (string.IsNullOrEmpty(obj.zhenduan))
            {
                obj.zhenduan = "体检";
            }
            if (this.fuzencombobox.Text.Trim().Equals("1个月"))
            {
                obj.fuzenday = this.checkday.Value.AddMonths(1).ToString("yyyy-MM-dd");
            }
            else if (this.fuzencombobox.Text.Trim().Equals("2个月"))
            {
                obj.fuzenday = this.checkday.Value.AddMonths(2).ToString("yyyy-MM-dd");
            }
            else if (this.fuzencombobox.Text.Trim().Equals("3个月"))
            {
                obj.fuzenday = this.checkday.Value.AddMonths(3).ToString("yyyy-MM-dd");
            }
            else if (this.fuzencombobox.Text.Trim().Equals("6个月"))
            {
                obj.fuzenday = this.checkday.Value.AddMonths(6).ToString("yyyy-MM-dd");
            }
            else if (this.fuzencombobox.Text.Trim().Equals("1年"))
            {
                obj.fuzenday = this.checkday.Value.AddYears(1).ToString("yyyy-MM-dd");
            }

            obj.childid = _womeninfo.cd_id;
            obj.user_code = globalInfoClass.UserCode;
            return obj;
        }

        //BMI事件计算方法
        private void textBox28_Leave(object sender, EventArgs e)
        {
            getTigepingjia(checkheight.Text, checkweight.Text, checktouwei.Text);
            if (!string.IsNullOrEmpty(checkzuogao.Text.Trim()) && !string.IsNullOrEmpty(checkheight.Text.Trim()))
            {
                this.lbl_zuogao.Visible = true;
                double height = Convert.ToDouble(checkheight.Text.Trim());
                double zuogao = Convert.ToDouble(checkzuogao.Text.Trim());
                double bfb = zuogao / height;
                this.lbl_zuogao.Text = bfb.ToString("0.00");
            }
            else
            {
                this.lbl_zuogao.Visible = false;
            }
            if (!string.IsNullOrEmpty(checkheight.Text.Trim()))
            {
                GetHeightWeight0_2();
            }
        }

        /// <summary>
        /// 身长的体重
        /// </summary>
        private void GetHeightWeight0_2()
        {
            lblHeigthWeight.ForeColor = Color.Black;
            lblHeigthWeight.Text = "体重/身高评价：";

            int[] age = CommonHelper.getAgeBytime(baseobj.childbirthday, this.checkday.Value.ToString("yyyy-MM-dd"));
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            if (age[0] <= 2)
            {
                string sqls_stand = "select top 1 * from who_childstand where sex='" + baseobj.childgender + "' and CAST(length as float) <='" + checkheight.Text.Trim() + "' and  ptype='身高的体重0-2' order by CAST(length as float) desc";
                //string sqls_stand = "select * from who_childstand where sex='" + baseobj.childgender + "' and length='" + checkheight.Text.Trim() + "' and  ptype='身高的体重0-2'";
                ArrayList standlist = new ChildStandardBll().getwhoStandardlist(sqls_stand);

                if (standlist != null && standlist.Count > 0)
                {
                    WhoChildStandardDayObj shengaostandobj = standlist[0] as WhoChildStandardDayObj;
                    Double high = 0;
                    Double weight = 0;
                    Double hw = 0;
                    //double.TryParse(checkheight, out high) && double.TryParse(checkweight, out weight)

                    if (double.TryParse(checkweight.Text.Trim(), out weight))
                    {
                        hw = weight;
                        if (shengaostandobj != null)
                        {
                            string highcankao = "";
                            if (hw < Convert.ToDouble(shengaostandobj.p01))
                            {
                                //highcankao = "<P01";
                                highcankao = "<P1";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "下";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p1))
                            {
                                //highcankao = "P01-P1";
                                highcankao = "<P1";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "下";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p3))
                            {
                                highcankao = "P1-P3";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "下";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p5))
                            {
                                highcankao = "P3-P5";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "中下";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p10))
                            {
                                highcankao = "P5-P10";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "中下";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p15))
                            {
                                highcankao = "p10-P15";
                                updownindex[4] = "中下";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p25))
                            {
                                highcankao = "p15-P25";
                                updownindex[4] = "中下";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p50))
                            {
                                highcankao = "p25-P50";
                                updownindex[4] = "中";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p75))
                            {
                                highcankao = "p50-P75";
                                updownindex[4] = "中";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p85))
                            {
                                highcankao = "p75-P85";
                                updownindex[4] = "中上";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p90))
                            {
                                highcankao = "p85-P90";
                                updownindex[4] = "中上";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p95))
                            {
                                highcankao = "p90-P95";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "中上";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p97))
                            {
                                highcankao = "p95-P97";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "中上";
                            }
                            else if (hw < Convert.ToDouble(shengaostandobj.p99))
                            {
                                highcankao = "p97-P99";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "上";
                            }
                            else
                            {
                                //highcankao = ">P999";

                                highcankao = ">P99";
                                lblHeigthWeight.ForeColor = Color.Red;
                                updownindex[4] = "上";
                            }
                            lblHeigthWeight.Text = "体重/身高评价：" + highcankao;
                        }
                    }
                    else
                    {
                        lblHeigthWeight.Text = "体重/身高评价：";
                        updownindex[4] = "";
                    }
                }
            }
        }

        //BMI事件计算方法
        private void getTigepingjia(string checkheight, string checkweight, string checktouwei)
        {
            label52.ForeColor = Color.Black;
            label52.Text = "BMI：";
            label53.ForeColor = Color.Black;
            label53.Text = "BMI评价：";
            label59.ForeColor = Color.Black;
            label59.Text = "身高评价：";
            label58.ForeColor = Color.Black;
            label58.Text = "体重评价：";
            label60.ForeColor = Color.Black;
            label60.Text = "头围评价：";



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
                    lblHeigthWeight.ForeColor = Color.Black;
                    if (bmistandbmiobj != null)
                    {
                        string bmicankao = "";
                        if (BMI < Convert.ToDouble(bmistandbmiobj.p01))
                        {
                            //bmicankao = "<P01";
                            bmicankao = "<P1";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "下";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p1))
                        {
                            //bmicankao = "P01-P1";
                            bmicankao = "<P1";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "下";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p3))
                        {
                            bmicankao = "P1-P3";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "下";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p5))
                        {
                            bmicankao = "P3-P5";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "中下";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p10))
                        {
                            bmicankao = "P5-P10";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "中下";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p15))
                        {
                            bmicankao = "p10-P15";
                            updownindex[0] = "中下";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p25))
                        {
                            bmicankao = "p15-P25";
                            updownindex[0] = "中下";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p50))
                        {
                            bmicankao = "p25-P50";
                            updownindex[0] = "中";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p75))
                        {
                            bmicankao = "p50-P75";
                            updownindex[0] = "中";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p85))
                        {
                            bmicankao = "p75-P85";
                            updownindex[0] = "中上";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p90))
                        {
                            bmicankao = "p85-P90";
                            updownindex[0] = "中上";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p95))
                        {
                            bmicankao = "p90-P95";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "中上";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p97))
                        {
                            bmicankao = "p95-P97";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "中上";
                        }
                        else if (BMI < Convert.ToDouble(bmistandbmiobj.p99))
                        {
                            bmicankao = "p97-P99";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "上";
                        }
                        else
                        {
                            //bmicankao = ">P999";
                            bmicankao = ">P99";
                            label53.ForeColor = Color.Red;
                            updownindex[0] = "上";
                        }
                        label53.Text = "BMI评价：" + bmicankao;
                    }
                }
                else
                {
                    label53.Text = "BMI评价：";
                    updownindex[0] = "";
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
                        if (high < Convert.ToDouble(shengaostandobj.p01))
                        {
                            //highcankao = "<P01";
                            highcankao = "<P1";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "下";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p1))
                        {
                            //highcankao = "P01-P1";
                            highcankao = "<P1";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "下";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p3))
                        {
                            highcankao = "P1-P3";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "下";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p5))
                        {
                            highcankao = "P3-P5";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "中下";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p10))
                        {
                            highcankao = "P5-P10";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "中下";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p15))
                        {
                            highcankao = "p10-P15";
                            updownindex[1] = "中下";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p25))
                        {
                            highcankao = "p15-P25";
                            updownindex[1] = "中下";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p50))
                        {
                            highcankao = "p25-P50";
                            updownindex[1] = "中";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p75))
                        {
                            highcankao = "p50-P75";
                            updownindex[1] = "中";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p85))
                        {
                            highcankao = "p75-P85";
                            updownindex[1] = "中上";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p90))
                        {
                            highcankao = "p85-P90";
                            updownindex[1] = "中上";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p95))
                        {
                            highcankao = "p90-P95";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "中上";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p97))
                        {
                            highcankao = "p95-P97";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "中上";
                        }
                        else if (high < Convert.ToDouble(shengaostandobj.p99))
                        {
                            highcankao = "p97-P99";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "上";
                        }
                        else
                        {
                            //highcankao = ">P999";
                            highcankao = ">P99";
                            label59.ForeColor = Color.Red;
                            updownindex[1] = "上";
                        }

                        label59.Text = "身高评价：" + highcankao;

                    }
                }
                else
                {
                    label59.Text = "身高评价：";
                    updownindex[1] = "";
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
                        if (weight < Convert.ToDouble(tizhongstandobj.p01))
                        {
                            //weightcankao = "<P01";
                            weightcankao = "<P1";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "下";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p1))
                        {
                            //weightcankao = "P01-P1";
                            weightcankao = "<P1";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "下";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p3))
                        {
                            weightcankao = "P1-P3";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "下";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p5))
                        {
                            weightcankao = "P3-P5";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "中下";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p10))
                        {
                            weightcankao = "P5-P10";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "中下";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p15))
                        {
                            weightcankao = "p10-P15";
                            updownindex[3] = "中下";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p25))
                        {
                            weightcankao = "p15-P25";
                            updownindex[3] = "中下";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p50))
                        {
                            weightcankao = "p25-P50";
                            updownindex[3] = "中";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p75))
                        {
                            weightcankao = "p50-P75";
                            updownindex[3] = "中";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p85))
                        {
                            weightcankao = "p75-P85";
                            updownindex[3] = "中上";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p90))
                        {
                            weightcankao = "p85-P90";
                            updownindex[3] = "中上";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p95))
                        {
                            weightcankao = "p90-P95";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "中上";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p97))
                        {
                            weightcankao = "p95-P97";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "中上";
                        }
                        else if (weight < Convert.ToDouble(tizhongstandobj.p99))
                        {
                            weightcankao = "p97-P99";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "上";
                        }
                        else
                        {
                            //weightcankao = ">P999";
                            weightcankao = ">P99";
                            label58.ForeColor = Color.Red;
                            updownindex[3] = "上";
                        }

                        label58.Text = "体重评价：" + weightcankao;
                    }
                }
                else
                {
                    label58.Text = "体重评价：";
                    updownindex[3] = "";
                }
            }


            if (!String.IsNullOrEmpty(checktouwei))
            {
                Double touwei = 0;
                if (double.TryParse(checktouwei, out touwei))
                {
                    if (touweistandobj != null)
                    {
                        string touweicankao = "";
                        if (touwei < Convert.ToDouble(touweistandobj.p01))
                        {
                            //touweicankao = "<P01";
                            touweicankao = "<P1";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "下";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p1))
                        {
                            //touweicankao = "P01-P1";
                            touweicankao = "<P1";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "下";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p3))
                        {
                            touweicankao = "P1-P3";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "下";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p5))
                        {
                            touweicankao = "P3-P5";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "中下";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p10))
                        {
                            touweicankao = "P5-P10";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "中下";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p15))
                        {
                            touweicankao = "p10-P15";
                            updownindex[2] = "中下";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p25))
                        {
                            touweicankao = "p15-P25";
                            updownindex[2] = "中下";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p50))
                        {
                            touweicankao = "p25-P50";
                            updownindex[2] = "中";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p75))
                        {
                            touweicankao = "p50-P75";
                            updownindex[2] = "中";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p85))
                        {
                            touweicankao = "p75-P85";
                            updownindex[2] = "中上";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p90))
                        {
                            touweicankao = "p85-P90";
                            updownindex[2] = "中上";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p95))
                        {
                            touweicankao = "p90-P95";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "中上";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p97))
                        {
                            touweicankao = "p95-P97";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "中上";
                        }
                        else if (touwei < Convert.ToDouble(touweistandobj.p99))
                        {
                            touweicankao = "p97-P99";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "上";
                        }
                        else
                        {
                            //touweicankao = ">P999";
                            touweicankao = ">P99";
                            label60.ForeColor = Color.Red;
                            updownindex[2] = "上";
                        }
                        label60.Text = "头围评价：" + touweicankao;
                    }
                }
                else
                {
                    label60.Text = "头围评价：";
                    updownindex[2] = "";
                }
            }
        }

        //BMI标准
        private void getBmistand(string checkday)
        {
            string sqls_stand = "";
            int[] ages = CommonHelper.getAgeBytime(baseobj.childbirthday, _checkobj?.checkday ?? comboBox1.Text);
            int yf = ages[0] * 12 + ages[1];

//            if (yf < 61)
//            {
//                sqls_stand = "select * from who_childstand_day where sex='" + baseobj.childgender + "' and tian=datediff(day,'" + baseobj.childbirthday + "','" + checkday + "')";
//            }
//            else
//            {
                sqls_stand = "select * from who_childstand where sex='" + baseobj.childgender + "' and month ='" + yf + "'"; ;
//            }

            ArrayList standlist = new ChildStandardBll().getwhoStandardlist(sqls_stand);
            if (standlist != null && standlist.Count > 0)
            {
                foreach (WhoChildStandardDayObj whostandobj in standlist)
                {
                    if (whostandobj.ptype.Contains("BMI"))
                    {
                        bmistandbmiobj = whostandobj;
                    }
                    else if (whostandobj.ptype == "体重")
                    {
                        tizhongstandobj = whostandobj;
                    }
                    else if (whostandobj.ptype == "身高")
                    {
                        shengaostandobj = whostandobj;
                    }
                    else if (whostandobj.ptype.Contains("头围"))
                    {
                        touweistandobj = whostandobj;
                    }
                }
            }

            getTigepingjia(checkheight.Text, checkweight.Text, checktouwei.Text);
        }

        //指导等
        private void getzhidaostand(string checkday)
        {
            string sqls_zhidao = "select * from tb_moban where  yf<=datediff(month,'" + baseobj.childbirthday + "','" + checkday + "') and yfend>=datediff(month,'" + baseobj.childbirthday + "','" + checkday + "') ";
            ArrayList mobanlist = new MobanManageBll().getMobanList(sqls_zhidao);
            if (mobanlist != null && mobanlist.Count > 0)
            {
                foreach (mobanManageObj mobanobj in mobanlist)
                {
                    //if (mobanobj.m_type == "营养指导")
                    //    txtyingyangzhidao.Text = mobanobj.m_content;
                    //if (mobanobj.m_type == "行为习惯培养")
                    //    txt_checkdiagonse.Text = mobanobj.m_content;
                    //if (mobanobj.m_type == "早期综合发展")
                    //    txtzaoqifazhan.Text = mobanobj.m_content;
                }
            }
        }
        /// <summary>
        /// 刷新体检界面
        /// </summary>
        private void RefreshcheckCode(int id)
        {
            //评价分等级  上、中上、中、中下、下
            // updownindex[0] BMI
            // updownindex[1] 身高
            // updownindex[2] 头围
            // updownindex[3] 体重
            // updownindex[4] 体重/身高
            updownindex = new string[5];
            label52.Text = "BMI：";
            label53.Text = "BMI评价：";
            label59.Text = "身高评价：";
            label58.Text = "体重评价：";
            label60.Text = "头围评价：";
            lblHeigthWeight.Text = "体重/身高评价：";

            label52.ForeColor = Color.Black;
            label53.ForeColor = Color.Black;
            label59.ForeColor = Color.Black;
            label58.ForeColor = Color.Black;
            label60.ForeColor = Color.Black;
            lblHeigthWeight.ForeColor = Color.Black;

            _checkobj = checkbll.Get(id);
            if (_checkobj != null)
            {
                 if (_checkobj.zhenduan != null)
                {
                    CommonHelper.setForm(_checkobj, panel2.Controls);
                }
                else
                {
                    SetControlsDefaultValue();
                    var obj = new tb_childcheck { checkweight = _checkobj.checkweight, checkheight = _checkobj.checkheight, checktouwei = _checkobj.checktouwei, checkzuogao = _checkobj.checkzuogao };
                    //CommonHelper.setForm(obj, panel2.Controls);
                    checkweight.Text = obj.checkweight;
                    checkheight.Text = obj.checkheight;
                    checktouwei.Text = obj.checktouwei;
                    checkzuogao.Text = obj.checkzuogao;

                }

                if (String.IsNullOrEmpty(_checkobj.doctorname))
                {
                    doctorname.Text = globalInfoClass.UserName;
                }
                getBmistand(this.checkday.Value.ToString("yyyy-MM-dd"));
                GetHeightWeight0_2();
            }
            else
            {
                SetControlsDefaultValue();
            }
			this.panel2.Focus();
        }

        /// <summary>
        /// 清空界面信息
        /// </summary>
        private void ClearPanel()
        {
            label52.Text = "BMI：";
            label53.Text = "BMI评价：";
            label59.Text = "身高评价：";
            label58.Text = "体重评价：";
            label60.Text = "头围评价：";
            lblHeigthWeight.Text = "体重/身高评价：";

            label52.ForeColor = Color.Black;
            label53.ForeColor = Color.Black;
            label59.ForeColor = Color.Black;
            label58.ForeColor = Color.Black;
            label60.ForeColor = Color.Black;
            lblHeigthWeight.ForeColor = Color.Black;

            this.checkday.Value = DateTime.Now;
            this.bloodsesu.Text = "";
            //cbx_doctorName.Text = globalInfoClass.UserName;

            //
            this.checkheight.Text = "";
            this.checkweight.Text = "";

            this.checktouwei.Text = "";
            this.checkzuogao.Text = "";
            this.zhushi.Text = "";
            this.fushi.Text = "";
            this.doctorname.Text = "";
            this.vitd.Text = "400IU/天";
            this.otherbingshi.Text = "";
            this.shehui.Text = "正常";
            this.wuguan.Text = "正常";
            this.checkbi.Text = "正常";
            this.dongzuo.Text = "正常";
            this.skin.Text = "正常";
            this.jizhu.Text = "未见畸形";
            this.laguage.Text = "正常";
            this.lingbajie.Text = "正常";
            this.bigsport.Text = "正常";
            this.xiongbu.Text = "正常";
            this.sizhi.Text = "正常";
            this.checkqianlu.Text = "";
            this.xinzang.Text = "正常";
            this.toulu.Text = "正常";
            this.feibu.Text = "正常";
            this.kouqiang.Text = "正常";
            this.yacinumber.Text = "";
            this.yucinumber.Text = "";
            this.fubu.Text = "正常";
            this.ganzang.Text = "正常";
            this.miniaoqi.Text = "正常";

            this.gouloubing.Text = "无";

            //复诊日期

            this.zhusu.Text = "";
            this.bingshi.Text = "";
            this.tijian.Text = "";
            this.zhenduan.Text = "";
            this.chuli.Text = "";

        }

        private void SetControlsDefaultValue()
        {
            label52.Text = "BMI：";
            label53.Text = "BMI评价：";
            label59.Text = "身高评价：";
            label58.Text = "体重评价：";
            label60.Text = "头围评价：";
            lblHeigthWeight.Text = "体重/身高评价：";
            //_checkobj.ChildId = globalInfoClass.Wm_Index;
            //_checkobj.CheckAge = this.txt_checkAge.Text.Trim();
            //月份
            //DateTime dtbirth = Convert.ToDateTime(obj.ChildBirthDay);
            //DateTime dtnow = DateTime.Now;
            //TimeSpan timeSpan = dtnow - dtbirth;
            int[] age = CommonHelper.getAgeBytime(baseobj.childbirthday, this.checkday.Value.ToString("yyyy-MM-dd"));
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            this.checkfactage.Text = agestr;



            this.checkday.Value = DateTime.Now;
            this.bloodsesu.Text = "";
            //cbx_doctorName.Text = globalInfoClass.UserName;

            //
            this.checkheight.Text = "";
            this.checkweight.Text = "";

            this.checktouwei.Text = "";
            this.checkzuogao.Text = "";
            if (age != null)
            {
                if (age[0] < 1)
                {
                    this.zhushi.Items.Clear();
                    this.zhushi.Items.Add("纯母乳");
                    this.zhushi.Items.Add("部分母乳");
                    this.zhushi.Items.Add("人工喂养");
                    if((age[0] * 12 + age[1])<6){
                    	this.zhushi.Text = "纯母乳";
                    }else{
                    	this.zhushi.Text = "部分母乳";
                    }
                }
                else
                {
                    this.zhushi.Items.Clear();
                    this.zhushi.Text = "人工喂养";
                    if (age[0]<5)
                    {
                        this.zhushi.Items.Add("部分母乳");
                        this.zhushi.Items.Add("人工喂养");
                    }else
                    {
                        this.zhushi.Items.Add("人工喂养");
                    }
                }
            }
            else
            {
                this.zhushi.Text = "";
            }
            this.fushi.Text = "";

            this.vitd.Text = "400IU/天";
            this.otherbingshi.Text = "";
            this.shehui.Text = "正常";
            this.wuguan.Text = "正常";
            this.checkbi.Text = "正常";
            this.dongzuo.Text = "正常";
            this.skin.Text = "正常";
            this.jizhu.Text = "未见畸形";
            this.laguage.Text = "正常";
            this.lingbajie.Text = "正常";
            this.bigsport.Text = "正常";
            this.xiongbu.Text = "正常";
            this.sizhi.Text = "正常";
            this.checkqianlu.Text = "";
            this.xinzang.Text = "正常";
            this.toulu.Text = "正常";
            this.feibu.Text = "正常";
            this.kouqiang.Text = "正常";
            this.yacinumber.Text = "";
            this.yucinumber.Text = "";
            this.fubu.Text = "正常";
            this.ganzang.Text = "正常";
            this.miniaoqi.Text = "正常";

            this.gouloubing.Text = "无";

            //复诊日期

            this.zhusu.Text = "";
            this.bingshi.Text = "";
            this.tijian.Text = "";
            this.zhenduan.Text = "";
            this.chuli.Text = "";

            //textBoxX15.Text = new TbGaoweiBll().getGaoweistr(_womeninfo.cd_id, "", "");

            getzhidaostand(this.checkday.Value.ToString("yyyy-MM-dd"));

            int totalmonth = age[0] * 12 + age[1];



            //getcheckmoban(totalmonth);

            getBmistand(this.checkday.Value.ToString("yyyy-MM-dd"));
            
        }

        /// <summary>
        /// 刷新体检列表
        /// </summary>
        public void RefreshcheckList()
        {
            _listcheck = checkbll.GetList(_womeninfo.cd_id);
            comboBox1.DisplayMember = "checkday";
            comboBox1.ValueMember = "id";
            comboBox1.DataSource = _listcheck;
            if (_listcheck != null && _listcheck.Count > 0)
            {
                if (checkobjForRef != null)
                    comboBox1.SelectedValue = checkobjForRef.id;
                else
                {
                    var date = (comboBox1.Items[0] as tb_childcheck).checkday;
                    foreach (tb_childcheck item in comboBox1.Items)
                    {
                        if (item.checkday != date)
                        {
                            break;
                        }
                        if (item.zhenduan == null)
                        {
                            comboBox1.SelectedValue = item.id;
                            break;
                        }
                    }
                    //comboBox1.SelectedIndex = _listcheck.Count - 1;
                }
            }
            else
            {
                ClearPanel();
                comboBox1.DataSource = _listcheck;
                comboBox1.Text = "";
            }
            this.panel2.Focus();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrinter_Click(object sender, EventArgs e)
        {
            printtijianjilu(false);
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPriview_Click(object sender, EventArgs e)
        {
            printtijianjilu(true);
        }

        private void printtijianjilu(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!saveChildCheck())
            {
                return;
            }


            if (_womeninfo.cd_id == -1)
            {
                MessageBox.Show("请保存儿童信息之后再预览打印！", "软件提示");
                return;
            }
            try
            {
                if (baseobj == null)
                {
                    MessageBox.Show("请保存儿童信息之后再打印！", "软件提示");
                    return;
                }
                else
                {
                    if (_checkobj == null)
                    {
                        MessageBox.Show("请选择儿童体检信息后再打印！", "软件提示");
                        return;
                    }

                    int[] ages = CommonHelper.getAgeBytime(baseobj.childbirthday, _checkobj.checkday);
                    int yf = ages[0] * 12 + ages[1];
                    mb_zd zdobj = new mb_zdbll().GetByYf(yf);
                    mb_wy wyobj = new mb_wybll().GetByYfandLx(yf, _checkobj.zhushi);
                    string[] bmipingfen = new string[] {
                        label52.Text,label53.Text ,
                        "身高：," + _checkobj.checkheight + "cm," + label59.Text.Remove(0,5),
                        "体重：," + _checkobj.checkweight + "kg," + label58.Text.Remove(0,5),
                        "头围：," + _checkobj.checktouwei + "cm," + label60.Text.Remove(0,5),
                        lblHeigthWeight.Text.Trim(),
                        "坐高：," + (_checkobj.checkzuogao ?? "    ") + "cm",
                        //评价分等级  上、中上、中、中下、下
				        // updownindex[0] BMI
				        // updownindex[1] 身高
				        // updownindex[2] 头围
				        // updownindex[3] 体重
				        // updownindex[4] 体重/身高
				        updownindex[1]+","+updownindex[3]+","+updownindex[2]+","+updownindex[0]+","+updownindex[4]
                    };
                    PaneljibenCheckBingliPrinter1 childcheckPrinter = new PaneljibenCheckBingliPrinter1(baseobj, _checkobj, bmipingfen, zdobj, wyobj);
                    //panelrecord.Patient = _patient;
                    childcheckPrinter.Print(ispre);
                    //}
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        //private void printtijianjilu(bool ispre)
        //{
        //    Cursor.Current = Cursors.WaitCursor;
        //    if (_womeninfo.cd_id == -1)
        //    {
        //        MessageBox.Show("请保存儿童信息之后再预览打印！", "软件提示");
        //        return;
        //    }
        //    try
        //    {
        //        if (baseobj == null)
        //        {
        //            MessageBox.Show("请保存儿童信息之后再打印！", "软件提示");
        //            return;
        //        }
        //        else
        //        {
        //            if (_checkobj == null)
        //            {
        //                MessageBox.Show("请选择儿童体检信息后再打印！", "软件提示");
        //                return;
        //            }

        //            string[] bmipingfen = new string[] { label52.Text + "  " + label53.Text + "  " + label59.Text , label58.Text + "  " + label60.Text };
        //            PaneljibenChildCheckPrinter childcheckPrinter = new PaneljibenChildCheckPrinter(baseobj, _checkobj, _womeninfo.cd_id, bmipingfen);
        //                //panelrecord.Patient = _patient;
        //                childcheckPrinter.Print(true);
        //            //}
        //        }
        //    }
        //    finally
        //    {
        //        Cursor.Current = Cursors.Default;
        //    }
        //}

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (_checkobj != null)
            {
                if (MessageBox.Show("删除该儿童本次体检记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {

                        if (checkbll.Delete(_checkobj.id))
                        {

                            MessageBox.Show("删除成功!", "软件提示");
                            _checkobj = null;
                            RefreshcheckList();
                        }
                        else
                        {
                            MessageBox.Show("删除失败!", "请联系管理员");
                        }


                    }
                    catch (Exception ex)
                    {
                        throw ex;
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


        private void dtp_checkDay_Leave(object sender, EventArgs e)
        {
            getBmistand(this.checkday.Value.ToString("yyyy-MM-dd"));
            getzhidaostand(this.checkday.Value.ToString("yyyy-MM-dd"));


            int[] age = CommonHelper.getAgeBytime(baseobj.childbirthday, checkday.Value.ToString("yyyy-MM-dd"));
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            checkfactage.Text = agestr;
            int totalmonth = age[0] * 12 + age[1];

            getcheckmoban(totalmonth);
        }

        private void buttonX7_Click_1(object sender, EventArgs e)
        {
            getzhidaostand(this.checkday.Value.ToString("yyyy-MM-dd"));
        }

        private void getcheckmoban(int yf)
        {
            string sqls = "select * from tb_childcheck_moban where yfks<=" + yf + " and  yfjs>=" + yf;
            checkmobanobj = new ChildCheckBll().getChildCheckobj(sqls);
            if (checkmobanobj != null)
            {
                this.shehui.Text = checkmobanobj.shehui;
                this.dongzuo.Text = checkmobanobj.dongzuo;
                this.laguage.Text = checkmobanobj.Laguage;
                this.bigsport.Text = checkmobanobj.BigSport;
            }
        }

        private void dtp_checkDay_ValueChanged(object sender, EventArgs e)
        {
            int[] age = CommonHelper.getAgeBytime(baseobj.childbirthday, checkday.Value.ToString("yyyy-MM-dd"));
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            checkfactage.Text = agestr;
            int totalmonth = age[0] * 12 + age[1];
            DateTime dtfucha = DateTime.Now;
            if (totalmonth < 6)
            {
                dtfucha = this.checkday.Value.AddMonths(1);
                this.fuzencombobox.SelectedIndex=0;
            }
            else if (totalmonth < 12)
            {
            	this.fuzencombobox.SelectedIndex=1;
            }
            else if (totalmonth < 24)
            {
            	this.fuzencombobox.SelectedIndex=2;
            }
            else if (totalmonth < 36)
            {
            	this.fuzencombobox.SelectedIndex=3;
            }
            else
            {
            	this.fuzencombobox.SelectedIndex=4;
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

        private void textBoxX25_TextChanged(object sender, EventArgs e)
        {
            if (_checkobj == null || String.IsNullOrEmpty(_checkobj.doctorname))
            {
                if (String.IsNullOrEmpty(zhushi.Text.Trim()))
                {
                    doctorname.Text = "";
                }
                else
                {
                    doctorname.Text = globalInfoClass.UserName;
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

        private void buttonX12_Click(object sender, EventArgs e)
        {

            tb_healthcheck_moban healthmoban = new tb_healthcheck_moban();
            healthmoban.zhusu = this.zhusu.Text.Trim();
            healthmoban.bingshi = this.bingshi.Text.Trim();
            healthmoban.tijian = this.tijian.Text.Trim();
            healthmoban.zhenduan = this.zhenduan.Text.Trim();
            healthmoban.chuli = this.chuli.Text.Trim();
            Panel_moban_save mobansave = new Panel_moban_save(healthmoban);
            mobansave.ShowDialog();
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            Panel_moban_list mobanlist = new Panel_moban_list();
            mobanlist.ShowDialog();
            if (mobanlist.DialogResult == DialogResult.OK)
            {
                tb_healthcheck_moban healthmoban = mobanlist._healthcheckobj;
                this.zhusu.Text = healthmoban.zhusu;
                this.bingshi.Text = healthmoban.bingshi;
                this.tijian.Text = healthmoban.tijian;
                this.zhenduan.Text = healthmoban.zhenduan;
                this.chuli.Text = healthmoban.chuli;
            }
        }

        //病历打印
        private void printbingli(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (!saveChildCheck())
            {
                return;
            }

            try
            {
                if (baseobj == null)
                {
                    MessageBox.Show("请保存儿童信息之后再打印！", "软件提示");
                    return;
                }
                else
                {
                    _checkobj = checkbll.Get(_checkobj.id);
                    if (_checkobj == null)
                    {
                        MessageBox.Show("请选择儿童体检信息后再打印！", "软件提示");
                        return;
                    }
                    tb_childcheck checkobj = _checkobj;
                    PaneljibenCheckBingliPrinter checkbingliPrinter = new PaneljibenCheckBingliPrinter(baseobj, checkobj, _womeninfo.cd_id);
                    //panelrecord.Patient = _patient;
                    checkbingliPrinter.Print(ispre);
                    //}
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            printbingli(true);
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            printbingli(false);
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            if (checkmobanobj != null)
            {
                string shehui = this.shehui.Text.Trim();
                if (shehui != checkmobanobj.shehui)
                {
                    this.shehui.ForeColor = Color.Red;
                }
                else
                {
                    this.shehui.ForeColor = Color.Black;
                }
            }
        }

        private void comboBox20_TextChanged(object sender, EventArgs e)
        {
            if (checkmobanobj != null)
            {
                string dongzuo = this.dongzuo.Text.Trim();
                if (dongzuo != checkmobanobj.dongzuo)
                {
                    this.dongzuo.ForeColor = Color.Red;
                }
                else
                {
                    this.dongzuo.ForeColor = Color.Black;
                }
            }
        }

        private void txt_laguage_TextChanged(object sender, EventArgs e)
        {
            if (checkmobanobj != null)
            {
                string yuyan = laguage.Text.Trim();
                if (yuyan != checkmobanobj.Laguage)
                {
                    laguage.ForeColor = Color.Red;
                }
                else
                {
                    laguage.ForeColor = Color.Black;
                }
            }
        }

        private void cbx_bigSport_TextChanged(object sender, EventArgs e)
        {
            string bigSport = bigsport.Text.Trim();
            if (checkmobanobj != null)
            {
                if (bigSport != checkmobanobj.BigSport)
                {
                    bigsport.ForeColor = Color.Red;
                }
                else
                {
                    bigsport.ForeColor = Color.Black;
                }
            }
        }

        private void tijian_panel_Load(object sender, EventArgs e)
        {
            int[] age = CommonHelper.getAgeBytime(baseobj.childbirthday, this.checkday.Value.ToString("yyyy-MM-dd"));


            if (age != null)
            {
                if (age[0] < 1)
                {
                    this.zhushi.Items.Clear();
                    this.zhushi.Items.Add("纯母乳");
                    this.zhushi.Items.Add("部分母乳");
                    this.zhushi.Items.Add("人工喂养");
                    if((age[0] * 12 + age[1])<6){
                    	this.zhushi.Text = "纯母乳";
                    }else{
                    	this.zhushi.Text = "部分母乳";
                    }
                }
                else
                {
                    this.zhushi.Items.Clear();
                    this.zhushi.Text = "人工喂养";
                    if (age[0] < 5)
                    {
                        this.zhushi.Items.Add("部分母乳");
                        this.zhushi.Items.Add("人工喂养");
                    }
                    else
                    {
                        this.zhushi.Items.Add("人工喂养");
                    }
                }
            }
            else
            {
                this.zhushi.Text = "";
            }


            RefreshcheckList();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int checkid = -1;
            //MessageBox.Show(comboBox1.SelectedValue.ToString()+"    /检查ID");
            if (Int32.TryParse(comboBox1.SelectedValue?.ToString(), out checkid))
            {
                RefreshcheckCode(checkid);
            }
            if (comboBox1.SelectedValue is tb_childcheck)
            {
                checkid = (comboBox1.SelectedValue as tb_childcheck).id;
                RefreshcheckCode(checkid);
            }

        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (_checkobj != null)
            {
                tb_childcheck che = new tb_childcheck();
                che.checkday = DateTime.Now.ToString("yyyy-MM-dd");
                _listcheck.Add(che);
                comboBox1.DataSource = null;//数据源先置空，否则同一个对象不会刷新
                comboBox1.DisplayMember = "checkday";
                comboBox1.ValueMember = "id";
                comboBox1.DataSource = _listcheck;
                comboBox1.SelectedIndex = _listcheck.Count - 1;
            }
            //RefreshcheckCode(-1);
        }
		void Panel2MouseEnter(object sender, EventArgs e)
		{
			this.panel2.Focus();
		}
    }
}
