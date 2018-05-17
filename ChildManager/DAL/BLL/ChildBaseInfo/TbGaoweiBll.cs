using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChildManager.DAL;
using System.Windows.Forms;
using ChildManager.Model.ChildBaseInfo;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using YCF.Common;
using ChildManager.Model;

namespace ChildManager.BLL.ChildBaseInfo
{
    class TbGaoweiBll
    {
        DateLogic dg = new DateLogic();
        private ProjectGaoweibll gwbll = new ProjectGaoweibll();
        public ArrayList getGaoweilist(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                TbGaoweiObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new TbGaoweiObj();
                        obj.id = Convert.ToInt32(sdr["id"]);
                        obj.childId = Convert.ToInt32(sdr["childId"]);
                        obj.gaoweiyinsu = sdr["gaoweiyinsu"].ToString();
                        obj.recordtime = sdr["recordtime"].ToString();
                        obj.type = sdr["type"].ToString();
                        obj.doctorName = sdr["doctorName"].ToString();
                        arraylist.Add(obj);
                    }
                }
                return arraylist;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }

        public string getGaoweistr(int childid,string gaoweitime,string type)
        {
            string sqls = "select * from tb_gaowei where childid=" + childid;
            if (!string.IsNullOrEmpty(gaoweitime))
            {
                sqls += " and recordtime='"+gaoweitime+"'";
            }
            if (!string.IsNullOrEmpty(type))
            {
                sqls += " and type='" + type + "'";
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
                    string delsqlstr = "select * from tb_gaowei where gaoweiyinsu not in('" + gaoweistr.Replace(",", "','") + "') and childId= " + globalInfoClass.Wm_Index;
                    SqlDataReader sdr = dg.executequery(delsqlstr);
                    if (sdr.HasRows)
                    {
                        string delstr = "delete from tb_gaowei where gaoweiyinsu not in('" + gaoweistr.Replace(",", "','") + "') and childId= " + globalInfoClass.Wm_Index;
                        dg.executeupdate(delstr);
                        while (sdr.Read())
                        {
                            string insertrecordsql = "insert into tb_gaowei_record ("
                                            + "childId"
                                            + ",gaoweiyinsu"
                                            + ",recordtime"
                                            + ",status"
                                            + ",doctorName"
                                            + " ) values ( "
                                            + "" + globalInfoClass.Wm_Index + ""
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
                                            + "" + globalInfoClass.Wm_Index + ""
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
                                            + "" + globalInfoClass.Wm_Index + ""
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
            else
            { 
                string delsqlstr = "select * from tb_gaowei where  childId= " + globalInfoClass.Wm_Index;
                SqlDataReader sdr = dg.executequery(delsqlstr);
                string delstr = "delete from tb_gaowei where  childId= " + globalInfoClass.Wm_Index;
                dg.executeupdate(delstr);
                while (sdr.Read())
                {
                    string insertrecordsql = "insert into tb_gaowei_record ("
                                           + "childId"
                                           + ",gaoweiyinsu"
                                           + ",recordtime"
                                           + ",status"
                                           + ",doctorName"
                                           + " ) values ( "
                                           + "" + globalInfoClass.Wm_Index + ""
                                           + ",'" + sdr["gaoweiyinsu"] + "'"
                                           + ",'" + gaoweitime + "'"
                                           + ",'取消'"
                                           + ",'" + globalInfoClass.UserName + "'"
                                           + ")";
                    dg.executeupdate(insertrecordsql);
                }
            }

            string basesql = "update tb_childbase set gaoweiyinsu='" + getGaoweistr(globalInfoClass.Wm_Index, "", "高危") + "'"
                + " ,yingyangbuliang='" + getGaoweistr(globalInfoClass.Wm_Index, "", "营养不良") + "' where id=" + globalInfoClass.Wm_Index;
            dg.executeupdate(basesql);
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

        public string getgaoweitype(string gaoweiyinsu)
        {
            string type = "高危";
            //string gaoweilist = "新生儿住院,运动发育障碍,语言问题,其他发育行为问题,ADHD,早产,缺氧缺血性脑病,高胆红素血症,遗传病或者代谢性疾病";
            string gaoweilist = gettype(1);
            string yinyanglist = gettype(2);

            //string jibinglist = "超重,低体重,消瘦,生长迟缓,轻度贫血,中重度贫血,,肥胖,贫血,佝偻病";
            string xinlijibinglist = gettype(3);

            string jibingguanli = gettype(4);
      
            if (gaoweilist.Contains(gaoweiyinsu))
            {
                type = "高危";
            }
            else if (yinyanglist.Contains(gaoweiyinsu))
            {
                type = "营养不良";
            }
            else if (xinlijibinglist.Contains(gaoweiyinsu))
            {
                type = "心理行为发育异常";
            }
            else if (jibingguanli.Contains(gaoweiyinsu))
            {
                type = "疾病管理";
            }
            return type;
        }

        private string gettype(int i)
        {
            string str = "";
            ArrayList list = gwbll.getContentlist(i);
            if (list != null && list.Count > 0)
            {
                foreach (ProjectGaoweiobj item in list)
                {
                    str += item.content+",";
                }
            }
            return str;
        }
    }
}
