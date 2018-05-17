using System;
using System.Windows.Forms;
using YCF.Common;
using YCF.BLL;
using YCF.Model;
using YCF.BLL.cepingshi;
using ChildManager.UI.printrecord.cepingshi;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Data;
using YCF.BLL.Template;

namespace ChildManager.UI.cepingshi
{
    public partial class cp_ados3_panel : UserControl
    {
        private cp_ados3_tabbll bll = new cp_ados3_tabbll();//儿童建档基本信息业务处理类
        tab_person_databll personbll = new tab_person_databll();
        cp_WomenInfo _cpwomeninfo = null;
        private bool _isShowTopPanel;
        IList<cp_ados3_tab> _list = null;
        private cp_ados3_tab _obj;
        string _hospital = globalInfoClass.Hospital;
        public List<DicObj> listszys = new List<DicObj>();
        InputLanguage InputHuoDong = null;//当前输入法

        public cp_ados3_panel(cp_WomenInfo cpwomeninfo)
        {
            InitializeComponent();
            _cpwomeninfo = cpwomeninfo;
            CommonHelper.SetAllControls(panel1);
            SetData(szyslist, listszys, "songzhen");
        }

        public cp_ados3_panel(cp_WomenInfo cpwomeninfo, bool isShowTopPanel) : this(cpwomeninfo)
        {
            _isShowTopPanel = isShowTopPanel;

            //panelEx1.Visible = _isShowTopPanel;
            if (!isShowTopPanel)
            {
                foreach (Control item in panelEx1.Controls)
                {
                    if (!item.Equals(update_time))
                    {
                        item.Visible = false;
                    }
                }
            }
        }

        private void PanelyibanxinxiMain_Load(object sender, EventArgs e)
        {
            RefreshRecordList();
        }

        private void RefreshRecordList()
        {
            _list = bll.GetList(_cpwomeninfo.cd_id);
            if (_list.Count > 0)
            {
                //update_time.DataSource = null;
                update_time.ValueMember = "id";
                update_time.DisplayMember = "update_time";
                update_time.DataSource = _list;
            }
            else
            {
                buttonX11.PerformClick();
                update_time.DataSource = _list;
                update_time.Text = "";
            }
        }

        private void update_time_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = (int)(update_time.SelectedValue ?? 0);
            RefreshCode(id);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            cp_ados3_tab obj = getObj();
            if (bll.SaveOrUpdate(obj))
            {
                MessageBox.Show("保存成功！");
                RefreshRecordList();
                update_time.SelectedIndex = _list.IndexOf(_list.FirstOrDefault(t => t.update_time == obj.update_time));

            }
            else
            {
                MessageBox.Show("保存失败！");
            }
            Cursor.Current = Cursors.Default;
        }

        private void kbxw_count(object sender, EventArgs e)
        {
            int i_kbxw_btxc = 0;
            int i_kbxw_dyxsc = 0;
            int i_kbxw_shsz = 0;
            int i_kbxw_qpxw = 0;
            int.TryParse(kbxw_btxc.Text.Trim(), out i_kbxw_btxc);
            int.TryParse(kbxw_dyxsc.Text.Trim(), out i_kbxw_dyxsc);
            int.TryParse(kbxw_shsz.Text.Trim(), out i_kbxw_shsz);
            int.TryParse(kbxw_qpxw.Text.Trim(), out i_kbxw_qpxw);
            kbxw_sum.Text = (i_kbxw_btxc + i_kbxw_dyxsc + i_kbxw_shsz + i_kbxw_qpxw).ToString();
        }

