using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using YCF.BLL.sys;
using YCF.Model;
using System.Runtime.InteropServices;
using YCF.Common;
using System.Collections;

namespace ChildManager.UI.sys
{
    public partial class sys_rolemenu : UserControl
    {
        IList<sys_role> listRole;
        /// <summary>
        /// 用来存放DGV单元格修改之前值
        /// </summary>
        Object cellTempValue = null;

        sys_roleBll blls = new sys_roleBll();
        sys_role_menuBll mbll = new sys_role_menuBll();
        //IList<sys_role_menu> listRoleMenu;
        private HelperTree helperTree = new HelperTree();

        DataTable dt = null;
        DataSet ds = new DataSet();

        #region 绘制TreeView
        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;
        private void HideCheckBox(TreeView tvw, TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage; public int cChildren; public IntPtr lParam;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        #endregion
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TVITEM lParam);

        Dictionary<string, string> dic = new Dictionary<string, string>();

        public sys_rolemenu()
        {
            InitializeComponent();

            this.tvInfo.CheckBoxes = true;
            this.tvInfo.ShowLines = true;
            this.tvInfo.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            tvInfo.AfterCheck += new TreeViewEventHandler(treeView_AfterCheck);
            //tvInfo.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
        }

        /// <summary>
        /// 点击文字，选中checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                e.Node.Expand();
            }

        }

        /// <summary>
        /// 选中子节点，勾选父节点，选中父节点，勾选子节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                UpdateCheckStatus(e);
            }

        }

        /// <summary>
        /// 选中子节点，勾选父节点，选中父节点，勾选子节点
        /// </summary>
        /// <param name="node"></param>
        private void CheckTreeNode(TreeNode node)
        {
            node.TreeView.AfterCheck -= new TreeViewEventHandler(treeView_AfterCheck);
            if (node.Parent != null)
            {
                TreeNode parent = node.Parent as TreeNode;
                //如果该节点是选中的
                if (node.Checked)
                {
                    //判断其父节点是否被选中，如果没有被选中则选中它
                    if (parent.Checked == false)
                    {
                        parent.Checked = true;
                    }
                }
                else
                {
                    bool ischecked = false;
                    foreach (TreeNode child in parent.Nodes)
                    {
                        if (child.Checked)
                        {
                            ischecked = true;
                            break;
                        }
                    }
                }
            }//如果等于null，说明选择的是根节点
            else
            {
                foreach (TreeNode child in node.Nodes)
                {
                    child.Checked = node.Checked;
                }
            }
            node.TreeView.AfterCheck += new TreeViewEventHandler(treeView_AfterCheck);

        }
        //加载角色
        private void loadRole()
        {
            dgvrole.Rows.Clear();
            try
            {
                listRole = blls.GetList();
                if (listRole.Count > 0)
                {
                    foreach (var obj in listRole)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgvrole, obj.role_name, obj.role_code, obj.id);
                        row.Tag = obj;
                        dgvrole.Rows.Add(row);
                        //加载角色名称的列表
                    }
                }
            }
            finally
            {
                dgvrole.ClearSelection();
            }
        }
        //控件加载
        private void sysrolemenu_Load(object sender, EventArgs e)
        {
            loadRole();
            BindTree();
        }
        private void BindTree()
        {
            foreach (TreeNode node in tvInfo.Nodes)
            {
                //加载菜单
                string sqls = string.Format("select * from sys_menus where menu_type='" + node.Text + "' and is_enable='1' and menu_parent='-1'");
                ds = blls.getDataSet(sqls);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    helperTree.BindRoot(node, dt);
                    tvInfo.ExpandAll();
                }
            }
            tvInfo.TopNode = tvInfo.Nodes[0];//TreeView出现滚动条，默认显示顶部
            //设置TreeView样式
            tvInfo.HideSelection = false;
            tvInfo.DrawMode = TreeViewDrawMode.Normal;
            //this.tvInfo.DrawNode += new DrawTreeNodeEventHandler(tvInfo_DrawNode);
        }

        /// <summary>
        /// DGV单元格开始编辑时触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvrole_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //cellTempValue = dgvrole.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }


        /// <summary>
        /// 从绘制失去焦点  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvInfo_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Parent == null)
            {
                HideCheckBox(this.tvInfo, e.Node);
                Color backColor, foreColor;
                if ((e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected)
                {
                    backColor = SystemColors.Highlight;
                    foreColor = SystemColors.HighlightText;
                }
                else if ((e.State & TreeNodeStates.Hot) == TreeNodeStates.Hot)
                {
                    backColor = SystemColors.HotTrack;
                    foreColor = SystemColors.HighlightText;
                }
                else
                {
                    backColor = e.Node.BackColor;
                    foreColor = e.Node.ForeColor;
                }

                if (this.tvInfo.ShowPlusMinus)
                {
                    //或者 系统默认 + -  号
                    #region 画一个“加号”表示未展开的
                    //Pen pen = new Pen(Brushes.Black);
                    //Rectangle plusBound = new Rectangle(new Point(0, e.Bounds.Top), new Size(this.tvInfo.Width, 18));
                    //e.Graphics.DrawRectangle(pen, plusBound.X + 7, plusBound.Y + 2, 10, 10);
                    //e.Graphics.DrawLine(pen, plusBound.X + 9, plusBound.Top + 7, plusBound.Left + 15, plusBound.Top + 7);
                    //if (!e.Node.IsExpanded)
                    //{
                    //    //如果节点未展开，则在减号中添加一条线，变成加号  
                    //    e.Graphics.DrawLine(pen, plusBound.X + 12, plusBound.Top + 4, plusBound.Left + 12, plusBound.Top + 10);
                    //}
                    #endregion
                }
                Rectangle newBounds = e.Node.Bounds;
                newBounds.X = 20;

                using (SolidBrush brush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(brush, newBounds);
                }
                TextRenderer.DrawText(e.Graphics, e.Node.Text, this.tvInfo.Font, newBounds, foreColor, backColor);
                if ((e.State & TreeNodeStates.Focused) == TreeNodeStates.Focused)
                {
                    ControlPaint.DrawFocusRectangle(e.Graphics, newBounds, foreColor, backColor);
                }

                e.DrawDefault = false;
            }
            else
            {
                e.DrawDefault = true;
            }

        }

        /// <summary>
        /// 实现 全选或反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbAll_CheckedChanged(object sender, EventArgs e)
        {
            helperTree.CheckAll(ckbAll, tvInfo);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvrole.SelectedRows.Count <= 0)
            {
                MessageUtil.ShowTips("请选择左侧的角色列表");
                return;
            }
            dic.Clear();
            string role_code = dgvrole.CurrentRow.Cells[1].Value.ToString();
            //int roleid = Convert.ToInt32(dgvrole.CurrentRow.Cells[2].Value.ToString());
            ArrayList list = new ArrayList();
            foreach (TreeNode node in tvInfo.Nodes)
            {
                helperTree.GetSubNodeCheckValue(node, list);
                //list.Add(node.Tag.ToString());
            }
            bool isTure = false;
            if (list.Count == 0)
            {
                MessageUtil.ShowTips("请选择要保存的功能菜单！");
                return;
            }
            try
            {
                //删除角色对应的编号
                string sqls = "delete from sys_role_menu where role_code='" + role_code + "'";
                mbll.DeleteRoleCode(sqls, role_code);

                //foreach (var item in dic.Values)//遍历dic取value的值
                //{
                //    sys_role_menu obj = new sys_role_menu();
                //    obj.role_code = role_code;
                //    obj.menu_code = item;
                //    isTure = mbll.Add(obj);
                //}
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        sys_role_menu obj = new sys_role_menu();
                        obj.role_code = role_code;
                        obj.menu_code = item.ToString();
                        isTure = mbll.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (isTure)
            {
                MessageUtil.ShowTips("保存成功！");
            }
            else
            {
                MessageUtil.ShowTips("保存失败！");
                return;
            }
        }


        private void UpdateCheckStatus(TreeViewEventArgs e)
        {
            helperTree.CheckAllChildNodes(e.Node);
            helperTree.UpdateAllParentNodes(e.Node);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            sys_role role = new sys_role();
            role.role_name = "";
            int code = 0;
            if (dgvrole.Rows.Count >= 1)
            {
                code = Convert.ToInt32(dgvrole.Rows[dgvrole.Rows.Count - 1].Cells[1].Value.ToString());
            }
            if (code < 10)
            {
                role.role_code = "0" + (code + 1).ToString();
            }
            else
            {
                role.role_code = (code + 1).ToString();
            }
            if (blls.Add(role))
            {
                loadRole();
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (dgvrole.SelectedRows.Count >= 1)
            {
                if (MessageBox.Show("确定要删除角色：" + dgvrole.CurrentRow.Cells[0].Value.ToString() + " 吗？", "删除提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvrole.CurrentRow.Cells[2].Value);
                    if (blls.Delete(id))
                    {
                        loadRole();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！", "提示");
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的行！", "删除提示");
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (dgvrole.SelectedRows.Count <= 0)
            {
                MessageUtil.ShowTips("请选择要修改的行！");
                return;
            }
            if (dgvrole.SelectedRows.Count >= 1)
            {
                this.dgvrole.CurrentCell = this.dgvrole.CurrentRow.Cells[0];
                this.dgvrole.BeginEdit(true);
            }
        }

        private void dgvrole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvrole.SelectedRows.Count >= 1)
            {
                this.dgvrole.CurrentCell = this.dgvrole.CurrentRow.Cells[0];
                this.dgvrole.BeginEdit(true);
            }
        }

        private void dgvrole_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvrole.BeginEdit(true))//如果是获得焦点编辑模式，返回不查询
            {
                return;
            }
            if (dgvrole.SelectedRows.Count >= 1)
            {
                foreach (TreeNode node in tvInfo.Nodes)
                {
                    helperTree.SetNodeValue(node, dgvrole.CurrentRow.Cells[1].Value.ToString());
                }
            }
        }

        private void dgvrole_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            sys_role roleobj = new sys_role();
            if (dgvrole.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                MessageBox.Show("请输入角色名称！", "系统提示");
                //dgvrole.CurrentCell = dgvrole[e.RowIndex,e.ColumnIndex];
                return;
            }
            //判断编辑前后的值是否一样（是否修改了内容）
            if (Object.Equals(cellTempValue, dgvrole.Rows[e.RowIndex].Cells[e.ColumnIndex].Value))
            {
                return;
            }
            //判断用户是否确定修改
            //if (MessageBox.Show("确定要修改吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.None) != DialogResult.OK)
            //{
            //    dgvrole.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = cellTempValue;
            //    return;
            //}

            //dgvrole.Columns[0].DataPropertyName; //所选单元格列名
            roleobj.role_name = dgvrole.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(); //所选单元格修改后的值
            roleobj.role_code = dgvrole.Rows[e.RowIndex].Cells[1].Value.ToString();
            roleobj.id = Convert.ToInt32(dgvrole.Rows[e.RowIndex].Cells[2].Value);
            blls.Update(roleobj);
        }
    }
}
