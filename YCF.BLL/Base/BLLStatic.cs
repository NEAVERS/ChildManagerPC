using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using YCF.Common;
using YCF.DAL.Base;
using YCF.Model;

namespace YCF.BLL.Base
{
    /// <summary>
    /// 部分sql静态方法类
    /// lrp
    /// 2017-06-12
    /// 
    /// </summary>
    public class BLLStatic
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
            return DALStatic.GetListBySql<T2>(sql, parameters);
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
            return DALStatic.GetBySql<T2>(sql, parameters);
        }


        /// <summary>
        /// 执行sql语句，返回受影响的行数
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return DALStatic.ExecuteSqlCommand(sql, parameters);
        }


        public static DataSet GetDataSet(string sql, System.Data.CommandType commandType, params object[] parameters)
        {
            return DALStatic.GetDataSet(sql, commandType, parameters);
        }
    }
}
