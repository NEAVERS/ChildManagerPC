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

namespace YCF.BLL.Template
{
    public partial class tab_ysf_xdnlbll : BLLBase<TAB_YSF_XDNL, tab_ysf_xdnldal>
    {

        public string Getfs_xdnl(string yuansf)
        {
            string zhi = "";

            IList<TAB_YSF_XDNL> list = dal.GetList();

            foreach (var item in list)
            {

                if (!String.IsNullOrEmpty(item.YSF_FS))
                {
                    if (item.YSF_FS.Contains("-"))
                    {
                        string[] objysf = item.YSF_FS.Split('-');
                        if (Convert.ToSingle(yuansf) >= Convert.ToSingle(objysf[0]) && Convert.ToSingle(yuansf) <= Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;

                        }

                    }
                    //else if (item.YSF_FS.Contains(">"))
                    //{
                    //    string[] objysf = item.YSF_FS.Split('>');
                    //    if (num2 > Convert.ToSingle(objysf[1]))
                    //    {
                    //        bfw.Text = item.bfw;
                    //        bzf.Text = item.bzf;
                    //        bzf1.Text = item.bzf;

                    //    }
                    //}
                    else
                    {
                        if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.YSF_FS))
                        {
                            zhi = item.XDNL;
                        }
                    }
                }
            }

            return zhi;
        }

        public string Getzs_xdnl(string yuansf)
        {
            string zhi = "";

            IList<TAB_YSF_XDNL> list = dal.GetList();

            foreach (var item in list)
            {

                if (!String.IsNullOrEmpty(item.YSF_ZS))
                {
                    if (item.YSF_ZS.Contains("-"))
                    {
                        string[] objysf = item.YSF_ZS.Split('-');
                        if (Convert.ToSingle(yuansf) >= Convert.ToSingle(objysf[0]) && Convert.ToSingle(yuansf) <= Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;

                        }

                    }
                    else if (item.YSF_ZS.Contains(">"))
                    {
                        string[] objysf = item.YSF_ZS.Split('>');
                        if (Convert.ToSingle(yuansf) > Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;
                        }
                    }
                    else
                    {
                        if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.YSF_ZS))
                        {
                            zhi = item.XDNL;
                        }
                    }
                }
            }

            return zhi;
        }

        public string Getyd_xdnl(string yuansf)
        {
            string zhi = "";

            IList<TAB_YSF_XDNL> list = dal.GetList();

            foreach (var item in list)
            {

                if (!String.IsNullOrEmpty(item.YSF_YD))
                {
                    if (item.YSF_YD.Contains("-"))
                    {
                        string[] objysf = item.YSF_YD.Split('-');
                        if (Convert.ToSingle(yuansf) >= Convert.ToSingle(objysf[0]) && Convert.ToSingle(yuansf) <= Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;

                        }

                    }
                    else if (item.YSF_YD.Contains(">"))
                    {
                        string[] objysf = item.YSF_YD.Split('>');
                        if (Convert.ToSingle(yuansf) > Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;
                        }
                    }
                    else
                    {
                        if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.YSF_YD))
                        {
                            zhi = item.XDNL;
                        }
                    }
                }
            }

            return zhi;
        }

        public string Getswcz_xdnl(string yuansf)
        {
            string zhi = "";

            IList<TAB_YSF_XDNL> list = dal.GetList();

            foreach (var item in list)
            {

                if (!String.IsNullOrEmpty(item.YSF_SWCZ))
                {
                    if (item.YSF_SWCZ.Contains("-"))
                    {
                        string[] objysf = item.YSF_SWCZ.Split('-');
                        if (Convert.ToSingle(yuansf) >= Convert.ToSingle(objysf[0]) && Convert.ToSingle(yuansf) <= Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;

                        }

                    }
                    else if (item.YSF_SWCZ.Contains(">"))
                    {
                        string[] objysf = item.YSF_SWCZ.Split('>');
                        if (Convert.ToSingle(yuansf) > Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;
                        }
                    }
                    else
                    {
                        if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.YSF_SWCZ))
                        {
                            zhi = item.XDNL;
                        }
                    }
                }
            }

            return zhi;
        }


        public string Getzw_xdnl(string yuansf)
        {
            string zhi = "";

            IList<TAB_YSF_XDNL> list = dal.GetList();

            foreach (var item in list)
            {

                if (!String.IsNullOrEmpty(item.YSF_ZW))
                {
                    if (item.YSF_ZW.Contains("-"))
                    {
                        string[] objysf = item.YSF_ZW.Split('-');
                        if (Convert.ToSingle(yuansf) >= Convert.ToSingle(objysf[0]) && Convert.ToSingle(yuansf) <= Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;

                        }

                    }
                    else if (item.YSF_ZW.Contains(">"))
                    {
                        string[] objysf = item.YSF_ZW.Split('>');
                        if (Convert.ToSingle(yuansf) > Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;
                        }
                    }
                    else
                    {
                        if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.YSF_ZW))
                        {
                            zhi = item.XDNL;
                        }
                    }
                }
            }

            return zhi;
        }
        public string Getsj_xdnl(string yuansf)
        {
            string zhi = "";

            IList<TAB_YSF_XDNL> list = dal.GetList();

            foreach (var item in list)
            {

                if (!String.IsNullOrEmpty(item.YSF_SJYD))
                {
                    if (item.YSF_SJYD.Contains("-"))
                    {
                        string[] objysf = item.YSF_SJYD.Split('-');
                        if (Convert.ToSingle(yuansf) >= Convert.ToSingle(objysf[0]) && Convert.ToSingle(yuansf) <= Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;

                        }

                    }
                    else if (item.YSF_SJYD.Contains(">"))
                    {
                        string[] objysf = item.YSF_SJYD.Split('>');
                        if (Convert.ToSingle(yuansf) > Convert.ToSingle(objysf[1]))
                        {
                            zhi = item.XDNL;
                        }
                    }
                    else
                    {
                        if (Convert.ToSingle(yuansf) == Convert.ToSingle(item.YSF_SJYD))
                        {
                            zhi = item.XDNL;
                        }
                    }
                }
            }

            return zhi;
        }

    }
}