        private cp_ados3_tab getObj()
        {
            if (pgzqm.Text.Trim() == "")
            {
                pgzqm.Text = globalInfoClass.UserName;
            }
            cp_ados3_tab obj = CommonHelper.GetObjMenzhen<cp_ados3_tab>(panel1.Controls, _cpwomeninfo.cd_id);
            obj.hospital = _hospital;
            if (_obj != null)
            {
                obj.id = _obj.id;
                obj.operate_code = _obj.operate_code;
                obj.operate_name = _obj.operate_name;
                obj.operate_time = _obj.operate_time;
            }
            return obj;

        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="id"></param>
        public void RefreshCode(int id)
        {
            Cursor.Current = Cursors.WaitCursor;
            _obj = bll.Get(id);
            if (_obj != null)
            {
                CommonHelper.setForm(_obj, panel1.Controls);
            }
            else
            {
                SetDefault();
            }
            Cursor.Current = Cursors.Default;
        }

        private void SetDefault()
        {
            CommonHelper.setForm(new cp_ados3_tab(), panel1.Controls);
            checkBox7.Checked = true;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (_obj != null)
            {
                if (MessageBox.Show("删除该记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    try
                    {
                        if (bll.Delete(_obj.id))
                        {
                            MessageBox.Show("删除成功!", "软件提示");
                            RefreshRecordList();
                        }
                        else
                        {
                            MessageBox.Show("删除失败!", "请联系管理员");
                        }

                    }
                    finally
                    {
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
            else
            {
                MessageBox.Show("该记录还未保存！");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            print(true);
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            print(false);
        }

        public void print(bool ispre)
        {
            Cursor.Current = Cursors.WaitCursor;
            cp_ados3_tab obj = bll.Get(_obj?.id ?? 0);
            if (obj == null)
            {
                MessageBox.Show("请保存后再预览打印！", "软件提示");
                return;
            }
            try
            {
                tb_childbase baseobj = new tb_childbasebll().Get(_cpwomeninfo.cd_id);
                cp_ados3_printer printer = new cp_ados3_printer(baseobj, obj);
                printer.Print(ispre);
            }
            catch (Exception ex)
            {
                MessageBox.Show("系统异常，请联系管理员！");
                throw ex;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void gt_count(object sender, EventArgs e)
        {
            int i_gt_hh = 0;
            int i_gt_kb = 0;
            int i_gt_msxdz = 0;
            int i_gt_bgsj = 0;
            int.TryParse(gt_hh.Text.Trim(), out i_gt_hh);
            int.TryParse(gt_kb.Text.Trim(), out i_gt_kb);
            int.TryParse(gt_msxdz.Text.Trim(), out i_gt_msxdz);
            int.TryParse(gt_bgsj.Text.Trim(), out i_gt_bgsj);
            int gtcou = i_gt_hh + i_gt_kb + i_gt_msxdz + i_gt_bgsj;
            gt_sum.Text = gtcou.ToString();

            int i_hd_bxc = 0;
            int i_hd_cxtr = 0;
            int i_hd_gx = 0;
            int i_hd_sjfy = 0;
            int i_hd_zdbd = 0;
            int i_hd_dcl = 0;
            int i_hd_xhxsh = 0;
            int.TryParse(hd_bxc.Text.Trim(), out i_hd_bxc);
            int.TryParse(hd_cxtr.Text.Trim(), out i_hd_cxtr);
            int.TryParse(hd_gx.Text.Trim(), out i_hd_gx);
            int.TryParse(hd_sjfy.Text.Trim(), out i_hd_sjfy);
            int.TryParse(hd_zdbd.Text.Trim(), out i_hd_zdbd);
            int.TryParse(hd_dcl.Text.Trim(), out i_hd_dcl);
            int.TryParse(hd_xhxsh.Text.Trim(), out i_hd_xhxsh);
            int hdcou = i_hd_bxc + i_hd_cxtr + i_hd_gx + i_hd_sjfy + i_hd_zdbd + i_hd_dcl + i_hd_xhxsh;
            hd_sum.Text = hdcou.ToString();

            gt_hd_sum.Text = (gtcou + hdcou).ToString();
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (_obj != null)
            {
                var obj = new cp_ados3_tab();
                obj.update_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _list.Add(obj);
                update_time.DataSource = null;//数据源先置空，否则同一个对象不会刷新
                update_time.ValueMember = "id";
                update_time.DisplayMember = "update_time";
                update_time.DataSource = _list;
                update_time.SelectedIndex = _list.Count - 1;
            }

        }

        #region 送诊医生操作
        private void szys_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && szyslist.Visible == true)
            {
                DicObj info = szyslist.SelectedItem as DicObj;
                szys.Text = info.name;
                szyslist.Visible = false;
                //this.SelectNextControl(this.ActiveControl, true, true, true, true);
            }
        }

        private void szys_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void szys_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left)
            {
                if (szyslist.SelectedIndex > 0)
                    szyslist.SelectedIndex--;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
            {
                if (szyslist.SelectedIndex < szyslist.Items.Count - 1)
                    szyslist.SelectedIndex++;
            }
            //回车
            else if (e.KeyCode == Keys.Enter)
            {
                //DicObj info = szyslist.SelectedItem as DicObj;
                //textBox1.Text = info.name;
                //szyslist.Visible = false;
            }
            else
            {
                szyslist.DataSource = null;

                string selpro = szys.Text.Trim();

                if (selpro != "")
                {
                    IList<DicObj> dataSource = listszys.FindAll(t => (t.code.Length >= selpro.Length && t.code.Substring(0, selpro.Length).ToUpper().Equals(selpro.ToUpper())) || (t.name.Length > selpro.Length && t.name.Substring(0, selpro.Length).Equals(selpro.ToUpper())));
                    if (dataSource.Count > 0)
                    {
                        szyslist.DataSource = dataSource;
                        szyslist.DisplayMember = "name";
                        szyslist.ValueMember = "code";
                        szyslist.Visible = true;
                    }
                    else
                        szyslist.Visible = false;
                }
                else
                {
                    szyslist.Visible = false;
                }
            }
            //textBox1.Focus();
            szys.Select(szys.Text.Length, 1); //光标定位到文本框最后
        }

        private void szys_Enter(object sender, EventArgs e)
        {
            InputHuoDong = InputLanguage.CurrentInputLanguage;
            foreach (InputLanguage Input in InputLanguage.InstalledInputLanguages)
            {
                if (Input.LayoutName.Contains("美式键盘"))
                {
                    InputLanguage.CurrentInputLanguage = Input;
                    break;
                }
            }
        }

        private void szys_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputHuoDong;
        }

        /// <summary>
        /// 设置自动获取签名
        /// 2017-12-01 cc
        /// </summary>
        /// <param name="con">控件</param>
        /// <param name="diclist"></param>
        /// <param name="type">分类</param>
        public void SetData(ListBox con, List<DicObj> diclist, string type)
        {
            IList<tab_person_data> list = personbll.GetList(type);
            DataTable dt = new DataTable();
            dt.Columns.Add("code", typeof(string));
            dt.Columns.Add("name", typeof(string));

            foreach (tab_person_data obj in list)
            {
                DicObj dicobj = new DicObj();
                dicobj.name = obj.name;   //获取name属性值  
                dicobj.code = obj.code;   //获取name属性值 
                diclist.Add(dicobj);
                dt.Rows.Add(dicobj.code, dicobj.name);
            }
            con.DataSource = dt;
            con.DisplayMember = "name";
            con.ValueMember = "code";
        }

        #endregion
    }
}
