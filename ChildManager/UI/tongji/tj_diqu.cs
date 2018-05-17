using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YCF.BLL;
using YCF.Model;

namespace ChildManager.UI.tongji
{
    public partial class tj_diqu : UserControl
    {
        tj_diqubll bll = new tj_diqubll();
        //private ChildMainForm _mianform = null;
        public tj_diqu()
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
            string sqls = "select d.province couname,count(*) as cou "
            + " from TB_CHILDCHECK a "
            + " left join TB_CHILDBASE d on a.childid=d.id "
            + "where  checkDay>='" + starttime + "' and checkDay <='" + endtime + "' group by d.province ";

            IList<countObj> list = bll.GetList(sqls);
            dataGridView1.Rows.Clear();

            List<string> xData = new List<string>();
            List<int> yData = new List<int>();

            if (list != null && list.Count > 0)
            {
                try
                {
                    int totalcount = 0;
                    foreach (countObj obj in list)
                    {
                        totalcount += obj.cou;
                    }
                    
                    int i = 1;
                    foreach (countObj obj in list)
                    {
                        string bili = (Convert.ToDouble(obj.cou)*100/ Convert.ToDouble(totalcount)).ToString("0.00")+"%";

                        if (String.IsNullOrEmpty(obj.couname))
                        {
                            obj.couname = "其他";
                        }

                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, i, obj.couname, obj.cou, bili);
                        row.Tag = obj;
                        dataGridView1.Rows.Add(row);

                        xData.Add(obj.couname);
                        yData.Add(obj.cou);
                        i++;
                    }
                }
                finally
                {
                    dataGridView1.ClearSelection();
                    Cursor.Current = Cursors.Default;
                }
            }

            chart1.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
            chart1.Series[0]["PieLabelStyle"] = "Outside";//将文字移到外侧
            chart1.Series[0]["PieLineColor"] = "Black";//绘制黑色的连线。
            chart1.Series[0].Points.DataBindXY(xData, yData);
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
