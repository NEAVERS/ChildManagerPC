using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_dream_printer : UserControl
    {
        private Rectangle _rectBody;

        private tb_childbase _baseobj = null;
        private cp_dream_tab _obj = null;

        public cp_dream_printer(tb_childbase baseobj, cp_dream_tab obj)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _obj = obj;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "过敏原检测结果";

            printDoc.PrintController = new StandardPrintController();//隐藏打印进度对话框
            printDoc.DefaultPageSettings.Margins = new Margins(80, 80, 80, 30);//设置上下左右边距
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
            int height = 40;
            Rectangle rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            string projectName = CommonHelper.GetHospitalName();
            MiddleCenterPrintText(projectName, new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("重庆市儿童行为发育与心理健康中心", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("梦想标准化语言评估报告", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印病人信息

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            StringFormat lf = new StringFormat();
            lf.Alignment = StringAlignment.Near;
            lf.LineAlignment = StringAlignment.Center;
            StringFormat lf1 = new StringFormat();
            lf1.Alignment = StringAlignment.Near;
            lf1.LineAlignment = StringAlignment.Near;
            Pen pen = new Pen(Color.Black);
            Pen bpen = new Pen(Color.Black, 3);

            Brush brush = new SolidBrush(Color.Black);

            int nameheadwidth = 45;
            int namewidth = 120;
            int sexheadwidth = 45;
            int sexwidth = 120;
            int ageheadwidth = 45;
            int agewidth = 120;

            Rectangle rect = new Rectangle(left, top, nameheadwidth, 30);
            MiddleLeftPrintText("姓名", rect, g);
            left += nameheadwidth;

            rect = new Rectangle(left, top, namewidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.childname, rect, g);
            left += namewidth;
			left += 50;
            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.childgender, rect, g);
            left += sexwidth;
			left += 50;
            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.childbirthday, _obj.csrq);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintTextAndLine(agestr, rect, g);
            left += ageheadwidth;

            top += 75;
            #endregion

            #region  主体内容

            left = _rectBody.Left ;
            int Width = 660;
            int High = 30;
            Font titlefont = new Font("宋体", 12f,FontStyle.Bold);
            Font font = new Font("宋体", 11f);
            rect = new Rectangle(left, top, Width, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("测试和诊断结果\r\n STANDARD SCORES & PERECNTILES", titlefont, brush, rect, sf);
            top += High * 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3);
            g.DrawRectangle(pen, rect);
            g.DrawString("语言标准分\r\n LANGUAGE STANDARD SCORES", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3);
            g.DrawRectangle(pen, rect);
            g.DrawString("整体语言\r\n TOTAL LANGUAGE", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3);
            g.DrawRectangle(pen, rect);
            g.DrawString("听力理解\r\n RECEPTIVE", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3);
            g.DrawRectangle(pen, rect);
            g.DrawString("语言表达\r\n EXPRESSIVE", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3);
            g.DrawRectangle(pen, rect);
            g.DrawString("语义\r\n SEMANTICS", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3);
            g.DrawRectangle(pen, rect);
            g.DrawString("句法\r\n SYNTAX", font, brush, rect, sf);
            top += High * 3;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("标准化分数\r\n STANDARD SCORES", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bzh_ztyy, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bzh_tllj, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bzh_yybd, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bzh_yy, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bzh_jf, font, brush, rect, sf);
            top += High * 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("百分等级\r\n PERCENTILES", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bfdj_ztyy, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bfdj_tllj, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bfdj_yybd, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bfdj_yy, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.bfdj_jf, font, brush, rect, sf);
            top += High * 2;

            rect = new Rectangle(left, top, Width, High * 8);
            g.DrawRectangle(pen, rect);
            rect = new Rectangle(left + 3, top + 3, Width - 6, High * 8 - 6);
            g.DrawString(_obj.doctor_result, font, brush, rect, lf);
            top += High * 8 ;

            rect = new Rectangle(left, top, Width, High * 8);
            g.DrawRectangle(pen, rect);
            rect = new Rectangle(left + 3, top + 3, Width - 6, High * 8 - 6);
            g.DrawString(_obj.doctor_advice, font, brush, rect, lf);
            top += High * 8 + 10;

            top += High;


            #endregion

            #region 签名

            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.cszqm, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("测试者签名", rect, g);

            top += 25;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.csrq, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("  测试日期", rect, g);
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
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }
        
		 private void MiddleLeftPrintText1(string text, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }
		 
        private void MiddleLeftPrintTextAndLine(string text, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f);
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
        }

        private void BoldMiddleLeftPrintText(string text, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }

        private void SmallMiddleLeftPrintText(string text, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 9f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }

    }
}
