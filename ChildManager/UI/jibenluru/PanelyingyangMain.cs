using System;
using System.Drawing;
using System.Windows.Forms;
using YCF.Common;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.Model;
using ChildManager.BLL;
using System.Collections;
using login.UI.printrecord;

namespace ChildManager.UI.jibenluru
{
    public partial class PanelyingyangMain : UserControl
    {
        private ChildBaseInfoBll bll = new ChildBaseInfoBll();//儿童建档基本信息业务处理类
        private ProjectGaoweibll gwbll = new ProjectGaoweibll();

        public PanelyingyangMain()
        {
            InitializeComponent();
            this.cbx_gaowei.Text = "";
            ArrayList list = gwbll.getContentlist(2);
            if (list != null && list.Count > 0)
            {
                foreach (ProjectGaoweiobj item in list)
                {
                    this.cbx_gaowei.Items.Add(item.content);
                }
            }

        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
           
        }
        // 回车代替tab键盘
        private void all_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
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

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrinter_Click(object sender, EventArgs e)
        {
            printtijianjilu(false);
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPriview_Click(object sender, EventArgs e)
        {
            printtijianjilu(true);
        }

        private void printtijianjilu(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string projectName = "";
                string formName = "儿童营养性疾病管理登记表";
                PanelYingyangmianPrinter dgvPrinter;
                if (dataGridView1 != null && dataGridView1.Rows.Count > 0)
                {
                    dgvPrinter = new PanelYingyangmianPrinter(dataGridView1);
                    dgvPrinter.Margin = new System.Drawing.Printing.Margins(50, 30, 30, 30);
                    dgvPrinter.PageHeaders.Add(new PanelYingyangmianPrinter.Header(projectName, new Font("Arial Unicode MS", 16f), 45));
                    dgvPrinter.PageHeaders.Add(new PanelYingyangmianPrinter.Header(formName, new Font("Arial Unicode MS", 19f, FontStyle.Bold), 45));
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


        private void all_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");

        }

        private void buttonX7_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string starttime = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            string endtime = dateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            string sqls = "select *,a.id cdid ,b.fuzenday,b.zhushi from "
            //+ " ("
            //+ "select childid,(select gaoweiyinsu+',' from tb_gaowei where childid=b.childid for xml path('')) as gaoweistr from tb_gaowei b where b.type='营养不良' group by childid"
            //+ ") c "
            + " TB_CHILDBASE a  "
            + " left join(  select childid,max(fuzenday) fuzenday from TB_CHILDCHECK  group by childid  ) c  "
            + " on a.id=c.childid   "
            + " left join TB_CHILDCHECK b  on b.childid=c.childid and b.fuzenday=c.fuzenday  "
            + " left join child_yingyanggean d on a.id=d.childid "
            + "where  childBuildDay>='" + starttime + "' and childBuildDay <='" + endtime + "' and yingyangbuliang is not null and yingyangbuliang!=''";

            string gaoweiyinsu = cbx_gaowei.Text.Trim();
            string zhuangui = getcheckedValue(pnl_zhuangui);
            string zhuangtai = getcheckedValue(panel2);

            if (!String.IsNullOrEmpty(gaoweiyinsu))
            {
                sqls += " and yingyangbuliang like '%" + gaoweiyinsu + "%'";
            }

            if (!String.IsNullOrEmpty(zhuangui))
            {
                sqls += " and zhuangui = '" + zhuangui + "'";
            }

            if (!String.IsNullOrEmpty(zhuangtai))
            {
                if (zhuangtai == "正在随访")
                {
                    sqls += " and (endtime is null or endtime ='' or endtime='0001-01-01')";
                }
                else
                {
                    sqls += " and (endtime is not null and endtime !='' and endtime!='0001-01-01')";
                }
            }

            sqls += " order by childBuildDay desc";

            ArrayList list = bll.getchildBaseYingyangList(sqls);
            dataGridView1.Rows.Clear();
            if (list != null && list.Count > 0)
            {
                
                try
                {
                    int i = 1;
                    foreach (ChildBaseInfoObj obj in list)
                    {
                        if (obj.endtime == "0001-01-01")
                        {
                            obj.endtime = "";
                        }

                        string birthstr = "";
                        string buidstr = "";
                        DateTime outtime = new DateTime();
                        if (DateTime.TryParse(obj.ChildBirthDay, out outtime))
                        {
                            birthstr = outtime.ToString("yyyy-MM-dd");
                        }
                        if (DateTime.TryParse(obj.ChildBuildDay, out outtime))
                        {
                            buidstr = outtime.ToString("yyyy-MM-dd");
                        }

                        DataGridViewRow row = new DataGridViewRow();

                        if (String.IsNullOrEmpty(obj.zhushi) || String.IsNullOrEmpty(obj.fuzhenday) || Convert.ToDateTime(obj.fuzhenday) <= Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59"))
                        {
                            row.DefaultCellStyle.ForeColor = Color.Red;
                        }
                        else
                        {
                            row.DefaultCellStyle.ForeColor = Color.Green;
                        }

                        row.CreateCells(dataGridView1, i,obj.HealthCardNo, obj.ChildName, obj.ChildGender,
                            birthstr, "", obj.address, obj.Telephone, "", "", obj.managetime, obj.endtime, obj.zhuangui);
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                ChildBaseInfoObj baseobj = dataGridView1.CurrentRow.Tag as ChildBaseInfoObj;
                PanelYingyangZhuananMain yingyangzhuananmain = new PanelYingyangZhuananMain(baseobj);
                yingyangzhuananmain.ShowDialog();
                //if (baseobj.yingyangbuliang.Contains("佝偻病"))
                //{
                //    Paneltsb_goulou_zhuanan zhuananadd = new Paneltsb_goulou_zhuanan(baseobj);
                //    zhuananadd.ShowDialog();
                //}
                //else if (baseobj.yingyangbuliang.Contains("贫血"))
                //{
                //    Paneltsb_pinxue_zhuanan zhuananadd = new Paneltsb_pinxue_zhuanan(baseobj);
                //    zhuananadd.ShowDialog();
                //}
                //else
                //{
                //    Paneltsb_yingyang_zhuanan zhuananadd = new Paneltsb_yingyang_zhuanan(baseobj);
                //    zhuananadd.ShowDialog();
                //}
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

        private void toBaseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0 && dataGridView1.CurrentCell.RowIndex != -1)
            {
                ChildBaseInfoObj obj = dataGridView1.CurrentRow.Tag as ChildBaseInfoObj;
                int id = obj.Id;
                globalInfoClass.Wm_Index = id;
                ChildMainForm mianform = this.ParentForm as ChildMainForm;
                mianform.updateMdiForm("儿保建卡", typeof(PanelyibanxinxiMain));
            }
            else
            {
                MessageBox.Show("请选择要跳转的儿童");
            }
        }

        /// <summary>
        /// 右键选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex != -1)
            {
                    this.dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }


    }
}
