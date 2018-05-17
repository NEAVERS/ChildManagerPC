using YCF.Model;
using System;
using System.Collections.Generic;
using YCF.DAL.sys;
using System.Linq.Expressions;
using YCF.Common;
using YCF.DAL.Base;

namespace YCF.BLL.sys
{
    public partial class sysmenuBll : BLLBase<SYS_MENUS, sysmenuDAL>
    {
        private readonly List<Expression<Func<SYS_MENUS, object>>> order;
        private readonly List<bool> isAscs;

        public sysmenuBll()
        {
            order = new List<Expression<Func<SYS_MENUS, object>>>() { t => t.MENU_CODE };
            isAscs = new List<bool>() { true };
        }
        public IList<SYS_MENUS> GetList(string menu_type)
        {
                var list = dal.GetList(t => t.MENU_TYPE == menu_type && t.IS_ENABLE == "1", order, isAscs);
                return list;
        }
        public SYS_MENUS GetMenus(string menu_type)
        {
            return dal.Get(t=>t.MENU_NAME== menu_type && t.MENU_TYPE == "功能菜单");
        }
        public IList<SYS_MENUS> GetListBySql(string menu_type, string role_code)
        {
            if (role_code == "-1")
            {
                return GetList(menu_type);
            }
            else
            {
                string sqls = " select b.* from sys_role_menu a "
                                + " left join SYS_MENUS b on a.menu_code=b.menu_code "
                                + " where a.role_code='" + role_code + "' and b.menu_type='" + menu_type + "' and is_enable='1' "
                                + " order by b.menu_code ";
                var list = DALStatic.GetListBySql<SYS_MENUS>(sqls);
                return list;
            }
        }

    }
}
