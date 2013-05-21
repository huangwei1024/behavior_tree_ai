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

		public override NodeType InitClassType
		{
			get { return StaticClassType; }
		}

		public static NodeType StaticClassType
		{
			get { return NodeType.NodeType_DecoratorLoop; }
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
			set 
			{ 
				loopCount = value;
				loopKey = ""; // 互斥
			}
		}

		private string loopKey;
		[CategoryAttribute("循环设置"),
		DescriptionAttribute("黑板循环索引")]
		public virtual string LoopBBKey
		{
			get { return loopKey; }
			set 
			{
				loopKey = value;
				loopCount = 0; // 互斥
			}
		}

		private string loopBBiWrite;
		[CategoryAttribute("循环设置"),
		DescriptionAttribute("黑板计数索引")]
		public virtual string LoopBBiName
		{
			get { return loopBBiWrite; }
			set { loopBBiWrite = value; }
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.d_loop == null)
				return false;

			loopCount = node.d_loop.loop_cnt;
			loopKey = node.d_loop.loop_key;
			loopBBiWrite = node.d_loop.bb_i;
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.d_loop = new BehaviorPB.DecoratorLoop();
			node.d_loop.loop_cnt = loopCount;
			node.d_loop.loop_key = loopKey;
			node.d_loop.bb_i = loopBBiWrite;
			return true;
		}
	}
}
