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
        private SYS_USERS _userobj = new SYS_USERS();
        private sys_roleBll rolebll = new sys_roleBll();

        public sys_user_edit(int rowindex, SYS_USERS userobj, sys_user_list yonghuguanli)
        {
            InitializeComponent();
            userlist = yonghuguanli;
            _userobj = userobj;
            if (rowindex != -1)
            {
                DataGridViewRow row = userlist.dgvConditionTreatRecordList.Rows[rowindex];
                _userobj = (SYS_USERS)row.Tag;
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

            SYS_USERS userobj = CommonHelper.GetObj<SYS_USERS>(this.Controls);
            userobj.ROLE_CODE = role_code.SelectedValue.ToString();

            if (_userobj == null)
            {
                userobj.PASSWORD = "123";
                userobj.PRE_MAX = 20;
                userobj.USER_TYPE = "2";
                SYS_USERS obj = bll.Get(user_code.Text.Trim());
                if (obj != null)
                {
                    MessageBox.Show("该账户已存在!", "系统提示");
                    user_code.Focus();
                    return;
                }
            }
            else
            {
                userobj.ID = _userobj.ID;
                userobj.PASSWORD = _userobj.PASSWORD;
                userobj.USER_TYPE = _userobj.USER_TYPE;
                userobj.PRE_MAX = _userobj.PRE_MAX;
                userobj.WARD_ID = _userobj.WARD_ID;
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
            IList<SYS_ROLE> listrole = rolebll.GetList();
            role_code.DataSource = listrole;
            role_code.DisplayMember = "role_name";
            role_code.ValueMember = "role_code";
            if (_userobj != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                CommonHelper.setForm<SYS_USERS>(_userobj,this.Controls);
                if (!string.IsNullOrEmpty(_userobj.ROLE_CODE))
                {
                    role_code.SelectedValue = _userobj.ROLE_CODE;
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
