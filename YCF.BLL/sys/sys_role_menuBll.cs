using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YCF.Model;
using YCF.DAL.sys;
using System.Linq.Expressions;
using YCF.DAL.Base;

namespace YCF.BLL.sys
{
    public partial class sys_role_menuBll:BLLBase<SYS_ROLE_MENU, sys_role_menuDAL>
    {
        private readonly List<Expression<Func<SYS_ROLE_MENU, object>>> order;
        private readonly List<bool> isAscs;

        public sys_role_menuBll()
        {
            order = new List<Expression<Func<SYS_ROLE_MENU, object>>>() { t =>t.MENU_CODE };
            isAscs = new List<bool>() { true };
        }

        /// <summary>
        /// 根据菜单 编号 查询所拥有的 权限菜单
        /// </summary>
        /// <param name="menu_code"></param>
        /// <returns></returns>
        public IList<SYS_ROLE_MENU> GetList(string menu_code)
        {
          
            var list = dal.GetList(t =>t.MENU_CODE == menu_code, order, isAscs);
            return list;
        }
        //dal.Get

        /// <summary>
        /// 根据角色 编码 查找
        /// </summary>
        /// <param name="role_code"></param>
        /// <returns></returns>
        public IList<SYS_ROLE_MENU> GetCodeList(string role_code)
        {
            var list = dal.GetList(t => t.ROLE_CODE == role_code, order, isAscs);
            return list;
        }

        public IList<SYS_ROLE_MENU> GetList(string menu_code, string role_code)
        {
            var list = dal.GetList(t =>t.MENU_CODE == menu_code && t.ROLE_CODE == role_code, order, isAscs);
            return list;
        }

        public int DeleteRoleCode(string sqls,string role_code)
        {
           return  DALStatic.ExecuteSqlCommand(sqls, role_code);
        }
    }
}
