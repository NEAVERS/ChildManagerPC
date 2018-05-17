using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.DAL.cepingshi;
using YCF.Model;

namespace YCF.BLL.cepingshi
{
    public partial class cp_adhd_tabbll : BLLBase<CP_ADHD_TAB, cp_adhd_tabdal>
    {

        public CP_ADHD_TAB GetByCdId(int cd_id)
        {
            return dal.Get(t => t.CHILD_ID == cd_id );
        }

        public bool DeleteByCdId(int cd_id)
        {
            return dal.Delete(t => t.CHILD_ID == cd_id)>0;
        }

        public bool SaveOrUpdate(CP_ADHD_TAB adhdobj)
        {
            if (adhdobj.ID != 0)
                return dal.Update(adhdobj) > 0;
            else
                return dal.Add(adhdobj) > 0;
        }

        public IList<CP_ADHD_TAB> GetList(int cd_id)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id, t => t.UPDATE_TIME, false);
        }
    }
}
