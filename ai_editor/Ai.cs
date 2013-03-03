using System;
using System.Collections.Generic;
using System.Text;

namespace ai_editor
{
	public class AiItem
	{
		public AiItem()
		{
			nodeID = lineID = 0;
			image = null;
			rect = new System.Drawing.Rectangle(0, 0, 0, 0);
		}

		// logic
		public int nodeID;
		public int lineID;

		// image
		public System.Drawing.Image image;
		public System.Drawing.Rectangle rect;
	}

	public class AiItemList
	{
		public List<AiItem> itemList;
	}
}
