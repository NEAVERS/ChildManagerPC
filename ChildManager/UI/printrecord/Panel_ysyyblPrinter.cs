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
    public partial class Panel_ysyyblPrinter : UserControl
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

        private tb_childbase _baseobj = null;
        private tb_childcheck _checkobj = null;
        int _height;
        int _weight;
        int _childid;
        string[] _bmipingfen;
        private List<PointF> _checkpointlist = null;
        private ys_yybl_tab _obj = null;

        public Panel_ysyyblPrinter(ys_yybl_tab obj)
        {
            InitializeComponent();
            _obj = obj;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "儿童健康检查表";
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
            //Font fontTitle = new Font("宋体", 18f);
            //MiddleCenterPrintText("南京军区总医院" + _form.name, fontTitle, new SolidBrush(Color.Black), _rectHeader, g);
            //string projectName = "重  庆  医  科  大  学  附  属  第  一  医  院";
            //string projectName = "孤独症贫血障碍";

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
            MiddleCenterPrintText("营养不良专案", new Font("宋体", 16f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
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
                string zs_check = _obj.zs_check;
                str += ""+ zs_check;
                str += "。\r\n现病史：";
                string xbs = _obj.xbs;
                str += xbs+"";
                str += "。\r\n生产史：";
                string sc_g = _obj.sc_g;
                str += "G："+sc_g;
                string sc_p = _obj.sc_p;
                str += "，P：" + sc_p;
                string sc_yz = _obj.sc_yz;
                str += "，孕："+sc_yz+" 周";
                string sc_fs = _obj.sc_fs;
                str += "，分娩方式：" + sc_fs;
                string sc_yy = _obj.sc_yy;
                str += "，原因：" + sc_yy;
                string sc_weight = _obj.sc_weight;
                str += "，产重：" + sc_weight;
                string sc_mqyqyy = _obj.sc_mqyqyy;
                str += "，母亲孕期营养："+_obj.sc_mqyqyy;
                //母孕期疾病
                string sc_ismqjb = _obj.sc_ismqjb;
                str += "，母孕期疾病：" + _obj.sc_ismqjb;

                string sc_mqjb = _obj.sc_mqjb;
                str += _obj.sc_mqjb;
                string sc_isbts = _obj.sc_isbts;
                string sc_bts = _obj.sc_bts;
                str += "，保胎史："+ sc_isbts+" "+ sc_bts;
                str += "。\r\n喂养史：";
                string wy_sh = _obj.wy_sh;
                str += "生后：" + wy_sh;
                string wy_xs = _obj.wy_xs;
                str += "，吸吮：" + wy_xs;
                string wy_qs = _obj.wy_qs;
                str += "，呛吮：" + wy_qs;
                string wy_cnsd = _obj.wy_cnsd;
                str += "，吃奶速度：" + wy_cnsd;
                string wy_jjcn = _obj.wy_jjcn;
                str += "，拒绝吃奶：" + wy_jjcn;
                string wy_jsl = _obj.wy_jsl;
                str += "，进食量：" + wy_jsl;

                string wy_fsnl = _obj.wy_fsnl;
                str += "，辅食添加年龄：" + wy_fsnl;
                str += "。\r\n生长发育史：";
                string szfy_dyd = _obj.szfy_dyd;
                if (szfy_dyd.Contains(":"))
                {
                    szfy_dyd = szfy_dyd.Replace(":", "") + "次";
                }
                str += "大运动：" + szfy_dyd;

                string szfy_jxyd = _obj.szfy_jxyd;
                if (szfy_jxyd.Contains(":"))
                {
                    szfy_jxyd = szfy_jxyd.Replace(":", "") + "次";
                }
                str += "，精细运动：" + szfy_jxyd;
                string szfy_yy = _obj.szfy_yy;
                if (szfy_yy.Contains(":"))
                {
                    szfy_yy = szfy_yy.Replace(":", "") + "次";
                }
                str += "，语言：" + szfy_yy;
                string szfy_grsh = _obj.szfy_grsh;
                if (szfy_grsh.Contains(":"))
                {
                    szfy_grsh = szfy_grsh.Replace(":", "") + "次";
                }
                str += "，个人社会：" + szfy_grsh;

                str += "。\r\n预防接种史：";
                string yfjz_jz = _obj.yfjz_jz;
                str += yfjz_jz;

                str += "。\r\n既往史：";
                string jws_sb = _obj.jws_sb;
                str += jws_sb;
                string jws_fxs = _obj.jws_fxs;
                if (jws_fxs.Contains(":"))
                {
                    jws_fxs = jws_fxs.Replace(":","")+"次";
                }
                str += "，腹泻史：" + jws_fxs;
                string jws_fys = _obj.jws_fys;
                if (jws_fys.Contains(":"))
                {
                    jws_fys = jws_fys.Replace(":", "") + "次";
                }
                str += "，肺炎史：" + jws_fys;
                string jws_xcs = _obj.jws_xcs;
                if (jws_xcs.Contains(":"))
                {
                    jws_xcs = jws_xcs.Replace(":", "") + "次";
                }
                str += "，哮喘史：" + jws_xcs;
                string swgm = _obj.jws_swgm;
                str += "，食物过敏："+swgm;
                string jws_ywgm = _obj.jws_ywgm;
                str += "，药物过敏：" + jws_ywgm;

                str += "。\r\n家庭生后环境：";
                string jthj_fmtx = _obj.jthj_fmtx;
                str += " " + jthj_fmtx;
                string jthj_ycs = _obj.jthj_ycs;
                str += "，遗传病史：" + jthj_ycs;
                string jthj_crbs = _obj.jthj_crbs;
                str += "，传染病接触史：" + jthj_crbs;

                str += "。\r\n体检：";
                string tj_w = _obj.tj_w;
                str += " W：" + tj_w+" Kg";
                string tj_l = _obj.tj_l;
                str += " ，L：" + tj_l+" CM";
                string tj_js = _obj.tj_js;
                str += "，精神：" + tj_js;
                str += "。";
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
