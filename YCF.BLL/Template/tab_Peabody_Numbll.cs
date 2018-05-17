using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using YCF.DAL;
using YCF.Model;
using YCF.DAL.Base;
using YCF.DAL.Template;
using System.Data.SqlClient;
using YCF.BLL.Base;

namespace YCF.BLL.Template
{
    public partial class tab_Peabody_Numbll : BLLBase<TAB_PEABODY_NUM, tab_Peabody_Numdal>
    {

        //public tab_yysc_Num getfanshe(string type)
        //{
        //    var list = dal.GetList(t => t.type == type, orders, isAscs);
        //    int count = list.Count();
        //    return list[count - 1];
        //}
        public IList<TAB_PEABODY_NUM> GetList( string yueling)
        {
          
            return dal.GetList(t =>  t.YUELING == yueling);
        }
 

    }
}
