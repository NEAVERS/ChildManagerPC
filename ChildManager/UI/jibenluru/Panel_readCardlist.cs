using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.Model;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.BLL.ChildBaseInfo;
using ChildManager.BLL;
using System.Collections;
using login.UI.printrecord;
using ChildManager.Model.childSetInfo;
using YCF.Common;
using ChildManager.UI.jibenluru;
using login.UI;
using ChildManager.UI.childSetInfo;

namespace ChildManager.UI
{
    public partial class Panel_readCardlist : Form
    {
        public Panel_moban_manage _mobanpanel;
        PanelyibanxinxiMain _panelyibanxinximain = null;
        private MobanManageBll mobanbll = new MobanManageBll();
        ArrayList _list = new ArrayList();
        string _txtNo = "";
        public Panel_readCardlist(ArrayList list, string txtNo, PanelyibanxinxiMain panelyibanxinxi)
        {
            InitializeComponent();
            _txtNo = txtNo;
            _list = list;
            _panelyibanxinximain = panelyibanxinxi;
        }


        private void Panel_readCardlist_Load(object sender, EventArgs e)
        {
            

            refreshRecordList();

        }

        public void refreshRecordList()
        {

            if (_list != null && _list.Count > 0)
            {
                dataGridView1.Rows.Clear();
                try
                {
                    int i = 1;
                    foreach (ChildBaseInfoObj obj in _list)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dataGridView1, obj.ChildName,obj.ChildGender, Convert.ToDateTime(obj.ChildBirthDay).ToString("yyyy-MM-dd"),obj.HealthCardNo);
                        row.Tag = obj;
                        dataGridView1.Rows.Add(row);
                        i++;
                    }
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                ChildBaseInfoObj obj = dataGridView1.CurrentRow.Tag as ChildBaseInfoObj;
                if (String.IsNullOrEmpty(obj.jiuzhenCardNo))
                {
                    obj.jiuzhenCardNo = _txtNo;
                }
                else
                {
                    obj.jiuzhenCardNo += "," + _txtNo;
                }
                int id = obj.ID;
                globalInfoClass.Wm_Index = id;
                _panelyibanxinximain.obj = obj;
                _panelyibanxinximain.RefreshCode();
                DialogResult = System.Windows.Forms.DialogResult.OK;

            }
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                ChildBaseInfoObj obj = dataGridView1.CurrentRow.Tag as ChildBaseInfoObj;
                if (String.IsNullOrEmpty(obj.jiuzhenCardNo))
                {
                    obj.jiuzhenCardNo = _txtNo;
                }
                else
                {
                    obj.jiuzhenCardNo += "," + _txtNo;
                }
                int id = obj.ID;
                globalInfoClass.Wm_Index = id;
                _panelyibanxinximain.obj = obj;
                _panelyibanxinximain.RefreshCode();
                DialogResult = System.Windows.Forms.DialogResult.OK;

            }
        }

    }
}
