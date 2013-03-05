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
			root = new AiTreeNode(null, null);
			root.nodes = this.Nodes;
		}

		public AiTreeNodeCollection AiNodes
		{
			get
			{
				return root.AiNodes;
			}
		}

		public AiTreeNode GetAiNodeAt(int x, int y)
		{
			Point cPointBK = new Point(x, y);
			foreach (AiTreeNode aiNode in AiNodes)
			{
				Point cPoint = cPointBK;
				Point point = aiNode.pageNode.Pos;
				if (cPoint.X < point.X || cPoint.Y < point.Y)
					continue;
				cPoint.X -= point.X;
				cPoint.Y -= point.Y;
				Size size = aiNode.pageNode.Size;
				if (cPoint.X > size.Width || cPoint.Y > size.Height)
					continue;
				Color color = aiNode.pageNode.Bitmap.GetPixel(cPoint.X, cPoint.Y);
				if (color.A > 0)
					return aiNode;
			}
			return null;
		}

		private AiTreeNode root;
	}

	public class AiTreeNode
	{
		public AiTreeNode(Node page, TreeNode view)
		{
			pageNode = page;
			viewNode = view;
			if (viewNode != null)
				nodes = viewNode.Nodes;
			aiNodes = new AiTreeNodeCollection(this);
		}

		public AiTreeNodeCollection AiNodes
		{
			get
			{
				return aiNodes;
			}
		}

		public TreeNodeCollection Nodes
		{
			get
			{
				return nodes;
			}
		}

		public Node pageNode;
		public TreeNode viewNode;
		public TreeNodeCollection nodes;
		public AiTreeNodeCollection aiNodes;
	}

	public class AiTreeNodeCollection : System.Collections.IEnumerable
	{
		public AiTreeNodeCollection(AiTreeNode node)
		{
			aiNode = node;
			subNodes = new Dictionary<string, AiTreeNode>();
		}

		public AiTreeNode AiAdd(string nodeType)
		{
			ImageInfo info = Node.ImageInfoMap[nodeType];
			Node pageNewNode = info.newNode();
			TreeNode viewNewNode = aiNode.Nodes.Add(pageNewNode.Key, pageNewNode.Key, info.name, info.name);
			AiTreeNode aiNewNode = new AiTreeNode(pageNewNode, viewNewNode);

			subNodes[pageNewNode.Key] = aiNewNode;
			return aiNewNode;
		}
		public void AiRemove(string key)
		{
			if (!subNodes.ContainsKey(key))
				return;

			AiTreeNode node = subNodes[key];
			aiNode.Nodes.Remove(node.viewNode);
		}
		public System.Collections.IEnumerator GetEnumerator()
		{
			return new AiTreeNodeEnum(this);
		}

		public AiTreeNode aiNode;
		public Dictionary<string, AiTreeNode> subNodes;
	}

	public class AiTreeNodeEnum : System.Collections.IEnumerator
	{
		private AiTreeNodeCollection collection;
		private Dictionary<string, AiTreeNode>.Enumerator enumerator;
		private Dictionary<string, AiTreeNode>.Enumerator subEnumerator;
		bool subIn;

		public AiTreeNodeEnum(AiTreeNodeCollection collect)
		{
			collection = collect;
			enumerator = collection.subNodes.GetEnumerator();
			subIn = false;
		}

		public bool MoveNext()
		{
			if (subIn)
			{
				if (!subEnumerator.MoveNext())
				{
					subIn = false;
					return false;
				}
				return true;
			}

			if (!enumerator.MoveNext())
				return false;
			return true;
		}

		public void Reset()
		{
			enumerator = collection.subNodes.GetEnumerator();
		}

		public object Current
		{
			get
			{
				if (subIn)
					return subEnumerator.Current.Value;

				if (enumerator.Current.Value.AiNodes.subNodes.Count > 0)
				{
					subEnumerator = (Dictionary<string, AiTreeNode>.Enumerator)enumerator.Current.Value.AiNodes.GetEnumerator();
					subIn = true;
				}
				return enumerator.Current.Value;
			}
		}


	}

}
