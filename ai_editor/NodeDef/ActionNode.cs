using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

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

		public override int ClassType
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
		private string scriptPath;

		[CategoryAttribute("��Ϊ����"),
		DescriptionAttribute("�ű�·��"),
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

			if (node.action == null)
				return false;

			scriptPath = node.action.script_path;
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
