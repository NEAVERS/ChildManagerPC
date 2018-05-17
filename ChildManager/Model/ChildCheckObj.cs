using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChildManager.Model.ChildBaseInfo;

namespace ChildManager.Model
{
   public class ChildCheckObj
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

        private int childId;
       /// <summary>
       /// 儿童基本 id
       /// </summary>
        public int ChildId
        {
            get { return childId; }
            set { childId = value; }
        }

       /// <summary>
       /// 体检年龄
       /// </summary>
        private string checkAge;

        public string CheckAge
        {
            get { return checkAge; }
            set { checkAge = value; }
        }

        private string checkFactAge;
        /// <summary>
        /// 体检 实际年龄
        /// </summary>
        public string CheckFactAge
        {
            get { return checkFactAge; }
            set { checkFactAge = value; }
        }

        private string checkDay;
       /// <summary>
       /// 体检日期
       /// </summary>
        public string CheckDay
        {
            get { return checkDay; }
            set { checkDay = value; }
        }

        private string doctorName;
       /// <summary>
       /// 医师签名
       /// </summary>
        public string DoctorName
        {
            get { return doctorName; }
            set { doctorName = value; }
        }

        private string checkHeight;
       /// <summary>
       /// 体检 身高
       /// </summary>
        public string CheckHeight
        {
            get { return checkHeight; }
            set { checkHeight = value; }
        }

        private string checkWeight;
       /// <summary>
       /// 体检 体重
       /// </summary>
        public string CheckWeight
        {
            get { return checkWeight; }
            set { checkWeight = value; }
        }

        private string checkTouwei;
       /// <summary>
       /// 体检 头围
       /// </summary>
        public string CheckTouwei
        {
            get { return checkTouwei; }
            set { checkTouwei = value; }
        }


       // private string checkFuwei;
       ///// <summary>
       ///// 体检  腹围
       ///// </summary>
       // public string CheckFuwei
       // {
       //     get { return checkFuwei; }
       //     set { checkFuwei = value; }
       // }

        private string fuwei;
       /// <summary>
       /// 腹围
       /// </summary>
        public string Fuwei
        {
            get { return fuwei; }
            set { fuwei = value; }
        }

        private string checkZuogao;
        /// <summary>
        /// 坐高
        /// </summary>      
        public string CheckZuogao
        {
            get { return checkZuogao; }
            set { checkZuogao = value; }
        }

        private string checkTunwei;
       /// <summary>
       /// 臀围
       /// </summary>
        public string CheckTunwei
        {
            get { return checkTunwei; }
            set { checkTunwei = value; }
        }

        private string checkQianlu;
       /// <summary>
       /// 前卤
       /// </summary>
        public string CheckQianlu
        {
            get { return checkQianlu; }
            set { checkQianlu = value; }
        }

        private string checkIQ;
       /// <summary>
       /// 智商  IQ
       /// </summary>
        public string CheckIQ
        {
            get { return checkIQ; }
            set { checkIQ = value; }
        }

        private string checkZiping;
       /// <summary>
       /// 智商 评价
       /// </summary>
        public string CheckZiping
        {
            get { return checkZiping; }
            set { checkZiping = value; }
        }

        private string yuCeHeight;
        /// <summary>
        /// 预测 身高
        /// </summary>
        public string YuCeHeight
        {
            get { return yuCeHeight; }
            set { yuCeHeight = value; }
        }

        private string leftyan;
       /// <summary>
       /// 左眼
       /// </summary>
        public string Leftyan
        {
            get { return leftyan; }
            set { leftyan = value; }
        }

        private string rightyan;
       /// <summary>
       /// 右眼
       /// </summary>
        public string Rightyan
        {
            get { return rightyan; }
            set { rightyan = value; }
        }

        private string leftyanshili;
       /// <summary>
       /// 左眼 视力
       /// </summary>
        public string Leftyanshili
        {
            get { return leftyanshili; }
            set { leftyanshili = value; }
        }

        private string rightyanshili;
       /// <summary>
       /// 右眼视力
       /// </summary>
        public string Rightyanshili
        {
            get { return rightyanshili; }
            set { rightyanshili = value; }
        }

