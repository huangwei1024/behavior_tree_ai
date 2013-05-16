using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using ai_editor.NodeDef;
using PBNode = BehaviorPB.Node;
using PBTree = BehaviorPB.Tree;
using System.IO;
using ProtoBuf;

namespace ai_editor
{
	//-------------------------------------------------------------------------
	// 树

	public partial class AiTreeView : TreeView
	{
		public AiTreeView()
		{
			InitializeComponent();
		}

		private new TreeNodeCollection Nodes
		{
			get { return base.Nodes; } 
		}

		public AiTreeNode AiRoot
		{
			get { return aiRoot; }
			set 
			{
				if (value == null)
				{
					aiRoot = null;
					base.Nodes.Clear();
					return;
				}

				aiRoot = value;
				aiRoot.LogicNode.TreeNode = new NodeDef.Tree(aiRoot.LogicNode);
				base.Nodes.Clear();
				base.Nodes.Add(aiRoot);
			}
		}

		public void SaveProtoBuf(string path)
		{
			PBTree tree = new PBTree();
			tree.root = new PBNode();
			AiRoot.LogicNode.TreeNode.Props.SaveProtoBuf(tree);
			AiRoot.LogicNode.Props.SaveProtoBuf(tree.root);
			AiRoot.AiNodes.SaveProtoBuf(tree.root);
			
			using (FileStream file = File.Create(path))
			{
				Serializer.Serialize(file, tree);
			}
		}

		public void LoadProtoBuf(string path)
		{
			PBTree tree;
			using (FileStream file = File.OpenRead(path))
			{
				tree = Serializer.Deserialize<PBTree>(file);
			}

			Node.sIDMax = 0;

			AiRoot = AiTreeNodeCollection.AiNew(tree.root.type);
			AiRoot.LogicNode.Props.LoadProtoBuf(tree.root);
			AiRoot.LogicNode.TreeNode.Props.LoadProtoBuf(tree);
			AiRoot.AiNodes.LoadProtoBuf(tree.root);

			// 恢复上次最大node id
			Node.sIDCounter = Node.sIDMax;
			Refresh();
		}

		public bool IsValid(string desc)
		{
			return true;
		}

		public override void Refresh()
		{
			AiRoot.Refresh();
			AiRoot.AiNodes.AiRefresh();

			base.Refresh();
		}

		public void Remove(string key)
		{
			if (key == AiRoot.LogicNode.Props.Key)
				AiRoot = null;
			else
				AiRoot.AiNodes.AiRemove(key, true);
		}

		private AiTreeNode aiRoot;
	}

	//-------------------------------------------------------------------------
	// 节点

	public class AiTreeNode : TreeNode
	{
		public AiTreeNode(Node logic)
		{
			logicNode = logic;
			aiNodes = new AiTreeNodeCollection(Nodes);
		}

		public Node LogicNode
		{
			get { return logicNode; }
		}

		public AiTreeNodeCollection AiNodes
		{
			get { return aiNodes; }
		}

		public void Refresh()
		{
			Name = logicNode.Props.Key;
			Text = logicNode.Props.Name;
			ImageKey = SelectedImageKey = logicNode.ImageName;
		}

		private Node logicNode;
		private AiTreeNodeCollection aiNodes;
	}

	//-------------------------------------------------------------------------
	// 节点集合

	public class AiTreeNodeCollection
	{
		public AiTreeNodeCollection(TreeNodeCollection _nodes)
		{
			nodes = _nodes;
		}

		public static AiTreeNode AiNew(int nodeType)
		{
			AiTreeNode aiNewNode = new AiTreeNode(NodeFactory.CreateInstance(nodeType));
			aiNewNode.Refresh();

			return aiNewNode;
		}

		public AiTreeNode AiAdd(int nodeType)
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

		public void AiRefresh()
		{
			for (int i = 0; i < nodes.Count; ++i)
			{
				AiTreeNode subNode = nodes[i] as AiTreeNode;
				subNode.Refresh();

				// dfs
				subNode.AiNodes.AiRefresh();
			}
		}

		public virtual AiTreeNode this[int index] 
		{
			get { return nodes[index] as AiTreeNode; }
			set { nodes[index] = value; }
		}

		
		public virtual bool LoadProtoBuf(BehaviorPB.Node pbNode)
		{
			AiClear();

			for (int i = 0; i < pbNode.nodes.Count; ++i)
			{
				BehaviorPB.Node subPBNode = pbNode.nodes[i];
				AiTreeNode subNode = AiAdd(subPBNode.type);
				subNode.LogicNode.Props.LoadProtoBuf(subPBNode);
				
				// dfs
				subNode.AiNodes.LoadProtoBuf(subPBNode);
			}

			return true;
		}

		public virtual bool SaveProtoBuf(BehaviorPB.Node pbNode)
		{
			pbNode.nodes.Clear();

			for (int i = 0; i < nodes.Count; ++i)
			{
				AiTreeNode subNode = nodes[i] as AiTreeNode;
				BehaviorPB.Node subPBNode = new BehaviorPB.Node();
				subNode.LogicNode.Props.SaveProtoBuf(subPBNode);
				pbNode.nodes.Add(subPBNode);

				// dfs
				subNode.AiNodes.SaveProtoBuf(subPBNode);
			}

			return true;
		}

		public TreeNodeCollection nodes;
	}


}
