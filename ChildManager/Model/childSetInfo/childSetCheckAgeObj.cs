using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildManager.Model.childSetInfo
{
   public class childSetCheckAgeObj
    {
        private int id;
       /// <summary>
       /// 主键 id
       /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private int childId;
       /// <summary>
       /// 儿童建档 id
       /// </summary>
        public int ChildId
        {
            get { return childId; }
            set { childId = value; }
        }

        private string checkMonth;
       /// <summary>
       /// 检查年龄月份
       /// </summary>
        public string CheckMonth
        {
            get { return checkMonth; }
            set { checkMonth = value; }
        }


        private string checkContent;
       /// <summary>
       /// 检查  or 不检查
       /// </summary>
        public string CheckContent
        {
            get { return checkContent; }
            set { checkContent = value; }
        }

        private int isCheck;
       /// <summary>
       /// 是否检查标示  1：检查   2：不检查
       /// </summary>
        public int IsCheck
        {
            get { return isCheck; }
            set { isCheck = value; }
        }
    }
}
