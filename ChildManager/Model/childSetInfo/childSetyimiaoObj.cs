using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildManager.Model.childSetInfo
{
    public class childSetyimiaoObj
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

        private string yimiaoName;
        /// <summary>
        /// 疫苗名称
        /// </summary>
        public string YimiaoName
        {
            get { return yimiaoName; }
            set { yimiaoName = value; }
        }


        private string factorName;
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string FactorName
        {
            get { return factorName; }
            set { factorName = value; }
        }

        private string productionDay;
        /// <summary>
        /// 生产时间
        /// </summary>
        public string ProductionDay
        {
            get { return productionDay; }
            set { productionDay = value; }
        }

        private string pihao;
        /// <summary>
        /// 批号
        /// </summary>
        public string Pihao
        {
            get { return pihao; }
            set { pihao = value; }
        }

        private string guige;
        /// <summary>
        /// 规格
        /// </summary>
        public string Guige
        {
            get { return guige; }
            set { guige = value; }
        }

        private string jiliangUnit;
        /// <summary>
        /// 剂量单位
        /// </summary>
        public string JiliangUnit
        {
            get { return jiliangUnit; }
            set { jiliangUnit = value; }
        }

        private string month;
        /// <summary>
        /// 接种月份
        /// </summary>
        public string Month
        {
            get { return month; }
            set { month = value; }
        }
         }
}
