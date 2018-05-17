using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_gesell_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_GESELL_TAB _obj = null;

        public cp_gesell_printer(TB_CHILDBASE baseobj, CP_GESELL_TAB obj)
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
            MiddleCenterPrintText("发育商测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
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
            int namewidth = 100;
            int sexheadwidth = 45;
            int sexwidth = 70;
            int ageheadwidth = 45;
            int agewidth = 100;
            int birthheadwidth = 70;
            int birthwidth = 120;

            Rectangle rect = new Rectangle(left, top, nameheadwidth, 30);
            MiddleCenterPrintText("姓名", rect, g);
            left += nameheadwidth;

            rect = new Rectangle(left, top, namewidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDNAME, rect, g);
            left += namewidth;

            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleCenterPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDGENDER, rect, g);
            left += sexwidth;

            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleCenterPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _obj.CSRQ);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintTextAndLine(agestr, rect, g);
            left += agewidth;

            rect = new Rectangle(left, top, birthheadwidth, 30);
            MiddleCenterPrintText("出生日期", rect, g);
            left += birthheadwidth;

            rect = new Rectangle(left, top, birthwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDBIRTHDAY, rect, g);
            left += birthwidth;

            top += 75;
            #endregion

            #region Gesell测试结果
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("Gesell测试结果", rect, g);

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 180, 30);
            MiddleCenterPrintText("1.适应性     发育商DQ(", rect, g);

            left += 180;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(_obj.SYX_DQ, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 140, 30);
            MiddleCenterPrintText(")分   （DA相当于", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 60, 30);
            MiddleCenterPrintText(_obj.SYX_DA, rect, g);

            left += 60;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText("月）", rect, g);

            top += 30;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 180, 30);
            MiddleCenterPrintText("2.大运动能   发育商DQ(", rect, g);

            left += 180;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(_obj.DYDN_DQ, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 140, 30);
            MiddleCenterPrintText(")分   （DA相当于", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 60, 30);
            MiddleCenterPrintText(_obj.DYDN_DA, rect, g);

            left += 60;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText("月）", rect, g);

            top += 30;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 180, 30);
            MiddleCenterPrintText("3.细运动能   发育商DQ(", rect, g);

            left += 180;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(_obj.XYDN_DQ, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 140, 30);
            MiddleCenterPrintText(")分   （DA相当于", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 60, 30);
            MiddleCenterPrintText(_obj.XYDN_DA, rect, g);

            left += 60;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText("月）", rect, g);

            top += 30;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 180, 30);
            MiddleCenterPrintText("4.言语能     发育商DQ(", rect, g);

            left += 180;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(_obj.YYN_DQ, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 140, 30);
            MiddleCenterPrintText(")分   （DA相当于", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 60, 30);
            MiddleCenterPrintText(_obj.YYN_DA, rect, g);

            left += 60;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText("月）", rect, g);

            top += 30;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 180, 30);
            MiddleCenterPrintText("5.个人社交   发育商DQ(", rect, g);

            left += 180;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(_obj.GRSJ_DQ, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 140, 30);
            MiddleCenterPrintText(")分   （DA相当于", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 60, 30);
            MiddleCenterPrintText(_obj.GRSJ_DA, rect, g);

            left += 60;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText("月）", rect, g);


            top += 50;

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


            #endregion

            #region 合作等


            left = _rectBody.Left;
            rect = new Rectangle(left, top, 60, 30);
            MiddleCenterPrintText("合作：", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 80, 30);
            MiddleCenterPrintText(_obj.HZ, rect, g);

            left += 100;
            rect = new Rectangle(left, top, 70, 30);
            MiddleCenterPrintText("注意力：", rect, g);

            left += 70;
            rect = new Rectangle(left, top, 80, 30);
            MiddleCenterPrintText(_obj.ZYL, rect, g);

            top += 30;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 60, 30);
            MiddleCenterPrintText("反应：", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 80, 30);
            MiddleCenterPrintText(_obj.FY, rect, g);

            left += 100;
            rect = new Rectangle(left, top, 70, 30);
            MiddleCenterPrintText("  情绪：", rect, g);

            left += 70;
            rect = new Rectangle(left, top, 80, 30);
            MiddleCenterPrintText(_obj.QX, rect, g);

            #endregion

            #region 签名
            top += 50;

            left = _rectBody.Left + 70;
            rect = new Rectangle(left, top, 80, 30);
            MiddleCenterPrintText("送诊医生", rect, g);
            left += 80;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintText(_obj.SZYS, rect, g);
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

            top += 40;
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
