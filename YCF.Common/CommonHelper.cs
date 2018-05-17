using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Collections;
using static System.Windows.Forms.Control;
using System.Threading;
using System.Globalization;
using System.Data;

namespace YCF.Common
{
    public class CommonHelper
    {
        private static string _efConnentionString = null;

        public static string EFConnentionString
        {
            get
            {
                if (string.IsNullOrEmpty(_efConnentionString))
                {
                    var db_name = OperatFile.GetIniFileString("DataBase", "db_name", "", Application.StartupPath + "\\Config.ini");
                    var db_server = OperatFile.GetIniFileString("DataBase", "db_server", "", Application.StartupPath + "\\Config.ini");
                    var UserID = OperatFile.GetIniFileString("DataBase", "UserID", "", Application.StartupPath + "\\Config.ini");
                    var Pwd = OperatFile.GetIniFileString("DataBase", "Pwd", "", Application.StartupPath + "\\Config.ini");

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                    {
                        DataSource = db_server,
                        InitialCatalog = db_name,
                        PersistSecurityInfo = true,
                        IntegratedSecurity = false,
                        MultipleActiveResultSets = true,

                        UserID = UserID,
                        Password = Pwd,
                    };


                    var entityConnectionStringBuilder = new EntityConnectionStringBuilder
                    {
                        Provider = "System.Data.SqlClient",
                        ProviderConnectionString = builder.ConnectionString,
                        Metadata = "res://*/Entities.csdl|res://*/Entities.ssdl|res://*/Entities.msl",
                    };

                    _efConnentionString = entityConnectionStringBuilder.ConnectionString;
                }

                return _efConnentionString;
            }
        }

        /// <summary>
        /// 获取医院配置名称
        /// </summary>
        /// <returns></returns>
        public static string GetHospitalName()
        {
            return OperatFile.GetIniFileString("HospitalInfo", "hospital_name", "", Application.StartupPath + "\\hospitalinfo.ini");
        }

