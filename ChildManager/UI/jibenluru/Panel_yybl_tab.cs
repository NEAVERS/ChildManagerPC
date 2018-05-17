using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YCF.Model;
using YCF.DAL;
using YCF.BLL;
using YCF.Common;
using ChildManager.UI.printrecord;

namespace ChildManager.UI.jibenluru
{
    public partial class Panel_yybl_tab : UserControl
    {
        private WomenInfo _womeninfo;
        private ys_yybl_tabbll blls = new ys_yybl_tabbll();

        public Panel_yybl_tab(WomenInfo womeninfo)
        {
            InitializeComponent();
            _womeninfo = womeninfo;
        }


        private void btnsave_Click(object sender, EventArgs e)
        {
            YS_YYBL_TAB tab = getObj();
            if (blls.SaveOrUpdate(tab))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private YS_YYBL_TAB getObj()
        {
            YS_YYBL_TAB _obj = blls.GetByCdId(_womeninfo.cd_id);
            YS_YYBL_TAB obj = CommonHelper.GetObjMenzhen<YS_YYBL_TAB>(panel1.Controls, _womeninfo.cd_id);
            if (_obj != null)
            {
                obj.ID = _obj.ID;
                obj.TESTNAME = _obj.TESTNAME;
                obj.TESTDATE = _obj.TESTDATE;
            }
            return obj;
        }

        private void Panel_yybl_tab_Load(object sender, EventArgs e)
        {
            RefreshCode();
        }

        private void RefreshCode()
        {
            Cursor.Current = Cursors.Default;
            YS_YYBL_TAB _obj = blls.GetByCdId(_womeninfo.cd_id);
            if (_obj != null)
            {
                CommonHelper.setForm(_obj, panel1.Controls);
            }
            else
            {
                testName.Text = globalInfoClass.UserName;
            }
            Cursor.Current = Cursors.WaitCursor;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            YS_YYBL_TAB _obj = blls.GetByCdId(_womeninfo.cd_id);
            if (_obj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (blls.Delete(_obj.ID))
                        {
                            MessageBox.Show("删除成功!", "软件提示");
                            RefreshCode();
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
                MessageBox.Show("该记录还未保存！");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            print(true);
        }

        public void print(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            YS_YYBL_TAB _obj = blls.GetByCdId(_womeninfo.cd_id);
            if (_obj == null)
            {
                MessageBox.Show("请保存后再预览打印！", "软件提示");
                return;
            }
            try
            {
                //TB_CHILDBASE baseobj = new tb_childbasebll().Get(_womeninfo.cd_id);
                Panel_ysyyblPrinter printer = new Panel_ysyyblPrinter(_obj);
                printer.Print(ispre);
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常，请联系管理员！");
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            print(false);
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
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
            if (checkBox7.Checked)
            {
                textBox6.Enabled = true;
            }
            else
            {
                textBox7.Enabled = false;
                textBox7.Text = "";
            }
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked)
            {
                textBox7.Enabled = true;
            }
            else
            {
                textBox7.Text = "";
                textBox7.Enabled = false;
            }
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked)
            {
                textBox8.Enabled = true;
            }
            else
            {
                textBox8.Text = "";
                textBox8.Enabled = false;
            }
        }
    }
}
