using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YCF.BLL.sys;
using YCF.BLL.Template;
using YCF.Model;

namespace ChildManager.UI.sys
{
    public partial class sys_name_list :Form
    {
        tab_person_databll databll = new tab_person_databll();
        string _type = "";
        int data_id = 0;

        public sys_name_list()
        {
            InitializeComponent();

        }


        public string SetType(string type)
        {
            string result = "";
            if (type == "测验者")
            {
                return result = "test";
            }
            else if (type == "送诊医生")
            {
                return result = "songzhen";
            }
            else if (type == "分诊医生")
            {
                return result = "fenzhen";
            }
            return result;
        }

        public string GetType(string type)
        {
            string result = "";
            if (type == "test")
            {
                return result = "测验者";
            }
            else if (type == "songzhen")
            {
                return result = "送诊医生";
            }
            else if (type == "fenzhen")
            {
                return result = "分诊医生";
            }
            return result;
        }

        /// <summary>
        /// 加载查询数据
        /// 2017-12-04 cc
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="type">类型</param>
        public void GetList(string name,string type)
        {
            Cursor.Current = Cursors.WaitCursor;
            IList<TAB_PERSON_DATA> list = null;
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(type))
            {
                list = databll.GetList();
            }
            else
            {
                list = databll.GetList(name, type);
            }
           
            dgvConditionTreatRecordList.Rows.Clear();//清空列表

            try
            {
                if (list != null && list.Count > 0)
                {
                    int i = 1;
                    foreach (TAB_PERSON_DATA obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        string _type = GetType(obj.TYPE);
                        row.CreateCells(dgvConditionTreatRecordList, i.ToString(),obj.ID, obj.NAME, obj.CODE, _type);
                        row.Tag = obj;
                        dgvConditionTreatRecordList.Rows.Add(row);
                        txtnum.Text = i.ToString();
                        i++;
                    }
                }
            }
            finally
            {
                dgvConditionTreatRecordList.ClearSelection();
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            TAB_PERSON_DATA newobj = new TAB_PERSON_DATA();
            newobj.NAME = name.Text.Trim();
            newobj.CODE=code.Text.Trim();
            newobj.TYPE = SetType(type.Text.Trim());
            if (databll.Add(newobj))
            {
                MessageBox.Show("添加成功！");
                GetList(txtName.Text.Trim(), SetType(txtType.Text.Trim()));
            }
            else
            {
                MessageBox.Show("添加失败！");
            }
        }
        //修改数据
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvConditionTreatRecordList.SelectedRows.Count >= 1)
            {
                TAB_PERSON_DATA updataobj = databll.Get(data_id);
                updataobj.NAME = name.Text.Trim();
                updataobj.CODE = code.Text.Trim();
                updataobj.TYPE = SetType(type.Text.Trim());
                if (databll.Update(updataobj))
                {
                    MessageBox.Show("修改成功！");
                    GetList(txtName.Text.Trim(), SetType(txtType.Text.Trim()));
                }
                else
                {
                    MessageBox.Show("修改失败！");
                }

            }
            else
            {
                MessageBox.Show("请选择要修改的行！", "系统提示");
            }
        }
        //删除数据
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvConditionTreatRecordList.SelectedRows.Count >= 1)
            {

                TAB_PERSON_DATA userobj = dgvConditionTreatRecordList.SelectedRows[0].Tag as TAB_PERSON_DATA;

                if (MessageBox.Show("删除所选的记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (databll.Delete(userobj))
                        {
                            GetList(txtName.Text.Trim(), SetType(txtType.Text.Trim()));
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
                MessageBox.Show("请选择要的删除的行！", "系统提示");
            }
        }

        private void yonghuliebiao_Load(object sender, EventArgs e)
        {
            GetList(null,null);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GetList(txtName.Text.Trim(), SetType(txtType.Text.Trim()));
        }

        private void dgvConditionTreatRecordList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexs = dgvConditionTreatRecordList.RowCount;
            if (indexs <= 0)
            {
                return;
            }
            this.data_id = Convert.ToInt32(this.dgvConditionTreatRecordList.SelectedRows[0].Cells[1].Value);//信息id
            TAB_PERSON_DATA dataobj = databll.Get(data_id);
            name.Text = dataobj.NAME;
            code.Text = dataobj.CODE;
            type.Text = GetType(dataobj.TYPE);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            name.Text = "";
            code.Text = "";
            type.Text = "";
            name.Focus();
        }
    }
}

