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
        TB_HEALTHCHECK_MOBAN _healthcheckobj = new TB_HEALTHCHECK_MOBAN();

        public Panel_moban_save(TB_HEALTHCHECK_MOBAN healthcheckobj)
        {
            InitializeComponent();
            _healthcheckobj = healthcheckobj;

            this.txtNo.Focus();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _healthcheckobj.MOBAN_NAME = txtNo.Text.Trim();
            _healthcheckobj.CREATER_NAME = globalInfoClass.UserName;
            _healthcheckobj.MOBAN_TYPE = CommonHelper.getcheckedValue(panel4);
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
