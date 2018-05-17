using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.BLL.ChildBaseInfo;
using System.Collections;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL;
using System.IO;

namespace ChildManager.UI.jibenluru
{
    public partial class Paneltsb_rowspanInfo : Form
    {
        ChildBaseInfoBll bll = new ChildBaseInfoBll();
        private ChildCheckBll checkbll = new ChildCheckBll();
        private ChildGaoweigeanBll gwbll = new ChildGaoweigeanBll();

        public Paneltsb_rowspanInfo()
        {
            InitializeComponent();
        }

        private void btn_seacher_Click(object sender, EventArgs e)
        {
            seacher_click();
            refreshCode1();  
        }


        private void refreshCode1()
        {
            rowMergeView1.Rows.Clear();
            string num1 = this.txtnumber1.Text.Trim();
            string num2 = this.txtnumber2.Text.Trim();

            string sqls = "select * from TB_CHILDBASE where status=1 and  healthCardNo in ('" + num1 + "','" + num2 + "')";
            sqls += " order by childBuildDay";
            ArrayList list = bll.getchildBaseList1(sqls);
            if (list != null && list.Count > 0)
            {
                try
                {
                    int i = 1;
                    foreach (ChildBaseInfoObj obj in list)
                    {
                        string birthstr = "";
                        string buidstr = "";
                        DataGridViewRow row = new DataGridViewRow();
                        if (i == 1)
                        {
                            row.CreateCells(rowMergeView1, obj.jiuzhenCardNo, obj.linshi,
                            obj.HealthCardNo, obj.ChildName, obj.ChildGender, obj.bloodType, obj.childBirthDay,
                            obj.childBuildDay, obj.childBuildHospital,
                            obj.FatherName, obj.fatherAge, obj.FatherHeight, obj.fatherEducation, obj.FatherJob,
                            obj.motherName, obj.MotherAge, obj.identityno, obj.motherHeight, obj.motherEducation, obj.MotherJob,
                            obj.telephone, obj.address, obj.nurseryInstitution, obj.cs_fetus, obj.cs_produce, obj.cs_week, obj.cs_day,
                            obj.modeDelivery, obj.perineumIncision, obj.neonatalCondition, obj.birthWeight, obj.birthHeight,
                            obj.birthaddress, obj.hospitalizedStates, obj.oneMonth, obj.inFourMonth, obj.fourToSixMonth, obj.gaowei
                            );
                            row.Tag = obj;
                            rowMergeView1.Rows.Add(row);
                        }
                        i++;
                    }
                }
                finally
                {
                    rowMergeView1.ClearSelection();
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        private void seacher_click()
        {
            dataGridView1.Rows.Clear();
            string num1 = this.txtnumber1.Text.Trim();
            string num2 = this.txtnumber2.Text.Trim();

            string sqls = "select * from TB_CHILDBASE where status=1 and  healthCardNo in ('"+num1+"','"+num2+"')";
            sqls += " order by childBuildDay";
            ArrayList list = bll.getchildBaseList1(sqls);
            if (list != null && list.Count > 0)
            {
                try
                {
                    int i = 1;
                    foreach (ChildBaseInfoObj obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1,obj.jiuzhenCardNo,obj.linshi,
                            obj.HealthCardNo, obj.ChildName, obj.ChildGender, obj.bloodType, obj.childBirthDay,
                            obj.childBuildDay, obj.childBuildHospital,
                            obj.FatherName,obj.fatherAge,obj.FatherHeight,obj.fatherEducation,obj.FatherJob,
                            obj.motherName, obj.MotherAge, obj.identityno, obj.motherHeight, obj.motherEducation, obj.MotherJob,
                            obj.telephone,obj.address,obj.nurseryInstitution,obj.cs_fetus,obj.cs_produce,obj.cs_week,obj.cs_day,
                            obj.modeDelivery,obj.perineumIncision,obj.neonatalCondition,obj.birthWeight,obj.birthHeight,
                            obj.birthaddress,obj.hospitalizedStates,obj.oneMonth,obj.inFourMonth,obj.fourToSixMonth,obj.gaowei
                            );
                        row.Tag = obj;
                        dataGridView1.Rows.Add(row);
                        i++;
                    }
                }
                finally
                {
                    dataGridView1.ClearSelection();
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count<=1)
            {
                MessageBox.Show("合并信息至少需要 2 条数据！");
                return;
            }

            ChildBaseInfoObj obj = new ChildBaseInfoObj();
            obj = rowMergeView1.Rows[0].Tag as ChildBaseInfoObj;
            
            obj.jiuzhenCardNo = rowMergeView1.Rows[0].Cells[0].Value.ToString();
            obj.linshi = rowMergeView1.Rows[0].Cells[1].Value.ToString();
            obj.HealthCardNo = rowMergeView1.Rows[0].Cells[2].Value.ToString();
            obj.ChildName = rowMergeView1.Rows[0].Cells[3].Value.ToString();
            obj.ChildGender = rowMergeView1.Rows[0].Cells[4].Value.ToString();
            obj.bloodType = rowMergeView1.Rows[0].Cells[5].Value.ToString();
            obj.childBirthDay = rowMergeView1.Rows[0].Cells[6].Value.ToString();
            obj.childBuildDay = rowMergeView1.Rows[0].Cells[7].Value.ToString();
            obj.childBuildHospital = rowMergeView1.Rows[0].Cells[8].Value.ToString();
            obj.FatherName = rowMergeView1.Rows[0].Cells[9].Value.ToString();
            obj.fatherAge = rowMergeView1.Rows[0].Cells[10].Value.ToString();
            obj.FatherHeight = rowMergeView1.Rows[0].Cells[11].Value.ToString();
            obj.fatherEducation = rowMergeView1.Rows[0].Cells[12].Value.ToString();
            obj.FatherJob = rowMergeView1.Rows[0].Cells[13].Value.ToString();
            obj.motherName = rowMergeView1.Rows[0].Cells[14].Value.ToString();
            obj.MotherAge = rowMergeView1.Rows[0].Cells[15].Value.ToString();
            obj.identityno = rowMergeView1.Rows[0].Cells[16].Value.ToString();
            obj.motherHeight = rowMergeView1.Rows[0].Cells[17].Value.ToString();
            obj.motherEducation = rowMergeView1.Rows[0].Cells[18].Value.ToString();
            obj.MotherJob = rowMergeView1.Rows[0].Cells[19].Value.ToString();
            obj.telephone = rowMergeView1.Rows[0].Cells[20].Value.ToString();
            obj.address = rowMergeView1.Rows[0].Cells[21].Value.ToString();
            obj.nurseryInstitution = rowMergeView1.Rows[0].Cells[22].Value.ToString();
            obj.cs_fetus = rowMergeView1.Rows[0].Cells[23].Value.ToString();
            obj.cs_produce = rowMergeView1.Rows[0].Cells[24].Value.ToString();
            obj.cs_week = rowMergeView1.Rows[0].Cells[25].Value.ToString();
            obj.cs_day = rowMergeView1.Rows[0].Cells[26].Value.ToString();

            obj.modeDelivery = rowMergeView1.Rows[0].Cells[27].Value.ToString();
            obj.perineumIncision = rowMergeView1.Rows[0].Cells[28].Value.ToString();
            obj.neonatalCondition = rowMergeView1.Rows[0].Cells[29].Value.ToString();
            obj.birthWeight = rowMergeView1.Rows[0].Cells[30].Value.ToString();
            obj.birthHeight = rowMergeView1.Rows[0].Cells[31].Value.ToString();
            obj.birthaddress = rowMergeView1.Rows[0].Cells[32].Value.ToString();
            obj.hospitalizedStates = rowMergeView1.Rows[0].Cells[33].Value.ToString();
            obj.oneMonth = rowMergeView1.Rows[0].Cells[34].Value.ToString();
            obj.inFourMonth = rowMergeView1.Rows[0].Cells[35].Value.ToString();
            obj.fourToSixMonth = rowMergeView1.Rows[0].Cells[36].Value.ToString();
            obj.gaowei = rowMergeView1.Rows[0].Cells[37].Value.ToString();

            string[] tableName = { "TB_CHILDCHECK", "child_yingyanggean", "child_yingyanggean_record",
                               "child_pinxuegean_record", "child_goulougean_record","tb_gaowei","tb_gaowei_record"
                                 };
            string sqls = "";
            if (bll.updateChildBaseInfo(obj))
            {
                for (int i = 1; i < dataGridView1.Rows.Count; i++)
                {
                    ChildBaseInfoObj newobj = rowMergeView1.Rows[0].Tag as ChildBaseInfoObj;
                    ChildBaseInfoObj obj1 = dataGridView1.Rows[i].Tag as ChildBaseInfoObj;
                    obj1.status = "0";
                    bll.updatejiben(obj1);//基本信息
                    for (int n = 0; n < tableName.Length; n++)
                    {
                        sqls = "update " + tableName[n] + " set childId=" + newobj.id + " where childId=" + obj1.id + "";
                        checkbll.deleterecord(sqls);
                    }
                  
                }
                MessageBox.Show("合并成功！");
                seacher_click();
                refreshCode1();
            }
            else
            {
                MessageBox.Show("合并失败！","系统提示");
                return;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }

            string value = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            this.rowMergeView1.Rows[0].Cells[e.ColumnIndex].Value = value;
        }


        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if(rowMergeView1.FirstDisplayedScrollingRowIndex!=-1&& dataGridView1.FirstDisplayedScrollingRowIndex!=-1)
            { 
            rowMergeView1.FirstDisplayedScrollingRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;
            rowMergeView1.HorizontalScrollingOffset = dataGridView1.HorizontalScrollingOffset;
            }
        }

        private void rowMergeView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (rowMergeView1.FirstDisplayedScrollingRowIndex != -1 && dataGridView1.FirstDisplayedScrollingRowIndex != -1)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = rowMergeView1.FirstDisplayedScrollingRowIndex;
                dataGridView1.HorizontalScrollingOffset = rowMergeView1.HorizontalScrollingOffset;
            }
        }


    }
}
