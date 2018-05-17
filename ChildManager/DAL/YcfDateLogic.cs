using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YCF.Common;
using System.Data.SqlClient;
using System.Data;

namespace ChildManager.DAL
{
    public class YcfDateLogic
    {
        public SqlConnection jianyi_conn;
        public string db_name = "";//数据库名称
        public string db_server = "";//数据库实例名
        public string UserID = "";//连接数据库用户名
        public string Pwd = "";
        public string str_conn = "";



        //初始化数据库链接基本信息
        public YcfDateLogic()
        {
            db_name = OperatFile.GetIniFileString("DataBase", "ycf_db_name", "", Application.StartupPath + "\\jianyi.ini");
            db_server = OperatFile.GetIniFileString("DataBase", "ycf_db_server", "", Application.StartupPath + "\\jianyi.ini");
            UserID = OperatFile.GetIniFileString("DataBase", "ycf_UserID", "", Application.StartupPath + "\\jianyi.ini");
            Pwd = OperatFile.GetIniFileString("DataBase", "ycf_Pwd", "", Application.StartupPath + "\\jianyi.ini");
            str_conn = "Server=" + db_server + ";Database=" + db_name + ";uid=" + UserID + ";pwd=" + Pwd + ";Connect Timeout=4";
        }

        //获取sql2005数据库的连接对象
        public SqlConnection getconn()
        {
            try
            {
                jianyi_conn = new SqlConnection(str_conn);//建立数据库连接
                jianyi_conn.Open();//打开数据库连接
                return jianyi_conn;
            }
            catch (Exception e)//连接数据库错误，抛出异常
            {
                throw e;
            }
        }

        //打开数据库
        public void con_open()
        {
            getconn();

        }

        //关闭销毁数据库
        public void con_close()
        {
            try
            {
                if (jianyi_conn != null)
                {
                    if (jianyi_conn.State == ConnectionState.Open)   //判断是否打开与数据库的连接
                    {
                        jianyi_conn.Close();   //关闭数据库的连接
                        jianyi_conn.Dispose();   //释放jianyi_conn变量的所有空间
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //执行数据库查询函数
        public SqlDataReader executequery(string sqls)
        {
            getconn();
            SqlCommand jianyi_comm = new SqlCommand(sqls, jianyi_conn);
            SqlDataReader jianyi_reader = jianyi_comm.ExecuteReader();
            jianyi_comm.Dispose();//SqlCommand对象销毁
            //con_close();//关闭数据库连接
            return jianyi_reader;
        }
        //执行增、删、改sql函数
        public int executeupdate(String sqls)
        {
            getconn();
            SqlCommand mycomm = new SqlCommand(sqls, jianyi_conn);
            int rows = mycomm.ExecuteNonQuery();
            mycomm.Dispose();//SqlCommand对象销毁
            con_close();//关闭数据库连接
            return rows;
        }
        //获取统计函数最大值
        public int stateval(string sqls)
        {
            getconn();
            int returns = 0;
            SqlCommand comms = new SqlCommand();
            comms.Connection = jianyi_conn;
            comms.CommandText = sqls;
            comms.CommandType = CommandType.Text;
            object obj = comms.ExecuteScalar();

            if (obj != null && obj != DBNull.Value)
            {
                returns = Convert.ToInt32(obj);
            }
            comms.Dispose();//SqlCommand对象销毁
            con_close();//关闭数据库连接
            return returns;//返回mrn数据库最大值
        }
        // //获取sql2005数据库命令行对象
        // public SqlCommand jianyicomm()
        // {

        //     return jianyi_comm;
        // }
        ////返回受影响的行数,一般增删改调用本方法
        // public int ExecdataBysql(String querysql)
        // {
        //     int returnvaluerows;
        //     jianyi_comm.CommandType = CommandType.Text;
        //     jianyi_comm.CommandText = querysql;

        //     try
        //     {
        //         if (jianyi_conn.State == ConnectionState.Closed)
        //         {
        //             jianyi_conn.Open();
        //         }

        //         returnvaluerows =Convert.ToInt32(jianyi_comm.ExecuteScalar());
        //     }
        //     catch (Exception e)
        //     {
        //         throw e;
        //     }
        //     finally
        //     {

        //         jianyi_conn.Close();
        //     }
        //     return returnvaluerows;

        // }
        // //获取数据库SqlDataReader对象
        // public SqlDataReader getDataReader(String sqls)
        // {
        //     SqlDataReader sdr;
        //     jianyi_comm.CommandType=CommandType.Text;
        //     jianyi_comm.CommandText = sqls;
        //     try
        //     {
        //        if(jianyi_conn.State==ConnectionState.Closed)
        //         {
        //             jianyi_conn.Open();
        //         }

        //        sdr = jianyi_comm.ExecuteReader();//在sqldataread关闭时会自动关闭
        //     }catch(Exception e)
        //     {
        //         throw e;
        //     }
        //     return sdr;
        // }

    }
}
