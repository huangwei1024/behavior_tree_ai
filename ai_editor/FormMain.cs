using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ai_editor.NodeDef;
using BehaviorPB;

namespace ai_editor
{
	public partial class FormMain : Form
	{
		private AiTreeNode selectedNode;
		private AiTreeNode dragMoveNode;
		private string filePath;

		public FormMain()
		{
			InitializeComponent();

			treeView_BTree.Clear();
			selectedNodeChange(null);
			替换节点ToolStripMenuItem.DropDown = 新建节点ToolStripMenuItem.DropDown;
		}

		private void RefreshUI()
		{
			propertyGrid1.Refresh();
			treeView_BTree.Refresh();
		}

		private void selectedNodeChange(AiTreeNode newSelected)
		{
			if (newSelected != null)
			{
				新建节点ToolStripMenuItem.Text = "新建节点";
				替换节点ToolStripMenuItem.Enabled = true;
				删除节点ToolStripMenuItem.Enabled = true;
				上移节点ToolStripMenuItem.Enabled = true;
				下移节点ToolStripMenuItem.Enabled = true;
			}
			else
			{
				新建节点ToolStripMenuItem.Text = "新建根节点";
				替换节点ToolStripMenuItem.Enabled = false;
				删除节点ToolStripMenuItem.Enabled = false;
				上移节点ToolStripMenuItem.Enabled = false;
				下移节点ToolStripMenuItem.Enabled = false;
			}

// 			if (newSelected == selectedNode)
// 				return;

			selectedNode = newSelected;
			if (selectedNode != null)
			{
				propertyGrid1.SelectedObject = newSelected.LogicNode.Props;
				if (newSelected.LogicNode.TreeNode != null)
					propertyGrid1.SelectedObject = selectedNode.LogicNode.TreeNode.Props;
				selectedNode.ExpandAll();
			}
			else
			{
				propertyGrid1.SelectedObject = null;
			}
			propertyGrid1.ExpandAllGridItems();
			propertyGrid1.Refresh();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (AboutBox about = new AboutBox())
			{
				about.ShowDialog();
			}
		}


		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			filePath = null;
			selectedNodeChange(null);
			treeView_BTree.Clear();
			NodeDef.Node.sIDCounter = 0;
		}


		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (filePath == null || filePath.Length == 0)
			{
				saveFileDialog1.ShowDialog();
				filePath = saveFileDialog1.FileName;
			}

			if (filePath == null || filePath.Length == 0)
				return;

			treeView_BTree.SaveProtoBuf(filePath);
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveFileDialog1.ShowDialog();
			filePath = saveFileDialog1.FileName;
			if (filePath == null || filePath.Length == 0)
				return;

			saveToolStripMenuItem_Click(sender, e);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			filePath = openFileDialog1.FileName;
			if (filePath == null || filePath.Length == 0)
				return;

			treeView_BTree.LoadProtoBuf(filePath);
			treeView_BTree.ExpandAll();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{

		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			RefreshUI();
		}


