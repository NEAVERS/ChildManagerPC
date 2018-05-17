using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_asd1_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_ASD1_TAB _obj = null;

        public cp_asd1_printer(TB_CHILDBASE baseobj, CP_ASD1_TAB obj)
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
            MiddleCenterPrintText("神经心理测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印病人信息
            StringFormat sf = new StringFormat();
            Pen pen = new Pen(Color.Black);
            Pen bpen = new Pen(Color.Black, 3);
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
            MiddleCenterPrintText("姓名", rect, g);
            left += nameheadwidth;
			
            rect = new Rectangle(left, top, namewidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDNAME, rect, g);
            left += namewidth;
			left += 50;
            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleCenterPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDGENDER, rect, g);
            left += sexwidth;
			left += 50;
            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleCenterPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _obj.CSRQ);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintTextAndLine(agestr, rect, g);
            left += ageheadwidth;
            
            top += 75;
            #endregion

            #region M-CHAT
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("ASD-1", rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("M-CHAT", rect, g);

            top += 25;
            left = _rectBody.Left+50;
            rect = new Rectangle(left, top, 130, 30);
            MiddleCenterPrintText("(1) 高危项目（", rect, g);

            left += 130;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(_obj.M_CHAT_GWXM, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 45, 30);
            MiddleCenterPrintText("）项", rect, g);

            top += 25;
            left = _rectBody.Left + 50;
            rect = new Rectangle(left, top, 130, 30);
            MiddleCenterPrintText("(2) 任意项目（", rect, g);

            left += 130;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(_obj.M_CHAT_RYXM, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 45, 30);
            MiddleCenterPrintText("）项", rect, g);

            top += 25;
            left = _rectBody.Left + 50;
            rect = new Rectangle(left, top, 100, 30);
            MiddleCenterPrintText("结论："+_obj.M_CHAT_JL, rect, g);
            #endregion

            #region ASD-3
            top += 80;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("ASD-3", rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("ABC", rect, g);

            top += 25;
            left = _rectBody.Left + 50;
            rect = new Rectangle(left, top, 80, 30);
            MiddleCenterPrintText("得分：（", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(_obj.ABC_DF, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 45, 30);
            MiddleCenterPrintText("）分", rect, g);
            
            top += 25;
            left = _rectBody.Left + 50;
            rect = new Rectangle(left, top, 100, 30);
            MiddleCenterPrintText("结论：" + _obj.ABC_JL, rect, g);
            #endregion

            #region 签名

            top += 60;

            if (_obj.BZ_VISIBLE != null)
            {
                if (_obj.BZ_VISIBLE.Contains("病人可见"))
                {
                    left = _rectBody.Left;
                    rect = new Rectangle(left, top, 50, 30);
                    BoldMiddleLeftPrintText("备注:", rect, g);

                    left += 50;
                    rect = new Rectangle(left, top, _rectBody.Width - 100, 130);
                    MiddleLeftPrintText(_obj.BZ, rect, g);

                    top += 150;
                }
            }

            left = _rectBody.Left + 70;
            rect = new Rectangle(left, top, 80, 30);
            MiddleCenterPrintText("送诊医生", rect, g);
            left += 80;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintTextNoBold(_obj.SZYS, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);

            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintText(_obj.CSZQM, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleCenterPrintText("测试者签名", rect, g);

            top += 25;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintText(_obj.CSRQ, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleCenterPrintText("  测试日期", rect, g);
            #endregion

            top += 50;

            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, 30);
            MiddleCenterPrintText("说明：本结论为一次测试结果，具体诊断请结合临床", rect, g);

        }


        private void MiddleCenterPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
        }

        private void MiddleCenterPrintTextNoBold(string text, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
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
        private void MiddleCenterPrintText(string text, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
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
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 9f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }

    }
}
