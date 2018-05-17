using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL.xunlianshi;
using YCF.Model;

namespace YCF.BLL.xunlianshi
{
    public partial class xl_yy_tabbll : BLLBase<XL_YY_TAB, xl_yy_tabdal>
    {
        private readonly List<Expression<Func<XL_YY_TAB, object>>> order;
        private readonly List<bool> isAscs;

        public xl_yy_tabbll()
        {
            order = new List<Expression<Func<XL_YY_TAB, object>>>() { t => t.YY_RQ };
            isAscs = new List<bool>() { false };
        }

        public IList<XL_YY_TAB> GetList(int cd_id)
        {
            var list = dal.GetList(t=>t.CHILD_ID==cd_id,order,isAscs);
            return list;
        }
        public XL_YY_TAB GetByCdId(int cd_id)
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

        public bool SaveOrUpdate(XL_YY_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }
    }
}
