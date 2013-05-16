using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class DecoratorTimerNode : Node
	{
		public DecoratorTimerNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "定时器"; }
		}

		public override string ClassNameEn
		{
			get { return "DecoratorTimer"; }
		}

		public override int ClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_DecoratorTimer; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new DecoratorTimerNodeProperties();
				return properties;
			}
		}


		public DecoratorTimerNodeProperties DerivedProps
		{
			get { return properties as DecoratorTimerNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// DecoratorTimer节点属性

	public class DecoratorTimerNodeProperties : NodeProperties
	{
		private int elpase;

		[CategoryAttribute("定时器设置"),
		DescriptionAttribute("时长")]
		public virtual int Elpase
		{
			get { return elpase; }
			set { elpase = value; }
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.d_timer == null)
				return false;

			elpase = node.d_timer.elpase;
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.d_timer = new BehaviorPB.DecoratorTimer();
			node.d_timer.elpase = elpase;
			return true;
		}
	}
}
