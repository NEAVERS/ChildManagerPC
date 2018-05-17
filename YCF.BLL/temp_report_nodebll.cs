using YCF.Model;
using YCF.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using YCF.BLL.Base;

namespace YCF.BLL
{
    public partial class temp_report_nodebll : BLLBase<TEMP_REPORT_NODE, temp_report_nodedal>
    {
        private readonly List<Expression<Func<TEMP_REPORT_NODE, object>>> orders;
        private readonly List<bool> isAscs;
        public temp_report_nodebll()
        {
            orders = new List<Expression<Func<TEMP_REPORT_NODE, object>>>() { t => t.NODE_NUMBER };
            isAscs = new List<bool>() { true };
        }
        public IList<TEMP_REPORT_NODE> GetList(int report_id)
        {
            return dal.GetList(t => t.REPORT_ID == report_id ,orders, isAscs);
        }
    }
}
