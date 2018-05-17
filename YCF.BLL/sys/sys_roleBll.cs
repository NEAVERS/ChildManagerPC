using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YCF.Model;
using YCF.DAL.sys;
using System.Linq.Expressions;

namespace YCF.BLL.sys
{
   public partial class sys_roleBll:BLLBase<SYS_ROLE,sys_roleDAL>
    {
        private readonly List<Expression<Func<SYS_ROLE, object>>> order;
        private readonly List<bool> isAscs;

        public sys_roleBll()
        {
            order = new List<Expression<Func<SYS_ROLE, object>>>() { t => t.ROLE_CODE };
            isAscs = new List<bool>() { true };
        }

        public IList<SYS_ROLE> GetList(string role_code)
        {
            var list = dal.GetList(t => t.ROLE_CODE == role_code, order, isAscs);
            return list;
        }

        public SYS_ROLE GetByCode(string role_code)
        {
            return dal.Get(t => t.ROLE_CODE == role_code);
        }
    }
}
