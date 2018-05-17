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
    public partial class tab_dept_databll : BLLBase<TAB_DEPT_DATA, tab_dept_datadal>
    {
        public tab_dept_databll()
        {
            
        }
        public IList<TAB_DEPT_DATA> GetList(string name, string code, string doctor)
        {
            return dal.GetList(t => t.DEPT_NAME == name && t.DEPT_CODE == code && t.DOCTOR_NAME == doctor);
        }
    }
}
