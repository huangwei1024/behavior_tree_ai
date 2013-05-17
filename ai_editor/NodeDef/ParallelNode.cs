using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;

namespace ai_editor.NodeDef
{
	public class ParallelNode : Node
	{
		public ParallelNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "并行"; }
		}

		public override string ClassNameEn
		{
			get { return "Parallel"; }
		}

		public override int InitClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_Parallel; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new ParallelNodeProperties();
				return properties;
			}
		}


		public ParallelNodeProperties DerivedProps
		{
			get { return properties as ParallelNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// Parallel节点属性

	public class ParallelNodeProperties : NodeProperties
	{
	}
}
