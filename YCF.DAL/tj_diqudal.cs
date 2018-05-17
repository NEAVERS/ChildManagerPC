using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YCF.Common;
using YCF.Model;

namespace YCF.DAL
{
    public partial class tj_diqudal : DALBase<countObj>
    {
        public IList<countObj> getList(string sqls)
        {
            using (db = new OracleEntities(CommonHelper.EFConnentionString))
            {
                return db.Database.SqlQuery<countObj>(sqls).ToList();
            }
        }
    }
}
