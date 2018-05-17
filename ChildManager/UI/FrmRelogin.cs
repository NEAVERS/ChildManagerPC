using System;
using System.Windows.Forms;
using YCF.Common;
using ChildManager.DAL;
using YCF.Model;
using YCF.BLL.sys;

namespace ChildManager.UI
{
    public partial class FrmRelogin : Form
    {
        sys_usersBll userbll = new sys_usersBll();
        private bool _modfiles = false;
        public FrmRelogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            String usercode = textBox1.Text.Trim();
            String password = textBox2.Text.Trim();

            if (String.IsNullOrEmpty(usercode))
            {
                MessageBox.Show("用户名不能为空！", "系统提示");
                textBox1.Focus();
                return;
            }
            if (String.IsNullOrEmpty(password))
            {
                MessageBox.Show("密码不能为空!", "系统提示");
                textBox2.Focus();
                return;
            }
            //连接后台数据库判断本用户是否存在！
            //连接后台数据库判断本用户是否存在！
            SYS_USERS users = userbll.Get(usercode);
            try
            {
                if (users == null)
                {
                    MessageBox.Show("用户名或者密码错误！", "系统提示");
                    textBox2.Focus();
                    return;
                }
                else
                {
                    if (password != users.PASSWORD)
                    {
                        MessageBox.Show("用户名或者密码错误！", "系统提示");
                        textBox2.Focus();
                        return;
                    }
                    else
                    {
                        globalInfoClass.PassWord = users.PASSWORD;//赋值用户密码
                        globalInfoClass.UserCode = users.USER_CODE;//赋值用户编码
                        globalInfoClass.UserName = users.USER_NAME;//赋值用户姓名
                        globalInfoClass.UserType = users.USER_TYPE;//赋值用户类型,判断权限
                        globalInfoClass.Pre_Max = (int)users.PRE_MAX;//赋值用户类型,判断权限
                        globalInfoClass.User_Role = users.ROLE_CODE;//赋值用户类型,判断权限
                        this.DialogResult = DialogResult.OK;//跳转到主窗体控件
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            this.DialogResult = DialogResult.OK;//跳转到主窗体控件
            this.Close();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if (_modfiles)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }


        //回车代替tab键盘
        private void all_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk_Click(sender,e);
            }
        }
    }
}
