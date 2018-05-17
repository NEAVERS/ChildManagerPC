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
using YCF.Common;
using YCF.Model;

namespace ChildManager.UI.printrecord
{
    public partial class PaneljibenCheckBingliPrinter : UserControl
    {

        private const int _itemNameWidth = 88;
        private const int _itemValueWidth = 107;
        private const int _itemOptionWidth = 98;
        private const int _itemHeight = 30;
        private const int _dateTimePickerWidth = 155;

        private ArrayList _lineTopPositionList = new ArrayList(10);
        private int _printHeaderHeight = 30;//表头高90
        private int _printTailHeight = 20;//底部高40
        private int _currPrintSectionIndex;
        private int _currPrintItemIndex;
        private Rectangle _rectHeader;
        private Rectangle _rectBody;
        private Rectangle _rectTail;

        private bool jibenxinxihasprint = true;
        private bool zhusuhasprint = true;
        private bool bingshihasprint = true;
        private bool tijianhasprint = true;
        private bool zhenduanhasprint = true;
        private bool chulihasprint = true;

        private TB_CHILDBASE _baseobj = null;
        private TB_CHILDCHECK _checkobj = null;
        int _height;
        int _weight;
        int _childid;
        private List<PointF> _checkpointlist=null;

        public PaneljibenCheckBingliPrinter(TB_CHILDBASE baseobj, TB_CHILDCHECK checkobj,int childid)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _checkobj = checkobj;
            _childid = childid;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "检查结果";

            printDoc.PrintController = new StandardPrintController();//隐藏打印进度对话框
            printDoc.DefaultPageSettings.Margins = new Margins(15, 57, 15, 70);//设置上下左右边距
            
            //原来的方式
//            PaperSize ps = new PaperSize("CustomPaperSize1",420, 644);
//            printDoc.DefaultPageSettings.PaperSize = ps;
//网上的方式
            foreach (PaperSize paper in printDoc.PrinterSettings.PaperSizes)
            {
                if (paper.PaperName == "门诊处方")
                {
                    printDoc.DefaultPageSettings.PaperSize = paper;
                    break;
                }
            }
            
            //无门诊处方纸张时提示
            //if( printDoc.DefaultPageSettings.PaperSize.PaperName != "门诊处方"){
            //	MessageBox.Show("请在打印机自定义纸张添加名为[门诊处方]5.16×7.40英寸的纸张！");
            //	return;
            //}

            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);

            int printableWidth = printDoc.DefaultPageSettings.PaperSize.Width - printDoc.DefaultPageSettings.Margins.Left
                - printDoc.DefaultPageSettings.Margins.Right;
            int printalbeHeight = printDoc.DefaultPageSettings.PaperSize.Height - printDoc.DefaultPageSettings.Margins.Top
                - printDoc.DefaultPageSettings.Margins.Bottom;
            _rectHeader = new Rectangle(printDoc.DefaultPageSettings.Margins.Left, printDoc.DefaultPageSettings.Margins.Top,
                printableWidth, _printHeaderHeight);
            _rectBody = new Rectangle(_rectHeader.Left, _rectHeader.Bottom + 15, printableWidth, printalbeHeight - _printHeaderHeight - _printTailHeight-20);
            _rectTail = new Rectangle(_rectHeader.Left, _rectBody.Bottom, printableWidth, _printTailHeight+10);

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
            bool hasnextpage = false;

            PrintHeader(e.Graphics);
            PrintBody(e.Graphics, ref hasnextpage);
            PrintTail(e.Graphics);

