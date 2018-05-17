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
using System.Drawing;

namespace ChildManager.UI.yuyue
{
    public partial class yy_all_panel : UserControl
    {
        yy_asd_tabbll bll = new yy_asd_tabbll();
        yy_pz_tabbll pzbll = new yy_pz_tabbll();
        yy_pz_details_tabbll detailsbll = new yy_pz_details_tabbll();
        yy_asd_tab _obj = null;
        string _pz_lx = "";
        private IList<yy_pz_tab> _xmList;
        private IList<yy_pz_details_tab> _detailList;

        public yy_all_panel(string pz_lx)
        {
            InitializeComponent();
            dgvConditionTreatRecordList.AutoGenerateColumns = false;
            dgvConditionTreatRecordList.MergeColumnNames.AddRange(new string[] 
            {
                "dataGridViewTextBoxColumn4",
            });
            CommonHelper.SetAllControls(panel1);

            dateTimePicker2.Value = DateTime.Now.AddDays(7);
            _pz_lx = pz_lx;

            _xmList = pzbll.GetList();

            yy_xm.DataSource = _xmList;
            yy_xm.DisplayMember = "pz_xm";
            yy_xm.ValueMember = "id";
            yy_xm.SelectedIndexChanged += Yy_xm_SelectedIndexChanged;

            yy_xm.SelectedIndex = 0;
            Yy_xm_SelectedIndexChanged(yy_xm, new EventArgs());
        }

        private void Yy_xm_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = (int)(sender as ComboBox).SelectedValue;
            _detailList = detailsbll.Get(id);

            yy_sjd.DataSource = _detailList;
            yy_sjd.DisplayMember = "time";
            yy_sjd.ValueMember = "id";
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
            if (dgvConditionTreatRecordList.SelectedRows.Count > 0)
            {
                yy_asd_tabNotMap qttab = dgvConditionTreatRecordList.SelectedRows[0].Tag as yy_asd_tabNotMap;
                obj.id = qttab.id;
                obj.child_id = qttab.child_id;
                obj.yy_djrq = qttab.yy_djrq;
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
            
            var startdate = dateTimePicker1.Value.Date;
            var enddate = dateTimePicker2.Value.Date;

            var list = bll.GetList(startdate, enddate, _pz_lx);

            dgvConditionTreatRecordList.DataSource = null;
            dgvConditionTreatRecordList.DataSource = list;

            Cursor.Current = Cursors.Default;
        }


        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dgvConditionTreatRecordList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要删除的记录！");
                return;
            }
            yy_asd_tabNotMap _checkobj = dgvConditionTreatRecordList.SelectedRows[0].Tag as yy_asd_tabNotMap;
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

        private void buttonX4_Click(object sender, EventArgs e)
        {
            Generate(true);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            RefreshCheckList();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            Generate(false);
        }

