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
using ChildManager.BLL;
using System.Collections;
using ChildManager.Model;
using login.UI.printrecord;

namespace ChildManager.UI.jibenluru
{
    public partial class Panelxinliyichang_zhuanan : Form
    {
        public Panelxinliyichang_zhuanan _gaoweizhuananpanel;
        private ChildBaseInfoObj _baseobj = null;
        private ChildGaoweigeanBll gaoweigeanbll = new ChildGaoweigeanBll();
        private TbGaoweiBll tbgaoweibll = new TbGaoweiBll();
        private ChildCheckBll checkbll = new ChildCheckBll();
        private ChildGaoweigeanObj gaoweigeanobj = null;

        public Panelxinliyichang_zhuanan(ChildBaseInfoObj baseobj)
        {
            InitializeComponent();
            _baseobj = baseobj;
            _gaoweizhuananpanel = this;
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

        public void refreshRecordList()
        {
            string childchecksqlstr = "select *,b.id recordid from TB_CHILDCHECK a left join child_xinlixingwei_record b on a.id=b.checkid "
                + "where a.childid=" + _baseobj.Id;// +" and a.checkDay>=(select max(recordtime)+' 00:00:00' from tb_gaowei where childid=" + _baseobj.Id + ")";
            ArrayList checklist = checkbll.getChildCheckAndGaoweiList(childchecksqlstr);

            if (checklist != null && checklist.Count > 0)
            {
                dataGridView1.Rows.Clear();
                try
                {
                    int i = 1;
                    foreach (ChildCheckObj obj in checklist)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, obj.CheckDay, obj.CheckAge, obj.gaoweirecordobj.pingufangfa,
                            obj.gaoweirecordobj.pingujieguo, obj.gaoweirecordobj.zhidao, obj.gaoweirecordobj.chuli, obj.DoctorName);
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

        public void savegaoweizhuanan()
        {
            gaoweigeanobj = getGaoweiGeanObj();

            if (gaoweigeanbll.savexinlixingweigeanObj(gaoweigeanobj))
            {
                MessageBox.Show("保存成功！");
            }
            else
            {
                MessageBox.Show("保存失败！");
            }

        }


        private ChildGaoweigeanObj getGaoweiGeanObj()
        {
            ChildGaoweigeanObj gaoweigeanobj = new ChildGaoweigeanObj();
            gaoweigeanobj.childId = _baseobj.Id;
            gaoweigeanobj.jiwangshi = textBoxX3.Text.Trim();
            gaoweigeanobj.managetime = dateTimeInput2.Value.ToString("yyyy-MM-dd");
            if (!String.IsNullOrEmpty(dateTimeInput3.Value.ToString()))
            {
                gaoweigeanobj.endtime = dateTimeInput3.Value.ToString("yyyy-MM-dd");
            }
            gaoweigeanobj.zhuangui = getcheckedValue(pnl_zhuangui);
            gaoweigeanobj.zhuanzhendanwei = textBoxX2.Text.Trim();
            return gaoweigeanobj;
        }

        private void Panelxinliyichang_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = _baseobj.childName;
            textBoxX4.Text = _baseobj.childGender;
            textBoxX5.Text = _baseobj.childBirthDay;
            textBoxX8.Text = _baseobj.MotherName;
            textBoxX9.Text = _baseobj.Telephone;
            textBoxX10.Text = _baseobj.address;
            textBoxX12.Text = new TbGaoweiBll().getGaoweistr(_baseobj.Id, "", "");

            string gaoweigeansqlstr = "select * from child_xinlixingwei where childid=" + _baseobj.Id;
            gaoweigeanobj = gaoweigeanbll.getGaoweigeanObj(gaoweigeansqlstr);
            if (gaoweigeanobj != null)
            {
                textBoxX3.Text = gaoweigeanobj.jiwangshi;
                if (!String.IsNullOrEmpty(gaoweigeanobj.managetime))
                {
                    dateTimeInput2.Value = Convert.ToDateTime(gaoweigeanobj.managetime);
                }
                if (!String.IsNullOrEmpty(gaoweigeanobj.endtime))
                {
                    dateTimeInput3.Value = Convert.ToDateTime(gaoweigeanobj.endtime);
                }
                setcheckedValue(pnl_zhuangui, gaoweigeanobj.zhuangui);
                textBoxX2.Text = gaoweigeanobj.zhuanzhendanwei;
            }
            else
            {
                dateTimeInput2.Value = Convert.ToDateTime(_baseobj.ChildBuildDay);
            }

            refreshRecordList();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            savegaoweizhuanan();
        }


        //打印
        public void btnprint(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string projectName = "";
                string formName = "心里行为发育异常专案管理记录";
                PanelGaoweizhuananPrinter dgvPrinter;
                if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
                {
                    dgvPrinter = new PanelGaoweizhuananPrinter(dataGridView1, _baseobj, gaoweigeanobj);
                    dgvPrinter.Margin = new System.Drawing.Printing.Margins(50, 30, 30, 30);
                    dgvPrinter.PageHeaders.Add(new PanelGaoweizhuananPrinter.Header(projectName, new Font("Arial Unicode MS", 16f), 45));
                    dgvPrinter.PageHeaders.Add(new PanelGaoweizhuananPrinter.Header(formName, new Font("Arial Unicode MS", 19f, FontStyle.Bold), 45));
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

        private void buttonX4_Click(object sender, EventArgs e)
        {
            btnprint(true);
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            btnprint(false);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                ChildCheckObj ckobj = dataGridView1.CurrentRow.Tag as ChildCheckObj;
                Panelxinliyichang_zhuanan_add add = new Panelxinliyichang_zhuanan_add(_gaoweizhuananpanel, ckobj);
                add.ShowDialog();
            }
            else
            {
                MessageBox.Show("请选中要修改的行信息");
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
