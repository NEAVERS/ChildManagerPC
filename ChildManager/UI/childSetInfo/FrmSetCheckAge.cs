using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.BLL.childSetInfo;
using ChildManager.Model.childSetInfo;
using System.Collections;
using ChildManager.UI.jibenluru;

namespace ChildManager.UI.childSetInfo
{
    public partial class FrmSetCheckAge : Form
    {
        private childSetCheckAgeBll bll = new childSetCheckAgeBll();
        private childSetCheckAgeObj obj = new childSetCheckAgeObj();
        ChildMainForm mainFrm = new ChildMainForm();
        private bool _modfiles = false;
        public FrmSetCheckAge()
        {
            InitializeComponent();
        }
        private int rowId;
        private int rowIndex;
        /// <summary>
        /// 加载体检年龄月份信息列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSetCheckAge_Load(object sender, EventArgs e)
        {
            RefreshCodedgv();
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        private void RefreshCodedgv()
        {
            dgvSetCheckAgeList.Rows.Clear();
            Cursor.Current = Cursors.WaitCursor;
            string sqls = string.Format("select * from tb_setcheckAge");
            ArrayList list = bll.getSetCheckAgeList(sqls);
            try
            {
                if (list != null && list.Count > 0)
                {
                    int i = 1;
                    int currentRow = 0;
                    foreach (childSetCheckAgeObj obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        //DataGridViewCell cell = dgvSetCheckAgeList.Rows[i].Cells[1];
                        if (obj.IsCheck == 1)//检查
                        {
                            row.CreateCells(dgvSetCheckAgeList, i.ToString(), obj.CheckMonth, obj.CheckContent, obj.ID);
                            row.Tag = obj;
                            dgvSetCheckAgeList.Rows.Add(row);
                            //cell.Style.ForeColor = Color.Red;
                            //cell.Style.BackColor = Color.LightGreen;
                            //设置体检  变色
                            dgvSetCheckAgeList.Rows[currentRow].Cells[1].Style.ForeColor = Color.Blue;
                        }
                        else if (obj.IsCheck == 2)//不检查
                        {
                            row.CreateCells(dgvSetCheckAgeList, i.ToString(), obj.CheckMonth, obj.CheckContent, obj.ID);
                            row.Tag = obj;
                            dgvSetCheckAgeList.Rows.Add(row);
                            //dgvSetCheckAgeList.Rows[currentRow].Cells[1].Style.ForeColor = Color.Blue;
                        }
                        this.dgvSetCheckAgeList.Rows[0].Selected = false;//默认不选中行
                        i++;
                        currentRow++;
                    }
                }
            }
            finally 
            {
                //dgvSetCheckAgeList.ClearSelection();
                Cursor.Current = Cursors.Default;
            }

        }

        private void dgvSetCheckAgeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowId =Convert.ToInt32( dgvSetCheckAgeList.CurrentRow.Cells[3].Value.ToString());
            rowIndex = dgvSetCheckAgeList.CurrentRow.Index;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string sqls = "";
            if (rowId == 0)
            {
                MessageBox.Show("请选择要修改的行","软件提示");
                return;
            }
            if (btn.Text == "体  检")
            {
                sqls = updateCheckAge(rowId, 1);
                if (bll.updaterecord(sqls) > 0)
                {
                    _modfiles = true;
                    RefreshCodedgv();
                    //Paint += new PaintEventHandler(checkInfo.PaneljibenChildCheckInfo_Paint);
                    //mainFrm.Refresh();
                    //checkInfo.RefreshCodeList();
                }
                else
                {
                    MessageBox.Show("修改失败！","软件提示");
                } 
            }
            else if (btn.Text == "不体检")
            { 
                sqls = updateCheckAge(rowId, 2);
                if (bll.updaterecord(sqls) > 0)
                {
                    _modfiles = true;
                    RefreshCodedgv();
                    mainFrm.Refresh();

                }
                else
                {
                    MessageBox.Show("修改失败！", "软件提示");
                }
            }

        }

        /// <summary>
        /// 修改  体检
        /// </summary>
        private string updateCheckAge(int _rowId,int state)
        {
            StringBuilder builder = new StringBuilder();
            childSetCheckAgeObj obj = new childSetCheckAgeObj();
            if (state == 1)
            {
                obj.CheckContent = "体检";
            }
            else if (state == 2)
            {
                obj.CheckContent = "不体检";
            }
            builder.Append("update tb_setcheckAge set  ");
            builder.Append("checkContent='" + obj.CheckContent + "',");
            builder.Append("isCheck=" + state + "");
            builder.Append(" where id=" + rowId + "");
            return builder.ToString();
        }

        /// <summary>
        /// 不体检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNoCheck_Click(object sender, EventArgs e)
        {
           btnCheck_Click(btnNoCheck,new EventArgs());
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
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

        void FrmSetCheckAge_Paint(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FrmSetCheckAge_FormClosing(object sender, FormClosingEventArgs e)
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
