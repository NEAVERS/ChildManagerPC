using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using YCF.BLL.Base;
using YCF.Model;

namespace YCF.BLL
{
    public class ys_ceping_tabbll
    {
        public IList<ys_ceping_tab> GetList(int cd_id)
        {
            var sql = "exec sp_GetCeping @cd_id";

            var prms = new SqlParameter[]
            {
                new SqlParameter("@cd_id", cd_id),
            };

            return BLLStatic.GetListBySql<ys_ceping_tab>(sql, prms);
        }
    }
}
