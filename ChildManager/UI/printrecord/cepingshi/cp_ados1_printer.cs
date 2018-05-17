using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_ados1_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_ADOS1_TAB _obj = null;

        public cp_ados1_printer(TB_CHILDBASE baseobj, CP_ADOS1_TAB obj)
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
            printDoc.DefaultPageSettings.Margins = new Margins(80, 80, 40, 30);//设置上下左右边距
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
            MiddleCenterPrintText(projectName, new Font("宋体", 16f,FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("重庆市儿童行为发育与心理健康中心", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("自闭症诊断观察量表（ADOS）  单元一", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印病人信息
            StringFormat sf = new StringFormat();
            Pen pen = new Pen(Color.Black);
            Pen bpen = new Pen(Color.Black,3);
            Brush brush = new SolidBrush(Color.Black);
            
            g.DrawLine(bpen, left, top, left + _rectBody.Width, top);
            top += 20;

            int nameheadwidth = 70;
            int namewidth = 120;
            int sexheadwidth = 45;
            int sexwidth = 45;
            int birthheadwidth = 70;
            int birthwidth = 120;
            int ageheadwidth = 45;
            int agewidth = 120;
            int pgrqheadwidth = 70;
            int pgrqwidth = 120;
            int pgzheadwidth = 70;
            int pgzwidth = 120;
            int szysheadwidth = 80;
            int szyswidth = 120;

            Rectangle rect = new Rectangle(left, top, nameheadwidth, 30);
            MiddleLeftPrintText("儿童姓名", rect, g);
            left += nameheadwidth;

            rect = new Rectangle(left, top, namewidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDNAME,rect,g);
            left += namewidth;

            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDGENDER, rect, g);
            left += sexwidth;

            rect = new Rectangle(left, top, birthheadwidth, 30);
            MiddleLeftPrintText("出生日期", rect, g);
            left += birthheadwidth;

            rect = new Rectangle(left, top, birthwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDBIRTHDAY, rect, g);
            left += birthwidth;

            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _obj.PGRQ);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintTextAndLine(agestr, rect, g);
            left += ageheadwidth;

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, pgrqheadwidth, 30);
            MiddleLeftPrintText("评估日期", rect, g);
            left += pgrqheadwidth;

            rect = new Rectangle(left, top, pgrqwidth, 30);
            MiddleLeftPrintTextAndLine(_obj.PGRQ, rect, g);
            left += pgrqwidth;

            rect = new Rectangle(left, top, pgzheadwidth, 30);
            MiddleLeftPrintText("评估者", rect, g);
            left += pgzheadwidth;

            rect = new Rectangle(left, top, pgzwidth, 30);
            MiddleLeftPrintTextAndLine(_obj.PGZQM, rect, g);
            left += pgzwidth + 60;

            rect = new Rectangle(left, top, szysheadwidth, 30);
            MiddleLeftPrintText("送诊医生", rect, g);
            left += szysheadwidth - 5;

            rect = new Rectangle(left, top, szyswidth, 30);
            MiddleLeftPrintTextAndLine(_obj.SZYS, rect, g);
            left += szyswidth;
            top += 25;
            #endregion

            #region 沟通
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("沟通", rect,g);
            
            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("朝向他人发声的频率", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.GT_CXTR, rect, g);
            

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("刻板的/特异的使用单字或片语", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.GT_KB, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("使用他人身体来沟通", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.GT_SYTRST, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("指物动作", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.GT_ZWDZ, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("姿势动作", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.GT_ZSDZ, rect, g);

            top += 25;
            left = _rectBody.Left+300;
            rect = new Rectangle(left, top, 180, 30);
            BoldMiddleLeftPrintText("           沟通总分：", rect, g);
            left += 180;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.GT_SUM, rect, g);

            top += 20;
            left = _rectBody.Left + 200;
            rect = new Rectangle(left, top, 400, 30);
            SmallMiddleLeftPrintText("（自闭症切截点=4 : 自闭症类群切截点=2）", rect, g);

            top += 20;
            #endregion

            #region 相互性社会互动
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("相互性社会互动", rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("不寻常的眼神接触", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.HD_BXC, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("朝向他人的脸部表情", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.HD_CXTR, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("互动中共享乐趣", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.HD_HDZGXLQ, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("展示", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.HD_ZS, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("自发地主动产生相互协调注意力", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.HD_ZFDZD, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("对相互协调注意力的反应", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.HD_DXHXT, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("主动表达社交意向的品质", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.HD_ZDBD, rect, g);

            top += 25;
            left = _rectBody.Left + 300;
            rect = new Rectangle(left, top, 180, 30);
            BoldMiddleLeftPrintText("     社会互动 总分：", rect, g);
            left += 180;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.HD_SUM, rect, g);

            top += 20;
            left = _rectBody.Left + 200;
            rect = new Rectangle(left, top, 400, 30);
            SmallMiddleLeftPrintText("（自闭症切截点=7 :  自闭症类群切截点=4 ）", rect, g);

            top += 20;
            left = _rectBody.Left + 300;
            rect = new Rectangle(left, top, 180, 30);
            BoldMiddleLeftPrintText("沟通+社会互动 总分：", rect, g);
            left += 180;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.GT_HD_SUM, rect, g);

            top += 20;
            left = _rectBody.Left + 200;
            rect = new Rectangle(left, top, 400, 30);
            SmallMiddleLeftPrintText("（自闭症切截点=12: 自闭症类群切截点= 7 ）", rect, g);

            top += 15;
            #endregion

            #region 游戏
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("游戏", rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("使用物品的功能性游戏", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.YX_SYWP, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("想象/创造力", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.YX_XX, rect, g);

            top += 25;
            left = _rectBody.Left + 300;
            rect = new Rectangle(left, top, 180, 30);
            BoldMiddleLeftPrintText("           游戏总分：", rect, g);
            left += 180;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.YX_SUM, rect, g);
            #endregion

            #region 刻板行为和局限兴趣
            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("刻板行为和局限兴趣", rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("对游戏素材/人的不寻常感官兴趣", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.KBXW_DYXSC, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("手和手指及其他复杂的特殊习性动作", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.KBXW_SHSZ, rect, g);

            top += 25;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 400, 30);
            MiddleLeftPrintText("不寻常的重复兴趣或刻板行为", rect, g);
            left += 400;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.KBXW_BTXC, rect, g);

            top += 25;
            left = _rectBody.Left + 300;
            rect = new Rectangle(left, top, 180, 30);
            BoldMiddleLeftPrintText("刻板行为和局限兴趣：", rect, g);
            left += 180;
            rect = new Rectangle(left, top, 45, 30);
            MiddleLeftPrintTextAndLine(_obj.KBXW_SUM, rect, g);
            #endregion

            #region 诊断标准
            //top += 25;
            //left = _rectBody.Left;
            //rect = new Rectangle(left, top, 400, 30);
            //MiddleLeftPrintText("诊断标准：", rect, g);

            //top += 25;
            //left = _rectBody.Left;
            //rect = new Rectangle(left, top, 300, 25);
            //g.DrawRectangle(pen,rect);
            //BoldMiddleLeftPrintText("全量表总分切截点", rect, g);
            //left += 300;
            //rect = new Rectangle(left, top, 250, 25);
            //g.DrawRectangle(pen, rect);
            //BoldMiddleLeftPrintText("得分", rect, g);

            //top += 25;
            //left = _rectBody.Left;
            //rect = new Rectangle(left, top, 300, 25);
            //g.DrawRectangle(pen, rect);
            //BoldMiddleLeftPrintText("孤独症", rect, g);
            //left += 300;
            //rect = new Rectangle(left, top, 250, 25);
            //g.DrawRectangle(pen, rect);
            //BoldMiddleLeftPrintText("≥12分", rect, g);

            //top += 25;
            //left = _rectBody.Left;
            //rect = new Rectangle(left, top, 300, 25);
            //g.DrawRectangle(pen, rect);
            //BoldMiddleLeftPrintText("孤独症谱系障碍", rect, g);
            //left += 300;
            //rect = new Rectangle(left, top, 250, 25);
            //g.DrawRectangle(pen, rect);
            //BoldMiddleLeftPrintText("7-11分", rect, g);

            //top += 25;
            //left = _rectBody.Left;
            //rect = new Rectangle(left, top, 300, 25);
            //g.DrawRectangle(pen, rect);
            //BoldMiddleLeftPrintText("非孤独症谱系障碍", rect, g);
            //left += 300;
            //rect = new Rectangle(left, top, 250, 25);
            //g.DrawRectangle(pen, rect);
            //BoldMiddleLeftPrintText("≤6分", rect, g);
            #endregion

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
                        
            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, 30);
            MiddleLeftPrintText("此结论仅反应儿童当前发育水平，请结合临床。", rect, g);

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

        private void BoldMiddleLeftPrintText(string text,Rectangle rect, Graphics g)
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
