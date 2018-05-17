using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.Model.ChildBaseInfo;
using YCF.Common;
using ChildManager.BLL;
using ChildManager.Model;
using login.UI.printrecord;
using ChildManager.Properties;
using YCF.Model;
using YCF.BLL;

namespace ChildManager.UI.jibenluru
{
    public partial class tongji_BMI519 : UserControl
    {
        private float bmlval = 0.0f;
        List<PointF> fuchapointlist = null;
        ChildStandardBll standbll = new ChildStandardBll();
        ChildCheckBll checkbll = new ChildCheckBll();
        private ChildBaseInfoBll bll = new ChildBaseInfoBll();
        TB_CHILDBASE jibenobj = null;
        private string _sex = "男";
        private string _birthtime = "";
        private string _week = "";
        private string _days = "";
        private string _ispre = "";
        private string _height = "";
        private string _weight = "";
        WomenInfo _womeninfo = null;
        public tongji_BMI519(WomenInfo womeninfo)
        {
            InitializeComponent();
            _womeninfo = womeninfo;
            jibenobj = new tb_childbasebll().Get(_womeninfo.cd_id);
            _sex = jibenobj.CHILDGENDER;
            if (_sex == "性别不明")
            {
                _sex = "男";
            }
            _birthtime = jibenobj.CHILDBIRTHDAY;
            _ispre = jibenobj.ISPRE;
            if (!String.IsNullOrEmpty(_ispre) && _ispre == "1")
            {
                ckb_isPre.Checked = true;
            }
            _week = jibenobj.CS_WEEK;
            _days = jibenobj.CS_DAY;
            _weight = jibenobj.BIRTHWEIGHT;
            _height = jibenobj.BIRTHHEIGHT;
            tongji_WHO_Paint519();

        }


