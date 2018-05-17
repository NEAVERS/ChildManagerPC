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
    public partial class Panel_pinxue_zhuanan_add : Form
    {
        private Panel_pinxue_zhuanan _pinxuezhuananpanel;
        private ChildPinxuegeanRecordBll pinxuerecordbll = new ChildPinxuegeanRecordBll();
        private ChildCheckObj _checkobj;
        public Panel_pinxue_zhuanan_add(Panel_pinxue_zhuanan pinxuezhuananpanel, ChildCheckObj checkobj)
        {
            InitializeComponent();

            _pinxuezhuananpanel = pinxuezhuananpanel;
            _checkobj = checkobj;
        }


        private void Paneltsb_pinxue_zhuanan_add_Load(object sender, EventArgs e)
        {
            textBoxX4.Text = _checkobj.CheckDay;
            textBoxX5.Text = _checkobj.CheckAge;
            textBoxX8.Text = _checkobj.DoctorName;

            textBoxX12.Text = _checkobj.pinxuerecordobj.hb;
            textBoxX1.Text = _checkobj.pinxuerecordobj.problem;
            textBoxX7.Text = _checkobj.pinxuerecordobj.yaowu;
            textBoxX11.Text = _checkobj.pinxuerecordobj.jiliang;
            textBoxX2.Text = _checkobj.pinxuerecordobj.zhidao;
            
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            ChildPinxuegeanRecordObj pinxuerecordobj = getpinxuerecordObj();
            if (pinxuerecordbll.savePinxuegeanrecord(pinxuerecordobj))
            {
                MessageBox.Show("保存成功！");
                this.Close();
                _pinxuezhuananpanel.refreshRecordList();
            }
            else
            {
                MessageBox.Show("保存失败！请联系管理员");
            }
        }

        private ChildPinxuegeanRecordObj getpinxuerecordObj()
        {
            ChildPinxuegeanRecordObj pinxuerecordobj = new ChildPinxuegeanRecordObj();
            pinxuerecordobj.id = _checkobj.pinxuerecordobj.id;
            pinxuerecordobj.checkid = _checkobj.Id;
            pinxuerecordobj.childId = _checkobj.ChildId;

            pinxuerecordobj.hb = textBoxX12.Text;
            pinxuerecordobj.problem = textBoxX1.Text;
            pinxuerecordobj.yaowu = textBoxX7.Text;
            pinxuerecordobj.jiliang = textBoxX11.Text;
            pinxuerecordobj.zhidao = textBoxX2.Text;

            pinxuerecordobj.zhidao = textBoxX2.Text;
            return pinxuerecordobj;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
