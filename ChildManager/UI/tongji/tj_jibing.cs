using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YCF.BLL;
using YCF.Model;

namespace ChildManager.UI.tongji
{
    public partial class tj_jibing : UserControl
    {
        tj_diqubll bll = new tj_diqubll();
        //private ChildMainForm _mianform = null;
        public tj_jibing()
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
            string starttime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string endtime = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string sqls = "select b.nnd couname,a.gaoweiyinsu couname1,count(*) cou from tb_gaowei a ";
            sqls += " left join";
            sqls += " (";
            sqls += " select";
            sqls += " case ";
            sqls += " when datediff(year, childbirthday, getdate()) < 1  then  '<1岁'";
            sqls += " when datediff(year, childbirthday, getdate()) >= 1 and datediff(year, childbirthday, getdate()) < 3 then '1~3岁'";
            sqls += "  when datediff(year, childbirthday, getdate()) >= 3 and datediff(year, childbirthday, getdate()) < 6 then '3~6岁'";
            sqls += "  when datediff(year, childbirthday, getdate()) >= 6 and datediff(year, childbirthday, getdate()) < 10 then '6~10岁'";
            sqls += "  when datediff(year, childbirthday, getdate()) >= 10 then '>10岁'";
            sqls += "  end";
            sqls += "  as nnd, id from tb_childbase";
            sqls += " ) b on a.childid = b.id";
            sqls += " where  recordtime>='" + starttime + "' and recordtime <='" + endtime + "' ";
            sqls += " group by b.nnd,a.gaoweiyinsu order by nnd ";

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
                        string bili = (Convert.ToSingle(obj.cou)*100/ Convert.ToSingle(totalcount)).ToString("0.0")+"%";

                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, i, obj.couname1,obj.couname, obj.cou, bili);
                        row.Tag = obj;
                        dataGridView1.Rows.Add(row);
                        xData.Add(obj.couname1+obj.couname);
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
