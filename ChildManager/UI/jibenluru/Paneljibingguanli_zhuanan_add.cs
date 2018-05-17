using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.Model;
using ChildManager.Model.ChildBaseInfo;

namespace ChildManager.UI.jibenluru
{
    public partial class Paneljibingguanli_zhuanan_add : Form
    {
        private Paneljibingguanli_zhuanan _yingyangzhuananpanel;
        private ChildYingyanggeanRecordBll yingyangrecordbll = new ChildYingyanggeanRecordBll();
        private ChildCheckObj _checkobj;

        public Paneljibingguanli_zhuanan_add(Paneljibingguanli_zhuanan yingyangzhuananpanel, ChildCheckObj checkobj)
        {
            InitializeComponent();
            _yingyangzhuananpanel = yingyangzhuananpanel;
            _checkobj = checkobj;
        }

        private void Paneljibingguanli_zhuanan_add_Load(object sender, EventArgs e)
        {
            textBoxX4.Text = _checkobj.CheckDay;
            textBoxX5.Text = _checkobj.CheckAge;
            textBoxX8.Text = _checkobj.DoctorName;
            textBoxX1.Text = _checkobj.CheckHeight;
            textBoxX3.Text = _checkobj.CheckWeight;

            textBoxX12.Text = _checkobj.yingyangrecordobj.pinggu;
            textBoxX6.Text = _checkobj.yingyangrecordobj.problem;
            textBoxX2.Text = _checkobj.yingyangrecordobj.zhidao;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            ChildYingyanggeanRecordObj yingyangrecordobj = getyingyangrecordObj();
            if (yingyangrecordbll.savejibingguanlirecord(yingyangrecordobj))
            {
                MessageBox.Show("保存成功！");
                this.Close();
                _yingyangzhuananpanel.refreshRecordList();
            }
            else
            {
                MessageBox.Show("保存失败！请联系管理员");
            }
        }
        private ChildYingyanggeanRecordObj getyingyangrecordObj()
        {
            ChildYingyanggeanRecordObj yingyangrecordobj = new ChildYingyanggeanRecordObj();
            yingyangrecordobj.id = _checkobj.yingyangrecordobj.id;
            yingyangrecordobj.checkid = _checkobj.Id;
            yingyangrecordobj.childId = _checkobj.ChildId;

            yingyangrecordobj.pinggu = textBoxX12.Text;
            yingyangrecordobj.problem = textBoxX6.Text;
            yingyangrecordobj.zhidao = textBoxX2.Text;
            return yingyangrecordobj;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
