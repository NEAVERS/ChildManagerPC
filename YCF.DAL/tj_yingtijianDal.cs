using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YCF.Common;
using YCF.Model;

namespace YCF.DAL
{
    public partial class tj_yingtijianDal : DALBase<yingtijianObj>
    {
        public IList<yingtijianObj> getList(string sqls)
        {
            using (db = new OracleEntities(CommonHelper.EFConnentionString))
            {
                return db.Database.SqlQuery<yingtijianObj>(sqls).ToList();
            }
        }
    }
}
