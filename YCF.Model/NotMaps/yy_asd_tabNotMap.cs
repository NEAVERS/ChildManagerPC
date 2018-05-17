using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCF.Model
{
    public class yy_asd_tabNotMap
    {
        public int id { get; set; }
        public string yy_djrq { get; set; }
        public string yy_bh { get; set; }
        public string yy_xm { get; set; }
        public string yy_rq { get; set; }
        public string yy_sjd { get; set; }
        public string yy_sfjf { get; set; }
        public string yy_bz { get; set; }
        public string operate_name { get; set; }
        public string operate_code { get; set; }
        public string operate_time { get; set; }
        public string update_name { get; set; }
        public string update_code { get; set; }
        public string update_time { get; set; }
        public int child_id { get; set; }

        //儿童基本信息
        public string childname { get; set; }
        public string childgender { get; set; }
        public string childbirthday { get; set; }
        public string telephone { get; set; }
    }
}
