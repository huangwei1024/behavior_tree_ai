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
		public Size size;
		public Bitmap image;
	}

	public abstract class Node
	{
		public Point pos;
		public string desc;

		protected int id;
		protected Bitmap bitmap;

		public abstract Image Image { get;}
		public abstract Point InPoint { get;}
		public abstract Point OutPoint { get;}
		public abstract Bitmap DefaultBitmap { get;}

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
		}

	}


	public class SelectorNode : Node
	{
		public static ImageInfo info;

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = "Selector";
			info.size = new Size(50, 50);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillEllipse(Brushes.LightGreen, rect);
				g.DrawEllipse(Pens.Black, rect);
				g.DrawString("选择", font, Brushes.Black, new Point(12, 18));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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
	}


	public class SequenceNode : Node
	{
		public static ImageInfo info;

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = "Sequence";
			info.size = new Size(50, 50);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillEllipse(Brushes.Green, rect);
				g.DrawEllipse(Pens.Black, rect);
				g.DrawString("顺序", font, Brushes.Black, new Point(12, 18));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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
	}


	public class ParallelNode : Node
	{
		public static ImageInfo info;

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = "Parallel";
			info.size = new Size(50, 50);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillEllipse(Brushes.LightBlue, rect);
				g.DrawEllipse(Pens.Black, rect);
				g.DrawString("并行", font, Brushes.Black, new Point(12, 18));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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
	}


	public class ConditionNode : Node
	{
		public static ImageInfo info;

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = "Condition";
			info.size = new Size(60, 30);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillRectangle(Brushes.Violet, rect);
				g.DrawRectangle(Pens.Black, rect);
				g.DrawString("条件", font, Brushes.Black, new Point(15, 5));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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
	}

	public class ActionNode : Node
	{
		public static ImageInfo info;

		public static Image NodeImage()
		{
			if (info.image != null)
				return info.image;

			info.name = "Action";
			info.size = new Size(60, 30);
			info.image = new Bitmap(info.size.Width + 2, info.size.Height + 2);

			Font font = new Font("宋体", 12, FontStyle.Regular, GraphicsUnit.Pixel);
			Graphics g = Graphics.FromImage(info.image);
			g.SmoothingMode = SmoothingMode.AntiAlias;
			{
				Rectangle rect = new Rectangle(0, 0, info.size.Width, info.size.Height);
				g.FillRectangle(Brushes.OrangeRed, rect);
				g.DrawRectangle(Pens.Black, rect);
				g.DrawString("行为", font, Brushes.Black, new Point(15, 5));
			}
			ImageInfoMap.Add(info.name, info);
			return info.image;
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
	}
}
