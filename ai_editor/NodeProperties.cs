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

		[CategoryAttribute("全局设置"),
		ReadOnlyAttribute(true),
		DescriptionAttribute("全局节点索引")]
		public virtual string Key
		{
			get { return node.Key; }
		}

		[CategoryAttribute("全局设置"),
		DescriptionAttribute("节点描述")]
		public virtual string Desc
		{
			get { return desc; }
			set { desc = value; }
		}
	}

}
