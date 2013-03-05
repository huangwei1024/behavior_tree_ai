using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

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
			using (AboutBox about = new AboutBox())
			{
				about.ShowDialog();
			}
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

			foreach (AiTreeNode aiNode in treeView_BTree.AiNodes)
			{
				aiNode.pageNode.Draw(g);
			}

			// draw drag node
			if (dragNode != null)
			{
				ImageInfo info = Node.ImageInfoMap[dragNode];
				Point point = tabControl_BTree.SelectedTab.PointToClient(System.Windows.Forms.Control.MousePosition);
				point.X -= info.size.Width / 2;
				point.Y -= info.size.Height / 2;
				g.DrawImage(info.image, point);
			}
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string title = "NewFile " + (tabControl_BTree.TabCount + 1).ToString();
			TabPage newPage = new TabPage(title);
			newPage.UseVisualStyleBackColor = true;
			tabControl_BTree.TabPages.Add(newPage);
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			saveFileDialog1.ShowDialog();
			MessageBox.Show(saveFileDialog1.FileName);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			MessageBox.Show(openFileDialog1.FileName);
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			this.SetStyle(ControlStyles.ResizeRedraw |
			  ControlStyles.OptimizedDoubleBuffer |
			  ControlStyles.AllPaintingInWmPaint, true);
			this.UpdateStyles();

			// imagelist
			this.imageList1.Images.Clear();
			this.imageList1.Images.Add(SelectorNode.Name, SelectorNode.NodeImage());
			this.imageList1.Images.Add(SequenceNode.Name, SequenceNode.NodeImage());
			this.imageList1.Images.Add(ParallelNode.Name, ParallelNode.NodeImage());
			this.imageList1.Images.Add(ActionNode.Name, ActionNode.NodeImage());
			this.imageList1.Images.Add(ConditionNode.Name, ConditionNode.NodeImage());

			// listview
			this.listView_Node.Groups.Clear();
			{
				ListViewGroup groupNode = new ListViewGroup("Group_Node", "Node");
				this.listView_Node.Groups.Add(groupNode);
				foreach (ImageInfo info in Node.ImageInfoMap.Values)
				{
					this.listView_Node.Items.Add(new ListViewItem(info.name, info.name, groupNode));
				}
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


		private void tabControl_BTree_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		

		private DragEventArgs dragEvent;
		private string dragNode;

		private void tabPage1_DragEnter(object sender, DragEventArgs e)
		{
			//判断是否目前拖动的数据是字符串，如果是则拖动符串对目的组件进行拷贝
			dragEvent = null;
			dragNode = null;
			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				e.Effect = DragDropEffects.Move;
				dragEvent = e;
				string dummy = "temp";
				dragNode = (string)dragEvent.Data.GetData(dummy.GetType());
			}
			else
				e.Effect = DragDropEffects.None;
		}

		private void tabPage1_DragOver(object sender, DragEventArgs e)
		{
			tabControl_BTree.SelectedTab.Refresh();
		}

		private void tabPage1_DragDrop(object sender, DragEventArgs e)
		{
			// insert node
			AiTreeNode aiNode = treeView_BTree.AiNodes.AiAdd(dragNode);
			propertyGrid1.SelectedObject = aiNode.pageNode.Props;

			aiNode.pageNode.Move(tabPage1.PointToClient(new Point(e.X, e.Y)));

			// refresh
			dragEvent = null;
			dragNode = null;
			treeView_BTree.Refresh();
			tabPage1.Refresh();
		}

	}


}