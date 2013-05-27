using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class LinkNode : Node
	{
		public LinkNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "外链"; }
		}

		public override string ClassNameEn
		{
			get { return "Link"; }
		}

		public override NodeType InitClassType
		{
			get { return StaticClassType; }
		}

		public static NodeType StaticClassType
		{
			get { return NodeType.NodeType_Link; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new LinkNodeProperties();
				return properties;
			}
		}


		public LinkNodeProperties DerivedProps
		{
			get { return properties as LinkNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// Link节点属性

	public class LinkNodeProperties : NodeProperties
	{
		private string subTreeName = "";
		[CategoryAttribute("外链设置"),
		DescriptionAttribute("树名称")]
		public virtual string SubTreeName
		{
			get { return subTreeName; }
			set { subTreeName = value; }
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.link == null)
				return false;

			subTreeName = node.link.sub_tree_name;
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.link = new BehaviorPB.Link();
			node.link.sub_tree_name = subTreeName;
			return true;
		}
	}
}
