using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_zylcy_printer : UserControl
    {
        private Rectangle _rectBody;

        private tb_childbase _baseobj = null;
        private cp_zylcy_tab _obj = null;

        public cp_zylcy_printer(tb_childbase baseobj, cp_zylcy_tab obj)
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
            MiddleLeftPrintText(_baseobj.childname,rect,g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += namewidth;

            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleLeftPrintText(_baseobj.childgender, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += sexwidth;

            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.childbirthday, _obj.cyrq);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleLeftPrintText(agestr, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += ageheadwidth;

            top += 30;
            #endregion

            top += 60;
            #region 注意力测验
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 120, height);
            MiddleLeftPrintText("注意力测验", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);//
            g.DrawLine(pen, rect.Left, rect.Top + 30, rect.Right-20, rect.Top + 30);

            top += 60;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("时间： "+_obj.zylcy_sj_fen+" 分" + _obj.zylcy_sj_miao + " 分", rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("划错："+_obj.zylcy_hc, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("漏划：" + _obj.zylcy_lh, rect, g);

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("得分：" + _obj.zylcy_df, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("评价：" + _obj.zylcy_pj, rect, g);
            
            #endregion

            top += 60;
            #region 瑞文测验
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 120, height);
            MiddleLeftPrintText("瑞文测验", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);//
            g.DrawLine(pen, rect.Left, rect.Top + 30, rect.Right-20, rect.Top + 30);

            top += 60;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("测验总时间： " + _obj.rwcy_sj_fen+" 分" + _obj.rwcy_sj_miao + " 秒", rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("答对题数：" + _obj.rwcy_ddts, rect, g);

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("IQ：" + _obj.rwcy_iq, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("百分位等级： P" + _obj.rwcy_bfwdj, rect, g);

            #endregion

            top += 60;
            #region 视觉-运动统合发展测验
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 240, height);
            MiddleLeftPrintText("视觉-运动统合发展测验", new Font("宋体", 14f, FontStyle.Bold), new SolidBrush(Color.Black), rect, g);//
            g.DrawLine(pen, rect.Left, rect.Top + 30, rect.Right-20, rect.Top + 30);

            top += 60;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("原始分数：" + _obj.sjyd_ysfs, rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("标准分数：" + _obj.sjyd_iq, rect, g);

            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("VMI： " + _obj.sjyd_vmi_sui +" 岁" + _obj.sjyd_vmi_yue + " 月", rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("视知觉： " + _obj.sjyd_szj_sui + " 岁" + _obj.sjyd_szj_yue + " 月", rect, g);

            left += 200;
            rect = new Rectangle(left, top, 200, 30);
            MiddleLeftPrintText("动作： " + _obj.sjyd_dz_sui + " 岁" + _obj.sjyd_dz_yue + " 月", rect, g);

            #endregion

            top += 60;
            

            
            top += 50;
            #region 签名
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.cyz, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("测查者签名", rect, g);

            top += 25;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleLeftPrintText(_obj.cyrq, rect, g);
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
