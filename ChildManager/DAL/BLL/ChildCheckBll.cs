using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChildManager.Model;
using System.Windows.Forms;
using ChildManager.DAL;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;

namespace ChildManager.BLL
{
    class ChildCheckBll
    {
        DateLogic dg = new DateLogic();
        //保存
        public bool saverecord(string sqls)
        {
            try
            {
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
        //修改
        public int updaterecord(string sqls)
        {
            DateLogic dg = new DateLogic();
            try
            {
                return dg.executeupdate(sqls);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败", "软件提示");
                throw ex;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public int deleterecord(string sqls)
        {
            DateLogic dg = new DateLogic();
            try
            {
                return dg.executeupdate(sqls);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败", "软件提示");
                throw ex;
            }
        }



        /// <summary>
        /// 查询儿童体检情况基本信息
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ChildCheckObj getChildCheckobj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
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
                    sdr.Read();//读取第一行数据记录
                    obj = new  ChildCheckObj();
                    obj.Id = Convert.ToInt32(sdr["id"]);
                    obj.ChildId = Convert.ToInt32(sdr["childId"]);
                    obj.CheckAge = sdr["checkAge"].ToString();
                    obj.CheckFactAge = sdr["checkFactAge"].ToString();
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
                    obj.nuerzhidao = sdr["nuerzhidao"].ToString();
                    obj.zonghefazhan = sdr["zonghefazhan"].ToString();
                    obj.fuzhujiancha = sdr["fuzhujiancha"].ToString();
                    obj.otherjiancha = sdr["otherjiancha"].ToString();
                }
                return obj;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }

        /// <summary>
        /// 查询儿童体检情况基本信息
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getChildCheckList(string sqls)
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
                        obj.ChildId = Convert.ToInt32(sdr["childId"]);
                        obj.CheckAge = sdr["CheckFactAge"].ToString();
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
                        obj.nuerzhidao = sdr["nuerzhidao"].ToString();
                        obj.zonghefazhan = sdr["zonghefazhan"].ToString();
                        obj.fuzhujiancha = sdr["fuzhujiancha"].ToString();
                        obj.otherjiancha = sdr["otherjiancha"].ToString();

                        obj.zhusu=  sdr["zhusu"].ToString();
                        obj.bingshi=  sdr["bingshi"].ToString();
                        obj.tijian=  sdr["tijian"].ToString();
                        obj.zhenduan = sdr["zhenduan"].ToString();
                        obj.chuli= sdr["chuli"].ToString();
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

        /// <summary>
        /// 查询儿童体检情况基本信息
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getChildCheckListForBmi(string sqls)
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
                        obj.yf = Convert.ToInt32(sdr["yf"]);
                        
                        obj.CheckHeight = sdr["checkHeight"].ToString();
                        obj.CheckWeight = sdr["checkWeight"].ToString();
                        obj.CheckTouwei = sdr["checkTouwei"].ToString();
                        
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


        public ArrayList getChildCheckListquexiantu(string sqls)
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
                        obj.ChildId = Convert.ToInt32(sdr["childId"]);
                        obj.CheckDay = sdr["checkDay"].ToString();
                        obj.CheckHeight = sdr["checkHeight"].ToString();
                        obj.CheckWeight = sdr["checkWeight"].ToString();
                        obj.CheckTouwei = sdr["checkTouwei"].ToString();
                        obj.Fuwei = sdr["fuwei"].ToString();
                        
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



        /// <summary>
        /// 查询儿童体检情况及高危记录基本信息
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getChildCheckAndGaoweiList(string sqls)
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
                        obj.ChildId = Convert.ToInt32(sdr["childId"]);
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

                        obj.nuerzhidao = sdr["nuerzhidao"].ToString();
                        obj.zonghefazhan = sdr["zonghefazhan"].ToString();

                        obj.zhushi = sdr["zhushi"].ToString();
                        obj.fushi = sdr["fushi"].ToString();
                        obj.shehui = sdr["shehui"].ToString();
                        obj.dongzuo = sdr["dongzuo"].ToString();
                        obj.toulu = sdr["toulu"].ToString();
                        obj.gouloubing = sdr["gouloubing"].ToString();

                        if (!String.IsNullOrEmpty(sdr["recordid"].ToString()))
                        {
                            obj.gaoweirecordobj.id = Convert.ToInt32(sdr["recordid"]);
                        }
                        obj.gaoweirecordobj.pingufangfa = sdr["pingufangfa"].ToString();
                        obj.gaoweirecordobj.pingujieguo = sdr["pingujieguo"].ToString();
                        obj.gaoweirecordobj.zhidao = sdr["zhidao"].ToString();
                        obj.gaoweirecordobj.chuli = sdr["chuli"].ToString();

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
        

        /// <summary>
        /// 体检评价
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ChildCheckObj getCheckobj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
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
                    sdr.Read();//读取第一行数据记录
                    obj = new ChildCheckObj();
                    obj.Id = Convert.ToInt32(sdr["id"]);
                    obj.ChildId = Convert.ToInt32(sdr["childId"]);
                    obj.nuerzhidao = sdr["nuerzhidao"].ToString();
                    obj.checkdiagnose = sdr["checkdiagnose"].ToString();

                    obj.zonghefazhan = sdr["zonghefazhan"].ToString();
                }
                return obj;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }






