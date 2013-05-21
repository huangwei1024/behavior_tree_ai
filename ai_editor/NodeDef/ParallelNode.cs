using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class ParallelNode : Node
	{
		public ParallelNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "����"; }
		}

		public override string ClassNameEn
		{
			get { return "Parallel"; }
		}

		public override NodeType InitClassType
		{
			get { return StaticClassType; }
		}

		public static NodeType StaticClassType
		{
			get { return NodeType.NodeType_Parallel; }
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
	// Parallel�ڵ�����

	public class ParallelNodeProperties : NodeProperties
	{
		private ParallelPolicy policy;

		[CategoryAttribute("��������"),
		DescriptionAttribute("���в���")]
		public virtual ParallelPolicy Policy
		{
			get { return policy; }
			set { policy = value; }
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.parallel == null)
				return false;

			policy = node.parallel.policy;
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.parallel = new BehaviorPB.Parallel();
			node.parallel.policy = policy;
			return true;
		}
	}
}
