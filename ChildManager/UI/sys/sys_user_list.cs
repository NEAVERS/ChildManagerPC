using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YCF.BLL.sys;
using YCF.Model;

namespace ChildManager.UI.sys
{
    public partial class sys_user_list : UserControl
    {
        sys_usersBll bll = new sys_usersBll();
        sys_roleBll rolebll = new sys_roleBll();

        public sys_user_list()
        {
            InitializeComponent();
        }

        public void jiazaiList()
        {
            Cursor.Current = Cursors.WaitCursor;
            IList<SYS_USERS> userlist = bll.GetList(textBox1.Text.Trim(), textBox2.Text.Trim(), comboBox2.SelectedValue.ToString());
            dgvConditionTreatRecordList.Rows.Clear();//清空列表

            try
            {
                if (userlist != null && userlist.Count > 0)
                {
                    int i = 1;
                    foreach (SYS_USERS csobj in userlist)
                    {
                        string role_name = "";
                        SYS_ROLE _Role = rolebll.GetByCode(csobj.ROLE_CODE);
                        if (_Role != null)
                        {
                            role_name = _Role.ROLE_NAME;
                        }
                        else
                        {
                            role_name = csobj.ROLE_CODE;
                        }
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvConditionTreatRecordList, new DataGridViewCheckBoxCell().Value, i.ToString(), csobj.USER_CODE
                            , csobj.USER_NAME, csobj.DEPT_NAME, csobj.DEPT_ID, csobj.PHONE, role_name, csobj.ADDRESS);
                        row.Tag = csobj;
                        dgvConditionTreatRecordList.Rows.Add(row);
                        i++;
                    }
                }
            }
            finally
            {
                dgvConditionTreatRecordList.ClearSelection();
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            sys_user_edit useredit = new sys_user_edit(-1,null, this);
            useredit.ShowDialog();
        }
        //双击记录数据
        private void dgvConditionTreatRecordList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SYS_USERS userobj = dgvConditionTreatRecordList.Rows[0].Tag as SYS_USERS;
                sys_user_edit useredit = new sys_user_edit(-1, userobj, this);
                useredit.ShowDialog();
            }
        }
        //修改数据
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvConditionTreatRecordList.SelectedRows.Count >= 1)
            {

                SYS_USERS userobj = dgvConditionTreatRecordList.SelectedRows[0].Tag as SYS_USERS;
                sys_user_edit useredit = new sys_user_edit(-1, userobj, this);
                useredit.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择要修改的行！", "系统提示");
            }
        }
        //删除数据
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvConditionTreatRecordList.SelectedRows.Count >= 1)
            {

                SYS_USERS userobj = dgvConditionTreatRecordList.SelectedRows[0].Tag as SYS_USERS;

                if (MessageBox.Show("删除所选的记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (bll.Delete(userobj))
                        {
                            jiazaiList();
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
                MessageBox.Show("请选择要的删除的行！", "系统提示");
            }
        }
        //重置密码，123
        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvConditionTreatRecordList.SelectedRows.Count >= 1)
            {
                SYS_USERS userobj = dgvConditionTreatRecordList.SelectedRows[0].Tag as SYS_USERS;
                if (MessageBox.Show("是否重置所选的记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        userobj.PASSWORD = "123";
                        if (bll.Update(userobj))
                        {
                            MessageBox.Show("重置成功", "软件提示");
                            jiazaiList();
                        }
                        else
                        {
                            MessageBox.Show("重置失败", "软件提示");
                        }

                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }

        //批量修改角色
        private void button4_Click(object sender, EventArgs e)
        {
            IList<int> list = new List<int>();
            for (int i = 0; i < dgvConditionTreatRecordList.Rows.Count; i++)
            {
                SYS_USERS userobj = dgvConditionTreatRecordList.Rows[i].Tag as SYS_USERS;
                string _selectValue = dgvConditionTreatRecordList.Rows[i].Cells["Column6"].EditedFormattedValue.ToString();
                if (_selectValue == "True")
                {
                    list.Add((int)userobj.ID);
                }
            }
            int[] ids = new int[list.Count];
            list.CopyTo(ids,0);
            sys_user_roles upusersrole = new sys_user_roles(ids, this);
            upusersrole.ShowDialog();
        }

        //加载页面
        private void yonghuliebiao_Load_1(object sender, EventArgs e)
        {

            IList<SYS_ROLE> listrole = rolebll.GetList();
            SYS_ROLE role = new SYS_ROLE();
            role.ROLE_CODE = "";
            role.ROLE_NAME = "全部";
            listrole.Add(role);
            comboBox2.DataSource = listrole;
            comboBox2.DisplayMember = "role_name";
            comboBox2.ValueMember = "role_code";
            comboBox2.SelectedValue = "";
            jiazaiList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            jiazaiList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sys_name_list namelist = new sys_name_list();
            namelist.ShowDialog();
        }

        private void dgvConditionTreatRecordList_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvConditionTreatRecordList.Rows[e.RowIndex].Tag != null)
                {
                    sys_user_edit useredit = new sys_user_edit(e.RowIndex,null, this);
                    useredit.ShowDialog();
                }
                else
                {
                    MessageBox.Show("选择纪录为空，不允许修改");
                    return;
                }
            }
        }
    }
}

