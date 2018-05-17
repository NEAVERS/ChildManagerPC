using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildManager.Model.childSetInfo
{
  public  class childSetNutritionguidObj
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

        private string setAge;
        /// <summary>
        /// 检查年龄月份
        /// </summary>
        public string SetAge
        {
            get { return setAge; }
            set { setAge = value; }
        }


        private string setContent;
        /// <summary>
        /// 检查  or 不检查
        /// </summary>
        public string SetContent
        {
            get { return setContent; }
            set { setContent = value; }
        }

        private int type;
        /// <summary>
        /// 是否检查标示  1：检查   2：不检查
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        private string shiyongxing;
      /// <summary>
      /// 适用性
      /// </summary>
        public string Shiyongxing
        {
            get { return shiyongxing; }
            set { shiyongxing = value; }
        }

        private string bigdongzuo;
      /// <summary>
      /// 大动作
      /// </summary>
        public string Bigdongzuo
        {
            get { return bigdongzuo; }
            set { bigdongzuo = value; }
        }
    }
}
