using System.Collections.Generic;
using YCF.DAL;
using YCF.Model;

namespace YCF.BLL
{
    public partial class tb_healthcheck_mobanbll : BLLBase<TB_HEALTHCHECK_MOBAN, tb_healthcheck_mobandal>
    {
        public bool ExsitsSaveOrUpdateById(TB_HEALTHCHECK_MOBAN obj)
        {
            if (dal.Count(t => t.ID == obj.ID) > 0)
            {
                return dal.Update(obj)>0;
            }
            else
            {
                return dal.Add(obj) > 0;
            }
        }

        public IList<TB_HEALTHCHECK_MOBAN> GetList(string moban_name,string moban_type,string creater_name)
        {
            if(!string.IsNullOrEmpty(moban_name)&& !string.IsNullOrEmpty(moban_type))
            {
                if(moban_type=="个人")
                {
                    return dal.GetList(t => t.MOBAN_NAME == moban_name && t.MOBAN_TYPE == moban_type && t.CREATER_NAME == creater_name);
                }
                else
                {
                    return dal.GetList(t => t.MOBAN_NAME == moban_name && t.MOBAN_TYPE == moban_type);
                }
            }
            else if(!string.IsNullOrEmpty(moban_name))
            { 
                return dal.GetList(t => t.MOBAN_NAME == moban_name);
            }
            else if (!string.IsNullOrEmpty(moban_type))
            {
                if (moban_type == "个人")
                {
                    return dal.GetList(t =>t.MOBAN_TYPE == moban_type && t.CREATER_NAME == creater_name);
                }
                else
                {
                    return dal.GetList(t => t.MOBAN_TYPE == moban_type);
                }
            }
            else
            { 
                return dal.GetList(t => t.CREATER_NAME == creater_name);
            }
        }
    }
}
