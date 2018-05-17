using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.Model.childSetInfo;
using ChildManager.BLL.childSetInfo;
using System.Collections;

namespace ChildManager.UI.childSetInfo
{
    public partial class FrmSetbaoJiangGuid : Form
    {
        private bool _modfiles = false;
        private childSetbaoJiangGuidObj obj = new childSetbaoJiangGuidObj();
        private childSetbaoJiangGuidBll bll = new childSetbaoJiangGuidBll();
        int id;
        public FrmSetbaoJiangGuid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载
        /// </summary>
        private void addbaojianList()
        {
            dgvbaojianguid.Rows.Clear();
            Cursor.Current = Cursors.WaitCursor;
            string sqls = string.Format("select * from tb_baojianguid");
            ArrayList list = bll.getbaojianList(sqls);
            try
            {
                if (list != null && list.Count > 0)
                {
                    foreach (childSetbaoJiangGuidObj obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvbaojianguid, obj.Setage, obj.ID);
                        row.Tag = obj;
                        dgvbaojianguid.Rows.Add(row);
                        this.dgvbaojianguid.Rows[0].Selected = true;
                    }
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void FrmSetbaoJiangGuid_Load(object sender, EventArgs e)
        {
            addbaojianList();
        }

        /// <summary>
        /// 切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Click(object sender, EventArgs e)
        {
            //int _rowIndex = Convert.ToInt32(dgvbaojianguid.CurrentRow.Cells[1].Value.ToString());
            //if (tabControl1.SelectedTab == tabPage1)
            //{
            //    addContent(_rowIndex);
            //}
            //else if (tabControl1.SelectedTab == tabPage2)
            //{
            //    addContent(_rowIndex);
            //}
            //else if (tabControl1.SelectedTab == tabPage3)
            //{
            //    addContent(_rowIndex);
            //}
            //else if (tabControl1.SelectedTab == tabPage4)
            //{
            //    addContent(_rowIndex);
            //}
            selectedTabpages();
        }

        /// <summary>
        /// 根据每行的Id 加载对应的设置内容
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="type">类型</param>
        private void addContent(int rowIndex)
        {
            string sqls = string.Format("select * from tb_baojianguid where id = " + rowIndex + "");
            obj = bll.getbaojianobj(sqls);
            if (obj != null)
            {
                this.txt_eduction.Text = obj.Eduction;
                this.txt_shanshi.Text = obj.Shanshi;
                this.txt_jibing.Text = obj.Jibing;
                this.txt_huli.Text = obj.Huli;
                id = obj.ID;
            }
            else
            {
                this.txt_eduction.Text = "";
                this.txt_shanshi.Text = "";
                this.txt_jibing.Text = "";
                this.txt_huli.Text = "";
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        private string getupdatesql()
        {
            StringBuilder builder = new StringBuilder();
            childSetbaoJiangGuidObj obj1 = new childSetbaoJiangGuidObj();
            obj1.Eduction = this.txt_eduction.Text.Trim();
            obj1.Shanshi = this.txt_shanshi.Text.Trim();
            obj1.Jibing = this.txt_jibing.Text.Trim();
            obj1.Huli = this.txt_huli.Text.Trim();

            builder.Append("update tb_baojianguid set ");
            builder.Append("setage='" + obj1.Setage + "',");
            builder.Append("eduction = '"+obj1.Eduction+"',");
            builder.Append("shanshi='" + obj1.Shanshi + "',");
            builder.Append("jibing='" + obj1.Jibing + "',");
            builder.Append("huli='" + obj1.Huli + "'");
            builder.Append(" where id = " + dgvbaojianguid.CurrentRow.Cells[1].Value.ToString() + "");
            return builder.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(dgvbaojianguid.CurrentRow.Cells[1].Value.ToString());
            string updatesql = this.getupdatesql();
            if (bll.updaterecord(updatesql) > 0)
            {
                MessageBox.Show("保存成功！", "软件提示");
                addContent(index);
            }
        }

        private void dgvbaojianguid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int _rowIndex = Convert.ToInt32(dgvbaojianguid.CurrentRow.Cells[1].Value.ToString());
            //if (tabControl1.SelectedTab == tabPage1)
            //{
            //    addContent(_rowIndex);
            //}
            //else if (tabControl1.SelectedTab == tabPage2)
            //{
            //    addContent(_rowIndex);
            //}
            //else if (tabControl1.SelectedTab == tabPage3)
            //{
            //    addContent(_rowIndex);
            //}
            //else if (tabControl1.SelectedTab == tabPage4)
            //{
            //    addContent(_rowIndex);
            //}
            id = Convert.ToInt32(dgvbaojianguid.CurrentRow.Cells[1].Value.ToString());
            selectedTabpages();
        }

        /// <summary>
        /// 选择切换查询
        /// </summary>
        private void selectedTabpages()
        {
            int _rowIndex = Convert.ToInt32(dgvbaojianguid.CurrentRow.Cells[1].Value.ToString());
            if (tabControl1.SelectedTab == tabPage1)
            {
                addContent(_rowIndex);
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                addContent(_rowIndex);
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                addContent(_rowIndex);
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                addContent(_rowIndex);
            }
        }

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
