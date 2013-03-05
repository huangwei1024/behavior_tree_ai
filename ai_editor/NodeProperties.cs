using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace ai_editor
{
	public class NodeProperties
	{
		public NodeProperties(Node _node)
		{
			node = _node;
		}

		private Node node;
		private string desc;

		[CategoryAttribute("ȫ������"),
		ReadOnlyAttribute(true),
		DescriptionAttribute("ȫ�ֽڵ�����")]
		public virtual string Key
		{
			get { return node.Key; }
		}

		[CategoryAttribute("ȫ������"),
		DescriptionAttribute("�ڵ�����")]
		public virtual string Desc
		{
			get { return desc; }
			set { desc = value; }
		}
	}

}
