using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using YCF.BLL.Base;
using YCF.DAL.Base;
using YCF.DAL.xunlianshi;
using YCF.DAL.yuyue;
using YCF.Model;

namespace YCF.BLL.yuyue
{
    public partial class yy_asd_tabbll : BLLBase<YY_ASD_TAB, yy_asd_tabdal>
    {

        public bool SaveOrUpdate(YY_ASD_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }

        public IList<yy_asd_tabNotMap> GetListByTime(string startdate, string enddate, string pz_lx)
        {
            string sql = " select a.*,b.childname,b.childgender,b.childbirthday,b.telephone "
                + " from YY_ASD_TAB a "
                + " left join yy_pz_tab pz on a.yy_xm = pz.pz_xm "
                + " left join tb_childbase b on a.child_id=b.id "
                + " where a.yy_rq>='" + startdate + "' and a.yy_rq>='" + startdate + "' and pz.pz_lx = '" + pz_lx + "'";

            return BLLStatic.GetListBySql<yy_asd_tabNotMap>(sql);

        }

        public bool IsFull(string yy_xm, string date, string time)
        {
            var sql = "exec sp_IsFull @pz_xm, @date, @time";
            var prms = new SqlParameter[]
            {
                new SqlParameter("@pz_xm", yy_xm),
                new SqlParameter("@date", date),
                new SqlParameter("@time", time),
            };

            return BLLStatic.GetBySql<int>(sql, prms) > 0;
        }

        public IList<yy_asd_tabCount> GetList(DateTime startTime, DateTime endTime, string pz_lx)
        {
            var list = new List<yy_asd_tabCount>();
            while (startTime.Date <= endTime.Date)
            {
                var sql = @"SELECT   @yy_rq yy_rq ,
         pz_xm ,
         pzDetails.time time ,
         (   SELECT COUNT(1)
             FROM   dbo.YY_ASD_TAB
             WHERE  yy_xm = pz.pz_xm
                    AND yy_sjd = pzDetails.time
                    AND yy_rq = @yy_rq ) count ,
         pzDetails.countPerTime totalCount
FROM     [dbo].[yy_pz_tab] pz
         LEFT JOIN dbo.YY_PZ_DETAILS_TAB pzDetails ON pzDetails.pzid = pz.id
WHERE    pz.pz_lx = @pz_lx
         AND pz.pz_xq LIKE ( '%'
                             + CAST(( DATEPART(dw, @yy_rq) - 1 ) AS VARCHAR(1))
                             + '%' )
ORDER BY pz.id;";

                var prms = new SqlParameter[]
                {
                new SqlParameter("@pz_lx", pz_lx),
                new SqlParameter("@yy_rq", startTime.ToString("yyyy-MM-dd")),
                };

                list.AddRange(DALStatic.GetListBySql<yy_asd_tabCount>(sql, prms));

                startTime = startTime.AddDays(1);
            }

            return list;
        }

        public bool Exists(int id, string text)
        {
            return dal.Count(t => t.CHILD_ID == id && t.YY_XM == text) > 0;
        }
    }
}
