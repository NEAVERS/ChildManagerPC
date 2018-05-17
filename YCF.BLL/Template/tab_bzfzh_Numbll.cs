using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using YCF.DAL;
using YCF.Model;
using YCF.DAL.Base;
using YCF.DAL.Template;
using System.Data.SqlClient;
using YCF.BLL.Base;
using YCF.Common;

namespace YCF.BLL.Template
{
    public partial class tab_bzfzh_Numbll : BLLBase<TAB_BZFZH_NUM, tab_bzfzh_Numdal>
    {

        public string Getfs_sange(string yuansf)
        {
            string zhi = "";


            IList<TAB_BZFZH_NUM> list = dal.GetList();

            foreach (var item in list)
            {
                string nullas = item.BZF_CDYD;
                if (!String.IsNullOrEmpty(item.BZF_CDYD))
                {
                    if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.BZF_CDYD))
                    {

                        zhi = item.TO_S + "," + item.TO_BFW;
                    }
                    else if (item.BZF_CDYD.Contains("<"))
                    {
                        string[] objysf = item.BZF_CDYD.Split('<');
                        if (Convert.ToSingle(yuansf) < Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.TO_S + "," + item.TO_BFW;
                        }
                    }
                    else
                    {

                    }
                }
            }

            return zhi;
        }

        public string Getjx_sange(string yuansf)
        {
            string zhi = "";


            IList<TAB_BZFZH_NUM> list = dal.GetList();

            foreach (var item in list)
            {

                if (!String.IsNullOrEmpty(item.BZF_JXYD))
                {
                    if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.BZF_JXYD))
                    {

                        zhi = item.TO_S + "," + item.TO_BFW;
                    }
                    else if (item.BZF_JXYD.Contains("<"))
                    {
                        string[] objysf = item.BZF_JXYD.Split('<');
                        if (Convert.ToSingle(yuansf) < Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.TO_S + "," + item.TO_BFW;
                        }
                    }
                    else
                    {

                    }
                }
            }

            return zhi;
        }

        public string Getz_sange(string yuansf)
        {
            string zhi = "";


            IList<TAB_BZFZH_NUM> list = dal.GetList();

            foreach (var item in list)
            {

                if (!String.IsNullOrEmpty(item.BZF_ZYD))
                {
                    if (item.BZF_ZYD.Contains(">"))
                    {
                        string[] objysf = item.BZF_ZYD.Split('>');
                        if (Convert.ToSingle(yuansf) > Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.TO_S + "," + item.TO_BFW;
                        }
                    }
                    else if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.BZF_ZYD))
                    {

                        zhi = item.TO_S + "," + item.TO_BFW;
                    }
                    else
                    {

                    }
                }
            }

            return zhi;
        }
    }
}