        public ArrayList getchildChectmonthList(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList(); ;
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
                        //sdr.Read();//读取第一行数据记录
                        ChildCheckObj obj = new ChildCheckObj();
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        obj.ChildId = Convert.ToInt32(sdr["childId"]);
                        obj.CheckMonth = sdr["checkMonth"].ToString();
                        arraylist.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
            return arraylist;
        }


        /// <summary>
        /// 查询儿童体检信息  返回集合
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getchildChectList(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList(); ;
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
                        //sdr.Read();//读取第一行数据记录
                       ChildCheckObj obj = new ChildCheckObj();
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        obj.ChildId = Convert.ToInt32(sdr["childId"]);
                        obj.CheckAge = sdr["checkAge"].ToString();
                        obj.CheckFactAge = sdr["checkFactAge"].ToString();
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
                        obj.YuCeHeight = sdr["yuCeHeight"].ToString();
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

                        obj.nuerzhidao = sdr["nuerzhidao"].ToString();
                        obj.zonghefazhan = sdr["zonghefazhan"].ToString();
                        arraylist.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
            return arraylist;
        }



        //打印获取打印坐标
        public List<PointF> getchildcheckPrint(string sqls)
        {
            SqlDataReader sdr = null;
            List<PointF> pointlist = new List<PointF>();
            //pointlist.Add(new PointF(61, 815));//其实坐标
            List<ChildCheckObj> checkrows = new List<ChildCheckObj>();
            try
            {
                sdr = dg.executequery(sqls);
                while (sdr.Read())
                {
                    ChildCheckObj obj = new ChildCheckObj();
                    obj.Id = Convert.ToInt32(sdr["id"]);
                    obj.ChildId = Convert.ToInt32(sdr["childId"]);
                    obj.CheckHeight = sdr["checkHeight"].ToString();
                    obj.CheckWeight = sdr["checkWeight"].ToString();
                    checkrows.Add(obj);
                }
                if (checkrows.Count > 0)
                {
                    //decimal weight1 = checkrows[0].CheckWeight;//孕妇的孕前体重
                    for (int i = 0; i < checkrows.Count; i++)
                    {
                        if (checkrows[i].CheckWeight == "" || checkrows[i].CheckHeight=="")
                        {
                            continue;
                        }
                       // decimal weight2 = checkrows[i].wm_weight;//体重
                        //int w = checkrows[i].CheckWeight.ToString();
                        //int d = Convert.ToInt32(checkrows[i].CheckHeight);
                        string ystr = checkrows[i].CheckWeight.ToString();
                        string xstr = checkrows[i].CheckHeight.ToString();

                        float y = 830-(float.Parse(ystr)-1)*20;
                        float x = (float.Parse(xstr)-45)*10+65;
                        PointF point = new PointF(x, y);
                        pointlist.Add(point);
                        //weight1 = fucharows[i].wm_weight;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("统计异常", "软件提示");
                throw ex;
            }
            finally
            {
                sdr.Close();
                dg.con_close();
            }

            return pointlist;
        }
    }
}
