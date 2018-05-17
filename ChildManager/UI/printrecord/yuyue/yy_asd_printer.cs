using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.yuyue
{
    public partial class yy_asd_printer : UserControl
    {
        private Rectangle _rectBody;
        
        private yy_asd_tabNotMap _obj = null;

        public yy_asd_printer(yy_asd_tabNotMap obj)
        {
            InitializeComponent();
            _obj = obj;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "预约单";

            printDoc.PrintController = new StandardPrintController();//隐藏打印进度对话框
            printDoc.DefaultPageSettings.Margins = new Margins(60, 80, 60, 30);//设置上下左右边距
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            int printableWidth = printDoc.DefaultPageSettings.PaperSize.Width - printDoc.DefaultPageSettings.Margins.Left
                - printDoc.DefaultPageSettings.Margins.Right;
            int printalbeHeight = printDoc.DefaultPageSettings.PaperSize.Height - printDoc.DefaultPageSettings.Margins.Top
                - printDoc.DefaultPageSettings.Margins.Bottom;

            _rectBody = new Rectangle(printDoc.DefaultPageSettings.Margins.Left, printDoc.DefaultPageSettings.Margins.Top, printableWidth, printalbeHeight);

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
            PrintBody(e.Graphics);
        }

        //打印页面主体部分
        private void PrintBody(Graphics g)
        {
            #region 打印表头
            int top = _rectBody.Top - 10;
            int left = _rectBody.Left;
            int height = 45;
            Rectangle rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("礼嘉儿保科专科门诊预约告知单", new Font("宋体", 9f,FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印预约信息
            StringFormat sf = new StringFormat();
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(Color.Black);

            string yyxinxi = _obj.childname + "预约的"+_obj.yy_xm+"专科门诊于" + _obj.yy_rq + " " + _obj.yy_sjd 
                + "时在礼嘉儿保科5楼B区分诊台分诊，请您按照预约的时间准时到达。未在预约时间内到达或临时不来就诊，不给予退费和临时更改预约时间。";

            Rectangle rect = new Rectangle(left, top, _rectBody.Width, _rectBody.Height-30);
            MiddleLeftPrintText(yyxinxi, rect, g);

            top += _rectBody.Height - 30;
            #endregion

            #region 签名
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("家长签字:", rect, g);
            
            #endregion
            

        }


        private void MiddleCenterPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
        }
        
        private void MiddleLeftPrintText(string text, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 9f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }
        private void BoldMiddleLeftPrintText(string text,Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 9f, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }
       
    }
}
