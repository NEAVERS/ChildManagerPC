using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.BLL.tongji;
using System.Collections;
using ChildManager.Model;

namespace ChildManager.UI.tongji
{
    public partial class tj_renci : UserControl
    {
        private ChildBaseInfoBll childBaseInfobll = new ChildBaseInfoBll();//儿童建档基本信息业务处理类
        tj_tijianBll bll = new tj_tijianBll();

        public tj_renci()
        {
            InitializeComponent();
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

            string fuzhujiancha = cbx_fuzhujianchan.Text.Trim();

            if (!String.IsNullOrEmpty(fuzhujiancha))
            {
                sqls += " and fuzhujiancha like '%" + fuzhujiancha + "%'";
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
                            birthstr, obj.baseinfoobj.MotherName, obj.baseinfoobj.Telephone, obj.baseinfoobj.gaowei, obj.CheckDay, obj.DoctorName);
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
    }
}
