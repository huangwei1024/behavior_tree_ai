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

		[CategoryAttribute("全局设置"),
		ReadOnlyAttribute(true),
		DescriptionAttribute("全局节点索引")]
		public virtual string Key
		{
			get { return node.Key; }
		}

		[CategoryAttribute("全局设置"),
		DescriptionAttribute("节点名称")]
		public virtual string Name
		{
			get { return name; }
			set { name = value; node.DispName = name; }
		}

		[CategoryAttribute("全局设置"),
		DescriptionAttribute("节点描述")]
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
				// 可以打开任何特定的对话框
				OpenFileDialog dialog = new OpenFileDialog();
				dialog.AddExtension = false;
				dialog.Filter = "脚本文件|*.py;*.lua";
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

		[CategoryAttribute("Condition设置"),
		DescriptionAttribute("脚本路径"),
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

		[CategoryAttribute("Action设置"),
		DescriptionAttribute("脚本路径"),
		EditorAttribute(typeof(PropertyGridFileItem), typeof(System.Drawing.Design.UITypeEditor))]
		public virtual string ScriptPath
		{
			get { return scriptPath; }
			set { scriptPath = value; }
		}


		[CategoryAttribute("Action设置"),
		DescriptionAttribute("类型"),
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

		[CategoryAttribute("Action设置"),
		DescriptionAttribute("Move参数")]
		public Action_Move ActionMove
		{
			set { actionMove = value; }
			get { return actionMove; }
		}
	}
}
