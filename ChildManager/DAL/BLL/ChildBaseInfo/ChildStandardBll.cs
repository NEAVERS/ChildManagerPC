using System;
using ChildManager.DAL;
using System.Windows.Forms;
using ChildManager.Model.ChildBaseInfo;
using System.Data.SqlClient;
using System.Collections;

namespace ChildManager.BLL.ChildBaseInfo
{
    class ChildStandardBll
    {
        DateLogic dg = new DateLogic();

        public ArrayList getchildStandardlist(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                ChildStandardObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new ChildStandardObj();
                        obj.id = Convert.ToInt32(sdr["id"]);
                        obj.yf = Convert.ToInt32(sdr["yf"]);
                        obj.sex = sdr["sex"].ToString();
                        obj.wd_3th = sdr["wd_3th"].ToString();
                        obj.wd_5th = sdr["wd_5th"].ToString();
                        obj.wd_10th = sdr["wd_10th"].ToString();
                        obj.wd_25th = sdr["wd_25th"].ToString();
                        obj.wd_50th = sdr["wd_50th"].ToString();
                        obj.wd_75th = sdr["wd_75th"].ToString();
                        obj.wd_90th = sdr["wd_90th"].ToString();
                        obj.wd_95th = sdr["wd_95th"].ToString();
                        obj.wd_97th = sdr["wd_97th"].ToString();
                        obj.ht_3th = sdr["ht_3th"].ToString();
                        obj.ht_5th = sdr["ht_5th"].ToString();
                        obj.ht_10th = sdr["ht_10th"].ToString();
                        obj.ht_25th = sdr["ht_25th"].ToString();
                        obj.ht_50th = sdr["ht_50th"].ToString();
                        obj.ht_75th = sdr["ht_75th"].ToString();
                        obj.ht_90th = sdr["ht_90th"].ToString();
                        obj.ht_95th = sdr["ht_95th"].ToString();
                        obj.ht_97th = sdr["ht_97th"].ToString();
                        obj.tw_3th = sdr["tw_3th"].ToString();
                        obj.tw_5th = sdr["tw_5th"].ToString();
                        obj.tw_10th = sdr["tw_10th"].ToString();
                        obj.tw_25th = sdr["tw_25th"].ToString();
                        obj.tw_50th = sdr["tw_50th"].ToString();
                        obj.tw_75th = sdr["tw_75th"].ToString();
                        obj.tw_90th = sdr["tw_90th"].ToString();
                        obj.tw_95th = sdr["tw_95th"].ToString();
                        obj.tw_97th = sdr["tw_97th"].ToString();

                        arraylist.Add(obj);
                    }
                }
                return arraylist;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }

        public ArrayList getchildStandard_w_hlist(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                ChildStandard_w_hObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new ChildStandard_w_hObj();
                        obj.id = Convert.ToInt32(sdr["id"]);
                        obj.sex = sdr["sex"].ToString();
                        obj.hight = Convert.ToInt32(sdr["hight"]);
                        obj.wd_3th = sdr["wd_3th"].ToString();
                        obj.wd_5th = sdr["wd_5th"].ToString();
                        obj.wd_10th = sdr["wd_10th"].ToString();
                        obj.wd_15th = sdr["wd_15th"].ToString();
                        obj.wd_25th = sdr["wd_25th"].ToString();
                        obj.wd_50th = sdr["wd_50th"].ToString();
                        obj.wd_75th = sdr["wd_75th"].ToString();
                        obj.wd_85th = sdr["wd_85th"].ToString();
                        obj.wd_90th = sdr["wd_90th"].ToString();
                        obj.wd_95th = sdr["wd_95th"].ToString();
                        obj.wd_97th = sdr["wd_97th"].ToString();
                        

                        arraylist.Add(obj);
                    }
                }
                return arraylist;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }

        public ArrayList getchildStandard_bmilist(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                ChildStandard_bmiObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new ChildStandard_bmiObj();
                        obj.id = Convert.ToInt32(sdr["id"]);
                        obj.yf = Convert.ToInt32(sdr["yf"]);
                        obj.sex = sdr["sex"].ToString();
                        obj.bmi_3th = sdr["bmi_3th"].ToString();
                        obj.bmi_5th = sdr["bmi_5th"].ToString();
                        obj.bmi_10th = sdr["bmi_10th"].ToString();
                        obj.bmi_15th = sdr["bmi_15th"].ToString();
                        obj.bmi_25th = sdr["bmi_25th"].ToString();
                        obj.bmi_50th = sdr["bmi_50th"].ToString();
                        obj.bmi_75th = sdr["bmi_75th"].ToString();
                        obj.bmi_85th = sdr["bmi_85th"].ToString();
                        obj.bmi_90th = sdr["bmi_90th"].ToString();
                        obj.bmi_95th = sdr["bmi_95th"].ToString();
                        obj.bmi_97th = sdr["bmi_97th"].ToString();
                        arraylist.Add(obj);
                    }
                }
                return arraylist;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }

        public ArrayList getwhoStandard_list(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                WhoChildStandardObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new WhoChildStandardObj();
                        obj.month = Convert.ToInt32(sdr["month"]);
                        obj.l = sdr["l"].ToString();
                        obj.m = sdr["m"].ToString();
                        obj.s = sdr["s"].ToString();
                        obj.sd = sdr["sd"].ToString();
                        obj.p01 = sdr["p01"].ToString();
                        obj.p1 = sdr["p1"].ToString();
                        obj.p3 = sdr["p3"].ToString();
                        obj.p5 = sdr["p5"].ToString();
                        obj.p10 = sdr["p10"].ToString();
                        obj.p15 = sdr["p15"].ToString();
                        obj.p25 = sdr["p25"].ToString();
                        obj.p50 = sdr["p50"].ToString();
                        obj.p75 = sdr["p75"].ToString();
                        obj.p85 = sdr["p85"].ToString();
                        obj.p90 = sdr["p90"].ToString();
                        obj.p95 = sdr["p95"].ToString();
                        obj.p97 = sdr["p97"].ToString();
                        obj.p99 = sdr["p99"].ToString();
                        obj.p999 = sdr["p999"].ToString();
                        obj.sex = sdr["sex"].ToString();
                        obj.ptype = sdr["ptype"].ToString();
                        obj.length = sdr["length"].ToString();

                        arraylist.Add(obj);
                    }
                }
                return arraylist;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }

        public ArrayList getwhoStandard_day_list(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                WhoChildStandardDayObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new WhoChildStandardDayObj();
                        obj.tian = Convert.ToInt32(sdr["tian"]);
                        obj.l = sdr["l"].ToString();
                        obj.m = sdr["m"].ToString();
                        obj.s = sdr["s"].ToString();
                        obj.p01 = sdr["p01"].ToString();
                        obj.p1 = sdr["p1"].ToString();
                        obj.p3 = sdr["p3"].ToString();
                        obj.p5 = sdr["p5"].ToString();
                        obj.p10 = sdr["p10"].ToString();
                        obj.p15 = sdr["p15"].ToString();
                        obj.p25 = sdr["p25"].ToString();
                        obj.p50 = sdr["p50"].ToString();
                        obj.p75 = sdr["p75"].ToString();
                        obj.p85 = sdr["p85"].ToString();
                        obj.p90 = sdr["p90"].ToString();
                        obj.p95 = sdr["p95"].ToString();
                        obj.p97 = sdr["p97"].ToString();
                        obj.p99 = sdr["p99"].ToString();
                        obj.p999 = sdr["p999"].ToString();
                        obj.sex = sdr["sex"].ToString();
                        obj.ptype = sdr["ptype"].ToString();

                        arraylist.Add(obj);
                    }
                }
                return arraylist;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }


        public ArrayList getwhoStandardlist(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                WhoChildStandardDayObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new WhoChildStandardDayObj();
                        //obj.tian = Convert.ToInt32(sdr["tian"]);
                        obj.l = sdr["l"].ToString();
                        obj.m = sdr["m"].ToString();
                        obj.s = sdr["s"].ToString();
                        obj.p01 = sdr["p01"].ToString();
                        obj.p1 = sdr["p1"].ToString();
                        obj.p3 = sdr["p3"].ToString();
                        obj.p5 = sdr["p5"].ToString();
                        obj.p10 = sdr["p10"].ToString();
                        obj.p15 = sdr["p15"].ToString();
                        obj.p25 = sdr["p25"].ToString();
                        obj.p50 = sdr["p50"].ToString();
                        obj.p75 = sdr["p75"].ToString();
                        obj.p85 = sdr["p85"].ToString();
                        obj.p90 = sdr["p90"].ToString();
                        obj.p95 = sdr["p95"].ToString();
                        obj.p97 = sdr["p97"].ToString();
                        obj.p99 = sdr["p99"].ToString();
                        obj.p999 = sdr["p999"].ToString();
                        obj.sex = sdr["sex"].ToString();
                        obj.ptype = sdr["ptype"].ToString();

                        arraylist.Add(obj);
                    }
                }
                return arraylist;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }
    }
}
