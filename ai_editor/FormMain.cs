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

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (AboutBox about = new AboutBox())
			{
				about.ShowDialog();
			}
		}


		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		private void dfs_save(XmlDocument doc, XmlElement root, TreeNodeCollection nodes)
		{


			foreach (AiTreeNode node in nodes)
			{
				XmlElement elem = doc.CreateElement("BTreeNode");
				dfs_save(doc, elem, node.Nodes);
				root.AppendChild(elem);
			}
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
			XmlElement root = xmldoc.CreateElement("BTree");
			dfs_save(xmldoc, root, treeView_BTree.Nodes);
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

			// TODO load
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

		private void treeView_BTree_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
			}
			else if (e.Button == MouseButtons.Right)
			{
				删除节点ToolStripMenuItem.Enabled = true;
				selectedNode = (AiTreeNode)treeView_BTree.SelectedNode;
				contextMenuStrip_Node.Show(treeView_BTree.PointToScreen(new Point(e.X, e.Y)));
			}
		}

		private void treeView_BTree_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
			}
			else if (e.Button == MouseButtons.Right)
			{
				selectedNode = null;
				contextMenuStrip_Tree.Show(treeView_BTree.PointToScreen(new Point(e.X, e.Y)));
			}
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
			e.Node.ExpandAll();
			AiTreeNode aiNode = (AiTreeNode)e.Node;
			propertyGrid1.SelectedObject = aiNode.LogicNode.Props;
		}

		private void 新建根节点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			selectedNode = null;
			insertAiTreeNode(SelectorNode.Name);
		}

		

	}


}