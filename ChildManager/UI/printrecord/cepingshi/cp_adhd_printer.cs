using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord.cepingshi
{
    public partial class cp_adhd_printer : UserControl
    {
        private Rectangle _rectBody;

        private tb_childbase _baseobj = null;
        private cp_adhd_tab _obj = null;

        public cp_adhd_printer(tb_childbase baseobj, cp_adhd_tab obj)
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

            Rectangle rect = new Rectangle(left, top, nameheadwidth, 30);
            MiddleLeftPrintText("姓名", rect, g);
            left += nameheadwidth;

            rect = new Rectangle(left, top, namewidth, 30);
            MiddleCenterPrintTextNoBold(_baseobj.childname,rect,g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += namewidth;
            left += 50;
            rect = new Rectangle(left, top, sexheadwidth, 30);
            MiddleLeftPrintText("性别", rect, g);
            left += sexheadwidth;

            rect = new Rectangle(left, top, sexwidth, 30);
            MiddleCenterPrintTextNoBold(_baseobj.childgender, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += sexwidth;
			left += 50;
            rect = new Rectangle(left, top, ageheadwidth, 30);
            MiddleLeftPrintText("年龄", rect, g);
            left += ageheadwidth;

            int[] age = CommonHelper.getAgeBytime(_baseobj.childbirthday, _obj.csrq);
            string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "") + (age[2] > 0 ? age[2].ToString() + "天" : "");
            rect = new Rectangle(left, top, agewidth, 30);
            MiddleCenterPrintTextNoBold(agestr, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left += ageheadwidth;

            top += 30;
            #endregion

            #region 1.ADHD-1 筛查结果
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("1.ADHD-1 筛查结果",rect,g);
            left += 300;
            
            left += 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("  病史提供者", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintTextNoBold(_obj.adhd1_bstgz, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);

            top += 40;
            left = _rectBody.Left+80;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText("（  "+ _obj.adhd1_pf + "  ）分", rect, g);

            top += 40;
            left = _rectBody.Left + 80;
            rect = new Rectangle(left, top, 60, 30);
            BoldMiddleLeftPrintText("结论：", rect, g);
            left += 60;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(_obj.adhd1_jl, rect, g);
            top += 40;
            #endregion

            #region 2.	ADHD-2 （父母问卷SNAP-IV）
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("2.	ADHD-2 （父母问卷SNAP-IV）", rect, g);
            left += 300;
            
            left += 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("  病史提供者", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintTextNoBold(_obj.adhd2_bstgz, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);

            top += 40;
            left = _rectBody.Left + 80;
            
            
            //添加多选框
            int checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd2_zyqx,out checkindex);
            MiddleCenterPrintText(checkindex>=6?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("注意缺陷(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd2_zyqx, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 9    总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd2_zyqx_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);

            top += 25;
            left = _rectBody.Left + 80;
            
              //添加多选框
            checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd2_ddcd,out checkindex);
            MiddleCenterPrintText(checkindex>=6?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("多动冲动(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd2_ddcd, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 9    总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd2_ddcd_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);
            top += 25;

            left = _rectBody.Left + 80;
            
            //添加多选框
            checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd2_gnsh,out checkindex);
            MiddleCenterPrintText(checkindex>=1?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("功能损害(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd2_gnsh, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 8    总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd2_gnsh_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);

            top += 40;
            left = _rectBody.Left + 80;
            rect = new Rectangle(left, top, 60, 30);
            BoldMiddleLeftPrintText("结论：", rect, g);
            left += 60;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(_obj.adhd2_jl, rect, g);
            top += 40;
#endregion

            #region 3. ADHD-3（父母问卷SNAP-IV）
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("3. ADHD-3（父母问卷SNAP-IV）", rect, g);
            left += 300;
            left += 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("  病史提供者", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintTextNoBold(_obj.adhd3_bstgz, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);

            top += 40;
            left = _rectBody.Left + 80;
            
            //添加多选框
            checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd3_dlwk,out checkindex);
            MiddleCenterPrintText(checkindex>=4?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText(" 对立违抗(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd3_dlwk, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 8    总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd3_dlwk_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);

            top += 25;
            left = _rectBody.Left + 80;
            
            //添加多选框
            checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd3_pxza,out checkindex);
            MiddleCenterPrintText(checkindex>=3?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText(" 品行障碍(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd3_pxza, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 14   总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd3_pxza_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);

            top += 25;
            left = _rectBody.Left + 80;
            
            //添加多选框
            checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd3_jlyy,out checkindex);
            MiddleCenterPrintText(checkindex>=3?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("焦虑/抑郁(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd3_jlyy, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 7    总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd3_jlyy_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);
#endregion

            #region 4.ADHD-4 （教师问卷）
            top += 40;
            left = _rectBody.Left;
            rect = new Rectangle(left, top, 300, 30);
            BoldMiddleLeftPrintText("4. ADHD-4 （教师问卷）", rect, g);
            left += 300;
            left += 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("教师任教科目", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintTextNoBold(_obj.adhd4_bstgz, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);

            top += 40;
            left = _rectBody.Left + 80;
            
            //添加多选框
            checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd4_zyqx,out checkindex);
            MiddleCenterPrintText(checkindex>=6?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("注意缺陷(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd4_zyqx, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 9    总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd4_zyqx_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);

            top += 25;
            left = _rectBody.Left + 80;
            
            //添加多选框
            checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd4_ddcd,out checkindex);
            MiddleCenterPrintText(checkindex>=6?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("多动冲动(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd4_ddcd, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 9    总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd4_ddcd_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);

            top += 25;
            left = _rectBody.Left + 80;
            
            //添加多选框
            checkindex = 0;
            rect = new Rectangle(left, top, 30, 30);
            int.TryParse(_obj.adhd4_gnsh,out checkindex);
            MiddleCenterPrintText(checkindex>=1?"☑":"☐", new Font("宋体", 16f), new SolidBrush(Color.Black),rect, g);
            left += 30;
            
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("功能损害(", rect, g);
            left += 100;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd4_gnsh, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(") / 8    总分（", rect, g);
            left += 150;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(_obj.adhd4_gnsh_sum, rect, g);
            left += 40;
            rect = new Rectangle(left, top, 40, 30);
            MiddleLeftPrintText(")", rect, g);

            top += 40;
            left = _rectBody.Left + 80;
            rect = new Rectangle(left, top, 60, 30);
            BoldMiddleLeftPrintText("结论：", rect, g);
            left += 60;
            rect = new Rectangle(left, top, 150, 30);
            MiddleLeftPrintText(_obj.adhd4_jl, rect, g);
            top += 40;
            #endregion

            #region 签名

            left = _rectBody.Left + 70;
            rect = new Rectangle(left, top, 80, 30);
            MiddleLeftPrintText("送诊医生", rect, g);
            left += 80;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintTextNoBold(_obj.szys, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);

            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintTextNoBold(_obj.cszqm, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("测试者签名", rect, g);

            top += 25;
            left = _rectBody.Right - 120;
            rect = new Rectangle(left, top, 120, 30);
            MiddleCenterPrintTextNoBold(_obj.csrq, rect, g);
            g.DrawLine(pen, rect.Left, rect.Top + 20, rect.Right, rect.Top + 20);
            left -= 100;
            rect = new Rectangle(left, top, 100, 30);
            MiddleLeftPrintText("  测试日期", rect, g);
            #endregion

            //top += 40;
            //left = _rectBody.Left;
            //rect = new Rectangle(left, top, _rectBody.Width, 30);
            //MiddleLeftPrintText("说明：本结论为一次测试结果，具体诊断请结合临床", rect, g);
            //MiddleLeftPrintText("*此数据将用教学与科学研究", new Rectangle(left, top+30, _rectBody.Width, 30), g);

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
       
        private void MiddleCenterPrintTextNoBold(string text, Rectangle rect, Graphics g)
        {
        	StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            Font printFont = new Font("宋体", 11f);
            Brush brush = new SolidBrush(Color.Black);
            g.DrawString(text, printFont, brush, rect, sf);
        }
    }
}
