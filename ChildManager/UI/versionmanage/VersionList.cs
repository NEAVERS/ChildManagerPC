using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using login.BLL;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using login.Model;
using ChildManager.DAL;

namespace ChildManager.UI.versionmanage
{
    public partial class VersionList : UserControl
    {
        private UpdateVersionbll bll = new UpdateVersionbll();
        public VersionList()
        {
            InitializeComponent();
            this.dataGridView1.Columns[0].ReadOnly = true;
            this.dataGridView1.Columns[1].ReadOnly = true;
            this.dataGridView1.Columns[3].ReadOnly = true;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            RefreshRecordList();

        }

        private void RefreshRecordList()
        {
            dataGridView1.Rows.Clear();
            string sqls = "select id,version,updatetime,isfabu,updatecontent from updateversion";
            ArrayList list = bll.getUpdateVersionList(sqls);
            foreach (UpdateVersionobj obj in list)
            {
                DataGridViewRow row = new DataGridViewRow();
                //Image image = new Image();
                if (obj.isfabu == 1)
                {
                    row.CreateCells(dataGridView1, ChildManager.Properties.Resources.ok, obj.ID, obj.version, obj.updatetime, obj.updatecontent);// 
                }
                else
                {
                    row.CreateCells(dataGridView1, ChildManager.Properties.Resources.nothing, obj.ID, obj.version, obj.updatetime, obj.updatecontent);// 
                }

                row.Tag = obj;
                //System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle105 = new System.Windows.Forms.DataGridViewCellStyle();
                //dataGridViewCellStyle105.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
                //dataGridViewCellStyle105.BackColor = Color.Pink;
                //dataGridViewCellStyle105.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //dataGridViewCellStyle105.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(104)))), ((int)(((byte)(152)))));
                //dataGridViewCellStyle105.SelectionBackColor = System.Drawing.SystemColors.Highlight;
                //dataGridViewCellStyle105.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                //dataGridViewCellStyle105.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
                //row.DefaultCellStyle = dataGridViewCellStyle105;
                dataGridView1.Rows.Add(row);
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            string sqls = getInsertSql();
            if (bll.saverecord(sqls))
            {
                RefreshRecordList();
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //string sqls = getUpdateSql();
            //if (bll.updaterecord(sqls) > 0)
            //{
            //    RefreshRecordList();
            //}
            //else
            //{
            //    MessageBox.Show("保存失败！");
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Tag != "")
                {
                    UpdateVersionobj versionobj = dataGridView1.SelectedRows[0].Tag as UpdateVersionobj;

                    if (MessageBox.Show("删除所选的记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        try
                        {
                            string sqls = "delete from updateversion where id=" + versionobj.id + "";
                            if (bll.deleterecord(sqls) > 0)
                            {

                                RefreshRecordList();
                            }
                            else
                            {
                                MessageBox.Show("删除失败!", "请联系管理员");
                            }

                        }
                        finally
                        {
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要的删除的行！", "系统提示");
            }
        }


        private string getInsertSql()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  insert into updateversion (");
            sb.Append("  version");
            sb.Append("  ,updatetime");
            sb.Append("  ,updatecontent");
            sb.Append("  ,isfabu");
            sb.Append("  ) values (");
            sb.Append("  ''");
            sb.Append("  ,''");
            sb.Append("  ,''");
            sb.Append("  ,0");
            sb.Append("  )");

            return sb.ToString();
        }

        private string getUpdateSql(UpdateVersionobj versionobj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("  update  updateversion set ");
            sb.Append("  version = '" + versionobj.version + "'");
            sb.Append("  ,updatecontent = '" + versionobj.updatecontent + "'");
            sb.Append("  where id=" + versionobj.id);

            return sb.ToString();
        }


        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            UpdateVersionobj versionobj = dataGridView1.Rows[e.RowIndex].Tag as UpdateVersionobj;
            versionobj.version = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            versionobj.updatecontent = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            string sqls = getUpdateSql(versionobj);
            if (bll.updaterecord(sqls) > 0)
            {
                //RefreshRecordList();
            }
            else
            {
                MessageBox.Show("保存失败");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Tag != "")
                {
                    UpdateVersionobj versionobj = dataGridView1.SelectedRows[0].Tag as UpdateVersionobj;

                    //if (MessageBox.Show("确定要上传更新包吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    //{
                    Cursor.Current = Cursors.WaitCursor;
                    OpenFileDialog filename = new OpenFileDialog();
                    filename.InitialDirectory = Application.StartupPath;
                    filename.Filter = "压缩文件|*.zip";
                    filename.RestoreDirectory = true;
                    if (filename.ShowDialog() == DialogResult.OK)
                    {
                        string path = filename.FileName.ToString();
                        //string name = path.Substring(path.LastIndexOf("\\")+1);

                        //保存文件到SQL Server数据库中
                        FileInfo fi = new FileInfo(path);
                        FileStream fs = fi.OpenRead();
                        byte[] bytes = new byte[fs.Length];
                        fs.Read(bytes, 0, Convert.ToInt32(fs.Length));

                        DateLogic dg = new DateLogic();
                        SqlConnection con = dg.getconn();

                        SqlCommand cm = new SqlCommand("update updateversion set  updateprogram =@updateprogram,updatetime=@updatetime where id=@id", con);


                        SqlParameter spFile = new SqlParameter("@updateprogram", SqlDbType.Image);
                        SqlParameter spupdatetime = new SqlParameter("@updatetime", SqlDbType.Char);
                        SqlParameter spid = new SqlParameter("@id", SqlDbType.Char);
                        spFile.Value = bytes;
                        spupdatetime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        spid.Value = versionobj.id;
                        cm.Parameters.Add(spFile);
                        cm.Parameters.Add(spupdatetime);
                        cm.Parameters.Add(spid);
                        cm.ExecuteNonQuery();
                        RefreshRecordList();
                    }
                    //}
                }
            }
            else
            {
                MessageBox.Show("请选择要上传的版本！", "系统提示");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlDataReader dr = null;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Tag != "")
                {
                    UpdateVersionobj versionobj = dataGridView1.SelectedRows[0].Tag as UpdateVersionobj;

                    if (String.IsNullOrEmpty(versionobj.updatetime))
                    {
                        MessageBox.Show("请先上传更新包");
                        return;
                    }
                    if (String.IsNullOrEmpty(versionobj.version))
                    {
                        MessageBox.Show("请填写该更新包的版本号");
                        return;
                    }

                    if (MessageBox.Show("确定要发布该更新吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        string updatestr = "update updateversion set isfabu=0 where id!=" + versionobj.id + ";update updateversion set isfabu=1 where id=" + versionobj.id;
                        if (bll.updaterecord(updatestr) > 0)
                        {
                            MessageBox.Show("发布成功");
                            RefreshRecordList();
                        }
                        else
                        {
                            MessageBox.Show("发布失败");
                        }


                    }
                }
            }
        }
    }
}
