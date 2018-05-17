using System.Collections.Generic;
using YCF.DAL;
using YCF.Model;

namespace YCF.BLL
{
    public partial class tj_diqubll : BLLBase<countObj, tj_diqudal>
    {

        public IList<countObj> GetList(string sqls)
        {
            return dal.getList(sqls);
        }
    }
}
