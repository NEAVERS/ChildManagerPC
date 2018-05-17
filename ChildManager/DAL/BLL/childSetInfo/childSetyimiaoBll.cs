using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ChildManager.DAL;
using System.Data.SqlClient;
using System.Windows.Forms;
using ChildManager.Model.childSetInfo;

namespace ChildManager.BLL.childSetInfo
{
    class childSetyimiaoBll
    {
        DateLogic dg = new DateLogic();
        //保存
        public bool saverecord(string sqls)
        {
            try
            {
                if (dg.executeupdate(sqls) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败", "软件提示");
                throw ex;
            }

        }
        //修改
        public int updaterecord(string sqls)
        {
            DateLogic dg = new DateLogic();
            try
            {
                return dg.executeupdate(sqls);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败", "软件提示");
                throw ex;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public int deleterecord(string sqls)
        {
            DateLogic dg = new DateLogic();
            try
            {
                return dg.executeupdate(sqls);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败", "软件提示");
                throw ex;
            }
        }
        /// <summary>
        /// 查询儿童体检信息  返回集合
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getyimiaoList(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList(); ;
            try
            {
                sdr = dg.executequery(sqls);
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        //sdr.Read();//读取第一行数据记录
                        childSetyimiaoObj obj = new childSetyimiaoObj();
                        obj.ID = Convert.ToInt32(sdr["id"]);
                        //obj.ChildId = Convert.ToInt32(sdr["childId"]);
                        obj.YimiaoName = sdr["yimiaoName"].ToString();
                        obj.FactorName = sdr["factorName"].ToString();
                        obj.ProductionDay = sdr["productionDay"].ToString();
                        obj.Pihao = sdr["pihao"].ToString();
                        obj.Guige = sdr["guige"].ToString();
                        obj.JiliangUnit = sdr["jiliangUnit"].ToString();
                        obj.Month = sdr["month"].ToString();
                        arraylist.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
            return arraylist;
        }

        public childSetyimiaoObj getyimiaoobj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            childSetyimiaoObj obj = null;
            ArrayList arraylist = new ArrayList(); ;
            try
            {
                sdr = dg.executequery(sqls);
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    obj = new childSetyimiaoObj();
                    sdr.Read();//读取第一行数据记录
                    obj.ID = Convert.ToInt32(sdr["id"]);
                    //obj.ChildId = Convert.ToInt32(sdr["childId"]);
                    obj.YimiaoName = sdr["yimiaoName"].ToString();
                    obj.FactorName = sdr["factorName"].ToString();
                    obj.ProductionDay = sdr["productionDay"].ToString();
                    obj.Pihao = sdr["pihao"].ToString();
                    obj.Guige = sdr["guige"].ToString();
                    obj.JiliangUnit = sdr["jiliangUnit"].ToString();
                    obj.Month = sdr["month"].ToString();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }
    }
}
