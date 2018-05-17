using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;
using ChildManager.Model.ChildBaseInfo;

namespace login.UI.printrecord
{
    public partial class PanelGoulouzhuananPrinter : UserControl
    {
        private DataGridView _dataGridView;
        private List<Header> _pageHeaders;
        private List<Header> _tableFoots;
        private int _tailHeight = 20;

        private bool _printPageNo = true;
        private bool _printPrintTime = true;

        private Rectangle _rectPrintBound;
        private Rectangle _rectBody;
        private Margins _margin = new Margins(5, 5, 15, 35);
        private List<int> _columnsPrintWidth;
        private int _columnHeaderPrintHeight;
        private int _dataGridViewPrintLeft;
        private int _patientInfoHeight = 30;
        /// <summary>
        /// 每页行数
        /// </summary>
        private int _pageRows = 32;

        /// <summary>
        /// 行高 
        /// </summary>
        private int _rowPrintHeight = 15;

        private int _currPrintRow;
        private int _currPrintTableFoot;
        private int _pageNo;
        private List<int> _firstRowIndexForPages = new List<int>();
        private bool _landscapePrinting = true;

        ChildBaseInfoObj _baseobj = null;
        ChildYingyanggeanObj _yinyanggeanobj = null;


        public PanelGoulouzhuananPrinter(DataGridView dataGridView, ChildBaseInfoObj baseobj, ChildYingyanggeanObj yinyanggeanobj)
        {
            InitializeComponent();
            _dataGridView = dataGridView;//页面传过来需要打印的datagridview对象
            _pageHeaders = new List<Header>();
            _tableFoots = new List<Header>();
            _columnsPrintWidth = new List<int>();
            _baseobj = baseobj;
            _yinyanggeanobj = yinyanggeanobj;
        }
        /// <summary>
        /// cjh 2009-4-1
        /// </summary>
        public List<Header> PageHeaders
        {
            get
            {
                return _pageHeaders;
            }
        }

        /// <summary>
        /// cjh 2009-12-8
        /// </summary>
        public List<Header> TableFoots
        {
            get
            {
                return _tableFoots;
            }
        }

        /// <summary>
        /// cjh 2009-4-3
        /// </summary>
        public Margins Margin
        {
            get
            {
                return _margin;
            }
            set
            {
                _margin = value;
            }
        }


        /// <summary>
        /// cjh 2009-12-16 是否打印页码
        /// </summary>
        public bool PrintPageNo
        {
            get
            {
                return _printPageNo;
            }
            set
            {
                _printPageNo = false;
            }
        }

        /// <summary>
        /// cjh 2009-12-16 是否打印打印时间
        /// </summary>
        public bool PrintPrintTime
        {
            get
            {
                return _printPrintTime;
            }
            set
            {
                _printPrintTime = value;
            }
        }

        /// <summary>
        /// cjh 2010-8-30 是否横向打印
        /// </summary>
        public bool LandscapePrinting
        {
            get
            {
                return _landscapePrinting;
            }
            set
            {
                _landscapePrinting = value;
            }
        }

        /// <summary>
        /// cjh 2010-9-27
        /// </summary>
        public int PatientInfoHeight
        {
            get
            {
                return _patientInfoHeight;
            }
            set
            {
                _patientInfoHeight = value;
            }
        }

        public void Print(string printer)
        {
            Print(printer, false);
        }


