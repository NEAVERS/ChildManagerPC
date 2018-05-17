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

            hs_yy_xx_tab obj = GetObj();

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
        private hs_yy_xx_tab GetObj()
        {
            hs_yy_xx_tab obj = CommonHelper.GetObj<hs_yy_xx_tab>(panel1.Controls);
            obj.child_id = _hswomeninfo.cd_id;
            if (dataGridView2.SelectedRows.Count > 0)
            {
                hs_yy_xx_tab qttab = dataGridView2.SelectedRows[0].Tag as hs_yy_xx_tab;
                obj.id = qttab.id;
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
                IList<hs_yy_xx_tab> checklist = bll.GetList(_hswomeninfo.cd_id);
                if (checklist != null)
                {
                    foreach (hs_yy_xx_tab checkobj in checklist)
                    {
                        string sq = "";
                        string sh = "";
                        if (checkobj.yy_sqsh == "术前")
                            sq = "是";
                        else if (checkobj.yy_sqsh == "术后")
                            sh = "是";
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView2, checkobj.yy_rq, sq,sh, checkobj.yy_bz,checkobj.yy_xc);
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
            hs_yy_xx_tab _checkobj = dataGridView2.SelectedRows[0].Tag as hs_yy_xx_tab;
            if (_checkobj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (bll.Delete(_checkobj.id))
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
                hs_yy_xx_tab checkobj = dataGridView2.SelectedRows[0].Tag as hs_yy_xx_tab;
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
