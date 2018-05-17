using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using YCF.Common;
using YCF.Model;

namespace YCF.DAL.Base
{
    /// <summary>
    /// 部分sql静态方法类
    /// lrp
    /// 2017-06-12
    /// 
    /// </summary>
    public class DALStatic
    {
        protected static Entities db = null;
        private static string connString = CommonHelper.EFConnentionString;

        /// <summary>
        /// 执行sql语句查询数据
        /// 实体类属性名与数据表字段名匹配
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static IList<T2> GetListBySql<T2>(string sql, params object[] parameters)
        {
            using (db = new Entities(connString))
            {
                return db.Database.SqlQuery<T2>(sql, parameters).ToList();
            }
        }

        /// <summary>
        /// 执行sql语句查询数据
        /// 实体类属性名与数据表字段名匹配
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static T2 GetBySql<T2>(string sql, params object[] parameters)
        {
            using (db = new Entities(connString))
            {
                return db.Database.SqlQuery<T2>(sql, parameters).FirstOrDefault();
            }
        }


        /// <summary>
        /// 执行sql语句，返回受影响的行数
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            using (db = new Entities(connString))
            {
                return db.Database.ExecuteSqlCommand(sql, parameters);
            }
        }

        public static System.Data.DataSet GetDataSet(string sql, System.Data.CommandType commandType, params object[] parameters)
        {
            var ds = new System.Data.DataSet();
            using (var db = new Entities(connString))
            using (var cmd = db.Database.Connection.CreateCommand() as SqlCommand)
            {
                cmd.CommandText = sql;
                cmd.CommandType = commandType;
                cmd.Parameters.Clear();
                if (parameters != null && parameters.Length > 1)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);

                return ds;
            }
        }
    }
}
