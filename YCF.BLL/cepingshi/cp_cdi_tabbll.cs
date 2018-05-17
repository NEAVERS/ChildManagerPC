using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.DAL.cepingshi;
using YCF.Model;

namespace YCF.BLL.cepingshi
{
    public partial class cp_cdi_tabbll : BLLBase<CP_CDI_TAB, cp_cdi_tabdal>
    {

        public CP_CDI_TAB GetByCdId(int cd_id)
        {
            return dal.Get(t => t.CHILD_ID == cd_id );
        }
        public bool DeleteByCdId(int cd_id)
        {
            return dal.Delete(t => t.CHILD_ID == cd_id) > 0;
        }

        public bool SaveOrUpdate(CP_CDI_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }

        public IList<CP_CDI_TAB> GetList(int cd_id)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id, t => t.UPDATE_TIME, false);
        }
        public IList<CP_CDI_TAB> GetList(int cd_id,string type)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id&&  t.TYPE== type);
        }

        public CP_CDI_TAB Get(int id, string type)
        {
            return dal.Get(t => t.ID == id && t.TYPE == type);
        }

        public IList<CP_CDI_TAB> GetList(int cd_id, string type, string hospital)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id && t.TYPE == type && t.HOSPITAL == hospital);
        }
    }
}
