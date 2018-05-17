using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_cdi_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_CDI_TAB _obj = null;

        public cp_cdi_printer(TB_CHILDBASE baseobj, CP_CDI_TAB obj)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _obj = obj;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "神经心理测试结果";

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
            string projectName = CommonHelper.GetHospitalName();
            MiddleCenterPrintText(projectName, new Font("宋体", 16f,FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("重庆市儿童行为发育与心理健康中心", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("神经心理测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印病人信息
            StringFormat sf = new StringFormat();
            Pen pen = new Pen(Color.Black);
            Pen bpen = new Pen(Color.Black,3);
            Brush brush = new SolidBrush(Color.Black);
            
            g.DrawLine(bpen, left, top, left + _rectBody.Width, top);
            top += 20;

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
            MiddleLeftPrintText(_baseobj.CHILDNAME,rect,g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += namewidth;

            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintText(_baseobj.CHILDGENDER, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += sexwidth;

            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _obj.CSRQ);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintText(agestr, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += ageheadwidth;

            top += 30;
            #endregion

            left = _rectBody.Left;
            Rectangle cdirect = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("CDI短表（词汇及手势）测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), cdirect, g);//画医院名称

            top += 60;
            #region 1.语言理解
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 140, 30);
            MiddleLeftPrintText("1.语言理解  （",rect,g);
            left += 140;
            rect = new Rectangle(left, top, 30, 30);
            MiddleLeftPrintText(_obj.YYLJ_DF, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 30, 30);
            MiddleLeftPrintText("）", rect, g);

            left += 30;
            rect = new Rectangle(left, top, 140, 30);
            MiddleLeftPrintText("（相当于同月龄", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 50, 30);
            MiddleLeftPrintText(_obj.YYLJ_YL, rect, g);

            left += 50;
            rect = new Rectangle(left, top, 30, 30);
            MiddleLeftPrintText("）", rect, g);

            top += 30;
            left = _rectBody.Left + 200;
            rect = new Rectangle(left, top, 80, 30);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.YYLJ_P50, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText("月P50水平）", rect, g);

            top += 30;
            left = _rectBody.Left + 200;
            rect = new Rectangle(left, top, 80, 30);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.YYLJ_P75, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText("月P75水平）", rect, g);
            #endregion

            top += 60;
            #region 2.	词汇表达
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 140, 30);
            MiddleLeftPrintText("2.词汇表达  （", rect, g);
            left += 140;
            rect = new Rectangle(left, top, 30, 30);
            MiddleLeftPrintText(_obj.CHBD_DF, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 30, 30);
            MiddleLeftPrintText("）", rect, g);

            left += 30;
            rect = new Rectangle(left, top, 140, 30);
            MiddleLeftPrintText("（相当于同月龄", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 50, 30);
            MiddleLeftPrintText(_obj.CHBD_YL, rect, g);

            left += 50;
            rect = new Rectangle(left, top, 30, 30);
            MiddleLeftPrintText("）", rect, g);

            top += 30;
            left = _rectBody.Left + 200;
            rect = new Rectangle(left, top, 80, 30);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.CHBD_P50, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText("月P50水平）", rect, g);

            top += 30;
            left = _rectBody.Left + 200;
            rect = new Rectangle(left, top, 80, 30);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.CHBD_P75, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText("月P75水平）", rect, g);
            #endregion

            #region 3. 病史提供者，儿童带养人
            top += 60;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("病史提供者", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintTextAndLine(_obj.BSTGZ, rect, g);

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("儿童带养人", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintTextAndLine(_obj.ETDYR, rect, g);

            #endregion
            top += 50;
            #region 签名
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.CSZQM, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("测试者签名", rect, g);

            top += 25;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.CSRQ, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("  测试日期", rect, g);
            #endregion

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, 30);
            MiddleLeftPrintText("说明：本结论为一次测试结果，具体诊断请结合临床", rect, g);

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
            Font printFont = new Font("宋体", 11f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }
        private void BoldMiddleLeftPrintText(string text,Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }

        private void MiddleLeftPrintTextAndLine(string text, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f);
            Pen pen = new Pen(Color.Black);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
        }
    }
}
