using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class globalyijiandangan
    {
        //private static string yijian_wm_mrn="";//孕妇档案号
        //private static string yijian_wm_identityno="";//孕妇身份证号
        //private static string yijian_wm_mobile_phone="";//孕妇手机号
        private static int yijian_id=-1;//孕妇ID
        private static string wm_name = "";

        public static int Yijian_id
        {
            get
            {
                return yijian_id;
            }
            set
            {
                yijian_id = value;
            }
        }
        public static string Wm_name
        {
            get
            {
                return wm_name;
            }
            set
            {
                wm_name = value;
            }
        }

    }
}
