using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace login.Hospital.Common
{
    class Converbirthage
    {
       public string getAge(string strMyBirth)
        {
            try
            {
                //string strFlag = "";
                int intFlagAge = 0;
                string strFlagUnit = "";
                DateTime dtime = DateTime.ParseExact(strMyBirth,"yyyyMMdd",null);
                DateTime myBirth= Convert.ToDateTime(dtime.ToString("yyyy-MM-dd"));
             
                DateTime myNow = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());

                int intYear = dateDiff(DatePart.YY, myBirth, myNow);
                int intMonth = dateDiff(DatePart.MM, myBirth, myNow);
                int intDay = dateDiff(DatePart.DD, myBirth, myNow);
                int intHour = dateDiff(DatePart.HH, myBirth, myNow);
                int intMinute = dateDiff(DatePart.MI, myBirth, myNow);
                int intSecond = dateDiff(DatePart.SS, myBirth, myNow);
                int intYearMillisecond = dateDiff(DatePart.MS, myBirth, myNow);

                if (intDay == 0)
                {
                    intFlagAge = intHour;
                    strFlagUnit = "时";
                }
                else
                {
                    if (intMonth == 0)
                    {
                        //不满一个月的
                        intFlagAge = intDay;
                        strFlagUnit = "天";
                    }
                    else if (intMonth < 12)
                    {
                        //满一个月但是未满一年的
                        int intAddDD = dateDiff(DatePart.DD, myBirth.AddMonths(intMonth), myNow);
                        if (intAddDD == 0)
                        {
                            intFlagAge = intMonth;
                            strFlagUnit = "月";
                        }
                        else if (intAddDD < 0)
                        {
                            if (intMonth - 1 == 0)
                            {
                                intFlagAge = intDay;
                                strFlagUnit = "天";
                            }
                            else
                            {
                                //strFlag = (intMonth - 1) + "月" + dateDiff(DatePart.DD, myBirth.AddMonths(intMonth - 1), myNow) + "天";
                                intFlagAge = (intMonth - 1);
                                strFlagUnit = "月";
                            }
                        }
                        else
                        {
                            //strFlag = intMonth + "月" + dateDiff(DatePart.DD, myBirth.AddMonths(intMonth - 1), myNow) + "天";
                            intFlagAge = intMonth;
                            strFlagUnit = "月";
                        }
                    }
                    else
                    {
                        //满一年但是小于或等于7岁
                        if (intYear <= 7)
                        {
                            int intAddMM = dateDiff(DatePart.MM, myBirth.AddYears(intYear), myNow);
                            if (intAddMM < 0)
                            {
                                //strFlag = (intYear - 1) + "岁" + (intMonth + 12) + "月";
                                intFlagAge = intYear - 1;
                                strFlagUnit = "岁";
                            }
                            else if (intAddMM == 0)
                            {
                                //strFlag = intYear + "岁";
                                intFlagAge = intYear;
                                strFlagUnit = "岁";
                            }
                            else
                            {
                                //strFlag = intYear + "岁" + intMonth + "月";
                                intFlagAge = intYear;
                                strFlagUnit = "岁";
                            }
                        }
                        else
                        {
                            if (myBirth.Month < myNow.Month)
                            {
                                //strFlag = intYear + "岁";
                                intFlagAge = intYear;
                                strFlagUnit = "岁";
                            }
                            else if (myBirth.Month == myNow.Month && myBirth.Day < myNow.Day)
                            {
                                //strFlag = intYear + "岁";
                                intFlagAge = intYear;
                                strFlagUnit = "岁";
                            }
                            else
                            {
                                //strFlag = (intYear - 1) + "岁";
                                intFlagAge = intYear - 1;
                                strFlagUnit = "岁";
                            }
                        }
                    }
                }

                return intFlagAge.ToString();// +"|" + strFlagUnit;
            }
            catch (Exception ex)
            {
                return "0|岁";
            }
        }

        /// <summary>         
        /// 返回两个日期的时间差         
        /// </summary>         
        /// <param name="datepart">DatePart枚举值</param>         
        /// <param name="starttime">起始时间</param>         
        /// <param name="endtime">结束时间</param>         
        /// <returns></returns>         
        private int dateDiff(DatePart datepart, DateTime starttime, DateTime endtime)
        {
            int rtn = 0;
            TimeSpan start = new TimeSpan(starttime.Ticks);
            TimeSpan end = new TimeSpan(endtime.Ticks);
            TimeSpan delta = end.Subtract(start);
            int year = endtime.Year - starttime.Year;
            int month = year * 12 + (endtime.Month - starttime.Month);
            int day = delta.Days;
            int hour = delta.Hours;
            int minute = delta.Minutes;
            int second = delta.Seconds;
            int milliseconds = delta.Milliseconds;
            switch (datepart)
            {
                case DatePart.YY:
                    rtn = year;
                    break;
                case DatePart.MM:
                    rtn = month;
                    break;
                case DatePart.DD:
                    rtn = day;
                    break;
                case DatePart.HH:
                    rtn = hour;
                    break;
                case DatePart.MI:
                    rtn = minute;
                    break;
                case DatePart.SS:
                    rtn = second;
                    break;
                case DatePart.MS:
                    rtn = milliseconds;
                    break;
            }
            return rtn;
        }

        /// <summary>     
        /// 日期枚举值     
        /// </summary>     
        private enum DatePart
        {
            /// <summary>         
            /// 年         
            /// </summary>         
            YY,
            /// <summary>         
            /// 月         
            /// </summary>         
            MM,
            /// <summary>         
            /// 日         
            /// </summary>         
            DD,
            /// <summary>         
            /// 时         
            /// </summary>         
            HH,
            /// <summary>         
            /// 分         
            /// </summary>         
            MI,
            /// <summary>         
            /// 秒         
            /// </summary>         
            SS,
            /// <summary>         
            /// 毫秒         
            /// </summary>         
            MS
        }
    }
   
}