        public void Print(string printer, bool isPreview)
        {
            _currPrintRow = 0; //当前打印的行数
            _currPrintTableFoot = 0;
            _pageNo = 1; //当前打印的页码

            PrintDocument printDoc = new PrintDocument();//打印文档对象
            printDoc.DocumentName = "维生素D缺乏性佝偻病儿童专案管理记录";
            printDoc.PrintController = new StandardPrintController();
            printDoc.DefaultPageSettings.Margins = _margin; //指定打印页码的边距尺寸
            printDoc.DefaultPageSettings.PrinterSettings.Duplex = Duplex.Simplex;//指定单面打印
            printDoc.DefaultPageSettings.Landscape = _landscapePrinting;//指定竖向打印
            PrintDialog printDialog = new PrintDialog();
            printDialog.AllowCurrentPage = true;
            printDialog.AllowPrintToFile = false;
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;
            printDialog.Document = printDoc;
            printDialog.PrinterSettings.FromPage = 1;
            printDialog.PrinterSettings.ToPage = 1;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.PrinterSettings.FromPage = printDialog.PrinterSettings.FromPage; //设置要打印的第一页页码
                printDoc.PrinterSettings.ToPage = printDialog.PrinterSettings.ToPage;//设置要打印的最后一页的页码
                printDoc.PrinterSettings.PrintRange = printDialog.PrinterSettings.PrintRange;//设置当前打印页的页码

                printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
                if (!isPreview)
                {
                    printDoc.Print();
                }
                else
                {
                    PrintPreviewDialog previewDialog = new PrintPreviewDialog();
                    previewDialog.Document = printDoc;
                    previewDialog.ShowIcon = false;
                    previewDialog.PrintPreviewControl.Zoom = 1.0;
                    previewDialog.WindowState = FormWindowState.Maximized;
                    previewDialog.ShowDialog();
                }
            }

        }


        private void CountPageNos(Graphics g)
        {
            int top = _rectPrintBound.Top;//定义打印页面的高度

            foreach (Header header in _pageHeaders)
            {
                top += header.Height;
            }
            top += _patientInfoHeight;//打印标题的高度
            top += 50;
            _rectBody = new Rectangle(_rectPrintBound.Left, top, _rectPrintBound.Width, _rectPrintBound.Bottom - top - _tailHeight);


            _columnsPrintWidth.Add(80);
            _columnsPrintWidth.Add(45);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(80);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);

            _columnHeaderPrintHeight += 50;

