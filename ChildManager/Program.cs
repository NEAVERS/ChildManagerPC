using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using ChildManager.UI;
using System.Data.Entity;
using ChildManager.UI.printrecord;
using ChildManager.UI.cepingshi;

namespace ChildManager
{
    static class Program
    {
        static System.Threading.Thread thread = null;
        /// <summary>
        /// 儿童保健管理系统主入口
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //设置应用程序处理异常方式：ThreadException处理
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                thread = new System.Threading.Thread(InitEF);
                thread.Start();
                //Application.Run(new FormPrint());
                //return;

                ChildManager.UI.login logs = new ChildManager.UI.login();
                if (logs.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new frmMain());
                }
            }
            catch (Exception ex)
            {
                string str1 = GetExceptionMsg(ex, string.Empty);
                string str = GetExceptionMsgInfo(ex, string.Empty);

                MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                writeLog(str1);
            }

        }
        /// <summary>
        /// 为EF刚开始运行做处理,以防止第一次使用时访问太慢的情况
        /// </summary>
        private static void InitEF()
        {
            try
            {
                Database.SetInitializer<YCF.Model.Entities>(null);

                using (var db = new YCF.Model.Entities(YCF.Common.CommonHelper.EFConnentionString))
                {
                    var objectContext = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
                    var mappingCollection = (System.Data.Entity.Core.Mapping.StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(System.Data.Entity.Core.Metadata.Edm.DataSpace.CSSpace);
                    mappingCollection.GenerateViews(new List<System.Data.Entity.Core.Metadata.Edm.EdmSchemaError>());

                    db.SYS_MENUS.Count(t => t.id == 0);
                }

            }
            catch (Exception e)
            {
                string str1 = null;
                string str = null;

                do
                {
                    str1 = GetExceptionMsg(e, string.Empty);
                    str = GetExceptionMsgInfo(e, string.Empty);
                    writeLog(str1);

                    if (YCF.Common.CommonHelper.IsDebug)
                    {
                        MessageBox.Show(str1);
                    }

                    e = e.InnerException;
                } while (e != null);

                MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally
            {
                if (thread.IsAlive)
                {
                    thread.Abort();
                }
            }

        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            string str1 = null;
            string str = null;

            var ex = e.Exception;
            do
            {
                str1 = GetExceptionMsg(ex, e.ToString());
                str = GetExceptionMsgInfo(ex, e.ToString());
                writeLog(str1);

                if (YCF.Common.CommonHelper.IsDebug)
                {
                    MessageBox.Show(str1);
                }

                ex = ex.InnerException;
            } while (ex != null);

            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str1 = null;
            string str = null;

            var ex = e.ExceptionObject as Exception;
            do
            {
                str1 = GetExceptionMsg(ex, e.ToString());
                str = GetExceptionMsgInfo(ex, e.ToString());
                writeLog(str1);

                if (YCF.Common.CommonHelper.IsDebug)
                {
                    MessageBox.Show(str1);
                }

                ex = ex.InnerException;
            } while (ex != null);

            MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);

                sb.AppendLine("【异常方法】：" + ex.TargetSite);

            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }
        static string GetExceptionMsgInfo(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("系统异常，请联系管理员,错误信息:" + ex.ToString());
            return sb.ToString();

        }
        static void writeLog(string str)
        {
            if (!Directory.Exists("jianyiLog"))
            {
                Directory.CreateDirectory("jianyiLog");
            }

            using (StreamWriter sw = new StreamWriter(@"jianyiLog\ErrLog.txt", true))
            {

                sw.WriteLine(str);
                sw.Close();

            }

        }

        /// <summary>
        /// 为EF刚开始运行做处理,以防止第一次使用时访问太慢的情况
        /// </summary>
    }
}
