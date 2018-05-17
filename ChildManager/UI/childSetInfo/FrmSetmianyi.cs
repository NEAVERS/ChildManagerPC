using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ChildManager.BLL.childSetInfo;
using ChildManager.Model.childSetInfo;

namespace ChildManager.UI.childSetInfo
{
    public partial class FrmSetmianyi : Form
    {
        private bool _modfiles = false;
        private childSetMianyiAgeBll bll = new childSetMianyiAgeBll();
        private childSetMianyiAgeObj obj = new childSetMianyiAgeObj();

        private int rowId;
        private int rowIndex;

        public FrmSetmianyi()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSetyiMiao_Load(object sender, EventArgs e)
        {
            RefreshCodedgv();
        }
        /// <summary>
        /// 刷新列表
        /// </summary>
        private void RefreshCodedgv()
        {
            dgvSetmianyiAge.Rows.Clear();
            string sqls = string.Format("select * from tb_mianyiAge");
            ArrayList list = bll.getSetmianyiAgeList(sqls);
            if (list != null && list.Count > 0)
            {
                int i = 1;
                int currentRow = 0;
                foreach (childSetMianyiAgeObj obj in list)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    //DataGridViewCell cell = dgvSetCheckAgeList.Rows[i].Cells[1];
                    if (obj.IsCheck == 1)//检查
                    {
                        row.CreateCells(dgvSetmianyiAge, i.ToString(), obj.MianyiMonth, obj.MianyiContent, obj.Id);
                        row.Tag = obj;
                        dgvSetmianyiAge.Rows.Add(row);
                        //cell.Style.ForeColor = Color.Red;
                        //cell.Style.BackColor = Color.LightGreen;
                        //设置体检  变色
                        dgvSetmianyiAge.Rows[currentRow].Cells[1].Style.ForeColor = Color.Blue;
                    }
                    else if (obj.IsCheck == 2)//不检查
                    {
                        row.CreateCells(dgvSetmianyiAge, i.ToString(), obj.MianyiMonth, obj.MianyiContent, obj.Id);
                        row.Tag = obj;
                        dgvSetmianyiAge.Rows.Add(row);
                        //dgvSetCheckAgeList.Rows[currentRow].Cells[1].Style.ForeColor = Color.Blue;
                    }
                    this.dgvSetmianyiAge.Rows[0].Selected = false;//默认不选中行
                    i++;
                    currentRow++;
                }
            }
        }

        /// <summary>
        /// 记录选中行id  和行索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSetmianyiAge_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowId = Convert.ToInt32(dgvSetmianyiAge.CurrentRow.Cells[3].Value.ToString());
            rowIndex = dgvSetmianyiAge.CurrentRow.Index;
        }

        /// <summary>
        /// 设置免疫或不免疫状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnmianyi_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string sqls = "";
            if (rowId == 0)
            {
                MessageBox.Show("请选择要修改的行", "软件提示");
                return;
            }
            if (btn.Text == "免  疫")
            {
                sqls = updateCheckAge(rowId, 1);
                if (bll.updaterecord(sqls) > 0)
                {
                    RefreshCodedgv();
                }
                else
                {
                    MessageBox.Show("修改失败！", "软件提示");
                }
            }
            else if (btn.Text == "不免疫")
            {
                sqls = updateCheckAge(rowId, 2);
                if (bll.updaterecord(sqls) > 0)
                {
                    RefreshCodedgv();
                }
                else
                {
                    MessageBox.Show("修改失败！", "软件提示");
                }
            }
        }

        /// <summary>
        /// 修改  是否免疫
        /// </summary>
        private string updateCheckAge(int _rowId, int state)
        {
            StringBuilder builder = new StringBuilder();
            childSetMianyiAgeObj obj = new  childSetMianyiAgeObj();
            if (state == 1)
            {
                obj.MianyiContent = "免疫";
            }
            else if (state == 2)
            {
                obj.MianyiContent = "不免疫";
            }
            builder.Append("update tb_mianyiAge set  ");
            builder.Append("mianyiContent='" + obj.MianyiContent + "',");
            builder.Append("isCheck=" + state + "");
            builder.Append(" where id=" + rowId + "");
            return builder.ToString();
        }
        /// <summary>
        /// 不免疫
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNomianyi_Click(object sender, EventArgs e)
        {
            btnmianyi_Click(btnNomianyi, new EventArgs());
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_modfiles)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }


    }
}
