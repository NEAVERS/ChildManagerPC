using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YCF.Common;
using ChildManager.BLL.ChildBaseInfo;
using System.Collections;
using ChildManager.Model.ChildBaseInfo;

namespace ChildManager.UI
{
    public partial class Paneltsb_jiben_select_gaowei : Form
    {
        private TbGaoweiBll tbgaoweibll = new TbGaoweiBll();
        //设置返回值
        private string _returnval;
        public string returnval
        {
            get { return _returnval; }
        }
        public Paneltsb_jiben_select_gaowei()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectval = "";
            foreach (Control c in panel1.Controls)
            {
                if (c != null && (c is CheckBox))
                {
                    if ((c as CheckBox).Checked)
                    {
                        if (selectval == "")
                        {
                            selectval = selectval + c.Text;
                        }
                        else
                        {
                            selectval = selectval + "," + c.Text;
                        }
                    }
                }
            }


            _returnval = selectval;

            this.DialogResult = DialogResult.OK;//关闭窗体，导入值
        }

        private void Paneltsb_jiben_select_gaowei_Load(object sender, EventArgs e)
        {
            if (globalInfoClass.Wm_Index != -1)
            {
                string sqls = "select * from tb_gaowei where childId=" + globalInfoClass.Wm_Index;
                ArrayList gaoweilist = tbgaoweibll.getGaoweilist(sqls);
                if (gaoweilist != null && gaoweilist.Count > 0)
                {
                    foreach (TbGaoweiObj gaoweiobj in gaoweilist)
                    {
                        foreach (Control c in this.panel1.Controls)
                        {
                            if (c is CheckBox && c.Text==gaoweiobj.gaoweiyinsu)
                            {
                                (c as CheckBox).Checked = true;
                            }
                        }
                    }
                }
            }
        }

    }
}
