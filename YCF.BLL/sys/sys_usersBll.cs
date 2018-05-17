using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YCF.Model;
using YCF.DAL.sys;
using System.Linq.Expressions;
using YCF.Extension;

namespace YCF.BLL.sys
{
   public partial class sys_usersBll : BLLBase<SYS_USERS, sys_usersDAL>
    {
        private readonly List<Expression<Func<SYS_USERS, object>>> order;
        private readonly List<bool> isAscs;

        public sys_usersBll()
        {
            order = new List<Expression<Func<SYS_USERS, object>>>() { t =>t.ROLE_CODE };
            isAscs = new List<bool>() { true };
        }

        public IList<SYS_USERS> GetList(string username,string usercode,string role_code)
        {
            Expression<Func<SYS_USERS, bool>> where = t => true;
            if (!String.IsNullOrEmpty(username))
                where = where.AndAlso(t => t.USER_NAME == username);
            if (!String.IsNullOrEmpty(usercode))
                where = where.AndAlso(t => t.USER_CODE == usercode);
            if (!String.IsNullOrEmpty(role_code))
                where = where.AndAlso(t =>t.ROLE_CODE == role_code);

            var list = dal.GetList(where);
            return list;
        }

        public SYS_USERS Get(string user_code)
        {
            return dal.Get(t => t.USER_CODE == user_code);
        }

        public bool SaveOrUpdate(SYS_USERS userobj)
        {
            if (userobj.ID != 0)
                return dal.Update(userobj) > 0;
            else
                return dal.Add(userobj) > 0;
        }

        public bool UpdateByIds(SYS_USERS userobj,List<int> ids)
        {
            return dal.Update(userobj, t => ids.Contains(t.ID),"role_code") >0;
        }

        public bool UpdatePwd(SYS_USERS userobj)
        {
            return dal.Update(userobj, t=>t.USER_CODE == userobj.USER_CODE, "password")>0;
        }

    }
}
