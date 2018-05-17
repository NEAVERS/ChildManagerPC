using System;
using ChildManager.DAL;
using System.Windows.Forms;
using ChildManager.Model.ChildBaseInfo;
using System.Data.SqlClient;
using YCF.Common;

namespace ChildManager.BLL.ChildBaseInfo
{
    class ChildGaoweigeanBll
    {
        DateLogic dg = new DateLogic();

        public ChildGaoweigeanObj getGaoweigeanObj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            try
            {
                sdr = dg.executequery(sqls);
                ChildGaoweigeanObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    sdr.Read();
                    obj = new ChildGaoweigeanObj();
                    obj.id = Convert.ToInt32(sdr["id"]);
                    obj.childId = Convert.ToInt32(sdr["childId"]);
                    obj.jiwangshi = sdr["jiwangshi"].ToString();
                    obj.endtime = sdr["endtime"].ToString();
                    obj.managetime = sdr["managetime"].ToString();
                    obj.zhuangui = sdr["zhuangui"].ToString();
                    obj.zhuanzhendanwei = sdr["zhuanzhendanwei"].ToString();
                    obj.recordtime = sdr["recordtime"].ToString();
                    obj.recordname = sdr["recordname"].ToString();
                }
                return obj;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }

        public string getGaoweistr(int childid,string gaoweitime)
        {
            string sqls = "select * from tb_gaowei where childid=" + childid;
            if (!string.IsNullOrEmpty(gaoweitime))
            {
                sqls += " and recordtime='"+gaoweitime+"'";
            }
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            string gaoweilist = "";
            try
            {
                sdr = dg.executequery(sqls);
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        if (string.IsNullOrEmpty(gaoweilist))
                        {
                            gaoweilist += sdr["gaoweiyinsu"].ToString();
                        }
                        else
                        {
                            gaoweilist += ","+sdr["gaoweiyinsu"].ToString();
                        }
                    }
                }
                return gaoweilist;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }


        //保存
        public void saveOrdeleteGaowei(string gaoweistr,string gaoweitime)
        {
            if (!String.IsNullOrEmpty(gaoweistr))
            {
                try
                {
                    string delsqlstr = "select * from tb_gaowei where gaoweiyinsu not in(" + gaoweistr + ") and childId= " + globalInfoClass.Wm_Index;
                    SqlDataReader sdr = dg.executequery(delsqlstr);
                    if (sdr.HasRows)
                    {
                        string delstr = "delete from tb_gaowei where gaoweiyinsu not in(" + gaoweistr + ") and childId= " + globalInfoClass.Wm_Index;
                        while (sdr.Read())
                        {
                            string insertrecordsql = "insert into tb_gaowei_record ("
                                            + "childId"
                                            + ",gaoweiyinsu"
                                            + ",recordtime"
                                            + ",status"
                                            + ",doctorName"
                                            + " ) values ( "
                                            + "," + globalInfoClass.Wm_Index + ""
                                            + ",'" + sdr["gaoweiyinsu"] + "'"
                                            + ",'" + gaoweitime + "'"
                                            + ",'取消'"
                                            + ",'" + globalInfoClass.UserName + "'"
                                            + ")";
                            dg.executeupdate(insertrecordsql);
                        }
                    }

                    string[] gaoweistrargs = gaoweistr.Split(',');
                    for (int i = 0; i < gaoweistrargs.Length; i++)
                    {
                        string selstr = "select * from tb_gaowei where gaoweiyinsu ='" + gaoweistrargs[i] + "' and childId= " + globalInfoClass.Wm_Index;
                        if (ifhascount(selstr))
                        {
                            string insertsql = "insert into tb_gaowei ("
                                            + "childId"
                                            + ",gaoweiyinsu"
                                            + ",recordtime"
                                            + ",type"
                                            + ",doctorName"
                                            + " ) values ( "
                                            + "," + globalInfoClass.Wm_Index + ""
                                            + ",'" + gaoweistrargs[i] + "'"
                                            + ",'" + gaoweitime + "'"
                                            + ",'" + getgaoweitype(gaoweistrargs[i]) + "'"
                                            + ",'" + globalInfoClass.UserName + "'"
                                            + ")";
                            dg.executeupdate(insertsql);
                            string insertrecordsql = "insert into tb_gaowei_record ("
                                            + "childId"
                                            + ",gaoweiyinsu"
                                            + ",recordtime"
                                            + ",status"
                                            + ",doctorName"
                                            + " ) values ( "
                                            + "," + globalInfoClass.Wm_Index + ""
                                            + ",'" + gaoweistrargs[i] + "'"
                                            + ",'" + gaoweitime + "'"
                                            + ",'新增'"
                                            + ",'" + globalInfoClass.UserName + "'"
                                            + ")";
                            dg.executeupdate(insertrecordsql);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存失败", "软件提示");
                    throw ex;
                }
            }
        }

        public string getgaoweitype(string gaoweiyinsu)
        {
            string type = "高危";
            string gaoweilist = "新生儿住院,运动发育障碍,语言问题,其他发育行为问题,ADHD";
            string jibinglist = "生长迟缓,营养不良,肥胖,贫血,佝偻病";
            if (gaoweilist.Contains(gaoweiyinsu))
            {
                type = "高危";
            }
            else
            {
                type = "营养不良";
            }
            return type;
        }

        //保存
        public bool saveGaoweigeanObj(ChildGaoweigeanObj gaoweigeanobj)
        {
            try
            {
                string selstr = "select * from child_gaoweigean where childid="+gaoweigeanobj.childId;
                string sqls = "";
                if (ifhascount(selstr))
                {
                    sqls = "insert into child_gaoweigean ("
                            + "childId"
                            + ",jiwangshi"
                            + ",endtime"
                            + ",managetime"
                            + ",zhuangui"
                            + ",zhuanzhendanwei"
                            + ",recordtime"
                            + ",recordname"
                            + " ) values ( "
                            + "" + gaoweigeanobj.childId + ""
                            + ",'" + gaoweigeanobj.jiwangshi + "'"
                            + ",'" + gaoweigeanobj.endtime + "'"
                            + ",'" + gaoweigeanobj.managetime + "'"
                            + ",'" + gaoweigeanobj.zhuangui + "'"
                            + ",'" + gaoweigeanobj.zhuanzhendanwei + "'"
                            + ",'" + gaoweigeanobj.recordtime + "'"
                            + ",'" + gaoweigeanobj.recordname + "'"
                            + ")";
                }
                else
                {
                    sqls = "update child_gaoweigean set "
                            + " jiwangshi = '" + gaoweigeanobj.jiwangshi + "'"
                            + " ,endtime = '" + gaoweigeanobj.endtime + "'"
                            + " ,managetime = '" + gaoweigeanobj.managetime + "'"
                            + " ,zhuangui = '" + gaoweigeanobj.zhuangui + "'"
                            + " ,zhuanzhendanwei = '" + gaoweigeanobj.zhuanzhendanwei + "'"
                            + " ,recordtime = '" + gaoweigeanobj.recordtime + "'"
                            + " ,recordname = '" + gaoweigeanobj.recordname + "'"
                            + " where childId= " + gaoweigeanobj.childId;
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


        public bool savexinlixingweigeanObj(ChildGaoweigeanObj gaoweigeanobj)
        {
            try
            {
                string selstr = "select * from child_xinlixingwei where childid=" + gaoweigeanobj.childId;
                string sqls = "";
                if (ifhascount(selstr))
                {
                    sqls = "insert into child_xinlixingwei ("
                            + "childId"
                            + ",jiwangshi"
                            + ",endtime"
                            + ",managetime"
                            + ",zhuangui"
                            + ",zhuanzhendanwei"
                            + ",recordtime"
                            + ",recordname"
                            + " ) values ( "
                            + "" + gaoweigeanobj.childId + ""
                            + ",'" + gaoweigeanobj.jiwangshi + "'"
                            + ",'" + gaoweigeanobj.endtime + "'"
                            + ",'" + gaoweigeanobj.managetime + "'"
                            + ",'" + gaoweigeanobj.zhuangui + "'"
                            + ",'" + gaoweigeanobj.zhuanzhendanwei + "'"
                            + ",'" + gaoweigeanobj.recordtime + "'"
                            + ",'" + gaoweigeanobj.recordname + "'"
                            + ")";

                }
                else
                {
                    sqls = "update child_xinlixingwei set "
                            + " jiwangshi = '" + gaoweigeanobj.jiwangshi + "'"
                            + " ,endtime = '" + gaoweigeanobj.endtime + "'"
                            + " ,managetime = '" + gaoweigeanobj.managetime + "'"
                            + " ,zhuangui = '" + gaoweigeanobj.zhuangui + "'"
                            + " ,zhuanzhendanwei = '" + gaoweigeanobj.zhuanzhendanwei + "'"
                            + " ,recordtime = '" + gaoweigeanobj.recordtime + "'"
                            + " ,recordname = '" + gaoweigeanobj.recordname + "'"
                            + " where childId= " + gaoweigeanobj.childId;
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