		private void nodeConnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

		}

		private bool isNewNode(ToolStripMenuItem item)
		{
			if (item.OwnerItem == 新建节点ToolStripMenuItem)
				return true;
			if (item.OwnerItem.OwnerItem == 新建节点ToolStripMenuItem)
				return true;
			return false;
		}

		private AiTreeNode insertAiTreeNode(NodeType nodeType)
		{
			if (selectedNode != null && Util.IsOneChildLimit(selectedNode.LogicNode.Props.Type)
				&& selectedNode.Nodes.Count > 0)
			{
				MessageBox.Show("该类型节点只能有一个子节点!");
				return null;
			}

			AiTreeNode newNode;
			if (selectedNode != null)
			{
				newNode = selectedNode.AiNodes.AiAdd(nodeType);
				selectedNode.ExpandAll();
			}
			else
			{
				// 加根节点
				newNode = treeView_BTree.AiRoot = AiTreeNodeCollection.AiNew(nodeType);
			}
			return newNode;
		}

		private void replaceAiTreeNode(NodeType nodeType)
		{
			if (selectedNode == null || nodeType == selectedNode.LogicNode.Props.Type)
				return;

			if (Util.IsOneChildLimit(nodeType)
				&& selectedNode.Nodes.Count > 0)
			{
				MessageBox.Show("该类型节点只能有一个子节点!");
				return;
			}

			if (treeView_BTree.AiRoot == selectedNode)
				treeView_BTree.RootReplace(nodeType);
			else
				AiTreeNodeCollection.AiReplace(selectedNode, nodeType);

			selectedNodeChange(selectedNode);
		}

		private void selectorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = SelectorNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void sequenceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = SequenceNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void parallelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = ParallelNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void conditionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = ConditionNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void actionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = ActionNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void linkToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = LinkNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void notToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = DecoratorNotNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void loopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = DecoratorLoopNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void timerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = DecoratorTimerNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void counterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = DecoratorCounterNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void randToolStripMenuItem_Click(object sender, EventArgs e)
		{
			NodeType nodeType = DecoratorRandNode.StaticClassType;
			if (isNewNode(sender as ToolStripMenuItem))
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void treeView_BTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			selectedNodeChange((AiTreeNode)e.Node);
		}

		private void treeView_BTree_MouseClick(object sender, MouseEventArgs e)
		{
			selectedNodeChange(selectedNode);
		}

		private void treeView_BTree_MouseUp(object sender, MouseEventArgs e)
		{
			TreeViewHitTestInfo info = treeView_BTree.HitTest(e.X, e.Y);
			
			if (e.Button == MouseButtons.Left)
			{
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (info.Location == TreeViewHitTestLocations.RightOfLabel ||
					info.Location == TreeViewHitTestLocations.None)
					selectedNodeChange(null);
				else
					selectedNodeChange((AiTreeNode)treeView_BTree.SelectedNode);

				treeView_BTree.SelectedNode = selectedNode;
				contextMenuStrip_Node.Show(Cursor.Position);
			}
		}

	
		private void validateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			RefreshUI();
		}

		private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

			treeView_BTree.Remove(selectedNode.LogicNode.Props.Key);
		}

		private void treeView_BTree_ItemDrag(object sender, ItemDragEventArgs e)
		{
			AiTreeNode tn = e.Item as AiTreeNode;
			//根节点不允许拖放操作
			if ((e.Button == MouseButtons.Left) && (tn != null) && (tn.Parent != null))
			{
				DoDragDrop(tn, DragDropEffects.Move);
			}
		}

		private void treeView_BTree_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(typeof(AiTreeNode)))
			{
				e.Effect = DragDropEffects.Move;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void treeView_BTree_DragDrop(object sender, DragEventArgs e)
		{
			AiTreeNode treeNode;;
			if (e.Data.GetDataPresent(typeof(AiTreeNode)))
			{
				// 拖放的目标节点
				AiTreeNode targetTreeNode;
				// 获取当前光标所处的坐标
				// 定义一个位置点的变量，保存当前光标所处的坐标点
				Point point = ((AiTreeView)sender).PointToClient(new Point(e.X, e.Y));
				// 根据坐标点取得处于坐标点位置的节点
				targetTreeNode = (AiTreeNode)((AiTreeView)sender).GetNodeAt(point);
				// 获取被拖动的节点
				treeNode = (AiTreeNode)e.Data.GetData(typeof(AiTreeNode));
				// 判断拖动的节点与目标节点是否是同一个,同一个不予处理
				if (treeNode.Name != targetTreeNode.Name)
				{
					treeNode.Move(targetTreeNode);
					targetTreeNode.ExpandAll();
				}

			}

			if (dragMoveNode != null)
				dragMoveNode.BackColor = Color.White;
			dragMoveNode = null;
		}

		private void treeView_BTree_DragLeave(object sender, EventArgs e)
		{

		}

		private void treeView_BTree_DragOver(object sender, DragEventArgs e)
		{
			if (dragMoveNode != null)
				dragMoveNode.BackColor = Color.White;

			Point point = ((AiTreeView)sender).PointToClient(new Point(e.X, e.Y));
			AiTreeNode moveNode = (AiTreeNode)((AiTreeView)sender).GetNodeAt(point);
			moveNode.BackColor = Color.Gray;
			dragMoveNode = moveNode;
		}

		private void treeView_BTree_MouseMove(object sender, MouseEventArgs e)
		{
		}

		private void 上移节点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

			selectedNode.UpPos();
		}

		private void 下移节点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

			selectedNode.DownPos();
		}

		
		
	}


}