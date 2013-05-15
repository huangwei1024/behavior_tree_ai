using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Xml;

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

		public void SaveXML(XmlDocument doc)
		{
			XmlElement trees = doc.CreateElement("BTrees");
			XmlElement session = doc.CreateElement("Session");
			trees.AppendChild(session);
			session.SetAttribute("SelectorNode.ObjCnt", SelectorNode.ObjCnt.ToString());
			session.SetAttribute("SequenceNode.ObjCnt", SequenceNode.ObjCnt.ToString());
			session.SetAttribute("ParallelNode.ObjCnt", ParallelNode.ObjCnt.ToString());
			session.SetAttribute("ActionNode.ObjCnt", ActionNode.ObjCnt.ToString());
			session.SetAttribute("ConditionNode.ObjCnt", ConditionNode.ObjCnt.ToString());

			foreach (AiTreeNode subNode in Nodes)
			{
				XmlElement root = doc.CreateElement("BTree");
				root.AppendChild(subNode.SaveXML(doc));
				trees.AppendChild(root);
			}
			doc.AppendChild(trees);
		}

		public void LoadXML(XmlDocument doc)
		{
			AiNodes.AiClear();

			XmlNode trees = doc.SelectSingleNode("BTrees");
			foreach (XmlNode tree in trees.SelectNodes("BTree"))
			{
				XmlNode root = tree.SelectSingleNode("BTreeNode");
				AiTreeNode aiNode = AiTreeNode.LoadXML((XmlElement)root);
				AiNodes.AiAdd(aiNode);
			}

			XmlElement session = (XmlElement)trees.SelectSingleNode("Session");
			SelectorNode.ObjCnt = Convert.ToInt32(session.GetAttribute("SelectorNode.ObjCnt"));
			SequenceNode.ObjCnt = Convert.ToInt32(session.GetAttribute("SequenceNode.ObjCnt"));
			ParallelNode.ObjCnt = Convert.ToInt32(session.GetAttribute("ParallelNode.ObjCnt"));
			ActionNode.ObjCnt = Convert.ToInt32(session.GetAttribute("ActionNode.ObjCnt"));
			ConditionNode.ObjCnt = Convert.ToInt32(session.GetAttribute("ConditionNode.ObjCnt"));
		}

		public bool IsValid(string desc)
		{
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

		public XmlElement SaveXML(XmlDocument doc)
		{
			XmlElement root = doc.CreateElement("BTreeNode");
			logicNode.Props.Save(doc, root);

			XmlElement nodes = doc.CreateElement("BTreeSubNodes");
			foreach (AiTreeNode subNode in Nodes)
			{
				nodes.AppendChild(subNode.SaveXML(doc));
			}
			root.AppendChild(nodes);
			return root;
		}

		public static AiTreeNode LoadXML(XmlElement xmlNode)
		{
			AiTreeNode aiNode = AiTreeNodeCollection.AiNew(NodeProperties.GetNodeTypeFromXML(xmlNode));
			aiNode.LogicNode.Props.Load(xmlNode);
			aiNode.Name = NodeProperties.GetNodeKeyFromXML(xmlNode);

			XmlNode xmlSubNodes = xmlNode.SelectSingleNode("BTreeSubNodes");
			XmlNodeList xmlSubList = xmlSubNodes.SelectNodes("BTreeNode");
			foreach (XmlNode xmlSubNode in xmlSubList)
			{
				AiTreeNode subNode = AiTreeNode.LoadXML((XmlElement)xmlSubNode);
				aiNode.AiNodes.AiAdd(subNode);
			}

			return aiNode;
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

		public static AiTreeNode AiNew(string nodeType)
		{
			Node pageNewNode = Node.CreateInstance(nodeType);
			AiTreeNode aiNewNode = new AiTreeNode(pageNewNode);
			aiNewNode.Name = pageNewNode.Key;
			aiNewNode.Text = pageNewNode.DispName;
			aiNewNode.ImageKey = aiNewNode.SelectedImageKey = nodeType;

			return aiNewNode;
		}
		public AiTreeNode AiAdd(string nodeType)
		{
			AiTreeNode aiNewNode = AiNew(nodeType);

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
		public void AiClear()
		{
			nodes.Clear();
		}

		public TreeNodeCollection nodes;
	}


}