        private void Generate(bool justOne)
        {
            var isFull = true;
            var yy_xm = this.yy_xm.Text;
            yy_pz_tab pzobj = pzbll.GetByXm(yy_xm);

            DateTime starttime = dateTimePicker1.Value;
            DateTime endtime = dateTimePicker2.Value;
            int daycount = (endtime - starttime).Days;
            if (pzobj != null)
            {
                yy_asd_tab yyobj = CommonHelper.GetObjMenzhen<yy_asd_tab>(panel2.Controls, 0);
                if (!String.IsNullOrEmpty(pzobj.pz_xq))
                {
                    string[] xqargs = pzobj.pz_xq.Split(',');
                    //foreach (string xq in xqargs)
                    //{
                    for (int i = 0; i <= daycount; i++)
                    {
                        var sjdargs = _detailList.Select(t => t.time).ToArray();
                        if (xqargs.Contains(((int)starttime.AddDays(i).DayOfWeek).ToString()))
                        {
                            for (int j = 0; j < sjdargs.Length; j++)
                            {
                                if (bll.IsFull(yy_xm, starttime.AddDays(i).ToString("yyyy-MM-dd"), sjdargs[j]))
                                {
                                    continue;
                                }

                                isFull = false;

                                yyobj.yy_bh = "";
                                yyobj.yy_bz = "";
                                yyobj.yy_djrq = DateTime.Now.ToString("yyyy-MM-dd");
                                yyobj.yy_rq = starttime.AddDays(i).ToString("yyyy-MM-dd");
                                yyobj.yy_sfjf = "";
                                yyobj.yy_sjd = sjdargs[j];
                                yyobj.yy_xm = yy_xm;
                                bll.Add(yyobj);

                                if (justOne)
                                {
                                    _obj = yyobj;

                                    RefreshCheckList();
                                    return;
                                }
                            }
                        }
                    }

                    //}

                    if (isFull)
                    {
                        MessageBox.Show("该时间段的预约已满");
                    }
                    else
                    {
                        RefreshCheckList();
                    }
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
            if (dgvConditionTreatRecordList.SelectedRows.Count <= 0)
            {
                MessageBox.Show("请选择要打印的记录！");
                return;
            }
            yy_asd_tabNotMap _obj = dgvConditionTreatRecordList.SelectedRows[0].Tag as yy_asd_tabNotMap;
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
            Paneltsb_searchInfo frmsearcher = new Paneltsb_searchInfo();

            frmsearcher.ShowDialog();
            if (frmsearcher.DialogResult == DialogResult.OK)
            {
                tb_childbase jibenobj = frmsearcher.returnval;
                if (jibenobj != null)
                {
                    yy_asd_tab obj = GetObj();
                    obj.child_id = jibenobj.id;
                    if (bll.SaveOrUpdate(obj))
                    {
                        MessageBox.Show("保存成功！", "软件提示");
                        RefreshCheckList();
                    }
                    else
                    {
                        MessageBox.Show("保存失败！", "软件提示");
                    }
                }
            }
        }

        private void buttonX8_Click_1(object sender, EventArgs e)
        {
            using (var edit = new yy_all_edit(null, null, _pz_lx, dateTimePicker1.Text))
            {
                edit.ShowDialog();

                RefreshCheckList();
            }
            
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var obj = (sender as DataGridView).Rows[e.RowIndex].DataBoundItem as yy_asd_tabCount;
                using (var edit = new yy_all_edit(null, obj, _pz_lx, dateTimePicker1.Text))
                {
                    edit.ShowDialog();

                    RefreshCheckList();
                }
            }
    
        }
        private void buttonX9_Click(object sender, EventArgs e)
        {
            using (var details = new yy_all_details(_pz_lx))
            {
                details.ShowDialog();

                RefreshCheckList();
            }
        }

        #region dgv

        private void dataGridView2_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                switch (e.ColumnIndex)
                {
                    case 1:
                        {
                            DrawCell(e);
                            break;
                        }
                    default:
                        break;
                }
            }
           
        }


        private void DrawCell(DataGridViewCellPaintingEventArgs e)
        {
            if (e.CellStyle.Alignment == DataGridViewContentAlignment.NotSet)
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            Brush gridBrush = new SolidBrush(dgvConditionTreatRecordList.GridColor);
            SolidBrush backBrush = new SolidBrush(e.CellStyle.BackColor);
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int cellwidth;
            //上面相同的行数
            int UpRows = 0;
            //下面相同的行数
            int DownRows = 0;
            //单元格高度
            var cellHeight = 0;
            var cellTopHeight = 0;
            //总行数
            int count = 0;


            cellwidth = e.CellBounds.Width;
            Pen gridLinePen = new Pen(gridBrush);
            string curValue = dgvConditionTreatRecordList.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? "";
            string curDateValue = dgvConditionTreatRecordList.Rows[e.RowIndex].Cells[0].Value?.ToString() ?? "";
            string curSelected = dgvConditionTreatRecordList.CurrentRow.Cells[e.ColumnIndex].Value == null ? "" : dgvConditionTreatRecordList.CurrentRow.Cells[e.ColumnIndex].Value.ToString().Trim();
            //if (!string.IsNullOrEmpty(curValue))
            //{
            #region 获取下面的行数
            for (int i = e.RowIndex; i < dgvConditionTreatRecordList.Rows.Count; i++)
            {
                var currCell = dgvConditionTreatRecordList.Rows[i].Cells[1];
                var currDateCell = dgvConditionTreatRecordList.Rows[i].Cells[0];
                if (currCell.Value.ToString().Equals(curValue) && currDateCell.Value.ToString().Equals(curDateValue))
                {
                    //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;
                    cellHeight += currCell.Size.Height;
                    DownRows++;
                    if (e.RowIndex != i)
                    {
                        cellwidth = cellwidth < currCell.Size.Width ? cellwidth : dgvConditionTreatRecordList.Rows[i].Cells[e.ColumnIndex].Size.Width;
                    }
                }
                else
                {
                    break;
                }
            }
            #endregion
            #region 获取上面的行数
            for (int i = e.RowIndex; i >= 0; i--)
            {
                var currCell = dgvConditionTreatRecordList.Rows[i].Cells[1];
                var currDateCell = dgvConditionTreatRecordList.Rows[i].Cells[0];
                if (currCell.Value.ToString().Equals(curValue) && currDateCell.Value.ToString().Equals(curDateValue))
                {
                    //this.Rows[i].Cells[e.ColumnIndex].Selected = this.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected;

                    UpRows++;
                    if (e.RowIndex != i)
                    {
                        cellHeight += currCell.Size.Height;
                        cellTopHeight += currCell.Size.Height;
                        cellwidth = cellwidth < currCell.Size.Width ? cellwidth : dgvConditionTreatRecordList.Rows[i].Cells[e.ColumnIndex].Size.Width;
                    }
                }
                else
                {
                    break;
                }
            }
            #endregion
            count = DownRows + UpRows - 1;
            if (count < 2)
            {
                return;
            }
            //}
            if (dgvConditionTreatRecordList.Rows[e.RowIndex].Selected)
            {
                backBrush.Color = e.CellStyle.SelectionBackColor;
                fontBrush.Color = e.CellStyle.SelectionForeColor;
            }
            //以背景色填充
            e.Graphics.FillRectangle(backBrush, e.CellBounds);
            //画字符串

            PaintingFont(e, cellTopHeight, cellwidth, cellHeight, UpRows, DownRows, count);

            if (DownRows == 1)
            {
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                count = 0;
                cellTopHeight = 0;
            }
            // 画右边线
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

            e.Handled = true;
        }

        private void PaintingFont(DataGridViewCellPaintingEventArgs e, int cellTopHeight, int cellwidth, int cellheight, int UpRows, int DownRows, int count)
        {
            SolidBrush fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            var rect = new RectangleF(e.CellBounds.X, e.CellBounds.Y - cellTopHeight, cellwidth, cellheight);
            using (var sf = new StringFormat())
            {
                switch (e.CellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.NotSet:
                    case DataGridViewContentAlignment.TopLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case DataGridViewContentAlignment.TopCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case DataGridViewContentAlignment.TopRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case DataGridViewContentAlignment.MiddleLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case DataGridViewContentAlignment.MiddleCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case DataGridViewContentAlignment.MiddleRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case DataGridViewContentAlignment.BottomCenter:
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    case DataGridViewContentAlignment.BottomRight:
                        sf.Alignment = StringAlignment.Far;
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                    default:
                        break;
                }

                e.Graphics.DrawString(e.FormattedValue?.ToString(), e.CellStyle.Font, fontBrush, rect, sf);
            }
        }

        #endregion


    }
}
