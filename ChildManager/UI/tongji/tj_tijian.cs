using System;
using System.Windows.Forms;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.Model;
using System.Collections;
using ChildManager.BLL.tongji;

namespace ChildManager.UI.tongji
{
    public partial class tj_tijian : UserControl
    {
        private ChildBaseInfoBll childBaseInfobll = new ChildBaseInfoBll();//儿童建档基本信息业务处理类
        tj_tijianBll bll = new tj_tijianBll();
        //private ChildMainForm _mianform = null;
        public tj_tijian()
        {
            InitializeComponent();
            //_mianform = mianform;
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string starttime = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            string endtime = dateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            string sqls = "select * from "
            //+" ("
            //+"select childid,(select gaoweiyinsu+',' from tb_gaowei where childid=b.childid for xml path('')) as gaoweistr from tb_gaowei b where b.type='高危' group by childid"
            //+") c "
            + " tb_childcheck a "
            + " left join tb_childBase d on a.childid=d.id "
            + "where  checkDay>='" + starttime + "' and checkDay <='" + endtime + "'  ";

            string doctorname = cbx_doc.Text.Trim();

            if (!String.IsNullOrEmpty(doctorname))
            {
                sqls += " and DoctorName = '" + doctorname + "'";
            }

            sqls += " order by checkDay desc";

            ArrayList list = bll.getTj_ChildCheckList(sqls);
            dataGridView1.Rows.Clear();
            if (list != null && list.Count > 0)
            {
                
                try
                {
                    int i = 1;
                    foreach (ChildCheckObj obj in list)
                    {
                        string birthstr = "";
                        string buidstr = "";
                        DateTime outtime = new DateTime();
                        if (DateTime.TryParse(obj.baseinfoobj.ChildBirthDay, out outtime))
                        {
                            birthstr = outtime.ToString("yyyy-MM-dd");
                        }
                        if (DateTime.TryParse(obj.baseinfoobj.ChildBuildDay, out outtime))
                        {
                            buidstr = outtime.ToString("yyyy-MM-dd");
                        }

                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, i, obj.baseinfoobj.HealthCardNo, obj.baseinfoobj.ChildName, obj.baseinfoobj.ChildGender,
                            birthstr, obj.baseinfoobj.MotherName, obj.baseinfoobj.Telephone, obj.baseinfoobj.gaowei, obj.CheckDay,obj.DoctorName);
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
