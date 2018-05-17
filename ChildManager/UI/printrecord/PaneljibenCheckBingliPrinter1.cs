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
    public partial class PaneljibenCheckBingliPrinter1 : UserControl
    {

        private const int _itemNameWidth = 88;
        private const int _itemValueWidth = 107;
        private const int _itemOptionWidth = 98;
        private const int _itemHeight = 30;
        private const int _dateTimePickerWidth = 155;

        private ArrayList _lineTopPositionList = new ArrayList(10);
        private int _printHeaderHeight = 45;//表头高90
        private int _printTailHeight = 30;//底部高40
        private int _currPrintSectionIndex;
        private int _currPrintItemIndex;
        private Rectangle _rectHeader;
        private Rectangle _rectBody;
        private Rectangle _rectTail;

        private bool jibenxinxihasprint = true;
        private bool zjhasprint = true;
        private bool wyhasprint = true;
        private bool fjhasprint = true;
        private int _fjIndex = 0;
        private IList<string> _fjTextList = null;

        private TB_CHILDBASE _baseobj = null;
        private TB_CHILDCHECK _checkobj = null;
        int _height;
        int _weight;
        string[] _bmipingfen;
        private List<PointF> _checkpointlist = null;
        MB_ZD _zdobj = null;
        MB_WY _wyobj = null;

        public PaneljibenCheckBingliPrinter1(TB_CHILDBASE baseobj, TB_CHILDCHECK checkobj, string[] bmipingfen, MB_ZD zdobj, MB_WY wyobj)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _checkobj = checkobj;
            _bmipingfen = bmipingfen;
            _zdobj = zdobj;
            _wyobj = wyobj;
            if (_zdobj == null)
                _zdobj = new MB_ZD();
            if (_wyobj == null)
                _wyobj = new MB_WY();
            _wyobj.WYZD = "喂养指导\r\n" + _wyobj.WYZD;
            _zdobj.ZJZD = "早教指导\r\n" + _zdobj.ZJZD;
            _fjTextList = new List<string>
            {
                "     (请于" + _checkobj.FUZENCOMBOBOX + "后再来复诊,下次复诊,请带此单。）",
                "儿保门诊预约方式：微信预约、工行自助机、www.jkwin.com.cn(医事",
                "通)挂号预约",
            };
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "儿童健康检查结果";

            printDoc.PrintController = new StandardPrintController();//隐藏打印进度对话框
            printDoc.DefaultPageSettings.Margins = new Margins(15, 57, 15, 70);//设置上下左右边距

            //         	PaperSize ps = new PaperSize("CustomPaperSize1",420, 644);
            //         	printDoc.DefaultPageSettings.PaperSize = ps;
            foreach (PaperSize paper in printDoc.PrinterSettings.PaperSizes)
            {
                if (paper.PaperName == "门诊处方")
                {

                    printDoc.DefaultPageSettings.PaperSize = paper;
                    break;
                }
            }

            //无门诊处方纸张时提示
            //if (printDoc.DefaultPageSettings.PaperSize.PaperName != "门诊处方")
            //{
            //    MessageBox.Show("请在打印机自定义纸张添加名为[门诊处方]5.16×7.40英寸的纸张！");
            //    return;
            //}


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
            bool hasnextpage = false;

            PrintHeader(e.Graphics);
            PrintBody(e.Graphics, ref hasnextpage);
            PrintTail(e.Graphics);

            if (hasnextpage)
            {
                e.HasMorePages = true; //调用打印printDoc_PrintPage，重新打印一张新的页面.
            }
        }
        //打印页面表头
        private void PrintHeader(Graphics g)
        {

            string projectName = "儿童健康检查结果";

            Rectangle rectProjectName = new Rectangle(_rectHeader.Left, _rectHeader.Top - 10, _rectHeader.Width, 45);

            MiddleCenterPrintText(projectName, new Font("新宋体", 16f), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称

            #region 打印病人信息
            //using (StringFormat sf = new StringFormat())
            //using (Pen pen = new Pen(Color.Black))
            //using (Brush brush = new SolidBrush(Color.Black))
            //{
            //    sf.Alignment = StringAlignment.Center;
            //    sf.LineAlignment = StringAlignment.Center;
            //    int left = _rectHeader.Left;
            //    int top = rectProjectName.Left;

            //    Rectangle rect = new Rectangle(left, top, 200, 30);
            //    g.DrawString("编号："+_baseobj.HEALTHCARDNO, new Font("宋体", 11f), brush, rect, sf);


            //}

            #endregion
        }

        //打印页面主体部分
        private void PrintBody(Graphics g, ref bool hasnextpage)
        {
            int currTop = _rectBody.Top + 10;
            int currLeft = _rectBody.Left;
            int currWidth = _rectBody.Width;

            using (Pen blackPen = new Pen(Color.Black))
            using (Pen SandyBrownPen = new Pen(Color.SandyBrown))//浅黄色
            using (Pen redPen = new Pen(Color.Red))//红画笔
            using (Pen LightSeaGreenPen = new Pen(Color.LightSeaGreen))//紫色画笔
                                                                       //Brush blackBrush = new SolidBrush(Color.Black);
            using (Brush brush = new SolidBrush(Color.Black))
            using (Pen pen = new Pen(brush))
            using (Font cellFont = new Font("新宋体", 10f))
            using (Font BcellFont = new Font("新宋体", 10f, FontStyle.Bold))
            using (StringFormat sf = new StringFormat())
            {
                pen.Width = 1.5F;
                g.DrawRectangle(pen, _rectBody);

                #region 基本信息
                if (jibenxinxihasprint)
                {
                    Rectangle rect = new Rectangle(currLeft, currTop, 200, 20);
                    g.DrawString("编号：" + _baseobj.HEALTHCARDNO, cellFont, brush, rect, sf);

                    currLeft += 180;
                    rect = new Rectangle(currLeft, currTop, 200, 20);
                    g.DrawString("姓名：" + _baseobj.CHILDNAME, cellFont, brush, rect, sf);

                    currLeft += 150;
                    rect = new Rectangle(currLeft, currTop, 200, 20);
                    g.DrawString("性别：" + _baseobj.CHILDGENDER, cellFont, brush, rect, sf);

                    currLeft = _rectBody.Left;
                    currTop += 20;
                    rect = new Rectangle(currLeft, currTop, 200, 20);
                    g.DrawString("出生日期：" + Convert.ToDateTime(_baseobj.CHILDBIRTHDAY).ToString("yyyy年MM月dd日"), cellFont, brush, rect, sf);

                    int[] ages = CommonHelper.getAgeBytime(_baseobj.CHILDBIRTHDAY, _checkobj.CHECKDAY);
                    currLeft += 200;
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
                    rect = new Rectangle(currLeft, currTop, 200, 20);
                    g.DrawString("年龄：" + ageStr, cellFont, brush, rect, sf);

                    currTop += 15;
                    currLeft = _rectBody.Left;

                    g.DrawLine(pen, currLeft, currTop, _rectBody.Right, currTop);

                    currLeft = _rectBody.Left;
                    currTop += 3;

                    rect = new Rectangle(currLeft, currTop, 130, 20);
                    g.DrawString("血色素：" + (_checkobj.BLOODSESU == null || _checkobj.BLOODSESU.Trim().Length == 0 ? "    " : _checkobj.BLOODSESU) + "g/L", cellFont, brush, rect, sf);

                    rect = new Rectangle(currLeft + 130, currTop, 130, 20);
                    g.DrawString("牙齿数：" + _checkobj.YACINUMBER + "     ", cellFont, brush, rect, sf);

                    rect = new Rectangle(currLeft + 260, currTop, 130, 20);
                    g.DrawString("  龋齿：" + _checkobj.YUCINUMBER + "     ", cellFont, brush, rect, sf);



                    currTop += 18;
                    currLeft = _rectBody.Left;
                    rect = new Rectangle(currLeft, currTop, 200, 25);
                    g.DrawString("体格发育评价：", cellFont, brush, rect, sf);

                    currTop += 18;
                    currLeft = _rectBody.Left + 30;
                    rect = new Rectangle(currLeft, currTop, 200, 25);
                    g.DrawString("生长水平：", cellFont, brush, rect, sf);
                    currLeft += 80;

                    //身高
                    rect = new Rectangle(currLeft, currTop, 60, 25);
                    g.DrawString(_bmipingfen[2].Split(',')[0], cellFont, brush, rect, sf);
                    //身高数值
                    rect = new Rectangle(currLeft + 60, currTop, 200, 25);
                    g.DrawString(_bmipingfen[2].Split(',')[1], cellFont, brush, rect, sf);
                    //身高评价
                    rect = new Rectangle(currLeft + 120, currTop, 200, 25);
                    g.DrawString(_bmipingfen[2].Split(',')[2], cellFont, brush, rect, sf);
                    //身高等级
                    rect = new Rectangle(currLeft + 250, currTop, 200, 25);
                    g.DrawString(_bmipingfen[7].Split(',')[0], cellFont, brush, rect, sf);

                    currTop += 18;
                    currLeft = _rectBody.Left + 110;
                    //体重
                    rect = new Rectangle(currLeft, currTop, 60, 25);
                    g.DrawString(_bmipingfen[3].Split(',')[0], cellFont, brush, rect, sf);
                    //体重数值
                    rect = new Rectangle(currLeft + 60, currTop, 60, 25);
                    g.DrawString(_bmipingfen[3].Split(',')[1], cellFont, brush, rect, sf);
                    //体重评价
                    rect = new Rectangle(currLeft + 120, currTop, 60, 25);
                    g.DrawString(_bmipingfen[3].Split(',')[2], cellFont, brush, rect, sf);
                    //体重等级
                    rect = new Rectangle(currLeft + 250, currTop, 200, 25);
                    g.DrawString(_bmipingfen[7].Split(',')[1], cellFont, brush, rect, sf);

                    currTop += 18;
                    currLeft = _rectBody.Left + 110;
                    //头围
                    rect = new Rectangle(currLeft, currTop, 60, 25);
                    g.DrawString(_bmipingfen[4].Split(',')[0], cellFont, brush, rect, sf);
                    //头围数值
                    rect = new Rectangle(currLeft + 60, currTop, 60, 25);
                    g.DrawString(_bmipingfen[4].Split(',')[1], cellFont, brush, rect, sf);
                    //头围评价
                    rect = new Rectangle(currLeft + 120, currTop, 60, 25);
                    g.DrawString(_bmipingfen[4].Split(',')[2], cellFont, brush, rect, sf);

                    //头围等级
                    rect = new Rectangle(currLeft + 250, currTop, 200, 25);
                    g.DrawString(_bmipingfen[7].Split(',')[2], cellFont, brush, rect, sf);

                    currTop += 18;
                    currLeft = _rectBody.Left + 110;
                    //坐高
                    rect = new Rectangle(currLeft, currTop, 60, 25);
                    g.DrawString(_bmipingfen[6].Split(',')[0], cellFont, brush, rect, sf);
                    //坐高数值
                    rect = new Rectangle(currLeft + 60, currTop, 60, 25);
                    g.DrawString(_bmipingfen[6].Split(',')[1], cellFont, brush, rect, sf);


                    currTop += 18;
                    currLeft = _rectBody.Left + 30;
                    rect = new Rectangle(currLeft, currTop, 200, 25);
                    g.DrawString("匀称度：", cellFont, brush, rect, sf);
                    currLeft += 80;

                    //BMI
                    rect = new Rectangle(currLeft, currTop, 60, 25);
                    g.DrawString(" " + _bmipingfen[0].Substring(0, 4), cellFont, brush, rect, sf);
                    //BMI数值
                    rect = new Rectangle(currLeft + 60, currTop, 200, 25);
                    g.DrawString(_bmipingfen[0].Substring(4), cellFont, brush, rect, sf);
                    //BMI评价
                    rect = new Rectangle(currLeft + 120, currTop, 200, 25);
                    g.DrawString(_bmipingfen[1].Substring(6), cellFont, brush, rect, sf);

                    //BMI等级
                    rect = new Rectangle(currLeft + 250, currTop, 200, 25);
                    g.DrawString(_bmipingfen[7].Split(',')[3], cellFont, brush, rect, sf);

                    currTop += 18;
                    currLeft = _rectBody.Left + 110;
                    //体重/身高
                    rect = new Rectangle(currLeft - 38, currTop, 100, 25);
                    g.DrawString("体重/身高：", cellFont, brush, rect, sf);
                    //体重/身高评价
                    rect = new Rectangle(currLeft + 120, currTop, 80, 25);
                    g.DrawString(_bmipingfen[5].Substring(8), cellFont, brush, rect, sf);

                    //体重/身高等级
                    rect = new Rectangle(currLeft + 250, currTop, 200, 25);
                    g.DrawString(_bmipingfen[7].Split(',')[4], cellFont, brush, rect, sf);

                    currTop += 18;
                    currLeft = _rectBody.Left;

                    Rectangle rect2;
                    if (_checkobj.BIGSPORT != null && _checkobj.BIGSPORT.Trim() != "" && _checkobj.BIGSPORT.Trim() != "正常")
                    {
                        rect2 = new Rectangle(currLeft, currTop, _rectBody.Width, 25);
                        g.DrawString("  大 运 动：" + _checkobj.BIGSPORT, cellFont, brush, rect2, sf);

                        currTop += 18;
                    }
                    if (_checkobj.DONGZUO != null && _checkobj.DONGZUO.Trim() != "" && _checkobj.DONGZUO.Trim() != "正常")
                    {
                        rect2 = new Rectangle(currLeft, currTop, _rectBody.Width, 25);
                        g.DrawString("  精细动作：" + _checkobj.DONGZUO, cellFont, brush, rect2, sf);

                        currTop += 18;
                    }
                    if (_checkobj.LAGUAGE != null && _checkobj.LAGUAGE.Trim() != "" && _checkobj.LAGUAGE.Trim() != "正常")
                    {
                        rect2 = new Rectangle(currLeft, currTop, _rectBody.Width, 25);
                        g.DrawString("  语    言：" + _checkobj.LAGUAGE, cellFont, brush, rect2, sf);

                        currTop += 18;
                    }
                    if (_checkobj.SHEHUI != null && _checkobj.SHEHUI.Trim() != "" && _checkobj.SHEHUI.Trim() != "正常")
                    {
                        rect2 = new Rectangle(currLeft, currTop, _rectBody.Width, 25);
                        g.DrawString("个人与社会：" + _checkobj.SHEHUI, cellFont, brush, rect2, sf);

                        currTop += 18;
                    }
                    currLeft = _rectBody.Left;

                    jibenxinxihasprint = false;
                }
                #endregion 基本信息

                currTop += 4;
                if (wyhasprint)
                {

                    int height1 = (int)Math.Ceiling(g.MeasureString(_wyobj.WYZD, cellFont, _rectBody.Width).Height);
                    if (height1 == 0)
                    {
                        height1 = 30;
                    }

                    if (currTop + height1 < _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, height1);
                        g.DrawString(_wyobj.WYZD, cellFont, brush, rect);

                        wyhasprint = false;
                        currLeft = _rectBody.Left;
                        currTop += height1;

                    }
                    else if (currTop + height1 == _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, height1);
                        g.DrawString(_wyobj.WYZD, cellFont, brush, rect);

                        wyhasprint = false;

                        hasnextpage = true;
                        return;
                    }
                    else
                    {
                        string printstr = getPrintStr(_wyobj.WYZD, cellFont, g, currTop);
                        if (printstr == "")
                        {
                            hasnextpage = true;
                            return;
                        }

                        Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, _rectBody.Bottom - currTop);
                        g.DrawString(printstr, cellFont, brush, rect);

                        _wyobj.WYZD = _wyobj.WYZD.Replace(printstr, "");

                        hasnextpage = true;
                        return;
                    }
                }

                currTop += 4;
                if (zjhasprint)
                {

                    int height1 = (int)Math.Ceiling(g.MeasureString(_zdobj.ZJZD, cellFont, _rectBody.Width).Height);
                    if (height1 == 0)
                    {
                        height1 = 30;
                    }

                    if (currTop + height1 < _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, height1);
                        g.DrawString(_zdobj.ZJZD, cellFont, brush, rect);

                        zjhasprint = false;
                        currLeft = _rectBody.Left;
                        currTop += height1;

                    }
                    else if (currTop + height1 == _rectBody.Bottom)
                    {
                        Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, height1);
                        g.DrawString(_zdobj.ZJZD, cellFont, brush, rect);

                        zjhasprint = false;

                        hasnextpage = true;
                        return;
                    }
                    else
                    {
                        string printstr = getPrintStr(_zdobj.ZJZD, cellFont, g, currTop);
                        if (printstr == "")
                        {
                            hasnextpage = true;
                            return;
                        }

                        Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, _rectBody.Bottom - currTop);
                        g.DrawString(printstr, cellFont, brush, rect);

                        _zdobj.ZJZD = _zdobj.ZJZD.Replace(printstr, "");

                        hasnextpage = true;
                        return;
                    }
                }
                currTop += 4;
                //复诊提醒
                if (fjhasprint)
                {
                    for (int i = _fjIndex; i < _fjTextList.Count; i++)
                    {
                        var fjText = _fjTextList[_fjIndex];
                        int height1 = (int)Math.Ceiling(g.MeasureString(fjText, cellFont, _rectBody.Width).Height);
                        if (height1 == 0)
                        {
                            height1 = 30;
                        }

                        if (currTop + height1 < _rectBody.Bottom)
                        {
                            Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, height1);
                            g.DrawString(fjText, cellFont, brush, rect);

                            currLeft = _rectBody.Left;
                            currTop += height1;

                            if (_fjIndex < 2)
                            {
                                _fjIndex++;
                            }
                            else
                            {
                                fjhasprint = false;
                               
                            }


                        }
                        else if (currTop + height1 == _rectBody.Bottom)
                        {
                            Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, height1);
                            g.DrawString(fjText, cellFont, brush, rect);

                            if (_fjIndex < 2)
                            {
                                _fjIndex++;
                                hasnextpage = true;
                            }
                            else
                            {
                                fjhasprint = false;
                                hasnextpage = true;
                            }
                            return;
                        }
                        else
                        {
                            //string printstr = getPrintStr("     (请于"+_checkobj.fuzenday+"后再来复诊,下次复诊,请带此单。）\n儿保门诊预约方式：www.jkwin.com.cn(医事通)、微信预约、工行自助机挂号预约", cellFont, g, currTop);
                            //Rectangle rect = new Rectangle(currLeft, currTop, _rectBody.Width, _rectBody.Bottom - currTop);
                            //g.DrawString(printstr, cellFont, brush, rect);
                            hasnextpage = true;
                            return;
                        }
                    }
                    
                }
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
                pen.Width = 1.5F;
                g.DrawRectangle(pen, _rectTail);
                Rectangle rect = new Rectangle(_rectTail.Left, _rectTail.Top + 5, _rectTail.Width, _rectTail.Height);
                g.DrawString("医师：" + _checkobj.CK_FZ + "                   打印日期：" + _checkobj.CHECKDAY, new Font("宋体", 11f), brush, rect, sf);

            }
        }

        private void PrintStr(string strname, ref string str, ref int pleft, ref int ptop, Font cellFont, Font BcellFont, Brush brush, Graphics g, ref bool hasprint, ref bool hasnextpage)
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
                    string printstr = getPrintStr(str, cellFont, g, ptop);

                    Rectangle rect = new Rectangle(pleft, ptop, 60, 20);
                    g.DrawString(strname, BcellFont, brush, pleft, ptop);
                    pleft += 60;
                    rect = new Rectangle(pleft, ptop, _rectBody.Width - 70, _rectBody.Bottom - ptop);
                    g.DrawString(printstr, cellFont, brush, rect);

                    str = str.Replace(printstr, "");

                    hasnextpage = true;
                    return;
                }
            }
        }

        private string getPrintStr(string str, Font cellFont, Graphics g, int ptop)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0, j = 0; i < str.Length; i++)
            {
                if (g.MeasureString(str.Substring(j, i - j + 1), cellFont, _rectBody.Width).Height > _rectBody.Bottom - ptop)
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
