using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.DAL;
using System.Data.SqlClient;

namespace ChildManager.UI
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sqls = "select * from temp1 ";
            DateLogic dg = new DateLogic();
            SqlDataReader sdr =  dg.executequery(sqls);
            if (sdr.HasRows)
            { 
                while(sdr.Read())
                {
                    string[] bb = sdr["aaaa"].ToString().Split(' ');
                    string[] dd = new string[20];
                    int j = 0;
                    for (int i = 0; i < bb.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(bb[i]))
                        {
                            dd[j] = bb[i];
                            j++;
                        }
                    }
                    string insertsql = "insert into who_childstand_day ("
                                        + " tian "
                                        + " ,l "
                                        + " ,m "
                                        + " ,s "
                                        + " ,p01 "
                                        + " ,p1 "
                                        + " ,p3 "
                                        + " ,p5 "
                                        + " ,p10 "
                                        + " ,p15 "
                                        + " ,p25 "
                                        + " ,p50 "
                                        + " ,p75 "
                                        + " ,p85 "
                                        + " ,p90 "
                                        + " ,p95 "
                                        + " ,p97 "
                                        + " ,p99 "
                                        + " ,p999 "
                                        + " ,sex "
                                        + " ,ptype "
                                        + " ) values ("
                                        + "'" + dd[0] + "'"
                                        + ",'" + dd[1] + "'"
                                        + ",'" + dd[2] + "'"
                                        + ",'" + dd[3] + "'"
                                        + ",'" + dd[4] + "'"
                                        + ",'" + dd[5] + "'"
                                        + ",'" + dd[6] + "'"
                                        + ",'" + dd[7] + "'"
                                        + ",'" + dd[8] + "'"
                                        + ",'" + dd[9] + "'"
                                        + ",'" + dd[10] + "'"
                                        + ",'" + dd[11] + "'"
                                        + ",'" + dd[12] + "'"
                                        + ",'" + dd[13] + "'"
                                        + ",'" + dd[14] + "'"
                                        + ",'" + dd[15] + "'"
                                        + ",'" + dd[16] + "'"
                                        + ",'" + dd[17] + "'"
                                        + ",'" + dd[18] + "'"
                                        + ",'"+textBox1.Text+"'"
                                        + ",'" + textBox2.Text + "'"
                                        + ")";
                    dg.executeupdate(insertsql);
                }
                
            }
            MessageBox.Show("转换完成");
            //string aa = "24      1       87.1161 0.03507 3.0551  77.7    80      81.4    82.1    83.2    83.9    85.1    87.1    89.2    90.3    91      92.1    92.9    94.2    ";
            
        }


       
    }
}

