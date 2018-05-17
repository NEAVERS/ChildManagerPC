using System;
using System.Windows.Forms;
using YCF.BLL.sys;
using YCF.Model;
using YCF.Common;
using System.Collections.Generic;

namespace ChildManager.UI.sys
{
    public partial class sys_user_edit : Form
    {
        private bool _modified = false;
        private sys_usersBll bll = new sys_usersBll();
        private sys_user_list userlist = null;
        private sys_users _userobj = new sys_users();
        private sys_roleBll rolebll = new sys_roleBll();

        public sys_user_edit(int rowindex, sys_users userobj, sys_user_list yonghuguanli)
        {
            InitializeComponent();
            userlist = yonghuguanli;
            _userobj = userobj;
            if (rowindex != -1)
            {
                DataGridViewRow row = userlist.dgvConditionTreatRecordList.Rows[rowindex];
                _userobj = (sys_users)row.Tag;
            }
        }
        //保存按钮
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrEmpty(user_code.Text.Trim()))
            {
                MessageBox.Show("账户不能为空!", "系统提示");
                user_code.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(user_name.Text.Trim()))
            {
                MessageBox.Show("姓名不能为空!", "系统提示");
                user_code.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(role_code.Text.Trim()))
            {
                MessageBox.Show("角色不能为空!", "系统提示");
                user_code.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(dept_name.Text.Trim()))
            {
                MessageBox.Show("科室不能为空!", "系统提示");
                user_code.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(zhiwu.Text.Trim()))
            {
                MessageBox.Show("职务不能为空!", "系统提示");
                zhiwu.Focus();
                return;
            }
            else if (String.IsNullOrEmpty(ward_name.Text.Trim()))
            {
                MessageBox.Show("地点不能为空!", "系统提示");
                ward_name.Focus();
                return;
            }

            sys_users userobj = CommonHelper.GetObj<sys_users>(this.Controls);
            userobj.role_code = role_code.SelectedValue.ToString();

            if (_userobj == null)
            {
                userobj.password = "123";
                userobj.pre_max = 20;
                userobj.user_type = "2";
                sys_users obj = bll.Get(user_code.Text.Trim());
                if (obj != null)
                {
                    MessageBox.Show("该账户已存在!", "系统提示");
                    user_code.Focus();
                    return;
                }
            }
            else
            {
                userobj.id = _userobj.id;
                userobj.password = _userobj.password;
                userobj.user_type = _userobj.user_type;
                userobj.pre_max = _userobj.pre_max;
                userobj.ward_id = _userobj.ward_id;
            }

            if (bll.SaveOrUpdate(userobj))
            {
                MessageBox.Show("保存成功", "软件提示");
                userlist.jiazaiList();
                this.Close();//本窗口关闭

            }
            else
            {
                MessageBox.Show("保存失败", "软件提示");
            }

        }
        //页面加载数据
        private void xiugaiu_Load(object sender, EventArgs e)
        {
            IList<sys_role> listrole = rolebll.GetList();
            role_code.DataSource = listrole;
            role_code.DisplayMember = "role_name";
            role_code.ValueMember = "role_code";
            if (_userobj != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                CommonHelper.setForm<sys_users>(_userobj,this.Controls);
                if (!string.IsNullOrEmpty(_userobj.role_code))
                {
                    role_code.SelectedValue = _userobj.role_code;
                }
                
                Cursor.Current = Cursors.Default;
            }
        }

        //关闭按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (_modified)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }

        }

      
    }

}
