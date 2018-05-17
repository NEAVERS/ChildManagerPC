using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Windows.Forms;
using YCF.Common.vo;
using YCF.Model.NotMaps;

namespace YCF.Common
{
    public class ReadCard
    {
        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_InitComm(int port);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_ExitComm(int port);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_InitComm_Baud(int port, int combaud);
        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_ResetMifare(int icdev, int _Mode);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_Config_Card(int icdev, char cardtype);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_Card_Hex(int icdev, int port, [Out]StringBuilder rbuff);


        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern int IC_InitType(int port, int stu);

        [DllImport("dcic32")]//引入“mwrf32.dll”API文件
        public static extern short IC_CpuReset_Hex(int icdev, ref byte rlen, StringBuilder rbuff);

        /// <summary>
        /// 判断读卡是否成功
        /// 2017-11-29 cc
        /// </summary>
        /// true=成功
        /// false=失败
        /// <returns></returns>
        public static bool IsSuccess()
        {
            int icdev;
            StringBuilder rbuff;
            int st = -1;
            byte le = 0;
            StringBuilder ls_redata;
            string a = "";
            IC_ExitComm(180);
            string gs_card_type = "COMM";
            icdev = IC_InitComm_Baud(100, 115200);
            if (icdev < 0)
            {
                MessageBox.Show("初始化读卡器失败！");
                IC_ExitComm(icdev);
                return false;
            }
            //射频复位
            st = IC_ResetMifare(icdev, 10);
            //配置要操作的卡型
            st = IC_Config_Card(icdev, 'A');
            if (st != 0)
            {
                MessageBox.Show("配置操作卡型失败！");
                IC_ExitComm(icdev);
                return false;
            }
            rbuff = new StringBuilder(256);

            //寻卡
            st = IC_Card_Hex(icdev, 0, rbuff);
            if (st != 0)
            {
                IC_ExitComm(icdev);
                // 读取就诊卡未找到信息，尝试读取医保卡
                icdev = IC_InitComm(100);

                if (icdev > 0)
                {
                    st = IC_InitType(icdev, 12);
                    if (st != 0)
                    {
                        MessageBox.Show("医保卡初始化失败1！");
                        IC_ExitComm(icdev);
                        icdev = 0;
                        return false;
                    }

                    ls_redata = new StringBuilder(1024);

                    st = IC_CpuReset_Hex(icdev, ref le, ls_redata);

                    if (st != 0)
                    {
                        IC_ExitComm(icdev);
                        icdev = 0;
                        MessageBox.Show("医保卡获取芯片失败！");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("医保卡初始化失败2！");
                    IC_ExitComm(icdev);
                    icdev = 0;
                    return false;
                }

                // 关闭德卡读卡器读卡信道
                IC_ExitComm(icdev);

                icdev = 0;

                if (String.IsNullOrEmpty(ls_redata.ToString()))
                {
                    MessageBox.Show("读卡信息失败,请确认卡是否放好！");
                    return false;
                }

                a = ls_redata.ToString();
                //MessageBox.Show(a);
                gs_card_type = "INSUR";//医保卡
            }
            else
            {
                a = "";

                MD5 md5 = MD5.Create(); //实例化一个md5对像
                                        // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                a = FormsAuthentication.HashPasswordForStoringInConfigFile(rbuff.ToString(), "MD5");
                a = a.ToLower();
                if (String.IsNullOrEmpty(a))
                {
                    icdev = IC_InitComm(100);
                    if (icdev > 0)
                    {
                        st = IC_InitType(icdev, 12);
                        if (st != 0)
                        {
                            MessageBox.Show("就诊卡初始化失败1！");
                            IC_ExitComm(icdev);
                            icdev = 0;
                            return false;
                        }

                        ls_redata = new StringBuilder(1024);

                        st = IC_CpuReset_Hex(icdev, ref le, ls_redata);

                        if (st != 0)
                        {
                            IC_ExitComm(icdev);
                            icdev = 0;
                            MessageBox.Show("就诊卡获取芯片失败！");
                            return false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("就诊卡初始化失败2！");
                        IC_ExitComm(icdev);
                        icdev = 0;
                        return false;
                    }

                    // 关闭德卡读卡器读卡信道
                    IC_ExitComm(icdev);

                    icdev = 0;

                    if (String.IsNullOrEmpty(ls_redata.ToString()))
                    {
                        MessageBox.Show("读卡信息失败,请确认卡是否放好！");
                        return false;
                    }

                }
                else
                {
                    // 关闭德卡读卡器读卡信道
                    IC_ExitComm(icdev);
                    icdev = 0;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取芯片号
        /// 2017-11-29 cc
        /// </summary>
        /// <returns></returns>
        public static string GetChipNum()
        {
            int icdev;
            StringBuilder rbuff;
            int st = -1;
            byte le = 0;
            StringBuilder ls_redata;
            string a = "";
            IC_ExitComm(180);
            string gs_card_type = "COMM";
            icdev = IC_InitComm_Baud(100, 115200);
            if (icdev < 0)
            {
                MessageBox.Show("初始化读卡器失败！");
                IC_ExitComm(icdev);
                return null;
            }
            //射频复位
            st = IC_ResetMifare(icdev, 10);
            //配置要操作的卡型
            st = IC_Config_Card(icdev, 'A');
            if (st != 0)
            {
                MessageBox.Show("配置操作卡型失败！");
                IC_ExitComm(icdev);
                return null;
            }
            rbuff = new StringBuilder(256);

            //寻卡
            st = IC_Card_Hex(icdev, 0, rbuff);
            if (st != 0)
            {
                IC_ExitComm(icdev);
                // 读取就诊卡未找到信息，尝试读取医保卡
                icdev = IC_InitComm(100);

                if (icdev > 0)
                {
                    st = IC_InitType(icdev, 12);
                    if (st != 0)
                    {
                        MessageBox.Show("医保卡初始化失败1！");
                        IC_ExitComm(icdev);
                        icdev = 0;
                        return null;
                    }

                    ls_redata = new StringBuilder(1024);

                    st = IC_CpuReset_Hex(icdev, ref le, ls_redata);

                    if (st != 0)
                    {
                        IC_ExitComm(icdev);
                        icdev = 0;
                        MessageBox.Show("医保卡获取芯片失败！");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show("医保卡初始化失败2！");
                    IC_ExitComm(icdev);
                    icdev = 0;
                    return null;
                }

                // 关闭德卡读卡器读卡信道
                IC_ExitComm(icdev);

                icdev = 0;

                if (String.IsNullOrEmpty(ls_redata.ToString()))
                {
                    MessageBox.Show("读卡信息失败,请确认卡是否放好！");
                    return null;
                }

                a = ls_redata.ToString();
                //MessageBox.Show(a);
                gs_card_type = "INSUR";//医保卡
                return a;
            }
            else
            {
                a = "";

                MD5 md5 = MD5.Create(); //实例化一个md5对像
                                        // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
                a = FormsAuthentication.HashPasswordForStoringInConfigFile(rbuff.ToString(), "MD5");
                a = a.ToLower();
                if (String.IsNullOrEmpty(a))
                {
                    icdev = IC_InitComm(100);
                    if (icdev > 0)
                    {
                        st = IC_InitType(icdev, 12);
                        if (st != 0)
                        {
                            MessageBox.Show("就诊卡初始化失败1！");
                            IC_ExitComm(icdev);
                            icdev = 0;
                            return null;
                        }

                        ls_redata = new StringBuilder(1024);

                        st = IC_CpuReset_Hex(icdev, ref le, ls_redata);

                        if (st != 0)
                        {
                            IC_ExitComm(icdev);
                            icdev = 0;
                            MessageBox.Show("就诊卡获取芯片失败！");
                            return null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("就诊卡初始化失败2！");
                        IC_ExitComm(icdev);
                        icdev = 0;
                        return null;
                    }

                    // 关闭德卡读卡器读卡信道
                    IC_ExitComm(icdev);

                    icdev = 0;

                    if (String.IsNullOrEmpty(ls_redata.ToString()))
                    {
                        MessageBox.Show("读卡信息失败,请确认卡是否放好！");
                        return null;
                    }

                    //a = ls_redata.ToString();

                    //a=就诊卡芯片号

                }
                else
                {
                    // 关闭德卡读卡器读卡信道
                    IC_ExitComm(icdev);
                    icdev = 0;
                }
            }
            return a;
        }

        /// <summary>
        /// 获取读芯片号对象
        /// 2017-11-29 cc
        /// </summary>
        /// <returns></returns>
        public static HisChipInfo GetChipObj()
        {
            if (IsSuccess())
            {
                string chipnum = GetChipNum();
                YHRequest YHReq = new YHRequest(YHRequest.fid_get_hischip);
                YHReq.addQuery(YHRequest.item_chip, YHRequest.compy_equals, "'" + chipnum + "'", YHRequest.splice_and);
                var result = YHMQUtil<HisChipInfo>.get(YHReq);
                HisChipInfo hisobj = result.data;

                return hisobj;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据读卡信息查询His接口信息
        /// 2017-11-29 cc
        /// <param name="PAT_INDEX_NO">病人ID</param>
        /// <param name="CARD_NO">就诊卡号</param>
        /// <param name="isNowDay">是否查询当天</param>
        /// </summary>
        /// <returns></returns>
        public static HisPatientInfo GetHisPatientInfo(string PAT_INDEX_NO, string CARD_NO, bool isNowDay)
        {
            YHRequest YHReq = new YHRequest(YHRequest.fid_get_hisregist);
            if (isNowDay)
            {
                YHReq.addQuery(YHRequest.item_date, YHRequest.compy_gt, "to_date('" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','yyyy-MM-dd')", YHRequest.splice_and);
            }
            if (!string.IsNullOrEmpty(PAT_INDEX_NO))
            {
                YHReq.addQuery(YHRequest.item_patient, YHRequest.compy_equals, "'" + PAT_INDEX_NO + "'", YHRequest.splice_and);
            }
            if (!string.IsNullOrEmpty(CARD_NO))
            {
                YHReq.addQuery(YHRequest.item_visit_no, YHRequest.compy_equals, "'" + CARD_NO + "'", YHRequest.splice_and);
            }

            var result = YHMQUtil<HisPatientInfo>.get(YHReq);
            if (result.success)
            {
                return result.data;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据读卡信息查询His接口信息
        /// 2017-12-05 cc
        /// </summary>
        /// <param name="PAT_INDEX_NO">病人ID</param>
        /// <param name="CARD_NO">就诊卡号</param>
        /// <param name="isNowDay">是否查询当天</param>
        /// <returns></returns>
        public static IList<HisPatientInfo> GetListHisPatientInfo(string PAT_INDEX_NO, string CARD_NO,bool isNowDay)
        {
            YHRequest YHReq = new YHRequest(YHRequest.fid_get_hisregist);
            if (isNowDay)
            {
                YHReq.addQuery(YHRequest.item_date, YHRequest.compy_gt, "to_date('" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "','yyyy-MM-dd')", YHRequest.splice_and);
            }
            if (!string.IsNullOrEmpty(PAT_INDEX_NO))
            {
                YHReq.addQuery(YHRequest.item_patient, YHRequest.compy_equals, "'" + PAT_INDEX_NO + "'", YHRequest.splice_and);
            }
            if (!string.IsNullOrEmpty(CARD_NO))
            {
                YHReq.addQuery(YHRequest.item_visit_no, YHRequest.compy_equals, "'" + CARD_NO + "'", YHRequest.splice_and);
            }

            var result = YHMQUtil<HisPatientInfo>.get(YHReq);
            if (result.success)
            {
                return result.dataList;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据科室list添加条件查询His信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        
        //public static IList<HisPatientInfo> GetListHisPatientByMore(List<tab_dept_data> list )
        //{
        //    YHRequest YHReq = new YHRequest(YHRequest.fid_get_hisregist);

        //}
    }
}
