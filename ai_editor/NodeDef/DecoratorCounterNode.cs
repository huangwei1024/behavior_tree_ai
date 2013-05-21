using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class DecoratorCounterNode : Node
	{
		public DecoratorCounterNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "计数器"; }
		}

		public override string ClassNameEn
		{
			get { return "DecoratorCounter"; }
		}

		public override NodeType InitClassType
		{
			get { return StaticClassType; }
		}

		public static NodeType StaticClassType
		{
			get { return NodeType.NodeType_DecoratorCounter; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new DecoratorCounterNodeProperties();
				return properties;
			}
		}

		public DecoratorCounterNodeProperties DerivedProps
		{
			get { return properties as DecoratorCounterNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// DecoratorCounter节点属性

	public class DecoratorCounterNodeProperties : NodeProperties
	{
		private int limitCount;
		[CategoryAttribute("计数器设置"),
		DescriptionAttribute("次数限制")]
		public virtual int LimitCount
		{
			get { return limitCount; }
			set { limitCount = value; }
		}

		private DecoratorCounter.Type decoratorCounterType = DecoratorCounter.Type.Null;
		[CategoryAttribute("计数器设置"),
	   DescriptionAttribute("计数器类型")]
		public virtual DecoratorCounter.Type DecoratorCounterType
		{
			get { return decoratorCounterType; }
			set
			{
				decoratorCounterType = value;
				if (value == DecoratorCounter.Type.Null)
					Type = DecoratorCounterNode.StaticClassType;
				else
					Type = (NodeType)decoratorCounterType;
			}
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.d_counter == null)
				return false;

			limitCount = node.d_counter.limit_cnt;
			decoratorCounterType = (DecoratorCounter.Type)Type;
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.d_counter = new BehaviorPB.DecoratorCounter();
			node.d_counter.limit_cnt = limitCount;
			return true;
		}
	}
}
