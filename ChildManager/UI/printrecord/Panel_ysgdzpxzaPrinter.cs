using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.Model;
using System.Drawing.Printing;
using ChildManager.BLL;
using System.Drawing.Drawing2D;
using YCF.Model;
using YCF.Common;

namespace ChildManager.UI.printrecord
{
    public partial class Panel_ysgdzpxzaPrinter : UserControl
    {

        private const int _itemNameWidth = 88;
        private const int _itemValueWidth = 107;
        private const int _itemOptionWidth = 98;
        private const int _itemHeight = 30;
        private const int _dateTimePickerWidth = 155;

        private ArrayList _lineTopPositionList = new ArrayList(10);
        private int _printHeaderHeight = 90;//表头高90
        private int _printTailHeight = 40;//底部高40
        private int _currPrintSectionIndex;
        private int _currPrintItemIndex;
        private Rectangle _rectHeader;
        private Rectangle _rectBody;
        private Rectangle _rectTail;

        private TB_CHILDBASE _baseobj = null;
        private TB_CHILDCHECK _checkobj = null;
        int _height;
        int _weight;
        int _childid;
        string[] _bmipingfen;
        private List<PointF> _checkpointlist = null;
        private YS_GDZPXZA_TAB _obj = null;

        public Panel_ysgdzpxzaPrinter(YS_GDZPXZA_TAB obj)
        {
            InitializeComponent();
            _obj = obj;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "儿童健康检查表";

            ChildCheckBll bll = new ChildCheckBll();
            string sqls = string.Format("select * from TB_CHILDCHECK where childId  = " + _childid + "");

            _checkpointlist = bll.getchildcheckPrint(sqls);
            #region
            //// 设置打印机名
            //System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            //xmlDoc.Load(App.AppDir + "\\" + App.AppName + ".xml");
            //if (xmlDoc.SelectSingleNode("egret/ehis/nursingRecordPrinter") == null)
            //{
            //    MessageBox.Show("未找到打印机名配置项", "系统提示", 0, MessageBoxIcon.Exclamation, 0);
            //    return;
            //}
            //string patrolCardPrinterName = xmlDoc.SelectSingleNode("egret/ehis/nursingRecordPrinter").InnerText;
            //printDoc.PrinterSettings.PrinterName = patrolCardPrinterName;  
            #endregion

            printDoc.PrintController = new StandardPrintController();//隐藏打印进度对话框
            printDoc.DefaultPageSettings.Margins = new Margins(45, 15, 45, 30);//设置上下左右边距
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            int printableWidth = printDoc.DefaultPageSettings.PaperSize.Width - printDoc.DefaultPageSettings.Margins.Left
                - printDoc.DefaultPageSettings.Margins.Right;
            int printalbeHeight = printDoc.DefaultPageSettings.PaperSize.Height - printDoc.DefaultPageSettings.Margins.Top
                - printDoc.DefaultPageSettings.Margins.Bottom;
            _rectHeader = new Rectangle(printDoc.DefaultPageSettings.Margins.Left, printDoc.DefaultPageSettings.Margins.Top,
                printableWidth, _printHeaderHeight);
            _rectBody = new Rectangle(_rectHeader.Left, _rectHeader.Bottom, printableWidth, printalbeHeight - _printHeaderHeight - _printTailHeight);
            _rectTail = new Rectangle(_rectHeader.Left, _rectBody.Bottom, printableWidth, _printTailHeight);

            _currPrintSectionIndex = 0;
            _currPrintItemIndex = 0;

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
            //Bitmap myBitmap = new Bitmap(this.Width, this.Height, e.Graphics);
            //this.DrawToBitmap(myBitmap, new Rectangle(0, 0, this.Width, this.Height));
            //e.Graphics.DrawImage(myBitmap, new Rectangle(0, 0, this.Width, this.Height));            

            PrintHeader(e.Graphics);
            PrintBody(e.Graphics);
            PrintTail(e.Graphics);
        }
        //打印页面表头
        private void PrintHeader(Graphics g)
        {
            #region 打印表头
            int top = _rectBody.Top - 80;
            int left = _rectBody.Left;
            int height = 45;
            Rectangle rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            string projectName = CommonHelper.GetHospitalName();
            MiddleCenterPrintText(projectName, new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("重庆市儿童行为发育与心理健康中心", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            rectProjectName = new Rectangle(left, top, _rectBody.Width, height);
            MiddleCenterPrintText("孤独症贫血障碍专案", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            top += height;
            Top += top;
            #endregion

            #region 打印病人信息
            using (StringFormat sf = new StringFormat())
            using (Pen pen = new Pen(Color.Black))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                //int nameHeaderWidth = 100;
                //int nameWidth = 220;
                //int wardHeaderWidth = 60;
                //int wardWidth = 80;
                //int bednoHeaderWidth = 55;
                //int bednoWidth = 80;
                //int mrnHeaderWidth = 70;
                //int mrnWidth = 70;

                //int patientInfoHeight = 45;

                //int width = nameHeaderWidth + nameWidth + wardHeaderWidth + wardWidth + bednoHeaderWidth + bednoWidth + mrnHeaderWidth + mrnWidth;
                ////int left = (_rectHeader.Width - width) / 2 + _rectHeader.Left;
                //int left = 0;
                ////int top = rectFormName.Bottom;
                //int top = _rectHeader.Top + 90;
                //Rectangle rect = new Rectangle(left, top, nameHeaderWidth, 45);
                //g.DrawString("医疗单位：", new Font("Arial Unicode MS", 9f), brush, rect, sf);
                //left += nameHeaderWidth;
                //rect = new Rectangle(left, top, nameWidth, patientInfoHeight);
                //g.DrawString("重庆市大足区妇幼保健院", new Font("Arial Unicode MS", 9f), brush, rect, sf);
                //g.DrawLine(pen, rect.Left, rect.Bottom - 14, rect.Left + 230, rect.Bottom - 14);
                //left += nameWidth + 15;


                //rect = new Rectangle(left, top, wardHeaderWidth, patientInfoHeight);
                //g.DrawString("姓名：", new Font("Arial Unicode MS", 9f), brush, rect, sf);
                //left += wardHeaderWidth;

                //rect = new Rectangle(left, top, wardWidth, patientInfoHeight);

                //g.DrawString(_patient.name, new Font("Arial Unicode MS", 9f), brush, rect, sf);

                //g.DrawLine(pen, rect.Left, rect.Bottom - 14, rect.Right, rect.Bottom - 14);
                //left += wardWidth + 15;

                //rect = new Rectangle(left, top, bednoHeaderWidth, patientInfoHeight);
                //g.DrawString("床号：", new Font("Arial Unicode MS", 9f), brush, rect, sf);
                //left += bednoHeaderWidth;

                //rect = new Rectangle(left, top, bednoWidth, patientInfoHeight);
                ////if (_patient != null)
                ////{
                //g.DrawString(_patient.bed.bedno, new Font("Arial Unicode MS", 9f), brush, rect, sf);
                ////}
                //g.DrawLine(pen, rect.Left, rect.Bottom - 14, rect.Right, rect.Bottom - 14);
                //left += bednoWidth + 15;

                //rect = new Rectangle(left, top, mrnHeaderWidth, patientInfoHeight);
                //g.DrawString("住院号：", new Font("Arial Unicode MS", 9f), brush, rect, sf);
                //left += mrnHeaderWidth;

                //rect = new Rectangle(left, top, mrnWidth, patientInfoHeight);
                ////if (_patient != null)
                ////{
                //g.DrawString(_patient.idCode, new Font("Arial Unicode MS", 9f), brush, rect, sf);
                ////}
                //g.DrawLine(pen, rect.Left, rect.Bottom - 14, rect.Right, rect.Bottom - 14);
                //left += mrnWidth;

            }

            #endregion
        }

        //打印页面主体部分
        private void PrintBody(Graphics g)
        {
            int currTop = Top;
            int currLeft = _rectBody.Left;

            using (Pen blackPen = new Pen(Color.Black))
            using (Pen SandyBrownPen = new Pen(Color.SandyBrown))//浅黄色
            using (Pen redPen = new Pen(Color.Red))//红画笔
            using (Pen LightSeaGreenPen = new Pen(Color.LightSeaGreen))//紫色画笔
            //Brush blackBrush = new SolidBrush(Color.Black);
            using (Brush brush = new SolidBrush(Color.Black))
            using (Pen pen = new Pen(brush))
            using (StringFormat sf = new StringFormat())
            {
                string str = "主诉：";
                string zs_check = _obj.ZS_CHECK;
                string zs_text = _obj.ZS_TEXT;
                str +=  zs_check+"。"+ zs_text;
                str += "。\r\n现病史：";
                string xbs_yylj = _obj.XBS_YYLJ;
                str += "语言理解："+ xbs_yylj;
                string xbs_yybd = _obj.XBS_YYBD;
                str += "，语言表达：" + xbs_yybd;
                string xbs_fyybd = _obj.XBS_FYYBD;
                str += "，非语言表达：" + xbs_fyybd;
                string xbs_shjw = _obj.XBS_SHJW;
                str += "，社会交往：" + xbs_shjw;
                string xbs_cfkb = _obj.XBS_CFKB;
                str += "，重复刻板和狭隘兴趣：" + xbs_cfkb;
                string xbs_cf = _obj.XBS_CFKB;
                str += "，重复：" + xbs_cf;
                //其他共患情况
                string xbs_other = _obj.XBS_OTHER;
                str += "，其他共患情况：" + xbs_cf;

                Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, _rectBody.Height);
                g.DrawString(str, new Font("宋体", 12f), brush, rect, sf);
            }
        }
        private void PrintTail(Graphics g)
        {
            Font fontTail = new Font("Arial Unicode MS", 9f);
            int left = _rectBody.Left;

            using (StringFormat sf = new StringFormat())
            using (Pen pen = new Pen(Color.Black))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                //sf.Alignment = StringAlignment.Center;
                //sf.LineAlignment = StringAlignment.Center;

                int doctorWidth = 95;
                int doctorwidthval = 70;
                int jiesheng = 70;
                int jieshengval = 70;
                int fenghe = 70;
                int fengheval = 70;
                int xunhui = 70;
                int xunhuival = 70;
                Rectangle rect = new Rectangle(left, _rectTail.Top + 20, doctorWidth, _rectTail.Height);
                //g.DrawString("负责医生：", fontTail, brush, rect, sf);
                //left += doctorWidth;

                //rect = new Rectangle(left, _rectTail.Top + 20, doctorwidthval, _rectTail.Height);
                //g.DrawString("", fontTail, brush, rect, sf);

                //g.DrawLine(pen, rect.Left - 10, _rectTail.Top + 40, rect.Right, _rectTail.Top + 40);
                //left += doctorwidthval + 25;

                //rect = new Rectangle(left, _rectTail.Top + 20, jiesheng, _rectTail.Height);
                //g.DrawString("接生者：", fontTail, brush, rect, sf);
                //left += jiesheng;
                //rect = new Rectangle(left, _rectTail.Top + 20, jieshengval, _rectTail.Height);
                //g.DrawString("", fontTail, brush, rect, sf);
                //g.DrawLine(pen, rect.Left, _rectTail.Top + 40, rect.Right, _rectTail.Top + 40);
                //left += jieshengval + 25;
                ////缝合者
                //rect = new Rectangle(left, _rectTail.Top + 20, fenghe, _rectTail.Height);
                //g.DrawString("缝合者：", fontTail, brush, rect, sf);
                //left += fenghe;
                //rect = new Rectangle(left, _rectTail.Top + 20, fengheval, _rectTail.Height);
                //g.DrawString("", fontTail, brush, rect, sf);
                //g.DrawLine(pen, rect.Left, _rectTail.Top + 40, rect.Right, _rectTail.Top + 40);
                //left += fengheval + 25;
                ////巡回者
                //rect = new Rectangle(left, _rectTail.Top + 20, xunhui, _rectTail.Height);
                //g.DrawString("巡回者：", fontTail, brush, rect, sf);
                //left += xunhui;
                //rect = new Rectangle(left, _rectTail.Top + 20, xunhuival, _rectTail.Height);
                //g.DrawString("", fontTail, brush, rect, sf);
                //g.DrawLine(pen, rect.Left, _rectTail.Top + 40, rect.Right, _rectTail.Top + 40);

                //g.DrawString("建档时间:", fontTail, brush, rect, sf);
                //left += timeHeaderWidth;

                //rect = new Rectangle(left+20, _rectTail.Top+10, timeWidth, _rectTail.Height);

                //// g.DrawString(_form.recordTime.ToString("yyyy-MM-dd HH:mm"), fontTail, brush, rect, sf);
                //g.DrawString("", fontTail, brush, rect, sf);
                //g.DrawLine(pen, rect.Left, rect.Bottom - 17, rect.Right, rect.Bottom - 17);



            }
        }

