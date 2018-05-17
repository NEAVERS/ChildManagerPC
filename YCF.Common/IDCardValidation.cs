using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCF.Common
{
    public class IDCardValidation
    {
         
        public bool CheckIDCard(string idNumber)  
        {  
            if (idNumber.Length == 18)  
            {  
                bool check = CheckIDCard18(idNumber);  
 
                return check;  
            }  
            else if (idNumber.Length == 15)  
            {  
                bool check = CheckIDCard15(idNumber);  
                return check;  
            }  
 
            else  
            {  
                return false;  
 
            }  
        }
        public bool Checkmobilephone(string phone)
        {
            if (phone.Length == 11)
            {
                return true;
            }
            return false;
        }
   
   
        /// <summary>  
        /// 18位身份证号码验证  
        /// </summary>  
        private bool CheckIDCard18(string idNumber)  
        {  
            long  n = 0;  
            if (long.TryParse(idNumber.Remove(17), out n) == false  || long.TryParse(idNumber.Replace('x', '0').Replace('X', '0'), out n) == false)  
            {  
                return false;//数字验证  
            }  
            return true;//符合GB11643-1999标准  
        }  
   
   
        /// <summary>  
        /// 16位身份证号码验证  
 
        /// </summary>  
        public bool CheckIDCard15(string idNumber)  
        {  
            long n = 0;  
            if (long.TryParse(idNumber, out n) == false)  
 
            {  
                return false;//数字验证  
            }  
            return true;  
        }   

    }
}
