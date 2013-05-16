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
		private string filePath;

		public FormMain()
		{
			InitializeComponent();
		}

		private void RefreshUI()
		{
			propertyGrid1.Refresh();
			treeView_BTree.Refresh();
		}

		private void selectedNodeChange(AiTreeNode newSelected)
		{
			if (newSelected == selectedNode)
				return;

			selectedNode = newSelected;
			if (selectedNode != null)
			{
				propertyGrid1.SelectedObject = newSelected.LogicNode.Props;
				if (newSelected.LogicNode.TreeNode != null)
					propertyGrid1.SelectedObject = selectedNode.LogicNode.TreeNode.Props;
				selectedNode.ExpandAll();
			}
			else
				propertyGrid1.SelectedObject = null;
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
			treeView_BTree.Nodes.Clear();
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

		private void selectorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(SelectorNode.StaticClassType);
		}

		private void sequenceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(SequenceNode.StaticClassType);
		}

		private void parallelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(ParallelNode.StaticClassType);
		}

		private void conditionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(ConditionNode.StaticClassType);
		}

		private void actionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(ActionNode.StaticClassType);
		}

		private void linkToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(LinkNode.StaticClassType);
		}

		private void notToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(DecoratorNotNode.StaticClassType);
		}

		private void loopToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(DecoratorLoopNode.StaticClassType);
		}

		private void timerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(DecoratorTimerNode.StaticClassType);
		}

		private void counterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(DecoratorCounterNode.StaticClassType);
		}


		private void treeView_BTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			selectedNodeChange((AiTreeNode)e.Node);
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

				if (selectedNode != null)
				{
					新建节点ToolStripMenuItem.Text = "新建节点";
					删除节点ToolStripMenuItem.Enabled = true;
				}
				else
				{
					新建节点ToolStripMenuItem.Text = "新建根节点";
					删除节点ToolStripMenuItem.Enabled = false;
				}
				contextMenuStrip_Node.Show(Cursor.Position);
			}
		}

	
		private void validateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			treeView_BTree.Refresh();
		}

		private void 删除节点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

			treeView_BTree.Remove(selectedNode.LogicNode.Props.Key);
		}

	}


}