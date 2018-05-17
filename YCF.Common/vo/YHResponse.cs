using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCF.Common.vo
{
   public class YHResponse<T>
    {
        public bool success { get; set; }

        public String msg { get; set; }

        public String xmlData { get; set; }

        public T data { get; set; }

        public IList<T> dataList { set; get;}
    }
}
