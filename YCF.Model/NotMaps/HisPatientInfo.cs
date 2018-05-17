using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCF.Model.NotMaps
{
    /// <summary>
    /// HIS查询信息对象
    /// </summary>
   public class HisPatientInfo
    {
        /// <summary>
        /// 病人ID
        /// </summary>
        public string PAT_INDEX_NO { get; set; }

        /// <summary>
        ///病人姓名 
        /// </summary>
        public string PAT_NAME { get; set; }
        /// <summary>
        ///病人性别
        /// </summary>
        public string PHYSI_SEX_NAME { get; set; }
        /// <summary>
        ///出生日期
        /// </summary>
        public string DATE_BIRTH { get; set; }
        /// <summary>
        ///挂号信息
        /// </summary>
        public string SIGNAL_SOURCE_CODE { get; set; }
        /// <summary>
        ///就诊卡号 
        /// </summary>
        public string VISIT_CARD_NO { get; set; }
        public string OUTHOSP_INDEX_NO { get; set; }
        public string OUTHOSP_NO { get; set; }
        /// <summary>
        ///挂号科室
        /// </summary>
        public string REGIST_DEPT_NAME { get; set; }
        public string REGIST_DR_CODE { get; set; }
        public string REGIST_DEPT_CODE { get; set; }
        public string REGIST_NO { get; set; }
        /// <summary>
        ///地址 
        /// </summary>
        public string CURR_ADDR { get; set; }
        /// <summary>
        ///电话
        /// </summary>
        public string PHONE_NO { get; set; }
        /// <summary>
        ///民族
        /// </summary>
        public string ETHNIC_NAME { get; set; }
    }
}
