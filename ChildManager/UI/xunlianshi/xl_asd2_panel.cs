using System;
using System.Windows.Forms;
using ChildManager.UI.cepingshi;

namespace ChildManager.UI.xunlianshi
{
    public partial class xl_asd2_panel : UserControl
    {
        xl_WomenInfo _xlwomeninfo = null;

        public xl_asd2_panel(xl_WomenInfo xlwomeninfo)
        {
            InitializeComponent();
            _xlwomeninfo = xlwomeninfo;
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            cp_WomenInfo cpwomeninfo = new cp_WomenInfo();
            cpwomeninfo.cd_id = _xlwomeninfo.cd_id;
            cp_asd2_panel cppanel = new cp_asd2_panel(cpwomeninfo);
            cppanel.buttonX1.Visible = false;
            cppanel.buttonX3.Visible = false;
            cppanel.buttonX2.Visible = false;
            cppanel.buttonX4.Visible = false;//隐藏功能按钮
            cppanel.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(cppanel);
        }
        
    }
}
