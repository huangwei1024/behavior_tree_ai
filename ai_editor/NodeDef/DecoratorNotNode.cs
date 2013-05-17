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
			get { return "ȡ��"; }
		}

		public override string ClassNameEn
		{
			get { return "DecoratorNot"; }
		}

		public override int InitClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_DecoratorNot; }
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
	// DecoratorNot�ڵ�����

	public class DecoratorNotNodeProperties : NodeProperties
	{
	}
}