        # region WHO
        private void tongji_WHO_Paint519()
        {
            # region 属性设置
            int left = 60;
            int top = 30;
            int width = 8;
            int hight = 8;

            int wd = 1600;
            //生成Bitmap对像
            Bitmap img = new Bitmap(wd, 1200);
            //生成绘图对像
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // 消锯齿（可选项）
            //定义黑色画笔
            Pen Bp = new Pen(Color.Black);
            //早产儿 颜色
            Pen Prep = new Pen(Color.Teal);
            Pen basepen = new Pen(Color.DarkSlateBlue);

            //定义蓝色色画笔
            Pen Bluepen = new Pen(Color.Blue);
            //定义红色画笔
            Pen Rp = new Pen(Color.Red);
            //定义银灰色画笔
            Pen Sp = new Pen(Color.Silver);
            //定义银灰色画笔
            Pen BSp = new Pen(Color.Gray);
            //定义大标题字体
            Font Bfont = new Font("宋体", 12, FontStyle.Bold);
            //定义一般字体
            Font font = new Font("Arial", 6f);
            //定义大点的字体
            Font Tfont = new Font("宋体", 9, FontStyle.Bold);
            //定义大点的加粗字体
            Font TBfont = new Font("宋体", 9, FontStyle.Bold);
            //定义加大字体
            Font BBfont = new Font("宋体", 10);

            //绘制底色
            g.DrawRectangle(new Pen(Color.White, 1200), 0, 0, img.Width, img.Height);
            //定义黑色过渡型笔刷
            Brush bbrush = new SolidBrush(Color.Black);

            Brush prebrush = new SolidBrush(Color.Teal);

            Brush basebrush = new SolidBrush(Color.DarkSlateBlue);

            Brush brush = new SolidBrush(Color.Blue);

            if (_sex == "女")
            {
                Bluepen = new Pen(Color.Red);
                brush = new SolidBrush(Color.Red);
            }
            //定义蓝色过渡型笔刷
            LinearGradientBrush Bluebrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.Blue, 1.2F, true);
            //定义红色过渡型笔刷
            LinearGradientBrush Redbrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Red, Color.Red, 1.2F, true);
            //定义蓝色过度笔刷
            LinearGradientBrush greenbrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.FromArgb(102, 205, 170), Color.FromArgb(102, 205, 170), 1.2F, true);
            //定义绿色过度笔刷
            LinearGradientBrush green1brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.FromArgb(127, 255, 0), Color.FromArgb(127, 255, 0), 1.2F, true);
            //定义黄色过度笔刷
            LinearGradientBrush orangebrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.FromArgb(127, 255, 0), Color.Orange, 1.2F, true);
            # endregion 属性设置



            # region 体重底板坐标宽高
            int w_left = left;
            int w_width = 8;
            int w_hight = 8;
            int w_top = top + hight * 44;
            #endregion 体重底板坐标宽高

            # region 体重底板
            //绘制大标题
            //Rectangle rect = new Rectangle(left, top - 20,width*60,30);
            g.DrawString("（WHO）5~10" + _sex + "童体重曲线图", BBfont, brush, w_left + w_width * 20, w_top - 20);
            //string info = "曲线图生成时间："+DateTime.Now.ToString();
            //g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //绘制图片边框
            //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            ////绘制竖坐标线       
            for (int i = 60; i <= 120; i++)
            {
                if (i % 12 == 0)
                {
                    //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                    g.DrawLine(Bluepen, w_left + w_width * (i - 60), w_top, w_left + w_width * (i - 60), w_top + 35 * hight);
                    g.DrawString((i / 12).ToString() + "岁", font, brush, w_left + w_width * (i - 60) - w_width / 2, w_top + 35 * hight);
                }
                else
                {
                    g.DrawLine(Sp, w_left + w_width * (i - 60), w_top, w_left + w_width * (i - 60), w_top + 34 * hight);
                    g.DrawString((i % 12).ToString(), font, brush, w_left + w_width * (i - 60) - w_width / 2, w_top + 34 * hight);
                }
            }
            //绘制横坐标线
            for (int i = 13; i <= 47; i++)
            {
                if ((60 - i) % 5 != 0)
                {
                    g.DrawLine(Sp, w_left, w_top + hight * (i - 13), w_left + w_width * 60, w_top + hight * (i - 13));
                }
                else
                {
                    g.DrawLine(Bluepen, w_left, w_top + hight * (i - 13), w_left + w_width * 62, w_top + hight * (i - 13));
                    g.DrawString((60 - i).ToString(), font, brush, w_left - w_width-5, w_top + hight * (i - 13));
                    g.DrawString((60 - i).ToString(), font, brush, w_left + w_width * 62 + 5, w_top + hight * (i - 13));
                }
                if (i == 13 || i == 47)
                {
                    g.DrawLine(Bluepen, w_left, w_top + hight * (i - 13), w_left + w_width * 62, w_top + hight * (i - 13));
                }
            }
            g.DrawLine(Bluepen, w_left + w_width * 62, w_top, w_left + w_width * 62, w_top + hight * 34);

            #endregion 体重底板

            #region 体重参考曲线
            Pen a = new Pen(Color.DarkRed);
            Pen b = new Pen(Color.Orange);
            Pen c = new Pen(Color.Green);

            List<Point> w_pointlist3 = new List<Point>();//曲线图上限点
            List<Point> w_pointlist15 = new List<Point>();//
            List<Point> w_pointlist50 = new List<Point>();//
            List<Point> w_pointlist85 = new List<Point>();//
            List<Point> w_pointlist97 = new List<Point>();//

            string sqls_stand = "select * from who_childstand where sex='" + _sex + "' and month >'60'and month <='120' and ptype='体重' order by month";
            ArrayList standlist = standbll.getwhoStandard_list(sqls_stand);

            foreach (WhoChildStandardObj standobj in standlist)
            {
                
                Point w_pointstr3 = new Point(w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p3)) * hight));
                w_pointlist3.Add(w_pointstr3);
                Point w_pointstr15 = new Point(w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p15)) * hight));
                w_pointlist15.Add(w_pointstr15);
                Point w_pointstr50 = new Point(w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p50)) * hight));
                w_pointlist50.Add(w_pointstr50);
                Point w_pointstr85 = new Point(w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p85)) * hight));
                w_pointlist85.Add(w_pointstr85);
                Point w_pointstr97 = new Point(w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p97)) * hight));
                w_pointlist97.Add(w_pointstr97);
                if ((standobj.month-60) == 60)
                {
                    g.DrawString("3", font, brush, w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p3)) * hight));
                    g.DrawString("15", font, brush, w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p15)) * hight));
                    g.DrawString("50", font, brush, w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p50)) * hight));
                    g.DrawString("85", font, brush, w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p85)) * hight));
                    g.DrawString("97", font, brush, w_left + (standobj.month-60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p97)) * hight));
                }
            }

            if (w_pointlist3.Count > 1)
            {
                g.DrawLines(a, w_pointlist3.ToArray());
            }

            if (w_pointlist15.Count > 1)
            {
                g.DrawLines(b, w_pointlist15.ToArray());
            }

            if (w_pointlist50.Count > 1)
            {
                g.DrawLines(c, w_pointlist50.ToArray());
            }

            if (w_pointlist85.Count > 1)
            {
                g.DrawLines(b, w_pointlist85.ToArray());
            }

            if (w_pointlist97.Count > 1)
            {
                g.DrawLines(a, w_pointlist97.ToArray());
            }

            #endregion 参考曲线

            #region 身高底板坐标宽高
            int h_left = left;
            int h_width = 3;
            int h_hight = 3;
            int h_top = top;
            #endregion 身高底板坐标宽高

            # region 身高底板
            //绘制大标题
            g.DrawString("（WHO）5~19" + _sex + "童身长/身高曲线图", BBfont, brush, h_left + 6 * h_width+5, h_top - 20);
            //string info = "曲线图生成时间："+DateTime.Now.ToString();
            //g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //绘制图片边框
            //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            ////绘制竖坐标线       
            for (int i = 60; i <= 228; i++)
            {
                if ((i-60) % 12 == 0)
                {
                    //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                    g.DrawLine(Bluepen, h_left + h_width * (i-60), h_top, h_left + h_width * (i - 60), h_top + 102 * h_hight);
                    g.DrawString((i/12).ToString() + "岁", font, brush, h_left + h_width * (i - 60) - h_width / 2, h_top + 102 * h_hight);
                }
                else if ((i - 60) % 3 == 0)
                {
                    g.DrawLine(Sp, h_left + h_width * (i - 60), h_top, h_left + h_width * (i - 60), h_top + 100 * h_hight);
                    g.DrawString(((i - 60) % 12).ToString(), font, brush, h_left + h_width * (i - 60) - h_width / 2, h_top + 100 * h_hight);
                }
            }
            //绘制横坐标线
            for (int i = 95; i <= 195; i++)
            {
                if (i == 95 || i == 195)
                {
                    g.DrawLine(Bluepen, h_left, h_top + h_hight * (i - 95), h_left + h_width * 172, h_top + h_hight * (i - 95));
                }
                else if (i % 10 == 0)
                {
                    g.DrawLine(Bluepen, h_left, h_top + h_hight * (i - 95), h_left + h_width * 172, h_top + h_hight * (i - 95));
                    g.DrawString((290-i).ToString(), font, brush, h_left - h_width * 5, h_top + h_hight * (i - 95));
                    g.DrawString((290-i).ToString(), font, brush, h_left + h_width * 174, h_top + h_hight * (i - 95));

                }
                else if (i % 5 == 0)
                {
                    g.DrawLine(Sp, h_left, h_top + h_hight * (i-95), h_left + h_width * 168, h_top + h_hight * (i - 95));
                }

                
            }
            g.DrawLine(Bluepen, h_left + h_width * 172, h_top, h_left + h_width * 172, h_top + h_hight * 100);
            # endregion 身高底板

            # region 身高参考曲线

            List<Point> h_pointlist3 = new List<Point>();//曲线图上限点
            List<Point> h_pointlist15 = new List<Point>();//
            List<Point> h_pointlist50 = new List<Point>();//
            List<Point> h_pointlist85 = new List<Point>();//
            List<Point> h_pointlist97 = new List<Point>();//

            string h_sqls_stand = "select * from who_childstand where sex='" + _sex + "' and ptype='身高' and month>'60' and month <= '228' order by month";
            ArrayList h_standlist = standbll.getwhoStandard_list(h_sqls_stand);

            foreach (WhoChildStandardObj standobj in h_standlist)
            {
                Point h_pointstr3 = new Point(h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p3)) * h_hight));
                h_pointlist3.Add(h_pointstr3);
                Point h_pointstr15 = new Point(h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p15)) * h_hight));
                h_pointlist15.Add(h_pointstr15);
                Point h_pointstr50 = new Point(h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p50)) * h_hight));
                h_pointlist50.Add(h_pointstr50);
                Point h_pointstr85 = new Point(h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p85)) * h_hight));
                h_pointlist85.Add(h_pointstr85);
                Point h_pointstr97 = new Point(h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p97)) * h_hight));
                h_pointlist97.Add(h_pointstr97);
                if (standobj.month == 228)
                {
                    g.DrawString("3", font, brush, h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p3)) * h_hight));
                    g.DrawString("15", font, brush, h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p15)) * h_hight));
                    g.DrawString("50", font, brush, h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p50)) * h_hight));
                    g.DrawString("85", font, brush, h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p85)) * h_hight));
                    g.DrawString("97", font, brush, h_left + (standobj.month - 60) * h_width, h_top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p97)) * h_hight));
                }
            }

            if (h_pointlist3.Count > 1)
            {
                g.DrawLines(a, h_pointlist3.ToArray());
            }

            if (h_pointlist15.Count > 1)
            {
                g.DrawLines(b, h_pointlist15.ToArray());
            }

            if (h_pointlist50.Count > 1)
            {
                g.DrawLines(c, h_pointlist50.ToArray());
            }

            if (h_pointlist85.Count > 1)
            {
                g.DrawLines(b, h_pointlist85.ToArray());
            }

            if (h_pointlist97.Count > 1)
            {
                g.DrawLines(a, h_pointlist97.ToArray());
            }

            # endregion 参考曲线

            #region BMI底板坐标宽高
            int bmi_left = left + width * 70;
            int bmi_top = top;
            int bmi_width = 3;
            int bmi_hight = 2;
            #endregion BMI底板坐标宽高

            #region BMI底板
            //绘制大标题
            g.DrawString("（WHO）5~19" + _sex + "童BMI曲线图", BBfont, brush, bmi_left + 6 * width, bmi_top - 20);
            //string info = "曲线图生成时间："+DateTime.Now.ToString();
            //g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //绘制图片边框
            //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            ////绘制竖坐标线       
            for (int i = 60; i <= 228; i++)
            {
                if ((i - 60) % 12 == 0)
                {
                    //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                    g.DrawLine(Bluepen, bmi_left + bmi_width * (i - 60), bmi_top, bmi_left + bmi_width * (i - 60), bmi_top + (180 + 3) * bmi_hight);
                    g.DrawString((i / 12).ToString() + "岁", font, brush, bmi_left + bmi_width * (i - 61) - bmi_width / 2, bmi_top + (180 + 3) * bmi_hight);
                }
                else if((i - 60) % 3 == 0)
                {
                    g.DrawLine(Sp, bmi_left + bmi_width * (i - 60), bmi_top, bmi_left + bmi_width * (i - 60), bmi_top + 180 * bmi_hight);
                    g.DrawString((i % 12).ToString(), font, brush, bmi_left + bmi_width * (i - 61) - bmi_width / 2, bmi_top + 180 * bmi_hight);
                }
            }
            //绘制横坐标线
            for (int i = 120; i <= 300; i++)
            {
                if ((i - 120) % 10 == 0)
                {
                    if (((i - 120)/10)%2==0) {
                        g.DrawLine(Bluepen, bmi_left, bmi_top + bmi_hight * (i - 120), bmi_left + bmi_width * 172, bmi_top + bmi_hight * (i - 120));
                        g.DrawString(((420 - i) / 10).ToString(), font, brush, bmi_left - bmi_width * 4, bmi_top + bmi_hight * (i - 121));
                        g.DrawString(((420 - i) / 10).ToString(), font, brush, bmi_left + bmi_width * 172, bmi_top + bmi_hight * (i - 120));
                    }
                    else
                    {
                        g.DrawLine(Sp, bmi_left, bmi_top + bmi_hight * (i - 120), bmi_left + bmi_width * 168, bmi_top + bmi_hight * (i - 120));
                    }

                }
                else if ((i - 120) % 5 == 0)
                {
                    g.DrawLine(Sp, bmi_left, bmi_top + bmi_hight * (i - 120), bmi_left + bmi_width * 168, bmi_top + bmi_hight * (i - 120));
                }
                if (i == 120 || i == 300)
                {
                    g.DrawLine(Bluepen, bmi_left, bmi_top + bmi_hight * (i - 120), bmi_left + bmi_width * 172, bmi_top + bmi_hight * (i - 120));
                }
            }
            //最右边竖线
            g.DrawLine(Bluepen, bmi_left + bmi_width * 172, bmi_top, bmi_left + bmi_width * 172, bmi_top + bmi_hight * 180);
            #endregion BMI底板

            #region BMI参考曲线

            List<Point> bmi_pointlist3 = new List<Point>();//曲线图上限点
            List<Point> bmi_pointlist15 = new List<Point>();//
            List<Point> bmi_pointlist50 = new List<Point>();//
            List<Point> bmi_pointlist85 = new List<Point>();//
            List<Point> bmi_pointlist97 = new List<Point>();//

            string bmi_sqls_stand = "select * from who_childstand where sex='" + _sex + "' and ptype='BMI' and month >'60' and month <='228' order by month";
            ArrayList bmi_standlist = standbll.getwhoStandard_list(bmi_sqls_stand);

            foreach (WhoChildStandardObj standobj in bmi_standlist)
            {
                Point bmi_pointstr3 = new Point(bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p3)) * 10 * bmi_hight));
                bmi_pointlist3.Add(bmi_pointstr3);
                Point bmi_pointstr15 = new Point(bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p15)) * 10 * bmi_hight));
                bmi_pointlist15.Add(bmi_pointstr15);
                Point bmi_pointstr50 = new Point(bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p50)) * 10 * bmi_hight));
                bmi_pointlist50.Add(bmi_pointstr50);
                Point bmi_pointstr85 = new Point(bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p85)) * 10 * bmi_hight));
                bmi_pointlist85.Add(bmi_pointstr85);
                Point bmi_pointstr97 = new Point(bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p97)) * 10 * bmi_hight));
                bmi_pointlist97.Add(bmi_pointstr97);
                if (standobj.month == 228)
                {
                    g.DrawString("3", font, brush, bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p3))  * 10 * bmi_hight));
                    g.DrawString("15", font, brush, bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p15)) * 10 * bmi_hight));
                    g.DrawString("50", font, brush, bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p50)) * 10 * bmi_hight));
                    g.DrawString("85", font, brush, bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p85)) * 10 * bmi_hight));
                    g.DrawString("97", font, brush, bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p97)) * 10 * bmi_hight));
                }
            }

            if (bmi_pointlist3.Count > 1)
            {
                g.DrawLines(a, bmi_pointlist3.ToArray());
            }

            if (bmi_pointlist15.Count > 1)
            {
                g.DrawLines(b, bmi_pointlist15.ToArray());
            }

            if (bmi_pointlist50.Count > 1)
            {
                g.DrawLines(c, bmi_pointlist50.ToArray());
            }

            if (bmi_pointlist85.Count > 1)
            {
                g.DrawLines(b, bmi_pointlist85.ToArray());
            }

            if (bmi_pointlist97.Count > 1)
            {
                g.DrawLines(a, bmi_pointlist97.ToArray());
            }

            #endregion BMI参考曲线

            #region 实际曲线
            List<Point> h_factlist = new List<Point>();//身高曲线图实际值点
            List<Point> w_factlist = new List<Point>();//体重曲线图实际值点
            List<Point> bmi_factlist = new List<Point>();//bmi曲线图实际值点

            List<Point> h_factlist1 = new List<Point>();//身高曲线图实际值点
            List<Point> w_factlist1 = new List<Point>();//体重曲线图实际值点
            List<Point> bmi_factlist1 = new List<Point>();//bmi曲线图实际值点

            List<Point> h_factlist2 = new List<Point>();//身高曲线图实际值点
            List<Point> w_factlist2 = new List<Point>();//体重曲线图实际值点
            List<Point> bmi_factlist2 = new List<Point>();//bmi曲线图实际值点
            
            string sqls_check = "select datediff(month,'" + _birthtime + "',checkday) as yf,* from TB_CHILDCHECK where childId=" + _womeninfo.cd_id + " and datediff(month,'" + _birthtime + "',checkday)>60 and checkDay!='0001-01-01 00:00:00' order by datediff(day,'" + _birthtime + "',checkday) asc";
            ArrayList checklist = checkbll.getChildCheckList(sqls_check);
            if (checklist != null)
            {
                foreach (ChildCheckObj checkobj in checklist)
                {
                    int[] age = getAgeBytime(_birthtime, checkobj.CheckDay);
                    double aa = (double)age[2] / 30;
                    double factmonth = age[0] * 12 + age[1] + (double)age[2] / 30;
                    double ispremonth = 0.0;
                    #region 早产儿
                    //if (!String.IsNullOrEmpty(_week) && !String.IsNullOrEmpty(_ispre))
                    //{
                    //    int result;
                    //    if (int.TryParse(_week, out result))
                    //    {
                    //        int yz = Convert.ToInt32(_week);
                    //        if (_ispre == "1" && yz < 37)
                    //        {
                    //            ispremonth = factmonth - (40 - yz) / 4;
                    //            if (!String.IsNullOrEmpty(checkobj.CheckHeight))
                    //            {
                    //                Point h_factstr = new Point(h_left + Convert.ToInt32(ispremonth * h_width), top + Convert.ToInt32((122 - Convert.ToDouble(checkobj.CheckHeight)) * h_hight));
                    //                g.FillEllipse(prebrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);
                    //                h_factlist1.Add(h_factstr);
                    //            }

                    //            if (!String.IsNullOrEmpty(checkobj.CheckWeight))
                    //            {
                    //                Point w_factstr = new Point(left + Convert.ToInt32(ispremonth * width), top + Convert.ToInt32((25 - Convert.ToDouble(checkobj.CheckWeight)) * hight));
                    //                g.FillEllipse(prebrush, w_factstr.X - 2, w_factstr.Y - 2, 4, 4);
                    //                w_factlist1.Add(w_factstr);
                    //            }

                    //            if (!String.IsNullOrEmpty(checkobj.CheckHeight) && !String.IsNullOrEmpty(checkobj.CheckWeight))
                    //            {
                    //                Point bmi_factstr = new Point(bmi_left + Convert.ToInt32(ispremonth * bmi_width), bmi_top + Convert.ToInt32((bmi_base_line - getBmi(checkobj.CheckHeight, checkobj.CheckWeight)) * 5 * bmi_hight));
                    //                g.FillEllipse(prebrush, bmi_factstr.X - 2, bmi_factstr.Y - 2, 4, 4);
                    //                bmi_factlist1.Add(bmi_factstr);
                    //            }

                    //            if (h_factlist1.Count > 1)
                    //            {
                    //                g.DrawLines(Prep, h_factlist1.ToArray());
                    //            }
                    //            if (w_factlist1.Count > 1)
                    //            {
                    //                g.DrawLines(Prep, w_factlist1.ToArray());
                    //            }
                    //            if (bmi_factlist1.Count > 1)
                    //            {
                    //                g.DrawLines(Prep, bmi_factlist1.ToArray());
                    //            }
                    //        }
                    //    }
                    //}
                    #endregion 早产儿


                    //身高实际曲线
                    if (!String.IsNullOrEmpty(checkobj.CheckHeight))
                    {
                        Point h_factstr = new Point(h_left + Convert.ToInt32((factmonth-60) * h_width), h_top + Convert.ToInt32((195 - Convert.ToDouble(checkobj.CheckHeight)) * h_hight));
                        //Point h_pointstr3 = new Point(h_left + (standobj.month - 60) * h_width, top + Convert.ToInt32((195 - Convert.ToDouble(standobj.p3)) * h_hight));
                        g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);
                        h_factlist.Add(h_factstr);
                    }

                    //体重实际曲线
                    if (factmonth <= 120 && !String.IsNullOrEmpty(checkobj.CheckWeight))
                    {
                        Point w_factstr = new Point(w_left + Convert.ToInt32((factmonth - 60) * w_width), w_top + Convert.ToInt32((47 - Convert.ToDouble(checkobj.CheckWeight)) * w_hight));
                        //Point w_pointstr3 = new Point(left + (standobj.month-60) * width, top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p3)) * hight));
                        g.FillEllipse(bbrush, w_factstr.X - 2, w_factstr.Y - 2, 4, 4);
                        w_factlist.Add(w_factstr);
                    }
                    //BMI实际曲线
                    if (!String.IsNullOrEmpty(checkobj.CheckHeight) && !String.IsNullOrEmpty(checkobj.CheckWeight))
                    {
                        Point bmi_factstr = new Point(bmi_left + Convert.ToInt32((factmonth - 60) * bmi_width), bmi_top + Convert.ToInt32((30 - getBmi(checkobj.CheckHeight, checkobj.CheckWeight)) * 10 * bmi_hight));
                        //Point bmi_pointstr3 = new Point(bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p3)) * 10 * bmi_hight));
                        g.FillEllipse(bbrush, bmi_factstr.X - 2, bmi_factstr.Y - 2, 4, 4);
                        bmi_factlist.Add(bmi_factstr);
                    }
                }

                if (h_factlist.Count > 1)
                {
                    g.DrawLines(Bp, h_factlist.ToArray());
                }
                if (w_factlist.Count > 1)
                {
                    g.DrawLines(Bp, w_factlist.ToArray());
                }
                if (bmi_factlist.Count > 1)
                {
                    g.DrawLines(Bp, bmi_factlist.ToArray());
                }

            }
            #endregion 实际曲线
            pictureBox1.Image = img;


        }
        # endregion WHO

        private void btnPrint_Click(object sender, EventArgs e)
        {

            PanelBMIPrinter bmiprinter = new PanelBMIPrinter();

            if (bmiprinter.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    bmiprinter.Print(false, !radioButton3.Checked, this.pictureBox1.Image);//打印BMI病人曲线图
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            PanelBMIPrinter bmiprinter = new PanelBMIPrinter();

            if (bmiprinter.Visible)
            {
                Cursor.Current = Cursors.WaitCursor;
                try
                {
                    bmiprinter.Print(true, !radioButton3.Checked, this.pictureBox1.Image);//打印BMI病人曲线图
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        private int getYf(string checkage)
        {
            int yf = 0;
            switch (checkage)
            {
                case "新生儿": yf = 0; break;
                case "1个月": yf = 1; break;
                case "2个月": yf = 2; break;
                case "3个月": yf = 3; break;
                case "4个月": yf = 4; break;
                case "5个月": yf = 5; break;
                case "6个月": yf = 6; break;
                case "7个月": yf = 7; break;
                case "8个月": yf = 8; break;
                case "9个月": yf = 9; break;
                case "10个月": yf = 10; break;
                case "11个月": yf = 11; break;
                case "1周岁": yf = 12; break;
                case "1岁3个月": yf = 15; break;
                case "1岁半": yf = 18; break;
                case "1岁9个月": yf = 21; break;
                case "2周岁": yf = 24; break;
                case "2岁3个月": yf = 27; break;
                case "2岁半": yf = 30; break;
                case "2岁9个月": yf = 33; break;
                case "3周岁": yf = 36; break;
                case "3岁半": yf = 42; break;
                case "4周岁": yf = 48; break;
                case "4岁半": yf = 52; break;
                case "5周岁": yf = 60; break;
                case "5岁半": yf = 66; break;
                case "6周岁": yf = 72; break;
                case "6岁半": yf = 78; break;
                case "7周岁": yf = 84; break;
                default: yf = 0; break;
            }
            return yf;
        }

        private void DrawRotatedString(Graphics g, string text, Font font, Brush br, Rectangle rect, StringFormat format, float angle)
        {
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            //g.FillRectangle(Brushes.White, new Rectangle(center.X - rect.Height / 2, center.Y - rect.Width / 2, rect.Height, rect.Width));
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(angle);
            rect.Offset(-center.X, -center.Y);
            g.DrawString(text, font, br, rect, format);
            g.ResetTransform();
        }

        private int[] getAgeBytime(string birthtime, string nowtime)
        {
            DateTime dtbirth = Convert.ToDateTime(birthtime);
            DateTime dtnow = Convert.ToDateTime(nowtime);
            int birthyear = dtbirth.Year;
            int birthmonth = dtbirth.Month;
            int birthday = dtbirth.Day;
            int nowyear = dtnow.Year;
            int nowmonth = dtnow.Month;
            int nowday = dtnow.Day;
            int yearcou = nowyear - birthyear;
            int monthcou = nowmonth - birthmonth;
            int daycou = nowday - birthday;

            if (yearcou < 0)
            {
                yearcou = 0;
            }
            if (monthcou < 0)
            {
                monthcou = 12 + monthcou;
                yearcou = yearcou - 1;
            }
            if (daycou < 0)
            {
                daycou = 30 + daycou;
                monthcou = monthcou - 1;
            }
            int[] age = new int[3];
            age[0] = yearcou;
            age[1] = monthcou;
            age[2] = daycou;
            return age;
        }

        private int getage(string birthtime)
        {
            int now = int.Parse(DateTime.Today.ToString("yyyyMMdd"));

            int dob = int.Parse(Convert.ToDateTime(birthtime).ToString("yyyyMMdd"));

            string dif = (now - dob).ToString();

            string age = "0";

            if (dif.Length > 4)
                age = dif.Substring(0, dif.Length - 4);

            return Convert.ToInt32(age);
        }

        //BMI事件计算方法
        private Double getBmi(string txt_checkHeight, string txt_checkWeight)
        {
            Double high = 0;
            Double weight = 0;
            Double BMI = 0;
            if (double.TryParse(txt_checkHeight, out high) && double.TryParse(txt_checkWeight, out weight))
            {
                BMI = weight / ((high / 100) * (high / 100));
            }
            return BMI;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                tongji_WHO_Paint519();
            }
        }


        private void ckb_isPre_Click(object sender, EventArgs e)
        {
            string sqls = "";
            if (ckb_isPre.Checked)
            {
                sqls = "update TB_CHILDBASE set ispre='1' where id=" + _womeninfo.cd_id + "";
                _ispre = "1";
            }
            else
            {
                sqls = "update TB_CHILDBASE set ispre='0' where id=" + _womeninfo.cd_id + "";
                _ispre = "0";
            }
            bll.updaterecord(sqls);
            //tongji_BMI_Paint();

            tongji_WHO_Paint519();
        }

        private void tongji_WHO_zaochan(int zaochanyunzhou)
        {
            #region 属性设置
            int left = 60;
            int top = 30;
            int width = 8;
            int hight = 12;

            int wd = 1600;
            //生成Bitmap对像
            Bitmap img = new Bitmap(wd, 2000);
            //生成绘图对像
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // 消锯齿（可选项）
            //定义黑色画笔
            Pen Bp = new Pen(Color.Black);
            //早产儿 颜色
            Pen Prep = new Pen(Color.Teal);
            Pen basepen = new Pen(Color.DarkSlateBlue);

            //定义蓝色色画笔
            Pen Bluepen = new Pen(Color.Blue);
            //定义红色画笔
            Pen Rp = new Pen(Color.Red);
            //定义银灰色画笔
            Pen Sp = new Pen(Color.Silver);
            //定义银灰色画笔
            Pen BSp = new Pen(Color.Gray);
            //定义大标题字体
            Font Bfont = new Font("宋体", 12, FontStyle.Bold);
            //定义一般字体
            Font font = new Font("Arial", 6f);
            //定义大点的字体
            Font Tfont = new Font("宋体", 9, FontStyle.Bold);
            //定义大点的加粗字体
            Font TBfont = new Font("宋体", 9, FontStyle.Bold);
            //定义加大字体
            Font BBfont = new Font("宋体", 10);

            //绘制底色
            g.DrawRectangle(new Pen(Color.White, 1600), 0, 0, img.Width, img.Height);
            //定义黑色过渡型笔刷
            Brush bbrush = new SolidBrush(Color.Black);

            Brush prebrush = new SolidBrush(Color.Teal);

            Brush basebrush = new SolidBrush(Color.DarkSlateBlue);

            Brush brush = new SolidBrush(Color.Blue);

            if (_sex == "女")
            {
                Bluepen = new Pen(Color.Red);
                brush = new SolidBrush(Color.Red);
            }
            //定义蓝色过渡型笔刷
            LinearGradientBrush Bluebrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.Blue, 1.2F, true);
            //定义红色过渡型笔刷
            LinearGradientBrush Redbrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Red, Color.Red, 1.2F, true);
            //定义蓝色过度笔刷
            LinearGradientBrush greenbrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.FromArgb(102, 205, 170), Color.FromArgb(102, 205, 170), 1.2F, true);
            //定义绿色过度笔刷
            LinearGradientBrush green1brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.FromArgb(127, 255, 0), Color.FromArgb(127, 255, 0), 1.2F, true);
            //定义黄色过度笔刷
            LinearGradientBrush orangebrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.FromArgb(127, 255, 0), Color.Orange, 1.2F, true);
            # endregion 属性设置

            Rectangle imagerect = new Rectangle(left, top, 700, 1000);
            g.DrawImage(Resources.view, imagerect, 0, 0, Resources.view.Width, Resources.view.Height, System.Drawing.GraphicsUnit.Pixel);
            g.DrawRectangle(Bp, imagerect);

            int imageleft = 69;
            int imageright = 108;
            int imagetop = 56;
            int imagebottom = 78;

            decimal onewidth = (decimal)(700 - imageleft - imageright) / 28;
            decimal onehight = (decimal)(1000 - imagetop - imagebottom) / 90;

            List<Point> h_factlist = new List<Point>();//身高曲线图实际值点
            List<Point> w_factlist = new List<Point>();//体重曲线图实际值点
            List<Point> tw_factlist = new List<Point>();//头围曲线图实际值点

            int zaochantian = 0;
            int.TryParse(_days, out zaochantian);

            //出生体重、身长
            if (!String.IsNullOrEmpty(_height))
            {
                Point h_factstr = new Point(left + imageleft + Convert.ToInt32((zaochanyunzhou - 22) * onewidth + onewidth * (zaochantian / 7)), top + imagetop + Convert.ToInt32((65 - Convert.ToDecimal(_height)) * onehight));
                g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);
                h_factlist.Add(h_factstr);
            }

            if (!String.IsNullOrEmpty(_weight))
            {
                Point w_factstr = new Point(left + imageleft + Convert.ToInt32((zaochanyunzhou - 22) * onewidth + onewidth * (zaochantian / 7)), top + imagetop + Convert.ToInt32((9 - Convert.ToDecimal(_weight)) * 10 * onehight));
                g.FillEllipse(bbrush, w_factstr.X - 2, w_factstr.Y - 2, 4, 4);
                w_factlist.Add(w_factstr);
            }

            //if (!String.IsNullOrEmpty(_height))
            //{
            //    Point h_factstr = new Point(left + imageleft + Convert.ToInt32((zaochanyunzhou - 22) * onewidth), top + imagetop + Convert.ToInt32((65 - Convert.ToDecimal(_height)) * onehight));
            //    g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);
            //    h_factlist.Add(h_factstr);
            //}

            string sqls_check = "select (" + Convert.ToInt32(_week) * 7 + " + datediff(day, '" + _birthtime + "', checkday)) as yf,* from TB_CHILDCHECK where childid = " + _womeninfo.cd_id + "";

            sqls_check += " and (" + Convert.ToInt32(_week) * 7 + " + datediff(day, '" + _birthtime + "', checkday)) < 350";
            sqls_check += " and (" + Convert.ToInt32(_week) * 7 + " + datediff(day, '" + _birthtime + "', checkday)) > 154 ";
            sqls_check += " order by checkday asc ";

            ArrayList checklist = checkbll.getChildCheckListForBmi(sqls_check);
            if (checklist != null)
            {
                foreach (ChildCheckObj checkobj in checklist)
                {
                    int zhou = checkobj.yf / 7;
                    int tian = checkobj.yf % 7;

                    decimal checkheight = 0;
                    if (decimal.TryParse(checkobj.CheckHeight, out checkheight))
                    {
                        Point h_factstr = new Point(left + imageleft + Convert.ToInt32((zhou - 22) * onewidth + onewidth * (tian / 7)), top + imagetop + Convert.ToInt32((65 - Convert.ToDecimal(checkheight)) * onehight));
                        g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);
                        h_factlist.Add(h_factstr);
                    }

                    decimal checkweight = 0;
                    if (decimal.TryParse(checkobj.CheckWeight, out checkweight))
                    {
                        Point w_factstr = new Point(left + imageleft + Convert.ToInt32((zhou - 22) * onewidth + onewidth * (tian / 7)), top + imagetop + Convert.ToInt32((9 - Convert.ToDecimal(checkweight)) * 10 * onehight));
                        g.FillEllipse(bbrush, w_factstr.X - 2, w_factstr.Y - 2, 4, 4);
                        w_factlist.Add(w_factstr);
                    }

                    decimal checktouwei = 0;
                    if (decimal.TryParse(checkobj.CheckTouwei, out checktouwei))
                    {
                        Point tw_factstr = new Point(left + imageleft + Convert.ToInt32((zhou - 22) * onewidth + onewidth * (tian / 7)), top + imagetop + Convert.ToInt32((65 - Convert.ToDecimal(checktouwei)) * onehight));
                        g.FillEllipse(bbrush, tw_factstr.X - 2, tw_factstr.Y - 2, 4, 4);
                        tw_factlist.Add(tw_factstr);
                    }

                }
            }

            if (h_factlist.Count > 1)
            {
                g.DrawLines(Bp, h_factlist.ToArray());
            }

            if (w_factlist.Count > 1)
            {
                g.DrawLines(Bluepen, w_factlist.ToArray());
            }
            if (tw_factlist.Count > 1)
            {
                g.DrawLines(Rp, tw_factlist.ToArray());
            }

            //for (int i=0;i<200;i++)
            //{
            //    Point h_factstr = new Point(i*10, top);
            //    g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);

            //    Point h_factstr1 = new Point(left, i*10);
            //    g.FillEllipse(bbrush, h_factstr1.X - 2, h_factstr1.Y - 2, 4, 4);
            //}

            pictureBox1.Image = img;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.pictureBox1.Focus();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                int result;
                if (int.TryParse(_week, out result))
                {
                    if (result < 34)
                    {
                        tongji_WHO_zaochan(result);
                    }
                    else
                    {
                        MessageBox.Show("该儿童不属于极低早产儿");
                        radioButton2.Checked = true;
                        tongji_WHO_Paint519();
                    }
                }
                else
                {
                    MessageBox.Show("该儿童不属于极低早产儿");
                    radioButton2.Checked = true;
                    tongji_WHO_Paint519();
                }
            }
        }
    }
}
