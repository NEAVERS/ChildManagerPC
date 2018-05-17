using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using YCF.DAL;
using YCF.Model;
using YCF.DAL.Base;
using YCF.DAL.Template;
using System.Data.SqlClient;
using YCF.BLL.Base;

namespace YCF.BLL.Template
{
    public partial class tab_yysc_Numbll : BLLBase<TAB_YYSC_NUM, tab_yysc_Numdal>
    {
        protected new List<Expression<Func<TAB_YYSC_NUM, int>>> orders;

        public tab_yysc_Numbll()
        {
            orders = new List<Expression<Func<TAB_YYSC_NUM, int>>>() { t => t.YUELING };
            isAscs = new List<bool>() { true };
        }
        public TAB_YYSC_NUM GetObj(string type, int yueling)
        {
            return dal.Get(t => t.TYPE == type && t.YUELING == yueling);
        }

        public TAB_YYSC_NUM GetMaxYueling(string type)
        {
            var list = dal.GetList(t => t.TYPE == type, orders, isAscs);
            int count = list.Count();
            return list[count - 1];
        }
        public int GetMinYueling(string type)
        {
            var list = dal.GetList(t => t.TYPE == type, orders, isAscs);
            return list[0].YUELING;
        }

        public string GetResultP50(string type, string num)
        {
            IList<TAB_YYSC_NUM> listcount = dal.GetList(t => t.TYPE == type && t.P50 == num, orders, isAscs);
            if (listcount.Count > 0)
            {
                if (listcount.Count == 1)
                {
                    return listcount[0].YUELING.ToString();
                }
                else
                {
                    int count = listcount.Count;
                    string minStr = listcount[0].YUELING.ToString();
                    string maxStr = listcount[count - 1].YUELING.ToString();
                    return minStr + "-" + maxStr;
                }
            }
            else
            {
                string sql = "exec sp_GetYueling_p50 @type, @num";

                var prms = new SqlParameter[]
                {
                new SqlParameter("@type", type),
                new SqlParameter("@num", num),
                };

                var list = BLLStatic.GetListBySql<int?>(sql, prms);

                var yueliiang = list[0];
                var yueliiang2 = list[1];


                if (yueliiang == null)
                {
                    return "<" + yueliiang2;
                }
                else if (yueliiang2 == null)
                {
                    return ">" + yueliiang;
                }
                else if (yueliiang == yueliiang2)
                {
                    return yueliiang.ToString();
                }
                else if (yueliiang < yueliiang2)
                {
                    return yueliiang + "-" + yueliiang2;
                }
                else
                {
                    return yueliiang2 + "-" + yueliiang;
                }
            }
        }

        public string GetResultP75(string type, string num)
        {
            IList<TAB_YYSC_NUM> listcount = dal.GetList(t => t.TYPE == type && t.P75 == num, orders, isAscs);
            if (listcount.Count > 0)
            {
                if (listcount.Count == 1)
                {
                    return listcount[0].YUELING.ToString();
                }
                else
                {
                    int count = listcount.Count;
                    string minStr = listcount[0].YUELING.ToString();
                    string maxStr = listcount[count - 1].YUELING.ToString();
                    return minStr + "-" + maxStr;
                }
            }
            else
            {
                string sql = "exec sp_GetYueling_p75 @type, @num";
                var prms = new SqlParameter[]
                {
                new SqlParameter("@type", type),
                new SqlParameter("@num", num),
                };

                var list = BLLStatic.GetListBySql<int?>(sql, prms);

                var yueliiang = list[0];
                var yueliiang2 = list[1];


                if (yueliiang == null)
                {
                    return "<" + yueliiang2;
                }
                else if (yueliiang2 == null)
                {
                    return ">" + yueliiang;
                }
                else if (yueliiang == yueliiang2)
                {
                    return yueliiang.ToString();
                }
                else if (yueliiang < yueliiang2)
                {
                    return yueliiang + "-" + yueliiang2;
                }
                else
                {
                    return yueliiang2 + "-" + yueliiang;
                }
            }
        }
    }
}
