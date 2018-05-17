using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.Model;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.UI.xunlianshi;
using YCF.BLL.xunlianshi;
using YCF.Model;
using YCF.Common;
using YCF.BLL.yuyue;

namespace ChildManager.UI.yuyue
{
    public partial class yy_pz_xg_panel : Form
    {
        private yy_pz_panel _yypzpanel;
        private yy_pz_tabbll yypzbll = new yy_pz_tabbll();
        private YY_PZ_TAB _pzobj;
        public yy_pz_xg_panel(yy_pz_panel yypzpanel, YY_PZ_TAB pzobj)
        {
            InitializeComponent();

            _pzobj = pzobj;
            _yypzpanel = yypzpanel;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            YY_PZ_TAB yypzobj = GetObj();
            if (yypzbll.SaveOrUpdate(yypzobj))
            {
                MessageBox.Show("保存成功！");
                this.Close();
                _yypzpanel.RefreshCode();
            }
            else
            {
                MessageBox.Show("保存失败！请联系管理员");
            }
        }

        private YY_PZ_TAB GetObj()
        {
            YY_PZ_TAB yypzobj = CommonHelper.GetObj<YY_PZ_TAB>(this.Controls);
            if(_pzobj!=null)
            {
                yypzobj.ID = _pzobj.ID;
            }
            return yypzobj;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            if(_pzobj!=null)
            {
                CommonHelper.setForm(_pzobj, this.Controls);
            }
        }
    }
}
