using System;
using System.Windows.Forms;
using YCF.Model;
using System.Collections.Generic;
using YCF.BLL.xunlianshi;

namespace ChildManager.UI.xunlianshi
{
    public partial class xl_yy_cou_panel : UserControl
    {
        private xl_yy_coubll xlbll = new xl_yy_coubll();//儿童建档基本信息业务处理类
        public xl_yy_cou_panel()
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

            IList<XL_YY_COU> yylist = xlbll.GetList();
            if (yylist != null)
            {
                dataGridView1.Rows.Clear();
                foreach (XL_YY_COU obj in yylist)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1, obj.XMMC, obj.XMSL);
                    row.Tag = obj;
                    dataGridView1.Rows.Add(row);
                }
            }
            Cursor.Current = Cursors.Default;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            XL_YY_COU obj = dataGridView1.Rows[e.RowIndex].Tag as XL_YY_COU;
            obj.XMSL = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

            if (xlbll.Update(obj))
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
