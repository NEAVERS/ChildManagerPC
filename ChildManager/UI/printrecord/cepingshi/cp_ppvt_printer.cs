using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_ppvt_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_PPVT_TAB _obj = null;

        public cp_ppvt_printer(TB_CHILDBASE baseobj, CP_PPVT_TAB obj)
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
            MiddleCenterPrintText("心理测验报告", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
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

            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _obj.CYRQ);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintText(agestr, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += ageheadwidth;

            top += 30;
            #endregion

            top += 60;
            #region 图片词汇测试（PPVT）
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, height);
            MiddleLeftPrintText("图片词汇测试结果：", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);//
            g.DrawLine(pen, rect.Left, rect.Top + 30, rect.Right-20, rect.Top + 30);

            top += 60;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("粗分：" + _obj.PPVT_DD, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("IQ：" + _obj.PPVT_CW, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("P：" + _obj.PPVT_ZS, rect, g);

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 55, 30);
            MiddleLeftPrintText("智龄:(", rect, g);
            left += 55;
            rect = new Rectangle(left, top, 100, 30);
            MiddleCenterPrintText(_obj.PPVT_ZL, new Font("宋体", 11f), brush, rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")岁(", rect, g);
            left += 40;
            rect = new Rectangle(left, top, 100, 30);
            MiddleCenterPrintText(_obj.PPVT_DF, new Font("宋体", 11f), brush, rect, g);
            left += 100;
            rect = new Rectangle(left, top, 80, 30);
            MiddleLeftPrintText(")月", rect, g);

            #endregion

            top += 60;
            #region 绘人试验
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 120, height);
            MiddleLeftPrintText("绘人试验：", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);//
            g.DrawLine(pen, rect.Left, rect.Top + 30, rect.Right-20, rect.Top + 30);

            top += 60;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("总分：" + _obj.HRSY_ZF, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("智商：" + _obj.HRSY_ZS, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("智龄：" + _obj.HRSY_ZL, rect, g);

            #endregion

            top += 60;
            #region 学前儿童能力测验
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 190, height);
            MiddleLeftPrintText("学前儿童能力测验：", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);//
            g.DrawLine(pen, rect.Left, rect.Top + 30, rect.Right-20, rect.Top + 30);

            top += 60;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("得分：" + _obj.NLCY_DF, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("结果：" + _obj.NLCY_JG, rect, g);
            
            #endregion
      
            top += 110;
            #region 签名
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.CYZ, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("测查者签名", rect, g);

            top += 25;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.CYRQ, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("  测查日期", rect, g);
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

        private void MiddleLeftPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
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
