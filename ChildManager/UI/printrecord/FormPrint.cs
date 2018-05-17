using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChildManager.UI.printrecord
{
    public partial class FormPrint : Form
    {
        public FormPrint()
        {
            InitializeComponent();
        }

        private void FormPrint_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("姓名", typeof(string));
            table.Columns.Add("性别", typeof(string));
            table.Columns.Add("年龄", typeof(string));
            table.Columns.Add("出生日期", typeof(string));
            table.Columns.Add("喂养指导", typeof(string));
            table.Columns.Add("早教指导", typeof(string));
            table.Rows.Add("张三", "男", "8月", "2016-08-07", "1-2个月，吃饭看到就分手快发来的时刻吃饭看到就\r\n分手快发来的时刻将飞吃饭看到就分手快发\r\n来的时刻将飞机上的浪费时间的离开\r\n房间的数量看风景的吃\r\n饭看到就分手快发来的时刻将飞机上的浪费时间\r\n的离开房间的数量看风景的实\r\n际发生了地实际发生吃饭看到就分手快发来的时刻将\r\n飞机上的浪费时间的离开房\r\n间的数量看风景的实\r\n际发生了地了地机上的浪费时间的离开房间的数量看风景的实际发生了地将飞机\r\n上的浪费时间的离开房间的数量看风景的实际发生了地方", "1-2个月，饭看到就分手快发来的时刻吃饭看\r\n到就分手快发来的时刻将飞吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地实际发生吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地了地机上的浪费时间的离开房间的数量看风景的实际发生了地将飞机上的浪费时间的离饭看到就分手快发来的时刻吃饭看到就分手快发来的时刻将飞吃饭\r\n看到就分手快发来的时刻将飞机上的浪费时间的离开房间\r\n的数量看风景的吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地实际发生吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地了地机上的浪费时间的离开房间的数量看风景的实际发生了地将飞机上的浪费时间的离饭看到就分手快发来的时刻吃饭看到就分手快发来的时刻将飞吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地实际发生吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地了地机上的浪费时间的离开房间的数量看风景的实际发生了地将飞机上的浪费时间的离饭看到就分手快发来的时刻吃饭看到就分手快发来的时刻将飞吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地实际发生吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地了地机上的浪费时间的离开房间的数量看风景的实际发生了地将飞机上的浪费时间的离饭看到就分手快发来的时刻吃饭看到就分手快发来的时刻将飞吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地实际发生吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地了地机上的浪费时间的离开房间的数量看风景的实际发生了地将飞机上的浪费时间的离饭看到就分手快发来的时刻吃饭看到就分手快发来的时刻将飞吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地实际发生吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地了地机上的浪费时间的离开房间的数量看风景的实际发生了地将飞机上的浪费时间的离饭看到就分手快发来的时刻吃饭看到就分手快发来的时刻将飞吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的吃饭看到就分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景的实际发生了地实际发生吃饭看到就\r\n分手快发来的时刻将飞机上的浪费时间的离开房间的数量看风景\r\n的实际发生了地了地机上的浪费时间的离开房间的数量看风景的实际发生了地将飞机上的浪费时间的离");

            ///---添加数据源  
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "DataSet1";
            rds.Value = table;
            ///---向报表绑定数据源  
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            ///---向报表查看器指定显示的报表  
            
            
            //this.reportViewer1.LocalReport.ReportPath = "Report1.rdlc";
            reportViewer1.LocalReport.ReportEmbeddedResource = "ChildManager.UI.printrecord.Report1.rdlc";
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.RefreshReport();
        }
    }
}
