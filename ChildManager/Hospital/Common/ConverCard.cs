using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace login.Hospital.Common
{
    class ConverCard
    {
        [DllImport("CqHosInt.dll", EntryPoint = "CPC_QryCardInfoFHos")]//引入“shell32.dll”API文件

        public static extern int CPC_QryCardInfoFHos(StringBuilder a, StringBuilder b, StringBuilder c);  //声明外部方法
        public string getCardno()
        {
            string returnstr="";
            StringBuilder temp1 = new StringBuilder(1024);
            StringBuilder temp2 = new StringBuilder(1024);
            StringBuilder temp3 = new StringBuilder(1024);
            //假如解析成功
            int a = CPC_QryCardInfoFHos(temp1, temp2, temp3);
            if (CPC_QryCardInfoFHos(temp1, temp2, temp3) == 0)
            {
               returnstr=temp2.ToString().Trim();
            }
          
            return returnstr;
        }
    }
}
