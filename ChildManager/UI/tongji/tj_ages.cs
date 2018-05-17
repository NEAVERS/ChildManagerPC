using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YCF.BLL;
using YCF.Model;

namespace ChildManager.UI.tongji
{
    public partial class tj_ages : UserControl
    {
        tj_diqubll bll = new tj_diqubll();
        //private ChildMainForm _mianform = null;
        public tj_ages()
        {
            InitializeComponent();
            //_mianform = mianform;
        }      
        
        private void buttonX1_Click(object sender, EventArgs e)
        {
            string starttime = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string endtime = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string sqls = "select b.nnd couname,count(*) cou from TB_CHILDBASE a ";
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
            sqls += "  as nnd, id from TB_CHILDBASE";
            sqls += " ) b on a.id = b.id";
            sqls += " where  childbuildday>='" + starttime + "' and childbuildday <='" + endtime + "' ";
            sqls += " group by b.nnd order by nnd ";
            

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
        
    }
}