        private string xieshi;
       /// <summary>
       /// 斜视
       /// </summary>
        public string Xieshi
        {
            get { return xieshi; }
            set { xieshi = value; }
        }


        private string lefter;
       /// <summary>
       /// 左耳
       /// </summary>
        public string Lefter
        {
            get { return lefter; }
            set { lefter = value; }
        }

        private string righter;
       /// <summary>
       /// 右耳
       /// </summary>
        public string Righter
        {
            get { return righter; }
            set { righter = value; }
        }

        private string lefterlisten;
       /// <summary>
       /// 左耳听力
       /// </summary>
        public string Lefterlisten
        {
            get { return lefterlisten; }
            set { lefterlisten = value; }
        }

        private string rightlisten;
       /// <summary>
       /// 右耳 听力
       /// </summary>
        public string Rightlisten
        {
            get { return rightlisten; }
            set { rightlisten = value; }
        }

        private string checkbi;
       /// <summary>
       /// 鼻 
       /// </summary>
        public string Checkbi
        {
            get { return checkbi; }
            set { checkbi = value; }
        }

        private string kouQiang;
       /// <summary>
       /// 空腔
       /// </summary>
        public string KouQiang
        {
            get { return kouQiang; }
            set { kouQiang = value; }
        }

        private string yaciNumber;
       /// <summary>
       /// 牙齿数
       /// </summary>
        public string YaciNumber
        {
            get { return yaciNumber; }
            set { yaciNumber = value; }
        }

        private string yuciNumber;
       /// <summary>
       /// 
       /// </summary>
        public string YuciNumber
        {
            get { return yuciNumber; }
            set { yuciNumber = value; }
        }

        private string skin;
       /// <summary>
       /// 皮肤
       /// </summary>
        public string Skin
        {
            get { return skin; }
            set { skin = value; }
        }

        private string lingbajie;
       /// <summary>
       /// 淋巴结
       /// </summary>
        public string Lingbaji
        {
            get { return lingbajie; }
            set { lingbajie = value; }
        }


        private string bianTaoti;
        /// <summary>
        /// 扁桃体
        /// </summary>
        public string BianTaoti
        {
            get { return bianTaoti; }
            set { bianTaoti = value; }
        }



        private string xinZang;
       /// <summary>
       /// 心脏
       /// </summary>
        public string XinZang
        {
            get { return xinZang; }
            set { xinZang = value; }
        }

        private string feiBu;
       /// <summary>
       /// 肺部
       /// </summary>
        public string FeiBu
        {
            get { return feiBu; }
            set { feiBu = value; }
        }

        private string ganZang;
       /// <summary>
       /// 肝脏
       /// </summary>
        public string GanZang
        {
            get { return ganZang; }
            set { ganZang = value; }
        }

        private string piZang;
        /// <summary>
        /// 脾脏
        /// </summary>
        public string PiZang
        {
            get { return piZang; }
            set { piZang = value; }
        }

        private string qiZhi;
       /// <summary>
       ///期指
       /// </summary>
        public string QiZhi
        {
            get { return qiZhi; }
            set { qiZhi = value; }
        }

        private string siZhi;
       /// <summary>
       /// 四肢
       /// </summary>
        public string SiZhi
        {
            get { return siZhi; }
            set { siZhi = value; }
        }

        private string xiongBu;
       /// <summary>
       /// 胸部
       /// </summary>
        public string XiongBu
        {
            get { return xiongBu; }
            set { xiongBu = value; }
        }

        private string miNiaoQi;
       /// <summary>
       /// 泌尿器
       /// </summary>
        public string MiNiaoQi
        {
            get { return miNiaoQi; }
            set { miNiaoQi = value; }
        }

        private string wuGuan;
       /// <summary>
       /// 五官
       /// </summary>
        public string WuGuan
        {
            get { return wuGuan; }
            set { wuGuan = value; }
        }

        private string jiZhu;
       /// <summary>
       /// 脊柱
       /// </summary>
        public string JiZhu
        {
            get { return jiZhu; }
            set { jiZhu = value; }
        }

