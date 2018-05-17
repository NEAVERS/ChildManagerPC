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
    public partial class cp_adhd_panel : UserControl
    {
        private cp_adhd_tabbll bll = new cp_adhd_tabbll();//儿童建档基本信息业务处理类
        tab_person_databll personbll = new tab_person_databll();
        cp_WomenInfo _cpwomeninfo = null;
        private bool _isShowTopPanel;
        IList<CP_ADHD_TAB> _list = null;
        private CP_ADHD_TAB _obj;
        string _hospital = globalInfoClass.Hospital;
        public List<DicObj> listszys = new List<DicObj>();
        InputLanguage InputHuoDong = null;//当前输入法

        public cp_adhd_panel(cp_WomenInfo cpwomeninfo)
        {
            InitializeComponent();
            _cpwomeninfo = cpwomeninfo;
            CommonHelper.SetAllControls(panel1);
            SetData(szyslist, listszys, "songzhen");
        }

        public cp_adhd_panel(cp_WomenInfo cpwomeninfo, bool isShowTopPanel) : this(cpwomeninfo)
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
            //RefreshCode();
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
            CP_ADHD_TAB obj = getObj();
            if (bll.SaveOrUpdate(obj))
            {
                MessageBox.Show("保存成功！");

                RefreshRecordList();
                update_time.SelectedIndex = _list.IndexOf(_list.FirstOrDefault(t => t.UPDATE_TIME == obj.UPDATE_TIME));
            }
            else
            {
                MessageBox.Show("保存失败！");
            }
            Cursor.Current = Cursors.Default;
        }

        private CP_ADHD_TAB getObj()
        {
            if (cszqm.Text.Trim() == "")
            {
                cszqm.Text = globalInfoClass.UserName;
            }
            CP_ADHD_TAB obj = CommonHelper.GetObjMenzhen<CP_ADHD_TAB>(panel1.Controls, _cpwomeninfo.cd_id);
            obj.HOSPITAL = _hospital;
            if (_obj != null)
            {
                obj.ID = _obj.ID;
                obj.OPERATE_CODE = _obj.OPERATE_CODE;
                obj.OPERATE_NAME = _obj.OPERATE_NAME;
                obj.OPERATE_TIME = _obj.OPERATE_TIME;
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
            CommonHelper.setForm(new CP_ADHD_TAB() { ADHD1_JL = "", ADHD2_JL = "", ADHD4_JL = "" }, panel1.Controls);
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
                        if (bll.Delete(_obj.ID))
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
            CP_ADHD_TAB obj = bll.Get(_obj?.ID ?? 0);
            if (obj == null)
            {
                MessageBox.Show("请保存后再预览打印！", "软件提示");
                return;
            }
            try
            {
                TB_CHILDBASE baseobj = new tb_childbasebll().Get(_cpwomeninfo.cd_id);
                cp_adhd_printer printer = new cp_adhd_printer(baseobj, obj);
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


        //病史提供者-父母选项同步
        void Adhd1_bstgzTextChanged(object sender, EventArgs e)
        {
            adhd2_bstgz.Text = adhd1_bstgz.Text;
            adhd3_bstgz.Text = adhd1_bstgz.Text;
        }
        void Adhd2_gnshTextChanged(object sender, EventArgs e)
        {
            int zyqx = 0;
            int ddcd = 0;
            int gnsh = 0;
            int.TryParse(adhd2_zyqx.Text.Trim(), out zyqx);
            int.TryParse(adhd2_ddcd.Text.Trim(), out ddcd);
            int.TryParse(adhd2_gnsh.Text.Trim(), out gnsh);
            checkBox7.Checked = zyqx >= 6;
            checkBox8.Checked = ddcd >= 6;
            checkBox9.Checked = gnsh >= 1;
            checkBox3.Checked = (zyqx >= 6 || ddcd >= 6) && gnsh >= 1;
            checkBox4.Checked = !checkBox3.Checked;
        }
        void Adhd3_dlwkTextChanged(object sender, EventArgs e)
        {
            int dlwk = 0;
            int pxza = 0;
            int jlyy = 0;
            int.TryParse(adhd3_dlwk.Text.Trim(), out dlwk);
            int.TryParse(adhd3_pxza.Text.Trim(), out pxza);
            int.TryParse(adhd3_jlyy.Text.Trim(), out jlyy);
            checkBox10.Checked = dlwk >= 4;
            checkBox11.Checked = pxza >= 3;
            checkBox12.Checked = jlyy >= 3;

        }
        void Adhd4_zyqxTextChanged(object sender, EventArgs e)
        {
            int zyqx = 0;
            int ddcd = 0;
            int gnsh = 0;
            int.TryParse(adhd4_zyqx.Text.Trim(), out zyqx);
            int.TryParse(adhd4_ddcd.Text.Trim(), out ddcd);
            int.TryParse(adhd4_gnsh.Text.Trim(), out gnsh);
            checkBox13.Checked = zyqx >= 6;
            checkBox14.Checked = ddcd >= 6;
            checkBox15.Checked = gnsh >= 1;
            checkBox5.Checked = (zyqx >= 6 || ddcd >= 6) && gnsh >= 1;
            checkBox6.Checked = !checkBox5.Checked;
        }
        void CheckBox1CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
            {
                foreach (CheckBox chk in (sender as CheckBox).Parent.Controls)
                {
                    if (chk != sender)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }
        void Adhd1_pfTextChanged(object sender, EventArgs e)
        {
            int pf = 0;
            int.TryParse(adhd1_pf.Text.Trim(), out pf);
            checkBox1.Checked = pf <= 15;
            checkBox2.Checked = !checkBox1.Checked;
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (_obj != null)
            {
                var obj = new CP_ADHD_TAB();
                obj.UPDATE_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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

        public void SetData(ListBox con, List<DicObj> diclist, string type)
        {
            IList<TAB_PERSON_DATA> list = personbll.GetList(type);
            DataTable dt = new DataTable();
            dt.Columns.Add("code", typeof(string));
            dt.Columns.Add("name", typeof(string));

            foreach (TAB_PERSON_DATA obj in list)
            {
                DicObj dicobj = new DicObj();
                dicobj.name = obj.NAME;   //获取name属性值  
                dicobj.code = obj.CODE;   //获取name属性值 
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
