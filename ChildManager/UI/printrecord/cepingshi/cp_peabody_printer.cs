using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;
using YCF.BLL;
using System;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_peabody_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_PEABODY_TAB _obj = null;
        private tb_childbasebll jibenbll = new tb_childbasebll();
        private TB_CHILDBASE _jibenobj = null;

        public cp_peabody_printer(TB_CHILDBASE baseobj, CP_PEABODY_TAB obj)
        {
            InitializeComponent();
            _baseobj = baseobj;

            TB_CHILDBASE jibenobj = jibenbll.Get(obj.CHILD_ID);
            _jibenobj = jibenobj;
            _obj = obj;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "Peabody运动测试报告";

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
            Pen pen = new Pen(Color.Black);
            #region 打印表头
            int top = _rectBody.Top - 10;
            int left = _rectBody.Left;
            int height = 40;
            Rectangle rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            string projectName = CommonHelper.GetHospitalName();
            MiddleCenterPrintText(""+projectName+"儿童行为发育与心理健康中心", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("神经心理测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            g.DrawLine(pen,left,top+5,left+_rectBody.Width,top+5);
            top += 5+height;

            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("Peabody运动发育量表测试报告", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            #endregion

            #region 打印病人信息

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            StringFormat lf = new StringFormat();
            lf.Alignment = StringAlignment.Near;
            lf.LineAlignment = StringAlignment.Center;
            StringFormat lf1 = new StringFormat();
            lf1.Alignment = StringAlignment.Near;
            lf1.LineAlignment = StringAlignment.Near;
            //Pen pen = new Pen(Color.Black);
            Pen bpen = new Pen(Color.Black, 3);

            Brush brush = new SolidBrush(Color.Black);

            int nameheadwidth = 90;
            int namewidth = 90;
            int sexheadwidth = 90;
            int sexwidth = 90;
            int yunzhouheadwidth = 90;
            int yunzhouwidth = 90;

            int birthheadwidth = 90;
            int birthwidth = 90;
            int shengliheadwidth = 90;
            int shengliwidth = 90;
            int jiaozhengheadwidth = 90;
            int jiaozhengwidth = 90;

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
            rect = new Rectangle(left, top, yunzhouheadwidth, 30);
            MiddleLeftPrintText("孕周", rect, g);
            left += yunzhouwidth;

            string yunzhoustr = "";
            if (string.IsNullOrEmpty(_jibenobj.SFZY))
            {

                if (_jibenobj.SFZY.Contains("足月"))
                {
                    yunzhoustr = "";
                }
                else
                {
                    yunzhoustr = (_jibenobj.CS_WEEK + "+" + _jibenobj.CS_DAY).Trim('+');
                }
            }
            else
            {
                yunzhoustr = "";
            }
            rect = new Rectangle(left, top, yunzhouwidth, 30);
            MiddleLeftPrintTextAndLine(yunzhoustr, rect, g);
            left = _rectBody.Left;

            top += 30;

            rect = new Rectangle(left, top, birthheadwidth, 30);
            MiddleLeftPrintText("出生日期", rect, g);
            left += birthheadwidth;
            rect = new Rectangle(left, top, birthwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDBIRTHDAY, rect, g);
            left += birthwidth;
            left += 50;

            rect = new Rectangle(left, top, shengliheadwidth, 30);
            MiddleLeftPrintText("生理年龄", rect, g);
            left += shengliheadwidth;
            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _obj.CSRQ);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, shengliwidth, 30);
            MiddleLeftPrintTextAndLine(agestr, rect, g);
            left += shengliwidth;
            left += 50;

            rect = new Rectangle(left, top, jiaozhengheadwidth, 30);
            MiddleLeftPrintText("矫正年龄", rect, g);
            left += jiaozhengheadwidth;
            rect = new Rectangle(left, top, jiaozhengwidth, 30);
            string jiaozhengnianlingstr = "";

            //var nowtime = Convert.ToDateTime(DateTime.Now.ToString("yyyy - MM - dd"));



            ///*计算出生日期到当前日期总月数*/
            //int Months = nowtime.Month - birth.Month + 12 * (nowtime.Year - birth.Year);
            ///*出生日期加总月数后，如果大于当前日期则减一个月*/
            //int totalMonth = (birth.AddMonths(Months) > nowtime) ? Months - 1 : Months;
            ///*计算整年*/
            //fullYear = totalMonth / 12;
            ///*计算整月*/
            //fullMonth = totalMonth % 12;
            ///*计算天数*/
            //DateTime changeDate = birth.AddMonths(totalMonth);
            //days = (nowtime - changeDate).TotalDays;

            if (!string.IsNullOrEmpty(_baseobj.CS_WEEK) && !string.IsNullOrEmpty(_baseobj.CS_DAY))
            {
                var d1 = Convert.ToDateTime(_baseobj.CHILDBIRTHDAY).AddDays(280 - Convert.ToInt16(_baseobj.CS_WEEK) * 7 - Convert.ToInt16(_baseobj.CS_DAY));
                int[] age1 = CommonHelper.getAgeBytime(Convert.ToString(d1), Convert.ToString(DateTime.Now));
                string age1str = (age1[0] > 0 ? age1[0].ToString() + "岁" : "") + (age1[1] > 0 ? age1[1].ToString() + "月" : "") + (age1[2] > 0 ? age1[2].ToString() + "天" : "");

                if (_jibenobj.SFZY.Contains("早产"))
                {
                    jiaozhengnianlingstr = age1str;
                }
                else
                {
                    jiaozhengnianlingstr = agestr;
                }
            }
            if (string.IsNullOrEmpty(_baseobj.CS_WEEK) && !string.IsNullOrEmpty(_baseobj.CS_DAY))
            {
                var d1 = Convert.ToDateTime(_baseobj.CHILDBIRTHDAY).AddDays(280  - Convert.ToInt16(_baseobj.CS_DAY));
                int[] age1 = CommonHelper.getAgeBytime(Convert.ToString(d1), Convert.ToString(DateTime.Now));
                string age1str = (age1[0] > 0 ? age1[0].ToString() + "岁" : "") + (age1[1] > 0 ? age1[1].ToString() + "月" : "") + (age1[2] > 0 ? age1[2].ToString() + "天" : "");

                if (_jibenobj.SFZY.Contains("早产"))
                {
                    jiaozhengnianlingstr = age1str;
                }
                else
                {
                    jiaozhengnianlingstr = agestr;
                }
            }
            if (!string.IsNullOrEmpty(_baseobj.CS_WEEK) && string.IsNullOrEmpty(_baseobj.CS_DAY))
            {
                var d1 = Convert.ToDateTime(_baseobj.CHILDBIRTHDAY).AddDays(280 - Convert.ToInt16(_baseobj.CS_WEEK) * 7);
                int[] age1 = CommonHelper.getAgeBytime(Convert.ToString(d1), Convert.ToString(DateTime.Now));
                string age1str = (age1[0] > 0 ? age1[0].ToString() + "岁" : "") + (age1[1] > 0 ? age1[1].ToString() + "月" : "") + (age1[2] > 0 ? age1[2].ToString() + "天" : "");

                if (_jibenobj.SFZY.Contains("早产"))
                {
                    jiaozhengnianlingstr = age1str;
                }
                else
                {
                    jiaozhengnianlingstr = agestr;
                }
            }

            MiddleLeftPrintTextAndLine(jiaozhengnianlingstr, rect, g);
            left += jiaozhengwidth;




            top += 70;
            #endregion

            #region  主体内容

            left = _rectBody.Left ;
            int Width = 660;
            int width = 90;
            int hanggao = 30;
            int hanggao1 = 50;
            Font titlefont = new Font("宋体", 12f,FontStyle.Bold);
            Font font = new Font("宋体", 11f);
            Font font1 = new Font("宋体", 16f, FontStyle.Bold);
            rect = new Rectangle(left, top, 120, hanggao);
            g.DrawRectangle(pen, rect);
            g.DrawString("PDMS-2", font, brush, rect, lf);


            rect = new Rectangle(left+120, top, width, hanggao);
            g.DrawRectangle(pen, rect);
            g.DrawString("原始分", font, brush, rect, sf);

            rect = new Rectangle(left + 120+width, top, width, hanggao);
            g.DrawRectangle(pen, rect);
            g.DrawString("相当年龄", font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width*2, top, width, hanggao);
            g.DrawRectangle(pen, rect);
            g.DrawString("百分位", font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 3, top, width*3, hanggao);
            g.DrawRectangle(pen, rect);
            g.DrawString("标准分", font, brush, rect, sf);

            top += hanggao;


            rect = new Rectangle(left, top, 120, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString("反射", font, brush, rect, lf);

            rect = new Rectangle(left+120,top,width,hanggao1);
            g.DrawRectangle(pen,rect);
            g.DrawString(_obj.FANSHE_YSF, font,brush,rect,sf);

            rect = new Rectangle(left + 120+width, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.FANSHE_XDNL, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width*2, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.FANSHE_BFW, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 3, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.FANSHE_BZF1, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.FANSHE_BZF2, font, brush, rect, sf);

            top += hanggao1;

            rect = new Rectangle(left, top, 120, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString("姿势", font, brush, rect, lf);

            rect = new Rectangle(left + 120, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZISHI_YSF, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZISHI_XDNL, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 2, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZISHI_BFW, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 3, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZISHI_BZF1, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZISHI_BZF2, font, brush, rect, sf);

            top += hanggao1;

            rect = new Rectangle(left, top, 120, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString("移动", font, brush, rect, lf);

            rect = new Rectangle(left + 120, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.YIDONG_YSF, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.YIDONG_XDNL, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 2, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.YIDONG_BFW, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 3, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.YIDONG_BZF1, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.YIDONG_BZF2, font, brush, rect, sf);

            top += hanggao1;

            rect = new Rectangle(left, top, 120, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString("实物操作", font, brush, rect, lf);

            rect = new Rectangle(left + 120, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.CAOZUO_YSF, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.CAOZUO_XDNL, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 2, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.CAOZUO_BFW, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 3, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.CAOZUO_BZF1, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.CAOZUO_BZF2, font, brush, rect, sf);

            top += hanggao1;

            rect = new Rectangle(left, top, 120, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString("抓握", font, brush, rect, lf);

            rect = new Rectangle(left + 120, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZHUAWO_YSF, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZHUAWO_XDNL, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 2, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZHUAWO_BFW, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 4, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZHUAWO_BZF1, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.ZHUAWO_BZF2, font, brush, rect, sf);

            top += hanggao1;

            rect = new Rectangle(left, top, 120, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString("视觉-运动整合", font, brush, rect, lf);

            rect = new Rectangle(left + 120, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.SHIJUE_YSF, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.SHIJUE_XDNL, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 2, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.SHIJUE_BFW, font, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 4, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.SHIJUE_BZF1, font, brush, rect, sf);
            g.DrawLine(pen,left+120+width*3,top+hanggao1,left+120+width*4,top+hanggao1);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawRectangle(pen, rect);
            g.DrawString(_obj.SHIJUE_BZF2, font, brush, rect, sf);

            top += hanggao1*2;


            #endregion

            #region 签名

            rect = new Rectangle(left+120+width*2-10,top,width+10,hanggao1);
            g.DrawString("标准分和",font1,brush,rect,lf);

            rect = new Rectangle(left+120+width*3,top,width,hanggao1);
            g.DrawString(_obj.BZF_GMQ,font,brush,rect,sf);
            g.DrawLine(pen,left+120+width*3+5,top+33,left+120+width*4-5,top+33);

            rect = new Rectangle(left + 120 + width * 4, top, width, hanggao1);
            g.DrawString(_obj.BZF_FMQ, font, brush, rect, sf);
            g.DrawLine(pen, left + 120 + width * 4 + 5, top + 33, left + 120 + width * 5 - 5, top + 33);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawString(_obj.BZF_TMQ, font, brush, rect, sf);
            g.DrawLine(pen, left + 120 + width * 5 + 5, top + 33, left + 120 + width * 6 - 5, top + 33);

            top += hanggao1;

            rect = new Rectangle(left + 120 + width * 3, top, width, hanggao1);
            g.DrawString("GMQ", font1, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 4, top, width, hanggao1);
            g.DrawString("FMQ", font1, brush, rect, sf);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawString("TMQ", font1, brush, rect, sf);

            top += hanggao1;

            rect = new Rectangle(left + 120 + width * 2-10, top, width+10, hanggao1);
            g.DrawString("发育商", font1, brush, rect, lf);

            rect = new Rectangle(left + 120 + width * 3, top, width, hanggao1);
            g.DrawString(_obj.FYS_GMQ, font, brush, rect, sf);
            g.DrawLine(pen, left + 120 + width * 3 + 5, top + 33, left + 120 + width * 4 - 5, top + 33);

            rect = new Rectangle(left + 120 + width * 4, top, width, hanggao1);
            g.DrawString(_obj.FYS_FMQ, font, brush, rect, sf);
            g.DrawLine(pen, left + 120 + width * 4 + 5, top + 33, left + 120 + width * 5 - 5, top + 33);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawString(_obj.FYS_TMQ, font, brush, rect, sf);
            g.DrawLine(pen, left + 120 + width * 5 + 5, top + 33, left + 120 + width * 6 - 5, top + 33);

            top += hanggao1;

            rect = new Rectangle(left + 120 + width * 2-10, top, width+10, hanggao1);
            g.DrawString("百分位", font1, brush, rect, lf);

            rect = new Rectangle(left + 120 + width * 3, top, width, hanggao1);
            g.DrawString(_obj.BFW_GMQ, font, brush, rect, sf);
            g.DrawLine(pen, left + 120 + width * 3 + 5, top + 33, left + 120 + width * 4 - 5, top + 33);

            rect = new Rectangle(left + 120 + width * 4, top, width, hanggao1);
            g.DrawString(_obj.BFW_FMQ, font, brush, rect, sf);
            g.DrawLine(pen, left + 120 + width * 4 + 5, top + 33, left + 120 + width * 5 - 5, top + 33);

            rect = new Rectangle(left + 120 + width * 5, top, width, hanggao1);
            g.DrawString(_obj.BFW_TMQ, font, brush, rect, sf);
            g.DrawLine(pen, left + 120 + width * 5 + 5, top + 33, left + 120 + width * 6 - 5, top + 33);

            top += hanggao1;


            rect = new Rectangle(left + 120 + width * 3, top, width*3, hanggao1);
            g.DrawString("报告人："+_obj.OPERATE_NAME, font, brush, rect, lf);

            top += hanggao1;

            rect = new Rectangle(left + 120 + width * 3, top, width*3, hanggao1);
            g.DrawString("测试日期："+_obj.CSRQ, font, brush, rect, lf);

            top += hanggao1;

            rect = new Rectangle(left, top, Width, hanggao1);
            g.DrawString("说明：本结论为一次测试结果，具体诊断请结合临床", font, brush, rect, lf);







            //left = _rectBody.Right - 120;
            //rect = new Rectangle(left, top, 120, 30);
            //MiddleLeftPrintText(_obj.CSZQM, rect, g);
            //g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            //left -= 100;
            //rect = new Rectangle(left, top, 100, 30);
            //MiddleLeftPrintText("测试者签名", rect, g);

            //top += 25;
            //left = _rectBody.Right - 120;
            //rect = new Rectangle(left, top, 120, 30);
            //MiddleLeftPrintText(_obj.CSRQ, rect, g);
            //g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            //left -= 100;
            //rect = new Rectangle(left, top, 100, 30);
            //MiddleLeftPrintText("  测试日期", rect, g);
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
            StringFormat lf = new StringFormat();
            lf.Alignment = StringAlignment.Near;
            lf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, lf);
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
            g.DrawLine(pen, rect.Left, rect.Top + 23, rect.Right, rect.Top + 23);
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
