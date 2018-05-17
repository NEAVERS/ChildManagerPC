using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;

namespace login.UI.printrecord
{
    public partial class PanelBMIPrinter : UserControl
    {
        
        private ArrayList _conrolsList = new ArrayList(100);
        private const int _itemNameWidth = 88;
        private const int _itemValueWidth = 107;
        private const int _itemOptionWidth = 98;
        private const int _itemHeight = 30;
        private const int _dateTimePickerWidth = 155;

        private ArrayList _lineTopPositionList = new ArrayList(10);
      
        string yuchanqi = "";
        string yunzhou = "";
        private int _printHeaderHeight = 80;
        private int _printTailHeight = 35;
        private Rectangle _rectHeader;
        private Rectangle _rectBody;
        private Rectangle _rectTail;
        private Image _img;

    
        public PanelBMIPrinter()
        {
            InitializeComponent();
        }

        //打印孕妇建档记录单
        public void Print(bool isPreview,bool landscape, Image img)
        {
            _img = img;
            //_bmlval = 32.0f;
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "儿童生长曲线图";
          

            printDoc.PrintController = new StandardPrintController();
            printDoc.DefaultPageSettings.Margins = new Margins(2, 2, 2, 2);
            printDoc.DefaultPageSettings.Landscape = landscape;//指定竖向打印
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            int printableWidth = printDoc.DefaultPageSettings.PaperSize.Width - printDoc.DefaultPageSettings.Margins.Left
                - printDoc.DefaultPageSettings.Margins.Right;
            int printalbeHeight = printDoc.DefaultPageSettings.PaperSize.Height - printDoc.DefaultPageSettings.Margins.Top
                - printDoc.DefaultPageSettings.Margins.Bottom;
            _rectHeader = new Rectangle(printDoc.DefaultPageSettings.Margins.Left, printDoc.DefaultPageSettings.Margins.Top,
                printableWidth, _printHeaderHeight);
            _rectBody = new Rectangle(_rectHeader.Left, _rectHeader.Bottom, printableWidth, printalbeHeight - _printHeaderHeight - _printTailHeight);
            _rectTail = new Rectangle(_rectHeader.Left, _rectBody.Bottom, printableWidth, _printTailHeight);


            if (isPreview)
            {
                PrintPreviewDialog previewDialog = new PrintPreviewDialog();
                previewDialog.PrintPreviewControl.StartPage = 0;
                previewDialog.PrintPreviewControl.Zoom = 1.0;
                previewDialog.WindowState = FormWindowState.Maximized; 
                previewDialog.TopLevel = true;
                previewDialog.Document = printDoc;
                previewDialog.ShowDialog();


            }
            else
            {
                printDoc.Print();
            }
        }
        void printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(_img, 0, 0); 

         
        }
        //打印页面表头
        private void PrintHeader(Graphics g)
        {
         

        }
       
        //打印页面主体部分
        private void PrintBody(Graphics g)
        {

            
          
        }
        /// <summary>
        /// 打印管理控制
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="rect"></param>
        /// <param name="g"></param>
        private void MiddleCenterPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
        }

        private void BottomCenterPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Far;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void TopCenterPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Near;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void MiddleLeftPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void BottomLeftPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Far;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void TopLeftPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void MiddleRightPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void BottomRightPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Far;

            g.DrawString(text, font, brush, rect, sf);
        }

        private void TopRightPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Near;

            g.DrawString(text, font, brush, rect, sf);
        }
        //预产期计算方法
        private string getyuchanqi(string yuchandatestr)
        {
            DateTime yuchanqi = Convert.ToDateTime(yuchandatestr);
            DateTime dt = yuchanqi.AddDays(+280d);//预产期在最后一次月经加280天
            return dt.ToShortDateString();
        }
        //孕周计算方法
        private string[] getyunzhou(string yuejing)
        {
            string[] aa = new string[2];
            DateTime date1 = DateTime.Now;
            DateTime date2 = Convert.ToDateTime(yuejing);
            TimeSpan time1 = new TimeSpan(date1.Ticks);
            TimeSpan time2 = new TimeSpan(date2.Ticks);
            TimeSpan ts = time1.Subtract(time2).Duration();
            int days = ts.Days;
            int smallzhou = days % 7;
            int bigzhou = (int)(days / 7);
            aa[0] = bigzhou.ToString();
            aa[1] = smallzhou.ToString();
            return aa;

        }

    }
}
