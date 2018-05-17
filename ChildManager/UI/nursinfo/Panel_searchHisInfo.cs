using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using YCF.Model;
using YCF.BLL;
using YCF.Common.vo;
using System.Xml;
using YCF.Common;
using YCF.Model.NotMaps;

namespace ChildManager.UI.nursinfo
{
    public partial class Panel_searchHisInfo : Office2007Form
    {
        tb_childbasebll bll = new tb_childbasebll();
        public HisPatientInfo returnval = null;
        private bool _isAutoQuery = true;
        IList<HisPatientInfo> _hislist = null;
        string _hospital = globalInfoClass.Hospital;

        public Panel_searchHisInfo()
        {
            InitializeComponent();
            //dateTimePicker1.Value = DateTime.Now.AddMonths(-1);

        }

        /// <summary>
        /// 若当天挂多次号，进行选择加载
        /// 2017-11-23 cc
        /// </summary>
        /// <param name="hislist">挂号数据集合</param>
        public Panel_searchHisInfo(IList<HisPatientInfo> hislist)
        {
            InitializeComponent();
            _isAutoQuery = false;
            panelEx1.Enabled = false;
            _hislist = hislist;
            int i = 0;
            foreach (HisPatientInfo obj in _hislist)
            {
                string[] strInfo = null;
                string visit_time = "";
                string type = "";
                if (!string.IsNullOrEmpty(obj.SIGNAL_SOURCE_CODE))
                {
                    string[] time = null;
                    strInfo = obj.SIGNAL_SOURCE_CODE.Split('_');
                    time = strInfo[0].Split('-');
                    visit_time = "20" + time[2] + "-" + time[1].Replace("? ", "") + "-" + time[0];
                    type = strInfo[1];
                }
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1, i + 1, obj.PAT_INDEX_NO, obj.VISIT_CARD_NO, obj.PAT_NAME, obj.DATE_BIRTH, obj.PHYSI_SEX_NAME
                    , visit_time, type);
                row.Tag = obj;
                dataGridView1.Rows.Add(row);
                i++;
            }
            labelX5.Text = i.ToString();

        }
        public Panel_searchHisInfo(bool isAutoQuery) : this()
        {
            _isAutoQuery = isAutoQuery;
        }

        private void Paneltsb_searchInfo_Load(object sender, EventArgs e)
        {
            if (_isAutoQuery)
            {
                Seach_Click();
            }

        }

        private void Seach_Click()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataGridView1.Rows.Clear();
            string starttime = dtp_starttime.Value.ToString("yyyy-MM-dd");
            string endtime = dtp_endtime.Value.ToString("yyyy-MM-dd");
            string birthtime = "";
            string checkTime = dtp_starttime.Value.ToString("yyyy-MM-dd");
            string childgender = txtSex.Text.Trim();
            string patientId = txtPatientId.Text.Trim();
            string patientName = txtName.Text.Trim();

            if (chkUseDate.Checked)
            {
                birthtime = dateTimePicker4.Value.ToString("yyyy-MM-dd");   
            }
            if (checkBox1.Checked)
            {
                starttime = "";
                endtime = "";
            }
            IList<HisPatientInfo> list = GetListRequest(patientId, patientName, childgender, birthtime, starttime, endtime);
           
