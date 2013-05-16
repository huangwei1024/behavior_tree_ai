using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;

namespace ai_editor.NodeDef
{
	public class SelectorNode : Node
	{
		public SelectorNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "选择"; }
		}

		public override string ClassNameEn
		{
			get { return "Selector"; }
		}

		public override int ClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_Selector; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new SelectorNodeProperties();
				return properties;
			}
		}


		public SelectorNodeProperties DerivedProps
		{
			get { return properties as SelectorNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// Selector节点属性

	public class SelectorNodeProperties : NodeProperties
	{
	}
}
