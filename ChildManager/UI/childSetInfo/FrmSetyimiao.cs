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

namespace ChildManager.UI.childSetInfo
{
    public partial class FrmSetyimiao : Form
    {
        private bool _modfiles = false;
        private childSetyimiaoBll bll = new childSetyimiaoBll();
        private childSetyimiaoObj obj = new childSetyimiaoObj();
        public FrmSetyimiao()
        {
            InitializeComponent();
        }
        int rowId;

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetdefaultControl();
        }
        /// <summary>
        /// 设置控件初始状态
        /// </summary>
        private void SetdefaultControl()
        {
            if (dgvyimiao.Rows.Count >= 0)
            {
                dgvyimiao.ClearSelection();   
            }
            this.txt_yimiaoName.Text = "";
            this.dtp_production.Value = DateTime.Now;
            this.cbx_factorName.Text = "";
            this.txt_pihao.Text = "";
            this.txt_guige.Text = "";
            this.cbx_jiliang.Text = "";
            this.txt_month.Text = "";
            rowId = 0;
        }

        /// <summary>
        /// 加载年龄段
        /// </summary>
        private void adddgvList()
        {
            dgvyimiao.Rows.Clear();
            Cursor.Current = Cursors.WaitCursor;
            string sqls = string.Format("select * from tb_setyimiao");
            ArrayList list = bll.getyimiaoList(sqls);
            try
            {
                if (list != null && list.Count > 0)
                {
                    foreach (childSetyimiaoObj obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvyimiao, obj.YimiaoName, obj.ProductionDay, 
                            obj.FactorName, obj.Pihao,
                            obj.Guige,obj.JiliangUnit,obj.Month,obj.Id);
                        row.Tag = obj;
                        dgvyimiao.Rows.Add(row);
                    }
                }
            }
            finally
            {
                dgvyimiao.ClearSelection();
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <returns></returns>
        private string getinsertsql()
        {
            StringBuilder builder = new StringBuilder();
            childSetyimiaoObj obj1 = new childSetyimiaoObj();
            obj1.YimiaoName = this.txt_yimiaoName.Text.Trim();
            obj1.ProductionDay = dtp_production.Value.ToString("yyyy-MM-dd");
            obj1.FactorName = this.cbx_factorName.Text.Trim();
            obj1.Pihao = this.txt_pihao.Text.Trim();
            obj1.Guige = this.txt_guige.Text.Trim();
            obj1.JiliangUnit = this.cbx_jiliang.Text.Trim();
            obj1.Month = this.txt_month.Text.Trim();

            builder.Append("insert into tb_setyimiao");
            builder.Append("(");
            builder.Append("yimiaoName");
            builder.Append(",productionDay");
            builder.Append(",factorName");
            builder.Append(",pihao");
            builder.Append(",guige");
            builder.Append(",jiliangUnit");
            builder.Append(",month");
            builder.Append(" ) values ( ");
            builder.Append("'"+obj1.YimiaoName+"'");
            builder.Append(",'" + obj1.ProductionDay + "'");
            builder.Append(",'" + obj1.FactorName + "'");
            builder.Append(",'" + obj1.Pihao+ "'");
            builder.Append(",'" + obj1.Guige + "'");
            builder.Append(",'" + obj1.JiliangUnit + "'");
            builder.Append(",'" + obj1.Month + "')");

            return builder.ToString();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        private string getupdatesql()
        {
            StringBuilder builder = new StringBuilder();
            childSetyimiaoObj obj1 = new childSetyimiaoObj();
            obj1.YimiaoName = this.txt_yimiaoName.Text.Trim();
            obj1.ProductionDay = dtp_production.Value.ToString("yyyy-MM-dd");
            obj1.FactorName = this.cbx_factorName.Text.Trim();
            obj1.Pihao = this.txt_pihao.Text.Trim();
            obj1.Guige = this.txt_guige.Text.Trim();
            obj1.JiliangUnit = this.cbx_jiliang.Text.Trim();
            obj1.Month = this.txt_month.Text.Trim();

            builder.Append("update tb_setyimiao set ");
            builder.Append("yimiaoName = '" + obj1.YimiaoName + "'");
            builder.Append(",productionDay = '" + obj1.ProductionDay + "'");
            builder.Append(",factorName = '" + obj1.FactorName + "'");
            builder.Append(",pihao = '" + obj1.Pihao + "'");
            builder.Append(",guige = '" + obj1.Guige + "'");
            builder.Append(",jiliangUnit = '" + obj1.JiliangUnit + "'");
            builder.Append(",month = '" + obj1.Month + "'  where id = " + rowId + "");
            return builder.ToString();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string sqls = string.Format("select * from tb_setyimiao where id="+rowId+"");
            childSetyimiaoObj obj2 = bll.getyimiaoobj(sqls);
            if (obj2 == null)
            {
                string insertsql = this.getinsertsql();
                if (bll.saverecord(insertsql))
                {
                    MessageBox.Show("保存成功！","软件提示");
                    adddgvList();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
            else
            {
                string updatesql = this.getupdatesql();
                if (bll.updaterecord(updatesql) > 0)
                {
                   MessageBox.Show("保存成功！","软件提示");
                    adddgvList();
                }
                else
                {
                    MessageBox.Show("保存失败！", "软件提示");
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvyimiao.Rows.Count >= 0)
            {
                string sqls = string.Format("delete from tb_setyimiao where id="+rowId+"");
                if (bll.deleterecord(sqls) > 0)
                {
                    MessageBox.Show("删除成功！","软件提示");
                    adddgvList();
                    SetdefaultControl();
                }
                else
                {
                    MessageBox.Show("删除失败！","软件提示");
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的行！","软件提示");
                return;
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

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvyimiao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             rowId =Convert.ToInt32(dgvyimiao.CurrentRow.Cells[7].Value.ToString());
             string sqls = string.Format("select * from tb_setyimiao where id="+rowId+"");
             childSetyimiaoObj obj2 = bll.getyimiaoobj(sqls);
             if (obj2 != null)
             {
                 this.txt_yimiaoName.Text = obj2.YimiaoName;
                 dtp_production.Value =Convert.ToDateTime(obj2.ProductionDay);
                 this.cbx_factorName.Text = obj2.FactorName;
                 this.txt_pihao.Text = obj2.Pihao;
                 this.txt_guige.Text = obj2.Guige;
                 this.cbx_jiliang.Text = obj2.JiliangUnit;
                 this.txt_month.Text = obj2.Month;
             }
        }

        private void FrmSetyimiao_Load(object sender, EventArgs e)
        {
            adddgvList();
        }

        //回车代替tab键盘
        private void all_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }

        }
        private void all_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }
    }
}
