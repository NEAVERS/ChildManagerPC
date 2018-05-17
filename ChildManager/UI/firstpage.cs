using ChildManager.UI.cepingshi;
using ChildManager.UI.jibenluru;
using ChildManager.UI.tongji;
using System;
using System.Windows.Forms;

namespace ChildManager.UI
{
    public partial class firstpage : UserControl
    {
        public firstpage()
        {
            InitializeComponent();
            //加载第一个图标
            this.pnlHomePage.Controls.Add(pnldangan);//添加图标
            this.pnldangan.Visible = true;
            this.pnldangan.Left = (pnlHomePage.Controls.Count - 1) * (pnldangan.Width + 10);//容器与左边缘的数据
            this.pnldangan.Top = 0;//容器与顶端的距离
            //加载第二个图标
            this.pnlHomePage.Controls.Add(pnlbingfang);
            this.pnlbingfang.Visible = true;
            this.pnlbingfang.Left = (pnlHomePage.Controls.Count - 1) * (pnlbingfang.Width + 10);
            this.pnlbingfang.Top = 0;
            //加载第三个图标
            this.pnlHomePage.Controls.Add(pnlCePing);
            this.pnlCePing.Visible = true;
            this.pnlCePing.Left = (pnlHomePage.Controls.Count - 1) * (pnlCePing.Width + 10);
            this.pnlCePing.Top = 0;
            //加载第四个图标
            this.pnlHomePage.Controls.Add(pnltongji);
            this.pnltongji.Visible = true;
            this.pnltongji.Left = (pnlHomePage.Controls.Count - 1) * (pnltongji.Width + 10);
            this.pnltongji.Top = 0;//容器与顶端的距离
            //加载第五个图标
            this.pnlHomePage.Controls.Add(pnlxuexiao);
            this.pnlxuexiao.Visible = true;
            this.pnlxuexiao.Left = (pnlHomePage.Controls.Count - 1) * (pnlxuexiao.Width + 10);
            this.pnlxuexiao.Top = 0;
            //显示图标容器在主容器的显示位置
            pnlHomePage.Left = (this.Width - pnlHomePage.Controls.Count * pnldangan.Width - (pnlHomePage.Controls.Count - 1) * 10) / 2;
            pnlHomePage.Top = (this.Height - pnldangan.Height) / 2;
        }

        private void pbInpNurseStation_Click(object sender, EventArgs e)
        {
            frmMain mianform = this.ParentForm as frmMain;
            mianform.SetMdiForm("护士工作站", typeof(NursInfo));
        }

        private void pbNursingManagement_Click(object sender, EventArgs e)
        {
            frmMain mianform = this.ParentForm as frmMain;
            mianform.SetMdiForm("医生工作站", typeof(WomenInfo));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmMain mianform = this.ParentForm as frmMain;
            mianform.SetMdiForm("儿童专案管理", typeof(WomenInfo));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmMain mianform = this.ParentForm as frmMain;
            mianform.SetMdiForm("统计报表", typeof(tongji_MainInfo));
        }

        private void firstpage_Resize(object sender, EventArgs e)
        {
            pnlHomePage.Left = (this.Width - pnlHomePage.Controls.Count * pnldangan.Width - (pnlHomePage.Controls.Count - 1) * 10) / 2;
            pnlHomePage.Top = (this.Height - pnldangan.Height) / 2;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmMain mianform = this.ParentForm as frmMain;
            mianform.SetMdiForm("测评室工作站", typeof(cp_WomenInfo));
        }
    }
}
