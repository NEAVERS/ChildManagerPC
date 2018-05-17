using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.DAL.cepingshi;
using YCF.Model;

namespace YCF.BLL.cepingshi
{
    public partial class cp_gmyjc_tabbll : BLLBase<CP_GMYJC_TAB, cp_gmyjc_tabdal>
    {

        public CP_GMYJC_TAB GetByCdId(int cd_id)
        {
            return dal.Get(t => t.CHILD_ID == cd_id );
        }
        public bool DeleteByCdId(int cd_id)
        {
            return dal.Delete(t => t.CHILD_ID == cd_id) > 0;
        }

        public bool SaveOrUpdate(CP_GMYJC_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }

        public IList<CP_GMYJC_TAB> GetList(int cd_id)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id, t => t.UPDATE_TIME, false);
        }
    }
}
