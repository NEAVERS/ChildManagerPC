using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using ChildManager.DAL;
using login.Model;

namespace login.BLL
{
    class UpdateVersionbll
    {
        DateLogic dg = new DateLogic();
        //保存一般护理记录单
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
        //修改一般护理记录单模板
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

        //删除一般护理记录单模板
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
        /// 查询一般护理记录单记录
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public UpdateVersionobj getUpdateVersionobj(string sqls)
        {

            DateLogic dg = new DateLogic();
            SqlDataReader drs = null;

            try
            {
                drs = dg.executequery(sqls);
                UpdateVersionobj updateobj = null;
                if (!drs.HasRows)
                {
                    return null;
                }
                else
                {
                    drs.Read();//读取第一行数据记录

                    updateobj = new UpdateVersionobj();

                    updateobj.id = Convert.ToInt32(drs["id"]);
                    updateobj.version = drs["version"].ToString();
                    updateobj.updatetime = drs["updatetime"].ToString();
                    updateobj.updatecontent = drs["updatecontent"].ToString();
                    updateobj.isfabu = Convert.ToInt32(drs["isfabu"]);

                }
                return updateobj;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }

        }
        /// <summary>
        /// 查询更新记录列表
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getUpdateVersionList(string sqls)
        {
            SqlDataReader drs = null;
            ArrayList updatelist = new ArrayList();
            try
            {
                drs = dg.executequery(sqls);
                while (drs.Read())
                {
                    UpdateVersionobj updateobj = new UpdateVersionobj();
                    updateobj.id = Convert.ToInt32(drs["id"]);
                    updateobj.version = drs["version"].ToString();
                    updateobj.updatetime = drs["updatetime"].ToString();
                    updateobj.updatecontent = drs["updatecontent"].ToString();
                    updateobj.isfabu = Convert.ToInt32(drs["isfabu"]);

                    updatelist.Add(updateobj);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("统计异常", "软件提示");
                throw ex;
            }
            finally
            {
                drs.Close();
                dg.con_close();
            }

            return updatelist;
        }

    
    }
}
