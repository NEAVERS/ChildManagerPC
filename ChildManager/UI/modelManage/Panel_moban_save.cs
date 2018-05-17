using System;
using DevComponents.DotNetBar;
using ChildManager.BLL.ChildBaseInfo;
using YCF.BLL;
using YCF.Model;
using YCF.Common;
using System.Windows.Forms;

namespace ChildManager.UI
{
    public partial class Panel_moban_save : Office2007Form
    {
        ChildBaseInfoBll bll = new ChildBaseInfoBll();
        tb_healthcheck_moban _healthcheckobj = new tb_healthcheck_moban();

        public Panel_moban_save(tb_healthcheck_moban healthcheckobj)
        {
            InitializeComponent();
            _healthcheckobj = healthcheckobj;

            this.txtNo.Focus();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _healthcheckobj.moban_name = txtNo.Text.Trim();
            _healthcheckobj.creater_name = globalInfoClass.UserName;
            _healthcheckobj.moban_type = CommonHelper.getcheckedValue(panel4);
            if(new tb_healthcheck_mobanbll().ExsitsSaveOrUpdateById(_healthcheckobj))
            {
                MessageBox.Show("模板保存成功！");
                this.Close();
            }
            else
            {
                MessageBox.Show("模板保存失败！");
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

    }
}
