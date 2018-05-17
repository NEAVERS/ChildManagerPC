using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace YCF.Common
{
    public class OperatFile
    {
         [DllImport("kernel32")]//引入“shell32.dll”API文件
       
         public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]//引入“shell32.dll”API文件
        public static extern int WritePrivateProfileString(string section, string key, string value, string filePath);
        public OperatFile()
         {

         }
         public static string GetIniFileString(string section, string key, string def, string filePath)
         {
             StringBuilder temp = new StringBuilder(1024);
             GetPrivateProfileString(section, key, def, temp, 1024, filePath);
             return temp.ToString();
         }
        public static string SetIniFileString(string section, string key, string value, string filePath)
        {
            StringBuilder temp = new StringBuilder(1024);
            WritePrivateProfileString(section, key, value, filePath);
            return temp.ToString();
        }
    }
}
