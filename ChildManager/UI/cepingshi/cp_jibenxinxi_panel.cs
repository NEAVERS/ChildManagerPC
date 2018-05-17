using System;
using System.Windows.Forms;
using ChildManager.UI.jibenluru;
using YCF.BLL.cepingshi;
using System.Collections.Generic;
using YCF.Model;
using YCF.Common;
using YCF.Model.NotMaps;

namespace ChildManager.UI.cepingshi
{
    public partial class cp_jibenxinxi_panel : UserControl
    {
        cp_WomenInfo _cpwomeninfo = null;
        cp_yy_tabbll yybll = new cp_yy_tabbll();
        cp_yy_coubll couyybll = new cp_yy_coubll();
        HisPatientInfo _hisPatientInfo = null;

        public cp_jibenxinxi_panel(cp_WomenInfo cpwomeninfo)
        {
            InitializeComponent();
            _cpwomeninfo = cpwomeninfo;
        }
        public cp_jibenxinxi_panel(cp_WomenInfo cpwomeninfo, HisPatientInfo hisPatientInfo)
        {
            InitializeComponent();
            _cpwomeninfo = cpwomeninfo;
            _hisPatientInfo = hisPatientInfo;
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            WomenInfo womeninfo = new WomenInfo();
            womeninfo.cd_id = _cpwomeninfo.cd_id;
            if (_hisPatientInfo == null)
            {
                jibenxinxi_panel jibenpanel = new jibenxinxi_panel(womeninfo);
                jibenpanel.buttonX1.Visible = false; //隐藏功能按钮
                jibenpanel.buttonX3.Visible = false; //jibenpanel.groupPanel4.Visible = false;
                jibenpanel.buttonX6.Visible = false; //jibenpanel.groupPanel6.Visible = false;//隐藏不需要显示的项目
                jibenpanel.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(jibenpanel);
            }
            else
            {
                jibenxinxi_panel jibenpanel = new jibenxinxi_panel(womeninfo, _hisPatientInfo);
                jibenpanel.buttonX1.Visible = true; //显示保存功能按钮
                jibenpanel.buttonX3.Visible = false; //隐藏本院新生儿导入功能按钮
                jibenpanel.buttonX6.Visible = false; //隐藏删除功能按钮
                jibenpanel.Dock = DockStyle.Fill;
                this.panel1.Controls.Add(jibenpanel);
            }
            
        }
        
    }
}
