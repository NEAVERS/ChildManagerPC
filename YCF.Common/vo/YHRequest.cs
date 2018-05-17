using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YCF.Common.vo
{
   public class YHRequest
    {
        public static String item_chip = "CARD_CHIP_NO";
        public static String item_date = "VISIT_DATE";
        public static String item_visit_no = "VISIT_CARD_NO";
        public static String item_patient = "PAT_INDEX_NO";
        public static String item_regist = "REGIST_DEPT_NAME";
        public static String item_sex = "PHYSI_SEX_NAME";
        public static String item_name = "PAT_NAME";
        public static String item_birth = "DATE_BIRTH";

        public static String compy_equals = " = ";
        public static String compy_in = " in ";
        public static String compy_like   = " LIKE ";
        public static String compy_lt_equals = " &lt;= ";
        public static String compy_gt_equals = " &gt;= ";
        public static String compy_gt = " &gt; ";
        public static String compy_lt = " &lt; ";

        public static String splice_and = "AND";
        public static String splice_or = "OR";

        public static String fid_get_hischip = "BS10016";
        public static String fid_get_nicu = "BS10017";
        public static String fid_get_hisuser = "MS02004";
        public static String fid_get_hisregist = "BS10006";
        public static String fid_get_hisregist1 = "BS10019";
        public static String fid_get_hisjianyan = "BS20003";
        public static String fid_get_hisjianyan_detail = "BS20004";
        public static String fid_get_hisjiancha = "BS20001";
        public static String fid_get_hischaosheng = "BS20009";
        public static String fid_get_hisshenqin = "BS35006";
        public static String fid_get_patient_chufang = "BS35003";//病人处方

        public String Fid { get; set;}

        private IList<String> queryList = new List<String>();

        public YHRequest(String fid)
        {
            this.Fid = fid;
        }

        public void addQuery(String item, String compy, String value, String splice)
        {
            queryList.Add(" <query item=\"" + item 
                + "\" compy=\"" + compy 
                + "\" value=\" " + value 
                + " \" splice=\" " + splice + " \"/> ");
        }

        public String getQueryXml() {
            StringBuilder xml = new StringBuilder();
            xml.Append("<ESBEntry>");
            xml.Append("<AccessControl>");//
            xml.Append("<SysFlag>1</SysFlag>");
            xml.Append("<UserName>EB</UserName>");
            xml.Append("<Password>123456</Password>");
            xml.Append("<Fid>").Append(this.Fid).Append("</Fid>");
            xml.Append("</AccessControl>");
            xml.Append("<MessageHeader>");
            xml.Append("<Fid>").Append(this.Fid).Append("</Fid>");
            xml.Append("<SourceSysCode>S36</SourceSysCode>");
            xml.Append("<TargetSysCode>S00</TargetSysCode>");
            xml.Append("<MsgDate></MsgDate>");
            xml.Append("</MessageHeader>");
            xml.Append("<RequestOption>");
            xml.Append("<triggerData>0</triggerData>");
            xml.Append("<dataAmount>5</dataAmount>");
            xml.Append("</RequestOption>");
            xml.Append("<MsgInfo flag=\"1\">");
            xml.Append("<Msg></Msg>");
            xml.Append("<distinct value=\"0\"/>");
            if (queryList != null) {
                foreach (String query in queryList) {
                    xml.Append(query);
                }
            }
            xml.Append("</MsgInfo>");
            xml.Append("<GroupInfo flag=\"0\">");
            xml.Append("<AS ID=\"\" linkField=\"\"/>");
            xml.Append("</GroupInfo>");
            xml.Append("</ESBEntry>");
            return xml.ToString();
        }
    }
}
