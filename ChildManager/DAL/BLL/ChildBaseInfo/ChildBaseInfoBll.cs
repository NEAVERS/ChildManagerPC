using System;
using System.Text;
using ChildManager.DAL;
using System.Windows.Forms;
using ChildManager.Model.ChildBaseInfo;
using System.Data.SqlClient;
using System.Collections;
using System.Data;

namespace ChildManager.BLL.ChildBaseInfo
{
    class ChildBaseInfoBll
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

        public ChildBaseInfoObj getchildBaseobj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            try
            {
                sdr = dg.executequery(sqls);
                ChildBaseInfoObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    sdr.Read();//读取第一行数据记录
                    obj = new ChildBaseInfoObj();
                    obj.Id = Convert.ToInt32(sdr["id"]);
                    obj.HealthCardNo = sdr["healthCardNo"].ToString();
                    obj.ChildName = sdr["childName"].ToString();
                    obj.ChildGender = sdr["childGender"].ToString();
                    obj.BloodType = sdr["bloodType"].ToString();
                    obj.ChildBirthDay = sdr["childBirthDay"].ToString();
                    obj.ChildBuildDay = sdr["childBuildDay"].ToString();
                    //保存图片  
                    //obj.ChildPhoto
                    //TODO


                    obj.ChildBuildHospital = sdr["childBuildHospital"].ToString();
                    obj.FatherName = sdr["fatherName"].ToString();
                    obj.FatherAge = sdr["fatherAge"].ToString();
                    obj.FatherHeight = sdr["fatherHeight"].ToString();
                    obj.FatherEducation = sdr["fatherEducation"].ToString();
                    obj.FatherJob = sdr["fatherJob"].ToString();
                    obj.MotherName = sdr["motherName"].ToString();
                    obj.MotherAge = sdr["motherAge"].ToString();
                    obj.MotherHeight = sdr["motherHeight"].ToString();
                    obj.MotherEducation = sdr["motherEducation"].ToString();
                    obj.MotherJob = sdr["motherJob"].ToString();
                    obj.Telephone = sdr["telephone"].ToString();
                    obj.Telephone2 = sdr["telephone2"].ToString();
                    obj.address = sdr["address"].ToString();
                    obj.NurseryInstitution = sdr["nurseryInstitution"].ToString();//托幼机构
                    //发证
                    obj.ImmuneUnit = sdr["immuneUnit"].ToString();
                    obj.ImmuneDay = sdr["immuneDay"].ToString();
                    obj.Community = sdr["community"].ToString();
                    obj.CensusRegister = sdr["censusRegister"].ToString();
                    //出生史
                    obj.Cs_fetus = sdr["cs_fetus"].ToString();
                    obj.Cs_produce = sdr["cs_produce"].ToString();
                    obj.Cs_week = sdr["cs_week"].ToString();
                    obj.Cs_day = sdr["cs_day"].ToString();
                    obj.ModeDelivery = sdr["modeDelivery"].ToString();
                    obj.PerineumIncision = sdr["perineumIncision"].ToString();//会阴侧开
                    obj.FetusNumber = sdr["fetusNumber"].ToString();
                    obj.NeonatalCondition = sdr["neonatalCondition"].ToString();
                    obj.BirthWeight = sdr["birthWeight"].ToString();
                    obj.BirthHeight = sdr["birthHeight"].ToString();
                    obj.Birthaddress = sdr["birthaddress"].ToString();
                    //母乳喂养
                    obj.HospitalizedStates = sdr["hospitalizedStates"].ToString();
                    obj.OneMonth = sdr["oneMonth"].ToString();
                    obj.InFourMonth = sdr["inFourMonth"].ToString();
                    obj.FourToSixMonth = sdr["fourToSixMonth"].ToString();
                    obj.Yunqi = sdr["yunqi"].ToString();
                    obj.Identityno = sdr["identityno"].ToString();
                    obj.jiuzhenCardNo = sdr["jiuzhenCardNo"].ToString();
                    obj.ispre = sdr["ispre"].ToString();
                    obj.gerenshi = sdr["gerenshi"].ToString();
                    obj.jiwangshi = sdr["jiwangshi"].ToString();
                    obj.jiazushi = sdr["jiazushi"].ToString();
                    obj.province = sdr["province"].ToString();
                    obj.city = sdr["city"].ToString();
                    obj.area = sdr["area"].ToString();
                }
                return obj;

            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }




        public ChildBaseInfoObj getchildBaseobjisPre(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            try
            {
                sdr = dg.executequery(sqls);
                ChildBaseInfoObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    sdr.Read();//读取第一行数据记录
                    obj = new ChildBaseInfoObj();
                    obj.ispre = sdr["ispre"].ToString();
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
        /// 读取 图片
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public byte[] getPhotobj(string sqls)
        {
            DateLogic dg = new DateLogic();
            SqlDataReader sdr = null;
            byte[] imageByte = null;
            try
            {
                sdr = dg.executequery(sqls);
                if (!sdr.HasRows)
                {

                }
                else
                {
                    sdr.Read();//读取第一行数据记录
                    imageByte = (byte[])sdr["childPhoto"];
                }
                return imageByte;
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常,请联系管理员", "软件提示");
                throw ex;
            }
        }
        /// <summary>
        /// 存储 图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        //public byte[] GetImageBytes(Image image)
        //{
        //    MemoryStream mstream = new MemoryStream();
        //    image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
        //    byte[] byteData = new Byte[mstream.Length];
        //    mstream.Position = 0;
        //    mstream.Read(byteData, 0, byteData.Length);
        //    mstream.Close();
        //    return byteData;
        //}

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


        /// <summary>
        /// 自动生成 保健卡号
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public int stateval(string sqls)
        {
            DateLogic dg = new DateLogic();
            int mrn;
            try
            {
                int returns = dg.stateval(sqls);
                if (returns == 0)
                {
                    mrn = 10001;
                }
                else
                {
                    mrn = returns + 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败", "软件提示");
                throw ex;
            }
            return mrn;
        }


        public int getchild_id(string sqls)
        {
            DateLogic dg = new DateLogic();
            int wm_id;
            try
            {
                wm_id = dg.stateval(sqls);
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败", "软件提示");
                throw ex;
            }
            return wm_id;
        }


        /// <summary>
        /// 查询儿童建档列表
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getchildBaseList(string sqls)
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
                        ChildBaseInfoObj obj = new ChildBaseInfoObj();
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        obj.HealthCardNo = sdr["healthCardNo"].ToString();
                        obj.ChildName = sdr["childName"].ToString();
                        obj.ChildGender = sdr["childGender"].ToString();
                        obj.BloodType = sdr["bloodType"].ToString();
                        obj.ChildBirthDay = sdr["childBirthDay"].ToString();
                        obj.ChildBuildDay = sdr["childBuildDay"].ToString();
                        //保存图片  
                        //obj.ChildPhoto
                        //TODO


                        obj.ChildBuildHospital = sdr["childBuildHospital"].ToString();
                        obj.FatherName = sdr["fatherName"].ToString();
                        obj.FatherAge = sdr["fatherAge"].ToString();
                        obj.FatherHeight = sdr["fatherHeight"].ToString();
                        obj.FatherEducation = sdr["fatherEducation"].ToString();
                        obj.FatherJob = sdr["fatherJob"].ToString();
                        obj.MotherName = sdr["motherName"].ToString();
                        obj.MotherAge = sdr["motherAge"].ToString();
                        obj.MotherHeight = sdr["motherHeight"].ToString();
                        obj.MotherEducation = sdr["motherEducation"].ToString();
                        obj.MotherJob = sdr["motherJob"].ToString();
                        obj.Telephone = sdr["telephone"].ToString();
                        obj.address = sdr["address"].ToString();
                        obj.NurseryInstitution = sdr["nurseryInstitution"].ToString();//托幼机构
                        //发证
                        obj.ImmuneUnit = sdr["immuneUnit"].ToString();
                        obj.ImmuneDay = sdr["immuneDay"].ToString();
                        obj.Community = sdr["community"].ToString();
                        obj.CensusRegister = sdr["censusRegister"].ToString();
                        //出生史
                        obj.Cs_fetus = sdr["cs_fetus"].ToString();
                        obj.Cs_produce = sdr["cs_produce"].ToString();
                        obj.Cs_week = sdr["cs_week"].ToString();
                        obj.Cs_day = sdr["cs_day"].ToString();
                        obj.ModeDelivery = sdr["modeDelivery"].ToString();
                        obj.PerineumIncision = sdr["perineumIncision"].ToString();//会阴侧开
                        obj.FetusNumber = sdr["fetusNumber"].ToString();
                        obj.NeonatalCondition = sdr["neonatalCondition"].ToString();
                        obj.BirthWeight = sdr["birthWeight"].ToString();
                        obj.BirthHeight = sdr["birthHeight"].ToString();
                        obj.Birthaddress = sdr["birthaddress"].ToString();
                        //母乳喂养
                        obj.HospitalizedStates = sdr["hospitalizedStates"].ToString();
                        obj.OneMonth = sdr["oneMonth"].ToString();
                        obj.InFourMonth = sdr["inFourMonth"].ToString();
                        obj.FourToSixMonth = sdr["fourToSixMonth"].ToString();
                        obj.Yunqi = sdr["yunqi"].ToString();
                        obj.Identityno = sdr["identityno"].ToString();
                        obj.jiuzhenCardNo = sdr["jiuzhenCardNo"].ToString();
                        obj.linshi = sdr["linshi"].ToString();

                        if (sdr["zhushi"] != null)
                        {
                            obj.zhushi = sdr["zhushi"].ToString();
                        }

                        if (sdr["fuzenday"] != null)
                        {
                            obj.fuzhenday = sdr["fuzenday"].ToString();
                        }

                        obj.gaowei = sdr["gaoweiyinsu"].ToString();
                        obj.ispre = sdr["ispre"].ToString();
                        obj.province = sdr["province"].ToString();
                        obj.city = sdr["city"].ToString();
                        obj.area = sdr["area"].ToString();
                        obj.gerenshi = sdr["gerenshi"].ToString();
                        obj.jiwangshi = sdr["jiwangshi"].ToString();
                        obj.jiazushi = sdr["jiazushi"].ToString();
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
        /// 查询儿童建档列表1
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getchildBaseListTwo(string sqls)
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
                        ChildBaseInfoObj obj = new ChildBaseInfoObj();
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        obj.HealthCardNo = sdr["healthCardNo"].ToString();
                        obj.ChildName = sdr["childName"].ToString();
                        obj.ChildGender = sdr["childGender"].ToString();
                        obj.BloodType = sdr["bloodType"].ToString();
                        obj.ChildBirthDay = sdr["childBirthDay"].ToString();
                        obj.ChildBuildDay = sdr["childBuildDay"].ToString();
                        //保存图片  
                        //obj.ChildPhoto
                        //TODO


                        obj.ChildBuildHospital = sdr["childBuildHospital"].ToString();
                        obj.FatherName = sdr["fatherName"].ToString();
                        obj.FatherAge = sdr["fatherAge"].ToString();
                        obj.FatherHeight = sdr["fatherHeight"].ToString();
                        obj.FatherEducation = sdr["fatherEducation"].ToString();
                        obj.FatherJob = sdr["fatherJob"].ToString();
                        obj.MotherName = sdr["motherName"].ToString();
                        obj.MotherAge = sdr["motherAge"].ToString();
                        obj.MotherHeight = sdr["motherHeight"].ToString();
                        obj.MotherEducation = sdr["motherEducation"].ToString();
                        obj.MotherJob = sdr["motherJob"].ToString();
                        obj.Telephone = sdr["telephone"].ToString();
                        obj.address = sdr["address"].ToString();
                        obj.NurseryInstitution = sdr["nurseryInstitution"].ToString();//托幼机构
                        //发证
                        obj.ImmuneUnit = sdr["immuneUnit"].ToString();
                        obj.ImmuneDay = sdr["immuneDay"].ToString();
                        obj.Community = sdr["community"].ToString();
                        obj.CensusRegister = sdr["censusRegister"].ToString();
                        //出生史
                        obj.Cs_fetus = sdr["cs_fetus"].ToString();
                        obj.Cs_produce = sdr["cs_produce"].ToString();
                        obj.Cs_week = sdr["cs_week"].ToString();
                        obj.Cs_day = sdr["cs_day"].ToString();
                        obj.ModeDelivery = sdr["modeDelivery"].ToString();
                        obj.PerineumIncision = sdr["perineumIncision"].ToString();//会阴侧开
                        obj.FetusNumber = sdr["fetusNumber"].ToString();
                        obj.NeonatalCondition = sdr["neonatalCondition"].ToString();
                        obj.BirthWeight = sdr["birthWeight"].ToString();
                        obj.BirthHeight = sdr["birthHeight"].ToString();
                        obj.Birthaddress = sdr["birthaddress"].ToString();
                        //母乳喂养
                        obj.HospitalizedStates = sdr["hospitalizedStates"].ToString();
                        obj.OneMonth = sdr["oneMonth"].ToString();
                        obj.InFourMonth = sdr["inFourMonth"].ToString();
                        obj.FourToSixMonth = sdr["fourToSixMonth"].ToString();
                        obj.Yunqi = sdr["yunqi"].ToString();
                        obj.Identityno = sdr["identityno"].ToString();
                        obj.jiuzhenCardNo = sdr["jiuzhenCardNo"].ToString();
                        obj.linshi = sdr["linshi"].ToString();

                        //if (sdr["zhushi"] != null)
                        //{
                        //    obj.zhushi = sdr["zhushi"].ToString();
                        //}

                        //if (sdr["fuzenday"] != null)
                        //{
                        //    obj.fuzhenday = sdr["fuzenday"].ToString();
                        //}

                        obj.gaowei = sdr["gaoweiyinsu"].ToString();
                        obj.ispre = sdr["ispre"].ToString();
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
        /// 查询儿童建档列表
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getchildBaseList1(string sqls)
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
                        ChildBaseInfoObj obj = new ChildBaseInfoObj();
                        obj.Id = Convert.ToInt32(sdr["id"]);
                        obj.HealthCardNo = sdr["healthCardNo"].ToString();
                        obj.ChildName = sdr["childName"].ToString();
                        obj.ChildGender = sdr["childGender"].ToString();
                        obj.BloodType = sdr["bloodType"].ToString();
                        obj.ChildBirthDay = sdr["childBirthDay"].ToString();
                        obj.ChildBuildDay = sdr["childBuildDay"].ToString();
                        //保存图片  
                        //obj.ChildPhoto
                        //TODO


                        obj.ChildBuildHospital = sdr["childBuildHospital"].ToString();
                        obj.FatherName = sdr["fatherName"].ToString();
                        obj.FatherAge = sdr["fatherAge"].ToString();
                        obj.FatherHeight = sdr["fatherHeight"].ToString();
                        obj.FatherEducation = sdr["fatherEducation"].ToString();
                        obj.FatherJob = sdr["fatherJob"].ToString();
                        obj.MotherName = sdr["motherName"].ToString();
                        obj.MotherAge = sdr["motherAge"].ToString();
                        obj.MotherHeight = sdr["motherHeight"].ToString();
                        obj.MotherEducation = sdr["motherEducation"].ToString();
                        obj.MotherJob = sdr["motherJob"].ToString();
                        obj.Telephone = sdr["telephone"].ToString();
                        obj.address = sdr["address"].ToString();
                        obj.NurseryInstitution = sdr["nurseryInstitution"].ToString();//托幼机构
                        //发证
                        obj.ImmuneUnit = sdr["immuneUnit"].ToString();
                        obj.ImmuneDay = sdr["immuneDay"].ToString();
                        obj.Community = sdr["community"].ToString();
                        obj.CensusRegister = sdr["censusRegister"].ToString();
                        //出生史
                        obj.Cs_fetus = sdr["cs_fetus"].ToString();
                        obj.Cs_produce = sdr["cs_produce"].ToString();
                        obj.Cs_week = sdr["cs_week"].ToString();
                        obj.Cs_day = sdr["cs_day"].ToString();
                        obj.ModeDelivery = sdr["modeDelivery"].ToString();
                        obj.PerineumIncision = sdr["perineumIncision"].ToString();//会阴侧开
                        obj.FetusNumber = sdr["fetusNumber"].ToString();
                        obj.NeonatalCondition = sdr["neonatalCondition"].ToString();
                        obj.BirthWeight = sdr["birthWeight"].ToString();
                        obj.BirthHeight = sdr["birthHeight"].ToString();
                        obj.Birthaddress = sdr["birthaddress"].ToString();
                        //母乳喂养
                        obj.HospitalizedStates = sdr["hospitalizedStates"].ToString();
                        obj.OneMonth = sdr["oneMonth"].ToString();
                        obj.InFourMonth = sdr["inFourMonth"].ToString();
                        obj.FourToSixMonth = sdr["fourToSixMonth"].ToString();
                        obj.Yunqi = sdr["yunqi"].ToString();
                        obj.Identityno = sdr["identityno"].ToString();
                        obj.jiuzhenCardNo = sdr["jiuzhenCardNo"].ToString();
                        obj.linshi = sdr["linshi"].ToString();

                        obj.gaowei = sdr["gaoweiyinsu"].ToString();
                        obj.ispre = sdr["ispre"].ToString();
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
        /// 查询高危儿童建档列表
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getchildBaseGaoweiList(string sqls)
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
                        ChildBaseInfoObj obj = new ChildBaseInfoObj();
                        obj.Id = Convert.ToInt32(sdr["cdid"]);
                        obj.HealthCardNo = sdr["healthCardNo"].ToString();
                        obj.ChildName = sdr["childName"].ToString();
                        obj.ChildGender = sdr["childGender"].ToString();
                        obj.BloodType = sdr["bloodType"].ToString();
                        obj.ChildBirthDay = sdr["childBirthDay"].ToString();
                        obj.ChildBuildDay = sdr["childBuildDay"].ToString();
                        //保存图片  
                        //obj.ChildPhoto
                        //TODO


                        obj.ChildBuildHospital = sdr["childBuildHospital"].ToString();
                        obj.FatherName = sdr["fatherName"].ToString();
                        obj.FatherAge = sdr["fatherAge"].ToString();
                        obj.FatherHeight = sdr["fatherHeight"].ToString();
                        obj.FatherEducation = sdr["fatherEducation"].ToString();
                        obj.FatherJob = sdr["fatherJob"].ToString();
                        obj.MotherName = sdr["motherName"].ToString();
                        obj.MotherAge = sdr["motherAge"].ToString();
                        obj.MotherHeight = sdr["motherHeight"].ToString();
                        obj.MotherEducation = sdr["motherEducation"].ToString();
                        obj.MotherJob = sdr["motherJob"].ToString();
                        obj.Telephone = sdr["telephone"].ToString();
                        obj.address = sdr["address"].ToString();
                        obj.NurseryInstitution = sdr["nurseryInstitution"].ToString();//托幼机构
                        //发证
                        obj.ImmuneUnit = sdr["immuneUnit"].ToString();
                        obj.ImmuneDay = sdr["immuneDay"].ToString();
                        obj.Community = sdr["community"].ToString();
                        obj.CensusRegister = sdr["censusRegister"].ToString();
                        //出生史
                        obj.Cs_fetus = sdr["cs_fetus"].ToString();
                        obj.Cs_produce = sdr["cs_produce"].ToString();
                        obj.Cs_week = sdr["cs_week"].ToString();
                        obj.Cs_day = sdr["cs_day"].ToString();
                        obj.ModeDelivery = sdr["modeDelivery"].ToString();
                        obj.PerineumIncision = sdr["perineumIncision"].ToString();//会阴侧开
                        obj.FetusNumber = sdr["fetusNumber"].ToString();
                        obj.NeonatalCondition = sdr["neonatalCondition"].ToString();
                        obj.BirthWeight = sdr["birthWeight"].ToString();
                        obj.BirthHeight = sdr["birthHeight"].ToString();
                        obj.Birthaddress = sdr["birthaddress"].ToString();
                        //母乳喂养
                        obj.HospitalizedStates = sdr["hospitalizedStates"].ToString();
                        obj.OneMonth = sdr["oneMonth"].ToString();
                        obj.InFourMonth = sdr["inFourMonth"].ToString();
                        obj.FourToSixMonth = sdr["fourToSixMonth"].ToString();
                        obj.Yunqi = sdr["yunqi"].ToString();
                        obj.Identityno = sdr["identityno"].ToString();
                        obj.gaowei = sdr["gaoweiyinsu"].ToString();
                        obj.zhuangui = sdr["zhuangui"].ToString();

                        if (sdr["zhushi"] != null)
                        {
                            obj.zhushi = sdr["zhushi"].ToString();
                        }

                        if (sdr["fuzenday"] != null)
                        {
                            obj.fuzhenday = sdr["fuzenday"].ToString();
                        }
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
        /// 查询营养不良儿童建档列表
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getchildBaseYingyangList(string sqls)
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
                        ChildBaseInfoObj obj = new ChildBaseInfoObj();
                        obj.Id = Convert.ToInt32(sdr["cdid"]);
                        obj.HealthCardNo = sdr["healthCardNo"].ToString();
                        obj.ChildName = sdr["childName"].ToString();
                        obj.ChildGender = sdr["childGender"].ToString();
                        obj.BloodType = sdr["bloodType"].ToString();
                        obj.ChildBirthDay = sdr["childBirthDay"].ToString();
                        obj.ChildBuildDay = sdr["childBuildDay"].ToString();
                        //保存图片  
                        //obj.ChildPhoto
                        //TODO


                        obj.ChildBuildHospital = sdr["childBuildHospital"].ToString();
                        obj.FatherName = sdr["fatherName"].ToString();
                        obj.FatherAge = sdr["fatherAge"].ToString();
                        obj.FatherHeight = sdr["fatherHeight"].ToString();
                        obj.FatherEducation = sdr["fatherEducation"].ToString();
                        obj.FatherJob = sdr["fatherJob"].ToString();
                        obj.MotherName = sdr["motherName"].ToString();
                        obj.MotherAge = sdr["motherAge"].ToString();
                        obj.MotherHeight = sdr["motherHeight"].ToString();
                        obj.MotherEducation = sdr["motherEducation"].ToString();
                        obj.MotherJob = sdr["motherJob"].ToString();
                        obj.Telephone = sdr["telephone"].ToString();
                        obj.address = sdr["address"].ToString();
                        obj.NurseryInstitution = sdr["nurseryInstitution"].ToString();//托幼机构
                        //发证
                        obj.ImmuneUnit = sdr["immuneUnit"].ToString();
                        obj.ImmuneDay = sdr["immuneDay"].ToString();
                        obj.Community = sdr["community"].ToString();
                        obj.CensusRegister = sdr["censusRegister"].ToString();
                        //出生史
                        obj.Cs_fetus = sdr["cs_fetus"].ToString();
                        obj.Cs_produce = sdr["cs_produce"].ToString();
                        obj.Cs_week = sdr["cs_week"].ToString();
                        obj.Cs_day = sdr["cs_day"].ToString();
                        obj.ModeDelivery = sdr["modeDelivery"].ToString();
                        obj.PerineumIncision = sdr["perineumIncision"].ToString();//会阴侧开
                        obj.FetusNumber = sdr["fetusNumber"].ToString();
                        obj.NeonatalCondition = sdr["neonatalCondition"].ToString();
                        obj.BirthWeight = sdr["birthWeight"].ToString();
                        obj.BirthHeight = sdr["birthHeight"].ToString();
                        obj.Birthaddress = sdr["birthaddress"].ToString();
                        //母乳喂养
                        obj.HospitalizedStates = sdr["hospitalizedStates"].ToString();
                        obj.OneMonth = sdr["oneMonth"].ToString();
                        obj.InFourMonth = sdr["inFourMonth"].ToString();
                        obj.FourToSixMonth = sdr["fourToSixMonth"].ToString();
                        obj.Yunqi = sdr["yunqi"].ToString();
                        obj.Identityno = sdr["identityno"].ToString();
                        obj.yingyangbuliang = sdr["yingyangbuliang"].ToString();
                        obj.zhuangui = sdr["zhuangui"].ToString();
                        obj.managetime = sdr["managetime"].ToString();
                        obj.endtime = sdr["endtime"].ToString();

                        if (sdr["zhushi"] != null)
                        {
                            obj.zhushi = sdr["zhushi"].ToString();
                        }

                        if (sdr["fuzenday"] != null)
                        {
                            obj.fuzhenday = sdr["fuzenday"].ToString();
                        }
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
        /// 新增数据sql
        /// </summary>
        /// <returns></returns>
        public bool insertChildBaseInfo(ChildBaseInfoObj obj1)
        {
            DateLogic dg = new DateLogic();
            StringBuilder builder = new StringBuilder();
            SqlConnection conn = dg.getconn();
            
            using (SqlCommand cmd = new SqlCommand(builder.ToString(), conn))
            {
                builder.Append("insert into tb_childBase ");
                builder.Append("( ");
                //儿童
                builder.Append("healthCardNo");
                builder.Append(",childName");
                builder.Append(",childGender");
                builder.Append(",bloodType");
                builder.Append(",childBirthDay");
                builder.Append(",childBuildDay");
                builder.Append(",childBuildHospital");
                //builder.Append(",childPhoto");
                //父亲 母亲
                builder.Append(",fatherName");
                builder.Append(",fatherAge");
                builder.Append(",fatherHeight");
                builder.Append(",fatherEducation");
                builder.Append(",fatherJob");
                builder.Append(",motherName");
                builder.Append(",motherAge");
                builder.Append(",motherHeight");
                builder.Append(",motherEducation");
                builder.Append(",motherJob");
                builder.Append(",telephone");
                builder.Append(",telephone2");
                builder.Append(",address");
                builder.Append(",province");
                builder.Append(",city");
                builder.Append(",area");
                builder.Append(",nurseryInstitution");
                //发证
                //builder.Append(",immuneUnit");
                //builder.Append(",immuneDay");
                //builder.Append(",community");
                //builder.Append(",censusRegister");
                //出生史
                builder.Append(",cs_fetus");
                builder.Append(",cs_produce");
                builder.Append(",cs_week");
                builder.Append(",cs_day");
                builder.Append(",modeDelivery");
                builder.Append(",perineumIncision");
                //builder.Append(",fetusNumber");
                builder.Append(",neonatalCondition");
                builder.Append(",birthWeight");
                builder.Append(",birthHeight");
                builder.Append(",birthaddress");
                //母乳喂养
                builder.Append(",hospitalizedStates");
                builder.Append(",oneMonth");
                builder.Append(",inFourMonth");
                builder.Append(",fourToSixMonth");
                builder.Append(",identityno");
                builder.Append(",status");
                builder.Append(",jiuzhenCardNo");
                builder.Append(",linshi");
                //builder.Append(",gerenshi");
                //builder.Append(",jiwangshi");
                //builder.Append(",jiazushi");
                builder.Append(") values (");//todo

                builder.Append("@healthCardNo");
                builder.Append(",@childName");
                builder.Append(",@childGender");
                builder.Append(",@bloodType");
                builder.Append(",@childBirthDay");
                builder.Append(",@childBuildDay");
                builder.Append(",@childBuildHospital");
                //builder.Append(",@childPhoto");
                //父亲 母亲
                builder.Append(",@fatherName");
                builder.Append(",@fatherAge");
                builder.Append(",@fatherHeight");
                builder.Append(",@fatherEducation");
                builder.Append(",@fatherJob");
                builder.Append(",@motherName");
                builder.Append(",@motherAge");
                builder.Append(",@motherHeight");
                builder.Append(",@motherEducation");
                builder.Append(",@motherJob");
                builder.Append(",@telephone");
                builder.Append(",@telephone2");
                builder.Append(",@address");
                builder.Append(",@province");
                builder.Append(",@city");
                builder.Append(",@area");
                builder.Append(",@nurseryInstitution");
                //发证
                //builder.Append(",@immuneUnit");
                //builder.Append(",@immuneDay");
                //builder.Append(",@community");
                //builder.Append(",@censusRegister");
                //出生史
                builder.Append(",@cs_fetus");
                builder.Append(",@cs_produce");
                builder.Append(",@cs_week");
                builder.Append(",@cs_day");
                builder.Append(",@modeDelivery");
                builder.Append(",@perineumIncision");
                //builder.Append(",@fetusNumber");
                builder.Append(",@neonatalCondition");
                builder.Append(",@birthWeight");
                builder.Append(",@birthHeight");
                builder.Append(",@birthaddress");
                //母乳喂养
                builder.Append(",@hospitalizedStates");
                builder.Append(",@oneMonth");
                builder.Append(",@inFourMonth");
                builder.Append(",@fourToSixMonth");
                builder.Append(",@identityno");
                builder.Append(",@status");
                builder.Append(",@jiuzhenCardNo");
                builder.Append(",@linshi");
                //builder.Append(",@gerenshi");
                //builder.Append(",@jiwangshi");
                //builder.Append(",@jiazushi");
                //builder.Append(",@yunqi)");
                builder.Append(")");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = builder.ToString();

                cmd.Parameters.Add("@healthCardNo", SqlDbType.VarChar);
                cmd.Parameters.Add("@childName", SqlDbType.VarChar);
                cmd.Parameters.Add("@childGender", SqlDbType.VarChar);
                cmd.Parameters.Add("@bloodType", SqlDbType.VarChar);
                cmd.Parameters.Add("@childBirthDay", SqlDbType.VarChar);
                cmd.Parameters.Add("@childBuildDay", SqlDbType.VarChar);
                cmd.Parameters.Add("@childBuildHospital", SqlDbType.VarChar);
                //cmd.Parameters.Add("@childPhoto", SqlDbType.Image);
                //父亲 母亲
                cmd.Parameters.Add("@fatherName", SqlDbType.VarChar);
                cmd.Parameters.Add("@fatherAge", SqlDbType.VarChar);
                cmd.Parameters.Add("@fatherHeight", SqlDbType.VarChar);
                cmd.Parameters.Add("@fatherEducation", SqlDbType.VarChar);
                cmd.Parameters.Add("@fatherJob", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherName", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherAge", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherHeight", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherEducation", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherJob", SqlDbType.VarChar);
                cmd.Parameters.Add("@telephone", SqlDbType.VarChar);
                cmd.Parameters.Add("@telephone2", SqlDbType.VarChar);
                cmd.Parameters.Add("@address", SqlDbType.VarChar);
                cmd.Parameters.Add("@province", SqlDbType.VarChar);
                cmd.Parameters.Add("@city", SqlDbType.VarChar);
                cmd.Parameters.Add("@area", SqlDbType.VarChar);
                cmd.Parameters.Add("@nurseryInstitution", SqlDbType.VarChar);
                //发证
                //cmd.Parameters.Add("@immuneUnit", SqlDbType.VarChar);
                //cmd.Parameters.Add("@immuneDay", SqlDbType.VarChar);
                //cmd.Parameters.Add("@community", SqlDbType.VarChar);
                //cmd.Parameters.Add("@censusRegister", SqlDbType.VarChar);
                //出生史
                cmd.Parameters.Add("@cs_fetus", SqlDbType.VarChar);
                cmd.Parameters.Add("@cs_produce", SqlDbType.VarChar);
                cmd.Parameters.Add("@cs_week", SqlDbType.VarChar);
                cmd.Parameters.Add("@cs_day", SqlDbType.VarChar);
                cmd.Parameters.Add("@modeDelivery", SqlDbType.VarChar);
                cmd.Parameters.Add("@perineumIncision", SqlDbType.VarChar);
                //cmd.Parameters.Add("@fetusNumber", SqlDbType.VarChar);
                cmd.Parameters.Add("@neonatalCondition", SqlDbType.VarChar);
                cmd.Parameters.Add("@birthWeight", SqlDbType.VarChar);
                cmd.Parameters.Add("@birthHeight", SqlDbType.VarChar);
                cmd.Parameters.Add("@birthaddress", SqlDbType.VarChar);
                //母乳喂养
                cmd.Parameters.Add("@hospitalizedStates", SqlDbType.VarChar);
                cmd.Parameters.Add("@oneMonth", SqlDbType.VarChar);
                cmd.Parameters.Add("@inFourMonth", SqlDbType.VarChar);
                cmd.Parameters.Add("@fourToSixMonth", SqlDbType.VarChar);
                cmd.Parameters.Add("@identityno", SqlDbType.VarChar);
                cmd.Parameters.Add("@jiuzhenCardno", SqlDbType.VarChar);
                cmd.Parameters.Add("@linshi", SqlDbType.VarChar);
                //cmd.Parameters.Add("@gerenshi", SqlDbType.VarChar);
                //cmd.Parameters.Add("@jiazushi", SqlDbType.VarChar);
                //cmd.Parameters.Add("@jiwangshi", SqlDbType.VarChar);
                cmd.Parameters.Add("@status", SqlDbType.VarChar);
                //cmd.Parameters.Add("@yunqi", SqlDbType.VarChar);

                //=============赋值===============
                cmd.Parameters["@healthCardNo"].Value = obj1.HealthCardNo;
                cmd.Parameters["@childName"].Value = obj1.ChildName;
                cmd.Parameters["@childGender"].Value = obj1.ChildGender;
                cmd.Parameters["@bloodType"].Value = obj1.BloodType;
                cmd.Parameters["@childBirthDay"].Value = obj1.ChildBirthDay;
                cmd.Parameters["@childBuildDay"].Value = obj1.ChildBuildDay;
                cmd.Parameters["@childBuildHospital"].Value = obj1.ChildBuildHospital;

                //cmd.Parameters["@childPhoto"].Value = imageBytes;//存入图片
                //父亲 母亲
                cmd.Parameters["@fatherName"].Value = obj1.FatherName;
                cmd.Parameters["@fatherAge"].Value = obj1.FatherAge;
                cmd.Parameters["@fatherHeight"].Value = obj1.FatherHeight;
                cmd.Parameters["@fatherEducation"].Value = obj1.FatherEducation;
                cmd.Parameters["@fatherJob"].Value = obj1.FatherJob;
                cmd.Parameters["@motherName"].Value = obj1.MotherName;
                cmd.Parameters["@motherAge"].Value = obj1.MotherAge;
                cmd.Parameters["@motherHeight"].Value = obj1.MotherHeight;
                cmd.Parameters["@motherEducation"].Value = obj1.MotherEducation;
                cmd.Parameters["@motherJob"].Value = obj1.MotherJob;
                cmd.Parameters["@telephone"].Value = obj1.Telephone;
                cmd.Parameters["@telephone2"].Value = obj1.Telephone2;
                cmd.Parameters["@address"].Value = obj1.address;
                cmd.Parameters["@province"].Value = obj1.province;
                cmd.Parameters["@city"].Value = obj1.city;
                cmd.Parameters["@area"].Value = obj1.area;
                cmd.Parameters["@nurseryInstitution"].Value = obj1.NurseryInstitution;
                //发证
                //cmd.Parameters["@immuneUnit"].Value = obj1.ImmuneUnit;
                //cmd.Parameters["@immuneDay"].Value = obj1.ImmuneDay;
                //cmd.Parameters["@community"].Value = obj1.Community;
                //cmd.Parameters["@censusRegister"].Value = obj1.CensusRegister;
                //出生史
                cmd.Parameters["@cs_fetus"].Value = obj1.Cs_fetus;
                cmd.Parameters["@cs_produce"].Value = obj1.Cs_produce;
                cmd.Parameters["@cs_week"].Value = obj1.Cs_week;
                cmd.Parameters["@cs_day"].Value = obj1.Cs_day;
                cmd.Parameters["@modeDelivery"].Value = obj1.ModeDelivery;
                cmd.Parameters["@perineumIncision"].Value = obj1.PerineumIncision;
                //cmd.Parameters["@fetusNumber"].Value = obj1.FetusNumber;
                cmd.Parameters["@neonatalCondition"].Value = obj1.NeonatalCondition;
                cmd.Parameters["@birthWeight"].Value = obj1.BirthWeight;
                cmd.Parameters["@birthHeight"].Value = obj1.BirthHeight;
                cmd.Parameters["@birthaddress"].Value = obj1.Birthaddress;
                //母乳喂养
                cmd.Parameters["@hospitalizedStates"].Value = obj1.HospitalizedStates;
                cmd.Parameters["@oneMonth"].Value = obj1.OneMonth;
                cmd.Parameters["@inFourMonth"].Value = obj1.InFourMonth;
                cmd.Parameters["@fourToSixMonth"].Value = obj1.FourToSixMonth;
                //cmd.Parameters["@yunqi"].Value = obj1.Yunqi;
                cmd.Parameters["@identityno"].Value = obj1.Identityno;
                cmd.Parameters["@jiuzhenCardNo"].Value = obj1.jiuzhenCardNo;
                cmd.Parameters["@linshi"].Value = obj1.linshi;
                //cmd.Parameters["@gerenshi"].Value = obj1.gerenshi;
                //cmd.Parameters["@jiwangshi"].Value = obj1.jiwangshi;
                //cmd.Parameters["@jiazushi"].Value = obj1.jiazushi;
                cmd.Parameters["@status"].Value = obj1.status;

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        /// <summary>
        /// 修改数据sql
        /// </summary>
        /// <returns></returns>
        public bool updateChildBaseInfo(ChildBaseInfoObj obj1)
        {
            DateLogic dg = new DateLogic();
            StringBuilder builder = new StringBuilder();
            SqlConnection conn = dg.getconn();

            using (SqlCommand cmd = new SqlCommand(builder.ToString(), conn))
            {
                builder.Append("update  tb_childBase set ");
                //儿童
                builder.Append("childName = @childName");
                builder.Append(",childGender = @childGender");
                builder.Append(",bloodType = @bloodType");
                builder.Append(",childBirthDay = @childBirthDay");
                builder.Append(",childBuildDay = @childBuildDay");
                builder.Append(",childBuildHospital = @childBuildHospital");
                //builder.Append(",childPhoto = @");
                //父亲 母亲
                builder.Append(",fatherName = @fatherName");
                builder.Append(",fatherAge = @fatherAge");
                builder.Append(",fatherHeight = @fatherHeight");
                builder.Append(",fatherEducation = @fatherEducation");
                builder.Append(",fatherJob = @fatherJob");
                builder.Append(",motherName = @motherName");
                builder.Append(",motherAge = @motherAge");
                builder.Append(",motherHeight = @motherHeight");
                builder.Append(",motherEducation = @motherEducation");
                builder.Append(",motherJob = @motherJob");
                builder.Append(",telephone = @telephone");
                builder.Append(",telephone2 = @telephone2");
                builder.Append(",address = @address");
                builder.Append(",province = @province");
                builder.Append(",city = @city");
                builder.Append(",area = @area");
                builder.Append(",nurseryInstitution = @nurseryInstitution");
                //发证
                //builder.Append(",immuneUnit = @");
                //builder.Append(",immuneDay = @");
                //builder.Append(",community = @");
                //builder.Append(",censusRegister = @");
                //出生史
                builder.Append(",cs_fetus = @cs_fetus");
                builder.Append(",cs_produce = @cs_produce");
                builder.Append(",cs_week = @cs_week");
                builder.Append(",cs_day = @cs_day");
                builder.Append(",modeDelivery = @modeDelivery");
                builder.Append(",perineumIncision = @perineumIncision");
                //builder.Append(",fetusNumber = @");
                builder.Append(",neonatalCondition = @neonatalCondition");
                builder.Append(",birthWeight = @birthWeight");
                builder.Append(",birthHeight = @birthHeight");
                builder.Append(",birthaddress = @birthaddress");
                //母乳喂养
                builder.Append(",hospitalizedStates = @hospitalizedStates");
                builder.Append(",oneMonth = @oneMonth");
                builder.Append(",inFourMonth = @inFourMonth");
                builder.Append(",fourToSixMonth = @fourToSixMonth");
                builder.Append(",identityno = @identityno");
                builder.Append(",jiuzhenCardNo = @jiuzhenCardNo");
                builder.Append(",linshi = @linshi");
                //builder.Append(",gerenshi = @gerenshi");
                //builder.Append(",jiwangshi = @jiwangshi");
                //builder.Append(",jiazushi = @jiazushi");
                builder.Append(" where  id = @id");
                
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = builder.ToString();

                cmd.Parameters.Add("@childName", SqlDbType.VarChar);
                cmd.Parameters.Add("@childGender", SqlDbType.VarChar);
                cmd.Parameters.Add("@bloodType", SqlDbType.VarChar);
                cmd.Parameters.Add("@childBirthDay", SqlDbType.VarChar);
                cmd.Parameters.Add("@childBuildDay", SqlDbType.VarChar);
                cmd.Parameters.Add("@childBuildHospital", SqlDbType.VarChar);
                //cmd.Parameters.Add("@childPhoto", SqlDbType.Image);
                //父亲 母亲
                cmd.Parameters.Add("@fatherName", SqlDbType.VarChar);
                cmd.Parameters.Add("@fatherAge", SqlDbType.VarChar);
                cmd.Parameters.Add("@fatherHeight", SqlDbType.VarChar);
                cmd.Parameters.Add("@fatherEducation", SqlDbType.VarChar);
                cmd.Parameters.Add("@fatherJob", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherName", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherAge", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherHeight", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherEducation", SqlDbType.VarChar);
                cmd.Parameters.Add("@motherJob", SqlDbType.VarChar);
                cmd.Parameters.Add("@telephone", SqlDbType.VarChar);
                cmd.Parameters.Add("@telephone2", SqlDbType.VarChar);
                cmd.Parameters.Add("@address", SqlDbType.VarChar);
                cmd.Parameters.Add("@province", SqlDbType.VarChar);
                cmd.Parameters.Add("@city", SqlDbType.VarChar);
                cmd.Parameters.Add("@area", SqlDbType.VarChar);
                cmd.Parameters.Add("@nurseryInstitution", SqlDbType.VarChar);
                //发证
                //cmd.Parameters.Add("@immuneUnit", SqlDbType.VarChar);
                //cmd.Parameters.Add("@immuneDay", SqlDbType.VarChar);
                //cmd.Parameters.Add("@community", SqlDbType.VarChar);
                //cmd.Parameters.Add("@censusRegister", SqlDbType.VarChar);
                //出生史
                cmd.Parameters.Add("@cs_fetus", SqlDbType.VarChar);
                cmd.Parameters.Add("@cs_produce", SqlDbType.VarChar);
                cmd.Parameters.Add("@cs_week", SqlDbType.VarChar);
                cmd.Parameters.Add("@cs_day", SqlDbType.VarChar);
                cmd.Parameters.Add("@modeDelivery", SqlDbType.VarChar);
                cmd.Parameters.Add("@perineumIncision", SqlDbType.VarChar);
                //cmd.Parameters.Add("@fetusNumber", SqlDbType.VarChar);
                cmd.Parameters.Add("@neonatalCondition", SqlDbType.VarChar);
                cmd.Parameters.Add("@birthWeight", SqlDbType.VarChar);
                cmd.Parameters.Add("@birthHeight", SqlDbType.VarChar);
                cmd.Parameters.Add("@birthaddress", SqlDbType.VarChar);
                //母乳喂养
                cmd.Parameters.Add("@hospitalizedStates", SqlDbType.VarChar);
                cmd.Parameters.Add("@oneMonth", SqlDbType.VarChar);
                cmd.Parameters.Add("@inFourMonth", SqlDbType.VarChar);
                cmd.Parameters.Add("@fourToSixMonth", SqlDbType.VarChar);
                cmd.Parameters.Add("@identityno", SqlDbType.VarChar);
                cmd.Parameters.Add("@jiuzhenCardno", SqlDbType.VarChar);
                cmd.Parameters.Add("@linshi", SqlDbType.VarChar);
                //cmd.Parameters.Add("@gerenshi", SqlDbType.VarChar);
                //cmd.Parameters.Add("@jiazushi", SqlDbType.VarChar);
                //cmd.Parameters.Add("@jiwangshi", SqlDbType.VarChar);
                cmd.Parameters.Add("@id", SqlDbType.Int);

                //=============赋值===============
                cmd.Parameters["@childName"].Value = obj1.ChildName;
                cmd.Parameters["@childGender"].Value = obj1.ChildGender;
                cmd.Parameters["@bloodType"].Value = obj1.BloodType;
                cmd.Parameters["@childBirthDay"].Value = obj1.ChildBirthDay;
                cmd.Parameters["@childBuildDay"].Value = obj1.ChildBuildDay;
                cmd.Parameters["@childBuildHospital"].Value = obj1.ChildBuildHospital;

                //cmd.Parameters["@childPhoto"].Value = imageBytes;//存入图片
                //父亲 母亲
                cmd.Parameters["@fatherName"].Value = obj1.FatherName;
                cmd.Parameters["@fatherAge"].Value = obj1.FatherAge;
                cmd.Parameters["@fatherHeight"].Value = obj1.FatherHeight;
                cmd.Parameters["@fatherEducation"].Value = obj1.FatherEducation;
                cmd.Parameters["@fatherJob"].Value = obj1.FatherJob;
                cmd.Parameters["@motherName"].Value = obj1.MotherName;
                cmd.Parameters["@motherAge"].Value = obj1.MotherAge;
                cmd.Parameters["@motherHeight"].Value = obj1.MotherHeight;
                cmd.Parameters["@motherEducation"].Value = obj1.MotherEducation;
                cmd.Parameters["@motherJob"].Value = obj1.MotherJob;
                cmd.Parameters["@telephone"].Value = obj1.Telephone;
                cmd.Parameters["@telephone2"].Value = obj1.Telephone2;
                cmd.Parameters["@address"].Value = obj1.address;
                cmd.Parameters["@province"].Value = obj1.province;
                cmd.Parameters["@city"].Value = obj1.city;
                cmd.Parameters["@area"].Value = obj1.area;
                cmd.Parameters["@nurseryInstitution"].Value = obj1.NurseryInstitution;
                //发证
                //cmd.Parameters["@immuneUnit"].Value = obj1.ImmuneUnit;
                //cmd.Parameters["@immuneDay"].Value = obj1.ImmuneDay;
                //cmd.Parameters["@community"].Value = obj1.Community;
                //cmd.Parameters["@censusRegister"].Value = obj1.CensusRegister;
                //出生史
                cmd.Parameters["@cs_fetus"].Value = obj1.Cs_fetus;
                cmd.Parameters["@cs_produce"].Value = obj1.Cs_produce;
                cmd.Parameters["@cs_week"].Value = obj1.Cs_week;
                cmd.Parameters["@cs_day"].Value = obj1.Cs_day;
                cmd.Parameters["@modeDelivery"].Value = obj1.ModeDelivery;
                cmd.Parameters["@perineumIncision"].Value = obj1.PerineumIncision;
                //cmd.Parameters["@fetusNumber"].Value = obj1.FetusNumber;
                cmd.Parameters["@neonatalCondition"].Value = obj1.NeonatalCondition;
                cmd.Parameters["@birthWeight"].Value = obj1.BirthWeight;
                cmd.Parameters["@birthHeight"].Value = obj1.BirthHeight;
                cmd.Parameters["@birthaddress"].Value = obj1.Birthaddress;
                //母乳喂养
                cmd.Parameters["@hospitalizedStates"].Value = obj1.HospitalizedStates;
                cmd.Parameters["@oneMonth"].Value = obj1.OneMonth;
                cmd.Parameters["@inFourMonth"].Value = obj1.InFourMonth;
                cmd.Parameters["@fourToSixMonth"].Value = obj1.FourToSixMonth;
                //cmd.Parameters["@yunqi"].Value = obj1.Yunqi;
                cmd.Parameters["@identityno"].Value = obj1.Identityno;
                cmd.Parameters["@jiuzhenCardNo"].Value = obj1.jiuzhenCardNo;
                cmd.Parameters["@linshi"].Value = obj1.linshi;
                //cmd.Parameters["@gerenshi"].Value = obj1.gerenshi;
                //cmd.Parameters["@jiwangshi"].Value = obj1.jiwangshi;
                //cmd.Parameters["@jiazushi"].Value = obj1.jiazushi;
                cmd.Parameters["@id"].Value = obj1.Id;

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        //是否存在记录
        public bool isExist(string cname, string Identityno)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(1) from tb_childBase");
            sql.Append(" where ");
            sql.Append(" childName = @childName and Identityno=@Identityno ");
            SqlParameter[] pms = {
                new SqlParameter("@childName",cname),
                new SqlParameter("@Identityno",Identityno)
            };
            return (int)DateLogic.ExecuteScalar(sql.ToString(), pms) > 0;
        }



        /// <summary>
        /// 修改数据sql
        /// </summary>
        /// <returns></returns>
        public bool updatejiben(ChildBaseInfoObj obj1)
        {
            DateLogic dg = new DateLogic();
            StringBuilder builder = new StringBuilder();
            SqlConnection conn = dg.getconn();
            using (SqlCommand cmd = new SqlCommand(builder.ToString(), conn))
            {
                builder.Append("update  tb_childBase set ");
                builder.Append("status = @status");
                builder.Append(" where  id = @id");
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = builder.ToString();
                cmd.Parameters.Add("@status", SqlDbType.VarChar);
                cmd.Parameters.Add("@id", SqlDbType.Int);

                cmd.Parameters["@status"].Value = obj1.status;
                cmd.Parameters["@id"].Value = obj1.Id;

                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }




    }
}
