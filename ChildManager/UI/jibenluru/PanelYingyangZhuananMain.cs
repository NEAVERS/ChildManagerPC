using System;
using System.Windows.Forms;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL.ChildBaseInfo;
using login.UI;

namespace ChildManager.UI.jibenluru
{
    public partial class PanelYingyangZhuananMain : Form
    {
        private ChildBaseInfoBll childBaseInfobll = new ChildBaseInfoBll();//儿童建档基本信息业务处理类
        public ChildBaseInfoObj obj = new ChildBaseInfoObj();//儿童基本信息类

        Panel_gaowei_zhuanan gaoweizhuanan = null;
        Panel_goulou_zhuanan goulouzhuanan = null;
        Panel_pinxue_zhuanan pinxuezhuanan = null;
        Panel_yingyang_zhuanan yingyangzhuanan = null;

        public PanelYingyangZhuananMain(ChildBaseInfoObj baseinfoobj)
        {
            InitializeComponent();
            obj = baseinfoobj;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (yingyangzhuanan == null)
                {
                    yingyangzhuanan = new Panel_yingyang_zhuanan(obj);
                    tabPage1.Controls.Clear();
                    tabPage1.Controls.Add(yingyangzhuanan);
                }
                
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                if (pinxuezhuanan == null)
                {
                    pinxuezhuanan = new Panel_pinxue_zhuanan(obj);
                    tabPage2.Controls.Clear();
                    tabPage2.Controls.Add(pinxuezhuanan);
                }
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                if (goulouzhuanan == null)
                {
                    goulouzhuanan = new Panel_goulou_zhuanan(obj);
                    tabPage3.Controls.Clear();
                    tabPage3.Controls.Add(goulouzhuanan);
                }
            }
        }


        private void PanelYingyangZhuananMain_Load(object sender, EventArgs e)
        {
            tabControl1_Click(tabPage1,e);
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {

        }

        private void buttonX6_Click(object sender, EventArgs e)
        {

        }

        private void btnPrinter_Click(object sender, EventArgs e)
        {

        }

        private void btnPriview_Click(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click(object sender, EventArgs e)
        {

        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                if (yingyangzhuanan != null)
                {
                    yingyangzhuanan.saveyingyangzhuanan();
                }
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                if (pinxuezhuanan != null)
                {
                    pinxuezhuanan.savepinxuezhuanan();
                }
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                if (goulouzhuanan != null)
                {
                    goulouzhuanan.savegoulouzhuanan();
                }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                yingyangzhuanan.btnprint(true);
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                pinxuezhuanan.btnprint(true);
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                goulouzhuanan.btnprint(true);
            }
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                yingyangzhuanan.btnprint(false);
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                pinxuezhuanan.btnprint(false);
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                goulouzhuanan.btnprint(false);
            }
        }
    }
}
