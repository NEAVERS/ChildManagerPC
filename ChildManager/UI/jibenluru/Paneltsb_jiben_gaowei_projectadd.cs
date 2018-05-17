using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.BLL;
using ChildManager.Model;

namespace ChildManager.UI.jibenluru
{
    public partial class Paneltsb_jiben_gaowei_projectadd : Form
    {
        private Paneltsb_jiben_gaowei _gaowei = null;
        private ProjectGaoweibll bll = new ProjectGaoweibll();
        private ProjectGaoweiobj _obj = null;
        private int _id;
        private bool _validate;
        private string _content;
        private int _p_id;
        private int _cid;
        public Paneltsb_jiben_gaowei_projectadd(bool validate, int id,int p_id, Paneltsb_jiben_gaowei gaowei,string content)
        {
            InitializeComponent();
            _id = id;
            _validate = validate;
            _gaowei = gaowei;
            _content = content;
            _p_id = p_id;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_validate)
            {
                if (bll.saverecord(Add()))
                {
                    MessageBox.Show("保存成功！");
                    this.Close();
                    _gaowei.RefreshCode();
                }
                else
                {
                    MessageBox.Show("保存失败！");
                    return;
                }
            }
            else
            {
                if (bll.updaterecord(Update()) > 0)
                {
                    MessageBox.Show("保存成功！");
                    this.Close();
                    _gaowei.RefreshCode();
                }
                else
                {
                    MessageBox.Show("保存失败！");
                    return;
                }
            }
        }


        private string Add()
        {
            StringBuilder buid = new StringBuilder();
            ProjectGaoweiobj obj1 = new ProjectGaoweiobj();
            obj1.p_id = _p_id;
            obj1.cid = _cid;
            obj1.content = this.textBoxX1.Text.Trim();
            buid.Append("insert into tb_gaoweicontent (");
            buid.Append("p_id");
            //buid.Append(",cid");
            buid.Append(",content");
            buid.Append(") values (");
            buid.Append("" + obj1.p_id + "");
            //buid.Append("" + obj1.cid + "");
            buid.Append(",'" + obj1.content + "')");
            return buid.ToString();
        }

        private string Update()
        {
            StringBuilder buid = new StringBuilder();
            ProjectGaoweiobj obj1 = new ProjectGaoweiobj();
            obj1.content = this.textBoxX1.Text.Trim();
            buid.Append("update tb_gaoweicontent set ");
            buid.Append("content='"+obj1.content+"'");
            buid.Append(" where id=" + _id);
            return buid.ToString();
        }

        private void Paneltsb_jiben_gaowei_projectadd_Load(object sender, EventArgs e)
        {
            if (!_validate)
            {
                this.textBoxX1.Text = _content;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
