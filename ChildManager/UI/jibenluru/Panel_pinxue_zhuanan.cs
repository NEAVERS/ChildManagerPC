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
using ChildManager.BLL;
using System.Collections;
using login.UI.printrecord;

namespace ChildManager.UI
{
    public partial class Panel_pinxue_zhuanan : UserControl
    {
        public Panel_pinxue_zhuanan _pinxuezhuananpanel;
        private ChildBaseInfoObj _baseobj = null;
        private ChildYingyanggeanBll yingyanggeanbll = new ChildYingyanggeanBll();
        private TbGaoweiBll tbgaoweibll = new TbGaoweiBll();
        private ChildPinxuegeanRecordBll pinxuegeanrecordbll = new ChildPinxuegeanRecordBll();
        private ChildYingyanggeanObj yingyanggeanobj = null;
        public Panel_pinxue_zhuanan(ChildBaseInfoObj baseobj)
        {
            InitializeComponent();

            _pinxuezhuananpanel = this;
            _baseobj = baseobj;
        }


        private void Paneltsb_pinxue_zhuanan_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = _baseobj.childName;
            textBoxX4.Text = _baseobj.childGender;
            textBoxX5.Text = _baseobj.childBirthDay;
            textBoxX8.Text = _baseobj.MotherName;
            textBoxX9.Text = _baseobj.Telephone;
            textBoxX10.Text = _baseobj.address;
            gerenshi.Text = _baseobj.gerenshi;
            jiwangshi.Text = _baseobj.jiwangshi;
            jiazushi.Text = _baseobj.jiazushi;
            textBoxX12.Text = new TbGaoweiBll().getGaoweistr(_baseobj.Id, "","");

            string pinxuegeansqlstr = "select * from child_yingyanggean where childid=" + _baseobj.Id;
             yingyanggeanobj = yingyanggeanbll.getYingyanggeanObj(pinxuegeansqlstr);
            if (yingyanggeanobj != null)
            {
                textBoxX3.Text = yingyanggeanobj.jiwangshi;
                if (!String.IsNullOrEmpty(yingyanggeanobj.managetime))
                {
                    dateTimeInput2.Value = Convert.ToDateTime(yingyanggeanobj.managetime);
                }
                if (!String.IsNullOrEmpty(yingyanggeanobj.endtime))
                {
                    dateTimeInput3.Value = Convert.ToDateTime(yingyanggeanobj.endtime);
                }
                setcheckedValue(pnl_zhuangui, yingyanggeanobj.zhuangui);
                textBoxX13.Text = yingyanggeanobj.yunzhou;
                textBoxX2.Text = yingyanggeanobj.yuntian;
                textBoxX14.Text = yingyanggeanobj.hb;
                setcheckedValue(panel1, yingyanggeanobj.tieji);
                textBoxX11.Text = yingyanggeanobj.yaowu;
                textBoxX6.Text = yingyanggeanobj.jiliang;
                textBoxX7.Text = yingyanggeanobj.liaocheng;
                setcheckedValue(panel2, yingyanggeanobj.muru);
                textBoxX15.Text = yingyanggeanobj.foodage;
            }
            else
            {
                dateTimeInput2.Value = Convert.ToDateTime(_baseobj.ChildBuildDay);
            }

            refreshRecordList();

        }

