
using IBM.WMQ.Nmqi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using YCF.Common.vo;

namespace YCF.Common
{
    public class YHMQUtil<T> where T : new()
    {
        public static YHResponse<T> get(YHRequest request)
        {
            YHResponse<T> response = new YHResponse<T>();
            try
            {
                //LogUtil.writeLog("MQ请求数据：" + request.getQueryXml());
                long ret = 0;
                string respMsgId = "";
                string respMsg = "";
                MQDLL.MQFuntion MQManagment = new MQDLL.MQFuntion();
                ret = MQManagment.connectMQ();
                ret = MQManagment.putMsg(request.Fid, request.getQueryXml(), ref respMsgId);
                ret = MQManagment.getMsgById(request.Fid, respMsgId, 60000, ref respMsg);
                MQManagment.disconnectMQ();
                response.xmlData = XmlUtil.filterSpASCII(respMsg);
                response.success = true;
                response = convert(response);
            }
            catch (Exception e)
            {
                response.msg = "调用医惠MQ出现错误";
                response.success = false;

                //while (e != null)
                //{
                //    MessageBox.Show(e.Message);
                //    e = e.InnerException;
                //}
                MessageBox.Show(response.msg);
                //LogUtil.writeLog("调用医惠MQ出现错误：" + e.ToString());
            }
            return response;
        }

        private static YHResponse<T> convert(YHResponse<T> resp)
        {
            if (!resp.success || String.IsNullOrEmpty(resp.xmlData))
            {
                return resp;
            }
            else
            {
                XmlDocument xml = new XmlDocument();
                try
                {  
                    xml.LoadXml(resp.xmlData);
                    //状态
                    XmlNode RetCode = xml.SelectSingleNode("/ESBEntry/RetInfo/RetCode");
                    XmlNode RetCon  = xml.SelectSingleNode("/ESBEntry/RetInfo/RetCon");
                    if (RetCode != null) {
                        resp.success = (RetCode.InnerText == "1");
                    }
                    if (RetCode != null)
                    {
                        resp.msg = RetCon.InnerText;
                    }
                    //成功、然后解析数据
                    if (resp.success) {
                        String prePath = "/msg/body/row";
                        XmlNodeList dataList = xml.SelectNodes("/ESBEntry/MsgInfo/Msg");
                        if (dataList != null && dataList.Count >0) {
                            if (dataList.Count == 1)
                            {
                              XmlNode node = dataList.Item(0);
                              T data = XmlUtil.DeserializeToObject<T>(prePath, node.InnerText);
                              IList<T> tList = new List<T>();
                              tList.Add(data);
                              resp.data = data;
                              resp.dataList = tList;
                            }
                            else
                            {
                                int len = dataList.Count;
                                IList<T> tList = new List<T>();
                                for (int i=0;i<len;i++) {
                                    XmlNode node = dataList.Item(i);
                                    T data = XmlUtil.DeserializeToObject<T>(prePath, node.InnerText);
                                    tList.Add(data);
                                }
                                resp.dataList = tList;
                                return resp;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //LogUtil.writeLog("解析MQ返回XML出现错误，" + e.ToString());
                    return resp;
                }
            }
            return resp;
        }
    }
}
