using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.Model;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.BLL;
using System.Collections;
using login.UI.printrecord;
using ChildManager.Model.childSetInfo;

namespace ChildManager.UI.childSetInfo
{
    public partial class Panel_moban_manage : UserControl
    {
        public Panel_moban_manage _mobanpanel;
        private MobanManageBll mobanbll = new MobanManageBll();
        public Panel_moban_manage()
        {
            InitializeComponent();

            _mobanpanel = this;
        }


        private void Panel_moban_manage_Load(object sender, EventArgs e)
        {
            

            refreshRecordList();

        }

        public void refreshRecordList()
        {
            string sqlstr = "select * from tb_moban where 1=1 ";
            if (!String.IsNullOrEmpty(comboBox1.Text.Trim()))
            {
                sqlstr += " and m_type='"+comboBox1.Text.Trim()+"'";
            }

            sqlstr += " order by Convert(int,yf) desc ";
               
            ArrayList mobanlist = mobanbll.getMobanList(sqlstr);

            if (mobanlist != null && mobanlist.Count > 0)
            {
                dataGridView1.Rows.Clear();
                try
                {
                    int i = 1;
                    foreach (mobanManageObj obj in mobanlist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, obj.yf+"-"+obj.yfend,obj.m_name, obj.m_type,obj.m_content);
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


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                mobanManageObj mobanobj = dataGridView1.CurrentRow.Tag as mobanManageObj;
                Panel_moban_manage_save mobansave = new Panel_moban_manage_save(_mobanpanel, mobanobj);
                mobansave.ShowDialog();
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

        private void buttonX4_Click(object sender, EventArgs e)
        {
            refreshRecordList();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            mobanManageObj mobanobj = new mobanManageObj();
            Panel_moban_manage_save mobansave = new Panel_moban_manage_save(_mobanpanel, mobanobj);
            mobansave.ShowDialog();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                mobanManageObj mobanobj = dataGridView1.CurrentRow.Tag as mobanManageObj;
                Panel_moban_manage_save mobansave = new Panel_moban_manage_save(_mobanpanel, mobanobj);
                mobansave.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择要修改的行");
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                if (MessageBox.Show("删除该模板？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        mobanManageObj mobanobj = dataGridView1.CurrentRow.Tag as mobanManageObj;
                        string sqls = "delete from tb_moban   where id=" + mobanobj.id;
                        if (mobanbll.deleterecord(sqls) > 0)
                        {

                            MessageBox.Show("删除成功!", "软件提示");
                            refreshRecordList();
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
                MessageBox.Show("请选择要删除的行");
            }
        }

    }
}
