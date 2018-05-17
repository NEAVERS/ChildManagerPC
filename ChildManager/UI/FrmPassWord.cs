using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YCF.Common;
using ChildManager.DAL;

namespace ChildManager.UI
{
    public partial class FrmPassWord : Form
    {
        DateLogic dg = new DateLogic();
        private bool _modfiles = false;
        public FrmPassWord()
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
            string strSql = "";
            if (String.IsNullOrEmpty(txt_oldPwd.Text) || String.IsNullOrEmpty(txt_newPwd.Text) || String.IsNullOrEmpty(txt_okNewPwd.Text))
            {
                MessageBox.Show("请填写完整！","软件提示");
                return;
            }
            if (txt_oldPwd.Text != globalInfoClass.PassWord)
            {
                MessageBox.Show("原密码输入错误！", "软件提示");
                this.txt_oldPwd.Focus();
                return;
            }
            if (txt_newPwd.Text != txt_okNewPwd.Text)
            {
                  MessageBox.Show("新密码输入不一致！", "软件提示");
                  this.txt_okNewPwd.Focus();
                return;
            }

            strSql = "Update sys_users Set PassWord = '" + txt_newPwd.Text + "' Where user_code = '" +
         globalInfoClass.UserCode+ "'";

            int i = dg.executeupdate(strSql);
            if (i > 0)
            {
                MessageBox.Show("修改密码成功！","软件提示");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                return;
            }
            else
            {
                MessageBox.Show("修改密码失败","软件提示");
            }
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
    }
}
