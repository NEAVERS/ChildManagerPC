using System;
using System.Windows.Forms;
using YCF.Common;
using YCF.BLL;
using YCF.Model;
using YCF.BLL.cepingshi;
using ChildManager.UI.printrecord.cepingshi;
using System.Collections.Generic;

namespace ChildManager.UI.nursinfo
{
    public partial class hs_tijian_panel : UserControl
    {
        tb_childcheckbll checkbll = new tb_childcheckbll();
        hs_WomenInfo _hswomeninfo = null;
        public hs_tijian_panel(hs_WomenInfo hswomeninfo)
        {
            InitializeComponent();
            _hswomeninfo = hswomeninfo;
            CommonHelper.SetAllControls(panel1);
            CommonHelper.setCombValue(ck_fz, "fenzhen.xml");
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            RefreshCheckList();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            saveChildCheck();
        }

        /// <summary>
        /// 保存儿童体检项目信息
        /// </summary>
        public void saveChildCheck()
        {
            if (_hswomeninfo.cd_id == -1)
            {
                MessageBox.Show("请先保存儿童基本信息", "系统提示");
                return;
            }
            if (String.IsNullOrEmpty(checkheight.Text.Trim()))
            {
                MessageBox.Show("请填写儿童身高后再保存", "系统提示");
                checkheight.Focus();
                return;
            }
            if (String.IsNullOrEmpty(checkweight.Text.Trim()))
            {
                MessageBox.Show("请选择儿童体重后再保存", "系统提示");
                checkweight.Focus();
                return;
            }

            TB_CHILDCHECK checkobj = GetObj();
            TB_CHILDCHECK _checkobj = checkbll.Get(checkobj.CHECKDAY, (int)checkobj.CHILDID);
            if (_checkobj == null)
            {
                if (checkbll.Add(checkobj))
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    _checkobj = checkobj;
                    RefreshCheckList();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            else
            {
                checkobj.ID = _checkobj.ID;
                if (checkbll.UpdateNurse(checkobj))
                {
                    MessageBox.Show("保存成功！", "软件提示");
                    _checkobj = checkobj;
                    RefreshCheckList();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
        }

        /// <summary>
        /// 体检保存
        /// </summary>
        /// <returns></returns>
        private TB_CHILDCHECK GetObj()
        {
            TB_CHILDCHECK obj = CommonHelper.GetObj<TB_CHILDCHECK>(panel1.Controls);
            obj.CHILDID = _hswomeninfo.cd_id;
            return obj;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCheckList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataGridView2.Rows.Clear();
            if (_hswomeninfo.cd_id != -1)
            {
                IList<TB_CHILDCHECK> checklist = checkbll.GetList(_hswomeninfo.cd_id);
                if (checklist != null)
                {
                    foreach (TB_CHILDCHECK checkobj in checklist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView2, checkobj.CHECKDAY, checkobj.CHECKWEIGHT, checkobj.CHECKHEIGHT, checkobj.CHECKTOUWEI, checkobj.checkzuogao, checkobj.CK_FZ);
                        row.Tag = checkobj;
                        dataGridView2.Rows.Add(row);
                        if (checkobj.CHECKDAY == checkday.Text)
                        {
                            row.Selected = true;
                        }
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }


        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的记录！");
                return;
            }
            TB_CHILDCHECK _checkobj = dataGridView2.SelectedRows[0].Tag as TB_CHILDCHECK;
            if (_checkobj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (checkbll.Delete(_checkobj.ID))
                        {
                            MessageBox.Show("删除成功!", "软件提示");
                            RefreshCheckList();
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
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                TB_CHILDCHECK checkobj = dataGridView2.SelectedRows[0].Tag as TB_CHILDCHECK;
                CommonHelper.setForm(checkobj, panel1.Controls);
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            checkday.Value = DateTime.Now;
            checkweight.Text = "";
            checkheight.Text = "";
            checktouwei.Text = "";
            checkzuogao.Text = "";
        }
    }
}
