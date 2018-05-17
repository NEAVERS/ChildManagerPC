using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.DAL.cepingshi;
using YCF.Model;

namespace YCF.BLL.cepingshi
{
    public partial class cp_ppvt_tabbll : BLLBase<CP_PPVT_TAB, cp_ppvt_tabdal>
    {

        public CP_PPVT_TAB GetByCdId(int cd_id, string type, int externalid)
        {
            return dal.Get(t => t.CHILD_ID == cd_id && t.TYPE == type && t.EXTERNALID == externalid);
        }
        public bool DeleteByCdId(int cd_id)
        {
            return dal.Delete(t => t.CHILD_ID == cd_id) > 0;
        }
        public bool DeleteByExternalid(int externalid)
        {
            return dal.Delete(t => t.EXTERNALID == externalid) > 0;
        }
        public CP_PPVT_TAB GetByType(int cd_id, string type, int externalid)
        {
            return dal.Get(t => t.CHILD_ID == cd_id && t.TYPE == type && t.EXTERNALID == externalid);
        }
        public bool SaveOrUpdate(CP_PPVT_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }

        public IList<CP_PPVT_TAB> GetList(int cd_id)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id, t => t.UPDATE_TIME, false);
        }

        public IList<CP_PPVT_TAB> GetList(int cd_id, string type)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id && t.TYPE == type);
        }
    }
}
