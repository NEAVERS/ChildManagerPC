using YCF.DAL.xunlianshi;
using YCF.Model;

namespace YCF.BLL.xunlianshi
{
    public partial class xl_yy_coubll : BLLBase<XL_YY_COU, xl_yy_coudal>
    {

        public bool SaveOrUpdate(XL_YY_COU obj)
        {
            if (obj.ID != 0)
                return dal.Update(obj) > 0;
            else
                return dal.Add(obj) > 0;
        }
    }
}
