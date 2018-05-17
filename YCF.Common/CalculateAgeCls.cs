using System;
using System.Collections.Generic;
using System.Text;

namespace YCF.Common
{
    /// <summary>
    /// 年龄计算工具类
    /// 由两个日期参数计算出相对年龄字符串
    /// </summary>
    public class CalculateAgeCls
    {
        /// 通过生日和当前日期计算岁，月，天
        /// </summary>
        /// <param name="birthday">生日</param>
        /// <param name="now">当前日期</param>
        /// <param name="year">岁</param>
        /// <param name="month">月</param>
        /// <param name="day">天</param>
        public string GetAgeByBirthdaystr(DateTime birthday, DateTime now)
        {
            //生日的年，月，日
            int birthdayYear = birthday.Year;
            int birthdayMonth = birthday.Month;
            int birthdayDay = birthday.Day;

            //当前时间的年,月,日

            int nowYear = now.Year;
            int nowMonth = now.Month;
            int nowDay = now.Day;
            int year, day, month;

            //得到天
            if (nowDay >= birthdayDay)
            {
                day = nowDay - birthdayDay;
            }
            else
            {
                nowMonth -= 1;
                day = GetDay(nowMonth, nowYear) + nowDay - birthdayDay;
            }

            //得到月
            if (nowMonth >= birthdayMonth)
            {
                month = nowMonth - birthdayMonth;
            }
            else
            {
                nowYear -= 1;
                month = 12 + nowMonth - birthdayMonth;
            }

            //得到年
            year = nowYear - birthdayYear;
            return year + "岁" + month + "月" + day + "天";
        }

        public int[] GetAgeByBirthday(DateTime birthday, DateTime now)
        {
            //生日的年，月，日
            int birthdayYear = birthday.Year;
            int birthdayMonth = birthday.Month;
            int birthdayDay = birthday.Day;

            //当前时间的年,月,日

            int nowYear = now.Year;
            int nowMonth = now.Month;
            int nowDay = now.Day;
            int year, day, month;

            //得到天
            if (nowDay >= birthdayDay)
            {
                day = nowDay - birthdayDay;
            }
            else
            {
                nowMonth -= 1;
                day = GetDay(nowMonth, nowYear) + nowDay - birthdayDay;
            }

            //得到月
            if (nowMonth >= birthdayMonth)
            {
                month = nowMonth - birthdayMonth;
            }
            else
            {
                nowYear -= 1;
                month = 12 + nowMonth - birthdayMonth;
            }

            //得到年
            year = nowYear - birthdayYear;
            int[] age = new int[3];
            age[0] = year;
            age[1] = month;
            age[2] = day;
            return age;
        }

        /// <summary>
        /// 获取天数
        /// </summary>
        private int GetDay(int month, int year)
        {
            int day = 0;
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    day = 31;
                    break;
                case 2:
                    //闰年天，平年天
                    if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                    {
                        day = 29;
                    }
                    else
                    {
                        day = 28;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    day = 30;
                    break;
            }
            return day;
        }
    }
}