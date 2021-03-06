﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YCF.BLL.sys;
using YCF.Model;

namespace ChildManager.UI.sys
{
    public partial class sys_user_roles : Form
    {
        private sys_user_list _yonghuguanli = null;
        private sys_usersBll bll = new sys_usersBll();
        private sys_roleBll rolebll = new sys_roleBll();
        int[] _ids;
        List<int> idList  = new List<int>() ;
        public sys_user_roles(int[] ids, sys_user_list yonghuguanli)
        {
            InitializeComponent();
            _ids = ids;
            _yonghuguanli = yonghuguanli;

        }
        //批量修改角色
        private void button1_Click(object sender, EventArgs e)
        {
            SYS_USERS userobj = new SYS_USERS();
            userobj.ROLE_CODE = comboBox1.SelectedValue.ToString();
            foreach(var id in _ids)
            {
                idList.Add(id);
            }
            if (bll.UpdateByIds(userobj, idList))
            {
                MessageBox.Show("修改成功", "软件提示");
                _yonghuguanli.jiazaiList();
                this.Close();//本窗口关闭
            }
            else
            {
                MessageBox.Show("修改失败", "软件提示");
            }
        }

        private void sys_user_roles_Load(object sender, EventArgs e)
        {

            IList<SYS_ROLE> listrole = rolebll.GetList();
            comboBox1.DataSource = listrole;
            comboBox1.DisplayMember = "role_name";
            comboBox1.ValueMember = "role_code";
        }
    }
}
