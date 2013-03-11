using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ai_editor
{
	public abstract class Node
	{
		protected AiTreeNode aiNode;
		protected NodeProperties properties;
		protected int id;

		public Node()
		{
			properties = this.DefaultProperties;
		}

		public AiTreeNode AiNode
		{
			set
			{
				aiNode = value;
			}
		}

		public NodeProperties Props
		{
			get
			{
				return properties;
			}
		}

		public abstract string ClassName { get;}
		public abstract NodeProperties DefaultProperties { get;}
		public virtual string Key
		{
			get
			{
				return string.Format("{0}{1}", ClassName, id);
			}
		}
		public virtual string DispName
		{
			set
			{
				aiNode.Text = DispName;
			}
			get
			{
				if (Props.Name == null || Props.Name.Length == 0)
					return Key;
				return string.Format("{0}:{1}", Key, Props.Name);
			}
		}

		public static Node CreateInstance(string key)
		{
			if (key == SelectorNode.Name)
				return new SelectorNode();
			else if (key == SequenceNode.Name)
				return new SequenceNode();
			else if (key == ParallelNode.Name)
				return new ParallelNode();
			else if (key == ConditionNode.Name)
				return new ConditionNode();
			else if (key == ActionNode.Name)
				return new ActionNode();
			return null;
		}

	}


	public class SelectorNode : Node
	{
		private static int nodeCnt;

		public SelectorNode()
		{
			id = ++nodeCnt;
		}

		public static string Name
		{
			get
			{
				return "Selector";
			}
		}

		public static string ChineseName
		{
			get
			{
				return "选择";
			}
		}

		public override string ClassName
		{
			get
			{
				return ChineseName;
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new NodeProperties(this);
			}
		}

	}


	public class SequenceNode : Node
	{
		private static int nodeCnt;

		public SequenceNode()
		{
			id = ++nodeCnt;
		}


		public static string Name
		{
			get
			{
				return "Sequence";
			}
		}

		public static string ChineseName
		{
			get
			{
				return "顺序";
			}
		}

		public override string ClassName
		{
			get
			{
				return ChineseName;
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new NodeProperties(this);
			}
		}

	}


	public class ParallelNode : Node
	{
		private static int nodeCnt;

		public ParallelNode()
		{
			id = ++nodeCnt;
		}

		public static string Name
		{
			get
			{
				return "Parallel";
			}
		}

		public static string ChineseName
		{
			get
			{
				return "并行";
			}
		}

		public override string ClassName
		{
			get
			{
				return ChineseName;
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new NodeProperties(this);
			}
		}

	}


	public class ConditionNode : Node
	{
		private static int nodeCnt;

		public ConditionNode()
		{
			id = ++nodeCnt;
		}

		public static string Name
		{
			get
			{
				return "Condition";
			}
		}

		public static string ChineseName
		{
			get
			{
				return "条件";
			}
		}

		public override string ClassName
		{
			get
			{
				return ChineseName;
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new ConditionProperties(this);
			}
		}

	}

	public class ActionNode : Node
	{
		private static int nodeCnt;

		public ActionNode()
		{
			id = ++nodeCnt;
		}


		public static string Name
		{
			get
			{
				return "Action";
			}
		}

		public static string ChineseName
		{
			get
			{
				return "行为";
			}
		}

		public override string ClassName
		{
			get
			{
				return ChineseName;
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new ActionProperties(this);
			}
		}

	}
}
