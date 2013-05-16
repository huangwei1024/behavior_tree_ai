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
	public class Tree
	{
		protected TreeProperties properties;

		public Tree(Node root)
		{
			DateTime T1 = new DateTime(2013, 5, 15);
			System.TimeSpan TS = new System.TimeSpan(DateTime.Now.Ticks - T1.Ticks);
			Props.Name = string.Format("Tree{0}", (int)TS.TotalSeconds);
			Props.Desc = "";
			Props.Root = root.Props;
		}

		public virtual TreeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new TreeProperties();
				return properties;
			}
		}

	}

	

	//-------------------------------------------------------------------------
	// 树基础属性

	public class TreeProperties
	{
		protected string name;
		protected string desc;
		protected NodeProperties root;

		[CategoryAttribute("树全局设置"),
		DescriptionAttribute("树名称")]
		public virtual string Name
		{
			get { return name; }
			set { name = value; }
		}

		// editor
		[CategoryAttribute("树全局设置"),
		DescriptionAttribute("树描述")]
		public virtual string Desc
		{
			get { return desc; }
			set { desc = value; }
		}

		[TypeConverterAttribute(typeof(ExpandableObjectConverter)),
		ReadOnlyAttribute(true),
		CategoryAttribute("根节点设置"),
		DescriptionAttribute("根节点")]
		public virtual NodeProperties Root
		{
			get { return root; }
			set { root = value; }
		}

		public virtual bool LoadProtoBuf(BehaviorPB.Tree tree)
		{
			if (tree.editor == null)
				return false;

			name = tree.name;
			desc = tree.editor.desc;

			return true;
		}

		public virtual bool SaveProtoBuf(BehaviorPB.Tree tree)
		{
			tree.editor = new BehaviorPB.Tree.Editor();
			tree.name = name;
			tree.editor.desc = desc;

			return true;
		}
	}
}
