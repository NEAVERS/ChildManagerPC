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
using System.Collections;
using ChildManager.BLL;

namespace ChildManager.UI.childSetInfo
{
    public partial class FrmCheck : Form
    {
        private bool _modfiles = false;
        //private ChildBaseInfoBll bll = new ChildBaseInfoBll();
        private ChildCheckBll bll = new ChildCheckBll();
        private ChildCheckObj _obj = new ChildCheckObj();
        int _id;
        public FrmCheck(ChildCheckObj obj)
        {
            InitializeComponent();
            _obj = obj;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string updatesql = this.getupdate();
            if (bll.updaterecord(updatesql)  > 0)
            {
                MessageBox.Show("保存成功！","软件提示");
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("保存失败！","软件提示");
            }
        }

        private string getupdate()
        {
            StringBuilder builder = new StringBuilder();
            ChildCheckObj obj = new ChildCheckObj();
            obj.nuerzhidao = this.txt_nuruzhidao.Text.Trim();
            obj.checkdiagnose = this.txt_diagnose.Text.Trim();
            builder.Append("update tb_childcheck set ");
            builder.Append("nuerzhidao = '" + obj.nuerzhidao + "',");
            builder.Append("checkdiagnose = '" + obj.checkdiagnose + "'");
            builder.Append(" where id ="+_obj.Id+"");
            return builder.ToString();
        }

        private void refreshCode()
        {

            txt_nuruzhidao.Text = _obj.nuerzhidao;
            txt_diagnose.Text = _obj.checkdiagnose;
            
        }


        private void FrmCheck_Load(object sender, EventArgs e)
        {
            refreshCode();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (_modfiles)
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
    }
}
