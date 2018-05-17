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

namespace ChildManager.UI
{
    public partial class Panel_goulou_zhuanan_add : Form
    {
        private Panel_goulou_zhuanan _goulouzhuananpanel;
        private ChildGoulougeanRecordBll goulourecordbll = new ChildGoulougeanRecordBll();
        private ChildCheckObj _checkobj;
        public Panel_goulou_zhuanan_add(Panel_goulou_zhuanan goulouzhuananpanel, ChildCheckObj checkobj)
        {
            InitializeComponent();

            _goulouzhuananpanel = goulouzhuananpanel;
            _checkobj = checkobj;
        }


        private void Paneltsb__goulou_zhuanan_add_Load(object sender, EventArgs e)
        {
            textBoxX4.Text = _checkobj.CheckDay;
            textBoxX5.Text = _checkobj.CheckAge;
            textBoxX8.Text = _checkobj.DoctorName;
            textBoxX12.Text = _checkobj.goulourecordobj.huwai;
            textBoxX1.Text = _checkobj.goulourecordobj.problem;
            textBoxX7.Text = _checkobj.goulourecordobj.vitdname;
            textBoxX11.Text = _checkobj.goulourecordobj.vitdliang;
            textBoxX2.Text = _checkobj.goulourecordobj.zhidao;
            
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            ChildGoulougeanRecordObj goulourecordobj = getgoulourecordObj();
            if (goulourecordbll.saveGoulougeanrecord(goulourecordobj))
            {
                MessageBox.Show("保存成功！");
                this.Close();
                _goulouzhuananpanel.refreshRecordList();
            }
            else
            {
                MessageBox.Show("保存失败！请联系管理员");
            }
        }

        private ChildGoulougeanRecordObj getgoulourecordObj()
        {
            ChildGoulougeanRecordObj goulourecordobj = new ChildGoulougeanRecordObj();
            goulourecordobj.id = _checkobj.goulourecordobj.id;
            goulourecordobj.checkid = _checkobj.Id;
            goulourecordobj.childId = _checkobj.ChildId;
            goulourecordobj.huwai = textBoxX12.Text;
            goulourecordobj.problem = textBoxX1.Text;
            goulourecordobj.vitdname = textBoxX7.Text;
            goulourecordobj.vitdliang = textBoxX11.Text;
            goulourecordobj.zhidao = textBoxX2.Text;
            return goulourecordobj;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
