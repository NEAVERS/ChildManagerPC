using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ChildManager.UI.jibenluru;
using YCF.Common;
using ChildManager.UI;
using ChildManager.UI.childSetInfo;
using login.UI;
using ChildManager.UI.tongji;
using ChildManager.UI.versionmanage;

namespace ChildManager
{
    public partial class ChildMainForm : Office2007Form 
    {
        public ChildMainForm()
        {
            InitializeComponent();
            this.Text = "重庆跃医通科技-儿童保健管理系统V1.0  当前用户:" + globalInfoClass.UserName;
        }

        private void ChildMainForm_Load(object sender, EventArgs e)
        {
            //清空默认的Tab
            superTabControl1.Tabs.Clear();//默认清空所有tab
            tijianluruToolStripMenuItem_Click(null, null);//默认点击新建档案
          //  this.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - 1250, Screen.PrimaryScreen.Bounds.Height - 800);
        }

        private void tijianluruToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            SetMdiForm("儿保建卡", typeof(PanelyibanxinxiMain));
        }
        /// <summary>
        /// 创建或者显示一个多文档界面页面
        /// </summary>
        /// <param name="caption">窗体标题</param>
        /// <param name="formType">窗体类型</param>
        public void SetMdiForm(string caption, Type formType)
        {
            bool IsOpened = false;

            //遍历现有的Tab页面，如果存在，那么设置为选中即可
            foreach (SuperTabItem tabitem in superTabControl1.Tabs)
            {
                if (tabitem.Name == caption)
                {
                    superTabControl1.SelectedTab = tabitem;
                    IsOpened = true;
                    break;
                }
            }

            //如果在现有Tab页面中没有找到，那么就要初始化了Tab页面了
            if (!IsOpened)
            {
                //为了方便管理，调用LoadMdiForm函数来创建一个新的窗体，并作为MDI的子窗体
                //然后分配给SuperTab控件，创建一个SuperTabItem并显示
                UserControl form = Activator.CreateInstance(formType) as UserControl;
                SuperTabItem tabItem = superTabControl1.CreateTab(caption);
                tabItem.Name = caption;
                tabItem.Text = caption;


              
                form.Visible = true;
                form.Dock = DockStyle.Fill;
                form.AutoScroll = true;
                //tabItem.Icon = form.Icon;
                tabItem.AttachedControl.Controls.Add(form);

                superTabControl1.SelectedTab = tabItem;
            }
        }

        /// <summary>
        /// 刷新界面页面
        /// </summary>
        /// <param name="caption">窗体标题</param>
        /// <param name="formType">窗体类型</param>
        public void updateMdiForm(string caption, Type formType)
        {
            bool IsOpened = false;

            //遍历现有的Tab页面，如果存在，那么设置为选中即可
            foreach (SuperTabItem tabitem in superTabControl1.Tabs)
            {
                if (tabitem.Name == caption)
                {
                    superTabControl1.SelectedTab = tabitem;
                    //tabitem.Dispose();
                    IsOpened = true;
                    UserControl form = Activator.CreateInstance(formType) as UserControl;
                    //form.Dispose();//控件使用完后Dispose。
                    tabitem.AttachedControl.Controls[0].Dispose();
                    tabitem.AttachedControl.Controls.Clear();

                    form.Visible = true;
                    form.Dock = DockStyle.Fill;
                    form.AutoScroll = true;
                    tabitem.AttachedControl.Controls.Add(form);
                    
                    break;
                }
            }

            //如果在现有Tab页面中没有找到，那么就要初始化了Tab页面了
            if (!IsOpened)
            {
                //为了方便管理，调用LoadMdiForm函数来创建一个新的窗体，并作为MDI的子窗体
                //然后分配给SuperTab控件，创建一个SuperTabItem并显示
                UserControl form = Activator.CreateInstance(formType) as UserControl;
                SuperTabItem tabItem = superTabControl1.CreateTab(caption);
                tabItem.Name = caption;
                tabItem.Text = caption;



                form.Visible = true;
                form.Dock = DockStyle.Fill;
                form.AutoScroll = true;
                //tabItem.Icon = form.Icon;
                tabItem.AttachedControl.Controls.Add(form);

                superTabControl1.SelectedTab = tabItem;
            }
        }

        private void tsb_jiben_Click(object sender, EventArgs e)
        {
            
        }

        private void gaoweiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("高危儿童管理", typeof(PanelgaoweiMain));
        }

        private void yingyangbuliangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("营养不良儿童管理", typeof(PanelyingyangMain));
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("版本管理", typeof(VersionList));
        }

        private void ChildMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("退出儿童保健管理系统？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void tologinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRelogin relogin = new FrmRelogin();
            relogin.ShowDialog();
            if (relogin.DialogResult == DialogResult.OK)
            {
                this.Text = "重庆跃医通科技-儿童保健管理系统V1.0  当前用户:" + globalInfoClass.UserName;
            }
        }

        private void changepasswordMenuItem_Click(object sender, EventArgs e)
        {
            FrmPassWord frmword = new FrmPassWord();
            frmword.ShowDialog();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tijiannianlingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetCheckAge checkage = new FrmSetCheckAge();
            checkage.ShowDialog();
            if (checkage.DialogResult == DialogResult.OK)
            {
                SetMdiForm("儿保建卡", typeof(PanelyibanxinxiMain));
            }
        }

        private void mobanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("模板设置", typeof(Panel_moban_manage));
        }

        private void TjmanageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TjhealthToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tjtijianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("健康检查统计", typeof(tj_tijian));
        }

        private void childRowspan_Click(object sender, EventArgs e)
        {
            //Paneltsb_rowspanInfo合并
            //SetMdiForm("儿童合并信息", typeof(Paneltsb_rowspanInfo));
            Paneltsb_rowspanInfo dialog = new Paneltsb_rowspanInfo();
            dialog.ShowDialog();
        }

        private void tsb_xinlixingwei_Click(object sender, EventArgs e)
        {
            //PanelxinlijibingMain.
            SetMdiForm("心里行为发育异常", typeof(PanelxinlijibingMain));
        }

        private void tsb_jibingguanli_Click(object sender, EventArgs e)
        {
            SetMdiForm("疾病管理",typeof(Paneljibingguanli));
        }

        private void tsb_gwrenci_Click(object sender, EventArgs e)
        {
            SetMdiForm("检查人次统计", typeof(tj_renci));
        }

        private void diquToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("地区统计", typeof(tj_diqu));
        }

        private void jibingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("疾病统计", typeof(tj_jibing));
        }

        private void yingjianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("应体检儿童", typeof(tj_yingtijian));
        }

        private void tjagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMdiForm("儿童年龄段统计", typeof(tj_ages));
        }
    }
}
