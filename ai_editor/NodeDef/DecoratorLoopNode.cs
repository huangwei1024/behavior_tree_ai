using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class DecoratorLoopNode : Node
	{
		public DecoratorLoopNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "循环"; }
		}

		public override string ClassNameEn
		{
			get { return "DecoratorLoop"; }
		}

		public override int InitClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_DecoratorLoop; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new DecoratorLoopNodeProperties();
				return properties;
			}
		}


		public DecoratorLoopNodeProperties DerivedProps
		{
			get { return properties as DecoratorLoopNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// DecoratorLoop节点属性

	public class DecoratorLoopNodeProperties : NodeProperties
	{
		private int loopCount;

		[CategoryAttribute("循环设置"),
		DescriptionAttribute("循环次数")]
		public virtual int LoopCount
		{
			get { return loopCount; }
			set { loopCount = value; }
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.d_loop == null)
				return false;

			loopCount = node.d_loop.loop_cnt;
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.d_loop = new BehaviorPB.DecoratorLoop();
			node.d_loop.loop_cnt = loopCount;
			return true;
		}
	}
}
