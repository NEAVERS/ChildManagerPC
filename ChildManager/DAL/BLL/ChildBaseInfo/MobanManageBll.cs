using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChildManager.DAL;
using System.Windows.Forms;
using ChildManager.Model.ChildBaseInfo;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using YCF.Common;
using ChildManager.Model.childSetInfo;

namespace ChildManager.BLL.ChildBaseInfo
{
    class MobanManageBll
    {
        DateLogic dg = new DateLogic();

        /// <summary>
        /// 查询模板列表
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getMobanList(string sqls)
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
                        mobanManageObj obj = new mobanManageObj();
                        obj.id = Convert.ToInt32(sdr["id"]);
                        obj.m_name = sdr["m_name"].ToString();
                        obj.m_type = sdr["m_type"].ToString();
                        obj.m_content = sdr["m_content"].ToString();
                        obj.yf = sdr["yf"].ToString();
                        obj.yfend = sdr["yfend"].ToString();
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

        //保存
        public bool saveMoban(mobanManageObj mobanobj)
        {
                try
                {
                    string sqls = "";
                    if (mobanobj.id != 0)
                    {
                        sqls = "update tb_moban set "
                                + " m_name = '" + mobanobj.m_name + "'"
                                + " ,m_type = '" + mobanobj.m_type + "'"
                                + " ,m_content = N'" + mobanobj.m_content + "'"
                                + " ,yf = '" + mobanobj.yf + "'"
                                + " ,yfend = '" + mobanobj.yfend + "'"
                                + " where id= " + mobanobj.id;
                    }
                    else
                    {
                        sqls = "insert into tb_moban ("
                                + "m_name"
                                + ",m_type"
                                + ",m_content"
                                + ",yf"
                                + ",yfend"
                                + " ) values ( "
                                + "'" + mobanobj.m_name + "'"
                                + ",'" + mobanobj.m_type + "'"
                                + ",N'" + mobanobj.m_content + "'"
                                + ",'" + mobanobj.yf + "'"
                                + ",'" + mobanobj.yfend + "'"
                                + ")";
                    }

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

        public bool ifhascount(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader drs = null;
            try
            {
                drs = dg.executequery(sqls);

                if (drs.HasRows)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                drs.Close();
                dg.con_close();
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

    }
}
