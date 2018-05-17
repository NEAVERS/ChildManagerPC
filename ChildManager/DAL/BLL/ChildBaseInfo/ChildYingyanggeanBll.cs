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

namespace ChildManager.BLL.ChildBaseInfo
{
    class ChildYingyanggeanBll
    {
        DateLogic dg = new DateLogic();

        public ChildYingyanggeanObj getYingyanggeanObj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            try
            {
                sdr = dg.executequery(sqls);
                ChildYingyanggeanObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    sdr.Read();
                    obj = new ChildYingyanggeanObj();
                    obj.id = Convert.ToInt32(sdr["id"]);
                    obj.childId = Convert.ToInt32(sdr["childId"]);
                    obj.jiwangshi = sdr["jiwangshi"].ToString();
                    obj.endtime = sdr["endtime"].ToString();
                    obj.managetime = sdr["managetime"].ToString();
                    obj.zhuangui = sdr["zhuangui"].ToString();
                    obj.chushengshi = sdr["chushengshi"].ToString();
                    obj.weiyangshi = sdr["weiyangshi"].ToString();
                    obj.foodchange = sdr["foodchange"].ToString();
                    obj.recordtime = sdr["recordtime"].ToString();
                    obj.recordname = sdr["recordname"].ToString();

                    obj.yunzhou = sdr["yunzhou"].ToString();
                    obj.yuntian = sdr["yuntian"].ToString();
                    obj.hb = sdr["hb"].ToString();
                    obj.tieji = sdr["tieji"].ToString();
                    obj.yaowu = sdr["yaowu"].ToString();
                    obj.jiliang = sdr["jiliang"].ToString();
                    obj.liaocheng = sdr["liaocheng"].ToString();
                    obj.muru = sdr["muru"].ToString();
                    obj.foodage = sdr["foodage"].ToString();

                    obj.buruqi = sdr["buruqi"].ToString();
                    obj.childvitd = sdr["childvitd"].ToString();
                    obj.startvitdmonth = sdr["startvitdmonth"].ToString();
                    obj.startvitdday = sdr["startvitdday"].ToString();
                    obj.vitdname = sdr["vitdname"].ToString();
                    obj.vitdliang = sdr["vitdliang"].ToString();
                    obj.tizheng = sdr["tizheng"].ToString();
                    obj.xuegai = sdr["xuegai"].ToString();
                    obj.xuelin = sdr["xuelin"].ToString();
                    obj.xueakp = sdr["xueakp"].ToString();
                    obj.xueoh = sdr["xueoh"].ToString();
                    obj.xjiancha = sdr["xjiancha"].ToString();
                }
                return obj;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }

        
        //保存
        public bool saveYingyanggeanObj(ChildYingyanggeanObj yingyanggeanobj)
        {
            try
            {
                string selstr = "select * from child_yingyanggean where childid=" + yingyanggeanobj.childId;
                string sqls = "";
                if (ifhascount(selstr))
                {
                    sqls = "insert into child_yingyanggean ("
                            + "childId"
                            + ",jiwangshi"
                            + ",endtime"
                            + ",managetime"
                            + ",zhuangui"
                            + ",chushengshi"
                            + ",weiyangshi"
                            + ",foodchange"

                            + ",yunzhou"
                            + ",yuntian"
                            + ",hb"
                            + ",tieji"
                            + ",yaowu"
                            + ",jiliang"
                            + ",liaocheng"
                            + ",muru"
                            + ",foodage"

                            + ",buruqi"
                            + ",childvitd"
                            + ",startvitdmonth"
                            + ",startvitdday"
                            + ",vitdname"
                            + ",vitdliang"
                            + ",tizheng"
                            + ",xuegai"
                            + ",xuelin"
                            + ",xueakp"
                            + ",xueoh"
                            + ",xjiancha"

                            + ",recordtime"
                            + ",recordname"
                            + " ) values ( "
                            + "" + yingyanggeanobj.childId + ""
                            + ",'" + yingyanggeanobj.jiwangshi + "'"
                            + ",'" + yingyanggeanobj.endtime + "'"
                            + ",'" + yingyanggeanobj.managetime + "'"
                            + ",'" + yingyanggeanobj.zhuangui + "'"
                            + ",'" + yingyanggeanobj.chushengshi + "'"
                            + ",'" + yingyanggeanobj.weiyangshi + "'"
                            + ",'" + yingyanggeanobj.foodchange + "'"

                            + ",'" + yingyanggeanobj.yunzhou + "'"
                            + ",'" + yingyanggeanobj.yuntian + "'"
                            + ",'" + yingyanggeanobj.hb + "'"
                            + ",'" + yingyanggeanobj.tieji + "'"
                            + ",'" + yingyanggeanobj.yaowu + "'"
                            + ",'" + yingyanggeanobj.jiliang + "'"
                            + ",'" + yingyanggeanobj.liaocheng + "'"
                            + ",'" + yingyanggeanobj.muru + "'"
                            + ",'" + yingyanggeanobj.foodage + "'"

                            + ",'" + yingyanggeanobj.buruqi + "'"
                            + ",'" + yingyanggeanobj.childvitd + "'"
                            + ",'" + yingyanggeanobj.startvitdmonth + "'"
                            + ",'" + yingyanggeanobj.startvitdday + "'"
                            + ",'" + yingyanggeanobj.vitdname + "'"
                            + ",'" + yingyanggeanobj.vitdliang + "'"
                            + ",'" + yingyanggeanobj.tizheng + "'"
                            + ",'" + yingyanggeanobj.xuegai + "'"
                            + ",'" + yingyanggeanobj.xuelin + "'"
                            + ",'" + yingyanggeanobj.xueakp + "'"
                            + ",'" + yingyanggeanobj.xueoh + "'"
                            + ",'" + yingyanggeanobj.xjiancha + "'"

                            + ",'" + yingyanggeanobj.recordtime + "'"
                            + ",'" + yingyanggeanobj.recordname + "'"
                            + ")";
                    
                }
                else
                {
                    sqls = "update child_yingyanggean set "
                            + " jiwangshi = '" + yingyanggeanobj.jiwangshi + "'"
                            + " ,endtime = '" + yingyanggeanobj.endtime + "'"
                            + " ,managetime = '" + yingyanggeanobj.managetime + "'"
                            + " ,zhuangui = '" + yingyanggeanobj.zhuangui + "'"
                            + " ,chushengshi = '" + yingyanggeanobj.chushengshi + "'"
                            + " ,weiyangshi = '" + yingyanggeanobj.weiyangshi + "'"
                            + " ,foodchange = '" + yingyanggeanobj.foodchange + "'"

                            + " ,yunzhou = '" + yingyanggeanobj.yunzhou + "'"
                            + " ,yuntian = '" + yingyanggeanobj.yuntian + "'"
                            + " ,hb = '" + yingyanggeanobj.hb + "'"
                            + " ,tieji = '" + yingyanggeanobj.tieji + "'"
                            + " ,yaowu = '" + yingyanggeanobj.yaowu + "'"
                            + " ,jiliang = '" + yingyanggeanobj.jiliang + "'"
                            + " ,liaocheng = '" + yingyanggeanobj.liaocheng + "'"
                            + " ,muru = '" + yingyanggeanobj.muru + "'"
                            + " ,foodage = '" + yingyanggeanobj.foodage + "'"

                            + " ,buruqi = '" + yingyanggeanobj.buruqi + "'"
                            + " ,childvitd = '" + yingyanggeanobj.childvitd + "'"
                            + " ,startvitdmonth = '" + yingyanggeanobj.startvitdmonth + "'"
                            + " ,startvitdday = '" + yingyanggeanobj.startvitdday + "'"
                            + " ,vitdname = '" + yingyanggeanobj.vitdname + "'"
                            + " ,vitdliang = '" + yingyanggeanobj.vitdliang + "'"
                            + " ,tizheng = '" + yingyanggeanobj.tizheng + "'"
                            + " ,xuegai = '" + yingyanggeanobj.xuegai + "'"
                            + " ,xuelin = '" + yingyanggeanobj.xuelin + "'"
                            + " ,xueakp = '" + yingyanggeanobj.xueakp + "'"
                            + " ,xueoh = '" + yingyanggeanobj.xueoh + "'"
                            + " ,xjiancha = '" + yingyanggeanobj.xjiancha + "'"

                            + " ,recordtime = '" + yingyanggeanobj.recordtime + "'"
                            + " ,recordname = '" + yingyanggeanobj.recordname + "'"
                            + " where childId= " + yingyanggeanobj.childId;
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


        public bool savejibingguanliObj(ChildYingyanggeanObj yingyanggeanobj)
        {
            try
            {
                string selstr = "select * from child_jibingguanli where childid=" + yingyanggeanobj.childId;
                string sqls = "";
                if (ifhascount(selstr))
                {
                    sqls = "insert into child_jibingguanli ("
                            + "childId"
                            + ",jiwangshi"
                            + ",endtime"
                            + ",managetime"
                            + ",zhuangui"
                            + ",chushengshi"
                            + ",weiyangshi"
                            + ",foodchange"

                            + ",yunzhou"
                            + ",yuntian"
                            + ",hb"
                            + ",tieji"
                            + ",yaowu"
                            + ",jiliang"
                            + ",liaocheng"
                            + ",muru"
                            + ",foodage"

                            + ",buruqi"
                            + ",childvitd"
                            + ",startvitdmonth"
                            + ",startvitdday"
                            + ",vitdname"
                            + ",vitdliang"
                            + ",tizheng"
                            + ",xuegai"
                            + ",xuelin"
                            + ",xueakp"
                            + ",xueoh"
                            + ",xjiancha"

                            + ",recordtime"
                            + ",recordname"
                            + " ) values ( "
                            + "" + yingyanggeanobj.childId + ""
                            + ",'" + yingyanggeanobj.jiwangshi + "'"
                            + ",'" + yingyanggeanobj.endtime + "'"
                            + ",'" + yingyanggeanobj.managetime + "'"
                            + ",'" + yingyanggeanobj.zhuangui + "'"
                            + ",'" + yingyanggeanobj.chushengshi + "'"
                            + ",'" + yingyanggeanobj.weiyangshi + "'"
                            + ",'" + yingyanggeanobj.foodchange + "'"

                            + ",'" + yingyanggeanobj.yunzhou + "'"
                            + ",'" + yingyanggeanobj.yuntian + "'"
                            + ",'" + yingyanggeanobj.hb + "'"
                            + ",'" + yingyanggeanobj.tieji + "'"
                            + ",'" + yingyanggeanobj.yaowu + "'"
                            + ",'" + yingyanggeanobj.jiliang + "'"
                            + ",'" + yingyanggeanobj.liaocheng + "'"
                            + ",'" + yingyanggeanobj.muru + "'"
                            + ",'" + yingyanggeanobj.foodage + "'"

                            + ",'" + yingyanggeanobj.buruqi + "'"
                            + ",'" + yingyanggeanobj.childvitd + "'"
                            + ",'" + yingyanggeanobj.startvitdmonth + "'"
                            + ",'" + yingyanggeanobj.startvitdday + "'"
                            + ",'" + yingyanggeanobj.vitdname + "'"
                            + ",'" + yingyanggeanobj.vitdliang + "'"
                            + ",'" + yingyanggeanobj.tizheng + "'"
                            + ",'" + yingyanggeanobj.xuegai + "'"
                            + ",'" + yingyanggeanobj.xuelin + "'"
                            + ",'" + yingyanggeanobj.xueakp + "'"
                            + ",'" + yingyanggeanobj.xueoh + "'"
                            + ",'" + yingyanggeanobj.xjiancha + "'"

                            + ",'" + yingyanggeanobj.recordtime + "'"
                            + ",'" + yingyanggeanobj.recordname + "'"
                            + ")";

                }
                else
                {
                    sqls = "update child_jibingguanli set "
                            + " jiwangshi = '" + yingyanggeanobj.jiwangshi + "'"
                            + " ,endtime = '" + yingyanggeanobj.endtime + "'"
                            + " ,managetime = '" + yingyanggeanobj.managetime + "'"
                            + " ,zhuangui = '" + yingyanggeanobj.zhuangui + "'"
                            + " ,chushengshi = '" + yingyanggeanobj.chushengshi + "'"
                            + " ,weiyangshi = '" + yingyanggeanobj.weiyangshi + "'"
                            + " ,foodchange = '" + yingyanggeanobj.foodchange + "'"

                            + " ,yunzhou = '" + yingyanggeanobj.yunzhou + "'"
                            + " ,yuntian = '" + yingyanggeanobj.yuntian + "'"
                            + " ,hb = '" + yingyanggeanobj.hb + "'"
                            + " ,tieji = '" + yingyanggeanobj.tieji + "'"
                            + " ,yaowu = '" + yingyanggeanobj.yaowu + "'"
                            + " ,jiliang = '" + yingyanggeanobj.jiliang + "'"
                            + " ,liaocheng = '" + yingyanggeanobj.liaocheng + "'"
                            + " ,muru = '" + yingyanggeanobj.muru + "'"
                            + " ,foodage = '" + yingyanggeanobj.foodage + "'"

                            + " ,buruqi = '" + yingyanggeanobj.buruqi + "'"
                            + " ,childvitd = '" + yingyanggeanobj.childvitd + "'"
                            + " ,startvitdmonth = '" + yingyanggeanobj.startvitdmonth + "'"
                            + " ,startvitdday = '" + yingyanggeanobj.startvitdday + "'"
                            + " ,vitdname = '" + yingyanggeanobj.vitdname + "'"
                            + " ,vitdliang = '" + yingyanggeanobj.vitdliang + "'"
                            + " ,tizheng = '" + yingyanggeanobj.tizheng + "'"
                            + " ,xuegai = '" + yingyanggeanobj.xuegai + "'"
                            + " ,xuelin = '" + yingyanggeanobj.xuelin + "'"
                            + " ,xueakp = '" + yingyanggeanobj.xueakp + "'"
                            + " ,xueoh = '" + yingyanggeanobj.xueoh + "'"
                            + " ,xjiancha = '" + yingyanggeanobj.xjiancha + "'"

                            + " ,recordtime = '" + yingyanggeanobj.recordtime + "'"
                            + " ,recordname = '" + yingyanggeanobj.recordname + "'"
                            + " where childId= " + yingyanggeanobj.childId;
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
