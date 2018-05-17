using System;
using DevComponents.DotNetBar;
using YCF.BLL;
using YCF.Model;
using YCF.Common;
using System.Windows.Forms;
using System.Collections.Generic;
using YCF.BLL.Template;

namespace ChildManager.UI
{
    public partial class Panel_Templet_list : Office2007Form
    {
        private tab_templet_Infobll bll = new tab_templet_Infobll();
        public TAB_TEMPLET_INFO _templetobj = new TAB_TEMPLET_INFO();
        string _type = "";
        string _child_type = "";

        public Panel_Templet_list(string type)
        {
            InitializeComponent();
            _type = type;
        }
        public Panel_Templet_list(string type,string child_type)
        {
            InitializeComponent();
            _type = type;
            _child_type = child_type;
        }

        public void refreshRecordList()
        {
            IList<TAB_TEMPLET_INFO> mobanlist = null;
            if (string.IsNullOrEmpty(_child_type))
            {
                mobanlist = bll.GetList(_type);
            }
            else
            {
                mobanlist = bll.GetList(_type,_child_type);
            }
            
            if (mobanlist != null && mobanlist.Count > 0)
            {
                dataGridView1.Rows.Clear();
                try
                {
                    int i = 1;
                    foreach (TAB_TEMPLET_INFO obj in mobanlist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, obj.NAME, obj.CONTS, obj.CHILD_TYPE, obj.TYPE);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            refreshRecordList();
            checkBoxs_CheckedChanged(sender,e);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                TAB_TEMPLET_INFO templet_Info = dataGridView1.SelectedRows[0].Tag as TAB_TEMPLET_INFO;
                _templetobj = templet_Info;
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                TAB_TEMPLET_INFO templet_Info = dataGridView1.SelectedRows[0].Tag as TAB_TEMPLET_INFO;
                _templetobj = templet_Info;
                this.DialogResult = DialogResult.OK;//关闭窗体，导入值
            }
            else
            {
                MessageBox.Show("请选择内容！","系统提示");
            }

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Selected == true)
                {
                    TAB_TEMPLET_INFO templet_Info = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Tag as TAB_TEMPLET_INFO;
                    _templetobj = templet_Info;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (dataGridView1.Rows[0].Selected == true)
                {
                    TAB_TEMPLET_INFO templet_Info = dataGridView1.Rows[0].Tag as TAB_TEMPLET_INFO;
                    _templetobj = templet_Info;
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
