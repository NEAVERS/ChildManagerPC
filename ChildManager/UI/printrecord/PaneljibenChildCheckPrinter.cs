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

namespace ChildManager.UI.printrecord
{
    public partial class PaneljibenChildCheckPrinter : UserControl
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
        string[] _bmipingfen ;
        private List<PointF> _checkpointlist=null;

        public PaneljibenChildCheckPrinter(TB_CHILDBASE baseobj, TB_CHILDCHECK checkobj,int childid, string[] bmipingfen)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _checkobj = checkobj;
            _childid = childid;
            _bmipingfen = bmipingfen;
        }

        //打印孕妇分娩记录单
        public void Print(bool isPreview)
        {

            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "儿童健康检查表";

            ChildCheckBll bll = new ChildCheckBll();
            string sqls = string.Format("select * from TB_CHILDCHECK where childId  = "+_childid+"");
            //ChildCheckObj obj = bll.getChildCheckobj(sqls);
            //if (obj != null)
            //{
            //    _height =String.IsNullOrEmpty(obj.CheckHeight)? 0 : Convert.ToInt32(obj.CheckHeight);
            //    _weight = String.IsNullOrEmpty(obj.CheckWeight) ? 0 : Convert.ToInt32(obj.CheckWeight);
            //}
           // _checkpointlist = bll.getchildChectList(sqls);
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
            //Font fontTitle = new Font("宋体", 18f);
            //MiddleCenterPrintText("南京军区总医院" + _form.name, fontTitle, new SolidBrush(Color.Black), _rectHeader, g);
            //string projectName = "重  庆  医  科  大  学  附  属  第  一  医  院";
            string projectName = "儿 童 健 康 检 查 表";

            // string projectName = "重庆市荣昌区妇幼保健院";
            Rectangle rectProjectName = new Rectangle(_rectHeader.Left, _rectHeader.Top - 10, _rectHeader.Width, 45);
            // Rectangle rectProjectName = new Rectangle(30,30, _rectHeader.Width, 45);

            //Rectangle imagerect = new Rectangle(150, 30, image.Width, 45);
            //Rectangle imagerect = new Rectangle(_rectHeader.Left+120, _rectHeader.Top, image.Width, 45);
            //g.DrawImage(image, imagerect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);//画医院logo
            MiddleCenterPrintText(projectName, new Font("Arial Unicode MS", 19f), new SolidBrush(Color.Black), rectProjectName, g);//画医院名称
            //Rectangle imagerect = new Rectangle(100, 30, image.Width, 45);
            //g.DrawImage(image, imagerect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);
            //Rectangle english = new Rectangle(_rectHeader.Left, _rectHeader.Top + 10, _rectHeader.Width, 45);
            //MiddleCenterPrintText("DIANJIANG PEOPLE'S HOSPITAL OF CHONGQING", new Font("Arial Unicode MS", 9f, FontStyle.Bold), new SolidBrush(Color.Black), english, g);

           // g.DrawLine(new Pen(Color.Black, 1.65f), _rectHeader.Left + 70, _rectHeader.Top + 45, (_rectHeader.Left + _rectHeader.Width - 70), _rectHeader.Top + 45);//画线
            //Rectangle rectFormName = new Rectangle(_rectHeader.Left, _rectHeader.Top + 40, _rectHeader.Width, 45);
            //MiddleCenterPrintText("儿童保健健康检查表", new Font("Arial Unicode MS", 19f, FontStyle.Regular), new SolidBrush(Color.Black), rectFormName, g);
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
            int heugt = 20;

            int currTop = _rectHeader.Top+50;
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
                #region 基本信息

                //画产程开始矩形
                //Rectangle cckaishi = new Rectangle(currLeft, currTop, 255, 45);
                //g.DrawRectangle(pen, cckaishi);
                Rectangle rect = new Rectangle(currLeft, currTop, 40, 20);
                g.DrawString("姓名", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 40;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString(_baseobj.CHILDNAME, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                //出生日期
                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("出生日期", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                DateTime dt = Convert.ToDateTime(_baseobj.CHILDBIRTHDAY);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                //年
                g.DrawString(dt.Year.ToString(), new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 20, 20);
                //年
                g.DrawString("年", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 20;
                //月
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(dt.Month.ToString(), new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 20, 20);
                //月
                g.DrawString("月", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 20;
                //日
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(dt.Day.ToString(), new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 20, 20);
                //日
                g.DrawString("日", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 30;
                //年龄
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("实足年龄", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString(_checkobj.CHECKFACTAGE, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("初、复诊", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 20, 20);
                g.DrawString("第", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 20;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString(_baseobj.CS_FETUS, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 40, 20);
                g.DrawString("胎第", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 40;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString(_baseobj.CS_PRODUCE, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 20, 20);
                g.DrawString("产", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 40;
                rect = new Rectangle(currLeft, currTop, 600, 20);
                g.DrawString("孕期：足月、早产、过期、双胎 生产情况：顺、钳、剖、胎吸", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("出生体重", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString(_baseobj.BIRTHWEIGHT, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 40, 20);
                g.DrawString("公斤", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 200, 20);
                g.DrawString("男  女  城  乡  父亲文化程度", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 200;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString(_baseobj.FATHEREDUCATION, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 100, 20);
                g.DrawString("母亲文化程度", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 100;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString(_baseobj.MOTHEREDUCATION, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("父亲身高", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(_baseobj.FATHERHEIGHT, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 30, 20);
                g.DrawString("cm", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 40;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("母亲身高", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(_baseobj.MOTHERHEIGHT, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 30, 20);
                g.DrawString("cm", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 40;
                rect = new Rectangle(currLeft, currTop, 480, 20);
                g.DrawString("卡介苗\\脊髓灰质炎\\百白破\\、麻疹\\乙肝  1、2、3", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString("现体重", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(_checkobj.CHECKWEIGHT, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("kg   身长", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(_checkobj.CHECKHEIGHT, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("cm   头围", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(_checkobj.CHECKTOUWEI, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 80, 20);
                g.DrawString("cm   坐高", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 80;
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(_checkobj.CHECKZUOGAO, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 30, 20);
                g.DrawString("cm", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 360, 20);
                g.DrawString("喂养方法：母乳、人工、部分母乳          血色素：", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 360;
                rect = new Rectangle(currLeft, currTop, 60, 20);
                g.DrawString(_checkobj.BLOODSESU, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                g.DrawLine(pen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 30, 20);
                g.DrawString("g/L", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 60, 45);
                g.DrawString("主食：", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 300, 45);
                g.DrawString(_checkobj.ZHUSHI, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 300;
                rect = new Rectangle(currLeft, currTop, 120, 45);
                g.DrawString("补充VitD制剂：", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 300, 45);
                g.DrawString(_checkobj.VITD, new Font("Arial Unicode MS", 11f), brush, rect, sf);


                currLeft = _rectBody.Left;
                currTop += 50;
                rect = new Rectangle(currLeft, currTop, 60, 45);
                g.DrawString("辅食：", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 300, 45);
                g.DrawString(_checkobj.FUSHI, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 300;
                rect = new Rectangle(currLeft, currTop, 60, 45);
                g.DrawString("其他：", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 60;
                rect = new Rectangle(currLeft, currTop, 300, 45);
                g.DrawString(_checkobj.OTHERBINGSHI, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                #endregion

                currLeft = _rectBody.Left;
                currTop += 50;
                rect = new Rectangle(currLeft, currTop, 720, 25);
                g.DrawString(_bmipingfen[0], new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 25;
                rect = new Rectangle(currLeft, currTop, 720, 25);
                g.DrawString(_bmipingfen[1], new Font("Arial Unicode MS", 11f), brush, rect, sf);

                //绘图
                //Rectangle bmi = new Rectangle(currLeft + 10, currTop + 350, 765, 300);
                //g.DrawRectangle(pen, bmi);

                #region 绘图身高体重
                ////int h = 0;
                //for (int i = 0; i <= 22; i++)
                //{
                //    //h += 2;
                //    g.DrawLine(blackPen, currLeft + 50, currTop + heugt * i+230, currLeft + 50 + heugt * 24 + 220, currTop + heugt * i + 230);
                //    //for (int m = 0; m <= 10; m++)
                //    //{
                //    //    Rectangle drawHeightstr = new Rectangle(currLeft + 30, currTop + 10 + heugt * i + 230, 30, 30);
                //    //    g.DrawString(h.ToString(), new Font("Arial Unicode MS", 9f), brush, drawHeightstr, sf);
                //    //}
                //}
                //int j = 40;
                //for (int i = 0; i <= 14; i++)
                //{
                //    j += 5;
                //    g.DrawLine(blackPen, currLeft + 50 + 50 * i, currTop + 230, currLeft + 50 + 50 * i, currTop + 67 * 10);
                //    Rectangle draweightstr;
                //    if (i == 14)
                //    {
                //         draweightstr = new Rectangle(currLeft + 45 + 49 * i, currTop + 670, 30, 30);
                //         g.DrawString("", new Font("Arial Unicode MS", 9f), brush, draweightstr, sf);
                //    }
                //    else
                //    {
                //         draweightstr = new Rectangle(currLeft + 45 + 49 * i, currTop + 670, 30, 30);
                //         g.DrawString(j.ToString(), new Font("Arial Unicode MS", 9f), brush, draweightstr, sf);
                //    }
                //} 

                ////画左边框值
                //int setheight_i = 0;

                //Rectangle heightstr1 = new Rectangle(currLeft + 30, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("22", new Font("Arial Unicode MS", 9f), brush, heightstr1, sf);
                //Rectangle heightstr12 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("22", new Font("Arial Unicode MS", 9f), brush, heightstr12, sf);
                //setheight_i +=2;

                //Rectangle heightstr2 = new Rectangle(currLeft + 30, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("20", new Font("Arial Unicode MS", 9f), brush, heightstr2, sf);
                //Rectangle heightstr22 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("20", new Font("Arial Unicode MS", 9f), brush, heightstr22, sf);
                //setheight_i +=2;

                //Rectangle heightstr3 = new Rectangle(currLeft + 30, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("18", new Font("Arial Unicode MS", 9f), brush, heightstr3, sf);
                //Rectangle heightstr33 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("18", new Font("Arial Unicode MS", 9f), brush, heightstr33, sf);
                //setheight_i += 2;

                //Rectangle heightstr4 = new Rectangle(currLeft + 30, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("16", new Font("Arial Unicode MS", 9f), brush, heightstr4, sf);
                //Rectangle heightstr44 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("16", new Font("Arial Unicode MS", 9f), brush, heightstr44, sf);
                //setheight_i += 2;

                //Rectangle heightstr5 = new Rectangle(currLeft + 30, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("14", new Font("Arial Unicode MS", 9f), brush, heightstr5, sf);
                //Rectangle heightstr55 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("14", new Font("Arial Unicode MS", 9f), brush, heightstr55, sf);
                //setheight_i += 2;

                //Rectangle heightstr6 = new Rectangle(currLeft + 30, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("12", new Font("Arial Unicode MS", 9f), brush, heightstr6, sf);
                //Rectangle heightstr66 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("12", new Font("Arial Unicode MS", 9f), brush, heightstr66, sf);
                //setheight_i += 2;

                //Rectangle heightstr7 = new Rectangle(currLeft + 30, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("10", new Font("Arial Unicode MS", 9f), brush, heightstr7, sf);
                //Rectangle heightstr77 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("10", new Font("Arial Unicode MS", 9f), brush, heightstr77, sf);
                //setheight_i += 2;

                //Rectangle heightstr8 = new Rectangle(currLeft + 35, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("8", new Font("Arial Unicode MS", 9f), brush, heightstr8, sf);
                //Rectangle heightstr88 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("8", new Font("Arial Unicode MS", 9f), brush, heightstr88, sf);
                //setheight_i += 2;

                //Rectangle heightstr9 = new Rectangle(currLeft + 35, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("6", new Font("Arial Unicode MS", 9f), brush, heightstr9, sf);
                //Rectangle heightstr99 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("6", new Font("Arial Unicode MS", 9f), brush, heightstr99, sf);
                //setheight_i += 2;

                //Rectangle heightstr10 = new Rectangle(currLeft + 35, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("4", new Font("Arial Unicode MS", 9f), brush, heightstr10, sf);
                //Rectangle heightstr101 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("4", new Font("Arial Unicode MS", 9f), brush, heightstr101, sf);
                //setheight_i += 2;

                //Rectangle heightstr11 = new Rectangle(currLeft + 35, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("2", new Font("Arial Unicode MS", 9f), brush, heightstr11, sf);
                //Rectangle heightstr111 = new Rectangle(currLeft + 750, currTop + 240 + heugt * setheight_i, 20, 30);
                //g.DrawString("2", new Font("Arial Unicode MS", 9f), brush, heightstr111, sf);
                //setheight_i += 2;

                ////体重kg
                //Rectangle leftNumber = new Rectangle(currLeft + 10, currTop + 380, 30, 30);
                //Rectangle leftNumber1 = new Rectangle(currLeft + 10, currTop + 410, 30, 30);
                //Rectangle leftNumber2 = new Rectangle(currLeft + 10, currTop + 440, 30, 30);
                //Rectangle leftNumber3 = new Rectangle(currLeft + 10, currTop + 470, 30, 30);
                //Rectangle leftNumber4 = new Rectangle(currLeft, currTop + 500, 40, 30);
                //g.DrawString("体", new Font("Arial Unicode MS", 10f), brush, leftNumber,sf);
                //g.DrawString("重", new Font("Arial Unicode MS", 10f), brush, leftNumber1, sf);
                //g.DrawString("增", new Font("Arial Unicode MS", 10f), brush, leftNumber2, sf);
                //g.DrawString("长", new Font("Arial Unicode MS", 10f), brush, leftNumber3, sf);
                //g.DrawString("(Kg)", new Font("Arial Unicode MS", 10f), brush, leftNumber4, sf);

                ////身长
                //Rectangle shenchang = new Rectangle(currLeft + 740, currTop + 670, 80, 30);
                //g.DrawString("身长(cm)", new Font("Arial Unicode MS", 10f), brush, shenchang, sf);


                ////定义黑色过渡型笔刷
                ////LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 912, 449), Color.Black, Color.Black, 1.2F, true);
                ////定义蓝色过渡型笔刷
                //LinearGradientBrush Greenbrush = new LinearGradientBrush(new Rectangle(0, 0, 912, 449), Color.Green, Color.Green, 1.0F, true);
                ////定义红色过渡型笔刷
                //LinearGradientBrush Redbrush = new LinearGradientBrush(new Rectangle(0, 0, 912, 449), Color.Red, Color.Red, 1.3F, true);
                ////定义浅黄色过度笔刷
                //LinearGradientBrush LightGreen = new LinearGradientBrush(new Rectangle(0, 0, 912, 449), Color.LightGreen, Color.LightGreen, 1.0F, true);
                ////LinearGradientBrush LightGreen = new LinearGradientBrush(new Rectangle(0, 0, 912, 449), Color.FromArgb(102, 205, 170), Color.FromArgb(102, 205, 170), 1.2F, true);
                ////定义绿色过度笔刷
                //LinearGradientBrush green1brush = new LinearGradientBrush(new Rectangle(0, 0, 912, 449), Color.FromArgb(127, 255, 0), Color.FromArgb(127, 255, 0), 1.2F, true);

                ////SandyBrownPen
                //LinearGradientBrush SandyBrownbrush = new LinearGradientBrush(new Rectangle(0, 0, 912, 449), Color.SandyBrown, Color.SandyBrown, 1.3F, true);

                ////LightSeaGreen
                //LinearGradientBrush LightSeaGreenbrush = new LinearGradientBrush(new Rectangle(0, 0, 912, 449), Color.LightSeaGreen, Color.LightSeaGreen, 1.3F, true);

                ////绘制曲线图线条
                //List<Point>threepointlist = new List<Point>();//曲线图3rd
                //List<Point> fivepointlist = new List<Point>();//曲线图15th
                //List<Point> fivethpointlist = new List<Point>();//曲线图 50th
                //List<Point> eightfivelist = new List<Point>();//曲线图 85th
                //List<Point> nightsenvenlist = new List<Point>();//曲线图 95th

                //Point threestr = new Point(0, 0);
                //Point fivestr = new Point(0, 0);
                //Point fivethstr = new Point(0, 0);
                //Point eightfivestr = new Point(0, 0);
                //Point nightsenven = new Point(0,0);
                //Point pinfang = new Point(0, 0);
                //string titlestr1 = "";
                //string titlestr2 = "";

                //#region 取值判断
                //if (_height > 110 && _weight > 14 || _weight < 16)
                //{
                //    //titlestr1 = "3rd";
                //    //titlestr2 = "体重过轻,建议孕期体重增长目标:12.5-18kg";

                //    threepointlist.Add(new Point(64, 810));
                //    threepointlist.Add(new Point(415, 740));
                //    threepointlist.Add(new Point(716, 535));
                //    threestr.X = 720;
                //    threestr.Y = 530;

                //    fivepointlist.Add(new Point(64, 805));
                //    fivepointlist.Add(new Point(385, 725));
                //    fivepointlist.Add(new Point(715, 515));
                //    fivestr.X = 720;
                //    fivestr.Y = 502;

                //    //
                //    fivethpointlist.Add(new Point(64, 800));
                //    fivethpointlist.Add(new Point(405, 705));
                //    fivethpointlist.Add(new Point(715, 490));
                //    fivethstr.X = 718;
                //    fivethstr.Y = 480;

                //    //
                //    eightfivelist.Add(new Point(64, 795));
                //    eightfivelist.Add(new Point(425, 645));
                //    eightfivelist.Add(new Point(716, 450));
                //    eightfivestr.X = 718;
                //    eightfivestr.Y = 440;

                //    nightsenvenlist.Add(new Point(64, 790));
                //    nightsenvenlist.Add(new Point(425, 625));
                //    nightsenvenlist.Add(new Point(717, 410));
                //    nightsenven.X = 718;
                //    nightsenven.Y = 405;
                //}
                //else if (_height >= 110 && _weight > 16 || _weight < 18)
                //{
                //    threepointlist.Add(new Point(64, 810));
                //    threepointlist.Add(new Point(415, 740));
                //    threepointlist.Add(new Point(716, 535));
                //    threestr.X = 720;
                //    threestr.Y = 530;

                //    fivepointlist.Add(new Point(64, 805));
                //    fivepointlist.Add(new Point(385, 725));
                //    fivepointlist.Add(new Point(715, 515));
                //    fivestr.X = 720;
                //    fivestr.Y = 502;

                //    //
                //    fivethpointlist.Add(new Point(64, 800));
                //    fivethpointlist.Add(new Point(405, 705));
                //    fivethpointlist.Add(new Point(715, 490));
                //    fivethstr.X = 718;
                //    fivethstr.Y = 480;

                //    //
                //    eightfivelist.Add(new Point(64, 795));
                //    eightfivelist.Add(new Point(425, 645));
                //    eightfivelist.Add(new Point(716, 450));
                //    eightfivestr.X = 718;
                //    eightfivestr.Y = 440;

                //    nightsenvenlist.Add(new Point(64, 790));
                //    nightsenvenlist.Add(new Point(425, 625));
                //    nightsenvenlist.Add(new Point(717, 410));
                //    nightsenven.X = 718;
                //    nightsenven.Y = 405;
                //}

                //else if (_height >= 110 && _weight > 18 || _weight < 20)
                //{
                //    threepointlist.Add(new Point(64, 810));
                //    threepointlist.Add(new Point(415, 740));
                //    threepointlist.Add(new Point(716, 535));
                //    threestr.X = 720;
                //    threestr.Y = 530;

                //    fivepointlist.Add(new Point(64, 805));
                //    fivepointlist.Add(new Point(385, 725));
                //    fivepointlist.Add(new Point(715, 515));
                //    fivestr.X = 720;
                //    fivestr.Y = 502;

                //    //
                //    fivethpointlist.Add(new Point(64, 800));
                //    fivethpointlist.Add(new Point(405, 705));
                //    fivethpointlist.Add(new Point(715, 490));
                //    fivethstr.X = 718;
                //    fivethstr.Y = 480;

                //    //
                //    eightfivelist.Add(new Point(64, 795));
                //    eightfivelist.Add(new Point(425, 645));
                //    eightfivelist.Add(new Point(716, 450));
                //    eightfivestr.X = 718;
                //    eightfivestr.Y = 440;

                //    nightsenvenlist.Add(new Point(64, 790));
                //    nightsenvenlist.Add(new Point(425, 625));
                //    nightsenvenlist.Add(new Point(717, 410));
                //    nightsenven.X = 718;
                //    nightsenven.Y = 405;
                //}
                //else if (_height >= 110 && _weight >= 22)
                //{
                //    threepointlist.Add(new Point(64, 810));
                //    threepointlist.Add(new Point(415, 740));
                //    threepointlist.Add(new Point(716, 535));
                //    threestr.X = 720;
                //    threestr.Y = 530;

                //    fivepointlist.Add(new Point(64, 805));
                //    fivepointlist.Add(new Point(385, 725));
                //    fivepointlist.Add(new Point(715, 515));
                //    fivestr.X = 720;
                //    fivestr.Y = 502;
                //    //
                //    fivethpointlist.Add(new Point(64, 800));
                //    fivethpointlist.Add(new Point(405, 705));
                //    fivethpointlist.Add(new Point(715, 490));
                //    fivethstr.X = 718;
                //    fivethstr.Y = 480;
                //    //
                //    eightfivelist.Add(new Point(64, 795));
                //    eightfivelist.Add(new Point(425, 645));
                //    eightfivelist.Add(new Point(716, 450));
                //    eightfivestr.X = 718;
                //    eightfivestr.Y = 440;

                //    nightsenvenlist.Add(new Point(64, 790));
                //    nightsenvenlist.Add(new Point(425, 625));
                //    nightsenvenlist.Add(new Point(717, 410));
                //    nightsenven.X = 718;
                //    nightsenven.Y = 405;
                //}
                //#endregion

                ////Point threestr = new Point(0, 0);
                ////Point fivestr = new Point(0, 0);
                ////Point fivethstr = new Point(0, 0);
                ////Point eightfivestr = new Point(0, 0);
                ////Point nightsenven = new Point(0, 0);

                ////using (Pen blackPen = new Pen(Color.Black))
                ////using (Pen redPen = new Pen(Color.Red))//红画笔
                ////using (Pen plumPen = new Pen(Color.Plum))//紫色画笔
                ////using (Pen bluePen = new Pen(Color.Blue))//蓝色画笔


                //g.DrawLines(redPen, threepointlist.ToArray());
                //g.DrawString("3rd", Font, Redbrush, threestr.X, threestr.Y);

                //g.DrawLines(SandyBrownPen, fivepointlist.ToArray());
                //g.DrawString("15th", Font, SandyBrownbrush, fivestr.X, fivestr.Y);

                //g.DrawLines(LightSeaGreenPen, fivethpointlist.ToArray());
                //g.DrawString("50th", Font, LightSeaGreenbrush, fivethstr.X, fivethstr.Y);

                //g.DrawLines(SandyBrownPen, eightfivelist.ToArray());
                //g.DrawString("85th", Font, SandyBrownbrush, eightfivestr.X, eightfivestr.Y);

                //g.DrawLines(redPen, nightsenvenlist.ToArray());
                //g.DrawString("97th", Font, Redbrush, nightsenven.X, nightsenven.Y);

                ////绘制实际的值点
                //foreach (PointF pointf in _checkpointlist)
                //{
                //    g.FillEllipse(Redbrush, pointf.X, pointf.Y, 4, 4);

                //}
                //List<PointF> newpointlist = new List<PointF>();
                //foreach (PointF pointf in _checkpointlist)
                //{
                //    PointF newpointf = new PointF();
                //    newpointf.X = pointf.X + 4;
                //    newpointf.Y = pointf.Y + 4;
                //    newpointlist.Add(newpointf);
                //}
                //if (newpointlist.Count > 1)
                //{
                //    g.DrawLines(blackPen, newpointlist.ToArray());
                //}

                #endregion

                #region 检查信息
                #region 第一行
                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("个人与社会", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.SHEHUI, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("五官", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.WUGUAN, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("脾", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.PIZANG, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                #endregion

                #region 第二行
                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("精细动作", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.DONGZUO, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("皮肤", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.SKIN, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("脊柱", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.JIZHU, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                #endregion  

                #region 第三行
                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("语言", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.LAGUAGE, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("淋巴结", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.LINGBAJIE, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("手足镯", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.JIZHU, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                #endregion

                #region 第四行
                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("大运动", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.BIGSPORT, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("胸廓", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.XIONGBU, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("四肢", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.SIZHI, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                #endregion

                #region 第五行
                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("前囟", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.CHECKQIANLU, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("心", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.XINZANG, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("\"OX\"腿", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                //g.DrawString(, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                #endregion

                #region 第六行
                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("头颅", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.TOULU, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("肺", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.FEIBU, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("隐睾", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                //g.DrawString(_checkobj., new Font("Arial Unicode MS", 11f), brush, rect, sf);
                #endregion

                #region 第七行
                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("牙齿", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.YACINUMBER, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("腹部", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.FUBU, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("泌尿生殖器", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.MINIAOQI, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                #endregion

                #region 第八行
                currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("龋齿", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.YUCINUMBER, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("肝", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.GANZANG, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString("其他", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 120;
                rect = new Rectangle(currLeft, currTop, 120, 30);
                g.DrawRectangle(pen, rect);
                g.DrawString(_checkobj.OTHERBINGSHI, new Font("Arial Unicode MS", 11f), brush, rect, sf);
                #endregion
                #endregion

                currLeft = _rectBody.Left;
                currTop += 40;

                rect = new Rectangle(currLeft, currTop, 250, 30);
                g.DrawString("体检时间："+Convert.ToDateTime(_checkobj.CHECKDAY).ToString("yyyy-MM-dd"), new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 200;
                rect = new Rectangle(currLeft, currTop, 600, 70);
                g.DrawString("诊断/结论：" + _checkobj.ZHENDUAN, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 70;

                //处理
                rect = new Rectangle(currLeft, currTop, 100, 20);
                g.DrawString("处理意见：", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 100;
                rect = new Rectangle(currLeft, currTop, 700, 70);
                g.DrawString( _checkobj.CHULI, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 70;
                rect = new Rectangle(currLeft, currTop, 240, 180);
                g.DrawString("营养指导："+ _checkobj.NUERZHIDAO, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 235;
                rect = new Rectangle(currLeft, currTop, 240, 180);
                g.DrawString("行为习惯培养："+_checkobj.CHECKDIAGNOSE, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 235;
                rect = new Rectangle(currLeft, currTop, 240, 180);
                g.DrawString("早期综合发展：" + _checkobj.ZONGHEFAZHAN, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft = _rectBody.Left;
                currTop += 180;

                Image image = Image.FromFile(Application.StartupPath + "\\erweima.png");
                Rectangle imagerect = new Rectangle(currLeft, currTop, 140, 110);
                g.DrawImage(image, imagerect, 0, 0, image.Width, image.Height, System.Drawing.GraphicsUnit.Pixel);//画医院logo
                //rect = new Rectangle(currLeft, currTop, 200, 150);
                //g.DrawString("联系电话：49589232", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currLeft += 450;
                rect = new Rectangle(currLeft, currTop, 280, 150);
                g.DrawString("医师签字：", new Font("Arial Unicode MS", 11f), brush, rect, sf);

                //currLeft = _rectBody.Left;
                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 720, 150);
                g.DrawString("复诊日期："+_checkobj.FUZENDAY, new Font("Arial Unicode MS", 11f), brush, rect, sf);

                currTop += 30;
                rect = new Rectangle(currLeft, currTop, 720, 150);
                g.DrawString("重医大附属儿童医院", new Font("Arial Unicode MS", 11f), brush, rect, sf);


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
