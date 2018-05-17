using System;
using System.Windows.Forms;
using ChildManager.UI.jibenluru;
using YCF.BLL.cepingshi;
using System.Collections.Generic;
using YCF.Model;
using YCF.Common;
using YCF.BLL.xunlianshi;

namespace ChildManager.UI.xunlianshi
{
    public partial class xl_jibenxinxi_panel : UserControl
    {
        xl_WomenInfo _xlwomeninfo = null;
        xl_yy_tabbll xlbll = new xl_yy_tabbll();
        xl_yy_coubll couxlbll = new xl_yy_coubll();

        public xl_jibenxinxi_panel(xl_WomenInfo xlwomeninfo)
        {
            InitializeComponent();
            _xlwomeninfo = xlwomeninfo;
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            WomenInfo womeninfo = new WomenInfo();
            womeninfo.cd_id = _xlwomeninfo.cd_id;
            jibenxinxi_panel jibenpanel = new jibenxinxi_panel(womeninfo);
            jibenpanel.buttonX1.Visible = false;
            jibenpanel.buttonX3.Visible = false;
            jibenpanel.buttonX6.Visible = false;//隐藏功能按钮
            //jibenpanel.groupPanel4.Visible = false;
            //jibenpanel.groupPanel6.Visible = false;//隐藏不需要显示的项目
            jibenpanel.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(jibenpanel);

            loadyyxinxi();
        }

        private void loadyyxinxi()
        {
            IList<xl_yy_tab> yylist = xlbll.GetList(_xlwomeninfo.cd_id);
            if (yylist != null)
            {
                int rowindex = -1;
                dataGridView2.Rows.Clear();
                for (int i=0;i<yylist.Count;i++ )
                {
                    xl_yy_tab obj = yylist[i];
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView2, obj.yy_rq, obj.yy_sjd, obj.yy_xm);
                    row.Tag = obj;
                    dataGridView2.Rows.Add(row);
                    if (obj.yy_rq.Equals(yy_rq.Text.Trim()))
                    {
                        rowindex = i;
                    }
                }
                if(rowindex==-1)
                {
                    dataGridView2.ClearSelection();
                }
                else
                {
                    dataGridView2.Rows[rowindex].Selected = true;
                    dataGridView2_CellClick(null, new DataGridViewCellEventArgs(1, rowindex));
                }
            }
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            if(_xlwomeninfo.cd_id==-1)
            {
                MessageBox.Show("请选择儿童后再保存！");
                return;
            }
            int sjdsfbd = 0;
            int xmsfbd = 0;
            xl_yy_tab _xlobj = CommonHelper.GetObjMenzhen<xl_yy_tab>(groupBox1.Controls, _xlwomeninfo.cd_id);
            if (dataGridView2.SelectedRows.Count>0)
            {
                xl_yy_tab xlobj = dataGridView2.SelectedRows[0].Tag as xl_yy_tab;
                _xlobj.id = xlobj.id;
                if (xlobj.yy_sjd == _xlobj.yy_sjd&&xlobj.yy_rq==_xlobj.yy_rq)
                    sjdsfbd = 1;
                if (xlobj.yy_xm == _xlobj.yy_xm && xlobj.yy_rq == _xlobj.yy_rq)
                    xmsfbd = 1;
            }
            int sjdsum = xlbll.GetSjdCou(_xlobj.yy_sjd,_xlobj.yy_rq)-sjdsfbd;//如果时间段没变，减去自己这条
            int xmsum = xlbll.GetXmCou(_xlobj.yy_xm,_xlobj.yy_rq)-xmsfbd;//如果项目没变，减去自己这条
            IList<xl_yy_cou> listcou = couxlbll.GetList();
            int sjdcou = 0;
            int xmcou = 0;
            foreach(xl_yy_cou obj in listcou)
            {
                if (obj.xmmc == _xlobj.yy_xm)
                    xmcou = obj.xmsl;
                else if (obj.xmmc == _xlobj.yy_sjd)
                    sjdcou = obj.xmsl;
            }
            if(sjdsum>=sjdcou)
            {
                MessageBox.Show(_xlobj.yy_rq+" "+_xlobj.yy_sjd+"已约满"+ sjdsum + "人");
                return;
            }
            if (xmsum >= xmcou)
            {
                MessageBox.Show(_xlobj.yy_rq + " " + _xlobj.yy_xm + "已约满" + xmsum + "人");
                return;
            }

            if (xlbll.SaveOrUpdate(_xlobj))
            {
                MessageBox.Show("保存成功！");
                loadyyxinxi();
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1)
            {
                xl_yy_tab xlobj = dataGridView2.SelectedRows[0].Tag as xl_yy_tab;
                CommonHelper.setForm(xlobj, groupBox1.Controls);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            dataGridView2.ClearSelection();
            yy_rq.Value = DateTime.Now;
            yy_sjd.Text = "";
            yy_xm.Text = "";
        }
    }
}
