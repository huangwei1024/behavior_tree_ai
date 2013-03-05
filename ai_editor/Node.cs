using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ai_editor
{
	public struct ImageInfo
	{
		public string name;
		public string chName;
		public Size size;
		public Bitmap image;

		public delegate Node NewNodeFunc();
		public NewNodeFunc newNode;
	}

	public abstract class Node
	{
		protected Point pos;
		protected Size size;
		protected NodeProperties properties;

		protected int id;
		protected Bitmap bitmap;

		public Point Pos
		{
			get
			{
				return pos;
			}
		}
		public Size Size
		{
			get
			{
				return size;
			}
		}
		public NodeProperties Props
		{
			get
			{
				return properties;
			}
		}

		public virtual void Draw(Graphics g)
		{
			g.DrawImage(bitmap, pos);
		}
		public virtual void Move(Point point)
		{
			point.X -= size.Width / 2;
			point.Y -= size.Height / 2;
			pos = point;
		}

		public abstract string ClassName { get;}
		public abstract Image Image { get;}
		public abstract Point InPoint { get;}
		public abstract Point OutPoint { get;}
		public abstract Bitmap DefaultBitmap { get;}
		public abstract NodeProperties DefaultProperties { get;}
		public abstract ImageInfo DefaultImageInfo { get;}
		public virtual string Key
		{
			get
			{
				return string.Format("{0}_{1}", ClassName, id);
			}
		}

		protected static Dictionary<string, ImageInfo> mapImages;
		public static Dictionary<string, ImageInfo> ImageInfoMap
		{
			get
			{
				if (mapImages == null)
					mapImages = new Dictionary<string, ImageInfo>();
				return mapImages;
			}
		}

		public Node()
		{
			bitmap = this.DefaultBitmap;
			properties = this.DefaultProperties;
			pos.X = 0;
			pos.Y = 0;
			size = this.DefaultImageInfo.size;
		}

	}


	public class SelectorNode : Node
	{
		private static ImageInfo info;
		private static int nodeCnt;

		public SelectorNode()
		{
			id = ++nodeCnt;
		}

		public static Node NewNode()
		{
			return new SelectorNode();
		}

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = Name;
			info.chName = ChineseName;
			info.size = new Size(50, 50);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);
			info.newNode = new ImageInfo.NewNodeFunc(NewNode);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillEllipse(Brushes.LightGreen, rect);
				g.DrawEllipse(Pens.Black, rect);
				g.DrawString(ChineseName, font, Brushes.Black, new Point(12, 18));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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

		public override Point InPoint
		{
			get
			{
				return new Point(bitmap.Size.Width / 2, 0);
			}
		}

		public override Point OutPoint
		{
			get
			{
				return new Point(bitmap.Size.Width / 2, bitmap.Size.Height);
			}
		}

		public override Image Image
		{
			get
			{
				return bitmap;
			}
		}

		public override Bitmap DefaultBitmap
		{
			get
			{
				return new Bitmap(NodeImage());
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new NodeProperties(this);
			}
		}

		public override ImageInfo DefaultImageInfo 
		{
			get
			{
				return info;
			}
		}
	}


	public class SequenceNode : Node
	{
		private static ImageInfo info;
		private static int nodeCnt;

		public SequenceNode()
		{
			id = ++nodeCnt;
		}

		public static Node NewNode()
		{
			return new SequenceNode();
		}

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = Name;
			info.chName = ChineseName;
			info.size = new Size(50, 50);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);
			info.newNode = new ImageInfo.NewNodeFunc(NewNode);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillEllipse(Brushes.Green, rect);
				g.DrawEllipse(Pens.Black, rect);
				g.DrawString(ChineseName, font, Brushes.Black, new Point(12, 18));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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

		public override Point InPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}

		public override Point OutPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}


		public override Image Image
		{
			get
			{
				return bitmap;
			}
		}

		public override Bitmap DefaultBitmap
		{
			get
			{
				return new Bitmap(NodeImage());
			}
		}


		public override NodeProperties DefaultProperties
		{
			get
			{
				return new NodeProperties(this);
			}
		}

		public override ImageInfo DefaultImageInfo
		{
			get
			{
				return info;
			}
		}
	}


	public class ParallelNode : Node
	{
		private static ImageInfo info;
		private static int nodeCnt;

		public ParallelNode()
		{
			id = ++nodeCnt;
		}

		public static Node NewNode()
		{
			return new ParallelNode();
		}

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = Name;
			info.chName = ChineseName;
			info.size = new Size(50, 50);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);
			info.newNode = new ImageInfo.NewNodeFunc(NewNode);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillEllipse(Brushes.LightBlue, rect);
				g.DrawEllipse(Pens.Black, rect);
				g.DrawString(ChineseName, font, Brushes.Black, new Point(12, 18));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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

		public override Point InPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}

		public override Point OutPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}


		public override Image Image
		{
			get
			{
				return bitmap;
			}
		}

		public override Bitmap DefaultBitmap
		{
			get
			{
				return new Bitmap(NodeImage());
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new NodeProperties(this);
			}
		}

		public override ImageInfo DefaultImageInfo
		{
			get
			{
				return info;
			}
		}
	}


	public class ConditionNode : Node
	{
		private static ImageInfo info;
		private static int nodeCnt;

		public ConditionNode()
		{
			id = ++nodeCnt;
		}

		public static Node NewNode()
		{
			return new ConditionNode();
		}

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = Name;
			info.chName = ChineseName;
			info.size = new Size(60, 30);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);
			info.newNode = new ImageInfo.NewNodeFunc(NewNode);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillRectangle(Brushes.Violet, rect);
				g.DrawRectangle(Pens.Black, rect);
				g.DrawString(ChineseName, font, Brushes.Black, new Point(15, 5));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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

		public override Point InPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}

		public override Point OutPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}


		public override Image Image
		{
			get
			{
				return bitmap;
			}
		}

		public override Bitmap DefaultBitmap
		{
			get
			{
				return new Bitmap(NodeImage());
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new NodeProperties(this);
			}
		}

		public override ImageInfo DefaultImageInfo
		{
			get
			{
				return info;
			}
		}
	}

	public class ActionNode : Node
	{
		private static ImageInfo info;
		private static int nodeCnt;

		public ActionNode()
		{
			id = ++nodeCnt;
		}

		public static Node NewNode()
		{
			return new ActionNode();
		}

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = Name;
			info.chName = ChineseName;
			info.size = new Size(60, 30);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);
			info.newNode = new ImageInfo.NewNodeFunc(NewNode);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillRectangle(Brushes.OrangeRed, rect);
				g.DrawRectangle(Pens.Black, rect);
				g.DrawString(ChineseName, font, Brushes.Black, new Point(15, 5));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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

		public override Point InPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}

		public override Point OutPoint
		{
			get
			{
				return new Point(0, 0);
			}
		}


		public override Image Image
		{
			get
			{
				return bitmap;
			}
		}

		public override Bitmap DefaultBitmap
		{
			get
			{
				return new Bitmap(NodeImage());
			}
		}

		public override NodeProperties DefaultProperties
		{
			get
			{
				return new NodeProperties(this);
			}
		}

		public override ImageInfo DefaultImageInfo
		{
			get
			{
				return info;
			}
		}
	}
}