            int rowIndex = 0;//行号下标为0
            _firstRowIndexForPages.Clear();//行号的列表集合
            while (rowIndex < _dataGridView.Rows.Count)
            {
                _firstRowIndexForPages.Add(rowIndex);//添加行号下标列表
                int y = _rectBody.Top;// +_columnHeaderPrintHeight;

                while (rowIndex < _dataGridView.Rows.Count)
                {
                    int rowPrintHeight = 0;
                    for (int i = 0; i < _dataGridView.Columns.Count; i++)
                    {
                        if (_dataGridView.Columns[i].Visible == false)
                        {
                            continue;
                        }

                        Font cellFont = _dataGridView[i, rowIndex].Style.Font;
                        if (cellFont == null)
                        {
                            cellFont = _dataGridView.Rows[rowIndex].DefaultCellStyle.Font;
                        }
                        if (cellFont == null)
                        {
                            cellFont = _dataGridView.Columns[i].DefaultCellStyle.Font;
                        }
                        if (cellFont == null)
                        {
                            cellFont = _dataGridView.DefaultCellStyle.Font;
                        }

                        DataGridViewCell cell = _dataGridView[i, rowIndex];
                        string text = cell.FormattedValue as string;
                        int height = (int)Math.Ceiling(g.MeasureString(text, cellFont, _columnsPrintWidth[i]).Height);
                        rowPrintHeight = Math.Max(rowPrintHeight, height);


                        //rowPrintHeight = height;
                    }
                    if (rowPrintHeight > 0)
                    {
                        rowPrintHeight += 5;
                    }
                    // rowPrintHeight = _rowPrintHeight;

                    if (y + rowPrintHeight >= _rectBody.Bottom)
                    {// 打印超过范围下界

                        break;
                    }

                    y += rowPrintHeight;
                    rowIndex++;
                }
            }
        }


        void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            _rectPrintBound = e.MarginBounds;

            if (_currPrintRow == 0)
            {
                CountPageNos(e.Graphics);
            }
            PrintDocument printDoc = sender as PrintDocument;
            if (printDoc.PrinterSettings.PrintRange == PrintRange.SomePages)
            {
                if (_pageNo < printDoc.PrinterSettings.FromPage)
                {
                    _pageNo = printDoc.PrinterSettings.FromPage;
                }
                if (_pageNo > printDoc.PrinterSettings.ToPage)
                {
                    e.Cancel = true;
                    _currPrintRow = 0;
                    _currPrintTableFoot = 0;
                    _pageNo = 1;
                    return;
                }
            }
            if (_pageNo > _firstRowIndexForPages.Count)
            {
                e.Cancel = true;
                _currPrintRow = 0;
                _currPrintTableFoot = 0;
                _pageNo = 1;
                return;
            }

            PrintHeaders(e.Graphics);

            if (_currPrintRow == 0)
            {
                //FormatRows(e.Graphics);
            }

            PrintColumnsHeader(e.Graphics);

            int y = _rectBody.Top + _columnHeaderPrintHeight;
            PrintRows(e.Graphics, ref y, _pageNo);

            y += 20;

            PrintTableFoots(e.Graphics, y);

            //PrintTail(e.Graphics);

            if (printDoc.PrinterSettings.PrintRange == PrintRange.SomePages && _pageNo >= printDoc.PrinterSettings.ToPage)
            {
                _currPrintRow = 0;
                _currPrintTableFoot = 0;
                _pageNo = 1;
            }
            else if (_currPrintRow < _dataGridView.Rows.Count || _currPrintTableFoot < _tableFoots.Count)
            {
                e.HasMorePages = true; //调用打印printDoc_PrintPage，重新打印一张新的页面.

                _pageNo += 1;
            }
            else
            {
                _currPrintRow = 0;
                _currPrintTableFoot = 0;
                _pageNo = 1;
            }
        }

        /// <summary>
        /// cjh 2009-3-31
        /// </summary>
        /// <param name="g"></param>
        private void PrintHeaders(Graphics g)
        {
            int top = _rectPrintBound.Top;
            using (StringFormat sf = new StringFormat())
            using (StringFormat sfleft = new StringFormat())
            using (Pen pen = new Pen(Color.Black))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                foreach (Header header in _pageHeaders)
                {
                    Rectangle rectHeader = new Rectangle(_rectPrintBound.Left, top, _rectPrintBound.Width, header.Height);
                    sf.Alignment = header.Alignment;
                    sf.LineAlignment = header.LineAlignment;
                    g.DrawString(header.Text, header.Font, brush, rectHeader, sf);

                    top += header.Height;
                }
                //Rectangle imagerect = new Rectangle(280, 25, image.Width, 45);
                //g.DrawImage(image, imagerect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
                //Rectangle english = new Rectangle(30, 35, _rectPrintBound.Width, 45);
                //g.DrawString("The First Affiliated Hospital of Chongqing Medical University", new Font("Arial Unicode MS", 9f, FontStyle.Bold), brush, english, sf);



                if (_yinyanggeanobj != null)
                {
                    int left = _rectPrintBound.Left;
                    int lineOffset = (_patientInfoHeight - 21) / 2;

                    Rectangle rect = new Rectangle(left, top, 80, _patientInfoHeight);
                    g.DrawString("儿童姓名:", new Font("Arial Unicode MS", 11f), brush, rect, sfleft);
                    left += 80;

                    rect = new Rectangle(left, top, 170, _patientInfoHeight);
                    g.DrawString(_baseobj.ChildName, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 170;

                    rect = new Rectangle(left, top, 45, _patientInfoHeight);
                    g.DrawString("性别:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 45;

                    rect = new Rectangle(left, top, 80, _patientInfoHeight);
                    g.DrawString(_baseobj.ChildGender, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 80;

                    rect = new Rectangle(left, top, 100, _patientInfoHeight);
                    g.DrawString("出生日期:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 100;

                    rect = new Rectangle(left, top, 190, _patientInfoHeight);
                    g.DrawString(Convert.ToDateTime(_baseobj.ChildBirthDay).ToString("yyyy年MM月dd日"), new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 190;

                    rect = new Rectangle(left, top, 120, _patientInfoHeight);
                    g.DrawString("开始管理时间:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 120;

                    rect = new Rectangle(left, top, 220, _patientInfoHeight);
                    g.DrawString(Convert.ToDateTime(_yinyanggeanobj.managetime).ToString("yyyy年MM月dd日"), new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);

                    //第二行
                    top += _patientInfoHeight;
                    left = _rectPrintBound.Left;


                    rect = new Rectangle(left, top, 100, _patientInfoHeight);
                    g.DrawString("家庭住址:", new Font("Arial Unicode MS", 11f), brush, rect, sfleft);
                    left += 100;

                    rect = new Rectangle(left, top, 530, _patientInfoHeight);
                    g.DrawString(_baseobj.address, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 530;

                    rect = new Rectangle(left, top, 80, _patientInfoHeight);
                    g.DrawString("联系电话:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 80;

                    rect = new Rectangle(left, top, 290, _patientInfoHeight);
                    g.DrawString(_baseobj.Telephone, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);

                    //第三行
                    top += _patientInfoHeight;
                    left = _rectPrintBound.Left;

                    rect = new Rectangle(left, top, 130, _patientInfoHeight);
                    g.DrawString("母孕期和哺乳期:", new Font("Arial Unicode MS", 11f), brush, rect, sfleft);
                    left += 130;

                    rect = new Rectangle(left, top, 165, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.chushengshi, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 165;

                    left += 265;
                    rect = new Rectangle(left, top, 150, _patientInfoHeight);
                    g.DrawString("儿童既往患病情况:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 150;

                    rect = new Rectangle(left, top, 290, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.jiwangshi, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);

                    //第四行
                    top += _patientInfoHeight;
                    left = _rectPrintBound.Left;

                    rect = new Rectangle(left, top, 130, _patientInfoHeight);
                    g.DrawString("儿童服用VitD:", new Font("Arial Unicode MS", 11f), brush, rect, sfleft);
                    left += 130;

                    rect = new Rectangle(left, top, 30, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.childvitd, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 30;

                    if (_yinyanggeanobj.childvitd == "有")
                    {
                        rect = new Rectangle(left, top, 150, _patientInfoHeight);
                        g.DrawString("（开始服用VitD年龄:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        left += 150;

                        rect = new Rectangle(left, top, 60, _patientInfoHeight);
                        g.DrawString(_yinyanggeanobj.startvitdmonth, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                        left += 60;

                        rect = new Rectangle(left, top, 20, _patientInfoHeight);
                        g.DrawString("月", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        left += 20;

                        rect = new Rectangle(left, top, 60, _patientInfoHeight);
                        g.DrawString(_yinyanggeanobj.startvitdday, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                        left += 60;

                        rect = new Rectangle(left, top, 20, _patientInfoHeight);
                        g.DrawString("天", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        left += 40;

                        rect = new Rectangle(left, top, 50, _patientInfoHeight);
                        g.DrawString("品名:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        left += 50;

                        rect = new Rectangle(left, top, 120, _patientInfoHeight);
                        g.DrawString(_yinyanggeanobj.vitdname, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                        left += 120;

                        rect = new Rectangle(left, top, 50, _patientInfoHeight);
                        g.DrawString("剂量:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        left += 50;

                        rect = new Rectangle(left, top, 120, _patientInfoHeight);
                        g.DrawString(_yinyanggeanobj.vitdliang, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                        left += 120;

                        rect = new Rectangle(left, top, 50, _patientInfoHeight);
                        g.DrawString("IU/d）", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                        left += 50;


                    }

                    //第五行
                    top += _patientInfoHeight;
                    left = _rectPrintBound.Left;

                    rect = new Rectangle(left, top, 80, _patientInfoHeight);
                    g.DrawString("血液检查:", new Font("Arial Unicode MS", 11f), brush, rect, sfleft);
                    left += 80;

                    rect = new Rectangle(left, top, 50, _patientInfoHeight);
                    g.DrawString("血钙:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 50;

                    rect = new Rectangle(left, top, 110, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.xuegai, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 110;

                    rect = new Rectangle(left, top, 50, _patientInfoHeight);
                    g.DrawString("血磷:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 50;

                    rect = new Rectangle(left, top, 110, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.xuelin, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 110;

                    rect = new Rectangle(left, top, 80, _patientInfoHeight);
                    g.DrawString("血AKP:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 80;

                    rect = new Rectangle(left, top, 110, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.xueakp, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 110;

                    rect = new Rectangle(left, top, 110, _patientInfoHeight);
                    g.DrawString("血25-(OH)D:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 110;

                    rect = new Rectangle(left, top, 110, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.xueoh, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 110;

                    //第六行
                    top += _patientInfoHeight;
                    left = _rectPrintBound.Left;

                    rect = new Rectangle(left, top, 50, _patientInfoHeight);
                    g.DrawString("体征:", new Font("Arial Unicode MS", 11f), brush, rect, sfleft);
                    left += 50;

                    rect = new Rectangle(left, top, 300, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.tizheng, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);

                    //第七行
                    top += _patientInfoHeight;
                    left = _rectPrintBound.Left;

                    rect = new Rectangle(left, top, 80, _patientInfoHeight);
                    g.DrawString("X线检查:", new Font("Arial Unicode MS", 11f), brush, rect, sfleft);
                    left += 80;

                    rect = new Rectangle(left, top, 600, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.xjiancha, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    

                    //第八行
                    top += _patientInfoHeight;
                    left = _rectPrintBound.Left;

                    rect = new Rectangle(left, top, 45, _patientInfoHeight);
                    g.DrawString("转归:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 45;

                    rect = new Rectangle(left, top, 165, _patientInfoHeight);
                    g.DrawString(_yinyanggeanobj.zhuangui, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);
                    left += 165;

                    left += 500;
                    rect = new Rectangle(left, top, 100, _patientInfoHeight);
                    g.DrawString("结案日期:", new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    left += 100;

                    rect = new Rectangle(left, top, 190, _patientInfoHeight);
                    if (!string.IsNullOrEmpty(_yinyanggeanobj.endtime) && _yinyanggeanobj.endtime != "0001-01-01")
                    {
                        g.DrawString(Convert.ToDateTime(_yinyanggeanobj.endtime).ToString("yyyy-MM-dd"), new Font("Arial Unicode MS", 11f), brush, rect, sf);
                    }
                    g.DrawLine(pen, rect.Left, rect.Bottom - lineOffset, rect.Right, rect.Bottom - lineOffset);


                    top += _patientInfoHeight;
                    
                }
            }

            _rectBody = new Rectangle(_rectPrintBound.Left, top, _rectPrintBound.Width, _rectPrintBound.Bottom - top - _tailHeight);
        }
        /// <summary>
        /// cjh 2009-3-31
        /// </summary>
        /// <param name="g"></param>
        private void PrintColumnsHeader(Graphics g)
        {
            int columnWidthSum = 0;
            foreach (DataGridViewColumn column in _dataGridView.Columns)
            {
                if (column.Visible)
                {
                    columnWidthSum += column.Width;
                }
            }

            int dataGridViewPrintWidth = _rectBody.Width;
            _columnsPrintWidth.Clear();


            _columnHeaderPrintHeight = 0;


            _columnsPrintWidth.Add(80);
            _columnsPrintWidth.Add(45);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(80);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);
            _columnsPrintWidth.Add(100);

            _columnHeaderPrintHeight += 40;

            _dataGridViewPrintLeft = _rectBody.Left;

            using (Brush brush = new SolidBrush(Color.Black))
            using (Pen pen = new Pen(brush))
            using (Font font = new Font(_dataGridView.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold))
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                int x = _dataGridViewPrintLeft;
                for (int i = 0; i < _dataGridView.Columns.Count; i++)
                {
                    if (_dataGridView.Columns[i].Visible == false)
                    {
                        continue;
                    }

                    Rectangle rectColumnHeaderCell = new Rectangle(x, _rectBody.Top, _columnsPrintWidth[i], _columnHeaderPrintHeight);
                    g.DrawRectangle(pen, rectColumnHeaderCell);
                    g.DrawString(_dataGridView.Columns[i].HeaderText, font, brush, rectColumnHeaderCell, sf);
               
                    x += _columnsPrintWidth[i];
                }

            }


        }

        private void PrintRows(Graphics g, ref int y, int pageNo)
        {
            _currPrintRow = 0;
            _currPrintRow = _firstRowIndexForPages[pageNo - 1];
            while (_currPrintRow < _dataGridView.Rows.Count && (_firstRowIndexForPages.Count <= pageNo || _firstRowIndexForPages.Count > pageNo && _currPrintRow < _firstRowIndexForPages[pageNo]))
            {
                int rowPrintHeight = 30;
                for (int i = 0; i < _dataGridView.Columns.Count; i++)
                {
                    if (_dataGridView.Columns[i].Visible == false)
                    {
                        continue;
                    }

                    Font cellFont = _dataGridView[i, _currPrintRow].Style.Font;
                    if (cellFont == null)
                    {
                        cellFont = _dataGridView.Rows[_currPrintRow].DefaultCellStyle.Font;
                    }
                    if (cellFont == null)
                    {
                        cellFont = _dataGridView.Columns[i].DefaultCellStyle.Font;
                    }
                    if (cellFont == null)
                    {
                        cellFont = _dataGridView.DefaultCellStyle.Font;
                    }

                    DataGridViewCell cell = _dataGridView[i, _currPrintRow];
                    string text = cell.FormattedValue as string;
                    int height = (int)Math.Ceiling(g.MeasureString(text, cellFont, _columnsPrintWidth[i]).Height);
                    rowPrintHeight = Math.Max(rowPrintHeight, height);
                    //Console.WriteLine(text + "-" + _columnsPrintWidth[i] + "-" + rowPrintHeight);
                }
                if (rowPrintHeight > 0)
                {
                    rowPrintHeight += 5;
                }
                // rowPrintHeight = Math.Max(rowPrintHeight, _dataGridView.RowTemplate.Height);

                using (Brush brush = new SolidBrush(Color.Black))
                using (Pen pen = new Pen(brush))
                using (StringFormat sf = new StringFormat())
                {

                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    int x = _dataGridViewPrintLeft;
                    for (int i = 0; i < _dataGridView.Columns.Count; i++)
                    {
                        if (_dataGridView.Columns[i].Visible == false)
                        {
                            continue;
                        }

                        DataGridViewContentAlignment alignment = _dataGridView[i, _currPrintRow].Style.Alignment;
                        if (alignment == DataGridViewContentAlignment.NotSet)
                        {
                            alignment = _dataGridView.Columns[i].DefaultCellStyle.Alignment;
                        }
                        if (alignment == DataGridViewContentAlignment.NotSet)
                        {
                            alignment = _dataGridView.DefaultCellStyle.Alignment;
                        }
                        switch (alignment)
                        {
                            case DataGridViewContentAlignment.BottomCenter:
                            case DataGridViewContentAlignment.BottomLeft:
                            case DataGridViewContentAlignment.BottomRight:
                                sf.LineAlignment = StringAlignment.Far;
                                break;
                            case DataGridViewContentAlignment.MiddleCenter:
                            case DataGridViewContentAlignment.MiddleLeft:
                            case DataGridViewContentAlignment.MiddleRight:
                                sf.LineAlignment = StringAlignment.Center;
                                break;
                            case DataGridViewContentAlignment.TopCenter:
                            case DataGridViewContentAlignment.TopLeft:
                            case DataGridViewContentAlignment.TopRight:
                                sf.LineAlignment = StringAlignment.Near;
                                break;
                        }
                        switch (alignment)
                        {
                            case DataGridViewContentAlignment.BottomLeft:
                            case DataGridViewContentAlignment.MiddleLeft:
                            case DataGridViewContentAlignment.TopLeft:
                                sf.Alignment = StringAlignment.Near;
                                break;
                            case DataGridViewContentAlignment.BottomCenter:
                            case DataGridViewContentAlignment.MiddleCenter:
                            case DataGridViewContentAlignment.TopCenter:
                                sf.Alignment = StringAlignment.Center;
                                break;
                            case DataGridViewContentAlignment.BottomRight:
                            case DataGridViewContentAlignment.MiddleRight:
                            case DataGridViewContentAlignment.TopRight:
                                sf.Alignment = StringAlignment.Far;
                                break;
                        }

                        Font cellFont = _dataGridView[i, _currPrintRow].Style.Font;
                        if (cellFont == null)
                        {
                            cellFont = _dataGridView.Rows[_currPrintRow].DefaultCellStyle.Font;
                        }
                        if (cellFont == null)
                        {
                            cellFont = _dataGridView.Columns[i].DefaultCellStyle.Font;
                        }
                        if (cellFont == null)
                        {
                            cellFont = _dataGridView.DefaultCellStyle.Font;
                        }

                        DataGridViewCell cell = _dataGridView[i, _currPrintRow];
                        string text = cell.FormattedValue as string;

                        Rectangle rectCell = new Rectangle(x, y, _columnsPrintWidth[i], rowPrintHeight);
                        g.DrawRectangle(pen, rectCell);
                        g.DrawString(text, cellFont, brush, rectCell, sf);

                        x += _columnsPrintWidth[i];
                    }
                }

                y += rowPrintHeight;
                _currPrintRow++;
            }
        }

        /// <summary>
        /// cjh 2009-12-8
        /// </summary>
        /// <param name="g"></param>
        /// <param name="y"></param>
        private void PrintTableFoots(Graphics g, int y)
        {
            StringFormat sf = new StringFormat();
            while (_currPrintTableFoot < _tableFoots.Count)
            {
                if (y + _tableFoots[_currPrintTableFoot].Height <= _rectBody.Bottom)
                {
                    Rectangle rectTableFoot = new Rectangle(_rectBody.Left, y, _rectBody.Width, _tableFoots[_currPrintTableFoot].Height);
                    sf.Alignment = _tableFoots[_currPrintTableFoot].Alignment;
                    sf.LineAlignment = _tableFoots[_currPrintTableFoot].LineAlignment;
                    g.DrawString(_tableFoots[_currPrintTableFoot].Text, _tableFoots[_currPrintTableFoot].Font, Brushes.Black, rectTableFoot, sf);
                    y += _tableFoots[_currPrintTableFoot].Height;
                    _currPrintTableFoot += 1;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// cjh 2009-4-18
        /// </summary>
        /// <param name="g"></param>
        private void PrintTail(Graphics g)
        {
            using (StringFormat sfMiddleCenter = new StringFormat())
            using (StringFormat sfMiddleRight = new StringFormat())
            using (Pen pen = new Pen(Color.Black))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                sfMiddleCenter.Alignment = StringAlignment.Center;
                sfMiddleCenter.LineAlignment = StringAlignment.Center;

                sfMiddleRight.Alignment = StringAlignment.Far;
                sfMiddleRight.LineAlignment = StringAlignment.Center;


                //Rectangle rectTail = new Rectangle(_rectPrintBound.Left, _rectBody.Bottom, _rectPrintBound.Width, _tailHeight);
                //Rectangle rectTail1 = new Rectangle(_rectPrintBound.Left + 350, _rectBody.Bottom, _rectPrintBound.Width, _tailHeight);
                //if (_printPageNo == true)
                //{
                //    g.DrawString("第" + _pageNo.ToString() + "页", new Font("Arial Unicode MS", 11f), brush, rectTail, sfMiddleCenter);
                //}

                //if (_printPrintTime == true)
                //{
                //    g.DrawString("审核者签名：", new Font("Arial Unicode MS", 11f), brush, rectTail1, sfMiddleCenter);
                //}
            }
        }

        /// <summary>
        /// cjh 2009-3-31
        /// </summary>
        public class Header
        {
            private string _text;
            private Font _font;
            private int _height;
            private StringAlignment _alignment = StringAlignment.Center;
            private StringAlignment _lineAlignment = StringAlignment.Center;

            /// <summary>
            /// cjh 2009-3-31
            /// </summary>
            /// <param name="text"></param>
            /// <param name="font"></param>
            /// <param name="height"></param>
            public Header(string text, Font font, int height)
            {
                _text = text;
                _font = font;
                _height = height;
            }

            /// <summary>
            /// cjh 2009-3-31
            /// </summary>
            public string Text
            {
                get
                {
                    return _text;
                }
            }

            /// <summary>
            /// cjh 2009-3-31
            /// </summary>
            public Font Font
            {
                get
                {
                    return _font;
                }
            }

            /// <summary>
            /// cjh 2009-3-31
            /// </summary>
            public int Height
            {
                get
                {
                    return _height;
                }
            }

            /// <summary>
            /// cjh 2009-12-8
            /// </summary>
            public StringAlignment Alignment
            {
                get
                {
                    return _alignment;
                }
                set
                {
                    _alignment = value;
                }
            }

            /// <summary>
            /// cjh 2009-12-8
            /// </summary>
            public StringAlignment LineAlignment
            {
                get
                {
                    return _lineAlignment;
                }
                set
                {
                    _lineAlignment = value;
                }
            }
        }
    }
}
