using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace YCF.Common
{
   public class XmlUtil
    {
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type,string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static object Deserialize(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return xmldes.Deserialize(stream);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            sr.Dispose();
            Stream.Dispose();
            return str;
        }

        public static string filterSpASCII(string tmp)
        {
            StringBuilder info = new StringBuilder();
            foreach (char cc in tmp)
            {
                int ss = (int)cc;
                if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)))
                {
                    info.AppendFormat("&#x{0:X};", ss);
                }   
                else
                {
                    info.Append(cc);
                }                
            }
            return info.ToString().Replace("&#x20;", " ");
        }

        public static T DeserializeToObject<T>(String prePath,string strXml) where T : new()
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                T data = new T();
                xml.LoadXml(strXml);
                Type type = typeof(T);//取得属性集合
                PropertyInfo[] propertyList = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (propertyList != null && propertyList.Length > 0)
                {
                    foreach (PropertyInfo property in propertyList)
                    {
                        XmlNode itemNode = xml.SelectSingleNode(prePath + "/" + property.Name);
                        if (itemNode != null)
                        {
                            try
                            {
                                property.SetValue(data, Convert.ChangeType(itemNode.InnerText,property.PropertyType), null);
                            }
                            catch (Exception e) {
                                //LogUtil.writeLog("XML转对象(属性)出现错误" + e.ToString());
                            }                            
                        }
                    }
                }
                return data;
            }
            catch (Exception e)
            {
                //LogUtil.writeLog("XML转对象出现错误" + e.ToString());
            }
            return default(T);
        }
    }
}
