using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;

namespace ai_editor.NodeDef
{
	public class SequenceNode : Node
	{
		public SequenceNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "À≥–Ú"; }
		}

		public override string ClassNameEn
		{
			get { return "Sequence"; }
		}

		public override int ClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_Sequence; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new SequenceNodeProperties();
				return properties;
			}
		}


		public SequenceNodeProperties DerivedProps
		{
			get { return properties as SequenceNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// SequenceΩ⁄µ„ Ù–‘

	public class SequenceNodeProperties : NodeProperties
	{
	}
}
