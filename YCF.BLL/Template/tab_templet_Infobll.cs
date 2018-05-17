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
    public partial class tab_templet_Infobll : BLLBase<TAB_TEMPLET_INFO, tab_templet_Infodal>
    {
        public IList<TAB_TEMPLET_INFO> GetList(string type)
        {
            return dal.GetList(t => t.TYPE == type);
        }
        public IList<TAB_TEMPLET_INFO> GetList(string type, string child_type)
        {
            return dal.GetList(t => t.TYPE == type && t.CHILD_TYPE==child_type);
        }
    }
}