        public static void setCombValue(ComboBox cbx, string xmlname)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlname);    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点  
            XmlNodeList personNodes = rootElem.GetElementsByTagName("person"); //获取person子节点集合  
            foreach (XmlNode node in personNodes)
            {
                string strName = ((XmlElement)node).GetAttribute("name");   //获取name属性值  
                cbx.Items.Add(strName);
            }
        }


        /// <summary>
        /// 根据控件名得到控件
        /// </summary>
        /// <param name="strTxt">控件名</param>
        /// <returns></returns>
        public static Control FindControl(string strTxt, UserControl userContorl)
        {
            return (Control)userContorl.GetType().GetField(strTxt, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase).GetValue(userContorl);
        }

        public static void DisposeControls(IEnumerable<Control> ctls)
        {
            foreach (var item in ctls)
            {
                item.Dispose();
            }
        }

        public static void DisposeControls(IEnumerable ctls)
        {
            foreach (var item in ctls)
            {
                (item as Control)?.Dispose();
            }
        }

        public static T GetObjMenzhen<T>(ControlCollection cc, int child_id) where T : new()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (Control item in cc)
            {
                if (item is TextBox || item is MaskedTextBox || item is ComboBox || item is DateTimePicker || item is DevComponents.Editors.DateTimeAdv.DateTimeInput)
                {
                    dic.Add(item.Name, item.Text.Trim());
                }
                else if (item is Panel)
                {
                    Panel pnitem = item as Panel;
                    dic.Add(pnitem.Name, getcheckedValue(pnitem));
                }
                else if (item is GroupBox)
                {
                    foreach (Control itempanel in (item as GroupBox).Controls)
                    {
                        if (itempanel is TextBox || itempanel is MaskedTextBox || itempanel is ComboBox || itempanel is DateTimePicker || itempanel is DevComponents.Editors.DateTimeAdv.DateTimeInput)
                        {

                            dic.Add(itempanel.Name, itempanel.Text.Trim());
                        }
                        else if (itempanel is Panel)
                        {
                            Panel pnitem = itempanel as Panel;
                            dic.Add(pnitem.Name, getcheckedValue(pnitem));
                        }
                    }

                }
            }

            dic.Add("child_id", child_id);
            dic.Add("update_name", globalInfoClass.UserName);
            dic.Add("update_code", globalInfoClass.UserCode);
            dic.Add("update_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            dic.Add("operate_code", globalInfoClass.UserCode);
            dic.Add("operate_name", globalInfoClass.UserName);
            dic.Add("operate_time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            var md = new T();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            foreach (var d in dic)
            {
                //var filed = textInfo.ToTitleCase(d.Key);
                try
                {
                    var value = d.Value;
                    md.GetType().GetProperty(d.Key).SetValue(md, value, null);
                }
                catch (Exception e)
                {

                }
            }
            return md;

        }

        public static T GetObj<T>(ControlCollection cc) where T : new()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (Control item in cc)
            {
                if (item is TextBox || item is ComboBox || item is DateTimePicker || item is DevComponents.Editors.DateTimeAdv.DateTimeInput)
                {
                    dic.Add(item.Name, item.Text.Trim());
                }
                else if (item is Panel)
                {
                    Panel pnitem = item as Panel;
                    dic.Add(pnitem.Name, getcheckedValue(pnitem));
                }
                else if (item is GroupBox)
                {
                    foreach (Control itempanel in (item as GroupBox).Controls)
                    {
                        if (itempanel is TextBox || itempanel is ComboBox || itempanel is DateTimePicker || itempanel is DevComponents.Editors.DateTimeAdv.DateTimeInput)
                        {
                            dic.Add(itempanel.Name, itempanel.Text.Trim());
                        }
                        else if (itempanel is Panel)
                        {
                            Panel pnitem = itempanel as Panel;
                            dic.Add(pnitem.Name, getcheckedValue(pnitem));
                        }
                    }

                }
            }

            var md = new T();
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;
            foreach (var d in dic)
            {
                //var filed = textInfo.ToTitleCase(d.Key);
                try
                {
                    var value = d.Value;
                    md.GetType().GetProperty(d.Key).SetValue(md, value, null);
                }
                catch (Exception e)
                {

                }
            }
            return md;
        }

        public static void setForm<T>(T t, ControlCollection cc)
        {
            JsonUtility ju = JsonUtility.Instance;

            string jsonstr = ju.ObjectToJson(t);
            Dictionary<string, object> JsonData = ju.JsonToDictionary(jsonstr);

            foreach (Control item in cc)
            {
                if (item is TextBox || item is MaskedTextBox || item is ComboBox || item is DateTimePicker || item is DevComponents.Editors.DateTimeAdv.DateTimeInput)
                {
                    if (JsonData.ContainsKey(item.Name))
                    {
                        item.Text = JsonData[item.Name]?.ToString();
                    }
                }
                else if (item is Panel)
                {
                    Panel pnitem = item as Panel;
                    if (JsonData.ContainsKey(item.Name))
                    {
                        setcheckedValue(pnitem, JsonData[item.Name]?.ToString());
                    }
                }
                else if (item is GroupBox)
                {
                    foreach (Control itempanel in (item as GroupBox).Controls)
                    {
                        if (itempanel is TextBox || itempanel is MaskedTextBox || itempanel is ComboBox || itempanel is DateTimePicker || itempanel is DevComponents.Editors.DateTimeAdv.DateTimeInput)
                        {
                            if (JsonData.ContainsKey(itempanel.Name))
                            {
                                itempanel.Text = JsonData[itempanel.Name]?.ToString();
                            }
                        }
                        else if (itempanel is Panel)
                        {
                            Panel pnitem = itempanel as Panel;
                            if (JsonData.ContainsKey(itempanel.Name))
                            {
                                setcheckedValue(pnitem, JsonData[itempanel.Name]?.ToString());
                            }

                        }
                    }

                }
            }
        }

        #region xml

        public static IList<string> GetXMLNodeValues(string fileName, string nodeName, string attrName)
        {
            List<string> list = null;
            XmlDocument doc = new XmlDocument();


            doc.Load(fileName);    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点
            XmlNodeList personNodes = rootElem.GetElementsByTagName(nodeName);
            if (personNodes != null && personNodes.Count > 0)
            {
                list = new List<string>();
                foreach (XmlNode node in personNodes)
                {
                    list.Add(((XmlElement)node).GetAttribute(attrName));
                }
            }

            return list;
        }

        public static IList<T> GetList<T>(string fileName, string nodeName)
            where T : new()
        {
            IList<T> list = null;
            var props = typeof(T).GetProperties();
            XmlDocument doc = new XmlDocument();
            nodeName = string.IsNullOrEmpty(nodeName) ? typeof(T).Name : nodeName;

            doc.Load(fileName);    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点
            XmlNodeList nodes = rootElem.GetElementsByTagName(nodeName);

            if (nodes != null && nodes.Count > 0)
            {
                list = new List<T>();

                foreach (XmlNode node in nodes)
                {
                    var element = (XmlElement)node;
                    var t = new T();
                    foreach (var prop in props)
                    {
                        if (prop.CanWrite)
                        {
                            prop.SetValue(t, element.GetAttribute(prop.Name), null);
                        }
                    }

                    list.Add(t);
                }
            }

            return list;
        }

        public static string GetXMLNodeValue(string fileName, string nodeName, string name, string attrName)
        {
            string str = null;
            XmlDocument doc = new XmlDocument();


            doc.Load(fileName);    //加载Xml文件  
            XmlElement rootElem = doc.DocumentElement;   //获取根节点
            XmlNodeList personNodes = rootElem.GetElementsByTagName(nodeName);
            if (personNodes != null && personNodes.Count > 0)
            {
                foreach (XmlNode node in personNodes)
                {
                    var element = (XmlElement)node;
                    if (element.GetAttribute("name") == name)
                    {
                        return element.GetAttribute(attrName);
                    }

                }
            }

            return str;
        }

        #endregion


        #region 读取和存储登录用户名历史记录
        private string[] array = null;
        public void InitAutoCompleteCustomSource(TextBox textBox, string name)
        {
            array = ReadUserNameTxt(name);
            if (array != null && array.Length > 0)
            {
                //AutoCompleteStringCollection autoComlete = new AutoCompleteStringCollection();

                for (int i = 0; i < array.Length; i++)
                {
                    //autoComlete.Add(array[i]);
                    textBox.Text = array[i];
                }
                //textBox.AutoCompleteCustomSource = autoComlete;
            }

        }


        public void InitAutoCompleteCustomSource(ComboBox combobox, string name)
        {
            array = ReadUserNameTxt(name);
            if (array != null && array.Length > 0)
            {
                AutoCompleteStringCollection autoComlete = new AutoCompleteStringCollection();
                for (int i = 0; i < array.Length; i++)
                {
                    autoComlete.Add(array[i]);
                }
                combobox.AutoCompleteCustomSource = autoComlete;
            }
        }
        /// <summary>
        /// 读取用户名
        /// </summary>
        /// <returns></returns>
        string[] ReadUserNameTxt(string name)
        {
            try
            {
                if (!File.Exists("'" + name + "'.txt"))
                {
                    FileStream fs =
                        File.Create("'" + name + "'.txt");
                    fs.Close();
                    fs = null;
                }
                return File.ReadAllLines("'" + name + "'.txt", Encoding.Default);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 登录 成功保存用户名
        /// </summary>
        /// <param name="str"></param>
        public void WriterUserNameTxt(string str, string name)
        {
            StreamWriter writer = null;
            try
            {
                if (array != null && !array.Contains(str))
                {
                    writer = new StreamWriter("'" + name + "'.txt", true, Encoding.Default);
                    writer.WriteLine(str);
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer = null;
                }
            }
        }

        #endregion

        /// <summary>
        /// 遍历 读取 字符串中包含的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetNumber(string str)
        {
            string result = "";
            System.Text.RegularExpressions.MatchCollection match = System.Text.RegularExpressions.Regex.Matches(str, @"(?<number>(\+|-)?(0|[1-9]\d*)(\.\d*[0-9])?)");
            foreach (System.Text.RegularExpressions.Match mc in match)
            {
                //g.DrawString("√", new Font("Arial Unicode MS", 9.5f), brush, jwanmang1, sf);
                //alist.Add(mc.Result("${number}"));
                //Console.WriteLine(alist.Last());
                result += mc.Result("${number}");
            }
            return result;
        }

        public static bool IsNumber(string input)
        {
            string pattern = "^-?\\d+$|^(-?\\d+)(\\.\\d+)?$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 判断dgv是否为空
        /// ywj
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool DataGridViewisNull(DataGridView dgv, string message)
        {
            bool isb = true;
            if (dgv.Rows.Count <= 0)
            {
                isb = false;
                MessageBox.Show(message, "系统提示");
            }
            return isb;
        }


        public static void SetAllControls(Control control, bool selectNextControl)
        {
            foreach (Control con in control.Controls)
            {
                if (con is Panel || con is GroupBox || con is Label)
                {
                    if (con.Controls.Count > 0)
                    {
                        SetAllControls(con, selectNextControl);
                    }
                }
                else if (con is DateTimePicker || con is ComboBox || con is TextBox || con is MaskedTextBox)
                {
                    if (selectNextControl)
                    {
                        con.KeyPress += new KeyPressEventHandler(all_SelectNexControl_KeyPress);
                        con.KeyDown += new KeyEventHandler(all_SelectNexControl_KeyDown);
                    }
                    else
                    {
                        con.KeyPress += new KeyPressEventHandler(all_KeyPress);
                        con.KeyDown += new KeyEventHandler(all_KeyDown);

                    }
                }
                else if (con is CheckBox)
                {
                    if (selectNextControl)
                    {
                        con.KeyPress += new KeyPressEventHandler(all_SelectNexControl_KeyPress);
                        con.KeyUp += new KeyEventHandler(all_SelectNexControl_KeyDown);
                    }
                    else
                    {
                        con.KeyPress += new KeyPressEventHandler(all_KeyPress);
                        con.KeyUp += new KeyEventHandler(all_KeyDown);
                    }
                }

            }
        }


        public static void SetAllControls(Control control)
        {
            foreach (Control con in control.Controls)
            {
                if (con is Panel || con is GroupBox || con is Label)
                {
                    if (con.Controls.Count > 0)
                    {
                        SetAllControls(con);
                    }
                }
                else if (con is DateTimePicker || con is ComboBox || con is TextBox || con is MaskedTextBox)
                {
                    con.KeyPress += new KeyPressEventHandler(all_KeyPress);
                    con.KeyDown += new KeyEventHandler(all_KeyDown);
                    //if (con is DateTimePicker || con is ComboBox)
                    //{
                    //    con.Enter += all_Enter;
                    //}
                }
                else if (con is CheckBox)
                {
                    con.KeyPress += new KeyPressEventHandler(all_KeyPress);
                    con.KeyUp += new KeyEventHandler(all_KeyDown);
                }

            }
        }

        private static void all_SelectNexControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //(sender as Control).TopLevelControl.SelectNextControl(sender as Control, true, true, true, true);
                var nextControl = GetNextTabControl((sender as Control), true, 2000);

                nextControl.Select();
                if (nextControl is TextBoxBase)
                {
                    (nextControl as TextBoxBase).SelectAll();
                }

            }
        }

        private static void all_SelectNexControl_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                var nextControl = GetNextTabControl((sender as Control), false, 2000);

                nextControl.Select();
                if (nextControl is TextBoxBase)
                {
                    (nextControl as TextBoxBase).SelectAll();
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                var nextControl = GetNextTabControl((sender as Control), true, 3000);

                nextControl.Select();
                if (nextControl is TextBoxBase)
                {
                    (nextControl as TextBoxBase).SelectAll();
                }
            }
        }


        private static void all_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{Tab}");
            }
        }

        private static void all_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                SendKeys.Send("+{Tab}");
            }
            else if (e.KeyCode == Keys.Right)
            {
                SendKeys.Send("{Tab}");
            }
        }

        /// <summary>
        /// 获取下一个可Tab的控件
        /// 2017-11-23
        /// </summary>
        /// <param name="ctl">当前控件</param>
        /// <param name="forward">向前(true)或向后(false)搜索</param>
        /// <param name="maxSearchCount">搜索控件的数量上限</param>
        /// <returns></returns>
        public static Control GetNextTabControl(Control ctl, bool forward, int maxSearchCount)
        {
            var topControl = ctl.TopLevelControl;
            Control nextCtl = null;
            for (int i = 0; i < maxSearchCount; i++)
            {
                ctl = topControl.GetNextControl(ctl, forward);

                if (ctl.TabStop)
                {
                    nextCtl = ctl;
                    break;
                }
            }

            return nextCtl;
        }

        //public static void GetPreControl(Control control, IList<string>)
        //{
        //    if (control.Parent is GroupBox || control.Parent is Panel)
        //    {
        //        GetPreControl(control.Parent, tabindex);
        //    }
        //    else if (control.Parent is UserControl || control.Parent is Form)
        //    {
        //        foreach (Control con in control.Controls)
        //        {
        //            if (con is Panel || con is GroupBox || con is Label)
        //            {
        //                if (con.Controls.Count > 0)
        //                {
        //                    SetAllControls(con);
        //                }
        //            }
        //            else if (con is DateTimePicker || con is ComboBox || con is TextBox || con is CheckBox)
        //            {
        //                if (con.TabIndex)
        //                    con.KeyPress += new KeyPressEventHandler(all_KeyPress);

        //                //if (con is DateTimePicker || con is ComboBox)
        //                //{
        //                //    con.Enter += all_Enter;
        //                //}
        //            }

        //        }
        //    }

        //}

        private static void all_Enter(object sender, EventArgs e)
        {
            SendKeys.Send("{F4}");
        }

        //获取复选框中的值及其他备注项目的值
        public static string getcheckedValue(Panel panels)
        {
            string returnval = "";
            foreach (Control c in panels.Controls)
            {
                if (c is CheckBox)
                {
                    if ((c as CheckBox).Checked)
                    {
                        returnval += (c.Text + ",");
                    }
                }
                else if (c is TextBox)
                {
                    if (!String.IsNullOrEmpty(c.Text))
                    {
                        returnval += (":" + c.Text + ",");
                    }
                }
                else if (c is ComboBox)
                {
                    if (!String.IsNullOrEmpty(c.Text))
                    {
                        returnval += (":" + c.Text + ",");
                    }
                }
            }
            if (returnval != "")
            {
                returnval = returnval.Substring(0, returnval.Length - 1);
            }
            return returnval;
        }

        public static void setcheckedValue(Panel panels, string checkvals)
        {
            //假如读取到的值不为null
            if (!String.IsNullOrEmpty(checkvals))
            {
                //通过分割符拆分字符串为数组
                string[] splitvals = null;
                if (checkvals.Contains(","))
                {
                    splitvals = checkvals.Split(',');
                }
                else
                {
                    splitvals = new string[] { checkvals };
                }
                //循环panel自动为复选框赋值
                foreach (Control c in panels.Controls)
                {
                    if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = false;//默认不选中
                        foreach (string splitval in splitvals)
                        {
                            if (c.Text == splitval)
                            {
                                (c as CheckBox).Checked = true;
                                break;
                            }
                        }
                    }
                    else if (c is TextBox)
                    {
                        c.Text = "";
                        foreach (string splitval in splitvals)
                        {
                            if (splitval.Contains(":"))
                            {
                                c.Text = splitval.Replace(":", "");
                                break;
                            }
                        }
                    }
                    else if (c is ComboBox)
                    {
                        c.Text = "";
                        foreach (string splitval in splitvals)
                        {
                            if (splitval.Contains(":"))
                            {
                                c.Text = splitval.Replace(":", "");
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Control c in panels.Controls)
                {
                    if (c is CheckBox)
                    {
                        (c as CheckBox).Checked = false;
                    }
                    else if (c is TextBox)
                    {
                        c.Text = "";
                    }
                }
            }

        }

        public static int[] getAgeBytime(string birthtime, string nowtime)
        {
            CalculateAgeCls m_AgeCls = new CalculateAgeCls();
            DateTime dtbirth = Convert.ToDateTime(birthtime);
            DateTime nowdate = Convert.ToDateTime(nowtime);
            return m_AgeCls.GetAgeByBirthday(dtbirth, nowdate);
        }

        public static string getAgeStr(string birthdate)
        {
            CalculateAgeCls m_AgeCls = new CalculateAgeCls();
            DateTime dtbirth = Convert.ToDateTime(birthdate);
            return m_AgeCls.GetAgeByBirthdaystr(dtbirth, DateTime.Now);
        }

        public static bool IsDebug
        {
            get
            {
                var isDebug = System.Configuration.ConfigurationManager.AppSettings["IsDebug"];
                if (!string.IsNullOrEmpty(isDebug) && isDebug.ToUpper().Equals("TRUE"))
                {
                    return true;
                }

                return false;

            }
        }
    }


}
