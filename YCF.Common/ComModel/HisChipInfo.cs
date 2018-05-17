using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCF.Model.NotMaps
{
    /// <summary>
    /// 就诊卡芯片号查询信息对象
    /// </summary>
   public class HisChipInfo
    {

        /// <summary>
        ///就诊卡号
        /// </summary>
        public string CARD_NO { get; set; }
        /// <summary>
        ///卡类型
        /// </summary>
        public string CARD_TYPE { get; set; }
        /// <summary>
        ///芯片号
        /// </summary>
        public string CARD_CHIP_NO { get; set; }
        /// <summary>
        ///病人ID
        /// </summary>
        public string PAT_INDEX_NO { get; set; }

    }
}
