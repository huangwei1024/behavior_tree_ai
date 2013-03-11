using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ai_editor
{
	public partial class AiTreeView : TreeView
	{
		public AiTreeView()
		{
			InitializeComponent();
			aiNodes = new AiTreeNodeCollection(Nodes);
		}

		public AiTreeNodeCollection AiNodes
		{
			get
			{
				return aiNodes;
			}
		}


		private AiTreeNodeCollection aiNodes;
	}

	public class AiTreeNode : TreeNode
	{
		public AiTreeNode(Node logic)
		{
			logicNode = logic;
			logicNode.AiNode = this;
			aiNodes = new AiTreeNodeCollection(Nodes);
		}

		public Node LogicNode
		{
			get
			{
				return logicNode;
			}
		}

		public AiTreeNodeCollection AiNodes
		{
			get
			{
				return aiNodes;
			}
		}

		public string AiFullPath
		{
			get
			{
				List<string> listPath = new List<string>();
				TreeNode node = this;
				while (node != null)
				{
					listPath.Add(node.Name);
					node = node.Parent;
				}
				listPath.Reverse();
				return string.Join(".", listPath.ToArray());
			}
		}

		private Node logicNode;
		private AiTreeNodeCollection aiNodes;
	}

	public class AiTreeNodeCollection
	{
		public AiTreeNodeCollection(TreeNodeCollection _nodes)
		{
			nodes = _nodes;
		}

		public AiTreeNode AiAdd(string nodeType)
		{
			Node pageNewNode = Node.CreateInstance(nodeType);
			AiTreeNode aiNewNode = new AiTreeNode(pageNewNode);
			aiNewNode.Name = pageNewNode.Key;
			aiNewNode.Text = pageNewNode.DispName;
			aiNewNode.ImageKey = aiNewNode.SelectedImageKey = nodeType;

			nodes.Add(aiNewNode);
			return aiNewNode;
		}
		public AiTreeNode AiAdd(AiTreeNode node)
		{
			nodes.Add(node);
			return node;
		}
		public bool AiRemove(string key, bool searchAll)
		{
			TreeNode[] nodeArr = nodes.Find(key, false);
			if (nodeArr.Length == 0)
			{
				if (searchAll)
				{
					foreach (AiTreeNode sunNode in nodes)
						if (sunNode.AiNodes.AiRemove(key, true))
							return true;
				}
				return false;
			}

			nodes.Remove(nodeArr[0]);
			return true;
		}

		public TreeNodeCollection nodes;
	}


}