        public void refreshRecordList()
        {
            string childchecksqlstr = "select *,b.id recordid from TB_CHILDCHECK a left join child_pinxuegean_record b on a.id=b.checkid "
                + "where a.childid=" + _baseobj.Id;// +" and a.checkDay>=(select max(recordtime)+' 00:00:00' from tb_gaowei where childid=" + _baseobj.Id + ")";
            ArrayList checklist = pinxuegeanrecordbll.getChildCheckAndPinxueList(childchecksqlstr);

            if (checklist != null && checklist.Count > 0)
            {
                dataGridView1.Rows.Clear();
                try
                {
                    int i = 1;
                    foreach (ChildCheckObj obj in checklist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, Convert.ToDateTime(obj.CheckDay).ToString("yyyy-MM-dd"), obj.CheckAge, obj.pinxuerecordobj.hb,
                            obj.pinxuerecordobj.problem, obj.pinxuerecordobj.yaowu+obj.pinxuerecordobj.jiliang, obj.pinxuerecordobj.zhidao, obj.DoctorName);
                        row.Tag = obj;
                        dataGridView1.Rows.Add(row);
                        i++;
                    }
                }
                finally
                {
                    dataGridView1.ClearSelection();
                    Cursor.Current = Cursors.Default;
                }
            }
        }

        public void savepinxuezhuanan()
        {

            yingyanggeanobj = getChildPinxuegeanObj();

            if (yingyanggeanbll.saveYingyanggeanObj(yingyanggeanobj))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
            
        }

        private ChildYingyanggeanObj getChildPinxuegeanObj()
        {
            ChildYingyanggeanObj pinxuegeanobj = new ChildYingyanggeanObj();
            pinxuegeanobj.childId = _baseobj.Id;
            pinxuegeanobj.jiwangshi = textBoxX3.Text.Trim();
            pinxuegeanobj.managetime = dateTimeInput2.Value.ToString("yyyy-MM-dd");
            if (!String.IsNullOrEmpty(dateTimeInput3.Value.ToString()))
            {
                pinxuegeanobj.endtime = dateTimeInput3.Value.ToString("yyyy-MM-dd");
            }
            pinxuegeanobj.zhuangui = getcheckedValue(pnl_zhuangui);

            pinxuegeanobj.yunzhou = textBoxX13.Text;
            pinxuegeanobj.yuntian = textBoxX2.Text;
            pinxuegeanobj.hb = textBoxX14.Text;
            pinxuegeanobj.tieji = getcheckedValue(panel1);
            pinxuegeanobj.yaowu = textBoxX11.Text;
            pinxuegeanobj.jiliang = textBoxX6.Text;
            pinxuegeanobj.liaocheng = textBoxX7.Text;
            pinxuegeanobj.muru = getcheckedValue(panel2);
            pinxuegeanobj.foodage = textBoxX15.Text;
            return pinxuegeanobj;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                ChildCheckObj checkobj = dataGridView1.CurrentRow.Tag as ChildCheckObj;
                Panel_pinxue_zhuanan_add zhuananadd = new Panel_pinxue_zhuanan_add(_pinxuezhuananpanel, checkobj);
                zhuananadd.ShowDialog();
            }
        }


        //获取复选框中的值及其他备注项目的值
        private string getcheckedValue(Panel panels)
        {
            string returnval = "";
            foreach (Control c in panels.Controls)
            {
                if (c is CheckBox)
                {
                    if ((c as CheckBox).Checked)
                    {
                        returnval += (c.Text + ",");
                    }
                }
                else if (c is TextBox)
                {
                    if (!String.IsNullOrEmpty(c.Text))
                    {
                        returnval += (":" + c.Text + ",");
                    }
                }
                else if (c is ComboBox)
                {
                    if (!String.IsNullOrEmpty(c.Text))
                    {
                        returnval += (":" + c.Text + ",");
                    }
                }
            }
            if (returnval != "")
            {
                returnval = returnval.Substring(0, returnval.Length - 1);
            }
            return returnval;
        }
        //根据读取的值重新为复选框赋值
        private void setcheckedValue(Panel panels, string checkvals)
        {
            //假如读取到的值不为null
            if (!String.IsNullOrEmpty(checkvals))
            {
                //通过分割符拆分字符串为数组
                string[] splitvals = null;
                if (checkvals.Contains(","))
                {
                    splitvals = checkvals.Split(',');
                }
                else
                {
                    splitvals = new string[] { checkvals };
                }
                //循环panel自动为复选框赋值
                foreach (Control c in panels.Controls)
                {
                    if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = false;//默认不选中
                        foreach (string splitval in splitvals)
                        {
                            if (c.Text == splitval)
                            {
                                (c as CheckBox).Checked = true;
                                break;
                            }
                        }
                    }
                    else if (c is TextBox)
                    {
                        c.Text = "";
                        foreach (string splitval in splitvals)
                        {
                            if (splitval.Contains(":"))
                            {
                                c.Text = splitval.Replace(":", "");
                                break;
                            }
                        }
                    }
                    else if (c is ComboBox)
                    {
                        c.Text = "";
                        foreach (string splitval in splitvals)
                        {
                            if (splitval.Contains(":"))
                            {
                                c.Text = splitval.Replace(":", "");
                                break;
                            }
                        }
                    }
                }

            }
            else
            {
                foreach (Control c in panels.Controls)
                {
                    if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = false;
                    }
                    else if (c is TextBox)
                    {
                        c.Text = "";
                    }
                }
            }

        }

        //打印
        public void btnprint(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string projectName = "";
                string formName = "营养性缺铁性贫血儿童专案管理记录";
                PanelPinxuezhuananPrinter dgvPrinter;
                if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
                {
                    dgvPrinter = new PanelPinxuezhuananPrinter(dataGridView1, _baseobj, yingyanggeanobj);
                    dgvPrinter.Margin = new System.Drawing.Printing.Margins(50, 30, 30, 30);
                    dgvPrinter.PageHeaders.Add(new PanelPinxuezhuananPrinter.Header(projectName, new Font("Arial Unicode MS", 16f), 45));
                    dgvPrinter.PageHeaders.Add(new PanelPinxuezhuananPrinter.Header(formName, new Font("Arial Unicode MS", 19f, FontStyle.Bold), 45));
                    dgvPrinter.Print("PDFCreator", ispre);
                }
                else
                {
                    MessageBox.Show("该儿童无相关打印记录", "软件提示");
                }

            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        //设置复选框单选
        private void checkBoxs_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked)
            {
                foreach (Control c in (sender as Control).Parent.Controls)
                {
                    if (c is CheckBox && !c.Equals(sender))
                    {
                        (c as CheckBox).Checked = false;
                    }
                }
            }
        }
    }
}
