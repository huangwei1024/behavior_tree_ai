using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ai_editor.NodeDef;

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
			�滻�ڵ�ToolStripMenuItem.DropDown = �½��ڵ�ToolStripMenuItem.DropDown;
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
				�½��ڵ�ToolStripMenuItem.Text = "�½��ڵ�";
				�滻�ڵ�ToolStripMenuItem.Enabled = true;
				ɾ���ڵ�ToolStripMenuItem.Enabled = true;
				���ƽڵ�ToolStripMenuItem.Enabled = true;
				���ƽڵ�ToolStripMenuItem.Enabled = true;
			}
			else
			{
				�½��ڵ�ToolStripMenuItem.Text = "�½����ڵ�";
				�滻�ڵ�ToolStripMenuItem.Enabled = false;
				ɾ���ڵ�ToolStripMenuItem.Enabled = false;
				���ƽڵ�ToolStripMenuItem.Enabled = false;
				���ƽڵ�ToolStripMenuItem.Enabled = false;
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

		private AiTreeNode insertAiTreeNode(int nodeType)
		{
			if (selectedNode != null && Util.IsOneChildLimit(selectedNode.LogicNode.Props.Type)
				&& selectedNode.Nodes.Count > 0)
			{
				MessageBox.Show("�����ͽڵ�ֻ����һ���ӽڵ�!");
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
				// �Ӹ��ڵ�
				newNode = treeView_BTree.AiRoot = AiTreeNodeCollection.AiNew(nodeType);
			}
			return newNode;
		}

		private void replaceAiTreeNode(int nodeType)
		{
			if (selectedNode == null || nodeType == selectedNode.LogicNode.Props.Type)
				return;

			if (Util.IsOneChildLimit(nodeType)
				&& selectedNode.Nodes.Count > 0)
			{
				MessageBox.Show("�����ͽڵ�ֻ����һ���ӽڵ�!");
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
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = SelectorNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void sequenceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = SequenceNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void parallelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = ParallelNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void conditionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = ConditionNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void actionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = ActionNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void linkToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = LinkNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void notToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = DecoratorNotNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void loopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = DecoratorLoopNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void timerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = DecoratorTimerNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
				insertAiTreeNode(nodeType);
			else
				replaceAiTreeNode(nodeType);
		}

		private void counterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			int nodeType = DecoratorCounterNode.StaticClassType;
			if (item.OwnerItem == �½��ڵ�ToolStripMenuItem)
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

		private void ɾ���ڵ�ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

			treeView_BTree.Remove(selectedNode.LogicNode.Props.Key);
		}

		private void treeView_BTree_ItemDrag(object sender, ItemDragEventArgs e)
		{
			AiTreeNode tn = e.Item as AiTreeNode;
			//���ڵ㲻�����ϷŲ���
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
				// �Ϸŵ�Ŀ��ڵ�
				AiTreeNode targetTreeNode;
				// ��ȡ��ǰ�������������
				// ����һ��λ�õ�ı��������浱ǰ��������������
				Point point = ((AiTreeView)sender).PointToClient(new Point(e.X, e.Y));
				// ���������ȡ�ô��������λ�õĽڵ�
				targetTreeNode = (AiTreeNode)((AiTreeView)sender).GetNodeAt(point);
				// ��ȡ���϶��Ľڵ�
				treeNode = (AiTreeNode)e.Data.GetData(typeof(AiTreeNode));
				// �ж��϶��Ľڵ���Ŀ��ڵ��Ƿ���ͬһ��,ͬһ�����账��
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

		private void ���ƽڵ�ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

			selectedNode.UpPos();
		}

		private void ���ƽڵ�ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

			selectedNode.DownPos();
		}
		
	}


}