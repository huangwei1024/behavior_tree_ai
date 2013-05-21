using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class DecoratorNotNode : Node
	{
		public DecoratorNotNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "取反"; }
		}

		public override string ClassNameEn
		{
			get { return "DecoratorNot"; }
		}

		public override NodeType InitClassType
		{
			get { return StaticClassType; }
		}

		public static NodeType StaticClassType
		{
			get { return NodeType.NodeType_DecoratorNot; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new DecoratorNotNodeProperties();
				return properties;
			}
		}


		public DecoratorNotNodeProperties DerivedProps
		{
			get { return properties as DecoratorNotNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// DecoratorNot节点属性

	public class DecoratorNotNodeProperties : NodeProperties
	{
	}
}
