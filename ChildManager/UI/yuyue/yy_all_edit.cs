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
    public partial class yy_all_edit : Form
    {
        yy_asd_tabbll bll = new yy_asd_tabbll();
        yy_pz_tabbll pzbll = new yy_pz_tabbll();
        yy_pz_details_tabbll detailsbll = new yy_pz_details_tabbll();
        yy_asd_tab _obj = null;
        string _pz_lx = "";
        private IList<yy_pz_tab> _xmList;
        private IList<yy_pz_details_tab> _detailList;
        private tb_childbase _jibenobj;
        private yy_asd_tabCount _countobj;

        public bool IsEdit
        {
            get
            {
                return _obj != null;
            }
        }

        public yy_all_edit(int? id, yy_asd_tabCount countobj, string pz_lx, string startTime)
        {
            InitializeComponent();
            CommonHelper.SetAllControls(panel1);
            //dateTimePicker2.Value = DateTime.Now.AddDays(7);
            _pz_lx = pz_lx;

            _xmList = pzbll.GetList(pz_lx);

            yy_xm.DataSource = _xmList;
            yy_xm.DisplayMember = "pz_xm";
            yy_xm.ValueMember = "id";
            yy_xm.SelectedIndexChanged += Yy_xm_SelectedIndexChanged;

            if (yy_xm.Items.Count > 0)
            {
                yy_xm.SelectedIndex = 0;
                Yy_xm_SelectedIndexChanged(yy_xm, new EventArgs());
            }
    

            _countobj = countobj;

            if (id != null)
            {
                _obj = bll.Get(id.Value);
            }

        }

        private void Yy_xm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedValue != null)
            {
                var id = (int)(sender as ComboBox).SelectedValue;
                _detailList = detailsbll.Get(id);

                yy_sjd.DataSource = _detailList;
                yy_sjd.DisplayMember = "time";
                yy_sjd.ValueMember = "id";
            }
        }


        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (!IsEdit)
            {
                if (_jibenobj == null)
                {
                    MessageBox.Show("请选择儿童！");

                    return;
                }
                if (bll.Exists(_jibenobj.id, yy_rq.Text))
                {
                    MessageBox.Show("该儿童当天已建档！");

                    return;
                }

                if (bll.IsFull(yy_xm.Text, yy_rq.Text, yy_sjd.Text))
                {
                    MessageBox.Show("该时间预约已满！");

                    return;
                }
            }
            else
            {
                if ((_obj.yy_xm != yy_xm.Text || _obj.yy_rq != yy_rq.Text || _obj.yy_sjd != yy_sjd.Text)
                    && bll.IsFull(yy_xm.Text, yy_rq.Text, yy_sjd.Text))
                {
                    MessageBox.Show("该时间预约已满！");

                    return;
                }
            }




            var obj = GetObj();

            if (bll.SaveOrUpdate(obj))
            {
                MessageBox.Show("保存成功！", "软件提示");
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("保存失败！", "软件提示");
            }
        }

        private yy_asd_tab GetObj()
        {
            var obj = CommonHelper.GetObjMenzhen<yy_asd_tab>(panel2.Controls, IsEdit ? _obj.child_id : _jibenobj.id);
            obj.id = _obj?.id ?? 0;

            return obj;
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            RefreshRecord();
        }

        private void RefreshRecord()
        {
            if (_obj != null)
            {
                CommonHelper.setForm(_obj, panel2.Controls);
            }
            else if (_countobj != null)
            {
                yy_rq.Text = _countobj.yy_rq;
                yy_xm.Text = _countobj.pz_xm;
                yy_sjd.Text = _countobj.time;
            }
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            using (Paneltsb_searchInfo frmsearcher = new Paneltsb_searchInfo())
            {
                frmsearcher.ShowDialog();
                if (frmsearcher.DialogResult == DialogResult.OK)
                {
                    _jibenobj = frmsearcher.returnval;
                }
            }
        }
    }
}
