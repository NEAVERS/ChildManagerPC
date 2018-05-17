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
        public string CARD_NO { get; set; }//就诊卡号
        public string CARD_TYPE { get; set; }//卡类型
        public string CARD_CHIP_NO { get; set; }//芯片号
        public string PAT_INDEX_NO { get; set; }//病人ID

    }
}
