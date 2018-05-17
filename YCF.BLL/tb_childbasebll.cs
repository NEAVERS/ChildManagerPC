using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using YCF.DAL;
using YCF.Model;
using YCF.DAL.Base;
using YCF.Common;
using YCF.Extension;

namespace YCF.BLL
{
    public partial class tb_childbasebll : BLLBase<TB_CHILDBASE, tb_childbasedal>
    {

        private readonly List<Expression<Func<TB_CHILDBASE, object>>> order;
        private readonly List<bool> isAscs;
        public tb_childbasebll()
        {
            order = new List<Expression<Func<TB_CHILDBASE, object>>>() { t => t.CHILDBUILDDAY };
            isAscs = new List<bool>() { false };
        }
        public int stateval()
        {
            int healthcardno = 0;
            int.TryParse(Max(t => t.HEALTHCARDNO), out healthcardno);
            return (healthcardno + 1);
        }

        public TB_CHILDBASE GetByCardNo(string cardno)
        {
            return dal.Get(t => t.JIUZHENCARDNO == cardno);
        }

        public TB_CHILDBASE GetByPatientId(string patient_id)
        {
            return dal.Get(t => t.PATIENT_ID == patient_id);
        }
        public TB_CHILDBASE GetByVisitNo(string visit_card_no)
        {
            return dal.Get(t => t.JIUZHENCARDNO == visit_card_no);
        }
        public TB_CHILDBASE GetByHealthNo(string healthno)
        {
            return dal.Get(t => t.HEALTHCARDNO == healthno);
        }

        public IList<TB_CHILDBASE> GetList(string starttime, string endtime, string birthstarttime, string birthendtime, string healthcardno, string childname, string childgender, string telephone, string motherName, string fatherName, string patientId)
        {
            Expression<Func<TB_CHILDBASE, bool>> where = t => true;
            if (!String.IsNullOrEmpty(starttime))
                where = where.AndAlso(t => t.CHILDBUILDDAY.CompareTo(starttime) >= 0 && t.CHILDBUILDDAY.CompareTo(endtime) <= 0);
            if (!String.IsNullOrEmpty(birthstarttime))
                where = where.AndAlso(t =>  t.CHILDBIRTHDAY.CompareTo(birthstarttime) >= 0 &&  t.CHILDBIRTHDAY.CompareTo(birthendtime) <= 0);
            if (!String.IsNullOrEmpty(healthcardno))
                where = where.AndAlso(t => t.HEALTHCARDNO == healthcardno);
            if (!String.IsNullOrEmpty(childname))
                where = where.AndAlso(t => t.CHILDNAME.Contains(childname));
            if (!String.IsNullOrEmpty(childgender))
                where = where.AndAlso(t => t.CHILDGENDER == childgender);
            if (!String.IsNullOrEmpty(telephone))
                where = where.AndAlso(t => t.TELEPHONE == telephone);
            if (!String.IsNullOrEmpty(motherName))
                where = where.AndAlso(t => t.MOTHERNAME == motherName);
            if (!String.IsNullOrEmpty(fatherName))
                where = where.AndAlso(t => t.FATHERNAME == fatherName);
            if (!String.IsNullOrEmpty(patientId))
                where = where.AndAlso(t => t.PATIENT_ID == patientId);

            return dal.GetList(where, order, isAscs);
        }

        public IList<TB_CHILDBASE> GetListBySqlCheckTime(string checkTime)
        {
            string sqls = "select distinct a.* from TB_CHILDBASE a where EXISTS (select 1 from tb_childcheck where a.id = childid AND checkday='" + checkTime + "')";
            return DALStatic.GetListBySql<TB_CHILDBASE>(sqls, "");
        }

        public IList<TB_CHILDBASE> GetListByCheckDoc(string checkday, string isjiuzhen, int cd_id)
        {
            string sqls = "select distinct b.* from tb_childcheck a left join TB_CHILDBASE b on a.childid=b.id where ((a.checkday='" + checkday + "'";
            if (isjiuzhen == "未就诊")
            {
                sqls += " and (a.zhenduan is null or a.zhenduan ='')";
            }
            else
            {
                sqls += " and a.zhenduan is not null and a.zhenduan !=''";
            }
            sqls += " ) or b.id=" + cd_id + ") and (a.ck_fz='" + globalInfoClass.UserName + "' or a.ck_fz='" + globalInfoClass.Zhicheng + "' or a.doctorname='" + globalInfoClass.UserName + "') order by b.childname";
            return DALStatic.GetListBySql<TB_CHILDBASE>(sqls, "");
        }

