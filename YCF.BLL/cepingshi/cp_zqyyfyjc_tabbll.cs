using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.DAL.cepingshi;
using YCF.Model;

namespace YCF.BLL.cepingshi
{
    public partial class cp_zqyyfyjc_tabbll : BLLBase<CP_ZQYYFYJC_TAB, cp_zqyyfyjc_tabdal>
    {

        public CP_ZQYYFYJC_TAB GetByCdId(int cd_id, string type, int externalid)
        {
            return dal.Get(t => t.CHILD_ID == cd_id && t.TYPE == type && t.EXTERNALID == externalid);
        }

        public bool DeleteByExternalid(int externalid)
        {
            return dal.Delete(t => t.EXTERNALID == externalid) > 0;
        }

        public bool SaveOrUpdate(CP_ZQYYFYJC_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }

        public IList<CP_ZQYYFYJC_TAB> GetList(int cd_id)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id, t => t.UPDATE_TIME, false);

        }
    }
}
