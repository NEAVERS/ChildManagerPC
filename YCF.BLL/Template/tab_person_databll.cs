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
    public partial class tab_person_databll : BLLBase<TAB_PERSON_DATA, tab_person_datadal>
    {
        public tab_person_databll()
        {
            orders = new List<Expression<Func<TAB_PERSON_DATA, object>>>() { t => t.ID };
            isAscs = new List<bool>() { true };
        }
        public IList<TAB_PERSON_DATA> GetList(string type)
        {
            return dal.GetList(t => t.TYPE == type );
        }
        public IList<TAB_PERSON_DATA> GetList(string name,string type)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(type))
            {
                return dal.GetList(t => t.NAME.Contains(name) && t.TYPE == type);
            }
            else if (string.IsNullOrEmpty(name))
            {
                return dal.GetList(t => t.TYPE == type);
            }
            else
            {
                return dal.GetList(t => t.NAME.Contains(name));
            }


        }

    }
}
