using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace login.Model
{
    public class UpdateVersionobj
    {
        public int id;
        /// <summary>
        /// 版本号
        /// </summary>
        public string version;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string updatetime;

        /// <summary>
        /// 更新内容
        /// </summary>
        public string updatecontent;

        /// <summary>
        /// 是否发布
        /// </summary>
        public int isfabu;


     }
}
