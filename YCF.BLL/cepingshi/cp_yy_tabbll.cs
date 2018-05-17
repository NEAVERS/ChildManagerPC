using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.DAL.cepingshi;
using YCF.Model;

namespace YCF.BLL.cepingshi
{
    public partial class cp_yy_tabbll : BLLBase<CP_YY_TAB, cp_yy_tabdal>
    {
        private readonly List<Expression<Func<CP_YY_TAB, object>>> order;
        private readonly List<bool> isAscs;

        public cp_yy_tabbll()
        {
            order = new List<Expression<Func<CP_YY_TAB, object>>>() { t => t.YY_RQ };
            isAscs = new List<bool>() { false };
        }

        public IList<CP_YY_TAB> GetList(int cd_id)
        {
            var list = dal.GetList(t=>t.CHILD_ID==cd_id,order,isAscs);
            return list;
        }
        public CP_YY_TAB GetByCdId(int cd_id)
        {
            return dal.Get(t => t.CHILD_ID == cd_id );
        }

        public int GetSjdCou(string sjd,string rq)
        {
            return dal.Count(t => t.YY_SJD == sjd&&t.YY_RQ==rq);
        }

        public int GetXmCou(string xm, string rq)
        {
            return dal.Count(t => t.YY_XM == xm && t.YY_RQ == rq);
        }

        public bool DeleteByCdId(int cd_id)
        {
            return dal.Delete(t => t.CHILD_ID == cd_id) > 0;
        }

        public bool SaveOrUpdate(CP_YY_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }
    }
}
