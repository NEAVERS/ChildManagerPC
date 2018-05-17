using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_yysc1_printer : UserControl
    {
        private Rectangle _rectBody;

        private tb_childbase _baseobj = null;
        private cp_cdi_tab _cdiobj = null;
        private cp_ddst_tab _ddstobj = null;
        private cp_cdi1_tab _cdi1obj = null;
        private cp_ppvt_tab _ppvtobj = null;

        public cp_yysc1_printer(tb_childbase baseobj, cp_cdi_tab cdiobj, cp_ddst_tab ddstobj, cp_cdi1_tab cdi1obj, cp_ppvt_tab ppvtobj)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _cdiobj = cdiobj;
            _ddstobj = ddstobj;
            _cdi1obj = cdi1obj;
            _ppvtobj = ppvtobj;
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
            int heigh = 32;

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
            left = _rectBody.Left + 40;

            Rectangle rect = new Rectangle(left, top, nameheadwidth, 30);
            MiddleLeftPrintText("姓名", rect, g);
            left += nameheadwidth;

            rect = new Rectangle(left, top, namewidth, 30);
            MiddleLeftPrintText(_baseobj.childname,rect,g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += namewidth+50;

            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintText(_baseobj.childgender, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += sexwidth+50;

            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.childbirthday, _ddstobj.csrq);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintText(agestr, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += ageheadwidth;
            top += 40;

            #endregion

            #region 丹佛发育筛查（DDST）结果报告

            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, heigh);
            BoldMiddleLeftPrintText("丹佛发育筛查（DDST）结果报告", rect, g);

            top += heigh;
            left = _rectBody.Left+30;
            rect = new Rectangle(left, top, 195, heigh);
            MiddleLeftPrintText("个人-社会能区:"+ _ddstobj.ddst_grysh, rect, g);

            left += 175;
            rect = new Rectangle(left, top, 195, heigh);
            MiddleLeftPrintText("精细运动能区:" + _ddstobj.ddst_jxydnq, rect, g);

            left += 175;
            rect = new Rectangle(left, top, 175, heigh);
            MiddleLeftPrintText("语言能区:" + _ddstobj.ddst_yynq, rect, g);

            left += 175;
            rect = new Rectangle(left, top, 195, heigh);
            MiddleLeftPrintText("大运动能区:" + _ddstobj.ddst_dydnq, rect, g);

            
            top += heigh;
            left = _rectBody.Left+30;
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
                left = _rectBody.Left+30;
                rect = new Rectangle(left, top, 140, heigh);
                MiddleLeftPrintText("1.语言理解  （", rect, g);
                left += 140;
                rect = new Rectangle(left, top, 30, heigh);
                MiddleLeftPrintText(_cdiobj.yylj_df, rect, g);

                left += heigh;
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
                rect = new Rectangle(left, top, 80, 30);
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

                left += heigh;
                rect = new Rectangle(left, top, 120, heigh);
                MiddleLeftPrintText("月P75水平）", rect, g);

                top += heigh;
                left = _rectBody.Left+30;
                rect = new Rectangle(left, top, 140, heigh);
                MiddleLeftPrintText("2.词汇表达  （", rect, g);
                left += 140;
                rect = new Rectangle(left, top, 30, heigh);
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
                left = _rectBody.Left+30;
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
                left = _rectBody.Left+30;
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
                left = _rectBody.Left+30;
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

            #region 皮勃迪图片词汇测试（PPVT）

            left = _rectBody.Left;
            rect = new Rectangle(left, top, _rectBody.Width, heigh);
            BoldMiddleLeftPrintText("皮勃迪图片词汇测试（PPVT）：",rect, g);//

            //top += heigh;
            //left = _rectbody.left+30;
            //rect = new rectangle(left, top, 200, heigh);
            //middleleftprinttext("粗分 (" + _ppvtobj.ppvt_dd, rect, g);

            //top += heigh;
            //rect = new rectangle(left, top, 200, heigh);
            //middleleftprinttext("iq   (" + _ppvtobj.ppvt_cw, rect, g);
            //top += heigh;
            //left = _rectbody.left + 30;
            //rect = new rectangle(left, top, 200, heigh);
            //middleleftprinttext("p    (" + _ppvtobj.ppvt_zs, rect, g);

            //top += heigh;
            //rect = new rectangle(left, top, 200, heigh);
            //middleleftprinttext("智龄 (" + _ppvtobj.ppvt_zl, rect, g);
            //left += 200;
            //rect = new rectangle(left, top, 200, heigh);
            //middleleftprinttext("）岁（" + _ppvtobj.ppvt_df, rect, g);

            //left += 200;
            //rect = new rectangle(left, top, 200, 30);
            //middleleftprinttext("）月", rect, g);

            top += 30;
            left = _rectBody.Left+40;
            rect = new Rectangle(left, top, 60, 30);
            MiddleLeftPrintText("粗分（", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText(_ppvtobj.ppvt_dd, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("）分", rect, g);

            top += 40;
            left = _rectBody.Left + 40;
            rect = new Rectangle(left, top, 60, 30);
            MiddleLeftPrintText("IQ  （", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText(_ppvtobj.ppvt_cw, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("）分", rect, g);

            top += 40;
            left = _rectBody.Left + 40;
            rect = new Rectangle(left, top, 60, 30);
            MiddleLeftPrintText("P   （", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText(_ppvtobj.ppvt_zs, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("）", rect, g);

            top += 40;
            left = _rectBody.Left + 40;
            rect = new Rectangle(left, top, 60, 30);
            MiddleLeftPrintText("智龄（", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText(_ppvtobj.ppvt_zl, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 60, 30);
            MiddleLeftPrintText("）岁（", rect, g);

            left += 60;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText(_ppvtobj.ppvt_df, rect, g);

            left += 40;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("）月", rect, g);



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
