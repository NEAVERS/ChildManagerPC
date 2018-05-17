using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace YCF.Common
{
    public class ExcelHelper
    {
        private readonly string connectionString;//连接字符串
        public OleDbConnection odc;//连接对象

        /// <summary>
        /// 不指定文件路径，只能写入
        /// </summary>
        public ExcelHelper()
        {

        }

        /// <summary>
        /// 初始化一个Excel操作实例
        /// <para>请注意,本连接字符串不支持office2010,如需支持,请自行更改连接字符串格式</para>
        /// </summary>
        /// <param name="pathString">请提供一个Excel文件路径,无论是已创建的或者是未创建的</param>
        public ExcelHelper(string pathString)
        {
            this.connectionString = "provider=Microsoft.Jet.OLEDB.4.0;data source=" +
                pathString +
                    ";Extended Properties='Excel 8.0;HDR=NO;';Persist Security Info=False;";
        }



        /// <summary>
        /// 返回一个Excel文档中的所有数据
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public DataSet ReadExcel(string sql)
        {
            using (odc = new OleDbConnection(connectionString))
            {
                DataSet ds = new DataSet();
                odc.Open();
                OleDbDataAdapter oda = new OleDbDataAdapter(sql, odc);
                oda.Fill(ds);

                return ds;
            }
        }

        /// <summary>
        /// 返回一个Excel文档中第一个Sheet档的Sheet数据
        /// </summary>
        /// <returns></returns>
        public DataTable ReadExcel()
        {
            using (var odc = new OleDbConnection(connectionString))
            {
                odc.Open();
                DataTable dt = odc.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                    new object[] { null, null, null, "TABLE" });

                return dt;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, params OleDbParameter[] prms)
        {
            using (var odc = new OleDbConnection(connectionString))
            {
                odc.Open();
                using (var cmd = new OleDbCommand(sql, odc))
                {
                    if (prms != null && prms.Length > 0)
                    {
                        cmd.Parameters.AddRange(prms);
                    }

                    try
                    {
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
            }

        }

        /// <summary>
        /// 打开一个已存在的Excel文档,并写入数据集
        /// <para>请注意,Excel文档内的多个Sheet有可能被覆盖</para>
        /// <para>原因:Sheet名相同</para>
        /// </summary>
        /// <param name="ds">数据集,至少包含一个或多个表(Sheet)</param>
        /// <param name="path">请提供一个Excel文件路径,如果不存在则自动创建</param>
        /// <returns></returns>
        public bool WriteExcel(DataSet ds, string path)
        {

            if (!File.Exists(path))
            {
                FileStream fs = File.Create(path);
                fs.Dispose();
                fs.Close();
            }
            bool result = true;
            Microsoft.Office.Interop.Excel.Application excel =
                new Microsoft.Office.Interop.Excel.Application();//创建Excel操作对象
            excel.Workbooks.Open(path);
            try
            {
                excel.SheetsInNewWorkbook = ds.Tables.Count;//创建sheet的数量
                excel.Workbooks.Add();//添加sheet
                for (int number = 0; number < ds.Tables.Count; number++)
                {
                    Microsoft.Office.Interop.Excel.Worksheet sheet =
                        excel.ActiveWorkbook.Worksheets[number + 1] as
                            Microsoft.Office.Interop.Excel.Worksheet;//获取sheet;
                    DataTable dt = ds.Tables[number];//获取表
                    sheet.Name = dt.TableName;//设置sheet名
                    int i = 0;
                    for (; i < dt.Columns.Count; i++)//动态添加
                    {
                        sheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;//添加表头
                    }
                    Microsoft.Office.Interop.Excel.Range range =
                        excel.get_Range(excel.Cells[1, 1], excel.Cells[1, i]);//编辑区域
                    range.Font.Bold = true;//字体加粗
                    range.Font.Color = 0;//字体颜色
                    range.Interior.ColorIndex = 15;
                    range.ColumnWidth = 15;//列宽
                    range.Borders.LineStyle =
                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;//边框样式
                    for (int y = 0; y < dt.Rows.Count; y++)//动态添加行数据
                    {
                        for (int x = 0; x < dt.Rows[y].Table.Columns.Count; x++)//动态添加列数据
                        {
                            sheet.Cells[y + 2, x + 1] = dt.Rows[y][x];//赋值
                        }
                    }
                }
                excel.Visible = false;//显示预览
                //System.Threading.Thread.Sleep(5000);
                excel.ActiveWorkbook.Save();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = false;
            }
            finally
            {
                excel.ActiveWorkbook.Close();//关闭Excel对象
                excel.Quit();//退出
                KillEXCELProcess();
            }
            return result;
        }

        /// <summary>
        /// 创建一个新的Excel文件,并写入数据集
        /// <para>请注意,Excel文件可能创建失败</para>
        /// <para>原因:指定Excel文件已存在</para>
        /// </summary>
        /// <param name="ds">数据集,至少包含一个或多个表(Sheet)</param>
        /// <param name="path">请提供一个Excel文件路径,如果不存在则自动创建</param>
        /// <returns></returns>
        public static bool CreateExcel(DataSet ds, string path)
        {
            bool result = true;
            Microsoft.Office.Interop.Excel.Application excel =
                new Microsoft.Office.Interop.Excel.Application();//create Excel manipulate objects
            try
            {
                excel.SheetsInNewWorkbook = ds.Tables.Count;//获取Sheet档数量
                excel.Workbooks.Add();
                for (int number = 0; number < ds.Tables.Count; number++)//循环添加Sheet档
                {
                    Microsoft.Office.Interop.Excel.Worksheet sheet =
                        excel.ActiveWorkbook.Worksheets[number + 1] as
                            Microsoft.Office.Interop.Excel.Worksheet;//获取sheet;
                    DataTable dt = ds.Tables[number];//获取表
                    sheet.Name = dt.TableName;//设置Sheet档名
                    int i = 0;
                    for (; i < dt.Columns.Count; i++)//循环添加列头
                    {
                        sheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;//设置列头名
                    }
                    Microsoft.Office.Interop.Excel.Range range =
                        excel.get_Range(excel.Cells[1, 1], excel.Cells[1, i]);//设置编辑区域
                    range.Font.Bold = true;//字体加粗
                    range.Font.ColorIndex = 0;//字体颜色
                    range.Interior.ColorIndex = 15;//背景颜色
                    range.ColumnWidth = 15;//列宽
                    range.Borders.LineStyle =
                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;//边框样式
                    range.HorizontalAlignment =
                        Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//字体居中
                    for (int y = 0; y < dt.Rows.Count; y++)//循环添加行
                    {
                        for (int x = 0; x < dt.Rows[y].Table.Columns.Count; x++)//循环添加列
                        {
                            sheet.Cells[y + 2, x + 1] = dt.Rows[y][x];//设置列值
                        }
                    }
                }
                excel.Visible = false;//设置为预览
                //System.Threading.Thread.Sleep(5000);//线程延迟5秒再预览
                excel.ActiveWorkbook.SaveAs(path,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);//另存为
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = false;
            }
            finally
            {
                excel.ActiveWorkbook.Close();//关闭Excel对象
                excel.Quit();//退出
                KillEXCELProcess();
            }
            return result;
        }

        /// <summary>
        /// 创建一个新的Excel文件,并写入数据集
        /// <para>请注意,Excel文件可能创建失败</para>
        /// <para>原因:指定Excel文件已存在</para>
        /// </summary>
        /// <param name="ds">数据集,至少包含一个或多个表(Sheet)</param>
        /// <param name="path">请提供一个Excel文件路径,如果不存在则自动创建</param>
        /// <param name="templatePath">请提供一个Excel模板文件路径,如果不存在则传入null</param>
        /// <returns></returns>
        public static bool CreateExcel(DataGridView dgv, string path, string templatePath)
        {
            bool result = true;
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();//create Excel manipulate objects

            bool hasTemplate = false;
            if (!string.IsNullOrEmpty(templatePath) && File.Exists(templatePath = Application.StartupPath + "\\" + templatePath))
            {
                File.Copy(templatePath, path, true);
                hasTemplate = true;
                excel.Workbooks.Open(path);
            }
            else
            {
                excel.Workbooks.Add();
            }

            try
            {
                var sheet = excel.ActiveWorkbook.Worksheets[1] as
                    Microsoft.Office.Interop.Excel.Worksheet;//获取sheet;

                var rowCount = sheet.UsedRange.Rows.Count;
                excel.Cells.NumberFormatLocal = "@";

                if (!hasTemplate)
                {
                    int i = 0;
                    for (; i < dgv.ColumnCount; i++)//循环添加列头
                    {
                        sheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;//设置列头名
                    }
                    Microsoft.Office.Interop.Excel.Range range =
                        excel.Range[excel.Cells[1, 1], excel.Cells[1, i]];//设置编辑区域
                    range.Font.Bold = true;//字体加粗
                                           //range.Font.ColorIndex = 0;//字体颜色
                                           //range.Interior.ColorIndex = 15;//背景颜色
                    range.ColumnWidth = 15;//列宽
                    range.Borders.LineStyle =
                        Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;//边框样式
                    range.HorizontalAlignment =
                        Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//字体居中
                }

                var objData = new object[dgv.RowCount, dgv.ColumnCount];

                for (int y = 0; y < dgv.RowCount; y++)//循环添加行
                {
                    for (int x = 0; x < dgv.ColumnCount; x++)//循环添加列
                    {
                        objData[y, x] = dgv[x, y].Value?.ToString() ?? "";
                        //sheet.Cells[y + rowCount + 1, x + 1] = dgv[x, y].Value?.ToString() ?? "";//设置列值
                    }
                }

                var contentRange = sheet.Range[excel.Cells[rowCount + 1, 1], excel.Cells[rowCount + dgv.RowCount, dgv.ColumnCount]] as Microsoft.Office.Interop.Excel.Range;

                contentRange.Value2 = objData;

                excel.Visible = false;//设置为预览
                                      //System.Threading.Thread.Sleep(5000);//线程延迟5秒再预览
                if (hasTemplate)
                {
                    excel.ActiveWorkbook.Save();
                }
                else
                {
                    excel.ActiveWorkbook.SaveAs(path,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);//另存为
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                result = false;
            }
            finally
            {
                excel?.ActiveWorkbook?.Close();//关闭Excel对象
                excel?.Quit();//退出
                KillEXCELProcess();
            }
            return result;
        }


        /// <summary>
        /// 添加1分钟， 5分钟， 10分钟3列，返回添加之前的总列数
        /// </summary>
        /// <param name="pathString"></param>
        /// <returns></returns>
        public static void AddScorceColumn(string pathString, int headerRowIndex, int leftBlankColumnCount, out string babyOneScoreColumnName, out string babyFiveScoreColumnName, out string babyTenScoreColumnName)
        {
            Microsoft.Office.Interop.Excel.Application excel =
                new Microsoft.Office.Interop.Excel.Application();//创建Excel操作对象
            excel.Workbooks.Open(pathString);

            try
            {
                var workSheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;

                var columnCount = workSheet.UsedRange.Columns.Count;
                var rowCount = workSheet.UsedRange.Rows.Count;

                babyOneScoreColumnName = "F" + (columnCount + 1);
                babyFiveScoreColumnName = "F" + (columnCount + 2);
                babyTenScoreColumnName = "F" + (columnCount + 3);

                workSheet.Cells[headerRowIndex, columnCount + leftBlankColumnCount + 1] = "1分钟";
                workSheet.Cells[headerRowIndex, columnCount + leftBlankColumnCount + 2] = "5分钟";
                workSheet.Cells[headerRowIndex, columnCount + leftBlankColumnCount + 3] = "10分钟";

                var oldHeaderRange = workSheet.Cells[headerRowIndex, columnCount + leftBlankColumnCount] as Microsoft.Office.Interop.Excel.Range;
                var oldContentRange = workSheet.Cells[headerRowIndex + 1, columnCount + leftBlankColumnCount] as Microsoft.Office.Interop.Excel.Range;
                var scoreHeaderRange = workSheet.Range[workSheet.Cells[headerRowIndex, columnCount + leftBlankColumnCount + 1], workSheet.Cells[headerRowIndex, columnCount + leftBlankColumnCount + 3]] as Microsoft.Office.Interop.Excel.Range;
                var scoreContentRange = workSheet.Range[workSheet.Cells[headerRowIndex + 1, columnCount + leftBlankColumnCount + 1], workSheet.Cells[rowCount, columnCount + leftBlankColumnCount + 3]] as Microsoft.Office.Interop.Excel.Range;

                //标题格式
                scoreHeaderRange.Font.Background = oldHeaderRange.Font.Background;
                scoreHeaderRange.Font.Bold = oldHeaderRange.Font.Bold;
                scoreHeaderRange.Font.Color = oldHeaderRange.Font.Color;
                scoreHeaderRange.Font.FontStyle = oldHeaderRange.Font.FontStyle;
                scoreHeaderRange.Font.Name = oldHeaderRange.Font.Name;
                scoreHeaderRange.Font.Italic = oldHeaderRange.Font.Italic;
                scoreHeaderRange.Font.OutlineFont = oldHeaderRange.Font.OutlineFont;
                scoreHeaderRange.Font.Size = oldHeaderRange.Font.Size;
                scoreHeaderRange.Font.Strikethrough = oldHeaderRange.Font.Strikethrough;
                scoreHeaderRange.Font.Subscript = oldHeaderRange.Font.Subscript;
                scoreHeaderRange.Font.Superscript = oldHeaderRange.Font.Superscript;
                scoreHeaderRange.Font.Underline = oldHeaderRange.Font.Underline;

                scoreHeaderRange.Borders.Color = oldHeaderRange.Borders.Color;
                scoreHeaderRange.Borders.ColorIndex = oldHeaderRange.Borders.ColorIndex;
                scoreHeaderRange.Borders.LineStyle = oldHeaderRange.Borders.LineStyle;
                scoreHeaderRange.Borders.Weight = oldHeaderRange.Borders.Weight;

                scoreHeaderRange.WrapText = oldHeaderRange.WrapText;

                scoreHeaderRange.HorizontalAlignment = oldHeaderRange.HorizontalAlignment;
                scoreHeaderRange.VerticalAlignment = oldHeaderRange.VerticalAlignment;

                scoreHeaderRange.ColumnWidth = oldHeaderRange.ColumnWidth;

                //内容格式
                scoreContentRange.Font.Background = oldContentRange.Font.Background;
                scoreContentRange.Font.Bold = oldContentRange.Font.Bold;
                scoreContentRange.Font.Color = oldContentRange.Font.Color;
                scoreContentRange.Font.FontStyle = oldContentRange.Font.FontStyle;
                scoreContentRange.Font.Name = oldContentRange.Font.Name;
                scoreContentRange.Font.Italic = oldContentRange.Font.Italic;
                scoreContentRange.Font.OutlineFont = oldContentRange.Font.OutlineFont;
                scoreContentRange.Font.Size = oldContentRange.Font.Size;
                scoreContentRange.Font.Strikethrough = oldContentRange.Font.Strikethrough;
                scoreContentRange.Font.Subscript = oldContentRange.Font.Subscript;
                scoreContentRange.Font.Superscript = oldContentRange.Font.Superscript;
                scoreContentRange.Font.Underline = oldContentRange.Font.Underline;

                scoreContentRange.Borders.Color = oldContentRange.Borders.Color;
                scoreContentRange.Borders.ColorIndex = oldContentRange.Borders.ColorIndex;
                scoreContentRange.Borders.LineStyle = oldContentRange.Borders.LineStyle;
                scoreContentRange.Borders.Weight = oldContentRange.Borders.Weight;

                scoreContentRange.WrapText = oldContentRange.WrapText;

                scoreContentRange.HorizontalAlignment = oldContentRange.HorizontalAlignment;
                scoreContentRange.VerticalAlignment = oldContentRange.VerticalAlignment;

                //scoreContentRange.ColumnWidth = oldContentRange.ColumnWidth;

                excel.ActiveWorkbook.Save();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                excel.ActiveWorkbook.Close();//关闭Excel对象
                excel.Quit();//退出
                KillEXCELProcess();
            }
        }

        public static int GetLastUsedRangeColumnIndex(string pathString)
        {
            Microsoft.Office.Interop.Excel.Application excel =
                new Microsoft.Office.Interop.Excel.Application();//创建Excel操作对象
            excel.Workbooks.Open(pathString);

            try
            {
                var workSheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;

                return workSheet.UsedRange.Columns.Count;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                excel.ActiveWorkbook.Close();//关闭Excel对象
                excel.Quit();//退出
                KillEXCELProcess();
            }
        }

        public static void KillEXCELProcess()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process process in processes)
            {
                bool b = process.MainWindowTitle == "";
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                }
            }
        }
    }
}