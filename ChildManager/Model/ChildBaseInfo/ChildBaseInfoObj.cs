using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChildManager.Model.ChildBaseInfo
{
    public class ChildBaseInfoObj
    {
        /// <summary>
        /// Id  主键  自增
        /// </summary>
        public int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string healthCardNo;
        /// <summary>
        /// 保健卡号
        /// </summary>
        public string HealthCardNo
        {
            get { return healthCardNo; }
            set { healthCardNo = value; }
        }


        public string childName;
        /// <summary>
        /// 儿童 姓名
        /// </summary>
        public string ChildName
        {
            get { return childName; }
            set { childName = value; }
        }


        public string childGender;
        /// <summary>
        /// 儿童性别
        /// </summary>
        public string ChildGender
        {
            get { return childGender; }
            set { childGender = value; }
        }

        public string bloodType;
        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType
        {
            get { return bloodType; }
            set { bloodType = value; }
        }

      
        public string childBirthDay;
        /// <summary>
        ///  儿童出生时间
        /// </summary>
        public string ChildBirthDay
        {
            get { return childBirthDay; }
            set { childBirthDay = value; }
        }

        public string childBuildDay;
        /// <summary>
        /// 建册日期
        /// </summary>
        public string ChildBuildDay
        {
            get { return childBuildDay; }
            set { childBuildDay = value; }
        }

        public string childBuildHospital;
        /// <summary>
        /// 建册 医院
        /// </summary>
        public string ChildBuildHospital
        {
            get { return childBuildHospital; }
            set { childBuildHospital = value; }
        }

        public string fatherName;
        /// <summary>
        /// 父亲姓名
        /// </summary>
        public string FatherName
        {
            get { return fatherName; }
            set { fatherName = value; }
        }

        public string fatherAge;
        /// <summary>
        /// 父亲 年龄
        /// </summary>
        public string FatherAge
        {
            get { return fatherAge; }
            set { fatherAge = value; }
        }

        public string fatherHeight;
        /// <summary>
        /// 父亲 身高
        /// </summary>
        public string FatherHeight
        {
            get { return fatherHeight; }
            set { fatherHeight = value; }
        }

        public string fatherEducation;
        /// <summary>
        /// 父亲  文化程度
        /// </summary>
        public string FatherEducation
        {
            get { return fatherEducation; }
            set { fatherEducation = value; }
        }

        public string fatherJob;
        /// <summary>
        /// 父亲 职业
        /// </summary>
        public string FatherJob
        {
            get { return fatherJob; }
            set { fatherJob = value; }
        }

        public string motherName;
        /// <summary>
        /// 母亲 姓名
        /// </summary>
        public string MotherName
        {
            get { return motherName; }
            set { motherName = value; }
        }

        public string motherAge;
        /// <summary>
        /// 母亲 年龄
        /// </summary>
        public string MotherAge
        {
            get { return motherAge; }
            set { motherAge = value; }
        }

        public string motherHeight;
        /// <summary>
        /// 母亲身高
        /// </summary>
        public string MotherHeight
        {
            get { return motherHeight; }
            set { motherHeight = value; }
        }

        public string motherEducation;
        /// <summary>
        /// 母亲 文化程度
        /// </summary>
        public string MotherEducation
        {
            get { return motherEducation; }
            set { motherEducation = value; }
        }

        public string motherJob;
        /// <summary>
        /// 母亲职业
        /// </summary>
        public string MotherJob
        {
            get { return motherJob; }
            set { motherJob = value; }
        }


        public string telephone;
        /// <summary>
        /// 联系 电话
        /// </summary>
        public string Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }

        public string Telephone2 { get; set; } 
        public string address;
        /// <summary>
        /// 家庭住址
        /// </summary>
        public string Address
        {
            get { return address; }
            set { address = value; }
        }


        public string nurseryInstitution;
        /// <summary>
        /// 托幼机构
        /// </summary>
        public string NurseryInstitution
        {
            get { return nurseryInstitution; }
            set { nurseryInstitution = value; }
        }

        public string immuneUnit;
        /// <summary>
        /// 免疫发证单位
        /// </summary>
        public string ImmuneUnit
        {
            get { return immuneUnit; }
            set { immuneUnit = value; }
        }


        public string immuneDay;
        /// <summary>
        /// 免疫 发证 日期
        /// </summary>
        public string ImmuneDay
        {
            get { return immuneDay; }
            set { immuneDay = value; }
        }

        public string community;

        /// <summary>
        /// 社区
        /// </summary>
        public string Community
        {
            get { return community; }
            set { community = value; }
        }

        public string censusRegister;
        /// <summary>
        /// 户籍 情况
        /// </summary>
        public string CensusRegister
        {
            get { return censusRegister; }
            set { censusRegister = value; }
        }

        public string cs_fetus;
        /// <summary>
        ///出生  胎
        /// </summary>
        public string Cs_fetus
        {
            get { return cs_fetus; }
            set { cs_fetus = value; }
        }

        public string cs_produce;
        /// <summary>
        /// 出生 产
        /// </summary>
        public string Cs_produce
        {
            get { return cs_produce; }
            set { cs_produce = value; }
        }

        public string cs_week;
        /// <summary>
        /// 出生 周
        /// </summary>
        public string Cs_week
        {
            get { return cs_week; }
            set { cs_week = value; }
        }


        public string cs_day;
        /// <summary>
        /// 出生 天
        /// </summary>
        public string Cs_day
        {
            get { return cs_day; }
            set { cs_day = value; }
        }

        public string modeDelivery;
        /// <summary>
        /// 出生 分娩方式
        /// </summary>
        public string ModeDelivery
        {
            get { return modeDelivery; }
            set { modeDelivery = value; }
        }

        public string perineumIncision;
        /// <summary>
        /// 会阴侧切
        /// </summary>
        public string PerineumIncision
        {
            get { return perineumIncision; }
            set { perineumIncision = value; }
        }

        public string fetusNumber;
        /// <summary>
        /// 胎次
        /// </summary>
        public string FetusNumber
        {
            get { return fetusNumber; }
            set { fetusNumber = value; }
        }

        public string neonatalCondition;
        /// <summary>
        /// 新生儿 情况
        /// </summary>
        public string NeonatalCondition
        {
            get { return neonatalCondition; }
            set { neonatalCondition = value; }
        }

        public string birthWeight;
        /// <summary>
        /// 出生 体重
        /// </summary>
        public string BirthWeight
        {
            get { return birthWeight; }
            set { birthWeight = value; }
        }

        public string birthHeight;
        /// <summary>
        /// 出生 身长
        /// </summary>
        public string BirthHeight
        {
            get { return birthHeight; }
            set { birthHeight = value; }
        }

        public string birthaddress;
        /// <summary>
        /// 出生地
        /// </summary>
        public string Birthaddress
        {
            get { return birthaddress; }
            set { birthaddress = value; }
        }

        public string hospitalizedStates;
        /// <summary>
        /// 在院期间
        /// </summary>
        public string HospitalizedStates
        {
            get { return hospitalizedStates; }
            set { hospitalizedStates = value; }
        }

        public string oneMonth;
        /// <summary>
        /// 一个月
        /// </summary>
        public string OneMonth
        {
            get { return oneMonth; }
            set { oneMonth = value; }
        }

        public string inFourMonth;
        /// <summary>
        /// 四个月 以内
        /// </summary>
        public string InFourMonth
        {
            get { return inFourMonth; }
            set { inFourMonth = value; }
        }


        public string fourToSixMonth;
        /// <summary>
        /// 4 至 6 个月
        /// </summary>
        public string FourToSixMonth
        {
            get { return fourToSixMonth; }
            set { fourToSixMonth = value; }
        }

        public string yunqi;
        /// <summary>
        /// 孕期
        /// </summary>
        public string Yunqi
        {
            get { return yunqi; }
            set { yunqi = value; }
        }

        public string identityno;
        /// <summary>
        /// 身份证号
        /// </summary>
        public string Identityno
        {
            get { return identityno; }
            set { identityno = value; }
        }

        public string status;

        public string gaowei;
        public string zhuangui;
        public string yingyangbuliang;
        public string managetime;
        public string endtime;
        public string jiuzhenCardNo;
        public string linshi;
        public string zhushi;
        public string fuzhenday;
        public string birthtime { get; set; }

        public string ispre { get; set; }

        public string province;
        public string city;
        public string area;
        public string gerenshi;
        public string jiwangshi;
        public string jiazushi;
    }
}
