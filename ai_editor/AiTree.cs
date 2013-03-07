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

		public void PageDraw(Graphics g)
		{
			foreach (AiTreeNode aiNode in AiNodes)
			{
				aiNode.PageNode.Draw(g);
				foreach (AiTreeNode subAiNode in aiNode.AiNodes.subNodes.Values)
				{
					g.DrawLine(Pens.Black, aiNode.PageNode.OutPoint, subAiNode.PageNode.InPoint);
				}
			}
		}

		public AiTreeNode GetAiNodeAt(int x, int y)
		{
			Point cPointBK = new Point(x, y);
			foreach (AiTreeNode aiNode in AiNodes)
			{
				Point cPoint = cPointBK;
				Point point = aiNode.PageNode.Pos;
				if (cPoint.X < point.X || cPoint.Y < point.Y)
					continue;
				cPoint.X -= point.X;
				cPoint.Y -= point.Y;
				Size size = aiNode.PageNode.Size;
				if (cPoint.X > size.Width || cPoint.Y > size.Height)
					continue;
				Color color = aiNode.PageNode.Bitmap.GetPixel(cPoint.X, cPoint.Y);
				if (color.A > 0)
					return aiNode;
			}
			return null;
		}


		private AiTreeNodeCollection aiNodes;
	}

	public class AiTreeNode : TreeNode
	{
		public AiTreeNode(Node page)
		{
			pageNode = page;
			aiNodes = new AiTreeNodeCollection(Nodes);
		}

		public Node PageNode
		{
			get
			{
				return pageNode;
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

		private Node pageNode;
		private AiTreeNodeCollection aiNodes;
	}

	public class AiTreeNodeCollection : System.Collections.IEnumerable
	{
		public AiTreeNodeCollection(TreeNodeCollection _nodes)
		{
			nodes = _nodes;
			subNodes = new Dictionary<string, AiTreeNode>();
		}

		public AiTreeNode AiAdd(string nodeType)
		{
			ImageInfo info = Node.ImageInfoMap[nodeType];
			Node pageNewNode = info.newNode();
			AiTreeNode aiNewNode = new AiTreeNode(pageNewNode);
			aiNewNode.Name = pageNewNode.Key;
			aiNewNode.Text = pageNewNode.Key + " Text";
			aiNewNode.ImageKey = aiNewNode.SelectedImageKey = info.name;

			subNodes[aiNewNode.Name] = aiNewNode;
			nodes.Add(aiNewNode);
			return aiNewNode;
		}
		public AiTreeNode AiAdd(AiTreeNode node)
		{
			subNodes[node.Name] = node;
			nodes.Add(node);
			return node;
		}
		public bool AiRemove(string key, bool searchAll)
		{
			if (!subNodes.ContainsKey(key))
			{
				if (searchAll)
				{
					foreach (AiTreeNode sunNode in subNodes.Values)
						if (sunNode.AiNodes.AiRemove(key, true))
							return true;
				}
				return false;
			}

			AiTreeNode node = subNodes[key];
			subNodes.Remove(node.Name);
			nodes.Remove(node);
			return true;
		}
// 		public void AiRemove(AiTreeNode node)
// 		{
// 			subNodes.Remove(node.Name);
// 			nodes.Remove(node);
// 		}
		public System.Collections.IEnumerator GetEnumerator()
		{
			return new AiTreeNodeEnum(this);
		}

		public TreeNodeCollection nodes;
		public Dictionary<string, AiTreeNode> subNodes;
	}

	public class AiTreeNodeEnum : System.Collections.IEnumerator
	{
		private AiTreeNodeCollection collection;
		private Dictionary<string, AiTreeNode>.Enumerator enumerator;
		private AiTreeNodeEnum subEnumerator;
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
				if (subEnumerator.MoveNext())
					return true;
				subIn = false;
			}

			return enumerator.MoveNext();
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
					return subEnumerator.Current;

				if (enumerator.Current.Value.AiNodes.subNodes.Count > 0)
				{
					subEnumerator = (AiTreeNodeEnum)enumerator.Current.Value.AiNodes.GetEnumerator();
					subIn = true;
				}
				return enumerator.Current.Value;
			}
		}


	}

}
