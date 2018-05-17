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
    public partial class FrmConfig : Form
    {
        DateLogic dg = new DateLogic();
        private bool _modfiles = false;
        public FrmConfig()
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
            int premax = 0;
            if (String.IsNullOrEmpty(pre_max.Text))
            {
                MessageBox.Show("请填写后再保存！","软件提示");
                return;
            }
            
            if(!Int32.TryParse(pre_max.Text,out premax))
            {
                MessageBox.Show("只能为数字！", "软件提示");
                return;
            }

            strSql = "Update tb_users Set pre_max = '" + pre_max.Text + "' Where user_code = '" +
         globalInfoClass.UserCode+ "'";

            int i = dg.executeupdate(strSql);
            if (i > 0)
            {
                globalInfoClass.Pre_Max = premax;
                MessageBox.Show("修改成功！","软件提示");
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                return;
            }
            else
            {
                MessageBox.Show("修改失败","软件提示");
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

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            pre_max.Text = globalInfoClass.Pre_Max.ToString();
        }
    }
}
