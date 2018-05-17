using System.Collections.Generic;
using YCF.DAL.xunlianshi;
using YCF.DAL.yuyue;
using YCF.Model;

namespace YCF.BLL.yuyue
{
    public partial class yy_pz_details_tabbll : BLLBase<YY_PZ_DETAILS_TAB, yy_pz_details_tabdal>
    {
        public new IList<YY_PZ_DETAILS_TAB> Get(int pzid)
        {
            return dal.GetList(t => t.PZID == pzid);
        }
    }
}
