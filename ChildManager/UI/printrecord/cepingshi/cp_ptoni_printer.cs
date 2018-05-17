using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;
using YCF.BLL;
using System;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_ptoni_printer : UserControl
    {
        private Rectangle _rectBody;

        private TB_CHILDBASE _baseobj = null;
        private CP_PTONI_TAB _obj = null;
        private tb_childbasebll jibenbll = new tb_childbasebll();
        private TB_CHILDBASE _jibenobj = null;

        public cp_ptoni_printer(TB_CHILDBASE baseobj, CP_PTONI_TAB obj)
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
            printDoc.DocumentName = "ptoni运动测试报告";

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
            MiddleCenterPrintText(projectName, new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("重庆市儿童行为发育与心理健康中心", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("非言语智力测试结果", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            g.DrawLine(pen,left,top+5,left+_rectBody.Width,top+5);
            top += 5+height;

            //rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            //MiddleCenterPrintText("ptoni运动发育量表测试报告", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            //top += height;
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

            int nameheadwidth = 50;
            int namewidth = 90;
            int sexheadwidth = 50;
            int sexwidth = 90;
            int nianjiheadwidth = 50;
            int nianjiwidth = 90;

            int birthheadwidth = 90;
            int birthwidth = 90;
            int ageheadwidth = 50;
            int agewidth = 90;
            

            Rectangle rect = new Rectangle(left, top, nameheadwidth, 30);
            MiddleLeftPrintText("姓名", rect, g);
            left += nameheadwidth+40;

            rect = new Rectangle(left, top, namewidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDNAME, rect, g);
            left += namewidth;
			left += 50;
            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth+40;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDGENDER, rect, g);
            left += sexwidth;
			left += 50;
            rect = new Rectangle(left, top, nianjiheadwidth, 30);
            MiddleLeftPrintText("年级", rect, g);
            left += nianjiheadwidth+40;

            //string yunzhoustr = "";
            //if (_jibenobj.sfzy.Contains("足月"))
            //{
            //    yunzhoustr = "";
            //}
            //else
            //{
            //    yunzhoustr = (_jibenobj.cs_week + "+" + _jibenobj.cs_day).Trim('+');
            //}

            rect = new Rectangle(left, top, nianjiwidth, 30);
            MiddleLeftPrintTextAndLine(_obj.NIANJI, rect, g);
            left = _rectBody.Left;

            top += 30;

            rect = new Rectangle(left, top, birthheadwidth, 30);
            MiddleLeftPrintText("出生日期", rect, g);
            left += birthheadwidth;
            rect = new Rectangle(left, top, birthwidth, 30);
            MiddleLeftPrintTextAndLine(_baseobj.CHILDBIRTHDAY, rect, g);
            left += birthwidth;
            left += 50;

            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth+40;
            int[] age = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _obj.CSRQ);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintTextAndLine(agestr, rect, g);
            left += agewidth;
            left += 50;

            //rect = new Rectangle(left, top, jiaozhengheadwidth, 30);
            //MiddleLeftPrintText("矫正年龄", rect, g);
            //left += jiaozhengheadwidth;
            //rect = new Rectangle(left, top, jiaozhengwidth, 30);
            //string jiaozhengnianlingstr = "";

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


           //var d1= Convert.ToDateTime(_baseobj.CHILDBIRTHDAY).AddDays(280-Convert.ToInt16(_baseobj.cs_week) * 7 - Convert.ToInt16(_baseobj.cs_day));
           // int[] age1 = CommonHelper.getAgeBytime(Convert.ToString( d1), Convert.ToString(DateTime.Now));
           // string age1str = (age1[0] > 0 ? age1[0].ToString() + "岁" : "") + (age1[1] > 0 ? age1[1].ToString() + "月" : "") + (age1[2] > 0 ? age1[2].ToString() + "天" : "");


           // if (_jibenobj.sfzy.Contains("早产"))
           // {
        
           //     jiaozhengnianlingstr = age1str;

           // }
           // else
           // {
           //     jiaozhengnianlingstr =agestr;
           // }
           // MiddleLeftPrintTextAndLine(jiaozhengnianlingstr, rect, g);
           // left += jiaozhengwidth;




            top += 70;
            #endregion

            #region  主体内容

            left = _rectBody.Left ;
            int Width = 660;
            int width = 220;
            int hanggao = 30;
            
            Font titlefont = new Font("宋体", 12f,FontStyle.Bold);
            Font font = new Font("宋体", 11f);
            Font font1 = new Font("宋体", 11f, FontStyle.Bold);
            rect = new Rectangle(left, top, width, hanggao);
            g.DrawString("非言语智力基础测验", font1, brush, rect, lf);
            top += hanggao;

            rect = new Rectangle(left, top, width, hanggao);
            g.DrawString("PTONI评估报告", font1, brush, rect, lf);
            top += 40;
            left += 60;

            rect = new Rectangle(left, top, width, hanggao);
            g.DrawString("部分一:基本信息", font1, brush, rect, lf);
            top += 40 ;

            //部分一
            rect = new Rectangle(left, top, 120, hanggao);
            g.DrawString("学校/幼儿园:", font, brush, rect, lf);
            rect = new Rectangle(left+120, top, width, hanggao);
            g.DrawString(_obj.SCHOOL, font, brush, rect, sf);
            g.DrawLine(pen, left+120, top+22, left +120+ width, top + 22);
            top += hanggao;

            rect = new Rectangle(left , top, 120, hanggao);
            g.DrawString("评估者职位:", font, brush, rect, lf);
            rect = new Rectangle(left + 120, top, width, hanggao);
            g.DrawString(_obj.ZHIWEI, font, brush, rect, sf);
            g.DrawLine(pen, left + 120, top + 22, left + 120 + width, top + 22);
            top += hanggao;

            rect = new Rectangle(left , top, 120, hanggao);
            g.DrawString("测试原因:", font, brush, rect, lf);
            rect = new Rectangle(left + 120, top, width, hanggao);
            g.DrawString(_obj.CS_YUANYIN, font, brush, rect, sf);
            g.DrawLine(pen, left + 120, top + 22, left + 120 + width, top + 22);
            top += hanggao;


            rect = new Rectangle(left , top, 120, hanggao);
            g.DrawString("测试语言:", font, brush, rect, lf);
            rect = new Rectangle(left + 120, top, width, hanggao);
            g.DrawString(_obj.CS_YUYAN, font, brush, rect, sf);
            g.DrawLine(pen, left + 120, top + 22, left + 120 + width, top + 22);
            top += hanggao;




            //部分二
            rect = new Rectangle(left, top, width, hanggao);
            g.DrawString("部分二:计分", font1, brush, rect, lf);
            top += 40 ;

            rect = new Rectangle(left , top, 90, hanggao);
            g.DrawString("原始分" , font, brush, rect, sf);
            rect = new Rectangle(left, top+15, 90, hanggao);
            g.DrawString(_obj.YUANSHIFEN, font, brush, rect, sf);
            g.DrawLine(pen, left , top + hanggao + 15, left +90, top + hanggao + 15);

            rect = new Rectangle(left+100, top, 90, hanggao);
            g.DrawString("非言语指数" , font, brush, rect, sf);
            rect = new Rectangle(left+100, top + 15, 90, hanggao);
            g.DrawString(_obj.ZHISHU, font, brush, rect, sf);
            g.DrawLine(pen, left+100, top + hanggao + 15, left + 190, top + hanggao + 15);

            rect = new Rectangle(left + 200, top, 90, hanggao);
            g.DrawString("SEM", font, brush, rect, sf);
            rect = new Rectangle(left + 200, top + 15, 90, hanggao);
            g.DrawString(_obj.SEM, font, brush, rect, sf);
            g.DrawLine(pen, left+200, top + hanggao + 15, left + 290, top + hanggao + 15);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("百分等级", font, brush, rect, sf);
            rect = new Rectangle(left + 300, top + 15, 90, hanggao);
            g.DrawString(_obj.DENGJI , font, brush, rect, sf);
            g.DrawLine(pen, left + 300, top + hanggao + 15, left + 390, top + hanggao + 15);

            rect = new Rectangle(left + 400, top, 90, hanggao);
            g.DrawString("描述项", font, brush, rect, sf);
            rect = new Rectangle(left + 400, top + 15, 90, hanggao);
            g.DrawString(_obj.MIAOSHU , font, brush, rect, sf);
            g.DrawLine(pen, left + 400, top + hanggao + 15, left + 490, top + hanggao + 15);

            rect = new Rectangle(left + 500, top, 90, hanggao);
            g.DrawString("发育年龄", font, brush, rect, sf);
            rect = new Rectangle(left + 500, top + 15, 90, hanggao);
            g.DrawString(_obj.FAYU, font, brush, rect, sf);
            g.DrawLine(pen, left + 500, top + hanggao + 15, left + 590, top + hanggao + 15);
            top += hanggao*2;

            //部分三
            rect = new Rectangle(left, top, Width, hanggao);
            g.DrawString("部分三:非言语指数与百分等级的等级分类", font1, brush, rect, lf);
            top += 40;

            rect = new Rectangle(left , top, 90, hanggao);
            g.DrawString("非言语指数", font, brush, rect, lf);

            rect = new Rectangle(left+150, top, 90, hanggao);
            g.DrawString("百分等级", font, brush, rect, lf);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("描述项", font, brush, rect, lf);

            rect = new Rectangle(left + 450, top, 90, hanggao);
            g.DrawString("人口百分比", font, brush, rect, lf);

            top += hanggao;

            rect = new Rectangle(left, top, 90, hanggao);
            g.DrawString(">130", font, brush, rect, lf);

            rect = new Rectangle(left + 150, top, 90, hanggao);
            g.DrawString(">98", font, brush, rect, lf);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("超高水平", font, brush, rect, lf);

            rect = new Rectangle(left + 450, top, 90, hanggao);
            g.DrawString("2.34", font, brush, rect, lf);

            top += hanggao;

            rect = new Rectangle(left, top, 90, hanggao);
            g.DrawString("121-130", font, brush, rect, lf);

            rect = new Rectangle(left + 150, top, 90, hanggao);
            g.DrawString("93-98", font, brush, rect, lf);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("较高水平", font, brush, rect, lf);

            rect = new Rectangle(left + 450, top, 90, hanggao);
            g.DrawString("6.87", font, brush, rect, lf);

            top += hanggao;

            rect = new Rectangle(left, top, 90, hanggao);
            g.DrawString("111-120", font, brush, rect, lf);

            rect = new Rectangle(left + 150, top, 90, hanggao);
            g.DrawString("76-91", font, brush, rect, lf);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("均值以上", font, brush, rect, lf);

            rect = new Rectangle(left + 450, top, 90, hanggao);
            g.DrawString("16.12", font, brush, rect, lf);

            top += hanggao;

            rect = new Rectangle(left, top, 90, hanggao);
            g.DrawString("90-110", font, brush, rect, lf);

            rect = new Rectangle(left + 150, top, 90, hanggao);
            g.DrawString("25-75", font, brush, rect, lf);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("均值", font, brush, rect, lf);

            rect = new Rectangle(left + 450, top, 90, hanggao);
            g.DrawString("49.51", font, brush, rect, lf);

            top += hanggao;

            rect = new Rectangle(left, top, 90, hanggao);
            g.DrawString("80-89", font, brush, rect, lf);

            rect = new Rectangle(left + 150, top, 90, hanggao);
            g.DrawString("9-24", font, brush, rect, lf);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("低于均值", font, brush, rect, lf);

            rect = new Rectangle(left + 450, top, 90, hanggao);
            g.DrawString("16.12", font, brush, rect, lf);

            top += hanggao;

            rect = new Rectangle(left, top, 90, hanggao);
            g.DrawString("70-79", font, brush, rect, lf);

            rect = new Rectangle(left + 150, top, 90, hanggao);
            g.DrawString("2-8", font, brush, rect, lf);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("低水平", font, brush, rect, lf);

            rect = new Rectangle(left + 450, top, 90, hanggao);
            g.DrawString("6.87", font, brush, rect, lf);

            top += hanggao;

            rect = new Rectangle(left, top, 90, hanggao);
            g.DrawString("<70", font, brush, rect, lf);

            rect = new Rectangle(left + 150, top, 90, hanggao);
            g.DrawString("<2", font, brush, rect, lf);

            rect = new Rectangle(left + 300, top, 90, hanggao);
            g.DrawString("非常低", font, brush, rect, lf);

            rect = new Rectangle(left + 450, top, 90, hanggao);
            g.DrawString("2.34", font, brush, rect, lf);

            top += hanggao*3;





            left = _rectBody.Left + 70;
            rect = new Rectangle(left, top, 80, 30);
            MiddleLeftPrintText("送诊医生", rect, g);
            left += 80;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintText(_obj.SZYS, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);



            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.CSZQM, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 22, rect.Right, rect.Top + 22);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("测试者签名", rect, g);

            top += 25;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.CSRQ , rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 22, rect.Right, rect.Top + 22);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("  测试日期", rect, g);

            top += hanggao;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, Width, hanggao);
            g.DrawString("说明：本结论为一次测试结果，具体诊断请结合临床", font, brush, rect, lf);
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
