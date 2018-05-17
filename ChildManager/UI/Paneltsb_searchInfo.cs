using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using YCF.Model;
using YCF.BLL;

namespace ChildManager.UI
{
    public partial class Paneltsb_searchInfo : Office2007Form
    {
        tb_childbasebll bll = new tb_childbasebll();
        public TB_CHILDBASE returnval = null;
        private bool _isAutoQuery = true;

        public Paneltsb_searchInfo()
        {
            InitializeComponent();
            //dateTimePicker1.Value = DateTime.Now.AddMonths(-1);

        }

        public Paneltsb_searchInfo(bool isAutoQuery) : this()
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
            string starttime = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            string endtime = dateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            string birthstarttime = dateTimePicker4.Value.ToString("yyyy-MM-dd");
            string birthendtime = dateTimePicker3.Value.ToString("yyyy-MM-dd");
            string checkTime = dtp_checktime.Value.ToString("yyyy-MM-dd");

            string healthcardno = txtNo.Text.Trim();
            string childname = textBoxX1.Text.Trim();
            string childgender = comboBox1.Text.Trim();
            string telephone = txtTelephone.Text.Trim();
            string patientId = txtPatientId.Text.Trim();
            string motherName = "";
            string fatherName = txtFatherName.Text.Trim();

            if (string.IsNullOrEmpty(healthcardno) && string.IsNullOrEmpty(childname) 
                && string.IsNullOrEmpty(childgender) && string.IsNullOrEmpty(telephone)
                && string.IsNullOrEmpty(patientId) && string.IsNullOrEmpty(fatherName)
                && !checkBox1.Checked && !chkUseDate.Checked && !ckb_check.Checked
                )
            {
                MessageBox.Show("请至少输入一个条件进行查询");
                return;
            }

            if (!chkUseDate.Checked)
            {
                starttime = "";
                endtime = "";
            }
            if (!checkBox1.Checked)
            {
                birthstarttime = "";
                birthendtime = "";
            }
            if (!ckb_check.Checked)
            {
                checkTime = "";
            }
            IList<TB_CHILDBASE> list = null;
            if (checkTime != "")
            {
                list = bll.GetListBySqlCheckTime(checkTime);
            }
            else
            {
                list = bll.GetList(starttime, endtime, birthstarttime, birthendtime, healthcardno, childname, childgender, telephone, motherName, fatherName, patientId);
            }
            if (list != null && list.Count > 0)
            {
                try
                {
                    int i = 1;
                    foreach (TB_CHILDBASE obj in list)
                    {
                        string birthstr = "";
                        string buidstr = "";
                        DateTime outtime = new DateTime();
                        if (DateTime.TryParse(obj.childbirthday, out outtime))
                        {
                            birthstr = outtime.ToString("yyyy-MM-dd");
                        }
                        if (DateTime.TryParse(obj.childbuildday, out outtime))
                        {
                            buidstr = outtime.ToString("yyyy-MM-dd");
                        }

                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, i, obj.healthcardno, obj.patient_id, obj.childname, obj.childgender,
                            birthstr, obj.telephone, buidstr, obj.gaoweiyinsu);
                        row.Tag = obj;
                        dataGridView1.Rows.Add(row);
                        i++;
                    }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Seach_Click();
        }

        private void dataGridViewX2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                TB_CHILDBASE obj = dataGridView1.Rows[e.RowIndex].Tag as TB_CHILDBASE;
                returnval = obj;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker4.Enabled = true;
                dateTimePicker3.Enabled = true;
            }
            else
            {
                dateTimePicker4.Enabled = false;
                dateTimePicker3.Enabled = false;
            }
        }

        private void chkUseDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDate.Checked)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag != null)
                   
                {
                    TB_CHILDBASE obj = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag as TB_CHILDBASE;
                    returnval = obj;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void ckb_check_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_check.Checked)
            {
                dtp_checktime.Enabled = true;
            }
            else
            {
                dtp_checktime.Enabled = false;
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Selected == true)
                {
                    TB_CHILDBASE obj = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag as TB_CHILDBASE;
                    returnval = obj;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }
    }
}
