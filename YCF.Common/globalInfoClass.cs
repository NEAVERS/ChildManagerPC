namespace YCF.Common
{
    public class globalInfoClass
    {
        private static string userid=""; //用户ID
        private static string usercode = " "; //定义用户code编码
        private static string username = "";
        private static string password = ""; //定义用户密码
        private static string usertype = "";//定义用户类别
        private static string user_role = "";
        private static int pre_max = 0;
        private static int wm_index = -1;
        private static string ward_name;
        private static string zhicheng;
        private static string hospital;
        private static string zhiwu;

        public static string Ward_name
        {
            get
            {
                return ward_name;
            }
            set
            {
                ward_name = value;
            }
        }
        public static int Wm_Index
        {
            get
            {
                return wm_index;
            }
            set
            {
                wm_index = value;
            }
        }
        public static int Pre_Max
        {
            get
            {
                return pre_max;
            }
            set
            {
                pre_max = value;
            }
        }
        public static string UserID 
        { 
            get 
            {
                return userid; 
            } 
            set 
            {
                userid = value; 
            } 
        } 
        public static string UserCode
        {
            get
            {
                return usercode;
            }
            set
            {
                usercode = value;
            } 
        }
        public static string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public static string PassWord
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public static string UserType
        {
            get
            {
                return usertype;
            }
            set
            {
                usertype = value;
            }
        }
        public static string User_Role
        {
            get
            {
                return user_role;
            }
            set
            {
                user_role = value;
            }
        }
        public static string Zhicheng
        {
            get
            {
                return zhicheng;
            }
            set
            {
                zhicheng = value;
            }
        }
        public static string Zhiwu
        {
            get
            {
                return zhiwu;
            }
            set
            {
                zhiwu = value;
            }
        }
        public static string Hospital
        {
            get
            {
                return hospital;
            }
            set
            {
                hospital = value;
            }
        }

    }
           
}
