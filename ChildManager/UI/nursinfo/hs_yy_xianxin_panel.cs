using System;
using System.Windows.Forms;
using YCF.Common;
using YCF.BLL;
using YCF.Model;
using YCF.BLL.cepingshi;
using ChildManager.UI.printrecord.cepingshi;
using System.Collections.Generic;
using YCF.BLL.nursinfo;

namespace ChildManager.UI.nursinfo
{
    public partial class hs_yy_xianxin_panel : UserControl
    {
        hs_yy_xx_tabbll bll = new hs_yy_xx_tabbll();
        hs_WomenInfo _hswomeninfo = null;
        public hs_yy_xianxin_panel(hs_WomenInfo hswomeninfo)
        {
            InitializeComponent();
            _hswomeninfo = hswomeninfo;
            CommonHelper.SetAllControls(panel1);
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            RefreshCheckList();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            saveRecord();
        }

        /// <summary>
        /// 保存儿童体检项目信息
        /// </summary>
        public void saveRecord()
        {
            if (_hswomeninfo.cd_id == -1)
            {
                MessageBox.Show("请先保存儿童基本信息", "系统提示");
                return;
            }

            HS_YY_XX_TAB obj = GetObj();

            if (bll.SaveOrUpdate(obj))
            {
                MessageBox.Show("保存成功！", "软件提示");
                RefreshCheckList();
            }
            else
            {
                MessageBox.Show("保存失败！", "软件提示");
            }
        }

        /// <summary>
        /// 体检保存
        /// </summary>
        /// <returns></returns>
        private HS_YY_XX_TAB GetObj()
        {
            HS_YY_XX_TAB obj = CommonHelper.GetObj<HS_YY_XX_TAB>(panel1.Controls);
            obj.CHILD_ID = _hswomeninfo.cd_id;
            if (dataGridView2.SelectedRows.Count > 0)
            {
                HS_YY_XX_TAB qttab = dataGridView2.SelectedRows[0].Tag as HS_YY_XX_TAB;
                obj.ID = qttab.ID;
            }
            return obj;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCheckList()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (_hswomeninfo.cd_id != -1)
            {
                IList<HS_YY_XX_TAB> checklist = bll.GetList(_hswomeninfo.cd_id);
                if (checklist != null)
                {
                    foreach (HS_YY_XX_TAB checkobj in checklist)
                    {
                        string sq = "";
                        string sh = "";
                        if (checkobj.YY_SQSH == "术前")
                            sq = "是";
                        else if (checkobj.YY_SQSH == "术后")
                            sh = "是";
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView2, checkobj.YY_RQ, sq,sh, checkobj.YY_BZ,checkobj.YY_XC);
                        row.Tag = checkobj;
                        dataGridView2.Rows.Add(row);
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
            HS_YY_XX_TAB _checkobj = dataGridView2.SelectedRows[0].Tag as HS_YY_XX_TAB;
            if (_checkobj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (bll.Delete(_checkobj.ID))
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
                HS_YY_XX_TAB checkobj = dataGridView2.SelectedRows[0].Tag as HS_YY_XX_TAB;
                CommonHelper.setForm(checkobj, panel1.Controls);
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            yy_rq.Value = DateTime.Now;
            yy_bz.Text = "";
            yy_xc.Text = "";
        }
    }
}
