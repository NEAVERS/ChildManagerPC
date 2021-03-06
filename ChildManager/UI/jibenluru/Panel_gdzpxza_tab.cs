﻿using System;
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
using login.UI.printrecord;
using ChildManager.UI.printrecord;

namespace ChildManager.UI.jibenluru
{
    public partial class Panel_gdzpxza_tab : UserControl
    {
        private WomenInfo _womeninfo;
        private ys_gdzpxza_tabbll blls = new ys_gdzpxza_tabbll();
        public Panel_gdzpxza_tab(WomenInfo womeninfo)
        {
            InitializeComponent();
            _womeninfo = womeninfo;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            YS_GDZPXZA_TAB tab = getObj();
            if (blls.SaveOrUpdate(tab))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private YS_GDZPXZA_TAB getObj()
        {
            YS_GDZPXZA_TAB _obj = blls.GetByCdId(_womeninfo.cd_id);
            YS_GDZPXZA_TAB obj = CommonHelper.GetObjMenzhen<YS_GDZPXZA_TAB>(panel1.Controls, _womeninfo.cd_id);
            if (_obj != null)
            {
                obj.ID = _obj.ID;
                obj.TESTNAME = _obj.TESTNAME;
                obj.TESTDATE = _obj.TESTDATE;
            }
            return obj;
        }

        private void Panel_gdzpxza_tab_Load(object sender, EventArgs e)
        {
            RefreshCode();
        }

        private void RefreshCode()
        {
  Cursor.Current = Cursors.Default;
            YS_GDZPXZA_TAB _obj = blls.GetByCdId(_womeninfo.cd_id);
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

        private void buttonX3_Click(object sender, EventArgs e)
        {
            YS_GDZPXZA_TAB _obj = blls.GetByCdId(_womeninfo.cd_id);
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
            YS_GDZPXZA_TAB _obj = blls.GetByCdId(_womeninfo.cd_id);
            if (_obj == null)
            {
                MessageBox.Show("请保存后再预览打印！", "软件提示");
                return;
            }
            try
            {
                //TB_CHILDBASE baseobj = new tb_childbasebll().Get(_womeninfo.cd_id);
                Panel_ysgdzpxzaPrinter printer = new Panel_ysgdzpxzaPrinter(_obj);
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
    }
}
