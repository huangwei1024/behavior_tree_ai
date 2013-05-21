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
			get { return "����"; }
		}

		public override string ClassNameEn
		{
			get { return "Condition"; }
		}

		public override NodeType InitClassType
		{
			get { return StaticClassType; }
		}

		public static NodeType StaticClassType
		{
			get { return NodeType.NodeType_Condition; }
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
	// Condition�ڵ�����

	public class ConditionNodeProperties : NodeProperties
	{

		private string scriptPath;
		[CategoryAttribute("��������"),
		DescriptionAttribute("�ű�·��"),
		EditorAttribute(typeof(PropertyGridFileItem), typeof(System.Drawing.Design.UITypeEditor))]
		public virtual string ScriptPath
		{
			get { return scriptPath; }
			set
			{ 
				scriptPath = value;
				if (value.Length > 0)
					ConditionType = Condition.Type.Null; // ����
			}
		}

		private Condition.Type conditionType = Condition.Type.Null;
		[CategoryAttribute("��������"),
		DescriptionAttribute("��������")]
		public virtual Condition.Type ConditionType
		{
			get { return conditionType; }
			set 
			{
				conditionType = value;
				if (value == Condition.Type.Null)
					Type = ConditionNode.StaticClassType;
				else
				{
					Type = (NodeType)conditionType;
					scriptPath = "";
				}
			}
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.condition == null)
				return false;

			scriptPath = node.condition.script_path;
			conditionType = (Condition.Type)Type;
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



// 		private string conditionType;
// 		[CategoryAttribute("��������"),
// 		DescriptionAttribute("��������"),
// 	   TypeConverter(typeof(ConditionNameConverter))]
// 		public virtual string ConditionType
// 		{
// 			get { return conditionType; }
// 			set
// 			{
// 				if (sMapConditionName.ContainsKey(value))
// 				{
// 					conditionType = value;
// 					Type = (NodeType)sMapConditionName[value];
// 					ScriptPath = ""; // ����
// 				}
// 				else
// 				{
// 					conditionType = "";
// 					Type = ConditionNode.StaticClassType;
// 				}
// 			}
// 		}
// 
// 		// ConditionType ���������б�
// 		public class ConditionNameConverter : StringConverter
// 		{
// 			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
// 			{
// 				return true;
// 			}
// 
// 			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
// 			{
// 				return new StandardValuesCollection(sMapConditionName.Keys);
// 			}
// 		}