using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using BehaviorPB;

namespace ai_editor.NodeDef
{
	public abstract class Node
	{
		public static int sIDCounter = 0;	// ȫ��node id
		public static int sIDMax = 0;		// �������node id

		protected Tree tree;
		protected NodeProperties properties;

		public Node()
		{
			Props.ID = ++sIDCounter;
			Props.Type = InitClassType;
			Props.Key = string.Format("Node{0}", Props.ID);
			Props.Name = string.Format("{0}{1}", ClassNameCn, Props.ID);
			Props.Desc = "";
		}

		public virtual NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new NodeProperties();
				return properties;
			}
		}

		public abstract string ClassNameCn { get;}

		public abstract string ClassNameEn { get;}

		// ֻ�ڳ�ʼ��ʱ�������ȡ�����ʼ����
		// ĳЩ�ڵ���ں���ѡ����Ľڵ�����, �� ActionNode�е�NodeType_PrintfAction
		// �ڵ����ͻ�ȡ��Props.TypeΪ׼����ֵд��protobuf
		public abstract NodeType InitClassType { get;}

		public virtual string ImageName
		{
			get { return ClassNameEn; }
		}

		public Tree TreeNode
		{
			get { return tree; }
			set { tree = value; }
		}
	}

	//-------------------------------------------------------------------------
	//

	public class NodeFactory
	{
		public static Node CreateInstance(NodeType type)
		{
			switch (type)
			{
			case NodeType.NodeType_Selector:
				return new SelectorNode();
			case NodeType.NodeType_Sequence:
				return new SequenceNode();
			case NodeType.NodeType_Parallel:
				return new ParallelNode();
			case NodeType.NodeType_Condition:
				return new ConditionNode();
			case NodeType.NodeType_Action:
				return new ActionNode();
			case NodeType.NodeType_Link:
				return new LinkNode();
			case NodeType.NodeType_DecoratorNot:
				return new DecoratorNotNode();
			case NodeType.NodeType_DecoratorLoop:
				return new DecoratorLoopNode();
			case NodeType.NodeType_DecoratorTimer:
				return new DecoratorTimerNode();
			case NodeType.NodeType_DecoratorCounter:
				return new DecoratorCounterNode();
			case NodeType.NodeType_DecoratorRand:
				return new DecoratorRandNode();
			
			// test
			case NodeType.NodeType_CntTestAction2_2:
			case NodeType.NodeType_CntTestAction0:
			case NodeType.NodeType_CntTestAction1:
			case NodeType.NodeType_CntTestAction2:
			case NodeType.NodeType_CntTestAction:
				return new ActionNode();
			case NodeType.NodeType_PrintfDecoratorCounter:
				return new DecoratorCounterNode();
			case NodeType.NodeType_PrintfCondtion:
				return new ConditionNode();
			case NodeType.NodeType_PrintfAction:
				return new ActionNode();
			}

			return null;
		}
	}

	//-------------------------------------------------------------------------
	// �ڵ��������

	public class NodeProperties
	{
		private NodeType type;

		[CategoryAttribute("ȫ������"),
		ReadOnlyAttribute(true),
		DescriptionAttribute("�ڵ�����")]
		public virtual NodeType Type
		{
			get { return type; }
			set { type = value; }
		}

		// editor
		private int id;
		private string key;
		private string desc;
		private string name;

		[CategoryAttribute("ȫ������"),
		ReadOnlyAttribute(true),
		DescriptionAttribute("ȫ�ֽڵ�ID")]
		public virtual int ID
		{
			get { return id; }
			set { id = value; }
		}

		[CategoryAttribute("ȫ������"),
		ReadOnlyAttribute(true),
		DescriptionAttribute("ȫ�ֽڵ�����")]
		public virtual string Key
		{
			get { return key; }
			set { key = value; }
		}

		[CategoryAttribute("ȫ������"),
		DescriptionAttribute("�ڵ�����")]
		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}

		[CategoryAttribute("ȫ������"),
		DescriptionAttribute("�ڵ�����")]
		public virtual string Desc
		{
			get { return desc; }
			set { desc = value; }
		}

		public virtual bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (node.editor == null)
				return false;

			type = node.type;
			id = node.editor.id;
			key = node.editor.key;
			name = node.editor.name;
			desc = node.editor.desc;

			Node.sIDMax = Math.Max(id, Node.sIDMax);

			return true;
		}

		public virtual bool SaveProtoBuf(BehaviorPB.Node node)
		{
			node.type = type;
			node.editor = new BehaviorPB.Node.Editor();
			node.editor.id = id;
			node.editor.key = key;
			node.editor.name = name;
			node.editor.desc = desc;

			return true;
		}
	}

	//-------------------------------------------------------------------------
	// �ļ�ѡ��Ի���

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

	public class Util
	{
		public static string GetKeyByValue(Dictionary<string, int> dict, int value)
		{
			foreach (KeyValuePair<string, int> item in dict)
			{
				if (item.Value == value)
					return item.Key;
			}
			return "";
		}
		public static bool IsOneChildLimit(NodeType nodeType)
		{
			switch (nodeType)
			{
			case NodeType.NodeType_Decorator:
			case NodeType.NodeType_DecoratorNot:
			case NodeType.NodeType_DecoratorLoop:
			case NodeType.NodeType_DecoratorCounter:
			case NodeType.NodeType_DecoratorTimer:
			case NodeType.NodeType_DecoratorRand:
			case NodeType.NodeType_PrintfDecoratorCounter:
				return true;	
			}
			return false;
		}

	}
}

