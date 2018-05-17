using System;
using System.Collections.Generic;
using System.Text;

namespace YCF.Common
{
    /// <summary>
    /// 日期处理工具类
    /// </summary>
    public class DateTimeUtilitiesCls
    {
        /// <summary>
        /// 获取上个月的天数
        /// </summary>
        public int GetDaysOfLastMonth(DateTime p_DateTime)
        {
            int lastMonth;//上个月
            int curMonth = p_DateTime.Month;//当前月
            //判断:若当前月为1月，上个月为12月
            if (curMonth == 1)
            {
                lastMonth = 12;
            }
            else
            {
                lastMonth = curMonth - 1;
            }
            //返回结果
            return this.GetDaysOfMonth(p_DateTime.Year, lastMonth);
        }

        /// <summary>
        /// 获取下个月的天数
        /// </summary>
        public int GetDaysOfNextMonth(DateTime p_DateTime)
        {
            int nextMonth;//下个月
            int curMonth = p_DateTime.Month;//当前月
            //判断:若当前月为12月，下个月为1月
            if (curMonth == 12)
            {
                nextMonth = 1;
            }
            else
            {
                nextMonth = curMonth + 1;
            }
            //返回结果
            return this.GetDaysOfMonth(p_DateTime.Year, nextMonth);
        }

        /// <summary>
        /// 获取某月的天数
        /// 若参数错误，返回-1
        /// </summary>
        /// <param name="p_DateTime">具体的日期参数</param>
        public int GetDaysOfMonth(DateTime p_DateTime)
        {
            return this.GetDaysOfMonth(p_DateTime.Year, p_DateTime.Month);
        }

        /// <summary>
        /// 获取某月的天数
        /// 若参数错误，返回-1
        /// </summary>
        /// <param name="p_Year">年份</param>
        /// <param name="p_Month">月份</param>
        public int GetDaysOfMonth(int p_Year, int p_Month)
        {
            //返回结果
            int result;
            //判断是否为闰年
            bool isLeapYear = DateTime.IsLeapYear(p_Year);
            //遍历并给结果赋值
            switch (p_Month)
            {
                //1、3、5、7、8、10、12这几个月=31天
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    result = 31;
                    break;
                //4、6、9、11这几个月=30天
                case 4:
                case 6:
                case 9:
                case 11:
                    result = 30;
                    break;
                //2月，闰年=29天，非闰年=28天
                case 2:
                    if (isLeapYear)
                    { result = 29; }
                    else
                    { result = 28; }
                    break;
                default:
                    result = -1;
                    break;
            }
            return result;
        }
    }
}