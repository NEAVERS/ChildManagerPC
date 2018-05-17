using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using ChildManager.UI.jibenluru;
using System.Collections;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.Model.ChildBaseInfo;
using YCF.Common;
using login.Hospital.DAL;
using System.Data.SqlClient;

namespace ChildManager.UI
{
    public partial class Paneltsb_readCard : Office2007Form
    {
        PanelyibanxinxiMain _panelyibanxinximain = null;
        ChildBaseInfoBll bll = new ChildBaseInfoBll();
        public Paneltsb_readCard(PanelyibanxinxiMain panelyibanxinxi)
        {
            InitializeComponent();
            _panelyibanxinximain = panelyibanxinxi;
            this.txtNo.Focus();
        }

        private void txtNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                string sqls = string.Format("select * from TB_CHILDBASE where status='1' ");
                if (!String.IsNullOrEmpty(txtNo.Text))
                {
                    sqls += " and jiuzhenCardNo like '%" + txtNo.Text + "%'";
                }
                else
                {
                    return;
                }

                ArrayList list = bll.getchildBaseListTwo(sqls);
                if (list != null && list.Count > 0)
                {
                    ChildBaseInfoObj obj = list[0] as ChildBaseInfoObj;
                    int id = obj.Id;
                    globalInfoClass.Wm_Index = id;
                    _panelyibanxinximain.obj = obj;
                    _panelyibanxinximain.RefreshCode();
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    SqlDataReader sdr = null;
                    HosDateLogic dg = new HosDateLogic();
                    String usersql = "select visit_number,patient_name,regist_time,identityno,contact_phone,sex,age,address,birthtime from v_yyt_registration where cardnum='" + txtNo.Text.Trim() + "' order by regist_time desc";
                    try
                    {
                        sdr = dg.executequery(usersql);

                        if (!sdr.HasRows)
                        {
                            MessageBox.Show("检索不到病人就诊信息！", "系统提示");
                            //textBoxX20.Focus();
                            return;
                        }
                        else
                        {
                            sdr.Read();
                            ChildBaseInfoObj obj = new ChildBaseInfoObj();
                            obj.ChildName = sdr["patient_name"].ToString().Trim();
                            obj.ChildGender = sdr["sex"].ToString().Trim();
                            obj.jiuzhenCardNo = txtNo.Text.Trim();
                            if (!String.IsNullOrEmpty(sdr["birthtime"].ToString()))
                            {
                                obj.childBirthDay = sdr["birthtime"].ToString();
                            }
                            sqls = "select * from TB_CHILDBASE where status='1' and childName like '%" + obj.ChildName + "%'";
                            list = bll.getchildBaseListTwo(sqls);
                            if (list != null && list.Count > 0)
                            {
                                if (list.Count == 1)
                                {
                                    obj = list[0] as ChildBaseInfoObj;
                                    if (String.IsNullOrEmpty(obj.jiuzhenCardNo))
                                    {
                                        obj.jiuzhenCardNo = txtNo.Text.Trim();
                                    }
                                    else
                                    {
                                        obj.jiuzhenCardNo += "," + txtNo.Text.Trim();
                                    }
                                    int id = obj.Id;
                                    globalInfoClass.Wm_Index = id;
                                    _panelyibanxinximain.obj = obj;
                                    _panelyibanxinximain.RefreshCode();
                                    DialogResult = System.Windows.Forms.DialogResult.OK;
                                }
                                else
                                {
                                    DialogResult = System.Windows.Forms.DialogResult.OK;
                                    Panel_readCardlist readlist = new Panel_readCardlist(list, txtNo.Text.Trim(), _panelyibanxinximain);
                                    readlist.ShowDialog();
                                }
                            }
                            else
                            {
                                MessageBox.Show("未检索到该儿童信息，请新建档案");
                                globalInfoClass.Wm_Index = -1;
                                _panelyibanxinximain.obj = obj;
                                _panelyibanxinximain.RefreshCode();
                                DialogResult = System.Windows.Forms.DialogResult.OK;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        sdr.Close();
                        dg.con_close();
                    }
                }
            }

        }

    }
}
