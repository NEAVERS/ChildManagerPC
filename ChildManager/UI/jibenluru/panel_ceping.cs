using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using YCF.BLL.sys;
using YCF.Model;
using YCF.BLL;
using YCF.Common;
using ChildManager.Model.ChildBaseInfo;
using ChildManager.UI.cepingshi;

namespace ChildManager.UI.jibenluru
{
    public partial class panel_ceping : UserControl
    {
        ys_ceping_tabbll bll = new ys_ceping_tabbll();
        private WomenInfo _womeninfo;
        private cp_WomenInfo _cp_WomenInfo;
        private bool _isFirstBinding = true;

        public panel_ceping(WomenInfo womeninfo)
        {
            InitializeComponent();
            dgv1.AutoGenerateColumns = false;
            _womeninfo = womeninfo;
            _cp_WomenInfo = new cp_WomenInfo() { cd_id = _womeninfo.cd_id };

        }

        private void panel_ceping_Load(object sender, EventArgs e)
        {
            RefreshRecordList();
        }

        private void RefreshRecordList()
        {
            Cursor.Current = Cursors.WaitCursor;

            _isFirstBinding = true;

            var list = bll.GetList(_womeninfo.cd_id);

            //dgv1.DataSource = null;
            dgv1.DataSource = list;

            Cursor.Current = Cursors.Default;

        }

        private void dgv1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                var url = ((sender as DataGridView).Rows[e.RowIndex].DataBoundItem as ys_ceping_tab).URL;
                RefreshContent(url);
            }
        }

        private void RefreshContent(string url)
        {
            CommonHelper.DisposeControls(pnlCeping.Controls);
            CommonHelper.DisposeControls(pnlCeping.Controls);
            UserControl uc = Activator.CreateInstance(Type.GetType(url), new object[] { _cp_WomenInfo, false }) as UserControl;
            uc.Dock = DockStyle.Fill;
            this.pnlCeping.Controls.Clear();
            this.pnlCeping.Controls.Add(uc);
            uc.Select();
        }

        private void dgv1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if ((sender as DataGridView).RowCount > 0 && _isFirstBinding)
            {
                _isFirstBinding = false;
                dgv1_CellClick(sender, new DataGridViewCellEventArgs(0, 0));
            }
        }
    }
}
