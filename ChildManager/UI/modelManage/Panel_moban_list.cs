using System;
using DevComponents.DotNetBar;
using YCF.BLL;
using YCF.Model;
using YCF.Common;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ChildManager.UI
{
    public partial class Panel_moban_list : Office2007Form
    {
        private tb_healthcheck_mobanbll bll = new tb_healthcheck_mobanbll();
        public TB_HEALTHCHECK_MOBAN _healthcheckobj = new TB_HEALTHCHECK_MOBAN();

        public Panel_moban_list()
        {
            InitializeComponent();

            this.txtNo.Focus();
        }

        public void refreshRecordList()
        {
            string moban_name = txtNo.Text.Trim();
            string moban_type = CommonHelper.getcheckedValue(panel4);
            string creater_name = globalInfoClass.UserName;

            IList<TB_HEALTHCHECK_MOBAN> mobanlist = bll.GetList(moban_name,moban_type, creater_name);
            if (mobanlist != null && mobanlist.Count > 0)
            {
                dataGridView1.Rows.Clear();
                try
                {
                    int i = 1;
                    foreach (TB_HEALTHCHECK_MOBAN obj in mobanlist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, obj.MOBAN_NAME);
                        row.Tag = obj;
                        dataGridView1.Rows.Add(row);
                        i++;
                    }
                }
                finally
                {
                    dataGridView1.ClearSelection();
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private void txtNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty((sender as TextBox).Text))
                {
                    refreshRecordList();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            refreshRecordList();
            checkBoxs_CheckedChanged(sender,e);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                _healthcheckobj = dataGridView1.CurrentRow.Tag as TB_HEALTHCHECK_MOBAN;
                this.DialogResult = DialogResult.OK;//关闭窗体，导入值
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

        private void Panel_moban_list_Load(object sender, EventArgs e)
        {
            refreshRecordList();
        }
    }
}
