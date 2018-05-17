using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_gyqt_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_GYQT_TAB _gyobj = null;

        public cp_gyqt_printer(TB_CHILDBASE baseobj, CP_GYQT_TAB gyobj)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _gyobj = gyobj;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "构音测试结果";

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

            int heigh = 29;

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
            MiddleCenterPrintText("构音测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印病人信息

            StringFormat sf = new StringFormat();
            Pen pen = new Pen(Color.Black);
            Pen bpen = new Pen(Color.Black,3);
            Brush brush = new SolidBrush(Color.Black);
            Font printFont = new Font("宋体", 11f);
            

            g.DrawLine(bpen, left, top, left + _rectBody.Width, top);
            top += 20;

            int nameheadwidth = 45;
            int namewidth = 120;
            int sexheadwidth = 45;
            int sexwidth = 120;
            int ageheadwidth = 45;
            int agewidth = 120;

            left = _rectBody.Left + 50;

            Rectangle rect = new Rectangle(left, top, nameheadwidth, heigh);
            MiddleLeftPrintText("姓名", rect, g);
            left += nameheadwidth;

            rect = new Rectangle(left, top, namewidth, heigh);
            MiddleLeftPrintText(_baseobj.CHILDNAME,rect,g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += namewidth + 50;

            rect = new Rectangle(left, top, sexheadwidth, heigh);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, heigh);
            MiddleLeftPrintText(_baseobj.CHILDGENDER, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += sexwidth + 50;

            rect = new Rectangle(left, top, ageheadwidth, heigh);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _gyobj.CSRQ);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintText(agestr, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += ageheadwidth;
            top += heigh *2;

            #endregion
            
            #region DDST

            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, heigh);
            MiddleLeftPrintText("DDST测试结果", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);

            top += heigh + 10;

            rect = new Rectangle(left + _rectBody.Width / 4 * 0, top, _rectBody.Width / 4, heigh);
            MiddleLeftPrintText("个人-社会能区:", rect, g);
            rect = new Rectangle(left + _rectBody.Width / 4 * 1, top, _rectBody.Width / 4, heigh);
            MiddleLeftPrintText(_gyobj.DDST_GRYSH, rect, g);
            rect = new Rectangle(left + _rectBody.Width / 4 * 2, top, _rectBody.Width / 4, heigh);
            MiddleLeftPrintText("精细运动能区:", rect, g);
            rect = new Rectangle(left + _rectBody.Width / 4 * 3, top, _rectBody.Width / 4, heigh);
            MiddleLeftPrintText(_gyobj.DDST_JXYDNQ, rect, g);

            top += heigh + 5;

            rect = new Rectangle(left + _rectBody.Width / 4 * 0, top, _rectBody.Width / 4, heigh);
            MiddleLeftPrintText("     语言能区:", rect, g);
            rect = new Rectangle(left + _rectBody.Width / 4 * 1, top, _rectBody.Width / 4, heigh);
            MiddleLeftPrintText(_gyobj.DDST_YYNQ, rect, g);
            rect = new Rectangle(left + _rectBody.Width / 4 * 2, top, _rectBody.Width / 4, heigh);
            MiddleLeftPrintText("  大运动能区:", rect, g);
            rect = new Rectangle(left + _rectBody.Width / 4 * 3, top, _rectBody.Width / 4, heigh);
            MiddleLeftPrintText(_gyobj.DDST_DYDNQ, rect, g);

            top += heigh + 5;

            rect = new Rectangle(left + _rectBody.Width / 4 * 0, top, _rectBody.Width / 5, heigh);
            MiddleLeftPrintText("         结果:", rect, g);
            rect = new Rectangle(left + _rectBody.Width / 5 * 1, top, _rectBody.Width , heigh);
            MiddleLeftPrintText(_gyobj.DDST_JG, rect, g);

            top += heigh *2;

            #endregion

            #region 图片词汇测试结果

            rect = new Rectangle(left, top, _rectBody.Width, heigh);
            MiddleLeftPrintText("图片词汇测试结果", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);

            top += heigh + 10;

            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("粗分：" + _gyobj.PPVT_CF, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("IQ：" + _gyobj.PPVT_IQ, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("P：" + _gyobj.PPVT_P, rect, g);

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 55, 30);
            MiddleLeftPrintText("智龄:(", rect, g);
            left += 55;
            rect = new Rectangle(left, top, 100, 30);
            MiddleCenterPrintText(_gyobj.PPVT_YEAR, new Font("宋体", 11f), brush, rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")岁(" , rect, g);
            left += 40;
            rect = new Rectangle(left, top, 100, 30);
            MiddleCenterPrintText( _gyobj.PPVT_MOUTH, new Font("宋体", 11f), brush, rect, g);
            left += 100;
            rect = new Rectangle(left, top, 80, 30);
            MiddleLeftPrintText(")月", rect, g);


            top += heigh *2;

            #endregion

            #region 打印声母韵母测试结果

            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, heigh);
            MiddleLeftPrintText("声母韵母测试结果", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);

            top += heigh + 10;

            int x = _rectBody.Left + 20;
            rect = new Rectangle(x, top, 300, heigh);
            BoldMiddleLeftPrintText("声母测试结果", rect, g);

            rect = new Rectangle(x+350, top, 300, heigh);
            BoldMiddleLeftPrintText("韵母测试结果", rect, g);

            top += heigh;

            string[] smstr = null;
            string sm_print = "";
            if (!string.IsNullOrEmpty(_gyobj.SHENGMU ))
            {
                smstr = _gyobj.SHENGMU.Split('〓');
            }
            if (smstr!=null)
            {
                for (int i = 0; i < smstr.Length; i++)
                {
                    sm_print += smstr[i].Replace("&", "") + " \r\n";
                }
            }
            rect = new Rectangle(x+70, top, 300, heigh * 10);
            g.DrawString(sm_print, printFont,brush, rect,sf);

            string[] ymstr = null;
            string ym_print = "";
            if (!string.IsNullOrEmpty(_gyobj.YUNMU))
            {
                ymstr = _gyobj.YUNMU.Split('〓');
            }
            if (ymstr != null)
            {
                for (int i = 0; i < ymstr.Length; i++)
                {
                    ym_print += ymstr[i].Replace("&", "") + " \r\n";
                }
            }
            rect = new Rectangle(x + 420, top, 300, heigh * 10);
            g.DrawString(ym_print, printFont, brush, rect, sf);

            top += heigh * 10;

            #endregion

            #region 签名

            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText(_gyobj.CSZQM, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, heigh);
            MiddleLeftPrintText("测试者签名", rect, g);

            top += heigh;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText(_gyobj.CSRQ, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, heigh);
            MiddleLeftPrintText("  测试日期", rect, g);

            #endregion

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, 30);
            MiddleLeftPrintText("说明：本结论为一次测试结果，具体诊断请结合临床", rect, g);
        }

        private void MiddleLeftPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
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
