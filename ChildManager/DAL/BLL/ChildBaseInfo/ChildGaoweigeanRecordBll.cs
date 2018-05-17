using System;
using ChildManager.DAL;
using System.Windows.Forms;
using ChildManager.Model.ChildBaseInfo;
using System.Data.SqlClient;

namespace ChildManager.BLL.ChildBaseInfo
{
    class ChildGaoweigeanRecordBll
    {
        DateLogic dg = new DateLogic();

        //保存
        public bool saveGaoweigeanrecord(ChildGaoweigeanRecordObj gaoweirecordobj)
        {
                try
                {
                    string sqls = "";
                    if (gaoweirecordobj.id != 0)
                    {
                        sqls = "update child_gaoweigean_record set "
                                + " childId = " + gaoweirecordobj.childId
                                + " ,checkid = " + gaoweirecordobj.checkid
                                + " ,pingufangfa = '" + gaoweirecordobj.pingufangfa+"'"
                                + " ,pingujieguo = '" + gaoweirecordobj.pingujieguo + "'"
                                + " ,zhidao = '" + gaoweirecordobj.zhidao + "'"
                                + " ,chuli = '" + gaoweirecordobj.chuli + "'"
                                + " where id= " + gaoweirecordobj.id;
                    }
                    else
                    {
                        sqls = "insert into child_gaoweigean_record ("
                                + "childId"
                                + ",checkid"
                                + ",pingufangfa"
                                + ",pingujieguo"
                                + ",zhidao"
                                + ",chuli"
                                + " ) values ( "
                                + "" + gaoweirecordobj.childId + ""
                                + "," + gaoweirecordobj.checkid + ""
                                + ",'" + gaoweirecordobj.pingufangfa + "'"
                                + ",'" + gaoweirecordobj.pingujieguo + "'"
                                + ",'" + gaoweirecordobj.zhidao + "'"
                                + ",'" + gaoweirecordobj.chuli + "'"
                                + ")";
                    }

                    if (dg.executeupdate(sqls) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败", "软件提示");
                    throw ex;
                }
        }


        public bool saveXinlixingweirecord(ChildGaoweigeanRecordObj gaoweirecordobj)
        {
            try
            {
                string sqls = "";
                if (gaoweirecordobj.id != 0)
                {
                    sqls = "update child_xinlixingwei_record set "
                            + " childId = " + gaoweirecordobj.childId
                            + " ,checkid = " + gaoweirecordobj.checkid
                            + " ,pingufangfa = '" + gaoweirecordobj.pingufangfa + "'"
                            + " ,pingujieguo = '" + gaoweirecordobj.pingujieguo + "'"
                            + " ,zhidao = '" + gaoweirecordobj.zhidao + "'"
                            + " ,chuli = '" + gaoweirecordobj.chuli + "'"
                            + " where id= " + gaoweirecordobj.id;
                }
                else
                {
                    sqls = "insert into child_xinlixingwei_record ("
                            + "childId"
                            + ",checkid"
                            + ",pingufangfa"
                            + ",pingujieguo"
                            + ",zhidao"
                            + ",chuli"
                            + " ) values ( "
                            + "" + gaoweirecordobj.childId + ""
                            + "," + gaoweirecordobj.checkid + ""
                            + ",'" + gaoweirecordobj.pingufangfa + "'"
                            + ",'" + gaoweirecordobj.pingujieguo + "'"
                            + ",'" + gaoweirecordobj.zhidao + "'"
                            + ",'" + gaoweirecordobj.chuli + "'"
                            + ")";
                }

                if (dg.executeupdate(sqls) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败", "软件提示");
                throw ex;
            }
        }

        public bool ifhascount(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader drs = null;
            try
            {
                drs = dg.executequery(sqls);

                if (drs.HasRows)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                drs.Close();
                dg.con_close();
            }

        }

    }
}
