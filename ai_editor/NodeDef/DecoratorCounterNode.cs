using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class DecoratorCounterNode : Node
	{
		public DecoratorCounterNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "������"; }
		}

		public override string ClassNameEn
		{
			get { return "DecoratorCounter"; }
		}

		public override int InitClassType
		{
			get { return StaticClassType; }
		}

		public static int StaticClassType
		{
			get { return (int)NodeType.NodeType_DecoratorCounter; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new DecoratorCounterNodeProperties();
				return properties;
			}
		}

		public DecoratorCounterNodeProperties DerivedProps
		{
			get { return properties as DecoratorCounterNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// DecoratorCounter�ڵ�����

	public class DecoratorCounterNodeProperties : NodeProperties
	{
		private static Dictionary<string, int> sMapDecoratorCounterName = new Dictionary<string, int>();
		private int limitCount;

		static DecoratorCounterNodeProperties()
		{
			sMapDecoratorCounterName["printf_test"] = (int)BehaviorPB.NodeType.NodeType_PrintfDecoratorCounter;
		}


		[CategoryAttribute("����������"),
		DescriptionAttribute("��������")]
		public virtual int LimitCount
		{
			get { return limitCount; }
			set { limitCount = value; }
		}

		private string decoratorCounterType;
		[CategoryAttribute("����������"),
	   DescriptionAttribute("����������"),
	   TypeConverter(typeof(DecoratorCounterNameConverter))]
		public virtual string DecoratorCounterType
		{
			get { return decoratorCounterType; }
			set
			{
				if (sMapDecoratorCounterName.ContainsKey(value))
				{
					decoratorCounterType = value;
					Type = sMapDecoratorCounterName[value];
				}
				else
				{
					decoratorCounterType = "";
					Type = DecoratorCounterNode.StaticClassType;
				}
			}
		}

		// ConditionType ���������б�
		public class DecoratorCounterNameConverter : StringConverter
		{
			public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
			{
				return true;
			}

			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				return new StandardValuesCollection(sMapDecoratorCounterName.Keys);
			}
		}  

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.d_counter == null)
				return false;

			limitCount = node.d_counter.limit_cnt;
			decoratorCounterType = Util.GetKeyByValue(sMapDecoratorCounterName, Type);
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.d_counter = new BehaviorPB.DecoratorCounter();
			node.d_counter.limit_cnt = limitCount;
			return true;
		}
	}
}
