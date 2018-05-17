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
            IList<XL_YY_TAB> yylist = xlbll.GetList(_xlwomeninfo.cd_id);
            if (yylist != null)
            {
                int rowindex = -1;
                dataGridView2.Rows.Clear();
                for (int i=0;i<yylist.Count;i++ )
                {
                    XL_YY_TAB obj = yylist[i];
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView2, obj.YY_RQ, obj.yy_sjd, obj.YY_XM);
                    row.Tag = obj;
                    dataGridView2.Rows.Add(row);
                    if (obj.YY_RQ.Equals(yy_rq.Text.Trim()))
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
            XL_YY_TAB _xlobj = CommonHelper.GetObjMenzhen<XL_YY_TAB>(groupBox1.Controls, _xlwomeninfo.cd_id);
            if (dataGridView2.SelectedRows.Count>0)
            {
                XL_YY_TAB xlobj = dataGridView2.SelectedRows[0].Tag as XL_YY_TAB;
                _xlobj.ID = xlobj.ID;
                if (xlobj.YY_SJD == _xlobj.YY_SJD&&xlobj.YY_RQ==_xlobj.YY_RQ)
                    sjdsfbd = 1;
                if (xlobj.YY_XM == _xlobj.YY_XM && xlobj.YY_RQ == _xlobj.YY_RQ)
                    xmsfbd = 1;
            }
            int sjdsum = xlbll.GetSjdCou(_xlobj.yy_sjd,_xlobj.YY_RQ)-sjdsfbd;//如果时间段没变，减去自己这条
            int xmsum = xlbll.GetXmCou(_xlobj.YY_XM,_xlobj.YY_RQ)-xmsfbd;//如果项目没变，减去自己这条
            IList<XL_YY_COU> listcou = couxlbll.GetList();
            int sjdcou = 0;
            int xmcou = 0;
            foreach(XL_YY_COU obj in listcou)
            {
                if (obj.XMMC == _xlobj.YY_XM)
                    xmcou = (int)obj.XMSL;
                else if (obj.XMMC == _xlobj.yy_sjd)
                    sjdcou = (int)obj.XMSL;
            }
            if(sjdsum>=sjdcou)
            {
                MessageBox.Show(_xlobj.YY_RQ+" "+_xlobj.yy_sjd+"已约满"+ sjdsum + "人");
                return;
            }
            if (xmsum >= xmcou)
            {
                MessageBox.Show(_xlobj.YY_RQ + " " + _xlobj.YY_XM + "已约满" + xmsum + "人");
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
                XL_YY_TAB xlobj = dataGridView2.SelectedRows[0].Tag as XL_YY_TAB;
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
