﻿using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_yysc_printer : UserControl
    {
        private Rectangle _rectBody;

        private tb_childbase _baseobj = null;
        private cp_ddst_tab _ddstobj = null;
        private cp_cdi_tab _cdiobj = null;
        private cp_cdi1_tab _cdi1obj = null;
        private cp_zqyyfyjc_tab _obj = null;

        public cp_yysc_printer(tb_childbase baseobj, cp_ddst_tab ddstobj, cp_cdi_tab cdiobj, cp_cdi1_tab cdi1obj, cp_zqyyfyjc_tab zqobj)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _ddstobj = ddstobj;
            _cdiobj = cdiobj;
            _cdi1obj = cdi1obj;
            _obj = zqobj;
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

            Rectangle rect = new Rectangle(left, top, nameheadwidth, heigh);
            MiddleLeftPrintText("姓名", rect, g);
            left += nameheadwidth;

            rect = new Rectangle(left, top, namewidth, heigh);
            MiddleLeftPrintText(_baseobj.childname,rect,g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += namewidth;

            rect = new Rectangle(left, top, sexheadwidth, heigh);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, heigh);
            MiddleLeftPrintText(_baseobj.childgender, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += sexwidth;

            rect = new Rectangle(left, top, ageheadwidth, heigh);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.childbirthday, _ddstobj.csrq);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintText(agestr, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += ageheadwidth;
            top += heigh;

            #endregion


            #region 丹佛发育筛查（DDST）结果报告

            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, heigh);
            BoldMiddleLeftPrintText("丹佛发育筛查（DDST）结果报告", rect, g);

            top += heigh;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 195, heigh);
            MiddleLeftPrintText("个人-社会能区:"+ _ddstobj.ddst_grysh, rect, g);

            left += 195;
            rect = new Rectangle(left, top, 195, heigh);
            MiddleLeftPrintText("精细运动能区:" + _ddstobj.ddst_jxydnq, rect, g);

            left += 195;
            rect = new Rectangle(left, top, 175, heigh);
            MiddleLeftPrintText("语言能区:" + _ddstobj.ddst_yynq, rect, g);

            left += 175;
            rect = new Rectangle(left, top, 195, heigh);
            MiddleLeftPrintText("大运动能区:" + _ddstobj.ddst_dydnq, rect, g);

            
            top += heigh;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, heigh);
            MiddleLeftPrintText("结果:"+ _ddstobj.ddst_jg, rect, g);
            top += heigh;

            #endregion

            if (_cdiobj.chbd_df != "")
            {
                #region CDI短表（词汇及手势）测试结果

                left = _rectBody.Left;
                rect = new Rectangle(left, top, _rectBody.Width, heigh);
                BoldMiddleLeftPrintText("CDI短表（词汇及手势）测试结果", rect, g);

                top += heigh;
                left = _rectBody.Left;
                rect = new Rectangle(left, top, 140, heigh);
                MiddleLeftPrintText("1.语言理解  （", rect, g);
                left += 140;
                rect = new Rectangle(left, top, 30, heigh);
                MiddleLeftPrintText(_cdiobj.yylj_df, rect, g);

                left += 30;
                rect = new Rectangle(left, top, 30, heigh);
                MiddleLeftPrintText("）", rect, g);

                //left += 30;
                //rect = new Rectangle(left, top, 140, 30);
                //MiddleLeftPrintText("（相当于同月龄", rect, g);

                //left += 140;
                //rect = new Rectangle(left, top, 50, 30);
                //MiddleLeftPrintText(_cdiobj.yylj_yl, rect, g);

                //left += 50;
                //rect = new Rectangle(left, top, 30, 30);
                //MiddleLeftPrintText("）", rect, g);

                left += 30;
                rect = new Rectangle(left, top, 80, heigh);
                MiddleLeftPrintText("（相当于", rect, g);

                left += 80;
                rect = new Rectangle(left, top, 40, heigh);
                MiddleLeftPrintText(_cdiobj.yylj_p50, rect, g);

                left += 40;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintText("月P50水平）", rect, g);

                top += heigh;
                left = _rectBody.Left + 200;
                rect = new Rectangle(left, top, 80, heigh);
                MiddleLeftPrintText("（相当于", rect, g);

                left += 80;
                rect = new Rectangle(left, top, 40, heigh);
                MiddleLeftPrintText(_cdiobj.yylj_p75, rect, g);

                left += 40;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintText("月P75水平）", rect, g);

                top += heigh;
                left = _rectBody.Left;
                rect = new Rectangle(left, top, 140, heigh);
                MiddleLeftPrintText("2.词汇表达  （", rect, g);
                left += 140;
                rect = new Rectangle(left, top, 30, 30);
                MiddleLeftPrintText(_cdiobj.chbd_df, rect, g);

                left += 30;
                rect = new Rectangle(left, top, 30, heigh);
                MiddleLeftPrintText("）", rect, g);

                //left += 30;
                //rect = new Rectangle(left, top, 140, 30);
                //MiddleLeftPrintText("（相当于同月龄", rect, g);

                //left += 140;
                //rect = new Rectangle(left, top, 50, 30);
                //MiddleLeftPrintText(_cdiobj.chbd_yl, rect, g);

                //left += 50;
                //rect = new Rectangle(left, top, 30, 30);
                //MiddleLeftPrintText("）", rect, g);

                left += 30;
                rect = new Rectangle(left, top, 80, heigh);
                MiddleLeftPrintText("（相当于", rect, g);

                left += 80;
                rect = new Rectangle(left, top, 40, heigh);
                MiddleLeftPrintText(_cdiobj.chbd_p50, rect, g);

                left += 40;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintText("月P50水平）", rect, g);

                top += heigh;
                left = _rectBody.Left + 200;
                rect = new Rectangle(left, top, 80, heigh);
                MiddleLeftPrintText("（相当于", rect, g);

                left += 80;
                rect = new Rectangle(left, top, 40, heigh);
                MiddleLeftPrintText(_cdiobj.chbd_p75, rect, g);

                left += 40;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintText("月P75水平）", rect, g);


                top += heigh;
                left = _rectBody.Left;
                rect = new Rectangle(left, top, 100, heigh);
                MiddleLeftPrintText("病史提供者", rect, g);
                left += 100;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintTextAndLine(_cdiobj.bstgz, rect, g);

                left += 200;
                rect = new Rectangle(left, top, 100, heigh);
                MiddleLeftPrintText("儿童带养人", rect, g);
                left += 100;
                rect = new Rectangle(left, top, 100, heigh);
                MiddleLeftPrintTextAndLine(_cdiobj.etdyr, rect, g);
                top += heigh;

                #endregion
            }

            if (_cdi1obj.chbd_df != "")
            {
                #region CDI短表（词汇及句子）测试结果
                left = _rectBody.Left;
                rect = new Rectangle(left, top, _rectBody.Width, heigh);
                BoldMiddleLeftPrintText("CDI短表（词汇及句子）测试结果", rect, g);

                top += heigh;
                left = _rectBody.Left;
                rect = new Rectangle(left, top, 140, heigh);
                MiddleLeftPrintText("词汇表达  （", rect, g);
                left += 140;
                rect = new Rectangle(left, top, 30, heigh);
                MiddleLeftPrintText(_cdi1obj.chbd_df, rect, g);

                left += 30;
                rect = new Rectangle(left, top, 30, heigh);
                MiddleLeftPrintText("）", rect, g);

                //left += 30;
                //rect = new Rectangle(left, top, 140, 30);
                //MiddleLeftPrintText("（相当于同月龄", rect, g);

                //left += 140;
                //rect = new Rectangle(left, top, 50, 30);
                //MiddleLeftPrintText(_cdiobj.chbd_yl, rect, g);

                //left += 50;
                //rect = new Rectangle(left, top, 30, 30);
                //MiddleLeftPrintText("）", rect, g);

                left += 30;
                rect = new Rectangle(left, top, 80, heigh);
                MiddleLeftPrintText("（相当于", rect, g);

                left += 80;
                rect = new Rectangle(left, top, 40, heigh);
                MiddleLeftPrintText(_cdi1obj.chbd_p50, rect, g);

                left += 40;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintText("月P50水平）", rect, g);

                top += heigh;
                left = _rectBody.Left + 200;
                rect = new Rectangle(left, top, 80, heigh);
                MiddleLeftPrintText("（相当于", rect, g);

                left += 80;
                rect = new Rectangle(left, top, 40, heigh);
                MiddleLeftPrintText(_cdi1obj.chbd_p75, rect, g);

                left += 40;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintText("月P75水平）", rect, g);


                top += heigh;
                left = _rectBody.Left;
                rect = new Rectangle(left, top, 100, heigh);
                MiddleLeftPrintText("病史提供者", rect, g);
                left += 100;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintTextAndLine(_cdi1obj.bstgz, rect, g);

                left += 200;
                rect = new Rectangle(left, top, 100, heigh);
                MiddleLeftPrintText("儿童带养人", rect, g);
                left += 100;
                rect = new Rectangle(left, top, 100, heigh);
                MiddleLeftPrintTextAndLine(_cdi1obj.etdyr, rect, g);
                top += heigh;

                #endregion
            }

            #region 《早期语言发育进程》测试结果

            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, heigh);
            BoldMiddleLeftPrintText("《早期语言发育进程》测试结果", rect, g);

            top += heigh;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 220, heigh);
            MiddleLeftPrintText("1.A语言表达能力        （", rect, g);
            left += 220;
            rect = new Rectangle(left, top, 30, heigh);
            MiddleLeftPrintText(_obj.yybd_df, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText("）分", rect, g);

            left += 100;
            rect = new Rectangle(left, top, 140, heigh);
            MiddleLeftPrintText("（相当于同月龄", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 50, heigh);
            MiddleLeftPrintText(_obj.yybd_yl, rect, g);

            left += 50;
            rect = new Rectangle(left, top, 30, heigh);
            MiddleLeftPrintText("）", rect, g);

            top += heigh;
            left = _rectBody.Left + 350;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText(_obj.yybd_p50, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText("月P50水平）", rect, g);

            top += heigh;
            left = _rectBody.Left + 350;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText(_obj.yybd_p75, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText("月P75水平）", rect, g);

            top += heigh;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 220, heigh);
            MiddleLeftPrintText("2.B听觉及语言理解能力  （", rect, g);
            left += 220;
            rect = new Rectangle(left, top, 30, heigh);
            MiddleLeftPrintText(_obj.tjnl_df, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText("）分", rect, g);

            left += 100;
            rect = new Rectangle(left, top, 140, heigh);
            MiddleLeftPrintText("（相当于同月龄", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 50, heigh);
            MiddleLeftPrintText(_obj.tjnl_yl, rect, g);

            left += 50;
            rect = new Rectangle(left, top, 30, heigh);
            MiddleLeftPrintText("）", rect, g);

            top += heigh;
            left = _rectBody.Left + 350;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText(_obj.tjnl_p50, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText("月P50水平）", rect, g);

            top += heigh;
            left = _rectBody.Left + 350;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText(_obj.tjnl_p75, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText("月P75水平）", rect, g);

            top += heigh;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 220, heigh);
            MiddleLeftPrintText("3.C与视觉相关的表达能力（", rect, g);
            left += 220;
            rect = new Rectangle(left, top, 30, heigh);
            MiddleLeftPrintText(_obj.sjxg_df, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText("）分", rect, g);

            left += 100;
            rect = new Rectangle(left, top, 140, heigh);
            MiddleLeftPrintText("（相当于同月龄", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 50, heigh);
            MiddleLeftPrintText(_obj.sjxg_yl, rect, g);

            left += 50;
            rect = new Rectangle(left, top, 30, heigh);
            MiddleLeftPrintText("）", rect, g);

            top += heigh;
            left = _rectBody.Left + 350;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText(_obj.sjxg_p50, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText("月P50水平）", rect, g);

            top += heigh;
            left = _rectBody.Left + 350;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText(_obj.sjxg_p75, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText("月P75水平）", rect, g);

            top += heigh;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 220, heigh);
            MiddleLeftPrintText("4.全量表               （", rect, g);
            left += 220;
            rect = new Rectangle(left, top, 30, heigh);
            MiddleLeftPrintText(_obj.qlb_df, rect, g);

            left += 30;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText("）分", rect, g);

            left += 100;
            rect = new Rectangle(left, top, 140, heigh);
            MiddleLeftPrintText("（相当于同月龄", rect, g);

            left += 140;
            rect = new Rectangle(left, top, 50, heigh);
            MiddleLeftPrintText(_obj.qlb_yl, rect, g);

            left += 50;
            rect = new Rectangle(left, top, 30, heigh);
            MiddleLeftPrintText("）", rect, g);

            top += heigh;
            left = _rectBody.Left + 350;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText(_obj.qlb_p50, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText("月P50水平）", rect, g);

            top += heigh;
            left = _rectBody.Left + 350;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText("（相当于", rect, g);

            left += 80;
            rect = new Rectangle(left, top, 40, heigh);
            MiddleLeftPrintText(_obj.qlb_p75, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText("月P75水平）", rect, g);

            top += heigh;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 60, heigh);
            MiddleLeftPrintText("合作：", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText(_obj.hz, rect, g);

            left += 100;
            rect = new Rectangle(left, top, 70, heigh);
            MiddleLeftPrintText("注意力：", rect, g);

            left += 70;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText(_obj.zyl, rect, g);

            top += heigh;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 60, heigh);
            MiddleLeftPrintText("反应：", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText(_obj.fy, rect, g);

            left += 100;
            rect = new Rectangle(left, top, 70, heigh);
            MiddleLeftPrintText("  情绪：", rect, g);

            left += 70;
            rect = new Rectangle(left, top, 80, heigh);
            MiddleLeftPrintText(_obj.qx, rect, g);
            top += heigh;

            #endregion

            #region 签名
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText(_ddstobj.cszqm, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, heigh);
            MiddleLeftPrintText("测试者签名", rect, g);

            top += heigh;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, heigh);
            MiddleLeftPrintText(_ddstobj.csrq, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, heigh);
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