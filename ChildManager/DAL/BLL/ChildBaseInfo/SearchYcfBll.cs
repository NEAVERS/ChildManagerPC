using System;
using ChildManager.DAL;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using YCF.Model;
using YCF.Common;

namespace ChildManager.BLL.ChildBaseInfo
{
    class SearchYcfBll
    {
        YcfDateLogic ycfdg = new YcfDateLogic();
        
        /// <summary>
        /// 查询本院儿童列表
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getchildBaseList(string sqls)
        {
            ArrayList arraylist = new ArrayList();
            SqlDataReader sdr = null;
            try
            {
                sdr = ycfdg.executequery(sqls);
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        //sdr.Read();//读取第一行数据记录
                        tb_childbase obj = new tb_childbase();
                        //obj.Id = Convert.ToInt32(sdr["id"]);
                        //obj.HealthCardNo = sdr["healthCardNo"].ToString();
                        //obj.ChildName = sdr["childName"].ToString();
                        obj.childgender = sdr["baby_sex"].ToString();
                        //obj.BloodType = sdr["bloodType"].ToString();
                        obj.childbirthday = sdr["fenmian_riqi"].ToString();
                        //obj.ChildBuildDay = sdr["childBuildDay"].ToString();
                        //保存图片  
                        //obj.ChildPhoto
                        //TODO


                        //obj.ChildBuildHospital = sdr["childBuildHospital"].ToString();
                        //obj.FatherName = sdr["fatherName"].ToString();
                        //obj.FatherAge = sdr["fatherAge"].ToString();
                        //obj.FatherHeight = sdr["fatherHeight"].ToString();
                        //obj.FatherEducation = sdr["fatherEducation"].ToString();
                        //obj.FatherJob = sdr["fatherJob"].ToString();
                        obj.mothername = sdr["wm_name"].ToString();
                        obj.motherage = sdr["wm_age"].ToString().Replace("岁","");
                        //obj.MotherHeight = sdr["motherHeight"].ToString();
                        //obj.MotherEducation = sdr["motherEducation"].ToString();
                        obj.motherjob = sdr["wm_job"].ToString();
                        //obj.Telephone = sdr["telephone"].ToString();
                        obj.province = sdr["wm_pro"].ToString();
                        obj.city = sdr["wm_city"].ToString();
                        obj.area = sdr["wm_area"].ToString();
                        obj.address = sdr["wm_address"].ToString();
                        //obj.NurseryInstitution = sdr["nurseryInstitution"].ToString();//托幼机构
                        //发证
                        //obj.ImmuneUnit = sdr["immuneUnit"].ToString();
                        //obj.ImmuneDay = sdr["immuneDay"].ToString();
                        //obj.Community = sdr["community"].ToString();
                        //obj.CensusRegister = sdr["censusRegister"].ToString();
                        //出生史
                        obj.cs_fetus = sdr["wm_yunci"].ToString();
                        obj.cs_produce = sdr["wm_chanci"].ToString();
                        obj.cs_week = sdr["wm_yunzhou"].ToString();
                        obj.cs_day = sdr["wm_yuntian"].ToString();
                        obj.modedelivery = sdr["fenmian_fangshi"].ToString();
                        if (String.IsNullOrEmpty(sdr["huiyin_qiekai"].ToString()))
                        {
                            obj.perineumincision = "否";
                        }
                        else
                        {
                            obj.perineumincision = "是";
                        }
                        //obj.FetusNumber = sdr["fetusNumber"].ToString();

                        //obj.NeonatalCondition = sdr["neonatalCondition"].ToString();
                        double weight = 0;
                        if(double.TryParse(sdr["baby_weight"].ToString(),out weight))
                        obj.birthweight = (weight/1000).ToString();
                        obj.birthheight = sdr["baby_hight"].ToString();
                        obj.birthaddress = CommonHelper.GetHospitalName();
                        //母乳喂养
                        //obj.HospitalizedStates = sdr["hospitalizedStates"].ToString();
                        //obj.OneMonth = sdr["oneMonth"].ToString();
                        //obj.InFourMonth = sdr["inFourMonth"].ToString();
                        //obj.FourToSixMonth = sdr["fourToSixMonth"].ToString();
                        //obj.Yunqi = sdr["yunqi"].ToString();
                        obj.identityno = sdr["wm_identityno"].ToString();
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
    }
}
