using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YCF.BLL.sys;
using YCF.Model;

namespace ChildManager.UI.sys
{
    /// <summary>
    /// TreeView 帮助类
    /// </summary>
    public class HelperTree
    {
        sys_role_menuBll mbll = new sys_role_menuBll();
        sysmenuBll menubll = new sysmenuBll();
        /// <summary>
        /// 系列节点 Checked 属性控制
        /// </summary>
        /// <param name="e"></param>
        public static void CheckControl(TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node != null && !Convert.IsDBNull(e.Node))
                {
                    //CheckParentNode(e.Node,false);
                    if (e.Node.Nodes.Count > 0)
                    {
                        CheckAllChildNodes(e.Node, e.Node.Checked);
                    }
                }
            }
        }

        /// <summary>
        /// 改变父节点的选中状态，此处为所有子节点不选中时才取消父节点选中，可以根据需要修改
        /// </summary>
        /// <param name="curNode">节点</param>
        /// <param name="isChecked">是否选中</param>
        public void CheckParentNode(TreeNode curNode, bool isChecked)
        {
            bool bChecked = false;
            if (curNode.Parent != null)
            {
                foreach (TreeNode node in curNode.Parent.Nodes)
                {
                    if (node.Checked)
                    {
                        bChecked = isChecked;
                        break;
                    }
                }
                if (bChecked)
                {
                    curNode.Parent.Checked = true;
                    CheckParentNode(curNode.Parent, isChecked);
                }
                else
                {
                    curNode.Parent.Checked = false;
                    CheckParentNode(curNode.Parent, isChecked);
                }
            }
        }

        /// <summary>
        /// 改变所有子节点的状态
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="IsChecked"></param>
        private static void CheckAllChildNodes(TreeNode pn, bool IsChecked)
        {
            foreach (TreeNode tn in pn.Nodes)
            {
                tn.Checked = IsChecked;

                if (tn.Nodes.Count > 0)
                {
                    CheckAllChildNodes(tn, IsChecked);
                }
            }
        }


        /// <summary>
        /// 递归 赋值选中项目
        /// </summary>
        /// <param name="node"></param>
        public void SetNodeValue(TreeNode node, string role_code)
        {
            foreach (TreeNode sbnod in node.Nodes)
            {
                IList<SYS_ROLE_MENU> list = mbll.GetList(sbnod.Tag.ToString(), role_code);
                if (list.Count > 0)
                {
                    node.Checked = true;
                    sbnod.Checked = true;
                    sbnod.Expand();
                }
                else
                {
                    sbnod.Checked = false;
                }
                SetNodeValue(sbnod, role_code);
            }

        }


        /// <summary>
        /// 遍历Check选中的值
        /// </summary>
        /// <param name="node"></param>
        public void GetSubNodeCheckValue(TreeNode node, Dictionary<string, string> dic)
        {
            foreach (TreeNode sbNode in node.Nodes)
            {
                if (sbNode.Checked)
                {
                    if (dic.ContainsValue(sbNode.Text))
                    {
                        dic.Add(sbNode.Text, sbNode.Tag.ToString());
                    }
                    GetSubNodeCheckValue(sbNode, dic);
                }
            }
        }


        /// <summary>
        /// ArrayList集合
        /// </summary>
        /// <param name="node"></param>
        /// <param name="list"></param>
        public void GetSubNodeCheckValue(TreeNode node, ArrayList list)
        {
            foreach (TreeNode sbNode in node.Nodes)
            {
                if (sbNode.Checked)
                {
                    list.Add(sbNode.Tag.ToString());
                    GetSubNodeCheckValue(sbNode, list);
                }
            }
        }

        /// <summary>
        /// 递归子节点全选或反选
        /// </summary>
        /// <param name="subNode"></param>
        /// <param name="isckb"></param>
        public void GetSubNode(TreeNode subNode, bool isChecked)
        {
            subNode.Checked = isChecked;
            subNode.ExpandAll();
            foreach (TreeNode tsub in subNode.Nodes)
            {
                tsub.Checked = isChecked;
                GetSubNode(tsub, isChecked);
            }
        }

        //绑定父节点
        public void BindRoot(TreeNode node, DataTable dt)
        {
            //Select  按照mum排序 或者id
            DataRow[] rows = dt.Select("id >0");//取根     
            foreach (DataRow dRow in rows)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Text = dRow["menu_name"].ToString();
                rootNode.Tag = dRow["menu_code"].ToString();
                node.Nodes.Add(rootNode);
                BindChildAreas(rootNode);
            }
        }

        //递归所有子节点 
        private void BindChildAreas(TreeNode fNode)
        {
            //父节点数据关联的数据行  
            string sqls = "select * from SYS_MENUS where menu_parent='" + fNode.Tag + "' and is_enable='1'";
            DataSet ds = menubll.getDataSet(sqls);
            DataTable dt = ds.Tables[0];
            //父节点ID           
            DataRow[] rows = dt.Select("menu_parent=" + fNode.Tag + "");//子区域        
            if (rows.Length == 0)  //递归终止，区域不包含子区域时          
            {
                return;
            }
            foreach (DataRow dRow in rows)
            {
                TreeNode node = new TreeNode();
                //node.ImageIndex = 1;//未选中状态时候的图像索引值
                //node.SelectedImageIndex = 0;//选中状态时候的图像索引值
                //node.Tag = dRow[0];
                node.Tag = dRow["menu_code"].ToString();
                node.Text = dRow["menu_name"].ToString();//添加子点   
                fNode.Nodes.Add(node); //递归

                //BindChildAreas(node);
            }
        }

        /// <summary>
        /// 实现 全选或反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckAll(DevComponents.DotNetBar.Controls.CheckBoxX ckbAll, TreeView tvInfo)
        {
            bool isckb = true;
            if (ckbAll.Checked)
            {
                isckb = false;
            }
            else
            {
                isckb = true;
            }
            foreach (TreeNode node in tvInfo.Nodes)
            {
                GetSubNode(node, isckb);
            }
        }

        /// <summary>
        /// 更新选中的父节点
        /// </summary>
        /// <param name="treeNode"></param>
        public void UpdateAllParentNodes(TreeNode treeNode)
        {
            TreeNode parent = treeNode.Parent;
            if (parent != null)
            {
                if (parent.Checked && !treeNode.Checked)
                {
                    bool b = true;
                    foreach (TreeNode node in parent.Nodes)
                    {
                        if (node.Checked)
                        {
                            b = true;
                            break;
                        }
                        else
                        {
                            b = false;
                            break;
                        }
                    }
                    parent.Checked = b;
                    UpdateAllParentNodes(parent);
                }
                //else if(parent.Checked)
                else if (!parent.Checked && treeNode.Checked)
                {
                    bool all = true;
                    foreach (TreeNode node in parent.Nodes)
                    {
                        if (node.Checked)
                        {
                            all = true;
                        }
                        //if (!node.Checked)
                        //{
                        //    all = false;
                        //    break;
                        //}
                    }
                    if (all)
                    {
                        parent.Checked = true;
                        UpdateAllParentNodes(parent);
                    }
                }
            }
        }

        /// <summary>
        /// 更新选中所有子节点
        /// </summary>
        /// <param name="treeNode"></param>
        public void CheckAllChildNodes(TreeNode treeNode)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = treeNode.Checked;
                if (node.Nodes.Count > 0)
                {
                    this.CheckAllChildNodes(node);
                }
            }
        }
    }
}
