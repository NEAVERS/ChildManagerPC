using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using login.UI;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL.ChildBaseInfo;
using System.Collections;
using ChildManager.Model;
using login.UI.printrecord;

namespace ChildManager.UI.jibenluru
{
    public partial class Paneljibingguanli_zhuanan : Form
    {
        public Paneljibingguanli_zhuanan _yingyangzhuananpanel;
        private ChildBaseInfoObj _baseobj = null;
        private ChildYingyanggeanBll yingyanggeanbll = new ChildYingyanggeanBll();
        private TbGaoweiBll tbgaoweibll = new TbGaoweiBll();
        private ChildYingyanggeanRecordBll yingyanggeanrecordbll = new ChildYingyanggeanRecordBll();

        private ChildYingyanggeanObj yingyanggeanobj = null;
        public Paneljibingguanli_zhuanan(ChildBaseInfoObj baseobj)
        {
            InitializeComponent();
            _yingyangzhuananpanel = this;
            _baseobj = baseobj;
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

        private void Paneljibingguanli_zhuanan_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = _baseobj.ChildName;
            textBoxX4.Text = _baseobj.ChildGender;
            textBoxX5.Text = _baseobj.ChildBirthDay;
            textBoxX8.Text = _baseobj.MotherName;
            textBoxX9.Text = _baseobj.Telephone;
            textBoxX10.Text = _baseobj.address;
            textBoxX12.Text = new TbGaoweiBll().getGaoweistr(_baseobj.Id, "", "");

            string yingyanggeansqlstr = "select * from child_jibingguanli where childid=" + _baseobj.Id;
            yingyanggeanobj = yingyanggeanbll.getYingyanggeanObj(yingyanggeansqlstr);
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

                setcheckedValue(panel1, yingyanggeanobj.chushengshi);
                setcheckedValue(panel2, yingyanggeanobj.weiyangshi);
                textBoxX15.Text = yingyanggeanobj.foodchange;
            }
            else
            {
                dateTimeInput2.Value = Convert.ToDateTime(_baseobj.ChildBuildDay);
            }

            refreshRecordList();
        }


        public void refreshRecordList()
        {
            string childchecksqlstr = "select *,b.id recordid from tb_childcheck a left join child_jibingguanli_record b on a.id=b.checkid "
                + "where a.childid=" + _baseobj.Id;//+ " and a.checkDay>=(select max(recordtime)+' 00:00:00' from tb_gaowei where childid=" + _baseobj.Id + ")";
            ArrayList checklist = yingyanggeanrecordbll.getChildCheckAndYingyangList(childchecksqlstr);

            if (checklist != null && checklist.Count > 0)
            {
                dataGridView1.Rows.Clear();
                try
                {
                    int i = 1;
                    foreach (ChildCheckObj obj in checklist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, Convert.ToDateTime(obj.CheckDay).ToString("yyyy-MM-dd"), obj.CheckAge, obj.CheckHeight, obj.CheckWeight,
                            obj.yingyangrecordobj.pinggu, obj.yingyangrecordobj.problem, obj.yingyangrecordobj.zhidao, obj.DoctorName);
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

        public void saveyingyangzhuanan()
        {

            yingyanggeanobj = getYingyangGeanObj();
            if (yingyanggeanbll.savejibingguanliObj(yingyanggeanobj))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }

        }

        private ChildYingyanggeanObj getYingyangGeanObj()
        {
            ChildYingyanggeanObj yingyanggeanobj = new ChildYingyanggeanObj();
            yingyanggeanobj.childId = _baseobj.Id;
            yingyanggeanobj.jiwangshi = textBoxX3.Text.Trim();
            yingyanggeanobj.managetime = dateTimeInput2.Value.ToString("yyyy-MM-dd");
            if (!String.IsNullOrEmpty(dateTimeInput3.Value.ToString()))
            {
                yingyanggeanobj.endtime = dateTimeInput3.Value.ToString("yyyy-MM-dd");
            }
            yingyanggeanobj.zhuangui = getcheckedValue(pnl_zhuangui);
            yingyanggeanobj.chushengshi = getcheckedValue(panel1);
            yingyanggeanobj.weiyangshi = getcheckedValue(panel2);
            yingyanggeanobj.foodchange = textBoxX15.Text;
            return yingyanggeanobj;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                ChildCheckObj ckobj = dataGridView1.CurrentRow.Tag as ChildCheckObj;
                Paneljibingguanli_zhuanan_add add = new Paneljibingguanli_zhuanan_add(_yingyangzhuananpanel, ckobj);
                add.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选择要修改的行信息！");
                return;
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            saveyingyangzhuanan();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            btnprint(true);
        }

        public void btnprint(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string projectName = "";
                string formName = "疾病专案管理记录";
                PanelYingyangzhuananPrinter dgvPrinter;
                if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
                {
                    dgvPrinter = new PanelYingyangzhuananPrinter(dataGridView1, _baseobj, yingyanggeanobj);
                    dgvPrinter.Margin = new System.Drawing.Printing.Margins(50, 30, 30, 30);
                    dgvPrinter.PageHeaders.Add(new PanelYingyangzhuananPrinter.Header(projectName, new Font("Arial Unicode MS", 16f), 45));
                    dgvPrinter.PageHeaders.Add(new PanelYingyangzhuananPrinter.Header(formName, new Font("Arial Unicode MS", 19f, FontStyle.Bold), 45));
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            btnprint(false);
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
