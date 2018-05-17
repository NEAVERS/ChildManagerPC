using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildManager.Model.childSetInfo
{
   public class childSetbaoJiangGuidObj
    {
        private int id;
       /// <summary>
       /// id
       /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string childId;
       /// <summary>
       /// 儿童 ID
       /// </summary>
        public string ChildId
        {
            get { return childId; }
            set { childId = value; }
        }

        private string setage;
       /// <summary>
       /// 年龄段
       /// </summary>
        public string Setage
        {
            get { return setage; }
            set { setage = value; }
        }

        private string eduction;
       /// <summary>
       /// 早期教育
       /// </summary>
        public string Eduction
        {
            get { return eduction; }
            set { eduction = value; }
        }

        private string shanshi;
       /// <summary>
       /// 营养 与 膳食
       /// </summary>
        public string Shanshi
        {
            get { return shanshi; }
            set { shanshi = value; }
        }

        private string jibing;
       /// <summary>
       /// 疾病
       /// </summary>
        public string Jibing
        {
            get { return jibing; }
            set { jibing = value; }
        }

        private string huli;
       /// <summary>
       /// 护理
       /// </summary>
        public string Huli
        {
            get { return huli; }
            set { huli = value; }
        }
    }
}
