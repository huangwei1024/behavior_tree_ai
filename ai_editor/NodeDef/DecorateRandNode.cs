using System;
using System.Collections.Generic;
using System.Text;
using BehaviorPB;
using System.ComponentModel;

namespace ai_editor.NodeDef
{
	public class DecoratorRandNode : Node
	{
		public DecoratorRandNode()
		{

		}

		public override string ClassNameCn
		{
			get { return "随机"; }
		}

		public override string ClassNameEn
		{
			get { return "DecoratorRand"; }
		}

		public override NodeType InitClassType
		{
			get { return StaticClassType; }
		}

		public static NodeType StaticClassType
		{
			get { return NodeType.NodeType_DecoratorRand; }
		}

		public override NodeProperties Props
		{
			get
			{
				if (properties == null)
					properties = new DecoratorRandNodeProperties();
				return properties;
			}
		}


		public DecoratorRandNodeProperties DerivedProps
		{
			get { return properties as DecoratorRandNodeProperties; }
		}

	}

	//-------------------------------------------------------------------------
	// DecoratorRand节点属性

	public class DecoratorRandNodeProperties : NodeProperties
	{
		private int rndIdx;
		[CategoryAttribute("随机设置"),
	   DescriptionAttribute("随机索引")]
		public virtual int RndIdx
		{
			get { return rndIdx; }
			set { rndIdx = value; }
		}

		private int rndBegin;
		[CategoryAttribute("随机设置"),
	   DescriptionAttribute("区间开始")]
		public virtual int RndBegin
		{
			get { return rndBegin; }
			set { rndBegin = value; }
		}

		private int rndEnd;
		[CategoryAttribute("随机设置"),
	  DescriptionAttribute("区间结束")]
		public virtual int RndEnd
		{
			get { return rndEnd; }
			set { rndEnd = value; }
		}

		private List<int> rndChooseArr = new List<int>();
		[CategoryAttribute("随机设置"),
	   DescriptionAttribute("命中数组。如果区间随机数命中，则执行")]
		public virtual List<int> RndChooseArr
		{
			get { return rndChooseArr; }
		}


		private string rndBBWrite = "";
		[CategoryAttribute("随机设置"),
	   DescriptionAttribute("黑板写入索引")]
		public virtual string RndBBName
		{
			get { return rndBBWrite; }
			set { rndBBWrite = value; }
		}

		public override bool LoadProtoBuf(BehaviorPB.Node node)
		{
			if (!base.LoadProtoBuf(node))
				return false;

			if (node.d_rand == null)
				return false;

			rndIdx = node.d_rand.r_idx;
			rndBegin = node.d_rand.r_begin;
			rndEnd = node.d_rand.r_end;
			rndChooseArr = node.d_rand.choose_arr;
			rndBBWrite = node.d_rand.bb_rnd;
			return true;
		}

		public override bool SaveProtoBuf(BehaviorPB.Node node)
		{
			if (!base.SaveProtoBuf(node))
				return false;

			node.d_rand = new BehaviorPB.DecoratorRand();
			node.d_rand.r_idx = rndIdx;
			node.d_rand.r_begin = rndBegin;
			node.d_rand.r_end = rndEnd;
			node.d_rand.choose_arr.Clear();
			node.d_rand.choose_arr.AddRange(rndChooseArr);
			node.d_rand.bb_rnd = rndBBWrite;
			return true;
		}
	}
}
