using System;
using System.Windows.Forms;
using ChildManager.UI.cepingshi;

namespace ChildManager.UI.xunlianshi
{
    public partial class xl_gesell_panel : UserControl
    {
        xl_WomenInfo _xlwomeninfo = null;

        public xl_gesell_panel(xl_WomenInfo xlwomeninfo)
        {
            InitializeComponent();
            _xlwomeninfo = xlwomeninfo;
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            cp_WomenInfo cpwomeninfo = new cp_WomenInfo();
            cpwomeninfo.cd_id = _xlwomeninfo.cd_id;
            cp_gesell_panel cppanel = new cp_gesell_panel(cpwomeninfo);
            cppanel.buttonX1.Visible = false;
            cppanel.buttonX3.Visible = false;
            cppanel.buttonX2.Visible = false;
            cppanel.buttonX4.Visible = false;//隐藏功能按钮
            cppanel.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(cppanel);
        }
        
    }
}
