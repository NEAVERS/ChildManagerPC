using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ChildManager.DAL;
using System.Data.SqlClient;
using ChildManager.Model.childSetInfo;
using System.Windows.Forms;

namespace ChildManager.BLL.childSetInfo
{
    class childSetNutritionguidBll
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
        /// 查询儿童体检信息  返回集合
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getNutritionguidList(string sqls)
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
                        childSetNutritionguidObj obj = new childSetNutritionguidObj();
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        //obj.ChildId = Convert.ToInt32(sdr["childId"]);
                        obj.SetAge = sdr["setAge"].ToString();
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

        public childSetNutritionguidObj getNutritionguidobj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            childSetNutritionguidObj obj = null;
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
                         obj = new childSetNutritionguidObj();
                        sdr.Read();//读取第一行数据记录
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        //obj.ChildId = Convert.ToInt32(sdr["childId"]);
                        obj.SetAge = sdr["setAge"].ToString();
                        //obj.SetContent = sdr["setContent"].ToString();
                        obj.Shiyongxing = sdr["shiyongxing"].ToString();
                        obj.Bigdongzuo = sdr["bigdongzuo"].ToString();
                        //obj.TYPE =Convert.ToInt32(sdr["type"]);
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
