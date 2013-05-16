using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class ConditionNode : Node
	{
		public ConditionNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "条件"; }
		}

		public override string ClassNameEn
		{
			get { return "Condition"; }
		}

		public override int ClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_Condition; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new ConditionNodeProperties();
				return properties;
			}
		}


		public ConditionNodeProperties DerivedProps
		{
			get { return properties as ConditionNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// Condition节点属性

	public class ConditionNodeProperties : NodeProperties
	{
		private string scriptPath;

		[CategoryAttribute("条件设置"),
		DescriptionAttribute("脚本路径"),
		EditorAttribute(typeof(PropertyGridFileItem), typeof(System.Drawing.Design.UITypeEditor))]
		public virtual string ScriptPath
		{
			get { return scriptPath; }
			set { scriptPath = value; }
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.condition == null)
				return false;

			scriptPath = node.condition.script_path;
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.condition = new BehaviorPB.Condition();
			node.condition.script_path = scriptPath;
			return true;
		}
	}
}
