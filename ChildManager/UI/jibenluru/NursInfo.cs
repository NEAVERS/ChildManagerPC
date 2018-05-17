using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YCF.Model;
using YCF.BLL;
using YCF.Common;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.BLL;
using System.Collections;
using ChildManager.Model;

namespace ChildManager.UI.jibenluru
{
    public partial class NursInfo : UserControl
    {
        hisRegistBll registbll = new hisRegistBll();
        tb_childbasebll jibenbll = new tb_childbasebll();
        tb_childcheckbll checkbll = new tb_childcheckbll();
        IDCardValidation validation = new IDCardValidation();
        tb_childbase _jibenobj = null;
        public NursInfo()
        {
            InitializeComponent();
        }

        //主窗体加载时自动打开基本信息的一般信息窗口
        private void WomenInfo_Load(object sender, EventArgs e)
        {
            bindDataNowday();//绑定左边列表数据
            RefreshCheckList();
        }

        private void RefreshCheckList()
        {
            if (_jibenobj != null)
            {
                IList<tb_childcheck> checklist = checkbll.GetList(_jibenobj.id);
                if (checklist != null)
                {
                    foreach (tb_childcheck checkobj in checklist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView2, checkobj.checkday, checkobj.checkweight, checkobj.checkheight, checkobj.checktouwei, checkobj.checkzuogao, checkobj.doctorname);
                        row.Tag = checkobj;
                        dataGridView2.Rows.Add(row);
                        if (checkobj.checkday == checkday.Text)
                        {
                            row.Selected = true;
                        }
                    }
                }
            }
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

            hisRegistObj registobj = this.dataGridView1.SelectedRows[0].Tag as hisRegistObj;//就诊号
            if(registobj==null)
            {
                return;
            }
            _jibenobj = jibenbll.GetByCardNo(registobj.cardno);
            setNursForm();
            if (_jibenobj == null)
            {
                jiuzhencardno.Text = registobj.cardno;
                childname.Text = registobj.cardno;
                childgender.Text = registobj.sex;
                childbirthday.Value = Convert.ToDateTime(registobj.sex);

            }

            Cursor.Current = Cursors.WaitCursor;

        }