        private bool PrintItem(Graphics g, ref int top, ref int left, string itemname, string itemvalue)
        {

            Font fontItemName = new Font("宋体", 10.0f);
            Font fontItemValue = new Font("宋体", 10.0f);

            SizeF itemNameSize = g.MeasureString(itemname + ":", fontItemName);
            int itemNameWidth = (int)Math.Ceiling(itemNameSize.Width);



            string itemValue = itemvalue;


            int valueWidth = (int)Math.Ceiling(g.MeasureString(itemValue, fontItemValue).Width) + 4;


            Rectangle rectItemName = new Rectangle(left, top, itemNameWidth, _itemHeight);

            TopLeftPrintText(itemname + ":", fontItemName, new SolidBrush(Color.Black), rectItemName, g);
            left += itemNameWidth;

            Rectangle rectItemValue = new Rectangle(left, top, valueWidth, _itemHeight);

            TopLeftPrintText(itemValue, fontItemValue, new SolidBrush(Color.Black), rectItemValue, g);
            left += (int)Math.Ceiling((itemNameWidth + valueWidth) * 1.0 / (_itemNameWidth + _itemValueWidth)) * (_itemNameWidth + _itemValueWidth) - itemNameWidth;


            return true;
        }

        private void MiddleCenterPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
        }

        private void BottomCenterPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Far;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void TopCenterPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Near;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void MiddleLeftPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void BottomLeftPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Far;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void TopLeftPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void MiddleRightPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Center;

            g.DrawString(text, font, brush, rect, sf);
        }
        private void BottomRightPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Far;

            g.DrawString(text, font, brush, rect, sf);
        }

        private void TopRightPrintText(string text, Font font, Brush brush, Rectangle rect, Graphics g)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Near;

            g.DrawString(text, font, brush, rect, sf);
        }

    }
}