        public IList<TB_CHILDBASE> GetListByCheckDoc(string checkday, string isjiuzhen, int cd_id, IList<SYS_MENUS> list)
        {
            string sqls = "";
            if (globalInfoClass.Ward_name.Trim().Equals("儿保室"))
            {
                sqls = "select distinct b.* from tb_childcheck a left join TB_CHILDBASE b on a.childid=b.id where ((a.checkday='" + checkday + "'";
                if (isjiuzhen == "未就诊")
                {
                    sqls += " and (a.zhenduan is null or a.zhenduan ='')";
                }
                else
                {
                    sqls += " and a.zhenduan is not null and a.zhenduan !=''";
                }
                sqls += " ) or b.id=" + cd_id + ") and (a.ck_fz='" + globalInfoClass.UserName + "' or a.ck_fz='" + globalInfoClass.Zhicheng + "' or a.ck_fz='' or a.doctorname='" + globalInfoClass.UserName + "') order by b.childname";
            }
            else if (globalInfoClass.Ward_name.Trim().Equals("测评室") || globalInfoClass.Ward_name.Trim().Equals("护士站"))
            {
                sqls = "";
                if (isjiuzhen == "未就诊")
                {

                }
                else
                {
                    sqls = "select distinct c.* from ( select a.* from dbo.TB_CHILDBASE a where 1 = 2 ";
                    foreach (SYS_MENUS obj in list)
                    {
                        /*
                         * ADHD报告
                         * ADOS1报告
                         * ADOS2报告
                         * ADOS3报告
                         * ADOS4报告
                         * ASD报告(4岁以下）
                         * ASD报告(4岁及以上）
                         * Gesell评价表
                         * SM评价表
                         * 大韦氏评价表
                         * 小韦氏评价表
                         * 语言筛查<3岁3月
                         * 语言筛查>3岁3月
                         * 能力测试<6岁
                         * 注意力测验
                         * DDST
                         * 图片词汇测试
                         * DST测试
                         */
                        if (obj.MENU_URL==null || obj.MENU_NAME.Trim().Equals("基本信息") || obj.MENU_URL.Trim().Equals(""))
                        {
                            continue;
                        }
                        if (obj.MENU_NAME.Trim().Equals("ADHD报告"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_adhd_tab b on a.id = b.child_id where b.csrq = "+"'"+checkday+"' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("ADOS1报告"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_ados1_tab b on a.id = b.child_id where b.pgrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("ADOS2报告"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_ados2_tab b on a.id = b.child_id where b.pgrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("ADOS3报告"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_ados3_tab b on a.id = b.child_id where b.pgrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("ADOS4报告"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_ados4_tab b on a.id = b.child_id where b.pgrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("ASD报告(4岁以下）"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_asd1_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("ASD报告(4岁及以上）"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_asd2_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("Gesell评价表"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_gesell_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("SM评价表"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_sm_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("大韦氏评价表"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_wisc_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("小韦氏评价表"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_wycsi_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("语言筛查<3岁3月"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_zqyyfyjc_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_cdi_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("语言筛查>3岁3月"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_cdi1_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("能力测试<6岁"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_ppvt_tab b on a.id = b.child_id where b.cyrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("注意力测验"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_zylcy_tab b on a.id = b.child_id where b.cyrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("DDST"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_ddst_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("图片词汇测试"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_tpch_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                        else if (obj.MENU_NAME.Trim().Equals("DST测试"))
                        {
                            sqls += "union all select a.* from dbo.TB_CHILDBASE a inner join dbo.cp_dst_tab b on a.id = b.child_id where b.csrq = " + "'" + checkday + "' ";
                        }
                    }

                    sqls += ") as c";
                }
            }
            else if (globalInfoClass.Ward_name.Trim().Equals("训练室"))
            {
                sqls = "";
                if (isjiuzhen == "未就诊")
                {

                }
                else
                {

                }
            }
            if (sqls.Trim().Equals(""))
                return null;
            return DALStatic.GetListBySql<TB_CHILDBASE>(sqls, "");
        }

        public IList<TB_CHILDBASE> GetListByCheckHs(string checkday, string isjiuzhen, string hospital, int cd_id)
        {
            string sqls = "select distinct b.* from tb_childcheck a left join TB_CHILDBASE b on a.childid=b.id where ((a.checkday='" + checkday + "'"  + " and  a.hospital='" + hospital + "'";
            if (isjiuzhen == "未就诊")
            {
                sqls += " and (a.zhenduan is null or a.zhenduan ='')";
            }
            else
            {
                sqls += " and a.zhenduan is not null and a.zhenduan !=''";
            }
            sqls += " ) or b.id=" + cd_id + ")  order by b.childname";
            return DALStatic.GetListBySql<TB_CHILDBASE>(sqls, "");
        }
    }
}
