using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.DAL;
using YCF.Model;

namespace YCF.BLL
{
    public partial class tb_childcheckbll : BLLBase<TB_CHILDCHECK, tb_childcheckdal>
    {
        private readonly List<Expression<Func<TB_CHILDCHECK, object>>> order;
        private readonly List<bool> isAscs;
        public tb_childcheckbll()
        {
            order = new List<Expression<Func<TB_CHILDCHECK, object>>>() { t => t.CHECKDAY };
            isAscs = new List<bool>() { false };
        }
        public IList<TB_CHILDCHECK> GetList(int cd_id)
        {
            return dal.GetList(t => t.CHILDID == cd_id, order, isAscs);
        }



        public TB_CHILDCHECK Get(string checkday, int cd_id)
        {
            return dal.Get(t => t.CHILDID == cd_id && t.CHECKDAY == checkday);
        }

        public bool SaveOrUpdate(TB_CHILDCHECK checkobj)
        {
            if (checkobj.ID != 0)
                return dal.Update(checkobj) > 0;
            else
                return dal.Add(checkobj) > 0;
        }

        public bool UpdateNurse(TB_CHILDCHECK checkobj)
        {
            return dal.Update(checkobj, "checkday", "checkweight", "checkheight", "ck_fz", "checktouwei", "checkzuogao") > 0;
        }


        public int countpre(string fuzhenday, string usercode)
        {
            return dal.Count(t => t.FUZENDAY == fuzhenday && t.USER_CODE == usercode);
        }
    }
}
