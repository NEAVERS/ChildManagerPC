using System;
using System.Windows.Forms;
using YCF.Common;
using YCF.BLL;
using YCF.Model;
using YCF.BLL.cepingshi;
using ChildManager.UI.printrecord.yuyue;
using System.Collections.Generic;
using YCF.BLL.nursinfo;
using YCF.BLL.xunlianshi;
using YCF.BLL.yuyue;

namespace ChildManager.UI.yuyue
{
    public partial class yy_xlk_panel : UserControl
    {
        yy_asd_tabbll bll = new yy_asd_tabbll();
        yy_pz_tabbll pzbll = new yy_pz_tabbll();
        yy_asd_tab _obj = null;
        string _yyxm = "";
        public yy_xlk_panel(string yy_xm)
        {
            InitializeComponent();
            CommonHelper.SetAllControls(panel1);
            dateTimePicker2.Value = DateTime.Now.AddDays(7);
            _yyxm = yy_xm;
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            RefreshCheckList();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            saveRecord();
        }

        /// <summary>
        /// 保存儿童体检项目信息
        /// </summary>
        public void saveRecord()
        {

            _obj = GetObj();

            if (bll.SaveOrUpdate(_obj))
            {
                MessageBox.Show("保存成功！", "软件提示");
                RefreshCheckList();
            }
            else
            {
                MessageBox.Show("保存失败！", "软件提示");
            }
        }

        /// <summary>
        /// 体检保存
        /// </summary>
        /// <returns></returns>
        private yy_asd_tab GetObj()
        {
            yy_asd_tab obj = CommonHelper.GetObj<yy_asd_tab>(panel2.Controls);
            if (dataGridView2.SelectedRows.Count > 0)
            {
                yy_asd_tabNotMap qttab = dataGridView2.SelectedRows[0].Tag as yy_asd_tabNotMap;
                obj.id = qttab.id;
                obj.child_id = qttab.child_id;
                obj.yy_djrq = qttab.yy_djrq;
                obj.yy_xm = _yyxm;
            }
            return obj;
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCheckList()
        {
            Cursor.Current = Cursors.WaitCursor;
            dataGridView2.Rows.Clear();
            string startdate = dateTimePicker1.Text;
            string enddate = dateTimePicker2.Text;
                IList<yy_asd_tabNotMap> checklist = bll.GetListByTime(startdate,enddate,_yyxm);
                if (checklist != null&&checklist.Count>0)
                {
                int i = 0;
                int selectindex = 0;
                    foreach (yy_asd_tabNotMap obj in checklist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView2, obj.yy_djrq,obj.yy_bh,obj.childname,obj.childgender, obj.childbirthday,obj.telephone,obj.yy_rq, obj.yy_sjd,obj.yy_sfjf, obj.yy_bz);
                        row.Tag = obj;
                        dataGridView2.Rows.Add(row);
                    if (_obj != null && obj.id == _obj.id)
                        selectindex = i;
                    i++;
                    }
                dataGridView2.Rows[selectindex].Selected = true;
                dataGridView2_CellClick(null,new DataGridViewCellEventArgs(0,selectindex));
                }
            viewcountlabel.Text = dataGridView2.Rows.Count.ToString();
            Cursor.Current = Cursors.Default;
        }


        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的记录！");
                return;
            }
            yy_asd_tabNotMap _checkobj = dataGridView2.SelectedRows[0].Tag as yy_asd_tabNotMap;
            if (_checkobj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (bll.Delete(_checkobj.id))
                        {
                            MessageBox.Show("删除成功!", "软件提示");
                            RefreshCheckList();
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
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                yy_asd_tabNotMap obj = dataGridView2.SelectedRows[0].Tag as yy_asd_tabNotMap;
                CommonHelper.setForm(obj, panel2.Controls);
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            yy_asd_tab yyobj = CommonHelper.GetObjMenzhen<yy_asd_tab>(panel2.Controls, 0);
            yyobj.yy_bh = "";
            yyobj.yy_bz = "";
            yyobj.yy_djrq = DateTime.Now.ToString("yyyy-MM-dd");
            yyobj.yy_rq = DateTime.Now.ToString("yyyy-MM-dd");
            yyobj.yy_sfjf = "";
            yyobj.yy_sjd = "";
            yyobj.yy_xm =_yyxm;
            bll.SaveOrUpdate(yyobj);
            _obj = yyobj;
            RefreshCheckList();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            RefreshCheckList();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            yy_pz_tab pzobj = pzbll.GetByXm(_yyxm);
            DateTime starttime = dateTimePicker1.Value;
            DateTime endtime = dateTimePicker2.Value;
            int daycount = (endtime - starttime).Days;
            if (pzobj!=null)
            {
                yy_asd_tab yyobj = CommonHelper.GetObjMenzhen<yy_asd_tab>(panel2.Controls, 0);
                if(!String.IsNullOrEmpty(pzobj.pz_xq))
                {
                    string[] xqargs = pzobj.pz_xq.Split(',');
                    foreach(string xq in xqargs)
                    {
                        for(int i=0;i<=daycount;i++)
                        {
                            string[] sjdargs = pzobj.pz_sjd.Split(',');
                            if(xq.Contains(getWeekStr(starttime.AddDays(i))))
                            {
                                for (int j=0;j<sjdargs.Length;j++)
                                {
                                    yyobj.yy_bh = "";
                                    yyobj.yy_bz = "";
                                    yyobj.yy_djrq = DateTime.Now.ToString("yyyy-MM-dd");
                                    yyobj.yy_rq = starttime.AddDays(i).ToString("yyyy-MM-dd");
                                    yyobj.yy_sfjf = "";
                                    yyobj.yy_sjd = sjdargs[j];
                                    yyobj.yy_xm = _yyxm;
                                    bll.Add(yyobj);
                                }
                                }
                        }
                        
                    }
                    RefreshCheckList();
                }
            }
        }

        private string getWeekStr(DateTime date)
        {
            string WeekDay = "";
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    WeekDay = "星期一";
                    break;
                case DayOfWeek.Friday:
                    WeekDay = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    WeekDay = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    WeekDay = "星期日";
                    break;
                case DayOfWeek.Thursday:
                    WeekDay = "星期四";
                    break;
                case DayOfWeek.Tuesday:
                    WeekDay = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    WeekDay = "星期三";
                    break;
            }
            return WeekDay;
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            print(true);
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            print(false);
        }

        public void print(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (dataGridView2.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要打印的记录！");
                return;
            }
            yy_asd_tabNotMap _obj = dataGridView2.SelectedRows[0].Tag as yy_asd_tabNotMap;
            if (_obj.child_id == 0)
            {
                MessageBox.Show("预约信息不完整！");
                return;
            }
            
            try
            {
                yy_asd_printer printer = new yy_asd_printer(_obj);
                printer.Print(ispre);
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常，请联系管理员！");
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
