using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.DAL.cepingshi;
using YCF.Model;

namespace YCF.BLL.cepingshi
{
    public partial class cp_asd1_tabbll : BLLBase<CP_ASD1_TAB, cp_asd1_tabdal>
    {

        public CP_ASD1_TAB GetByCdId(int cd_id)
        {
            return dal.Get(t => t.CHILD_ID == cd_id );
        }

        public bool DeleteByCdId(int cd_id)
        {
            return dal.Delete(t => t.CHILD_ID == cd_id) > 0;
        }

        public bool SaveOrUpdate(CP_ASD1_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }

        public IList<CP_ASD1_TAB> GetList(int cd_id)
        {
            return dal.GetList(t => t.CHILD_ID == cd_id, t => t.UPDATE_TIME, false);
        }
    }
}
