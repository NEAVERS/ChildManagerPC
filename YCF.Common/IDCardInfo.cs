using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Common
{
    public class IDCardInfo
    {
        public string Code { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string Folk { set; get; }
        public string BirthDate { set; get; }
        public string Address { set; get; }
        public string Agency { set; get; }
        public string ExpireStart { set; get; }
        public string ExpireEnd { set; get; }
        public Bitmap Photo { get; set; }
    }
}