            if (list != null && list.Count > 0)
            {
                try
                {
                    int i = 0;
                    foreach (HisPatientInfo obj in list)
                    {
                        string[] strInfo = null;
                        string visit_time = "";
                        string type = "";
                        if (!string.IsNullOrEmpty(obj.SIGNAL_SOURCE_CODE))
                        {
                            string[] time = null;
                            strInfo=obj.SIGNAL_SOURCE_CODE.Split('_');
                            time = strInfo[0].Split('-');
                            visit_time = "20" + time[2] + "-" + time[1].Replace("? ", "") + "-" + time[0];
                            type = strInfo[1];
                        }
                        string birth = "";
                        birth = obj.DATE_BIRTH.Substring(0,10);
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, i+1, obj.PAT_INDEX_NO,obj.VISIT_CARD_NO ,obj.PAT_NAME, birth, obj.PHYSI_SEX_NAME
                            , visit_time, type,obj.REGIST_DEPT_NAME);
                        row.Tag = obj;
                        dataGridView1.Rows.Add(row);
                        i++;
                    }
                    labelX5.Text = i.ToString();
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                    if (dataGridView1.Rows.Count >= 0)
                    {
                        this.dataGridView1.Focus();
                        this.dataGridView1.Rows[0].Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// 根据条件查询
        /// 2017-11-14 cc
        /// </summary>
        /// <param name="txtPatientId">病人ID</param>
        /// <param name="txtPatientName">姓名</param>
        /// <param name="txtSex">性别</param>
        /// <param name="txtBirth">出生日期</param>
        /// <param name="txtStartTime">开始时间</param>
        /// <param name="txtEbdTime">结束时间</param>
        /// <returns></returns>
        public IList<HisPatientInfo> GetListRequest(string txtPatientId, string txtPatientName,string txtSex,string txtBirth,string txtStartTime,string txtEbdTime)
        {
            YHRequest YHReq = new YHRequest(YHRequest.fid_get_hisregist);

            if (!string.IsNullOrEmpty(txtPatientId))
            {
                YHReq.addQuery(YHRequest.item_patient, YHRequest.compy_equals, "'" + txtPatientId + "'", YHRequest.splice_and);
            }
            if (!string.IsNullOrEmpty(txtPatientName))
            {
                YHReq.addQuery(YHRequest.item_name, YHRequest.compy_like, "'%" + txtPatientName + "%'", YHRequest.splice_and);
            }
            if (!string.IsNullOrEmpty(txtSex))
            {
                YHReq.addQuery(YHRequest.item_sex, YHRequest.compy_equals, "'" + txtSex + "'", YHRequest.splice_and);
            }
            if (txtStartTime != "")
            {
                if (_hospital.Contains("礼嘉"))
                {
                    YHReq.addQuery(YHRequest.item_date, YHRequest.compy_gt, "to_date('" + txtStartTime + "','yyyy-MM-dd')", YHRequest.splice_and);
                    YHReq.addQuery(YHRequest.item_date, YHRequest.compy_lt, "to_date('" + ((Convert.ToDateTime(txtEbdTime)).AddDays(1)).ToString("yyyy-MM-dd") + "','yyyy-MM-dd') and (REGIST_DEPT_NAME ='礼嘉儿保科' or REGIST_DEPT_NAME = '礼嘉心理科')", YHRequest.splice_and);
                }
                else
                {
                    YHReq.addQuery(YHRequest.item_date, YHRequest.compy_gt, "to_date('" + txtStartTime + "','yyyy-MM-dd')", YHRequest.splice_and);
                    YHReq.addQuery(YHRequest.item_date, YHRequest.compy_lt, "to_date('" + ((Convert.ToDateTime(txtEbdTime)).AddDays(1)).ToString("yyyy-MM-dd") + "','yyyy-MM-dd') and (REGIST_DEPT_NAME ='儿保科' or REGIST_DEPT_NAME = '心理科')", YHRequest.splice_and);

                }
            }
            else
            {
                if (_hospital.Contains("礼嘉"))
                {
                    YHReq.addQuery(YHRequest.item_date, YHRequest.compy_gt_equals, "to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + "','yyyy-MM-dd') and (REGIST_DEPT_NAME ='礼嘉儿保科' or REGIST_DEPT_NAME = '礼嘉心理科')", YHRequest.splice_and);
                }
                else
                {
                    YHReq.addQuery(YHRequest.item_date, YHRequest.compy_gt_equals, "to_date('" + DateTime.Now.ToString("yyyy-MM-dd") + "','yyyy-MM-dd') and (REGIST_DEPT_NAME ='儿保科' or REGIST_DEPT_NAME = '心理科')", YHRequest.splice_and);
                }
            }
            if (!string.IsNullOrEmpty(txtBirth))
            {
                YHReq.addQuery(YHRequest.item_birth, YHRequest.compy_equals, "to_date('" + txtBirth + "','yyyy-MM-dd')", YHRequest.splice_and);
            }
            var result = YHMQUtil<HisPatientInfo>.get(YHReq);
            if (result.success)
            {
                return result.dataList;
            }
            else
            {
                return null;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Seach_Click();
        }

        private void dataGridViewX2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                HisPatientInfo obj = dataGridView1.Rows[e.RowIndex].Tag as HisPatientInfo;
                returnval = obj;
                DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 是否启用就诊时间查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtp_starttime.Enabled = false;
                dtp_endtime.Enabled = false;
            }
            else
            {
                dtp_starttime.Enabled = true;
                dtp_endtime.Enabled = true;
            }
        }
       
        /// <summary>
        /// 是否启用出生日期查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUseDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDate.Checked)
            {          
                dateTimePicker4.Enabled = true;  
            }
            else
            {
                dateTimePicker4.Enabled = false;        
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (dataGridView1.Rows[0].Selected == true)
                {
                    HisPatientInfo obj = dataGridView1.Rows[0].Tag as HisPatientInfo;
                    returnval = obj;
                    DialogResult = DialogResult.OK;
                }
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)  // 按下的是回车键
            {
                Seach_Click();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void dtp_starttime_ValueChanged(object sender, EventArgs e)
        {
            DateTime startTime = dtp_starttime.Value;
            DateTime endTime = dtp_endtime.Value;
            DateTime nowTime = DateTime.Now;
            double Num1 = (nowTime - endTime).TotalDays;
            if (Num1 < 0)
            {
                MessageBox.Show("时间超出范围");
                dtp_endtime.Text = nowTime.ToString("yyyy年MM月dd日");
                return;
            }
            double Num2 = (nowTime - startTime).TotalDays;
            if (Num2 < 0)
            {
                MessageBox.Show("时间超出范围");
                dtp_starttime.Text = nowTime.ToString("yyyy年MM月dd日");
                return;
            }
            double Num = (startTime - endTime).TotalDays;
            if (Num > 0)
            {
                dtp_endtime.Text= dtp_starttime.Text ;
            }
        }

        private void dtp_endtime_ValueChanged(object sender, EventArgs e)
        {
            DateTime startTime = dtp_starttime.Value;
            DateTime endTime = dtp_endtime.Value;
            DateTime nowTime = DateTime.Now;
            double Num1 = (nowTime - endTime).TotalDays;
            if (Num1 < 0)
            {
                MessageBox.Show("时间超出范围");
                dtp_endtime.Text = nowTime.ToString("yyyy年MM月dd日");
                return;
            }
            double Num2 = (nowTime - startTime).TotalDays;
            if (Num2 < 0)
            {
                MessageBox.Show("时间超出范围");
                dtp_starttime.Text = nowTime.ToString("yyyy年MM月dd日");
                return;
            }
            double Num = (startTime - endTime).TotalDays;
            if (Num > 0)
            {
                dtp_starttime.Text=dtp_endtime.Text ;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Selected == true)
                {
                    HisPatientInfo obj = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag as HisPatientInfo;
                    returnval = obj;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }
    }

}
