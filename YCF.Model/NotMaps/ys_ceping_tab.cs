using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCF.Model
{
    public class ys_ceping_tab
    {
        public string cepingName { get; set; }

        public int cepingCount { get; set; }

        public string URL { get; set; }

        public string cepingDisplay
        {
            get
            {
                return string.Format("{0}({1})", cepingName, cepingCount);
            }
        }
    }
}
