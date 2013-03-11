using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace ai_editor
{

	public class NodeProperties
	{
		public NodeProperties(Node _node)
		{
			node = _node;
		}

		protected Node node;
		private string desc;
		private string name;

		[CategoryAttribute("ȫ������"),
		ReadOnlyAttribute(true),
		DescriptionAttribute("ȫ�ֽڵ�����")]
		public virtual string Key
		{
			get { return node.Key; }
		}

		[CategoryAttribute("ȫ������"),
		DescriptionAttribute("�ڵ�����")]
		public virtual string Name
		{
			get { return name; }
			set { name = value; node.DispName = name; }
		}

		[CategoryAttribute("ȫ������"),
		DescriptionAttribute("�ڵ�����")]
		public virtual string Desc
		{
			get { return desc; }
			set { desc = value; }
		}
	}



	public class PropertyGridFileItem : UITypeEditor
	{
		public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

			if (edSvc != null)
			{
				// ���Դ��κ��ض��ĶԻ���
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.AddExtension = false;
				dialog.Filter = "�ű��ļ�|*.py;*.lua";
				if (dialog.ShowDialog().Equals(DialogResult.OK))
				{
					return dialog.FileName;
				}
			}
			return value;
		}
	}


	public class ConditionProperties : NodeProperties
	{
		public ConditionProperties(Node _node)
			: base(_node)
		{
		}

		private string scriptPath;

		[CategoryAttribute("Condition����"),
		DescriptionAttribute("�ű�·��"),
		EditorAttribute(typeof(PropertyGridFileItem), typeof(System.Drawing.Design.UITypeEditor))]
		public virtual string ScriptPath
		{
			get { return scriptPath; }
			set { scriptPath = value; }
		}
	}

	public class ActionProperties : NodeProperties
	{
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class Action_Move
		{
			private int x = 0;
			private int y = 0;

			[DescriptionAttribute("X")]
			public int X
			{
				set { x = value; }
				get { return x; }
			}

			[DescriptionAttribute("Y")]
			public int Y
			{
				set { y = value; }
				get { return y; }
			}

			public override string ToString()
			{
				return string.Format("({0}, {1})", x, y);
			}
		}

		public enum Action_Type
		{
			PlayAni,
			Move,
			Script,
		}

		private Action_Type actionMode;
		private Action_Move actionMove;
		private string scriptPath;

		public ActionProperties(Node _node)
			: base(_node)
		{
		}

		[CategoryAttribute("Action����"),
		DescriptionAttribute("�ű�·��"),
		EditorAttribute(typeof(PropertyGridFileItem), typeof(System.Drawing.Design.UITypeEditor))]
		public virtual string ScriptPath
		{
			get { return scriptPath; }
			set { scriptPath = value; }
		}


		[CategoryAttribute("Action����"),
		DescriptionAttribute("����"),
		DefaultValueAttribute(Action_Type.Move)]
		public Action_Type ActionType
		{
			set
			{
				actionMode = value;
				actionMove = null;
				if (actionMode == Action_Type.PlayAni)
				{
				}
				else if (actionMode == Action_Type.Move)
				{
					actionMove = new Action_Move();
				}
			}
			get { return actionMode; }
		}

		[CategoryAttribute("Action����"),
		DescriptionAttribute("Move����")]
		public Action_Move ActionMove
		{
			set { actionMove = value; }
			get { return actionMove; }
		}
	}
}
