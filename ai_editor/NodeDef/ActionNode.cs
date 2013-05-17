using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;
using System.Windows.Forms;

namespace ai_editor.NodeDef
{
	public class ActionNode : Node
	{
		public ActionNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "��Ϊ"; }
		}

		public override string ClassNameEn
		{
			get { return "Action"; }
		}

		public override int InitClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_Action; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new ActionNodeProperties();
				return properties;
			}
		}


		public ActionNodeProperties DerivedProps
		{
			get { return properties as ActionNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// Action�ڵ�����

	public class ActionNodeProperties : NodeProperties
	{
		private static Dictionary<string, int> sMapActionName = new Dictionary<string, int>();
		private string scriptPath;

		static ActionNodeProperties()
		{
			//Util.InsertStringIntPair(sMapActionName, "printf_test", (int)BehaviorPB.NodeType.NodeType_PrintfAction);
			sMapActionName["printf_test"] = (int)BehaviorPB.NodeType.NodeType_PrintfAction;
		}

		[CategoryAttribute("��Ϊ����"),
		DescriptionAttribute("�ű�·��"),
		EditorAttribute(typeof(PropertyGridFileItem), typeof(System.Drawing.Design.UITypeEditor))]
		public virtual string ScriptPath
		{
			get { return scriptPath; }
			set 
			{ 
				scriptPath = value;
				if (value.Length > 0)
					ActionType = ""; // ����
			}
		}


		// editor
		private string actionType;
		[CategoryAttribute("��Ϊ����"),
		DescriptionAttribute("��Ϊ����"),
		TypeConverter(typeof(ActionNameConverter))]
		public virtual string ActionType
		{
			get { return actionType; }
			set 
			{
				if (sMapActionName.ContainsKey(value))
				{
					actionType = value;
					Type = sMapActionName[value];
					ScriptPath = ""; // ����
				}
				else
				{
					actionType = "";
					Type = ActionNode.StaticClassType;
				}
				
			}
		}

		// ActionType ���������б�
		public class ActionNameConverter : StringConverter 
		{
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				return new StandardValuesCollection(sMapActionName.Keys);
			}
		}  

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.action == null)
				return false;

			scriptPath = node.action.script_path;
			actionType = Util.GetKeyByValue(sMapActionName, Type);
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.action = new BehaviorPB.Action();
			node.action.script_path = scriptPath;
			return true;
		}
	}
}
