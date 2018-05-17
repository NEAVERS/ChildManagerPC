using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChildManager.DAL;
using System.Windows.Forms;
using System.Data.SqlClient;
using ChildManager.Model;
using System.Collections;

namespace ChildManager.BLL
{
    class ProjectGaoweibll
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
        /// 查询儿童体检情况基本信息
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ProjectGaoweiobj getprojectobj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            try
            {
                sdr = dg.executequery(sqls);
                ProjectGaoweiobj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    sdr.Read();//读取第一行数据记录
                    obj = new ProjectGaoweiobj();
                    obj.id = Convert.ToInt32(sdr["id"]);
                    obj.type = sdr["type"].ToString();
                }
                return obj;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }



        public ArrayList getContentlist(int p_id)
        {
            string sqls="select * from tb_gaoweicontent where p_id="+p_id;
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                ProjectGaoweiobj obj = null;
                while (sdr.Read())
                {
                    obj = new ProjectGaoweiobj();
                    obj.id = Convert.ToInt32(sdr["id"]);
                    obj.type = sdr["p_id"].ToString();
                    obj.content = sdr["content"].ToString();
                    arraylist.Add(obj);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }

            return arraylist;
        }

    }
}
