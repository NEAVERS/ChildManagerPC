using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using YCF.BLL.Base;
using YCF.BLL;
using YCF.Model;
using YCF.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ChildManager.UI.tongji
{
    public partial class tongji_publicmodel : UserControl
    {
        temp_reportbll reportbll = new temp_reportbll();
        temp_report_nodebll nodebll = new temp_report_nodebll();
        private TEMP_REPORT reportobj = null;
        int _report_id = 0;

        public tongji_publicmodel(string reportid)
        {
            InitializeComponent();
            //dgvDiscourseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _report_id = Convert.ToInt32(reportid);
        }
        private void dgvDiscourseList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                Rectangle rect = e.CellBounds;
                rect.Offset(-1, -1);
                using (Brush brush = new LinearGradientBrush(e.CellBounds, Color.White, Color.FromArgb(210, 224, 237), LinearGradientMode.Vertical))
                using (Pen penGrid = new Pen((sender as DataGridView).GridColor))
                {
                    e.Graphics.FillRectangle(brush, rect);
                    e.Graphics.DrawRectangle(penGrid, rect);
                    e.PaintContent(e.ClipBounds);
                }
                e.Handled = true;
            }
        }

        private void RefreshRecordList()
        {
            IList<SqlParameter> prms = new List<SqlParameter>();
            foreach (Control ct in panel2.Controls)
            {
                if (ct is TextBox || ct is ComboBox || ct is DateTimePicker)
                {
                    prms.Add(new SqlParameter("@" + ct.Name, (ct.Text.Trim() + " " + ct.Tag?.ToString() ?? "").Trim()));
                }
            }
            string strSql = reportobj.REPORT_PROCEDURE;
            DataSet ds = BLLStatic.GetDataSet(strSql, CommandType.StoredProcedure, prms.ToArray());
            dgvDiscourseList.DataSource = ds.Tables[0];
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            RefreshRecordList();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出Excel(*.xls)|*.xls";
            sfd.FileName = "" + reportobj.REPORT_DISPLAYNAME + "" + DateTime.Now.ToString("yyyyMMddhhmmss");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                bool res = ExcelHelper.CreateExcel(dgvDiscourseList, sfd.FileName, @"" + (reportobj.REPORT_XLS == "publicXls" ? "" : reportobj.REPORT_XLS) + "");
                if (res)
                    MessageBox.Show("导出成功！");
                else
                    MessageBox.Show("导出失败！");
                Cursor.Current = Cursors.Default;
            }
        }

        private void tongji_putong_Load(object sender, EventArgs e)
        {
            reportobj = reportbll.Get(_report_id);
            IList<TEMP_REPORT_NODE> list = nodebll.GetList(_report_id);

            string report_name = reportobj.REPORT_NAME;

            int locationLeft = 3;
            int locationTop = 6;
            Font font = new Font("宋体", 10.5F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(134)));
            foreach (TEMP_REPORT_NODE node in list)
            {
                Size presize = TextRenderer.MeasureText(node.NODE_PREFIX, font);
                int width = Convert.ToInt32(node.NODE_WIDTH);
                Size sufsize = TextRenderer.MeasureText(node.NODE_PREFIX, font);
                if ((locationLeft + presize.Width + width + sufsize.Width) > 760)
                {
                    locationLeft = 3;
                    locationTop += 26;
                }

                if (!String.IsNullOrEmpty(node.NODE_PREFIX))
                {
                    Label label = new Label();
                    label.AutoSize = true;
                    label.BackColor = Color.Transparent;
                    label.Font = font;
                    label.Location = new Point(locationLeft, (locationTop + 3));
                    label.Name = "labelpre_" + node.NODE_NAME;
                    label.Size = new Size((presize.Width - 3), presize.Height);
                    label.Text = node.NODE_PREFIX;
                    this.panel2.Controls.Add(label);
                    locationLeft += presize.Width;
                }

                if (node.NODE_TYPE == "文本框")
                {
                    TextBox temcontrol = new TextBox();
                    temcontrol.Font = font;
                    temcontrol.Location = new Point(locationLeft, locationTop);
                    temcontrol.Name = node.NODE_NAME;
                    temcontrol.Size = new Size(width, 23);
                    this.panel2.Controls.Add(temcontrol);
                    locationLeft += width;
                }
                else if (node.NODE_TYPE == "日期框")
                {
                    DateTimePicker temcontrol = new DateTimePicker();
                    temcontrol.CustomFormat = "yyyy-MM-dd";
                    temcontrol.Font = font;
                    temcontrol.Format = DateTimePickerFormat.Custom;
                    temcontrol.Location = new Point(locationLeft, locationTop);
                    temcontrol.Name = node.NODE_NAME;
                    temcontrol.Tag = node.NODE_VALUE;
                    temcontrol.Size = new Size(width, 23);
                    this.panel2.Controls.Add(temcontrol);
                    locationLeft += width;
                }
                else if (node.NODE_TYPE == "下拉框")
                {
                    ComboBox temcontrol = new ComboBox();
                    temcontrol.Font = font;
                    temcontrol.FormattingEnabled = true;
                    string[] comlist = node.NODE_VALUE.Split(',');
                    temcontrol.Items.AddRange(comlist);
                    temcontrol.Location = new Point(locationLeft, locationTop);
                    temcontrol.Name = node.NODE_NAME;
                    temcontrol.Size = new Size(width, 23);
                    this.panel2.Controls.Add(temcontrol);
                    locationLeft += width;
                }

                if (!String.IsNullOrEmpty(node.NODE_SUFFIX))
                {
                    Label label = new Label();
                    label.AutoSize = true;
                    label.BackColor = Color.Transparent;
                    label.Font = font;
                    label.Location = new Point(locationLeft, (locationTop + 3));
                    label.Name = "labelsuf_" + node.NODE_NAME + "";
                    label.Size = new Size((presize.Width - 3), sufsize.Height);
                    label.Text = node.NODE_SUFFIX;
                    panel2.Controls.Add(label);
                    locationLeft += sufsize.Width;
                }

            }
            this.panel2.Size = new Size(1014, (locationTop + 28));
        }
    }
}
