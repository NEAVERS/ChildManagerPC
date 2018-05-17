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
using login.Hospital.DAL;

namespace ChildManager.BLL
{
    class hisRegistBll
    {
        HosDateLogic dg = new HosDateLogic();
        
        /// <summary>
        /// 查询挂号基本信息
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public ArrayList getRegistList(string sqls)
        {
            SqlDataReader sdr = null;
            ArrayList arraylist = new ArrayList();
            try
            {
                sdr = dg.executequery(sqls);
                hisRegistObj obj = null;
                if (!sdr.HasRows)
                {
                    return null;
                }
                else
                {
                    while (sdr.Read())
                    {
                        obj = new hisRegistObj();
                        obj.cardno = sdr["cardno"].ToString();
                        obj.visit_number = sdr["visit_number"].ToString();
                        obj.patient_name = sdr["patient_name"].ToString();
                        obj.regist_time = sdr["regist_time"].ToString();
                        obj.identityno = sdr["identityno"].ToString();
                        obj.dept_code = sdr["dept_code"].ToString();
                        obj.dept_name = sdr["dept_name"].ToString();
                        obj.contact_phone = sdr["contact_phone"].ToString();
                        obj.sex = sdr["sex"].ToString();
                        obj.age = sdr["age"].ToString();
                        obj.address = sdr["address"].ToString();
                        obj.doctor_name = sdr["doctor_name"].ToString();
                        obj.remark = sdr["remark"].ToString();
                        obj.birth_date = sdr["birth_date"].ToString();
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
        
    }
}
