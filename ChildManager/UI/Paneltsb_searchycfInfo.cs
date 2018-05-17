using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Collections;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.Model.ChildBaseInfo;
using YCF.Model;

namespace ChildManager.UI
{
    public partial class Paneltsb_searchycfInfo : Office2007Form
    {
        SearchYcfBll bll = new SearchYcfBll();
        public tb_childbase returnval = new tb_childbase();
        public Paneltsb_searchycfInfo()
        {
            InitializeComponent();
        }

        private void Paneltsb_searchInfo_Load(object sender, EventArgs e)
        {
        }

        private void Seach_Click()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (String.IsNullOrEmpty(txtMothername.Text.Trim()) && String.IsNullOrEmpty(txtidno.Text.Trim()) && String.IsNullOrEmpty(txtTelephone.Text.Trim()))
            {
                MessageBox.Show("请输入一个检索条件！");
                return;
            }
            dataGridViewX1.Rows.Clear();
            //string starttime = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            //string endtime = dateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            string sqls = string.Format("select * from v_yyt_fenmian_jiben where 1=1 ");//childBuildDay>='" + starttime + "' and childBuildDay <='" + endtime + "' ");
            if (!String.IsNullOrEmpty(txtMothername.Text.Trim()))
            {
                sqls += " and wm_name = '" + txtMothername.Text + "'";
            }
            if (!String.IsNullOrEmpty(txtidno.Text))
            {
                sqls += " and wm_identityno = '" + txtidno.Text + "'";
            }
            if (!String.IsNullOrEmpty(txtTelephone.Text))
            {
                sqls += " and wm_mobile_phone = '" + txtTelephone.Text + "'";
            }

            ArrayList list = bll.getchildBaseList(sqls);
            if (list != null && list.Count > 0)
            {
                try
                {
                    int i = 1;
                    foreach (tb_childbase obj in list)
                    {

                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridViewX1, i, obj.childgender,
                            obj.childbirthday, obj.telephone, obj.fathername, obj.mothername, obj.identityno,obj.telephone);
                        row.Tag = obj;
                        dataGridViewX1.Rows.Add(row);
                        i++;
                    }
                }
                finally
                {
                    dataGridViewX1.ClearSelection();
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Seach_Click();
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewX1.Rows[e.RowIndex].Tag != "")
            {
                tb_childbase obj = dataGridViewX1.Rows[e.RowIndex].Tag as tb_childbase;
                returnval = obj; 
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

    }
}
