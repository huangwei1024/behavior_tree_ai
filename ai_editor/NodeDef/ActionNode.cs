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

		public override NodeType InitClassType
		{
			get { return StaticClassType; }
		}

		public static NodeType StaticClassType
		{
			get { return NodeType.NodeType_Action; }
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
			set 
			{ 
				scriptPath = value;
				if (value.Length > 0)
					ActionType = Action.Type.Null; // ����
			}
		}


		// editor
		private Action.Type actionType = Action.Type.Null;
		[CategoryAttribute("��Ϊ����"),
		DescriptionAttribute("��Ϊ����")]
		public virtual Action.Type ActionType
		{
			get { return actionType; }
			set 
			{
				actionType = value;
				if (value == Action.Type.Null)
					Type = ActionNode.StaticClassType;
				else
				{
					Type = (NodeType)value;
					scriptPath = ""; // ����
				}
				
			}
		}


		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.action == null)
				return false;

			scriptPath = node.action.script_path;
			actionType = (Action.Type)Type;
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
