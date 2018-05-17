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
    public partial class FrmSetNutritionguid : Form
    {
        private bool _modfiles = false;
        private childSetNutritionguidObj obj = new childSetNutritionguidObj();
        private childSetNutritionguidBll bll = new childSetNutritionguidBll();
        int id;
        public FrmSetNutritionguid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSetNutritionguid_Load(object sender, EventArgs e)
        {
            //加载年龄段
            adddgvList();
            //tabControl1_Click(tabPage1, new EventArgs());
        }

        /// <summary>
        /// 切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl1_Click(object sender, EventArgs e)
        {
            //int _rowIndex = Convert.ToInt32(dgvNutrition.Rows[0].Cells[1].Value.ToString());
            int _rowIndex = Convert.ToInt32(dgvNutrition.CurrentRow.Cells[1].Value.ToString());
            if (tabControl1.SelectedTab == tabPage1)
            {
                addContent(_rowIndex);
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                addContent(_rowIndex);
            }
        }
        /// <summary>
        /// 加载年龄段
        /// </summary>
        private void adddgvList()
        {
            dgvNutrition.Rows.Clear();
            Cursor.Current = Cursors.WaitCursor;
            string sqls = string.Format("select id,setAge from tb_nutritionguid");
            ArrayList list = bll.getNutritionguidList(sqls);
            try
            {
                if (list != null && list.Count > 0)
                {
                    int i = 1;
                    foreach (childSetNutritionguidObj obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvNutrition, obj.SetAge, obj.Id);
                        row.Tag = obj;
                        dgvNutrition.Rows.Add(row);
                        this.dgvNutrition.Rows[0].Selected = true;
                        i++;
                    }
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 根据每行的Id 加载对应的设置内容
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="type">类型</param>
        private void addContent(int rowIndex)
        {
            string sqls = string.Format("select * from tb_nutritionguid where id = " + rowIndex + "");
            obj = bll.getNutritionguidobj(sqls);
            if (obj != null)
            {
                 this.txt_shiyingxing.Text = obj.Shiyongxing;
                 this.txt_big.Text = obj.Bigdongzuo;
                 id = obj.Id;
            }
            else
            {
                    this.txt_shiyingxing.Text = "";
                    this.txt_big.Text = "";
            }
        }

        /// <summary>
        /// 单击一行  加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvNutrition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = Convert.ToInt32(dgvNutrition.CurrentRow.Cells[1].Value.ToString());
            if (tabControl1.SelectedTab == tabPage1)
            {
                addContent(index);
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                addContent(index);
            }
        
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string getupdatesql()
        {
            StringBuilder builder = new StringBuilder();
            childSetNutritionguidObj obj1 = new childSetNutritionguidObj();
                obj1.SetAge = dgvNutrition.CurrentRow.Cells[0].Value.ToString();
                obj1.Shiyongxing = this.txt_shiyingxing.Text.Trim();
                obj1.Bigdongzuo = this.txt_big.Text.Trim();

            builder.Append("update tb_nutritionguid set ");
            builder.Append("setAge='"+obj1.SetAge+"',");
            builder.Append("shiyongxing='" + obj1.Shiyongxing + "',");
            builder.Append("bigdongzuo='" + obj1.Bigdongzuo + "' ");
            builder.Append(" where id = "+ id +"");
            return builder.ToString();
        }

        //保存
        private string getinsertsql(int type)
        {
            StringBuilder builder = new StringBuilder();
            childSetNutritionguidObj obj1 = new childSetNutritionguidObj();
            if (type == 1)
            {
                obj1.SetAge = dgvNutrition.CurrentRow.Cells[0].Value.ToString();
                obj1.SetContent = this.txt_shiyingxing.Text.Trim();
            }
            else if (type == 2)
            {
                obj1.SetAge = dgvNutrition.CurrentRow.Cells[0].Value.ToString();
                obj1.SetContent = this.txt_big.Text.Trim();
            }
            obj1.Type = type;
            builder.Append("insert  into tb_nutritionguid (");
            builder.Append("setAge,");
            builder.Append("shiyongxing,");
            builder.Append("bigdongzuo,");
            builder.Append("type ");
            builder.Append(") values (");
            builder.Append("'"+obj1.SetAge+"',");
            builder.Append("'" + obj1.Shiyongxing + "',");
            builder.Append("'" + obj1.Bigdongzuo + "',");
            builder.Append("'" + obj1.Type + "' )");
            return builder.ToString();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
             int index  = Convert.ToInt32(dgvNutrition.CurrentRow.Cells[1].Value.ToString());
             string updatesql = this.getupdatesql();
             if (bll.updaterecord(updatesql) > 0)
             {
                 MessageBox.Show("保存成功！", "软件提示");
                 addContent(index);
             }

             //if (tabControl1.SelectedTab == tabPage1)
             //{
             //    //string sqls = string.Format("select * from tb_nutritionguid where id = " + index + " and type = " + 1 + "");
             //    //obj = bll.getNutritionguidobj(sqls);
             //    //if (obj == null)
             //    //{
             //    //    string insertsql = this.getinsertsql(1);
             //    //    if (bll.saverecord(insertsql))
             //    //    {
             //    //        MessageBox.Show("保存成功！", "软件提示");
             //    //        addContent(index, 1);
             //    //    }
             //    //}
             //    //else
             //    //{
             //    //    string updatesql = this.getupdatesql(1);
             //    //    if (bll.updaterecord(updatesql) > 0)
             //    //    {
             //    //        MessageBox.Show("保存成功！", "软件提示");
             //    //        addContent(index, 1);
             //    //    }
             //    //}
             //    string updatesql = this.getupdatesql();
             //    if (bll.updaterecord(updatesql) > 0)
             //    {
             //        MessageBox.Show("保存成功！", "软件提示");
             //        addContent(index, 1);
             //    }
             //}
             //else if (tabControl1.SelectedTab == tabPage2)
             //{
             //    //string sqls = string.Format("select * from tb_nutritionguid where id = " + index + " and type = " + 2 + "");
             //    //obj = bll.getNutritionguidobj(sqls);
             //    //if (obj == null)
             //    //{
             //    //    string insertsql = this.getinsertsql(1);
             //    //    if (bll.saverecord(insertsql))
             //    //    {
             //    //        MessageBox.Show("保存成功！", "软件提示");
             //    //        addContent(index, 1);
             //    //    }
             //    //}
             //    //else
             //    //{
             //    //}
             //        string updatesql = this.getupdatesql();
             //        if (bll.updaterecord(updatesql) > 0)
             //        {
             //            MessageBox.Show("保存成功！", "软件提示");
             //            addContent(index, 2);
             //        }
             //}
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

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
