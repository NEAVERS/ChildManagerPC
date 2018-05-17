using System;
using ChildManager.DAL;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
using ChildManager.Model.childSetInfo;

namespace ChildManager.BLL.childSetInfo
{
    class childSetbaoJiangGuidBll
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
        public ArrayList getbaojianList(string sqls)
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
                        childSetbaoJiangGuidObj obj = new  childSetbaoJiangGuidObj();
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        //obj.ChildId = Convert.ToInt32(sdr["childId"]);
                        obj.Setage = sdr["setage"].ToString();
                        obj.Eduction = sdr["eduction"].ToString();
                        obj.Shanshi = sdr["shanshi"].ToString();
                        obj.Jibing = sdr["jibing"].ToString();
                        obj.Huli = sdr["huli"].ToString();
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



/// <summary>
/// 儿童保健对象
/// </summary>
/// <param name="sqls"></param>
/// <returns></returns>
        public childSetbaoJiangGuidObj getbaojianobj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            childSetbaoJiangGuidObj obj = null;
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
                    obj = new  childSetbaoJiangGuidObj();
                    sdr.Read();//读取第一行数据记录
                    obj.Id = Convert.ToInt32(sdr["id"]);
                    //obj.ChildId = Convert.ToInt32(sdr["childId"]);
                    obj.Setage = sdr["setage"].ToString();
                    obj.Eduction = sdr["eduction"].ToString();
                    obj.Shanshi = sdr["shanshi"].ToString();
                    obj.Jibing = sdr["jibing"].ToString();
                    obj.Huli = sdr["huli"].ToString();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
                //ErrorHandle.OnError(ex);
            }
        }
    }
}