            if(hasnextpage)
            {
                e.HasMorePages = true; //调用打印printDoc_PrintPage，重新打印一张新的页面.
            }
        }
        //打印页面表头
        private void PrintHeader(Graphics g)
        {

            string projectName = "病历";

            Rectangle rectProjectName = new Rectangle(_rectHeader.Left, _rectHeader.Top - 10, _rectHeader.Width, 45);

            MiddleCenterPrintText(projectName, new Font("宋体", 19f, FontStyle.Bold), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            
            #region 打印病人信息
            using (StringFormat sf = new StringFormat())
            using (Pen pen = new Pen(Color.Black))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                int left = _rectHeader.Left;
                int top = _rectHeader.Top;

               // Rectangle rect = new Rectangle(left, top, 200, 30);
               // g.DrawString("编号：", new Font("宋体", 10.5f), brush, rect, sf);
                

            }

            #endregion
        }

        //打印页面主体部分
        private void PrintBody(Graphics g, ref bool hasnextpage)
        {
            int currTop = _rectBody.Top+5;
            int currLeft = _rectBody.Left;
            int currWidth = _rectBody.Width;

            using (Pen blackPen = new Pen(Color.Black))
            using (Pen SandyBrownPen = new Pen(Color.SandyBrown))//浅黄色
            using (Pen redPen = new Pen(Color.Red))//红画笔
            using (Pen LightSeaGreenPen = new Pen(Color.LightSeaGreen))//紫色画笔
           //Brush blackBrush = new SolidBrush(Color.Black);
            using (Brush brush = new SolidBrush(Color.Black))
            using (Pen pen = new Pen(brush))
            using (Font cellFont = new Font("宋体", 10.5f))
            using (Font BcellFont = new Font("宋体", 10.5f, FontStyle.Bold))
            using (StringFormat sf = new StringFormat())
            {
            	pen.Width=1.5F;
                g.DrawRectangle(pen, _rectBody);

                #region 基本信息
                if(jibenxinxihasprint)
                { 
                	
                	Rectangle rect = new Rectangle(currLeft, currTop, 200, 25);
                	g.DrawString("编号："+_baseobj.HEALTHCARDNO.Trim(), new Font("宋体", 10.5f), brush, rect, sf);
                	currLeft += 100;
                	rect = new Rectangle(currLeft, currTop, 200, 25);
                	g.DrawString("姓名：" + _baseobj.CHILDNAME.Trim(), cellFont, brush, rect, sf);
                    
                    currLeft += 140;
                    
                    rect = new Rectangle(currLeft, currTop, 200, 25);
                    g.DrawString("性别：" + _baseobj.CHILDGENDER.Trim(), cellFont, brush, rect, sf);
                    
					currLeft += 70;
					
					//得到年龄
					int[] ages = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _checkobj.CHECKDAY);
                    
                    string ageStr = "";
                    if (ages[0] != 0)
                    {
                        ageStr = ages[0] + "岁" + ages[1] + "月" + ages[2] + "天";
                    }
                    else if (ages[1] != 0)
                    {
                        ageStr = ages[1] + "月" + ages[2] + "天";
                    }
                    else
                    {
                        ageStr = ages[2] + "天";
                    }
                    
                    rect = new Rectangle(currLeft, currTop, 200, 25);
                    g.DrawString("年龄：" + (_checkobj.CHECKFACTAGE==null || _checkobj.CHECKFACTAGE.Trim().Length==0?ageStr:_checkobj.CHECKFACTAGE.Trim()), cellFont, brush, rect, sf);
                    
                    currLeft = _rectBody.Left;
                    currTop += 20;
                    
                    rect = new Rectangle(currLeft, currTop, 200, 25);
                    g.DrawString("就诊时间：" + Convert.ToDateTime(_checkobj.CHECKDAY).ToString("yyyy-MM-dd"), cellFont, brush, rect, sf);

                    currLeft += 180;
                    rect = new Rectangle(currLeft, currTop, 200, 25);
                    g.DrawString("出生日期：" + Convert.ToDateTime(_baseobj.CHILDBIRTHDAY).ToString("yyyy年MM月dd日"), cellFont, brush, rect, sf);

                   
					currTop += 5;
                    g.DrawLine(pen, _rectBody.Left, currTop + 15, _rectBody.Right, currTop + 15);

                    currLeft = _rectBody.Left;
                    currTop += 20;

                    jibenxinxihasprint = false;
                }
                #endregion 基本信息

                if (zhusuhasprint)
                {
                    string zhusu = _checkobj.ZHUSU;
                    int height = (int)Math.Ceiling(g.MeasureString(zhusu, cellFont, _rectBody.Width - 70).Height);
                    if(height==0)
                    {
                        height = 20;
                    }

                    if (currTop + height < _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("主诉：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.ZHUSU, cellFont, brush, rect);

                        zhusuhasprint = false;
                        currLeft = _rectBody.Left;
                        currTop += height;

                    }
                    else if (currTop + height == _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("主诉：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.ZHUSU, cellFont, brush, rect);

                        zhusuhasprint = false;

                        hasnextpage = true;
                        return;
                    }
                    else
                    {
                        string printstr = getPrintStr(_checkobj.ZHUSU, cellFont, g, currTop);
                        if (printstr == "")
                        {
                            hasnextpage = true;
                            return;
                        }

                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("主诉：", BcellFont, brush, currLeft, currTop);
                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, _rectBody.Bottom - currTop);
                        g.DrawString(printstr, cellFont, brush, rect);
                        
                            _checkobj.ZHUSU = _checkobj.ZHUSU.Replace(printstr, "");

                        hasnextpage = true;
                        return;
                    }
                }

                if (bingshihasprint)
                {

                    int height = (int)Math.Ceiling(g.MeasureString(_checkobj.BINGSHI, cellFont, _rectBody.Width - 70).Height);
                    if (height == 0)
                    {
                        height = 20;
                    }

                    if (currTop + height < _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("病史：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.BINGSHI, cellFont, brush, rect);

                        bingshihasprint = false;
                        currLeft = _rectBody.Left;
                        currTop += height;

                    }
                    else if (currTop + height == _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("病史：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.BINGSHI, cellFont, brush, rect);

                        bingshihasprint = false;

                        hasnextpage = true;
                        return;
                    }
                    else
                    {
                        string printstr = getPrintStr(_checkobj.BINGSHI, cellFont, g, currTop);
                        if (printstr == "")
                        {
                            hasnextpage = true;
                            return;
                        }

                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("病史：", BcellFont, brush, currLeft, currTop);
                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, _rectBody.Bottom - currTop);
                        g.DrawString(printstr, cellFont, brush, rect);
                        
                            _checkobj.BINGSHI = _checkobj.BINGSHI.Replace(printstr, "");

                        hasnextpage = true;
                        return;
                    }
                }

                if (tijianhasprint)
                {

                    int height = (int)Math.Ceiling(g.MeasureString(_checkobj.TIJIAN, cellFont, _rectBody.Width - 70).Height);
                    if (height == 0)
                    {
                        height = 20;
                    }

                    if (currTop + height < _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("体检：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.TIJIAN, cellFont, brush, rect);

                        tijianhasprint = false;
                        currLeft = _rectBody.Left;
                        currTop += height;

                    }
                    else if (currTop + height == _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("体检：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.TIJIAN, cellFont, brush, rect);

                        tijianhasprint = false;

                        hasnextpage = true;
                        return;
                    }
                    else
                    {
                        string printstr = getPrintStr(_checkobj.TIJIAN, cellFont, g, currTop);
                        if (printstr == "")
                        {
                            hasnextpage = true;
                            return;
                        }

                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("体检：", BcellFont, brush, currLeft, currTop);
                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, _rectBody.Bottom - currTop);
                        g.DrawString(printstr, cellFont, brush, rect);
                        
                            _checkobj.TIJIAN = _checkobj.TIJIAN.Replace(printstr, "");

                        hasnextpage = true;
                        return;
                    }
                }

                if (zhenduanhasprint)
                {

                    int height = (int)Math.Ceiling(g.MeasureString(_checkobj.ZHENDUAN, cellFont, _rectBody.Width - 70).Height);
                    if (height == 0)
                    {
                        height = 20;
                    }

                    if (currTop + height < _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("诊断：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.ZHENDUAN, cellFont, brush, rect);

                        zhenduanhasprint = false;
                        currLeft = _rectBody.Left;
                        currTop += height;

                    }
                    else if (currTop + height == _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("诊断：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.ZHENDUAN, cellFont, brush, rect);

                        zhenduanhasprint = false;

                        hasnextpage = true;
                        return;
                    }
                    else
                    {
                        string printstr = getPrintStr(_checkobj.ZHENDUAN, cellFont, g, currTop);
                        if (printstr == "")
                        {
                            hasnextpage = true;
                            return;
                        }

                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("诊断：", BcellFont, brush, currLeft, currTop);
                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, _rectBody.Bottom - currTop);
                        g.DrawString(printstr, cellFont, brush, rect);
                        
                            _checkobj.ZHENDUAN = _checkobj.ZHENDUAN.Replace(printstr, "");

                        hasnextpage = true;
                        return;
                    }
                }

                if (chulihasprint)
                {

                    int height = (int)Math.Ceiling(g.MeasureString(_checkobj.CHULI, cellFont, _rectBody.Width - 70).Height);
                    if (height == 0)
                    {
                        height = 20;
                    }

                    if (currTop + height < _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("处理：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.CHULI, cellFont, brush, rect);

                        chulihasprint = false;
                        currLeft = _rectBody.Left;
                        currTop += height;

                    }
                    else if (currTop + height == _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("处理：", BcellFont, brush, currLeft, currTop);

                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, height);
                        g.DrawString(_checkobj.CHULI, cellFont, brush, rect);

                        chulihasprint = false;

                        hasnextpage = true;
                        return;
                    }
                    else
                    {
                        string printstr = getPrintStr(_checkobj.CHULI, cellFont, g,currTop);
                        if (printstr == "")
                        {
                            hasnextpage = true;
                            return;
                        }
                            

                        Rectangle rect = new Rectangle(currLeft, currTop, 60, 25);
                        g.DrawString("处理：", BcellFont, brush, currLeft, currTop);
                        currLeft += 60;
                        rect = new Rectangle(currLeft, currTop, _rectBody.Width - 70, _rectBody.Bottom - currTop);
                        g.DrawString(printstr, cellFont, brush, rect);

                        
                            _checkobj.CHULI = _checkobj.CHULI.Replace(printstr, "");

                        hasnextpage = true;
                        return;
                    }
                }

                //PrintStr("主诉：", ref _checkobj.ZHUSU, ref currLeft, ref currTop,cellFont,BcellFont,brush,g,ref zhusuhasprint,ref hasnextpage);
                //if (hasnextpage) return;

                //PrintStr("病史：", ref _checkobj.BINGSHI, ref currLeft, ref currTop, cellFont, BcellFont, brush, g, ref bingshihasprint, ref hasnextpage);
                //if (hasnextpage) return;

                //PrintStr("体检：", ref _checkobj.TIJIAN, ref currLeft, ref currTop, cellFont, BcellFont, brush, g, ref tijianhasprint, ref hasnextpage);
                //if (hasnextpage) return;

                //PrintStr("诊断：", ref _checkobj.ZHENDUAN, ref currLeft, ref currTop, cellFont, BcellFont, brush, g, ref zhenduanhasprint, ref hasnextpage);
                //if (hasnextpage) return;

                //PrintStr("处理：", ref _checkobj.CHULI, ref currLeft, ref currTop, cellFont, BcellFont, brush, g, ref chulihasprint, ref hasnextpage);

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
            	pen.Width=1.5F;
                g.DrawRectangle(pen, _rectTail);
                Rectangle rect = new Rectangle(_rectTail.Left, _rectTail.Top + 6, _rectTail.Width, _rectTail.Height);
                g.DrawString("医师：" + _checkobj.CK_FZ + "                   打印日期：" + _checkobj.CHECKDAY, new Font("宋体", 11f), brush, rect, sf);

            }
        }

        private void PrintStr(string strname,ref string str,ref int pleft, ref int ptop, Font cellFont, Font BcellFont, Brush brush, Graphics g, ref bool hasprint, ref bool hasnextpage)
        {
            if (hasprint)
            {
                int height = (int)Math.Ceiling(g.MeasureString(str, cellFont, _rectBody.Bottom - 70).Height);

                if (ptop + height < _rectBody.Bottom)
                {
                    Rectangle rect = new Rectangle(pleft, ptop, 60, 20);
                    g.DrawString(strname, BcellFont, brush, pleft, ptop);

                    pleft += 60;
                    rect = new Rectangle(pleft, ptop, _rectBody.Width - 70, height);
                    g.DrawString(str, cellFont, brush, rect);

                    hasprint = false;
                    pleft = _rectBody.Left;
                    ptop += height;

                }
                else if (ptop + height == _rectBody.Bottom)
                {
                    Rectangle rect = new Rectangle(pleft, ptop, 60, 20);
                    g.DrawString(strname, BcellFont, brush, pleft, ptop);

                    pleft += 60;
                    rect = new Rectangle(pleft, ptop, _rectBody.Width - 70, height);
                    g.DrawString(str, cellFont, brush, rect);

                    hasprint = false;

                    hasnextpage = true;
                    return;
                }
                else
                {
                    string printstr = getPrintStr(str, cellFont, g,ptop);

                    Rectangle rect = new Rectangle(pleft, ptop, 60, 20);
                    g.DrawString(strname, BcellFont, brush, pleft, ptop);
                    pleft += 60;
                    rect = new Rectangle(pleft, ptop, _rectBody.Width - 70, _rectBody.Bottom - ptop);
                    g.DrawString(printstr, cellFont, brush, rect);

                    if(printstr!="")
                    str = str.Replace(printstr, "");

                    hasnextpage = true;
                    return;
                }
            }
        }

        private string getPrintStr(string str,Font cellFont, Graphics g,int ptop)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, j = 0; i < str.Length; i++)
            {
                if (g.MeasureString(str.Substring(j, i - j + 1), cellFont,_rectBody.Width-70).Height > _rectBody.Bottom - ptop)
                {
                    break;
                }
                builder.Append(str[i]);
            }
            return builder.ToString();
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
