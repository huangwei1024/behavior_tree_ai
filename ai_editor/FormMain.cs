using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ai_editor
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			(new AboutBox()).Show();
		}

		private void tabPage1_Click(object sender, EventArgs e)
		{
		}

		private void tabPage1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
// 			Point point = new Point(0, 0);
// 			foreach (ImageInfo info in Node.ImageInfoMap.Values)
// 			{
// 				Size size = info.size;
// 				Bitmap image = info.image;
// 				g.DrawImage(image, point);
// 				//g.DrawRectangle(Pens.Red, new Rectangle(point, size));
// 				point.X += size.Width + 5;
// 			}
// 			
			if (dragEvent != null)
			{
				System.Diagnostics.Debug.Write("tabPage1_Paint dragEvent\n");

				string dummy = "temp";
				dummy = (string)dragEvent.Data.GetData(dummy.GetType());
				Point point = tabControl_BTree.PointToClient(System.Windows.Forms.Control.MousePosition);
				g.DrawImage(Node.ImageInfoMap[dummy].image, point);
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string title = "NewFile " + (tabControl_BTree.TabCount + 1).ToString();
			TabPage newPage = new TabPage(title);
			newPage.UseVisualStyleBackColor = true;
			tabControl_BTree.TabPages.Add(newPage);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			//MessageBox.Show(openFileDialog1.FileName);
			//System.Diagnostics.Trace.Assert(false, "hehe");
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			// imagelist
			this.imageList1.Images.Clear();
			this.imageList1.Images.Add(SelectorNode.NodeImage());
			this.imageList1.Images.Add(SequenceNode.NodeImage());
			this.imageList1.Images.Add(ParallelNode.NodeImage());
			this.imageList1.Images.Add(ActionNode.NodeImage());
			this.imageList1.Images.Add(ConditionNode.NodeImage());

			// listview
			this.listView_Node.Groups.Clear();
			{
				ListViewGroup groupNode = new ListViewGroup("Group_Node", "Node");
				this.listView_Node.Groups.Add(groupNode);
				this.listView_Node.Items.Add(new ListViewItem("Selector", 0, groupNode));
				this.listView_Node.Items.Add(new ListViewItem("Sequence", 1, groupNode));
				this.listView_Node.Items.Add(new ListViewItem("Parallel", 2, groupNode));
				this.listView_Node.Items.Add(new ListViewItem("Action", 3, groupNode));
				this.listView_Node.Items.Add(new ListViewItem("Condition", 4, groupNode));
			}
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
			this.tabControl_BTree.Refresh();
		}

		private void listView_Node_ItemDrag(object sender, ItemDragEventArgs e)
		{
			string strDragItem = ((ListViewItem)e.Item).Text;
			//开始进行"Drag"操作
			DoDragDrop(strDragItem, DragDropEffects.Copy | DragDropEffects.Move);
		}

		private void tabControl_BTree_DragEnter(object sender, DragEventArgs e)
		{
			//判断是否目前拖动的数据是字符串，如果是则拖动符串对目的组件进行拷贝
			dragEvent = null;
			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				e.Effect = DragDropEffects.Move;
				dragEvent = e;
			}
			else
				e.Effect = DragDropEffects.None;
		}

		private DragEventArgs dragEvent;

		private void tabControl_BTree_DragOver(object sender, DragEventArgs e)
		{
			tabControl_BTree.SelectedTab.Invalidate();
		}

		private void tabControl_BTree_DragDrop(object sender, DragEventArgs e)
		{
			dragEvent = null;
		}

		private void tabControl_BTree_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}