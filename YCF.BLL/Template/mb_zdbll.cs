using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using YCF.DAL;
using YCF.Model;
using YCF.DAL.Base;
using YCF.DAL.Template;

namespace YCF.BLL.Template
{
    public partial class mb_zdbll : BLLBase<MB_ZD, mb_zddal>
    {

        private readonly List<Expression<Func<MB_ZD, int>>> order;
        private readonly List<bool> isAscs;
        public mb_zdbll()
        {
            order = new List<Expression<Func<MB_ZD, int>>>() { t => t.YFKS };
            isAscs = new List<bool>() { false };
        }

        public MB_ZD GetByYf(int yf)
        {
            return dal.Get(t => t.YFKS <= yf && t.YFJS > yf);
        }


        public IList<MB_ZD> GetList()
        {
            //Expression<Func<MB_ZD, bool>> where = t => true;
            //if (!String.IsNullOrEmpty(starttime))
            //    where = where.AndAlso(t => t.childbuildday.CompareTo(starttime) >= 0&& t.childbuildday.CompareTo(endtime) <= 0);
            //if (!String.IsNullOrEmpty(birthstarttime))
            //    where = where.AndAlso(t => t.childbirthday.CompareTo(birthstarttime) >= 0 && t.childbirthday.CompareTo(birthendtime) <= 0);
            //if (!String.IsNullOrEmpty(healthcardno))
            //    where = where.AndAlso(t => t.healthcardno == healthcardno);
            //if (!String.IsNullOrEmpty(childname))
            //    where = where.AndAlso(t => t.childname == childname);
            //if (!String.IsNullOrEmpty(childgender))
            //    where = where.AndAlso(t => t.childgender == childgender);
            //if (!String.IsNullOrEmpty(telephone))
            //    where = where.AndAlso(t => t.telephone == telephone);
            //if (!String.IsNullOrEmpty(motherName))
            //    where = where.AndAlso(t => t.mothername == motherName);
            //if (!String.IsNullOrEmpty(fatherName))
            //    where = where.AndAlso(t => t.fathername == fatherName);

            return dal.GetList(t => 1 == 1, order, isAscs);
        }

    }
}
