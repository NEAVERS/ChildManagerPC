using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YCF.BLL;
using YCF.Model;
using YCF.Common;
using System.Drawing;

namespace ChildManager.UI.tongji
{
    public partial class tj_yingtijian : UserControl
    {
        tj_yingtijianBll bll = new tj_yingtijianBll();
        //private ChildMainForm _mianform = null;
        public tj_yingtijian()
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
            string sqls = "select healthCardNo,childName,childGender,childBirthDay,telephone,a.fuzenday,b.checkday"
            + " from tb_childcheck a "
            + " left join tb_childBase d on a.childid=d.id "
            + " left join tb_childcheck b on a.childid=b.childid and DATEDIFF ( day, b.checkday, a.fuzenday )>=-7 and DATEDIFF ( day, b.checkday, a.fuzenday )<=7 "
            + "where  a.fuzenDay>='" + starttime + "' and a.fuzenDay <='" + endtime + "' and d.id is not null and a.user_code='"+globalInfoClass.UserCode+"'";

            IList<yingtijianObj> list = bll.GetList(sqls);
            dataGridView1.Rows.Clear();
            
            if (list != null && list.Count > 0)
            {
                try
                {
                    int i = 1;
                    foreach (yingtijianObj obj in list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        if (String.IsNullOrEmpty(obj.checkday))
                        {
                            row.DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            row.DefaultCellStyle.ForeColor = Color.Green;
                        }
                        int[] age = CommonHelper.getAgeBytime(obj.childBirthDay, DateTime.Now.ToString());
                        string agestr = (age[0] > 0 ? age[0].ToString() + "岁" : "") + (age[1] > 0 ? age[1].ToString() + "月" : "");// +(age[2] > 0 ? age[2].ToString() + "天" : "");
                        row.CreateCells(dataGridView1, i, obj.healthCardNo, obj.childName, obj.childGender,obj.childGender, agestr, obj.telephone,obj.fuzenday);
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
