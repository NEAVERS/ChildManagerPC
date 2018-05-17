using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.DAL.cepingshi;
using YCF.DAL.nursinfo;
using YCF.Model;

namespace YCF.BLL.nursinfo
{
    public partial class hs_yy_xx_tabbll : BLLBase<HS_YY_XX_TAB, hs_yy_xx_tabdal>
    {
        private readonly List<Expression<Func<HS_YY_XX_TAB, object>>> order;
        private readonly List<bool> isAscs;

        public hs_yy_xx_tabbll()
        {
            order = new List<Expression<Func<HS_YY_XX_TAB, object>>>() { t => t.YY_RQ };
            isAscs = new List<bool>() { false };
        }

        public IList<HS_YY_XX_TAB> GetList(int cd_id)
        {
            var list = dal.GetList(t=>t.CHILD_ID==cd_id,order,isAscs);
            return list;
        }
        public HS_YY_XX_TAB GetByCdId(int cd_id)
        {
            return dal.Get(t => t.CHILD_ID == cd_id );
        }

        public bool DeleteByCdId(int cd_id)
        {
            return dal.Delete(t => t.CHILD_ID == cd_id) > 0;
        }

        public bool SaveOrUpdate(HS_YY_XX_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }
    }
}
