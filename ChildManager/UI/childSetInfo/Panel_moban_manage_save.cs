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
using ChildManager.Model.childSetInfo;

namespace ChildManager.UI.childSetInfo
{
    public partial class Panel_moban_manage_save : Form
    {
        private Panel_moban_manage _mobanpanel;
        private MobanManageBll mobanbll = new MobanManageBll();
        private mobanManageObj _mobanobj;
        public Panel_moban_manage_save(Panel_moban_manage mobanpanel, mobanManageObj mobanobj)
        {
            InitializeComponent();

            _mobanpanel = mobanpanel;
            _mobanobj = mobanobj;
        }


        private void Panel_moban_manage_save_Load(object sender, EventArgs e)
        {
            textBoxX5.Text = _mobanobj.m_name;
            comboBox1.Text = _mobanobj.m_type;
            textBoxX2.Text = _mobanobj.m_content;
            textBoxX4.Text = _mobanobj.yf;
            textBoxX1.Text = _mobanobj.yfend;
            
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            mobanManageObj mobanobj = getMobanObj();
            if (mobanbll.saveMoban(mobanobj))
            {
                MessageBox.Show("保存成功！");
                this.Close();
                _mobanpanel.refreshRecordList();
            }
            else
            {
                MessageBox.Show("保存失败！请联系管理员");
            }
        }

        private mobanManageObj getMobanObj()
        {
            mobanManageObj mobanobj = new mobanManageObj();
            mobanobj.id = _mobanobj.id;
            mobanobj.m_name = textBoxX5.Text;
            mobanobj.m_type = comboBox1.Text;
            mobanobj.m_content = textBoxX2.Text;
            mobanobj.yf = textBoxX4.Text;
            mobanobj.yfend = textBoxX1.Text;
            return mobanobj;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
