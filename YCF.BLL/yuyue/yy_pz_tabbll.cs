using System;
using System.Collections.Generic;
using YCF.DAL.xunlianshi;
using YCF.DAL.yuyue;
using YCF.Model;

namespace YCF.BLL.yuyue
{
    public partial class yy_pz_tabbll : BLLBase<YY_PZ_TAB, yy_pz_tabdal>
    {

        public bool SaveOrUpdate(YY_PZ_TAB obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }

        public YY_PZ_TAB GetByXm(string yyxm)
        {
            return dal.Get(t => t.PZ_XM == yyxm);
        }

        public IList<YY_PZ_TAB> GetList(string pz_lx)
        {
            return dal.GetList(t => t.PZ_LX == pz_lx);
        }
    }
}
