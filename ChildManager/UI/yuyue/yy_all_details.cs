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
using System.Linq;

namespace ChildManager.UI.yuyue
{
    public partial class yy_all_details : Form
    {
        yy_asd_tabbll bll = new yy_asd_tabbll();
        yy_pz_tabbll pzbll = new yy_pz_tabbll();
        yy_pz_details_tabbll detailsbll = new yy_pz_details_tabbll();
        yy_asd_tab _obj = null;
        string _pz_lx = "";
        private IList<yy_pz_tab> _xmList;
        private IList<yy_pz_details_tab> _detailList;

        public yy_all_details(string pz_lx)
        {
            InitializeComponent();
            CommonHelper.SetAllControls(panel1);
            dateTimePicker2.Value = DateTime.Now.AddDays(7);
            _pz_lx = pz_lx;

            _xmList = pzbll.GetList(pz_lx);

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
            IList<yy_asd_tabNotMap> checklist = bll.GetListByTime(startdate, enddate, _pz_lx);
            if (checklist != null && checklist.Count > 0)
            {
                int i = 0;
                int selectindex = 0;
                foreach (yy_asd_tabNotMap obj in checklist)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView2, obj.yy_djrq, obj.yy_bh, obj.childname, obj.childgender, obj.childbirthday, obj.telephone, obj.yy_rq, obj.yy_xm, obj.yy_sjd, obj.yy_sfjf, obj.yy_bz);
                    row.Tag = obj;
                    dataGridView2.Rows.Add(row);
                    if (_obj != null && obj.id == _obj.id)
                        selectindex = i;
                    i++;
                }
                dataGridView2.Rows[selectindex].Selected = true;
                dataGridView2_CellClick(null, new DataGridViewCellEventArgs(0, selectindex));
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
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            RefreshCheckList();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
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

        private void buttonX8_Click(object sender, EventArgs e)
        {

        }

        private void buttonX8_Click_1(object sender, EventArgs e)
        {
            //using (var edit = new yy_all_edit(null, _pz_lx, dateTimePicker1.Text))
            //{
            //    edit.ShowDialog();

            //    RefreshCheckList();
            //}

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var obj = dataGridView2.Rows[e.RowIndex].Tag as yy_asd_tabNotMap;
                using (var edit = new yy_all_edit(obj.id, null, _pz_lx, obj.yy_rq))
                {
                    edit.ShowDialog();

                    RefreshCheckList();
                }
            }
        }
    }
}
