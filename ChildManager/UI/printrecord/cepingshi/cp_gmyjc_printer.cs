using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_gmyjc_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_GMYJC_TAB _obj = null;

        public cp_gmyjc_printer(TB_CHILDBASE baseobj, CP_GMYJC_TAB obj)
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
            MiddleCenterPrintText("过敏源检测测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印病人信息

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            StringFormat lf = new StringFormat();
            lf.Alignment = StringAlignment.Near;
            lf.LineAlignment = StringAlignment.Center;
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
            MiddleLeftPrintTextAndLine(_baseobj.CHILDNAME, rect, g);
            left += namewidth;
			left += 50;
            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDGENDER, rect, g);
            left += sexwidth;
			left += 50;
            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _obj.CSRQ);
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
            Font font = new Font("宋体", 12f);
            rect = new Rectangle(left,top, Width, High);
            g.DrawString("    食物过敏皮肤点刺试验是常用的食物过敏诊断方法，操作简便，安全，国际上已开", font,brush,rect, lf);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("展数十年，在我国也有近十年的历史。该检查用专用的皮肤点刺针将特异的食物抗原提", font, brush, rect, lf);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("取物轻点皮肤上，15分钟后观察结果，为儿童及家长提供合理的喂养指导。以下儿童需", font, brush, rect, lf);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("做食物过敏点刺试验。", font, brush, rect,lf);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("1、将要添加哺食的婴儿", font, brush, rect, lf);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("2、有可疑食物过敏症状的儿童: 如反复湿疹、红斑、呕吐、腹泻、哮喘、拒食等。", font, brush, rect, lf);
            top += High ;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("食物名称", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("丘疹/红晕\r\n(mm)", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("+~++++", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("食物名称", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("丘疹/红晕\r\n(mm)", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("+~++++", font, brush, rect, sf);
            top += High * 3 / 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("鸡肉", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.JR_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.JR_RESULT, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("小麦", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.XM_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.XM_RESULT, font, brush, rect, sf);
            top += High * 3 / 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("鸡蛋", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.JD_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.JD_RESULT, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("橘子", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.JZ_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.JZ_RESULT, font, brush, rect, sf);
            top += High * 3 / 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("牛奶", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.NN_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.NN_RESULT, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("核桃", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.HT_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.HT_RESULT, font, brush, rect, sf);
            top += High * 3 / 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("大豆", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.DD_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.DD_RESULT, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("大米", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.DM_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.DM_RESULT, font, brush, rect, sf);
            top += High * 3 / 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("花生", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.HS_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.HS_RESULT, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("菠菜", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.BC_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.BC_RESULT, font, brush, rect, sf);
            top += High * 3 / 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("鲫鱼", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.JY_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.JY_RESULT, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("阳性对照", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.YANG_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("", font, brush, rect, sf);
            top += High * 3 / 2;

            rect = new Rectangle(left + Width / 6 * 0, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("虾", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 1, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.X_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 2, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.X_RESULT, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 3, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("阴性对照", font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 4, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.YIN_QIUZHEN, font, brush, rect, sf);
            rect = new Rectangle(left + Width / 6 * 5, top, Width / 6, High * 3 / 2);
            g.DrawRectangle(pen, rect);
            g.DrawString("", font, brush, rect, sf);
            top += High * 3 / 2;

            Pen linepen = new Pen(Color.Red,2f);
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("注意事项：", font, brush, rect, lf);
            g.DrawLine(linepen, left, top + High - 5, left + 80, top + High - 5);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("1、食物过敏点刺试验是最安全的检查方法，但极少数的儿童可出现迟发的过敏症状，", font, brush, rect, lf);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("   因此食物过敏检测后回家应继续观察3-5小时，如果儿童出现喘息、皮肤瘙痒、血", font, brush, rect, lf);
            g.DrawLine(linepen, left + 230, top + High - 5, left + Width - 40, top + High - 5);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("   管性水肿、荨麻疹等，请立刻到医院检查治疗。", font, brush, rect, lf);
            g.DrawLine(linepen, left + 30, top + High - 5, left + 370, top + High - 5);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("2、皮肤点刺试验阳性者、需由医生结合病史综合考虑，必要时做食物激发测试，以进", font, brush, rect, lf);
            top += High + 5;
            rect = new Rectangle(left, top, Width, High);
            g.DrawString("   一步诊断和治疗。", font, brush, rect, lf);
            top += High*2;


            #endregion

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
