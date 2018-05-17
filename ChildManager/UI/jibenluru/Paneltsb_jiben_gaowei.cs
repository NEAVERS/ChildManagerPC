using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChildManager.BLL;
using YCF.Common;
using ChildManager.DAL;
using System.Runtime.InteropServices;

namespace ChildManager.UI.jibenluru
{
    public partial class Paneltsb_jiben_gaowei : Form
    {
        DataTable dt = null;
        DataSet ds = new DataSet();
        private ProjectGaoweibll bll = new ProjectGaoweibll();
        DateLogic dg = new DateLogic();
        private string value = "";
        private int _cid;
        //设置返回值
        private string _returnval;
        public string returnval
        {
            get { return _returnval; }
        }
        private Paneltsb_jiben_gaowei _gaowei = null;

        public Paneltsb_jiben_gaowei(string content, int cid)
        {
            InitializeComponent();
            _gaowei = this;
            value = content;
            _cid = cid;
            this.tvInfo.CheckBoxes = true;
            this.tvInfo.ShowLines = true;
            //this.tvInfo.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            tvInfo.AfterCheck += new TreeViewEventHandler(treeView_AfterCheck);
            tvInfo.AfterSelect += new TreeViewEventHandler(treeView_AfterSelect);
        }

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

        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TVITEM lParam);

        /// <summary>
        /// 刷新树结构
        /// </summary>
        public void RefreshCode()
        {
            this.tvInfo.Nodes.Clear();
            //加载树
            string strSqls = "select * from tb_gaoweiproject order by id";
            ds = dg.GetDataSet(strSqls);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)            {
                //绑定树
                BindRoot(cmsProject, cmscontent, value);
                //tvInfo.ExpandAll();
            }

            //设置TreeView样式
            tvInfo.HideSelection = false;
            //tvInfo.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.tvInfo.DrawNode += new DrawTreeNodeEventHandler(tvInfo_DrawNode);
        }

        /// <summary>
        /// 从绘制失去焦点  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvInfo_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //+ -  号
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

        //绑定根节点      
        private void BindRoot(ContextMenuStrip contextMenuRoot, ContextMenuStrip content, string oldvalue)
        {
            //Select  按照mum排序 或者id
            DataRow[] rows = dt.Select("id >0");//取根     
            foreach (DataRow dRow in rows)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Tag = dRow[0];
                //rootNode.Name = dRow["num"].ToString();
                rootNode.Text = dRow["type"].ToString();
                rootNode.ContextMenuStrip = contextMenuRoot;
                //tvInfo.CheckBoxes = true;//显示复选框

                tvInfo.Nodes.Add(rootNode);
                BindChildAreas(rootNode, content, oldvalue);
            }
        }


        //递归绑定子区域     
        private void BindChildAreas(TreeNode fNode, ContextMenuStrip contextMenuChlid, string oldvalue)
        {
            //父节点数据关联的数据行           
            int pid = Convert.ToInt32(fNode.Tag);
            string sqls = "select * from tb_gaoweicontent where p_id=" + pid + "";
            ds = dg.GetDataSet(sqls);
            dt = ds.Tables[0];
            //父节点ID           
            DataRow[] rows = dt.Select("p_id=" + pid);//子区域        
            if (rows.Length == 0)  //递归终止，区域不包含子区域时          
            {
                return;
            }
            foreach (DataRow dRow in rows)
            {
                TreeNode node = new TreeNode();
                node.ImageIndex = 1;//未选中状态时候的图像索引值
                node.SelectedImageIndex = 0;//选中状态时候的图像索引值
                node.Tag = dRow[0];
                node.Name = dRow["p_id"].ToString();
                node.Text = dRow["content"].ToString();//添加子点   
                node.ContextMenuStrip = contextMenuChlid;
                if (oldvalue != "")
                {
                    string[] spilt = oldvalue.Split(',');
                    for (int i = 0; i < spilt.Length; i++)
                    {
                        if (spilt[i] == node.Text)
                        {
                            node.Checked = true;
                            fNode.Expand();
                        }
                    }
                }
                fNode.Nodes.Add(node); //递归
                //BindChildNode(node, contextMenuStripChidyz);
            }
        }

        private void Paneltsb_jiben_gaowei_Load(object sender, EventArgs e)
        {
            RefreshCode();
            value = "";


        }


        private void button1_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tvInfo.Nodes)
            {
                foreach (TreeNode item in node.Nodes)
                {
                    if (item.Checked == true)
                    {
                        value += item.Text + ",";
                    }
                }

            }
            if (value != "")
            {
                value = value.Substring(0, value.Length - 1);
            }
            _returnval = value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            if (e.Node.Parent == null)
            {
                e.Node.Expand();
            }

            if (e.Node.Checked == false)
            {
                e.Node.Collapse();
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
                    if (ischecked)
                    {
                        parent.Checked = true;
                        value += parent.Text + ",";
                    }
                    else
                        parent.Checked = false;
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


        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvInfo.SelectedNode.Parent == null && tvInfo.SelectedNode.Tag != "")
            {
                int p_id = Convert.ToInt32(tvInfo.SelectedNode.Tag);
                Paneltsb_jiben_gaowei_projectadd add = new Paneltsb_jiben_gaowei_projectadd(true, 0, p_id, _gaowei, "");
                add.ShowDialog();
            }
        }


        private void tsbupdate_Click(object sender, EventArgs e)
        {
            if (tvInfo.SelectedNode != null && tvInfo.SelectedNode.Tag != "")
            {
                int id = Convert.ToInt32(tvInfo.SelectedNode.Tag);
                string content = tvInfo.SelectedNode.Text;
                Paneltsb_jiben_gaowei_projectadd add = new Paneltsb_jiben_gaowei_projectadd(false, id, 0, _gaowei, content);
                add.ShowDialog();
            }
        }

        private void tsbdelete_Click(object sender, EventArgs e)
        {
            if (tvInfo.SelectedNode != null && tvInfo.SelectedNode.Tag != "")
            {
                int id = Convert.ToInt32(tvInfo.SelectedNode.Tag);
                if (bll.deleterecord("delete from tb_gaoweicontent where id=" + id) > 0)
                {
                    MessageBox.Show("删除成功！");
                    RefreshCode();
                }
                else
                {
                    MessageBox.Show("删除失败！");
                    return;
                }
            }
        }

    }
}
