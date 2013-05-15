using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;

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
			SelectorNode.ObjCnt = 0;
			SequenceNode.ObjCnt = 0;
			ParallelNode.ObjCnt = 0;
			ActionNode.ObjCnt = 0;
			ConditionNode.ObjCnt = 0;
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

			XmlDocument xmldoc = new XmlDocument();
			treeView_BTree.SaveXML(xmldoc);
			xmldoc.Save(filePath);
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

			XmlDocument xmldoc = new XmlDocument();
			xmldoc.Load(filePath);
			treeView_BTree.LoadXML(xmldoc);
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

		private AiTreeNode insertAiTreeNode(string nodeType)
		{
			AiTreeNode newNode;
			if (selectedNode != null)
			{
				newNode = selectedNode.AiNodes.AiAdd(nodeType);
				selectedNode.ExpandAll();
			}
			else
			{
				newNode = treeView_BTree.AiNodes.AiAdd(nodeType);
			}
			return newNode;
		}

		private void selectorToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(SelectorNode.Name);
		}

		private void sequenceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(SequenceNode.Name);
		}

		private void parallelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(ParallelNode.Name);
		}

		private void conditionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(ConditionNode.Name);
		}

		private void actionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			insertAiTreeNode(ActionNode.Name);
		}

		private void treeView_BTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			selectedNodeChange((AiTreeNode)e.Node);
		}

		private void 新建根节点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			selectedNode = null;
			insertAiTreeNode(SelectorNode.Name);
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
					contextMenuStrip_Node.Show(Cursor.Position);
				}
				else
				{
					contextMenuStrip_Tree.Show(Cursor.Position);
				}
			}
		}

	
		private void validateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string desc = new string;
			if (treeView_BTree.IsValid(desc))
				MessageBox.Show("BTree节点正常");
			else
				MessageBox.Show(desc);
		}


	}


}