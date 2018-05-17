using System;
using System.Windows.Forms;
using YCF.Model;
using System.Collections.Generic;
using YCF.BLL.xunlianshi;
using YCF.BLL.yuyue;

namespace ChildManager.UI.yuyue
{
    public partial class yy_pz_panel : UserControl
    {
        private yy_pz_tabbll yypzbll = new yy_pz_tabbll();//儿童建档基本信息业务处理类
        public yy_pz_panel()
        {
            InitializeComponent();
            this.dataGridView1.Columns[0].ReadOnly = true;
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            RefreshCode();
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode()
        {
            Cursor.Current = Cursors.WaitCursor;

            IList<YY_PZ_TAB> yypzlist = yypzbll.GetList();
            if (yypzlist != null)
            {
                dataGridView1.Rows.Clear();
                foreach (YY_PZ_TAB obj in yypzlist)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    //row.CreateCells(dataGridView1, obj.pz_lx, obj.pz_xq,obj.pz_sjd);
                    row.Tag = obj;
                    dataGridView1.Rows.Add(row);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        YY_PZ_TAB pzobj = dataGridView1.SelectedRows[0].Tag as YY_PZ_TAB;
                        if (yypzbll.Delete(pzobj.ID))
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
    }
}
