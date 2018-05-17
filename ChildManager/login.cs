using System;
using System.Windows.Forms;
using YCF.Common;
using YCF.Model;
using YCF.BLL.sys;

namespace ChildManager.UI
{
    public partial class login : Form
    {
        sys_usersBll userbll = new sys_usersBll();

        public login()
        {
            InitializeComponent();
            hospital.Text = OperatFile.GetIniFileString("HospitalInfo", "hospital_name", "", Application.StartupPath + "\\hospitalinfo.ini");
            textBox1.Select();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - 1100, Screen.PrimaryScreen.Bounds.Height - 700);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }


        //回车换行
        //回车代替tab键盘
        private void all_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }
        /// <summary>
        /// 验证登录方法
        /// </summary>
        private void Login()
        {
            String usercode = textBox1.Text.Trim();
            String password = textBox2.Text.Trim();
            string hospital=this.hospital.Text.Trim();

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
            //if (string.IsNullOrEmpty(hospital))
            //{
            //    MessageBox.Show("请选择院区!", "系统提示");
            //    this.hospital.Focus();
            //    return;
            //}
            //连接后台数据库判断本用户是否存在！
            SYS_USERS users = userbll.Get(usercode);
            try
            {
                if (users==null)
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
                        globalInfoClass.Ward_name = users.WARD_NAME;
                        globalInfoClass.Zhicheng = users.ZHICHENG;
                        globalInfoClass.Zhiwu = users.ZHIWU;
                        //globalInfoClass.Hospital = "礼嘉分院";//this.hospital.Text.Trim();
                        textBox1.Text = OperatFile.SetIniFileString("HospitalInfo", "hospital_name", hospital, Application.StartupPath + "\\hospitalinfo.ini");

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
    }
}
