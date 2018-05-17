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
    public partial class tongji_BMI : UserControl
    {
        ChildStandardBll standbll = new ChildStandardBll();
        ChildCheckBll checkbll = new ChildCheckBll();
        private ChildBaseInfoBll bll = new ChildBaseInfoBll();
        tb_childbase jibenobj = null;
        private string _sex = "男";
        private string _birthtime = "";
        private string _week = "";
        private string _days = "";
        private string _ispre = "";
        private string _height = "";
        private string _weight = "";
        WomenInfo _womeninfo = null;
        public tongji_BMI(WomenInfo womeninfo)
        {
            InitializeComponent();
            _womeninfo = womeninfo;
            jibenobj = new tb_childbasebll().Get(_womeninfo.cd_id);
            _sex = jibenobj.childgender;
            if (_sex == "性别不明")
            {
                _sex = "男";
            }
            _birthtime = jibenobj.childbirthday;
            _ispre = jibenobj.ispre;
            if (!String.IsNullOrEmpty(_ispre) && _ispre == "1")
            {
                ckb_isPre.Checked = true;
            }
            _week = jibenobj.cs_week;
            _days = jibenobj.cs_day;
            _weight = jibenobj.birthweight;
            _height = jibenobj.birthheight;
            //计算月份
            int[] age = getAgeBytime(_birthtime, DateTime.Now.ToString("yyyy-MM-dd"));
            double aa = (double)age[2] / 30;
            double factmonth = age[0] * 12 + age[1] + (double)age[2] / 30;
            if (factmonth>=61)
            {
                comboBox1.SelectedIndex = 1;
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }

        }

        # region 九省市
        private void tongji_BMI_Paint()
        {
            int left = 60;
            int top = 30;
            int width = 12;
            int hight = 4;

            int t_hight = hight * 10 / 4;
            decimal tz_sg_width = (decimal)36 * width / 65;
            int tz_sg_hight = t_hight;
            //取得记录数量
            //int count = ds.Tables[0].Rows.Count;
            //记算图表宽度
            //int wd = 80 + 20 * (count - 1);
            //设置最小宽度为800
            //if (wd < 800) wd = 800;
            int wd = 1200;
            //生成Bitmap对像
            Bitmap img = new Bitmap(wd, 1600);
            //生成绘图对像
            Graphics g = Graphics.FromImage(img);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; // 消锯齿（可选项）
            //定义黑色画笔
            Pen Bp = new Pen(Color.Black);
            //定义蓝色色画笔
            Pen Bluepen = new Pen(Color.Blue);
            //定义红色画笔
            Pen Rp = new Pen(Color.Red);
            //定义银灰色画笔
            Pen Sp = new Pen(Color.Silver);
            //定义银灰色画笔
            Pen BSp = new Pen(Color.Gray);
            //定义大标题字体
            Font Bfont = new Font("Arial", 12, FontStyle.Bold);
            //定义一般字体
            Font font = new Font("Arial", 6);
            //定义大点的字体
            Font Tfont = new Font("Arial", 9, FontStyle.Bold);
            //定义大点的加粗字体
            Font TBfont = new Font("Arial", 9, FontStyle.Bold);
            //定义加大字体
            Font BBfont = new Font("Arial", 10);

            //绘制底色
            g.DrawRectangle(new Pen(Color.White, 1200), 0, 0, img.Width, img.Height);
            //定义黑色过渡型笔刷
            Brush bbrush = new SolidBrush(Color.Black);

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

            //年龄小于3岁，画身高体重头围曲线图
            if (getage(_birthtime) < 3)
            {
                # region 身长、体重底板
                //绘制大标题
                g.DrawString("中国0~3" + _sex + "童身长、体重百分位曲线图", BBfont, brush, left + 6 * width, top - 50);
                //string info = "曲线图生成时间："+DateTime.Now.ToString();
                //g.DrawString(info, Tfont, Bluebrush, 80, 25);
                //绘制图片边框
                //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

                ////绘制竖坐标线       
                for (int i = 0; i <= 36; i++)
                {
                    g.DrawLine(Sp, left + width * i, top + hight, left + width * i, top + 159 * hight);
                    g.DrawLine(Bluepen, left + width * i, top, left + width * i, top + hight);
                    g.DrawLine(Bluepen, left + width * i, top + 159 * hight, left + width * i, top + 160 * hight);
                    if (i % 2 == 0)
                    {
                        //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                        g.DrawLine(Bluepen, left + width * i, top, left + width * i, top + 160 * hight);
                        if (i != 36)
                        {
                            g.DrawString(i.ToString(), font, brush, left + width * i - width / 2, top + 160 * hight);
                            g.DrawString(i.ToString(), font, brush, left + width * i - width / 2, top - hight * 2);
                        }
                        else
                        {
                            g.DrawString(i.ToString() + "月", font, brush, left + width * i - width / 2, top + 160 * hight);
                            g.DrawString(i.ToString() + "月", font, brush, left + width * i - width / 2, top - hight * 2);
                        }
                    }
                }
                //绘制横坐标线
                for (int i = 0; i <= 160; i++)
                {
                    if (i % 2 == 0)
                    {
                        g.DrawLine(Sp, left - width / 2, top + hight * i, left + width * 36 + width / 2, top + hight * i);
                    }
                    else
                    {
                        if (i == 135)
                        {
                            g.DrawLine(Sp, left, top + hight * i, left + width * 36 + width / 4, top + hight * i);
                        }
                        else if (i == 55)
                        {
                            g.DrawLine(Sp, left - width / 4, top + hight * i, left + width * 36, top + hight * i);
                        }
                        else
                        {
                            g.DrawLine(Sp, left - width / 4, top + hight * i, left + width * 36 + width / 4, top + hight * i);
                        }
                    }

                    //绘制发送量轴坐标标签
                    if (i % 10 == 0)
                    {
                        g.DrawLine(Bluepen, left - width / 2, top + hight * i, left + width * 36 + width / 2, top + hight * i);
                        if (i == 0)
                        {
                            g.DrawLine(Bluepen, left - width * 2, top + hight * i, left + width * 36 + width * 2, top + hight * i);

                        }
                        else if (i == 160)
                        {
                            g.DrawLine(Bluepen, left - width * 2, top + hight * i, left + width * 36 + width * 2, top + hight * i);

                        }
                        else
                        {
                            if (i > 134)
                            {
                                g.DrawString((32 - i / 5).ToString(), font, brush, left - width * 2, top + hight * i - hight / 2);
                            }
                            else
                            {
                                g.DrawString((110 - i / 2).ToString(), font, brush, left - width * 2, top + hight * i - hight / 2);
                            }
                            if (i > 54)
                            {
                                g.DrawString((32 - i / 5).ToString(), font, brush, left + width * 36 + width / 2, top + hight * i - hight / 2);
                            }
                            else
                            {
                                g.DrawString((110 - i / 2).ToString(), font, brush, left + width * 36 + width / 2, top + hight * i - hight / 2);
                            }
                        }
                    }
                    else if (i % 5 == 0)
                    {
                        if (i > 60)
                        {
                            g.DrawLine(Bluepen, left + width * 36, top + hight * i, left + width * 38, top + hight * i);
                        }
                        if (i > 140)
                        {
                            g.DrawLine(Bluepen, left - width * 2, top + hight * i, left, top + hight * i);
                        }
                    }
                }
                //画边框
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Rectangle rect = new Rectangle(left + width * 30, top + hight * 48, width * 2, hight * 6);
                g.DrawString("身长", font, brush, rect, sf);

                rect = new Rectangle(left + width * 30, top + hight * 108, width * 2, hight * 6);
                g.DrawString("体重", font, brush, rect, sf);

                rect = new Rectangle(left - width * 4, top + hight * 50, width * 2, hight * 7);
                g.DrawString("身\r\n长", font, brush, rect, sf);

                rect = new Rectangle(left - width * 4, top + hight * 57, width * 2, hight * 10);

                DrawRotatedString(g, "(cm)", font, brush, rect, sf, 90);

                rect = new Rectangle(left - width * 4, top + hight * 140, width * 2, hight * 7);
                g.DrawString("体\r\n重", font, brush, rect, sf);

                rect = new Rectangle(left - width * 4, top + hight * 147, width * 2, hight * 10);

                DrawRotatedString(g, "(kg)", font, brush, rect, sf, 90);

                rect = new Rectangle(left + width * 38, top + hight * 24, width * 2, hight * 7);
                g.DrawString("身\r\n长", font, brush, rect, sf);

                rect = new Rectangle(left + width * 38, top + hight * 31, width * 2, hight * 10);

                DrawRotatedString(g, "(cm)", font, brush, rect, sf, 90);

                rect = new Rectangle(left + width * 38, top + hight * 78, width * 2, hight * 7);
                g.DrawString("体\r\n重", font, brush, rect, sf);

                rect = new Rectangle(left + width * 38, top + hight * 85, width * 2, hight * 10);

                DrawRotatedString(g, "(kg)", font, brush, rect, sf, 90);

                g.DrawRectangle(Bluepen, left - width * 2, top, width * 2, hight * 134);
                g.DrawRectangle(Bluepen, left - width * 2, top + hight * 136, width * 2, hight * 24);
                g.DrawRectangle(Bluepen, left + width * 36, top, width * 2, hight * 54);
                g.DrawRectangle(Bluepen, left + width * 36, top + hight * 56, width * 2, hight * 104);
                //g.DrawRectangle(Bp, left, top, width*42, hight*90);
                # endregion 身高底板

                # region 参考曲线
                Pen a = new Pen(Color.Red);
                Pen b = new Pen(Color.FromArgb(102, 205, 170));
                Pen c = new Pen(Color.FromArgb(127, 255, 0));

                List<Point> h_pointlist3 = new List<Point>();//曲线图上限点
                //List<Point> h_pointlist5 = new List<Point>();//曲线图下限点
                List<Point> h_pointlist10 = new List<Point>();//曲线图推荐值点
                List<Point> h_pointlist25 = new List<Point>();//
                List<Point> h_pointlist50 = new List<Point>();//
                List<Point> h_pointlist75 = new List<Point>();//
                List<Point> h_pointlist90 = new List<Point>();//
                //List<Point> h_pointlist95 = new List<Point>();//
                List<Point> h_pointlist97 = new List<Point>();//

                List<Point> w_pointlist3 = new List<Point>();//曲线图上限点
                //List<Point> w_pointlist5 = new List<Point>();//曲线图下限点
                List<Point> w_pointlist10 = new List<Point>();//曲线图推荐值点
                List<Point> w_pointlist25 = new List<Point>();//
                List<Point> w_pointlist50 = new List<Point>();//
                List<Point> w_pointlist75 = new List<Point>();//
                List<Point> w_pointlist90 = new List<Point>();//
                //List<Point> w_pointlist95 = new List<Point>();//
                List<Point> w_pointlist97 = new List<Point>();//

                List<Point> t_pointlist3 = new List<Point>();//曲线图上限点
                //List<Point> t_pointlist5 = new List<Point>();//曲线图下限点
                List<Point> t_pointlist10 = new List<Point>();//曲线图推荐值点
                List<Point> t_pointlist25 = new List<Point>();//
                List<Point> t_pointlist50 = new List<Point>();//
                List<Point> t_pointlist75 = new List<Point>();//
                List<Point> t_pointlist90 = new List<Point>();//
                //List<Point> t_pointlist95 = new List<Point>();//
                List<Point> t_pointlist97 = new List<Point>();//

                List<Point> w_h_pointlist3 = new List<Point>();//曲线图上限点
                //List<Point> w_h_pointlist5 = new List<Point>();//曲线图下限点
                List<Point> w_h_pointlist10 = new List<Point>();//曲线图推荐值点
                List<Point> w_h_pointlist25 = new List<Point>();//
                List<Point> w_h_pointlist50 = new List<Point>();//
                List<Point> w_h_pointlist75 = new List<Point>();//
                List<Point> w_h_pointlist90 = new List<Point>();//
                //List<Point> w_h_pointlist95 = new List<Point>();//
                List<Point> w_h_pointlist97 = new List<Point>();//

                string sqls_stand = "select * from child_standard where sex='" + _sex + "' and yf<=36 order by yf asc";
                ArrayList standlist = standbll.getchildStandardlist(sqls_stand);

                string sqls_stand_w_h = "select * from child_standard_w_h where sex='" + _sex + "' order by hight asc";
                ArrayList stand_w_hlist = standbll.getchildStandard_w_hlist(sqls_stand_w_h);
                foreach (ChildStandardObj standobj in standlist)
                {
                    Point h_pointstr3 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_3th)) * hight * 2));
                    h_pointlist3.Add(h_pointstr3);
                    //Point h_pointstr5 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_5th)) * hight * 2));
                    //h_pointlist5.Add(h_pointstr5);
                    Point h_pointstr10 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_10th)) * hight * 2));
                    h_pointlist10.Add(h_pointstr10);
                    Point h_pointstr25 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_25th)) * hight * 2));
                    h_pointlist25.Add(h_pointstr25);
                    Point h_pointstr50 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_50th)) * hight * 2));
                    h_pointlist50.Add(h_pointstr50);
                    Point h_pointstr75 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_75th)) * hight * 2));
                    h_pointlist75.Add(h_pointstr75);
                    Point h_pointstr90 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_90th)) * hight * 2));
                    h_pointlist90.Add(h_pointstr90);
                    //Point h_pointstr95 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_95th)) * hight * 2));
                    //h_pointlist95.Add(h_pointstr95);
                    Point h_pointstr97 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_97th)) * hight * 2));
                    h_pointlist97.Add(h_pointstr97);

                    Point w_pointstr3 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_3th)) / 2 * hight * 10));
                    w_pointlist3.Add(w_pointstr3);
                    //Point w_pointstr5 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_5th)) / 2 * hight * 10));
                    //w_pointlist5.Add(w_pointstr5);
                    Point w_pointstr10 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_10th)) / 2 * hight * 10));
                    w_pointlist10.Add(w_pointstr10);
                    Point w_pointstr25 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_25th)) / 2 * hight * 10));
                    w_pointlist25.Add(w_pointstr25);
                    Point w_pointstr50 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_50th)) / 2 * hight * 10));
                    w_pointlist50.Add(w_pointstr50);
                    Point w_pointstr75 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_75th)) / 2 * hight * 10));
                    w_pointlist75.Add(w_pointstr75);
                    Point w_pointstr90 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_90th)) / 2 * hight * 10));
                    w_pointlist90.Add(w_pointstr90);
                    //Point w_pointstr95 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_95th)) / 2 * hight * 10));
                    //w_pointlist95.Add(w_pointstr95);
                    Point w_pointstr97 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_97th)) / 2 * hight * 10));
                    w_pointlist97.Add(w_pointstr97);

                    Point t_pointstr3 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_3th)) * 2 * t_hight));
                    t_pointlist3.Add(t_pointstr3);
                    //Point t_pointstr5 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_5th)) * 2 * t_hight));
                    //t_pointlist5.Add(t_pointstr5);
                    Point t_pointstr10 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_10th)) * 2 * t_hight));
                    t_pointlist10.Add(t_pointstr10);
                    Point t_pointstr25 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_25th)) * 2 * t_hight));
                    t_pointlist25.Add(t_pointstr25);
                    Point t_pointstr50 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_50th)) * 2 * t_hight));
                    t_pointlist50.Add(t_pointstr50);
                    Point t_pointstr75 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_75th)) * 2 * t_hight));
                    t_pointlist75.Add(t_pointstr75);
                    Point t_pointstr90 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_90th)) * 2 * t_hight));
                    t_pointlist90.Add(t_pointstr90);
                    //Point t_pointstr95 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_95th)) * 2 * t_hight));
                    //t_pointlist95.Add(t_pointstr95);
                    Point t_pointstr97 = new Point(left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_97th)) * 2 * t_hight));
                    t_pointlist97.Add(t_pointstr97);

                    if (standobj.yf == 35)
                    {
                        g.DrawString("3", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_3th)) * hight * 2));
                        g.DrawString("10", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_10th)) * hight * 2));
                        g.DrawString("25", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_25th)) * hight * 2));
                        g.DrawString("50", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_50th)) * hight * 2));
                        g.DrawString("75", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_75th)) * hight * 2));
                        g.DrawString("90", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_90th)) * hight * 2));
                        g.DrawString("97", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_97th)) * hight * 2));

                        g.DrawString("3", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_3th)) / 2 * hight * 10));
                        g.DrawString("10", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_10th)) / 2 * hight * 10));
                        g.DrawString("25", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_25th)) / 2 * hight * 10));
                        g.DrawString("50", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_50th)) / 2 * hight * 10));
                        g.DrawString("75", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_75th)) / 2 * hight * 10));
                        g.DrawString("90", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_90th)) / 2 * hight * 10));
                        g.DrawString("97", font, bbrush, left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_97th)) / 2 * hight * 10));

                        g.DrawString("3", font, bbrush, left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_3th)) * 2 * t_hight));
                        g.DrawString("10", font, bbrush, left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_10th)) * 2 * t_hight));
                        g.DrawString("25", font, bbrush, left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_25th)) * 2 * t_hight));
                        g.DrawString("50", font, bbrush, left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_50th)) * 2 * t_hight));
                        g.DrawString("75", font, bbrush, left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_75th)) * 2 * t_hight));
                        g.DrawString("90", font, bbrush, left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_90th)) * 2 * t_hight));
                        g.DrawString("97", font, bbrush, left + 45 * width + standobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(standobj.tw_97th)) * 2 * t_hight));
                    }

                }

                foreach (ChildStandard_w_hObj standobj in stand_w_hlist)
                {
                    Point w_h_pointstr3 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_3th)) * 2 * tz_sg_hight));
                    w_h_pointlist3.Add(w_h_pointstr3);
                    //Point w_h_pointstr5 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_5th)) * 2 * tz_sg_hight));
                    //w_h_pointlist5.Add(w_h_pointstr5);
                    Point w_h_pointstr10 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_10th)) * 2 * tz_sg_hight));
                    w_h_pointlist10.Add(w_h_pointstr10);
                    Point w_h_pointstr25 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_25th)) * 2 * tz_sg_hight));
                    w_h_pointlist25.Add(w_h_pointstr25);
                    Point w_h_pointstr50 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_50th)) * 2 * tz_sg_hight));
                    w_h_pointlist50.Add(w_h_pointstr50);
                    Point w_h_pointstr75 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_75th)) * 2 * tz_sg_hight));
                    w_h_pointlist75.Add(w_h_pointstr75);
                    Point w_h_pointstr90 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_90th)) * 2 * tz_sg_hight));
                    w_h_pointlist90.Add(w_h_pointstr90);
                    //Point w_h_pointstr95 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_95th)) * 2 * tz_sg_hight));
                    //w_h_pointlist95.Add(w_h_pointstr95);
                    Point w_h_pointstr97 = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_97th)) * 2 * tz_sg_hight));
                    w_h_pointlist97.Add(w_h_pointstr97);

                    if (standobj.hight == 107)
                    {
                        g.DrawString("3", font, bbrush, left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_3th)) * 2 * tz_sg_hight));
                        g.DrawString("10", font, bbrush, left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_10th)) * 2 * tz_sg_hight));
                        g.DrawString("25", font, bbrush, left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_25th)) * 2 * tz_sg_hight));
                        g.DrawString("50", font, bbrush, left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_50th)) * 2 * tz_sg_hight));
                        g.DrawString("75", font, bbrush, left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_75th)) * 2 * tz_sg_hight));
                        g.DrawString("90", font, bbrush, left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_90th)) * 2 * tz_sg_hight));
                        g.DrawString("97", font, bbrush, left + 45 * width + Convert.ToInt32((Convert.ToDecimal(standobj.hight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(standobj.wd_97th)) * 2 * tz_sg_hight));
                    }
                }


                # endregion 参考曲线


                # region 实际曲线
                List<Point> h_factlist = new List<Point>();//身高曲线图实际值点
                //h_factlist.Add(new Point(0,0));
                List<Point> w_factlist = new List<Point>();//体重曲线图实际值点

                List<Point> t_factlist = new List<Point>();//头围曲线图实际值点
                //h_factlist.Add(new Point(0,0));
                List<Point> h_w_factlist = new List<Point>();//身高、体重曲线图实际值点
                //w_factlist.Add(new Point(0,0));
                string sqls_check = "select datediff(month,'" + _birthtime + "',checkday) as yf,* from tb_childcheck where childId=" + _womeninfo.cd_id + " and datediff(month,'" + _birthtime + "',checkday)<=36 and checkDay!='0001-01-01 00:00:00' order by checkDay asc";
                ArrayList checklist = checkbll.getChildCheckList(sqls_check);

                if (checklist != null)
                {
                    foreach (ChildCheckObj checkobj in checklist)
                    {
                        int[] age = getAgeBytime(_birthtime, checkobj.CheckDay);
                        double aa = (double)age[2] / 30;
                        double factmonth = age[0] * 12 + age[1] + (double)age[2] / 30;
                        if (!String.IsNullOrEmpty(checkobj.CheckHeight))
                        {
                            Point h_factstr = new Point(left + Convert.ToInt32(factmonth * width), top + Convert.ToInt32((110 - Convert.ToDouble(checkobj.CheckHeight)) * hight * 2));
                            g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 5, 5);
                            h_factlist.Add(h_factstr);
                        }

                        if (!String.IsNullOrEmpty(checkobj.CheckWeight))
                        {
                            Point w_factstr = new Point(left + Convert.ToInt32(factmonth * width), top + Convert.ToInt32((32 - Convert.ToDouble(checkobj.CheckWeight)) / 2 * hight * 10));
                            g.FillEllipse(bbrush, w_factstr.X - 2, w_factstr.Y - 2, 5, 5);
                            w_factlist.Add(w_factstr);
                        }

                        if (!String.IsNullOrEmpty(checkobj.CheckTouwei))
                        {
                            Point t_factstr = new Point(left + 45 * width + Convert.ToInt32(factmonth * width), top + Convert.ToInt32((54 - Convert.ToDouble(checkobj.CheckTouwei)) * 2 * t_hight));
                            g.FillEllipse(bbrush, t_factstr.X - 2, t_factstr.Y - 2, 5, 5);
                            t_factlist.Add(t_factstr);
                        }

                        if (!String.IsNullOrEmpty(checkobj.CheckHeight) && !String.IsNullOrEmpty(checkobj.CheckWeight))
                        {
                            Point h_w_factstr = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(checkobj.CheckHeight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(checkobj.CheckWeight)) * 2 * tz_sg_hight));
                            g.FillEllipse(bbrush, h_w_factstr.X - 2, h_w_factstr.Y - 2, 5, 5);
                            h_w_factlist.Add(h_w_factstr);
                        }
                    }
                }
                # endregion 实际曲线

                # region 头围、身长底板

                left += 45 * width;

                //绘制大标题
                g.DrawString("中国0~3" + _sex + "童头围、身长的体重百分位曲线图", BBfont, brush, left + 6 * width, top - 50);
                //string info = "曲线图生成时间："+DateTime.Now.ToString();
                //g.DrawString(info, Tfont, Bluebrush, 80, 25);
                //绘制图片边框
                //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

                ////绘制竖坐标线       
                for (int i = 0; i <= 36; i++)
                {
                    g.DrawLine(Sp, left + width * i, top + t_hight, left + width * i, top + 48 * t_hight - i * t_hight * 24 / 36);
                    g.DrawLine(Bluepen, left + width * i, top, left + width * i, top + t_hight);
                    if (i % 2 == 0)
                    {
                        //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                        g.DrawLine(Bluepen, left + width * i, top, left + width * i, top + 48 * t_hight - i * t_hight * 24 / 36);
                        if (i != 36)
                        {
                            g.DrawString(i.ToString(), font, brush, left + width * i - width / 2, top - hight * 2);
                        }
                        else
                        {
                            g.DrawString(i.ToString() + "月", font, brush, left + width * i - width / 2, top - hight * 2);
                        }
                    }
                }
                //绘制横坐标线
                for (int i = 0; i <= 47; i++)
                {
                    if (i <= 24)
                    {
                        g.DrawLine(Sp, left - width / 2, top + t_hight * i, left + width * 36 + width / 2, top + t_hight * i);
                    }
                    else
                    {
                        g.DrawLine(Sp, left - width / 2, top + t_hight * i, left + width * 36 - (i - 24) * 36 * width / 24, top + t_hight * i);
                    }

                    if (i % 2 == 0)
                    {
                        if (i <= 24)
                        {
                            g.DrawLine(Bluepen, left - width / 2, top + t_hight * i, left, top + t_hight * i);
                            g.DrawLine(Bluepen, left + width * 36, top + t_hight * i, left + width * 36 + width / 2, top + t_hight * i);
                        }
                        else
                        {
                            g.DrawLine(Bluepen, left - width / 2, top + t_hight * i, left, top + t_hight * i);
                        }
                    }

                    //绘制发送量轴坐标标签
                    if (i % 4 == 0)
                    {
                        if (i < 24)
                        {
                            if (i != 0)
                            {
                                g.DrawString((54 - i / 2).ToString(), font, brush, left - width - width / 2, top + t_hight * i - t_hight / 2);
                                g.DrawString((54 - i / 2).ToString(), font, brush, left + width * 36 + width / 2, top + t_hight * i - t_hight / 2);
                            }
                            g.DrawLine(Bluepen, left - width / 2, top + t_hight * i, left + width * 36 + width / 2, top + t_hight * i);
                        }
                        else
                        {
                            g.DrawString((54 - i / 2).ToString(), font, brush, left - width - width / 2, top + t_hight * i - t_hight / 2);
                            g.DrawLine(Bluepen, left - width / 2, top + t_hight * i, left + width * 36 - (i - 24) / 4 * 6 * width, top + t_hight * i);
                        }


                    }
                }

                //画边框
                g.DrawLine(Bluepen, left, top + t_hight * 48, left + width * 36, top + t_hight * 24);
                g.DrawLine(Bluepen, left, top + t_hight * 48 + tz_sg_hight, left + width * 36, top + t_hight * 24 + tz_sg_hight);

                rect = new Rectangle(left + width * 29, top + t_hight * 18, width * 2, t_hight * 2);
                g.DrawString("头围", font, brush, rect, sf);

                rect = new Rectangle(left + Convert.ToInt32(tz_sg_width * 50), top + tz_sg_hight * 49, Convert.ToInt32(tz_sg_width * 10), tz_sg_hight * 4);
                g.DrawString("身长的体重", font, brush, rect, sf);

                rect = new Rectangle(left - width * 4, top + t_hight * 38, width * 2, t_hight * 7);
                g.DrawString("头\r\n围", font, brush, rect, sf);

                rect = new Rectangle(left - width * 4, top + t_hight * 42, width * 2, t_hight * 10);

                DrawRotatedString(g, "(cm)", font, brush, rect, sf, 90);

                rect = new Rectangle(left + width * 38, top + t_hight * 6, width * 2, t_hight * 7);
                g.DrawString("头\r\n围", font, brush, rect, sf);

                rect = new Rectangle(left + width * 38, top + t_hight * 10, width * 2, t_hight * 10);

                DrawRotatedString(g, "(cm)", font, brush, rect, sf, 90);


                rect = new Rectangle(left - width * 4, top + t_hight * 64, width * 2, t_hight * 7);
                g.DrawString("体\r\n重", font, brush, rect, sf);

                rect = new Rectangle(left - width * 4, top + t_hight * 68, width * 2, t_hight * 10);

                DrawRotatedString(g, "(kg)", font, brush, rect, sf, 90);


                rect = new Rectangle(left + width * 38, top + t_hight * 30, width * 2, t_hight * 7);
                g.DrawString("体\r\n重", font, brush, rect, sf);

                rect = new Rectangle(left + width * 38, top + t_hight * 34, width * 2, t_hight * 10);

                DrawRotatedString(g, "(kg)", font, brush, rect, sf, 90);

                g.DrawRectangle(Bluepen, left - width * 2, top, width * 2, t_hight * 48);
                g.DrawRectangle(Bluepen, left - width * 2, top + t_hight * 48 + tz_sg_hight, width * 2, tz_sg_hight * 24);
                g.DrawRectangle(Bluepen, left + width * 36, top, width * 2, t_hight * 24);
                g.DrawRectangle(Bluepen, left + width * 36, top + t_hight * 24 + tz_sg_hight, width * 2, tz_sg_hight * 48);



                //绘制身高、体重曲线
                ////绘制竖坐标线  
                top += t_hight * 24 + tz_sg_hight;
                for (int i = 0; i <= 65; i++)
                {
                    g.DrawLine(Sp, left + Convert.ToInt32(tz_sg_width * i), top + Convert.ToInt32(tz_sg_hight * 24 - i * ((decimal)24 * tz_sg_hight / 65)), left + Convert.ToInt32(tz_sg_width * i), top + tz_sg_hight * 47);
                    g.DrawLine(Bluepen, left + Convert.ToInt32(tz_sg_width * i), top + tz_sg_hight * 47, Convert.ToInt32(left + tz_sg_width * i), top + tz_sg_hight * 48);
                    if (i % 5 == 0)
                    {
                        //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                        g.DrawLine(Bluepen, left + Convert.ToInt32(tz_sg_width * i), top + Convert.ToInt32(tz_sg_hight * 24 - i * ((decimal)24 * tz_sg_hight / 65)), left + Convert.ToInt32(tz_sg_width * i), top + tz_sg_hight * 48);
                        if (i != 65)
                        {
                            g.DrawString((45 + i).ToString(), font, brush, left + Convert.ToInt32(tz_sg_width * i - tz_sg_width / 2), top + tz_sg_hight * 48);
                        }
                        else
                        {
                            g.DrawString((45 + i).ToString() + "cm", font, brush, left + Convert.ToInt32(tz_sg_width * i - tz_sg_width / 2), top + tz_sg_hight * 48);
                        }
                    }
                }
                //绘制横坐标线
                for (int i = 1; i <= 48; i++)
                {
                    if (i <= 24)
                    {
                        g.DrawLine(Sp, Convert.ToInt32(left + tz_sg_width * 65 - i * (65 * tz_sg_width / 24)), top + tz_sg_hight * i, Convert.ToInt32(left + tz_sg_width * 65 + tz_sg_width / 2), top + tz_sg_hight * i);
                    }
                    else
                    {
                        g.DrawLine(Sp, Convert.ToInt32(left - tz_sg_width / 2), top + tz_sg_hight * i, Convert.ToInt32(left + tz_sg_width * 65 + tz_sg_width / 2), top + tz_sg_hight * i);
                    }

                    if (i % 2 == 0)
                    {
                        if (i <= 24)
                        {
                            g.DrawLine(Bluepen, Convert.ToInt32(left + tz_sg_width * 65), top + tz_sg_hight * i, Convert.ToInt32(left + tz_sg_width * 65 + tz_sg_width / 2), top + tz_sg_hight * i);
                        }
                        else
                        {
                            g.DrawLine(Bluepen, Convert.ToInt32(left - tz_sg_width / 2), top + tz_sg_hight * i, left, top + tz_sg_hight * i);
                            g.DrawLine(Bluepen, Convert.ToInt32(left + tz_sg_width * 65), top + tz_sg_hight * i, Convert.ToInt32(left + tz_sg_width * 65 + tz_sg_width / 2), top + tz_sg_hight * i);
                        }
                    }

                    //绘制发送量轴坐标标签
                    if (i % 4 == 0)
                    {
                        if (i <= 24)
                        {
                            g.DrawString((24 - i / 2).ToString(), font, brush, Convert.ToInt32(left + tz_sg_width * 65 + tz_sg_width / 2), top + tz_sg_hight * i - tz_sg_hight / 2);
                            g.DrawLine(Bluepen, Convert.ToInt32(left + tz_sg_width * 65 - i * (65 * tz_sg_width / 24)), top + tz_sg_hight * i, Convert.ToInt32(left + tz_sg_width * 65), top + tz_sg_hight * i);
                        }
                        else
                        {
                            if (i != 48)
                            {
                                g.DrawString((24 - i / 2).ToString(), font, brush, Convert.ToInt32(left - tz_sg_width - tz_sg_width / 2), top + tz_sg_hight * i - tz_sg_hight / 2);
                                g.DrawString((24 - i / 2).ToString(), font, brush, Convert.ToInt32(left + tz_sg_width * 65 + tz_sg_width / 2), top + tz_sg_hight * i - tz_sg_hight / 2);
                            }
                            g.DrawLine(Bluepen, left, top + tz_sg_hight * i, Convert.ToInt32(left + tz_sg_width * 65), top + tz_sg_hight * i);
                        }


                    }
                }

                # endregion


                g.DrawLines(Bluepen, h_pointlist3.ToArray());
                //g.DrawLines(BSp, h_pointlist5.ToArray());
                g.DrawLines(BSp, h_pointlist10.ToArray());
                g.DrawLines(BSp, h_pointlist25.ToArray());
                g.DrawLines(Bluepen, h_pointlist50.ToArray());
                g.DrawLines(BSp, h_pointlist75.ToArray());
                g.DrawLines(BSp, h_pointlist90.ToArray());
                //g.DrawLines(BSp, h_pointlist95.ToArray());
                g.DrawLines(Bluepen, h_pointlist97.ToArray());

                g.DrawLines(Bluepen, w_pointlist3.ToArray());
                //g.DrawLines(BSp, w_pointlist5.ToArray());
                g.DrawLines(BSp, w_pointlist10.ToArray());
                g.DrawLines(BSp, w_pointlist25.ToArray());
                g.DrawLines(Bluepen, w_pointlist50.ToArray());
                g.DrawLines(BSp, w_pointlist75.ToArray());
                g.DrawLines(BSp, w_pointlist90.ToArray());
                //g.DrawLines(BSp, w_pointlist95.ToArray());
                g.DrawLines(Bluepen, w_pointlist97.ToArray());

                g.DrawLines(Bluepen, t_pointlist3.ToArray());
                //g.DrawLines(BSp, t_pointlist5.ToArray());
                g.DrawLines(BSp, t_pointlist10.ToArray());
                g.DrawLines(BSp, t_pointlist25.ToArray());
                g.DrawLines(Bluepen, t_pointlist50.ToArray());
                g.DrawLines(BSp, t_pointlist75.ToArray());
                g.DrawLines(BSp, t_pointlist90.ToArray());
                //g.DrawLines(BSp, t_pointlist95.ToArray());
                g.DrawLines(Bluepen, t_pointlist97.ToArray());

                g.DrawLines(Bluepen, w_h_pointlist3.ToArray());
                //g.DrawLines(BSp, w_h_pointlist5.ToArray());
                g.DrawLines(BSp, w_h_pointlist10.ToArray());
                g.DrawLines(BSp, w_h_pointlist25.ToArray());
                g.DrawLines(Bluepen, w_h_pointlist50.ToArray());
                g.DrawLines(BSp, w_h_pointlist75.ToArray());
                g.DrawLines(BSp, w_h_pointlist90.ToArray());
                //g.DrawLines(BSp, w_h_pointlist95.ToArray());
                g.DrawLines(Bluepen, w_h_pointlist97.ToArray());


                if (h_factlist.Count > 1)
                {
                    g.DrawLines(Bp, h_factlist.ToArray());
                }
                if (w_factlist.Count > 1)
                {
                    g.DrawLines(Bp, w_factlist.ToArray());
                }
                if (t_factlist.Count > 1)
                {
                    g.DrawLines(Bp, t_factlist.ToArray());
                }
                if (h_w_factlist.Count > 1)
                {
                    g.DrawLines(Bp, h_w_factlist.ToArray());
                }
            }
            else//年龄大于等于3岁，画身高体重曲线图和bmi曲线图
            {
                width = width / 2;
                int hengcou = 140;
                if (_sex == "女")
                {
                    hengcou = 130;
                }
                # region 身长、体重底板
                //绘制大标题
                g.DrawString("中国2~18岁" + _sex + "童身长、体重百分位曲线图", BBfont, brush, left + 6 * width, top - 50);
                //string info = "曲线图生成时间："+DateTime.Now.ToString();
                //g.DrawString(info, Tfont, Bluebrush, 80, 25);
                //绘制图片边框
                //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

                ////绘制竖坐标线       
                for (int i = 0; i <= 64; i++)
                {
                    g.DrawLine(Sp, left + width * i, top + hight, left + width * i, top + (hengcou - 1) * hight);
                    g.DrawLine(Bluepen, left + width * i, top, left + width * i, top + hight);
                    g.DrawLine(Bluepen, left + width * i, top + (hengcou - 1) * hight, left + width * i, top + hengcou * hight);
                    if (i % 4 == 0)
                    {
                        //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                        g.DrawLine(Bluepen, left + width * i, top, left + width * i, top + hengcou * hight);
                        if (i != 64)
                        {
                            g.DrawString((2 + i / 4).ToString(), font, brush, left + width * i - width / 2, top + hengcou * hight);
                            g.DrawString((2 + i / 4).ToString(), font, brush, left + width * i - width / 2, top - hight * 2);
                        }
                        else
                        {
                            g.DrawString((2 + i / 4).ToString() + "岁", font, brush, left + width * i - width / 2, top + hengcou * hight);
                            g.DrawString((2 + i / 4).ToString() + "岁", font, brush, left + width * i - width / 2, top - hight * 2);
                        }
                    }
                }
                //绘制横坐标线
                for (int i = 0; i <= hengcou; i++)
                {
                    if (i % 2 == 0)
                    {
                        g.DrawLine(Sp, left - width / 2, top + hight * i, left + width * 64 + width / 2, top + hight * i);
                    }
                    else
                    {
                        if (i == 116)
                        {
                            g.DrawLine(Sp, left, top + hight * i, left + width * 64 + width / 4, top + hight * i);
                        }
                        else if (i == 46)
                        {
                            g.DrawLine(Sp, left - width / 4, top + hight * i, left + width * 64, top + hight * i);
                        }
                        else
                        {
                            g.DrawLine(Sp, left - width / 4, top + hight * i, left + width * 64 + width / 4, top + hight * i);
                        }
                    }

                    //绘制发送量轴坐标标签
                    if (i % 10 == 0)
                    {
                        g.DrawLine(Bluepen, left - width / 2, top + hight * i, left + width * 64 + width / 2, top + hight * i);
                        if (i == 0)
                        {
                            g.DrawLine(Bluepen, left - width * 2, top + hight * i, left + width * 64 + width * 2, top + hight * i);

                        }
                        else if (i == hengcou)
                        {
                            g.DrawLine(Bluepen, left - width * 2, top + hight * i, left + width * 64 + width * 2, top + hight * i);

                        }
                        else
                        {
                            if (i > hengcou - 30)
                            {
                                g.DrawString((hengcou - i).ToString(), font, brush, left - width * 4, top + hight * i - hight / 2);
                            }
                            else
                            {
                                g.DrawString((hengcou + 50 - i).ToString(), font, brush, left - width * 4, top + hight * i - hight / 2);
                            }
                            if (i > hengcou - 100)
                            {
                                g.DrawString((hengcou - i).ToString(), font, brush, left + width * 64 + width / 2, top + hight * i - hight / 2);
                            }
                            else
                            {
                                g.DrawString((hengcou + 50 - i).ToString(), font, brush, left + width * 64 + width / 2, top + hight * i - hight / 2);
                            }
                        }
                    }
                    else if (i % 5 == 0)
                    {
                        if (i != hengcou - 95)
                        {
                            g.DrawLine(Bluepen, left + width * 64, top + hight * i, left + width * 68, top + hight * i);
                        }
                        g.DrawLine(Bluepen, left - width * 4, top + hight * i, left, top + hight * i);
                    }
                }
                //画边框
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Rectangle rect = new Rectangle(left + width * 54, top + hight * 33, width * 4, hight * 6);
                g.DrawString("身高", font, brush, rect, sf);

                rect = new Rectangle(left + width * 54, top + hight * 93, width * 4, hight * 6);
                g.DrawString("体重", font, brush, rect, sf);

                rect = new Rectangle(left - width * 8, top + hight * 33, width * 4, hight * 7);
                g.DrawString("身\r\n高", font, brush, rect, sf);

                rect = new Rectangle(left - width * 8, top + hight * 40, width * 4, hight * 10);

                DrawRotatedString(g, "(cm)", font, brush, rect, sf, 90);

                rect = new Rectangle(left - width * 8, top + hight * 110, width * 4, hight * 7);
                g.DrawString("体\r\n重", font, brush, rect, sf);

                rect = new Rectangle(left - width * 8, top + hight * 117, width * 4, hight * 10);

                DrawRotatedString(g, "(kg)", font, brush, rect, sf, 90);

                rect = new Rectangle(left + width * 68, top + hight * 13, width * 4, hight * 7);
                g.DrawString("身\r\n高", font, brush, rect, sf);

                rect = new Rectangle(left + width * 68, top + hight * 20, width * 4, hight * 10);

                DrawRotatedString(g, "(cm)", font, brush, rect, sf, 90);

                rect = new Rectangle(left + width * 68, top + hight * 68, width * 4, hight * 7);
                g.DrawString("体\r\n重", font, brush, rect, sf);

                rect = new Rectangle(left + width * 68, top + hight * 75, width * 4, hight * 10);

                DrawRotatedString(g, "(kg)", font, brush, rect, sf, 90);

                g.DrawRectangle(Bluepen, left - width * 4, top, width * 4, hight * (hengcou - 25));
                g.DrawRectangle(Bluepen, left - width * 4, top + hight * (hengcou - 23), width * 4, hight * 23);
                g.DrawRectangle(Bluepen, left + width * 64, top, width * 4, hight * (hengcou - 96));
                g.DrawRectangle(Bluepen, left + width * 64, top + hight * (hengcou - 94), width * 4, hight * 94);
                //g.DrawRectangle(Bp, left, top, width*42, hight*90);
                # endregion 身高底板

                # region bmi底板
                int scleft = left + width * 75;
                int bmiwidth = width * 3;
                int bmihight = hight * 3;
                for (int i = 0; i <= 28; i++)
                {

                    if (i % 4 == 0)
                    {
                        g.DrawLine(Bluepen, scleft + bmiwidth * i, top, scleft + bmiwidth * i, top + bmihight * 61);
                        g.DrawString((i / 4).ToString(), font, brush, scleft + bmiwidth * i - bmiwidth / 2, top + 60 * bmihight);
                    }
                    else
                    {
                        g.DrawLine(Sp, scleft + bmiwidth * i, top, scleft + bmiwidth * i, top + bmihight * 60);
                    }

                }
                for (int i = 0; i <= 60; i++)
                {

                    if (i % 5 == 0)
                    {
                        g.DrawLine(Bluepen, scleft - bmiwidth, top + bmihight * i, scleft + bmiwidth * 28, top + bmihight * i);
                        g.DrawString((22 - i / 5).ToString(), font, brush, scleft - bmiwidth, top + bmihight * i);
                    }
                    else
                    {
                        g.DrawLine(Sp, scleft, top + bmihight * i, scleft + bmiwidth * 28, top + bmihight * i);
                    }

                }
                # endregion bmi底板

                # region 参考曲线
                Pen a = new Pen(Color.Red);
                Pen b = new Pen(Color.FromArgb(102, 205, 170));
                Pen c = new Pen(Color.FromArgb(127, 255, 0));

                List<Point> h_pointlist3 = new List<Point>();//曲线图上限点
                //List<Point> h_pointlist5 = new List<Point>();//曲线图下限点
                List<Point> h_pointlist10 = new List<Point>();//曲线图推荐值点
                List<Point> h_pointlist25 = new List<Point>();//
                List<Point> h_pointlist50 = new List<Point>();//
                List<Point> h_pointlist75 = new List<Point>();//
                List<Point> h_pointlist90 = new List<Point>();//
                //List<Point> h_pointlist95 = new List<Point>();//
                List<Point> h_pointlist97 = new List<Point>();//

                List<Point> w_pointlist3 = new List<Point>();//曲线图上限点
                //List<Point> w_pointlist5 = new List<Point>();//曲线图下限点
                List<Point> w_pointlist10 = new List<Point>();//曲线图推荐值点
                List<Point> w_pointlist25 = new List<Point>();//
                List<Point> w_pointlist50 = new List<Point>();//
                List<Point> w_pointlist75 = new List<Point>();//
                List<Point> w_pointlist90 = new List<Point>();//
                //List<Point> w_pointlist95 = new List<Point>();//
                List<Point> w_pointlist97 = new List<Point>();//

                List<Point> bmi_pointlist3 = new List<Point>();//曲线图上限点
                //List<Point> w_h_pointlist5 = new List<Point>();//曲线图下限点
                List<Point> bmi_pointlist10 = new List<Point>();//曲线图推荐值点
                List<Point> bmi_pointlist15 = new List<Point>();//
                List<Point> bmi_pointlist25 = new List<Point>();//
                List<Point> bmi_pointlist50 = new List<Point>();//
                List<Point> bmi_pointlist75 = new List<Point>();//
                List<Point> bmi_pointlist85 = new List<Point>();//
                List<Point> bmi_pointlist90 = new List<Point>();//
                //List<Point> w_h_pointlist95 = new List<Point>();//
                List<Point> bmi_pointlist97 = new List<Point>();//

                string sqls_stand = "select * from child_standard where sex='" + _sex + "' and yf>=24 order by yf asc";
                ArrayList standlist = standbll.getchildStandardlist(sqls_stand);

                string sqls_stand_bmi = "select * from child_standard_bmi where sex='" + _sex + "' order by yf asc";
                ArrayList stand_bmilist = standbll.getchildStandard_bmilist(sqls_stand_bmi);
                foreach (ChildStandardObj standobj in standlist)
                {
                    Point h_pointstr3 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_3th)) * hight));
                    h_pointlist3.Add(h_pointstr3);
                    //Point h_pointstr5 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_5th)) * hight * 2));
                    //h_pointlist5.Add(h_pointstr5);
                    Point h_pointstr10 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_10th)) * hight));
                    h_pointlist10.Add(h_pointstr10);
                    Point h_pointstr25 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_25th)) * hight));
                    h_pointlist25.Add(h_pointstr25);
                    Point h_pointstr50 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_50th)) * hight));
                    h_pointlist50.Add(h_pointstr50);
                    Point h_pointstr75 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_75th)) * hight));
                    h_pointlist75.Add(h_pointstr75);
                    Point h_pointstr90 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_90th)) * hight));
                    h_pointlist90.Add(h_pointstr90);
                    //Point h_pointstr95 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_95th)) * hight * 2));
                    //h_pointlist95.Add(h_pointstr95);
                    Point h_pointstr97 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_97th)) * hight));
                    h_pointlist97.Add(h_pointstr97);

                    Point w_pointstr3 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_3th)) * hight));
                    w_pointlist3.Add(w_pointstr3);
                    //Point w_pointstr5 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_5th)) / 2 * hight * 10));
                    //w_pointlist5.Add(w_pointstr5);
                    Point w_pointstr10 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_10th)) * hight));
                    w_pointlist10.Add(w_pointstr10);
                    Point w_pointstr25 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_25th)) * hight));
                    w_pointlist25.Add(w_pointstr25);
                    Point w_pointstr50 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_50th)) * hight));
                    w_pointlist50.Add(w_pointstr50);
                    Point w_pointstr75 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_75th)) * hight));
                    w_pointlist75.Add(w_pointstr75);
                    Point w_pointstr90 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_90th)) * hight));
                    w_pointlist90.Add(w_pointstr90);
                    //Point w_pointstr95 = new Point(left + standobj.yf * width, top + Convert.ToInt32((32 - Convert.ToDouble(standobj.wd_95th)) / 2 * hight * 10));
                    //w_pointlist95.Add(w_pointstr95);
                    Point w_pointstr97 = new Point(left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_97th)) * hight));
                    w_pointlist97.Add(w_pointstr97);


                    if (standobj.yf == 204)
                    {
                        g.DrawString("3", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_3th)) * hight));
                        g.DrawString("10", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_10th)) * hight));
                        g.DrawString("25", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_25th)) * hight));
                        g.DrawString("50", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_50th)) * hight));
                        g.DrawString("75", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_75th)) * hight));
                        g.DrawString("90", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_90th)) * hight));
                        g.DrawString("97", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(standobj.ht_97th)) * hight));

                        g.DrawString("3", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_3th)) * hight));
                        g.DrawString("10", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_10th)) * hight));
                        g.DrawString("25", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_25th)) * hight));
                        g.DrawString("50", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_50th)) * hight));
                        g.DrawString("75", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_75th)) * hight));
                        g.DrawString("90", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_90th)) * hight));
                        g.DrawString("97", font, bbrush, left + Convert.ToInt32((standobj.yf - 24) * Convert.ToDecimal(width) / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(standobj.wd_97th)) * hight));

                    }

                }

                foreach (ChildStandard_bmiObj standobj in stand_bmilist)
                {
                    Point bmi_pointstr3 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_3th)) * bmihight * 5));
                    bmi_pointlist3.Add(bmi_pointstr3);
                    //Point h_pointstr5 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_5th)) * hight * 2));
                    //h_pointlist5.Add(h_pointstr5);
                    Point bmi_pointstr10 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_10th)) * bmihight * 5));
                    bmi_pointlist10.Add(bmi_pointstr10);
                    Point bmi_pointstr15 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_15th)) * bmihight * 5));
                    bmi_pointlist15.Add(bmi_pointstr15);
                    Point bmi_pointstr25 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_25th)) * bmihight * 5));
                    bmi_pointlist25.Add(bmi_pointstr25);
                    Point bmi_pointstr50 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_50th)) * bmihight * 5));
                    bmi_pointlist50.Add(bmi_pointstr50);
                    Point bmi_pointstr75 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_75th)) * bmihight * 5));
                    bmi_pointlist75.Add(bmi_pointstr75);
                    Point bmi_pointstr85 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_85th)) * bmihight * 5));
                    bmi_pointlist85.Add(bmi_pointstr85);
                    Point bmi_pointstr90 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_90th)) * bmihight * 5));
                    bmi_pointlist90.Add(bmi_pointstr90);
                    //Point h_pointstr95 = new Point(left + standobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(standobj.ht_95th)) * hight * 2));
                    //h_pointlist95.Add(h_pointstr95);
                    Point bmi_pointstr97 = new Point(scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_97th)) * bmihight * 5));
                    bmi_pointlist97.Add(bmi_pointstr97);

                    if (standobj.yf == 83)
                    {
                        g.DrawString("3", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_3th)) * bmihight * 5));
                        g.DrawString("10", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_10th)) * bmihight * 5));
                        g.DrawString("15", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_15th)) * bmihight * 5));
                        g.DrawString("25", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_25th)) * bmihight * 5));
                        g.DrawString("50", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_50th)) * bmihight * 5));
                        g.DrawString("75", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_75th)) * bmihight * 5));
                        g.DrawString("85", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_85th)) * bmihight * 5));
                        g.DrawString("90", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_90th)) * bmihight * 5));
                        g.DrawString("97", font, bbrush, scleft + Convert.ToInt32(standobj.yf * Convert.ToDecimal(bmiwidth) / 3), top + Convert.ToInt32((22 - Convert.ToDecimal(standobj.bmi_97th)) * bmihight * 5));

                    }
                }
                g.DrawLines(Bluepen, h_pointlist3.ToArray());
                //g.DrawLines(BSp, t_pointlist5.ToArray());
                g.DrawLines(BSp, h_pointlist10.ToArray());
                g.DrawLines(BSp, h_pointlist25.ToArray());
                g.DrawLines(Bluepen, h_pointlist50.ToArray());
                g.DrawLines(BSp, h_pointlist75.ToArray());
                g.DrawLines(BSp, h_pointlist90.ToArray());
                //g.DrawLines(BSp, t_pointlist95.ToArray());
                g.DrawLines(Bluepen, h_pointlist97.ToArray());

                g.DrawLines(Bluepen, w_pointlist3.ToArray());
                //g.DrawLines(BSp, t_pointlist5.ToArray());
                g.DrawLines(BSp, w_pointlist10.ToArray());
                g.DrawLines(BSp, w_pointlist25.ToArray());
                g.DrawLines(Bluepen, w_pointlist50.ToArray());
                g.DrawLines(BSp, w_pointlist75.ToArray());
                g.DrawLines(BSp, w_pointlist90.ToArray());
                //g.DrawLines(BSp, t_pointlist95.ToArray());
                g.DrawLines(Bluepen, w_pointlist97.ToArray());

                g.DrawLines(Bluepen, bmi_pointlist3.ToArray());
                //g.DrawLines(BSp, t_pointlist5.ToArray());
                g.DrawLines(BSp, bmi_pointlist10.ToArray());
                g.DrawLines(Bluepen, bmi_pointlist15.ToArray());
                g.DrawLines(BSp, bmi_pointlist25.ToArray());
                g.DrawLines(Bluepen, bmi_pointlist50.ToArray());
                g.DrawLines(BSp, bmi_pointlist75.ToArray());
                g.DrawLines(Bluepen, bmi_pointlist85.ToArray());
                g.DrawLines(BSp, bmi_pointlist90.ToArray());
                //g.DrawLines(BSp, t_pointlist95.ToArray());
                g.DrawLines(Bluepen, bmi_pointlist97.ToArray());


                # endregion 参考曲线

                # region 实际曲线
                List<Point> h_factlist = new List<Point>();//身高曲线图实际值点
                List<Point> w_factlist = new List<Point>();//体重曲线图实际值点
                List<Point> bmi_factlist = new List<Point>();//bmi曲线图实际值点

                string sqls_check = "select datediff(month,'" + _birthtime + "',checkday) as yf,* from tb_childcheck where childId=" + _womeninfo.cd_id + " and datediff(month,'" + _birthtime + "',checkday)>=24 and checkDay!='0001-01-01 00:00:00' order by checkDay asc";
                ArrayList checklist = checkbll.getChildCheckList(sqls_check);

                if (checklist != null)
                {
                    foreach (ChildCheckObj checkobj in checklist)
                    {
                        int[] age = getAgeBytime(_birthtime, checkobj.CheckDay);
                        double aa = (double)age[2] / 30;
                        double factmonth = age[0] * 12 + age[1] + (double)age[2] / 30;
                        if (factmonth >= 24)
                        {
                            if (!String.IsNullOrEmpty(checkobj.CheckHeight))
                            {
                                Point h_factstr = new Point(left + Convert.ToInt32((factmonth - 24) * (double)width / 3), top + Convert.ToInt32((hengcou + 50 - Convert.ToDecimal(checkobj.CheckHeight)) * hight));
                                g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 5, 5);
                                h_factlist.Add(h_factstr);
                            }

                            if (!String.IsNullOrEmpty(checkobj.CheckWeight))
                            {
                                Point w_factstr = new Point(left + Convert.ToInt32((factmonth - 24) * (double)width / 3), top + Convert.ToInt32((hengcou - Convert.ToDecimal(checkobj.CheckWeight)) * hight));
                                g.FillEllipse(bbrush, w_factstr.X - 2, w_factstr.Y - 2, 5, 5);
                                w_factlist.Add(w_factstr);
                            }
                        }

                        if (!String.IsNullOrEmpty(checkobj.CheckHeight) && !String.IsNullOrEmpty(checkobj.CheckWeight))
                        {

                            decimal wm_high = Convert.ToDecimal(checkobj.CheckHeight);//身高
                            decimal wm_weight = Convert.ToDecimal(checkobj.CheckWeight);//体重
                            decimal fenmu = (wm_high / 100) * (wm_high / 100);
                            decimal bmi = wm_weight / fenmu;
                            float factbmi = float.Parse(bmi.ToString("f1"));

                            Point bmi_factstr = new Point(scleft + Convert.ToInt32(factmonth * (double)bmiwidth / 3), top + Convert.ToInt32((22 - factbmi) * bmihight * 5));
                            g.FillEllipse(bbrush, bmi_factstr.X - 2, bmi_factstr.Y - 2, 5, 5);
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
                # endregion 实际曲线

            }

            pictureBox1.Image = img;


        }
        # endregion 九省市

        # region WHO
        private void tongji_WHO_Paint()
        {
            # region 属性设置
            int left = 30;
            int top = 30;
            int width = 8;
            int hight = 12;

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

            # region 体重底板
            //绘制大标题
            //Rectangle rect = new Rectangle(left, top - 20,width*60,30);
            g.DrawString("（WHO）0~5" + _sex + "童体重曲线图", BBfont, brush, left + width * 20, top - 20);
            //string info = "曲线图生成时间："+DateTime.Now.ToString();
            //g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //绘制图片边框
            //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            ////绘制竖坐标线       
            for (int i = 0; i <= 60; i++)
            {
                if (i % 12 == 0)
                {
                    //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                    g.DrawLine(Bluepen, left + width * i, top, left + width * i, top + 25 * hight);
                    g.DrawString((i / 12).ToString() + "岁", font, brush, left + width * i - width / 2, top + 25 * hight);
                }
                else
                {
                    g.DrawLine(Sp, left + width * i, top, left + width * i, top + 24 * hight);
                    g.DrawString((i % 12).ToString(), font, brush, left + width * i - width / 2, top + 24 * hight);
                }
            }
            //绘制横坐标线
            for (int i = 0; i <= 24; i++)
            {
                if (i % 2 == 0)
                {
                    g.DrawLine(Sp, left, top + hight * i, left + width * 60, top + hight * i);
                }
                else
                {
                    g.DrawLine(Bluepen, left, top + hight * i, left + width * 62, top + hight * i);
                    g.DrawString((25 - i).ToString(), font, brush, left - width, top + hight * i);
                    g.DrawString((25 - i).ToString(), font, brush, left + width * 62, top + hight * i);
                }
                if (i == 0 || i == 24)
                {
                    g.DrawLine(Bluepen, left, top + hight * i, left + width * 62, top + hight * i);
                }
            }
            g.DrawLine(Bluepen, left + width * 62, top, left + width * 62, top + hight * 24);

            # endregion 体重底板

            # region 参考曲线
            Pen a = new Pen(Color.DarkRed);
            Pen b = new Pen(Color.Orange);
            Pen c = new Pen(Color.Green);

            List<Point> w_pointlist3 = new List<Point>();//曲线图上限点
            List<Point> w_pointlist15 = new List<Point>();//
            List<Point> w_pointlist50 = new List<Point>();//
            List<Point> w_pointlist85 = new List<Point>();//
            List<Point> w_pointlist97 = new List<Point>();//

            string sqls_stand = "select * from who_childstand where sex='" + _sex + "' and ptype='体重' and month <='60' order by month";
            ArrayList standlist = standbll.getwhoStandard_list(sqls_stand);

            foreach (WhoChildStandardObj standobj in standlist)
            {
                Point w_pointstr3 = new Point(left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p3)) * hight));
                w_pointlist3.Add(w_pointstr3);
                Point w_pointstr15 = new Point(left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p15)) * hight));
                w_pointlist15.Add(w_pointstr15);
                Point w_pointstr50 = new Point(left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p50)) * hight));
                w_pointlist50.Add(w_pointstr50);
                Point w_pointstr85 = new Point(left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p85)) * hight));
                w_pointlist85.Add(w_pointstr85);
                Point w_pointstr97 = new Point(left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p97)) * hight));
                w_pointlist97.Add(w_pointstr97);
                if (standobj.month == 60)
                {
                    g.DrawString("3", font, brush, left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p3)) * hight));
                    g.DrawString("15", font, brush, left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p15)) * hight));
                    g.DrawString("50", font, brush, left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p50)) * hight));
                    g.DrawString("85", font, brush, left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p85)) * hight));
                    g.DrawString("97", font, brush, left + standobj.month * width, top + Convert.ToInt32((25 - Convert.ToDouble(standobj.p97)) * hight));
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

            # endregion 参考曲线

            # region 实际曲线
            //List<Point> h_factlist = new List<Point>();//身高曲线图实际值点
            //List<Point> w_factlist = new List<Point>();//体重曲线图实际值点
            //List<Point> t_factlist = new List<Point>();//头围曲线图实际值点
            //List<Point> h_w_factlist = new List<Point>();//身高、体重曲线图实际值点

            //string sqls_check = "select datediff(month,'" + _birthtime + "',checkday) as yf,* from tb_childcheck where childId=" + globalInfoClass.Wm_Index + " and datediff(month,'" + _birthtime + "',checkday)<=60 and checkDay!='0001-01-01 00:00:00' order by checkDay asc";
            //ArrayList checklist = checkbll.getChildCheckList(sqls_check);

            //if (checklist != null)
            //{
            //    foreach (ChildCheckObj checkobj in checklist)
            //    {
            //        if (!String.IsNullOrEmpty(checkobj.CheckHeight))
            //        {
            //            //Point h_factstr = new Point(left + checkobj.yf * width, top + Convert.ToInt32((110 - Convert.ToDouble(checkobj.CheckHeight)) * hight * 2));
            //            //g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 5, 5);
            //            //h_factlist.Add(h_factstr);
            //        }

            //        if (!String.IsNullOrEmpty(checkobj.CheckWeight))
            //        {
            //            Point w_factstr = new Point(left + checkobj.yf * width, top + Convert.ToInt32((25-Convert.ToDouble(checkobj.CheckWeight))* hight));
            //            g.FillEllipse(bbrush, w_factstr.X - 2, w_factstr.Y - 2, 5, 5);
            //            w_factlist.Add(w_factstr);
            //        }

            //        if (!String.IsNullOrEmpty(checkobj.CheckTouwei))
            //        {
            //                //Point t_factstr = new Point(left + 45 * width + checkobj.yf * width, top + Convert.ToInt32((54 - Convert.ToDouble(checkobj.CheckTouwei)) * 2 * t_hight));
            //                //g.FillEllipse(bbrush, t_factstr.X - 2, t_factstr.Y - 2, 5, 5);
            //                //t_factlist.Add(t_factstr);
            //        }

            //        if (!String.IsNullOrEmpty(checkobj.CheckHeight) && !String.IsNullOrEmpty(checkobj.CheckWeight))
            //        {
            //                //Point h_w_factstr = new Point(left + 45 * width + Convert.ToInt32((Convert.ToDecimal(checkobj.CheckHeight) - 45) * tz_sg_width), top + t_hight * 24 + tz_sg_hight + Convert.ToInt32((24 - Convert.ToDouble(checkobj.CheckWeight)) * 2 * tz_sg_hight));
            //                //g.FillEllipse(bbrush, h_w_factstr.X - 2, h_w_factstr.Y - 2, 5, 5);
            //                //h_w_factlist.Add(h_w_factstr);
            //        }
            //    }
            //}
            //if (h_factlist.Count > 1)
            //{
            //    g.DrawLines(Bp, h_factlist.ToArray());
            //}
            //if (w_factlist.Count > 1)
            //{
            //    g.DrawLines(Bp, w_factlist.ToArray());
            //}
            //if (t_factlist.Count > 1)
            //{
            //    g.DrawLines(Bp, t_factlist.ToArray());
            //}
            //if (h_w_factlist.Count > 1)
            //{
            //    g.DrawLines(Bp, h_w_factlist.ToArray());
            //}
            # endregion 实际曲线

            # region 身高底板
            int h_left = left + width * 62 + 50;
            int h_width = width;
            int h_hight = 4;
            //绘制大标题
            g.DrawString("（WHO）0~5" + _sex + "童身长/身高曲线图", BBfont, brush, h_left + 6 * width, top - 20);
            //string info = "曲线图生成时间："+DateTime.Now.ToString();
            //g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //绘制图片边框
            //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            ////绘制竖坐标线       
            for (int i = 0; i <= 60; i++)
            {
                if (i % 12 == 0)
                {
                    //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                    g.DrawLine(Bluepen, h_left + h_width * i, top, h_left + h_width * i, top + 80 * h_hight);
                    g.DrawString((i / 12).ToString() + "岁", font, brush, h_left + h_width * i - h_width / 2, top + 80 * h_hight);
                }
                else
                {
                    g.DrawLine(Sp, h_left + h_width * i, top, h_left + h_width * i, top + 79 * h_hight);
                    g.DrawString((i % 12).ToString(), font, brush, h_left + h_width * i - h_width / 2, top + 79 * h_hight);
                }
            }
            //绘制横坐标线
            for (int i = 0; i <= 79; i++)
            {
                if ((i - 2) % 5 == 0)
                {
                    g.DrawLine(Bluepen, h_left, top + h_hight * i, h_left + h_width * 62, top + h_hight * i);
                    g.DrawString((122 - i).ToString(), font, brush, h_left - h_width * 2, top + h_hight * i);
                    g.DrawString((122 - i).ToString(), font, brush, h_left + h_width * 62, top + h_hight * i);

                }
                else
                {
                    g.DrawLine(Sp, h_left, top + h_hight * i, h_left + h_width * 60, top + h_hight * i);
                }
                if (i == 0 || i == 79)
                {
                    g.DrawLine(Bluepen, h_left, top + h_hight * i, h_left + h_width * 62, top + h_hight * i);
                }
            }
            g.DrawLine(Bluepen, h_left + h_width * 62, top, h_left + h_width * 62, top + h_hight * 79);
            # endregion 身高底板

            # region 参考曲线

            List<Point> h_pointlist3 = new List<Point>();//曲线图上限点
            List<Point> h_pointlist15 = new List<Point>();//
            List<Point> h_pointlist50 = new List<Point>();//
            List<Point> h_pointlist85 = new List<Point>();//
            List<Point> h_pointlist97 = new List<Point>();//

            string h_sqls_stand = "select * from who_childstand where sex='" + _sex + "' and ptype='身高' and month <= '60' order by month";
            ArrayList h_standlist = standbll.getwhoStandard_list(h_sqls_stand);

            foreach (WhoChildStandardObj standobj in h_standlist)
            {
                Point h_pointstr3 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p3)) * h_hight));
                h_pointlist3.Add(h_pointstr3);
                Point h_pointstr15 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p15)) * h_hight));
                h_pointlist15.Add(h_pointstr15);
                Point h_pointstr50 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p50)) * h_hight));
                h_pointlist50.Add(h_pointstr50);
                Point h_pointstr85 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p85)) * h_hight));
                h_pointlist85.Add(h_pointstr85);
                Point h_pointstr97 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p97)) * h_hight));
                h_pointlist97.Add(h_pointstr97);
                if (standobj.month == 60)
                {
                    g.DrawString("3", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p3)) * h_hight));
                    g.DrawString("15", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p15)) * h_hight));
                    g.DrawString("50", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p50)) * h_hight));
                    g.DrawString("85", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p85)) * h_hight));
                    g.DrawString("97", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((122 - Convert.ToDouble(standobj.p97)) * h_hight));
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

            # region 头围底板
            int tw_left = left;
            int tw_top = top + hight * 26 + 30;
            int tw_width = width;
            int tw_hight = width;
            int tw_shucount = 45;
            int tw_base = 1;
            int tw_base_high = 54;
            double tw_base_line = 54.5;
            if (_sex == "女")
            {
                tw_base = 3;
                tw_base_high = 52;
                tw_base_line = 53.5;
            }
            //绘制大标题
            g.DrawString("（WHO）0~5" + _sex + "童头围曲线图", BBfont, brush, tw_left + 6 * width, tw_top - 20);
            //string info = "曲线图生成时间："+DateTime.Now.ToString();
            //g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //绘制图片边框
            //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            ////绘制竖坐标线       
            for (int i = 0; i <= 60; i++)
            {
                if (i % 12 == 0)
                {
                    //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                    g.DrawLine(Bluepen, tw_left + tw_width * i, tw_top, tw_left + tw_width * i, tw_top + (tw_shucount + 1) * tw_hight);
                    g.DrawString((i / 12).ToString() + "岁", font, brush, tw_left + tw_width * i - tw_width / 2, tw_top + (tw_shucount + 1) * tw_hight);
                }
                else
                {
                    g.DrawLine(Sp, tw_left + tw_width * i, tw_top, tw_left + tw_width * i, tw_top + tw_shucount * tw_hight);
                    g.DrawString((i % 12).ToString(), font, brush, tw_left + tw_width * i - tw_width / 2, tw_top + tw_shucount * tw_hight);
                }
            }
            //绘制横坐标线
            for (int i = 0; i <= tw_shucount; i++)
            {
                if ((i - tw_base) % 4 == 0)
                {
                    g.DrawLine(Bluepen, tw_left, tw_top + tw_hight * i, tw_left + tw_width * 62, tw_top + tw_hight * i);
                    g.DrawString((tw_base_high - (i - tw_base) / 4 * 2).ToString(), font, brush, tw_left - tw_width * 2, tw_top + tw_hight * i);
                    g.DrawString((tw_base_high - (i - tw_base) / 4 * 2).ToString(), font, brush, tw_left + tw_width * 62, tw_top + tw_hight * i);

                }
                else
                {
                    g.DrawLine(Sp, tw_left, tw_top + tw_hight * i, tw_left + tw_width * 60, tw_top + tw_hight * i);
                }
                if (i == 0 || i == tw_shucount)
                {
                    g.DrawLine(Bluepen, tw_left, tw_top + tw_hight * i, tw_left + tw_width * 62, tw_top + tw_hight * i);
                }
            }
            g.DrawLine(Bluepen, tw_left + tw_width * 62, tw_top, tw_left + tw_width * 62, tw_top + tw_hight * tw_shucount);

            # endregion 头围底板

            # region 参考曲线

            List<Point> tw_pointlist3 = new List<Point>();//曲线图上限点
            List<Point> tw_pointlist15 = new List<Point>();//
            List<Point> tw_pointlist50 = new List<Point>();//
            List<Point> tw_pointlist85 = new List<Point>();//
            List<Point> tw_pointlist97 = new List<Point>();//

            string tw_sqls_stand = "select * from who_childstand where sex='" + _sex + "' and ptype='头围0-5' and month <= '60' order by month";
            ArrayList tw_standlist = standbll.getwhoStandard_list(tw_sqls_stand);

            foreach (WhoChildStandardObj standobj in tw_standlist)
            {
                Point tw_pointstr3 = new Point(tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p3)) * 2 * tw_hight));
                tw_pointlist3.Add(tw_pointstr3);
                Point tw_pointstr15 = new Point(tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p15)) * 2 * tw_hight));
                tw_pointlist15.Add(tw_pointstr15);
                Point tw_pointstr50 = new Point(tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p50)) * 2 * tw_hight));
                tw_pointlist50.Add(tw_pointstr50);
                Point tw_pointstr85 = new Point(tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p85)) * 2 * tw_hight));
                tw_pointlist85.Add(tw_pointstr85);
                Point tw_pointstr97 = new Point(tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p97)) * 2 * tw_hight));
                tw_pointlist97.Add(tw_pointstr97);
                if (standobj.month == 60)
                {
                    g.DrawString("3", font, brush, tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p3)) * 2 * tw_hight));
                    g.DrawString("15", font, brush, tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p15)) * 2 * tw_hight));
                    g.DrawString("50", font, brush, tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p50)) * 2 * tw_hight));
                    g.DrawString("85", font, brush, tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p85)) * 2 * tw_hight));
                    g.DrawString("97", font, brush, tw_left + standobj.month * tw_width, tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(standobj.p97)) * 2 * tw_hight));
                }
            }

            if (tw_pointlist3.Count > 1)
            {
                g.DrawLines(a, tw_pointlist3.ToArray());
            }

            if (tw_pointlist15.Count > 1)
            {
                g.DrawLines(b, tw_pointlist15.ToArray());
            }

            if (tw_pointlist50.Count > 1)
            {
                g.DrawLines(c, tw_pointlist50.ToArray());
            }

            if (tw_pointlist85.Count > 1)
            {
                g.DrawLines(b, tw_pointlist85.ToArray());
            }

            if (tw_pointlist97.Count > 1)
            {
                g.DrawLines(a, tw_pointlist97.ToArray());
            }

            #endregion 参考曲线


            //if (getage(_birthtime) <= 2)//0-2
            //{
            //    # region 身高体重底板
            //    //int h_left1 = left + width * 62 + 50;
            //    //int h_width1 = width;
            //    //int h_hight1 = 4;
            //    //top = top + hight * 26 + 45;
            //    ////绘制大标题
            //    //g.DrawString("（WHO）0~2" + _sex + "童身高/体重曲线图", BBfont, brush, h_left1 + 6 * width, top - 20);
            //    ////string info = "曲线图生成时间："+DateTime.Now.ToString();
            //    ////g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //    ////绘制图片边框
            //    ////g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            //    ////绘制竖坐标线
            //    //for (int i = 0; i <= 65; i++)
            //    //{
            //    //    if (i % 5 == 0)
            //    //    {
            //    //        //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
            //    //        g.DrawLine(Bluepen, h_left1 + h_width1 * i, top, h_left1 + h_width1 * i, top + 79 * h_hight1);
            //    //        g.DrawString((110-i).ToString(), font, brush, h_left1 + h_width1 * i - h_width1 / 2, top + 79 * h_hight1);
            //    //    }
            //    //    else
            //    //    {
            //    //        g.DrawLine(Sp, h_left1 + h_width1 * i, top, h_left1 + h_width1 * i, top + 79 * h_hight1);
            //    //        //g.DrawString(i.ToString(), font, brush, h_left1 + h_width1 * i - h_width1 / 2, top + 79 * h_hight1);
            //    //    }
            //    //    if (i == 0 || i == 65)
            //    //    {
            //    //        g.DrawLine(Bluepen, h_left1 + h_width1 * i, top, h_left1 + h_width1 * i, top + 79 * h_hight1);
            //    //    }
            //    //}

            //    ////绘制横坐标线
            //    //for (int i = 0; i <= 79; i++)
            //    //{
            //    //    if ((i - 2) % 5 == 0)
            //    //    {
            //    //        g.DrawLine(Bluepen, h_left1, top + h_hight1 * i, h_left1 + h_width1 * 65, top + h_hight1 * i);
            //    //        g.DrawString((122 - i).ToString(), font, brush, h_left1 - h_width1 * 2, top + h_hight1 * i);
            //    //        g.DrawString((122 - i).ToString(), font, brush, h_left1 + h_width1 * 67, top + h_hight1 * i);
            //    //    }
            //    //    else
            //    //    {
            //    //        g.DrawLine(Sp, h_left1, top + h_hight1 * i, h_left1 + h_width1 * 65, top + h_hight1 * i);
            //    //    }
            //    //    if (i == 0 || i == 79)
            //    //    {
            //    //        g.DrawLine(Bluepen, h_left1, top + h_hight1 * i, h_left1 + h_width1 * 65, top + h_hight1 * i);
            //    //    }
            //    //}

            //    //g.DrawLine(Bluepen, h_left1 + h_width1 * 65, top, h_left1 + h_width1 * 65, top + h_hight1 * 65);
            //    # endregion 身高体重底板
            //    # region 身高底板
            //     h_left = left + width * 62 + 50;
            //     h_width = width;
            //     h_hight = 6;
            //    top = top + hight * 26 + 45;
            //    //绘制大标题
            //    g.DrawString("（WHO）0~5" + _sex + "童身长/身高曲线图", BBfont, brush, h_left + 6 * width, top - 20);
            //    //string info = "曲线图生成时间："+DateTime.Now.ToString();
            //    //g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //    //绘制图片边框
            //    //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            //    ////绘制竖坐标线
            //    for (int i = 0; i <= 67; i++)
            //    {
            //        if (i % 5 == 0)
            //        {
            //            g.DrawLine(Bluepen, h_left + h_width * i, top, h_left + h_width * i, top + 59 * h_hight);
            //            g.DrawString((110 - i).ToString(), font, brush, h_left + h_width * i - h_width / 2, top + 59 * h_hight);
            //        }
            //        else
            //        {
            //            g.DrawLine(Sp, h_left + h_width * i, top, h_left + h_width * i, top + 59 * h_hight);
            //            //g.DrawString(i.ToString(), font, brush, h_left + h_width * i - h_width / 2, top + 58 * h_hight);
            //        }
            //        if (i == 0 || i == 67)
            //        {
            //            g.DrawLine(Bluepen, h_left + h_width * i, top, h_left + h_width * i, top + 59 * h_hight);
            //        }
            //        //g.DrawLine(Bluepen, h_left + h_width * i+2, top, h_left + h_width * i, top + 59 * h_hight);
            //    }



            //    //绘制横坐标线
            //    for (int i = 0; i <= 59; i++)
            //    {
            //        if ((i-2) % 5 == 0)
            //        {
            //            g.DrawLine(Bluepen, h_left, top + h_hight * i, h_left + h_width * 67, top + h_hight * i);
            //            g.DrawString((59 - i).ToString(), font, brush, h_left - h_width * 2, top + h_hight * i);
            //            g.DrawString((59 - i).ToString(), font, brush, h_left + h_width * 67, top + h_hight * i);
            //        }
            //        else
            //        {
            //            g.DrawLine(Sp, h_left, top + h_hight * i, h_left + h_width * 65, top + h_hight * i);
            //        }
            //        if (i == 0 || i == 59)
            //        {
            //            g.DrawLine(Bluepen, h_left, top + h_hight * i, h_left + h_width * 67, top + h_hight * i);
            //        }
            //    }
            //    //g.DrawLine(Bluepen, h_left + h_width * 67, top, h_left + h_width * 67, top + h_hight * 59);
            //    # endregion 身高底板

            //    # region 参考曲线

            //    List<Point> wh_pointlist3 = new List<Point>();//
            //    List<Point> wh_pointlist15 = new List<Point>();//
            //    List<Point> wh_pointlist50 = new List<Point>();//
            //    List<Point> wh_pointlist85 = new List<Point>();//
            //    List<Point> wh_pointlist97 = new List<Point>();//

            //    string wh_sqls_stand = "select * from who_childstand where sex='" + _sex + "' and ptype='体重' order by month";
            //    ArrayList wh_standlist = standbll.getwhoStandard_list(wh_sqls_stand);

            //    foreach (WhoChildStandardObj standobj in wh_standlist)
            //    {
            //        Point wh_pointstr3 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p3)) * h_hight));
            //        wh_pointlist3.Add(wh_pointstr3);
            //        Point wh_pointstr15 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p15)) * h_hight));
            //        wh_pointlist15.Add(wh_pointstr15);
            //        Point wh_pointstr50 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p50)) * h_hight));
            //        wh_pointlist50.Add(wh_pointstr50);
            //        Point wh_pointstr85 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p85)) * h_hight));
            //        wh_pointlist85.Add(wh_pointstr85);
            //        Point wh_pointstr97 = new Point(h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p97)) * h_hight));
            //        wh_pointlist97.Add(wh_pointstr97);
            //        if (standobj.month == 60)
            //        {
            //            g.DrawString("3", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p3)) * h_hight));
            //            g.DrawString("15", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p15)) * h_hight));
            //            g.DrawString("50", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p50)) * h_hight));
            //            g.DrawString("85", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p85)) * h_hight));
            //            g.DrawString("97", font, brush, h_left + standobj.month * h_width, top + Convert.ToInt32((59 - Convert.ToDouble(standobj.p97)) * h_hight));
            //        }
            //    }

            //    if (wh_pointlist3.Count > 1)
            //    {
            //        g.DrawLines(a, wh_pointlist3.ToArray());
            //    }

            //    if (wh_pointlist15.Count > 1)
            //    {
            //        g.DrawLines(b, wh_pointlist15.ToArray());
            //    }

            //    if (wh_pointlist50.Count > 1)
            //    {
            //        g.DrawLines(c, wh_pointlist50.ToArray());
            //    }

            //    if (wh_pointlist85.Count > 1)
            //    {
            //        g.DrawLines(b, wh_pointlist85.ToArray());
            //    }

            //    if (wh_pointlist97.Count > 1)
            //    {
            //        g.DrawLines(a, wh_pointlist97.ToArray());
            //    }

            //    # endregion 参考曲线

            //}
            //else
            //{
            #region BMI底板
            int bmi_left = left + width * 60 + 60;
            int bmi_top = top + hight * 26 + 45;
            int bmi_width = width;
            int bmi_hight = 6;
            int bmi_shucount = 59;
            int bmi_base = 2;
            int bmi_base_high = 21;
            double bmi_base_line = 21.4;

            //绘制大标题
            g.DrawString("（WHO）0~5" + _sex + "童BMI曲线图", BBfont, brush, bmi_left + 6 * width, bmi_top - 20);
            //string info = "曲线图生成时间："+DateTime.Now.ToString();
            //g.DrawString(info, Tfont, Bluebrush, 80, 25);
            //绘制图片边框
            //g.DrawRectangle(Bp, 0, 0, img.Width - 1, img.Height - 1);

            ////绘制竖坐标线       
            for (int i = 0; i <= 60; i++)
            {
                if (i % 12 == 0)
                {
                    //g.DrawLine(BSp, left + width * i, top, left + width * i, top + 90 * hight);
                    g.DrawLine(Bluepen, bmi_left + bmi_width * i, bmi_top, bmi_left + bmi_width * i, bmi_top + (bmi_shucount + 1) * bmi_hight);
                    g.DrawString((i / 12).ToString() + "岁", font, brush, bmi_left + bmi_width * i - bmi_width / 2, bmi_top + (bmi_shucount + 1) * bmi_hight);
                }
                else
                {
                    g.DrawLine(Sp, bmi_left + bmi_width * i, bmi_top, bmi_left + bmi_width * i, bmi_top + bmi_shucount * bmi_hight);
                    g.DrawString((i % 12).ToString(), font, brush, bmi_left + bmi_width * i - bmi_width / 2, bmi_top + bmi_shucount * bmi_hight);
                }
            }
            //绘制横坐标线
            for (int i = 0; i <= bmi_shucount; i++)
            {
                if ((i - bmi_base) % 5 == 0)
                {
                    g.DrawLine(Bluepen, bmi_left, bmi_top + bmi_hight * i, bmi_left + bmi_width * 62, bmi_top + bmi_hight * i);
                    g.DrawString((bmi_base_high - (i - bmi_base) / 5).ToString(), font, brush, bmi_left - bmi_width * 2, bmi_top + bmi_hight * i);
                    g.DrawString((bmi_base_high - (i - bmi_base) / 5).ToString(), font, brush, bmi_left + bmi_width * 62, bmi_top + bmi_hight * i);

                }
                else
                {
                    g.DrawLine(Sp, bmi_left, bmi_top + bmi_hight * i, bmi_left + bmi_width * 60, bmi_top + bmi_hight * i);
                }
                if (i == 0 || i == bmi_shucount)
                {
                    g.DrawLine(Bluepen, bmi_left, bmi_top + bmi_hight * i, bmi_left + bmi_width * 62, bmi_top + bmi_hight * i);
                }
            }
            g.DrawLine(Bluepen, bmi_left + bmi_width * 62, bmi_top, bmi_left + bmi_width * 62, bmi_top + bmi_hight * bmi_shucount);
            #endregion BMI底板

            #region 参考曲线

            List<Point> bmi_pointlist3 = new List<Point>();//曲线图上限点
            List<Point> bmi_pointlist15 = new List<Point>();//
            List<Point> bmi_pointlist50 = new List<Point>();//
            List<Point> bmi_pointlist85 = new List<Point>();//
            List<Point> bmi_pointlist97 = new List<Point>();//

            string bmi_sqls_stand = "select * from who_childstand where sex='" + _sex + "' and (ptype='BMI0-2' or ptype='BMI2-5') and month <= '60' order by month";
            ArrayList bmi_standlist = standbll.getwhoStandard_list(bmi_sqls_stand);

            foreach (WhoChildStandardObj standobj in bmi_standlist)
            {
                Point bmi_pointstr3 = new Point(bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p3)) * 5 * bmi_hight));
                bmi_pointlist3.Add(bmi_pointstr3);
                Point bmi_pointstr15 = new Point(bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p15)) * 5 * bmi_hight));
                bmi_pointlist15.Add(bmi_pointstr15);
                Point bmi_pointstr50 = new Point(bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p50)) * 5 * bmi_hight));
                bmi_pointlist50.Add(bmi_pointstr50);
                Point bmi_pointstr85 = new Point(bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p85)) * 5 * bmi_hight));
                bmi_pointlist85.Add(bmi_pointstr85);
                Point bmi_pointstr97 = new Point(bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p97)) * 5 * bmi_hight));
                bmi_pointlist97.Add(bmi_pointstr97);
                if (standobj.month == 60)
                {
                    g.DrawString("3", font, brush, bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p3)) * 5 * bmi_hight));
                    g.DrawString("15", font, brush, bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p15)) * 5 * bmi_hight));
                    g.DrawString("50", font, brush, bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p50)) * 5 * bmi_hight));
                    g.DrawString("85", font, brush, bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p85)) * 5 * bmi_hight));
                    g.DrawString("97", font, brush, bmi_left + standobj.month * bmi_width, bmi_top + Convert.ToInt32((bmi_base_line - Convert.ToDouble(standobj.p97)) * 5 * bmi_hight));
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

            #endregion 参考曲线

            #region 实际曲线
            List<Point> h_factlist = new List<Point>();//身高曲线图实际值点
            List<Point> w_factlist = new List<Point>();//体重曲线图实际值点
            List<Point> tw_factlist = new List<Point>();//头围曲线图实际值点
            List<Point> bmi_factlist = new List<Point>();//bmi曲线图实际值点

            List<Point> h_factlist1 = new List<Point>();//身高曲线图实际值点
            List<Point> w_factlist1 = new List<Point>();//体重曲线图实际值点
            List<Point> tw_factlist1 = new List<Point>();//头围曲线图实际值点
            List<Point> bmi_factlist1 = new List<Point>();//bmi曲线图实际值点

            List<Point> h_factlist2 = new List<Point>();//身高曲线图实际值点
            List<Point> w_factlist2 = new List<Point>();//体重曲线图实际值点
                                                        //List<Point> tw_factlist1 = new List<Point>();//头围曲线图实际值点
            List<Point> bmi_factlist2 = new List<Point>();//bmi曲线图实际值点

            //出生体重、身长、bmi图
            if (!String.IsNullOrEmpty(_height))
            {
                Point h_factstr = new Point(h_left, top + Convert.ToInt32((122 - Convert.ToDouble(_height)) * h_hight));
                g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);
                h_factlist.Add(h_factstr);
            }
            if (!String.IsNullOrEmpty(_weight))
            {
                Point w_factstr = new Point(left, top + Convert.ToInt32((25 - Convert.ToDouble(_weight)) * hight));
                g.FillEllipse(bbrush, w_factstr.X - 2, w_factstr.Y - 2, 4, 4);
                w_factlist.Add(w_factstr);
            }

            if (!String.IsNullOrEmpty(_weight) && !String.IsNullOrEmpty(_height))
            {
                Point bmi_factstr = new Point(bmi_left, bmi_top + Convert.ToInt32((bmi_base_line - getBmi(_height, _weight)) * 5 * bmi_hight));
                g.FillEllipse(bbrush, bmi_factstr.X - 2, bmi_factstr.Y - 2, 4, 4);
                bmi_factlist.Add(bmi_factstr);
            }
            string sqls_check = "select datediff(month,'" + _birthtime + "',checkday) as yf,* from tb_childcheck where childId=" + _womeninfo.cd_id + " and datediff(month,'" + _birthtime + "',checkday)<=60 and checkDay!='0001-01-01 00:00:00' order by datediff(day,'" + _birthtime + "',checkday) asc";
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
                    if (!String.IsNullOrEmpty(_week) && !String.IsNullOrEmpty(_ispre))
                    {
                        int result;
                        if (int.TryParse(_week, out result))
                        {
                            int yz = Convert.ToInt32(_week);
                            if (_ispre == "1" && yz < 37)
                            {
                                ispremonth = factmonth - (40 - yz) / 4;
                                if (!String.IsNullOrEmpty(checkobj.CheckHeight))
                                {
                                    Point h_factstr = new Point(h_left + Convert.ToInt32(ispremonth * h_width), top + Convert.ToInt32((122 - Convert.ToDouble(checkobj.CheckHeight)) * h_hight));
                                    g.FillEllipse(prebrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);
                                    h_factlist1.Add(h_factstr);
                                }

                                if (!String.IsNullOrEmpty(checkobj.CheckWeight))
                                {
                                    Point w_factstr = new Point(left + Convert.ToInt32(ispremonth * width), top + Convert.ToInt32((25 - Convert.ToDouble(checkobj.CheckWeight)) * hight));
                                    g.FillEllipse(prebrush, w_factstr.X - 2, w_factstr.Y - 2, 4, 4);
                                    w_factlist1.Add(w_factstr);
                                }

                                if (!String.IsNullOrEmpty(checkobj.CheckTouwei))
                                {
                                    Point tw_factstr = new Point(tw_left + Convert.ToInt32(ispremonth * tw_width), tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(checkobj.CheckTouwei)) * 2 * tw_hight));
                                    g.FillEllipse(prebrush, tw_factstr.X - 2, tw_factstr.Y - 2, 4, 4);
                                    tw_factlist1.Add(tw_factstr);
                                }

                                if (!String.IsNullOrEmpty(checkobj.CheckHeight) && !String.IsNullOrEmpty(checkobj.CheckWeight))
                                {
                                    Point bmi_factstr = new Point(bmi_left + Convert.ToInt32(ispremonth * bmi_width), bmi_top + Convert.ToInt32((bmi_base_line - getBmi(checkobj.CheckHeight, checkobj.CheckWeight)) * 5 * bmi_hight));
                                    g.FillEllipse(prebrush, bmi_factstr.X - 2, bmi_factstr.Y - 2, 4, 4);
                                    bmi_factlist1.Add(bmi_factstr);
                                }

                                if (h_factlist1.Count > 1)
                                {
                                    g.DrawLines(Prep, h_factlist1.ToArray());
                                }
                                if (w_factlist1.Count > 1)
                                {
                                    g.DrawLines(Prep, w_factlist1.ToArray());
                                }
                                if (tw_factlist1.Count > 1)
                                {
                                    g.DrawLines(Prep, tw_factlist1.ToArray());
                                }
                                if (bmi_factlist1.Count > 1)
                                {
                                    g.DrawLines(Prep, bmi_factlist1.ToArray());
                                }
                            }
                        }
                    }
                    #endregion 早产儿



                    if (!String.IsNullOrEmpty(checkobj.CheckHeight))
                    {
                        Point h_factstr = new Point(h_left + Convert.ToInt32(factmonth * h_width), top + Convert.ToInt32((122 - Convert.ToDouble(checkobj.CheckHeight)) * h_hight));
                        g.FillEllipse(bbrush, h_factstr.X - 2, h_factstr.Y - 2, 4, 4);
                        h_factlist.Add(h_factstr);
                    }

                    if (!String.IsNullOrEmpty(checkobj.CheckWeight))
                    {
                        Point w_factstr = new Point(left + Convert.ToInt32(factmonth * width), top + Convert.ToInt32((25 - Convert.ToDouble(checkobj.CheckWeight)) * hight));
                        g.FillEllipse(bbrush, w_factstr.X - 2, w_factstr.Y - 2, 4, 4);
                        w_factlist.Add(w_factstr);
                    }

                    if (!String.IsNullOrEmpty(checkobj.CheckTouwei))
                    {
                        Point tw_factstr = new Point(tw_left + Convert.ToInt32(factmonth * tw_width), tw_top + Convert.ToInt32((tw_base_line - Convert.ToDouble(checkobj.CheckTouwei)) * 2 * tw_hight));
                        g.FillEllipse(bbrush, tw_factstr.X - 2, tw_factstr.Y - 2, 4, 4);
                        tw_factlist.Add(tw_factstr);
                    }

                    if (!String.IsNullOrEmpty(checkobj.CheckHeight) && !String.IsNullOrEmpty(checkobj.CheckWeight))
                    {
                        Point bmi_factstr = new Point(bmi_left + Convert.ToInt32(factmonth * bmi_width), bmi_top + Convert.ToInt32((bmi_base_line - getBmi(checkobj.CheckHeight, checkobj.CheckWeight)) * 5 * bmi_hight));
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
                if (tw_factlist.Count > 1)
                {
                    g.DrawLines(Bp, tw_factlist.ToArray());
                }
                if (bmi_factlist.Count > 1)
                {
                    g.DrawLines(Bp, bmi_factlist.ToArray());
                }

            }
            #endregion 实际曲线
            //}

            pictureBox1.Image = img;


        }
        # endregion WHO

        # region WHO_5-19岁
        private void tongji_WHO_Paint519()
        {
            # region 属性设置
            int left = 30;
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
                    g.DrawString((60 - i).ToString(), font, brush, w_left - w_width - 5, w_top + hight * (i - 13));
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
            if (standlist == null)
            {
                return;
            }
            foreach (WhoChildStandardObj standobj in standlist)
            {

                Point w_pointstr3 = new Point(w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p3)) * hight));
                w_pointlist3.Add(w_pointstr3);
                Point w_pointstr15 = new Point(w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p15)) * hight));
                w_pointlist15.Add(w_pointstr15);
                Point w_pointstr50 = new Point(w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p50)) * hight));
                w_pointlist50.Add(w_pointstr50);
                Point w_pointstr85 = new Point(w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p85)) * hight));
                w_pointlist85.Add(w_pointstr85);
                Point w_pointstr97 = new Point(w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p97)) * hight));
                w_pointlist97.Add(w_pointstr97);
                if ((standobj.month - 60) == 60)
                {
                    g.DrawString("3", font, brush, w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p3)) * hight));
                    g.DrawString("15", font, brush, w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p15)) * hight));
                    g.DrawString("50", font, brush, w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p50)) * hight));
                    g.DrawString("85", font, brush, w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p85)) * hight));
                    g.DrawString("97", font, brush, w_left + (standobj.month - 60) * w_width, w_top + Convert.ToInt32((47 - Convert.ToDouble(standobj.p97)) * hight));
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
            g.DrawString("（WHO）5~19" + _sex + "童身长/身高曲线图", BBfont, brush, h_left + 6 * h_width + 5, h_top - 20);
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
                    g.DrawLine(Bluepen, h_left + h_width * (i - 60), h_top, h_left + h_width * (i - 60), h_top + 102 * h_hight);
                    g.DrawString((i / 12).ToString() + "岁", font, brush, h_left + h_width * (i - 60) - h_width / 2, h_top + 102 * h_hight);
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
                    g.DrawString((290 - i).ToString(), font, brush, h_left - h_width * 5, h_top + h_hight * (i - 95));
                    g.DrawString((290 - i).ToString(), font, brush, h_left + h_width * 174, h_top + h_hight * (i - 95));

                }
                else if (i % 5 == 0)
                {
                    g.DrawLine(Sp, h_left, h_top + h_hight * (i - 95), h_left + h_width * 168, h_top + h_hight * (i - 95));
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
                else if ((i - 60) % 3 == 0)
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
                    if (((i - 120) / 10) % 2 == 0)
                    {
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
                    g.DrawString("3", font, brush, bmi_left + (standobj.month - 60) * bmi_width, bmi_top + Convert.ToInt32((30 - Convert.ToDouble(standobj.p3)) * 10 * bmi_hight));
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

            string sqls_check = "select datediff(month,'" + _birthtime + "',checkday) as yf,* from tb_childcheck where childId=" + _womeninfo.cd_id + " and datediff(month,'" + _birthtime + "',checkday)>60 and checkDay!='0001-01-01 00:00:00' order by datediff(day,'" + _birthtime + "',checkday) asc";
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
                        Point h_factstr = new Point(h_left + Convert.ToInt32((factmonth - 60) * h_width), h_top + Convert.ToInt32((195 - Convert.ToDouble(checkobj.CheckHeight)) * h_hight));
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
        # endregion WHO_5-19岁
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
                tongji_BMI_Paint();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                if (comboBox1.SelectedIndex == 0) {
                    tongji_WHO_Paint();
                }
                else if(comboBox1.SelectedIndex == 1)
                {
                    tongji_WHO_Paint519();
                }
                
            }
        }


        private void ckb_isPre_Click(object sender, EventArgs e)
        {
            string sqls = "";
            if (ckb_isPre.Checked)
            {
                sqls = "update tb_childbase set ispre='1' where id=" + _womeninfo.cd_id + "";
                _ispre = "1";
            }
            else
            {
                sqls = "update tb_childbase set ispre='0' where id=" + _womeninfo.cd_id + "";
                _ispre = "0";
            }
            bll.updaterecord(sqls);
            //tongji_BMI_Paint();

            tongji_WHO_Paint();
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

            string sqls_check = "select (" + Convert.ToInt32(_week) * 7 + " + datediff(day, '" + _birthtime + "', checkday)) as yf,* from tb_childcheck where childid = " + _womeninfo.cd_id + "";

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
                        if (radioButton2.Checked && comboBox1.SelectedIndex == 0)
                        {
                            tongji_WHO_Paint();
                        }
                        else if (radioButton2.Checked && comboBox1.SelectedIndex == 1)
                        {
                            tongji_WHO_Paint519();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("该儿童不属于极低早产儿");
                    radioButton2.Checked = true;
                    if (radioButton2.Checked && comboBox1.SelectedIndex == 0)
                    {
                        tongji_WHO_Paint();
                    }
                    else if (radioButton2.Checked && comboBox1.SelectedIndex == 1)
                    {
                        tongji_WHO_Paint519();
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked && comboBox1.SelectedIndex == 0)
            {
                tongji_WHO_Paint();
            }
            else if(radioButton2.Checked && comboBox1.SelectedIndex == 1)
            {
                tongji_WHO_Paint519();
            }
        }
    }
}