        private string pingPqiu;
       /// <summary>
       /// 乒乓球
       /// </summary>
        public string PingPqiu
        {
            get { return pingPqiu; }
            set { pingPqiu = value; }
        }

        private string checkContent;
       /// <summary>
       /// 检查  、、、
       /// </summary>
        public string CheckContent
        {
            get { return checkContent; }
            set { checkContent = value; }
        }

        private string yufangJiezhong;
       /// <summary>
       /// 预防接种
       /// </summary>
        public string YufangJiezhong
        {
            get { return yufangJiezhong; }
            set { yufangJiezhong = value; }
        }

        private string bloodseSu;
       /// <summary>
       /// 血色素
       /// </summary>
        public string BloodseSu
        {
            get { return bloodseSu; }
            set { bloodseSu = value; }
        }

        private string shiWugouCheng;
       /// <summary>
       /// 食物构成
       /// </summary>
        public string ShiWugouCheng
        {
            get { return shiWugouCheng; }
            set { shiWugouCheng = value; }
        }

        private string vitd;
       /// <summary>
       /// VitD
       /// </summary>
        public string Vitd
        {
            get { return vitd; }
            set { vitd = value; }
        }

        private string bigSport;
       /// <summary>
       /// 大运动
       /// </summary>
        public string BigSport
        {
            get { return bigSport; }
            set { bigSport = value; }
        }

        private string spirtSport;
       /// <summary>
       /// 精神运动
       /// </summary>
        public string SpirtSport
        {
            get { return spirtSport; }
            set { spirtSport = value; }
        }

        private string laguage;
       /// <summary>
       /// 语言
       /// </summary>
        public string Laguage
        {
            get { return laguage; }
            set { laguage = value; }
        }

        private string signSocial;
       /// <summary>
       /// 个人社会
       /// </summary>
        public string SignSocial
        {
            get { return signSocial; }
            set { signSocial = value; }
        }

        private string otherBingshi;
       /// <summary>
       /// 其他病史
       /// </summary>
        public string OtherBingshi
        {
            get { return otherBingshi; }
            set { otherBingshi = value; }
        }

        private string handle;
       /// <summary>
       /// 处理
       /// </summary>
        public string Handle
        {
            get { return handle; }
            set { handle = value; }
        }

        private string diagnose;
       /// <summary>
       /// 诊断
       /// </summary>
        public string Diagnose
        {
            get { return diagnose; }
            set { diagnose = value; }
        }


        private string checkMonth;
       /// <summary>
       /// 检查月份
       /// </summary>
        public string CheckMonth
        {
            get { return checkMonth; }
            set { checkMonth = value; }
        }


        private string fuzenDay;
       /// <summary>
       /// 复诊日期
       /// </summary>
        public string FuzenDay
        {
            get { return fuzenDay; }
            set { fuzenDay = value; }
        }

        private string fubu;
       /// <summary>
       /// 腹部
       /// </summary>
        public string Fubu
        {
            get { return fubu; }
            set { fubu = value; }
        }
       /// <summary>
       /// 是否画  早产儿曲线
       /// </summary>
        public string ispre { get; set; }

        public string nuerzhidao;
        public string checkdiagnose;

       public string zhushi;
            public string fushi;
            public string shehui;
            public string dongzuo;
            public string toulu;
            public string gouloubing;
            public int yf;
            public string zonghefazhan;
            public string fuzhujiancha;
            public string otherjiancha { get; set; }
       public string zhusu;
       public string bingshi;
       public string tijian;
      public string zhenduan;
      public string chuli;

            public ChildGaoweigeanRecordObj gaoweirecordobj = new ChildGaoweigeanRecordObj();
            public ChildGoulougeanRecordObj goulourecordobj = new ChildGoulougeanRecordObj();
            public ChildPinxuegeanRecordObj pinxuerecordobj = new ChildPinxuegeanRecordObj();
            public ChildYingyanggeanRecordObj yingyangrecordobj = new ChildYingyanggeanRecordObj();
            public ChildBaseInfoObj baseinfoobj = new ChildBaseInfoObj();
    }
}
