using System;
using System.Windows.Forms;
using YCF.Common;
using YCF.BLL;
using YCF.Model;
using YCF.BLL.cepingshi;
using ChildManager.UI.printrecord.cepingshi;
using System.Collections.Generic;

namespace ChildManager.UI.cepingshi
{
    public partial class cp_yy_cou_panel : UserControl
    {
        private cp_yy_coubll yybll = new cp_yy_coubll();//儿童建档基本信息业务处理类
        public cp_yy_cou_panel()
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

            IList<cp_yy_cou> yylist = yybll.GetList();
            if (yylist != null)
            {
                dataGridView1.Rows.Clear();
                foreach (cp_yy_cou obj in yylist)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1, obj.xmmc, obj.xmsl);
                    row.Tag = obj;
                    dataGridView1.Rows.Add(row);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            cp_yy_cou obj = dataGridView1.Rows[e.RowIndex].Tag as cp_yy_cou;
            obj.xmsl = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

            if (yybll.Update(obj))
            {
                //RefreshRecordList();
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }
    }
}
