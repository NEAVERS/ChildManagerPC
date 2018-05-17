using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YCF.Model;
using YCF.DAL;
using YCF.BLL.Base;

namespace YCF.BLL
{
    public class ys_gdzpxza_tabbll : BLLBase<YS_GDZPXZA_TAB, ys_gdzpxza_tabdal>
    {
        public YS_GDZPXZA_TAB GetByCdId(int cd_id)
        {
            return dal.Get(t => t.CHILD_ID == cd_id);
        }

        public bool DeleteByCdId(int cd_id)
        {
            return dal.Delete(t => t.CHILD_ID == cd_id) > 0;
        }

        public bool SaveOrUpdate(YS_GDZPXZA_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }
    }
}
