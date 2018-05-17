using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_wycsi_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_WYCSI_TAB _obj = null;

        public cp_wycsi_printer(TB_CHILDBASE baseobj, CP_WYCSI_TAB obj)
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
            MiddleCenterPrintText("小韦氏智力测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印病人信息
            StringFormat sf = new StringFormat();
            Pen pen = new Pen(Color.Black);
            Pen bpen = new Pen(Color.Black, 3);
            Pen bpen2 = new Pen(Color.Black, 2);
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

            top += 50;
            #endregion

            #region Gesell测试结果
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 150, 30);
            BoldMiddleLeftPrintText("C-WYCSI  测试结果", rect, g);

            left += 200;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintText("1.总智商   IQ(", rect, g);

            left += 120;
            rect = new Rectangle(left, top, 30, 30);
            MiddleCenterPrintText(_obj.ZZS, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(")分", rect, g);

            top += 30;
            left = _rectBody.Left + 200;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintText("2.语言智商 IQ(", rect, g);

            left += 120;
            rect = new Rectangle(left, top, 30, 30);
            MiddleCenterPrintText(_obj.YYZS, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(")分", rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintText("3.操作智商 IQ(", rect, g);

            left += 120;
            rect = new Rectangle(left, top, 30, 30);
            MiddleCenterPrintText(_obj.CZZS, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 40, 30);
            MiddleCenterPrintText(")分", rect, g);
            
            #endregion

            #region 表格开始
            int lfbheight = 20;
            top += 30;
            left = _rectBody.Left + 15;
            Rectangle lbfrect = new Rectangle(left, top, _rectBody.Width - 30, lfbheight * 25);
            g.DrawRectangle(bpen2, lbfrect);
            int lfbwidth = Convert.ToInt32((_rectBody.Width - 80) / 12.5);

            left = lbfrect.Left + 40;
            rect = new Rectangle(left, top + 10, 140, 30);
            MiddleCenterPrintText("言语测验 总分（", rect, g);

            left += 140;
            rect = new Rectangle(left, top + 10, 30, 30);
            MiddleCenterPrintText(_obj.YYCY_SUM, rect, g);

            left += 30;
            rect = new Rectangle(left, top + 10, 30, 30);
            MiddleCenterPrintText("）", rect, g);

            left = lbfrect.Left + lfbwidth * 6 + 20;
            rect = new Rectangle(left, top + 10, 140, 30);
            MiddleCenterPrintText("操作测验 总分（", rect, g);

            left += 140;
            rect = new Rectangle(left, top + 10, 30, 30);
            MiddleCenterPrintText(_obj.CZCY_SUM, rect, g);

            left += 30;
            rect = new Rectangle(left, top + 10, 30, 30);
            MiddleCenterPrintText("）", rect, g);

            #endregion

            #region 画分数说明
            top += lfbheight * 2;
            left = lbfrect.Left + lfbwidth / 2;
            g.DrawLine(bpen2, left, top, left + lfbwidth * 5, top);
            left = lbfrect.Left + lfbwidth * 6;
            g.DrawLine(bpen2, left, top, left + lfbwidth * 7, top);

            left = lbfrect.Left;
            rect = new Rectangle(left, top, lfbwidth / 2, lfbheight * 4);
            MiddleCenterPrintText("量表分", rect, g);

            left += lfbwidth / 2;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("知\r\n识", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("图片\r\n词汇", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("算\r\n术", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("图片\r\n概括", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("领\r\n悟", rect, g);
            
            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth / 2, lfbheight * 4);
            MiddleCenterPrintText("量表分", rect, g);

            left += lfbwidth / 2;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("动物\r\n下蛋", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("下蛋\r\n重测", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("图画\r\n填充", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("迷\r\n津", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("木块\r\n图案", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("几何\r\n图形", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText("视觉\r\n分析", rect, g);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth / 2, lfbheight * 4);
            MiddleCenterPrintText("量表分", rect, g);

            #endregion

            #region 画分数表格
            top += lfbheight * 2;
            left = lbfrect.Left + lfbwidth / 2;
            g.DrawLine(pen, left, top, left + lfbwidth * 5, top);
            left = lbfrect.Left + lfbwidth * 6;
            g.DrawLine(pen, left, top, left + lfbwidth * 7, top);

            left = lbfrect.Left + lfbwidth / 2;
            for (int i = 0; i < 6; i++)
            {
                g.DrawLine(bpen2, left, top, left, top + lfbheight * 2);
                left += lfbwidth;
            }

            left -= lfbwidth / 2;
            for (int i = 0; i < 8; i++)
            {
                g.DrawLine(bpen2, left, top, left, top + lfbheight * 2);
                left += lfbwidth;
            }

            #endregion

            #region 画言语测验分数
            IList<string> listyycy = new List<string>();
            left = lbfrect.Left + lfbwidth / 2;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.YYCY_ZS, rect, g);
            listyycy.Add(_obj.YYCY_ZS);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.YYCY_TPCH, rect, g);
            listyycy.Add(_obj.YYCY_TPCH);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.YYCY_SS, rect, g);
            listyycy.Add(_obj.YYCY_SS);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.YYCY_TPGK, rect, g);
            listyycy.Add(_obj.YYCY_TPGK);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.YYCY_LW, rect, g);
            listyycy.Add(_obj.YYCY_LW);
            
            #endregion

            #region 画操作测验分数
            IList<string> listczcy = new List<string>();
            left += lfbwidth + lfbwidth / 2;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.CZCY_DWXD, rect, g);
            listczcy.Add(_obj.CZCY_DWXD);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.CZCY_XDCC, rect, g);
            listczcy.Add(_obj.CZCY_XDCC);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.CZCY_THTC, rect, g);
            listczcy.Add(_obj.CZCY_THTC);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.CZCY_MJ, rect, g);
            listczcy.Add(_obj.CZCY_MJ);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.CZCY_MKTA, rect, g);
            listczcy.Add(_obj.CZCY_MKTA);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.CZCY_JHTX, rect, g);
            listczcy.Add(_obj.CZCY_JHTX);

            left += lfbwidth;
            rect = new Rectangle(left, top, lfbwidth, lfbheight * 2);
            MiddleCenterPrintText(_obj.CZCY_SJFX, rect, g);
            listczcy.Add(_obj.CZCY_SJFX);

            #endregion

            #region 画表格
            top += lfbheight * 2;
            left = lbfrect.Left;
            g.DrawLine(bpen2, left, top, lbfrect.Right, top);

            for (int i = 0; i < 19; i++)
            {
                rect = new Rectangle(left, top + i * lfbheight, lfbwidth / 2, lfbheight);
                MiddleCenterPrintText((19 - i).ToString(), rect, g);

                rect = new Rectangle(left + lfbwidth * 5 + lfbwidth / 2, top + i * lfbheight, lfbwidth / 2, lfbheight);
                MiddleCenterPrintText((19 - i).ToString(), rect, g);

                rect = new Rectangle(left + lfbwidth * 13, top + i * lfbheight, lfbwidth / 2, lfbheight);
                MiddleCenterPrintText((19 - i).ToString(), rect, g);

            }

            left = lbfrect.Left + lfbwidth / 2;
            top += lfbheight / 2;
            int[] xixian = { 0,3,4,7,10,11,14,17,18};
            for (int i = 0; i < 19; i++)
            {
                if (xixian.Contains(i))
                {
                    g.DrawLine(pen, left, top + i * lfbheight, left + lfbwidth * 5, top + i * lfbheight);
                    g.DrawLine(pen, left + lfbwidth * 5 + lfbwidth / 2, top + i * lfbheight, left + lfbwidth * 12 + lfbwidth / 2, top + i * lfbheight);
                }
                else
                {
                    g.DrawLine(bpen2, left, top + i * lfbheight, left + lfbwidth * 5, top + i * lfbheight);
                    g.DrawLine(bpen2, left + lfbwidth * 5 + lfbwidth / 2, top + i * lfbheight, left + lfbwidth * 12 + lfbwidth / 2, top + i * lfbheight);
                }
            }

            for (int i = 0; i < 6; i++)
            {
                g.DrawLine(bpen2, left, top, left, top + lfbheight * 18);
                left += lfbwidth;
            }

            left -= lfbwidth / 2;
            for (int i = 0; i < 8; i++)
            {
                g.DrawLine(bpen2, left, top, left, top + lfbheight * 18);
                left += lfbwidth;
            }

            #endregion

            #region 画曲线
            top += lfbheight * 18;
            left = lbfrect.Left + lfbwidth;
            List<Point> yycy_pointlist = new List<Point>();//言语测验曲线图
            List<Point> czcy_pointlist = new List<Point>();//操作测验曲线图
            for (int i = 0; i < 5; i++)
            {
                if (!String.IsNullOrEmpty(listyycy[i]))
                {
                    Point pt = new Point(left + i * lfbwidth, top - (Convert.ToInt32(listyycy[i])-1) * lfbheight);
                    yycy_pointlist.Add(pt);
                }
            }
            for (int i = 0; i < 7; i++)
            {
                if (!String.IsNullOrEmpty(listczcy[i]))
                {
                    Point pt = new Point(left + lfbwidth * 5 + lfbwidth / 2 + i * lfbwidth, top - (Convert.ToInt32(listczcy[i]) - 1) * lfbheight);
                    czcy_pointlist.Add(pt);
                }
            }
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // 消锯齿（可选项）
            if (yycy_pointlist.Count > 1)
            {
                g.DrawLines(pen, yycy_pointlist.ToArray());
            }
            if (czcy_pointlist.Count > 1)
            {
                g.DrawLines(pen, czcy_pointlist.ToArray());
            }

            #endregion

            #region 合作等

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
            top += 60;

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