        /// <summary>
        /// 绑定列表建档信息
        /// ywj
        /// 2016.8.29
        /// </summary>
        public void bindDataNowday()
        {
            //dataGridView1.Rows.Clear();
            //string starttime = DateTime.Now.ToString("yyyy-MM-dd")+" 00:00:00";
            //string endtime = DateTime.Now.ToString("yyyy-MM-dd")+" 23:59:59";
            //string istijian = CommonHelper.getcheckedValue(panel1);
            ////string sqls = "select * from v_yyt_registration where regist_time>='"+starttime+ "' and regist_time<='" + endtime + "'";
            //string sqls = "select * from v_yyt_registration ";
            //ArrayList list = registbll.getRegistList(sqls);
            //try
            //{
            //    if(list!=null)
            //    {
            //        foreach (hisRegistObj obj in list)
            //        {
            //            DataGridViewRow row = new DataGridViewRow();
            //            row.CreateCells(dataGridView1,obj.cardno,obj.patient_name);
            //            row.Tag = obj;
            //            dataGridView1.Rows.Add(row);
            //        }
            //    }

            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{

            //}
        }

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
                tb_childbase jibenobj = frmsearcher.returnval;
                if (jibenobj != null)
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

                }

            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            Paneltsb_searchycfInfo search = new Paneltsb_searchycfInfo();
            if (search.ShowDialog() == DialogResult.OK)
            {
                RefreshCode1(search.returnval);
            }
        }

        /// <summary>
        /// 本院信息导入
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode1(tb_childbase obj)
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
                bloodtype.Text = obj.bloodtype;//血型
                DateTime dtbirth = DateTime.Now;
                DateTime.TryParse(obj.childbirthday, out dtbirth);
                childbirthday.Value = dtbirth;
                childbuildday.Value = DateTime.Now;
                childbuildhospital.Text = "重医大附属儿童医院";//建册医院
                                                      //存图片
                                                      //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
                                                      //父亲母亲
                fathername.Text = obj.fathername;
                fatherage.Text = obj.fatherage;
                fatherheight.Text = obj.fatherheight;
                fathereducation.Text = obj.fathereducation;
                fatherjob.Text = obj.fatherjob;
                mothername.Text = obj.mothername;
                motherage.Text = obj.motherage;
                motherheight.Text = obj.motherheight;
                mothereducation.Text = obj.mothereducation;
                motherjob.Text = obj.motherjob;
                telephone.Text = obj.telephone;
                province.Text = obj.province;
                city.Text = obj.city;
                area.Text = obj.area;
                address.Text = obj.address;
                nurseryinstitution.Text = obj.nurseryinstitution;

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
                perineumincision.Text = obj.perineumincision;
                //obj.FetusNumber = cbx_fetusNumber.Text = ;
                neonatalcondition.Text = obj.neonatalcondition;
                birthweight.Text = obj.birthweight;
                birthheight.Text = obj.birthheight;
                birthaddress.Text = obj.birthaddress;
                //母乳喂养
                hospitalizedstates.Text = "纯母乳";
                onemonth.Text = "纯母乳";
                infourmonth.Text = "纯母乳";
                fourtosixmonth.Text = "纯母乳";
                //obj.Yunqi = getcheckedValue(pnl_yunqi);
                identityno.Text = obj.identityno;
            }
            else
            {
                healthcardno.Text = "";//保健卡号
                childname.Text = "";//姓名
                childgender.Text = "";//性别
                bloodtype.Text = "";//血型
                childbirthday.Value = DateTime.Now;
                childbuildday.Value = DateTime.Now;
                childbuildhospital.Text = "重医大附属儿童医院";//建册医院
                //存图片
                //byte[] imageBytes = bll.GetImageBytes(this.pictureBox1.Image);//转换图片为二进制
                //父亲母亲
                fathername.Text = "";
                fatherage.Text = "";
                fatherheight.Text = "";
                fathereducation.Text = "";
                fatherjob.Text = "";
                mothername.Text = "";
                motherage.Text = "";
                motherheight.Text = "";
                mothereducation.Text = "";
                motherjob.Text = "";
                telephone.Text = "";
                address.Text = "";
                nurseryinstitution.Text = "";

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
                perineumincision.Text = "";
                //obj.FetusNumber = cbx_fetusNumber.Text = ;
                neonatalcondition.Text = "";
                birthweight.Text = "";
                birthheight.Text = "";
                birthaddress.Text = "";
                //母乳喂养
                hospitalizedstates.Text = "纯母乳";
                onemonth.Text = "纯母乳";
                infourmonth.Text = "纯母乳";
                fourtosixmonth.Text = "纯母乳";
                //obj.Yunqi = getcheckedValue(pnl_yunqi);
                identityno.Text = "";
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        private tb_childbase getChildBaseInfoObj()
        {
            tb_childbase obj = CommonHelper.GetObj<tb_childbase>(panel2.Controls);
            obj.status = "1";
            return obj;
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
            if (String.IsNullOrEmpty(childbuildhospital.Text.Trim()))
            {
                MessageUtil.ShowTips("建册医院不能为空!");
                childbuildhospital.Focus();
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
                b = jibenbll.Update(obj);
                if (b)
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    _jibenobj = obj;
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
                    _jibenobj = obj;
                    this.healthcardno.Text = obj.healthcardno;
                    MessageBox.Show("保存成功！", "软件提示");
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {

        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            saveChildCheck();
        }

        /// <summary>
        /// 保存儿童体检项目信息
        /// </summary>
        public void saveChildCheck()
        {
            if (_jibenobj == null)
            {
                MessageBox.Show("请先保存儿童基本信息", "系统提示");
                return;
            }
            if (String.IsNullOrEmpty(checkheight.Text.Trim()))
            {
                MessageBox.Show("请填写儿童身高后再保存", "系统提示");
                checkheight.Focus();
                return;
            }
            if (String.IsNullOrEmpty(checkweight.Text.Trim()))
            {
                MessageBox.Show("请选择儿童体重后再保存", "系统提示");
                checkweight.Focus();
                return;
            }

            tb_childcheck checkobj = GetObj();
            tb_childcheck _checkobj = checkbll.Get(checkobj.checkday, checkobj.childid);
            if (_checkobj == null)
            {
                if (checkbll.Add(checkobj))
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    _checkobj = checkobj;
                    RefreshCheckList();
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
                    MessageBox.Show("保存成功！", "软件提示");
                    _checkobj = checkobj;
                    RefreshCheckList();
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
            tb_childcheck obj = CommonHelper.GetObj<tb_childcheck>(panel2.Controls);
            obj.childid = _jibenobj.id;
            return obj;
        }

        private void textBoxX2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                _jibenobj = jibenbll.GetByHealthNo(healthcardno.Text);
                if (_jibenobj != null)
                    setNursForm();
                else
                    MessageBox.Show("无儿童信息！");
            }
        }

        private void setNursForm()
        {
            if (_jibenobj != null)
            {
                CommonHelper.setForm(_jibenobj, panel2.Controls);
                IList<tb_childcheck> checklist = checkbll.GetList(_jibenobj.id);
                if (checklist != null)
                {
                    dataGridView2.Rows.Clear();
                    int rowindex = -1;
                    for (int i=0;i<checklist.Count;i++)
                    {
                        tb_childcheck checkobj = checklist[i];
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView2, checkobj.checkday, checkobj.checkweight, checkobj.checkheight, checkobj.checktouwei, checkobj.checkzuogao, checkobj.doctorname);
                        row.Tag = checkobj;
                        dataGridView2.Rows.Add(row);
                        if (checkobj.checkday == checkday.Text.Trim())
                        {
                            rowindex = i;
                        }
                    }
                    if (rowindex == -1)
                    {
                        dataGridView2.ClearSelection();
                    }
                    else
                    {
                        dataGridView2.Rows[rowindex].Selected = true;
                        dataGridView2_RowEnter(null, new DataGridViewCellEventArgs(1, rowindex));
                    }
                }
            }
        }

        private void jiuzhencardno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                _jibenobj = jibenbll.GetByCardNo(jiuzhencardno.Text);
                if (_jibenobj != null)
                    setNursForm();
                else
                {
                    hisRegistObj registobj = this.dataGridView1.SelectedRows[0].Tag as hisRegistObj;//就诊号
                    jiuzhencardno.Text = registobj.cardno;
                    childname.Text = registobj.patient_name;
                    childgender.Text = registobj.sex;
                    childbirthday.Value = Convert.ToDateTime(registobj.sex);
                }

            }
        }

        private void GetRegistInfo()
        {
            string cardno = jiuzhencardno.Text.Trim();

        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                tb_childcheck checkobj = dataGridView2.SelectedRows[0].Tag as tb_childcheck;
                CommonHelper.setForm(checkobj, panel2.Controls);
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            tb_childcheck checkobj = new tb_childcheck();
            checkday.Value = DateTime.Now;
            checkweight.Text = "";
            checkheight.Text = "";
            checktouwei.Text = "";
            checkzuogao.Text = "";
            doctorname.Text = "";
        }
    }
}
