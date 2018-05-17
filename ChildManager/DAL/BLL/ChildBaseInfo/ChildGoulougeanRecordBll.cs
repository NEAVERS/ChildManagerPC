using System;
using ChildManager.DAL;
using System.Windows.Forms;
using ChildManager.Model.ChildBaseInfo;
using System.Data.SqlClient;
using System.Collections;
using ChildManager.Model;

namespace ChildManager.BLL.ChildBaseInfo
{
    class ChildGoulougeanRecordBll
    {
        DateLogic dg = new DateLogic();

        /// <summary>
        /// 查询儿童体检情况及佝偻病记录基本信息
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getChildCheckAndGoulouList(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                ChildCheckObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new ChildCheckObj();
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        obj.ChildId = Convert.ToInt32(sdr["childid"]);
                        obj.CheckAge = sdr["checkFactAge"].ToString();
                        //obj.CheckFactAge = sdr["checkFactAge"].ToString();
                        obj.CheckDay = sdr["checkDay"].ToString();
                        obj.DoctorName = sdr["doctorName"].ToString();

                        obj.CheckHeight = sdr["checkHeight"].ToString();
                        obj.CheckWeight = sdr["checkWeight"].ToString();
                        obj.CheckTouwei = sdr["checkTouwei"].ToString();
                        obj.Fuwei = sdr["fuwei"].ToString();
                        obj.CheckZuogao = sdr["checkZuogao"].ToString();
                        obj.CheckTunwei = sdr["checkTunwei"].ToString();
                        obj.CheckQianlu = sdr["checkQianlu"].ToString();
                        obj.CheckIQ = sdr["checkIQ"].ToString();
                        obj.CheckZiping = sdr["checkZiping"].ToString();
                        //obj.YuCeHeight = sdr["yuCeHeight"].ToString();
                        obj.Leftyan = sdr["leftyan"].ToString();
                        obj.Rightyan = sdr["rightyan"].ToString();
                        obj.Leftyanshili = sdr["leftyanshili"].ToString();
                        obj.Rightyanshili = sdr["rightyanshili"].ToString();
                        obj.Xieshi = sdr["xieshi"].ToString();
                        obj.Lefter = sdr["lefter"].ToString();
                        obj.Righter = sdr["righter"].ToString();
                        obj.Lefterlisten = sdr["lefterlisten"].ToString();
                        obj.Rightlisten = sdr["rightlisten"].ToString();
                        obj.Checkbi = sdr["checkbi"].ToString();
                        obj.KouQiang = sdr["kouQiang"].ToString();
                        obj.YaciNumber = sdr["yaciNumber"].ToString();
                        obj.YuciNumber = sdr["yuciNumber"].ToString();
                        obj.Skin = sdr["skin"].ToString();
                        obj.Lingbaji = sdr["lingbajie"].ToString();
                        obj.BianTaoti = sdr["bianTaoti"].ToString();
                        obj.XinZang = sdr["xinZang"].ToString();
                        obj.FeiBu = sdr["feiBu"].ToString();
                        obj.GanZang = sdr["ganZang"].ToString();
                        obj.PiZang = sdr["piZang"].ToString();
                        obj.QiZhi = sdr["qiZhi"].ToString();
                        obj.SiZhi = sdr["siZhi"].ToString();
                        obj.XiongBu = sdr["xiongBu"].ToString();
                        obj.MiNiaoQi = sdr["miNiaoQi"].ToString();
                        obj.WuGuan = sdr["wuGuan"].ToString();
                        obj.JiZhu = sdr["jiZhu"].ToString();
                        obj.PingPqiu = sdr["pingPqiu"].ToString();
                        obj.CheckContent = sdr["checkContent"].ToString();
                        obj.YufangJiezhong = sdr["yufangJiezhong"].ToString();
                        obj.BloodseSu = sdr["bloodseSu"].ToString();
                        obj.ShiWugouCheng = sdr["shiWugouCheng"].ToString();
                        obj.Vitd = sdr["vitd"].ToString();
                        obj.BigSport = sdr["bigSport"].ToString();
                        obj.SpirtSport = sdr["spirtSport"].ToString();
                        obj.Laguage = sdr["laguage"].ToString();
                        obj.SignSocial = sdr["signSocial"].ToString();

                        obj.OtherBingshi = sdr["otherBingshi"].ToString();
                        obj.Handle = sdr["handle"].ToString();
                        obj.Diagnose = sdr["diagnose"].ToString();
                        obj.CheckMonth = sdr["checkMonth"].ToString();
                        obj.FuzenDay = sdr["fuzenDay"].ToString();
                        obj.Fubu = sdr["fubu"].ToString();

                        obj.nuerzhidao = sdr["nuerzhidao"].ToString();
                        obj.checkdiagnose = sdr["checkdiagnose"].ToString();

                        obj.zhushi = sdr["zhushi"].ToString();
                        obj.fushi = sdr["fushi"].ToString();
                        obj.shehui = sdr["shehui"].ToString();
                        obj.dongzuo = sdr["dongzuo"].ToString();
                        obj.toulu = sdr["toulu"].ToString();
                        obj.gouloubing = sdr["gouloubing"].ToString();

                        if (!String.IsNullOrEmpty(sdr["recordid"].ToString()))
                        {
                            obj.goulourecordobj.id = Convert.ToInt32(sdr["recordid"]);
                        }
                        obj.goulourecordobj.huwai = sdr["huwai"].ToString();
                        obj.goulourecordobj.problem = sdr["problem"].ToString();
                        obj.goulourecordobj.vitdname = sdr["vitdname"].ToString();
                        obj.goulourecordobj.vitdliang = sdr["vitdliang"].ToString();
                        obj.goulourecordobj.zhidao = sdr["zhidao"].ToString();
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

        //保存
        public bool saveGoulougeanrecord(ChildGoulougeanRecordObj goulourecordobj)
        {
                try
                {
                    string sqls = "";
                    if (goulourecordobj.id != 0)
                    {
                        sqls = "update child_goulougean_record set "
                                + " childId = " + goulourecordobj.childId
                                + " ,checkid = " + goulourecordobj.checkid
                                + " ,huwai = '"+ goulourecordobj.huwai+"'"
                                + " ,problem = '" + goulourecordobj.problem + "'"
                                + " ,vitdname = '" + goulourecordobj.vitdname + "'"
                                + " ,vitdliang = '" + goulourecordobj.vitdliang + "'"
                                + " ,zhidao = '" + goulourecordobj.zhidao + "'"
                                + " where id= " + goulourecordobj.id;
                    }
                    else
                    {
                        sqls = "insert into child_goulougean_record ("
                                + "childId"
                                + ",checkid"
                                + ",huwai"
                                + ",problem"
                                + ",vitdname"
                                + ",vitdliang"
                                + ",zhidao"
                                + " ) values ( "
                                + "" + goulourecordobj.childId + ""
                                + "," + goulourecordobj.checkid + ""
                                + ",'" + goulourecordobj.huwai + "'"
                                + ",'" + goulourecordobj.problem + "'"
                                + ",'" + goulourecordobj.vitdname + "'"
                                + ",'" + goulourecordobj.vitdliang + "'"
                                + ",'" + goulourecordobj.zhidao + "'"
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
