using System.Collections.Generic;
using YCF.DAL;
using YCF.Model;

namespace YCF.BLL
{
    public partial class tj_yingtijianBll : BLLBase<yingtijianObj, tj_yingtijianDal>
    {

        public IList<yingtijianObj> GetList(string sqls)
        {
            return dal.getList(sqls);
        }
    }
}
