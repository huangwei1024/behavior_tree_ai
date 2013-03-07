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
		private AiTreeNode selectedNode;
		private AiTreeNode dragNode;
		private Point dragNodePosDist;
		private Point movePoint;
		private bool isConnectNode;
		private string dragListNodeKey;

		public FormMain()
		{
			InitializeComponent();

			isConnectNode = false;
		}

		private void RefreshUI()
		{
			tabPage1.Refresh();
			propertyGrid1.Refresh();
			treeView_BTree.Refresh();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (AboutBox about = new AboutBox())
			{
				about.ShowDialog();
			}
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

			if (selectedNode != null)
			{
				Rectangle rect = new Rectangle(selectedNode.PageNode.Pos, selectedNode.PageNode.Size);
				rect.Inflate(4, 4);
				using (Pen pen = new Pen(Color.DarkRed, 2))
				{
					g.DrawRectangle(pen, rect);
				}
			}

			treeView_BTree.PageDraw(g);

			Point point = tabPage1.PointToClient(System.Windows.Forms.Control.MousePosition);
			// draw list drag node
			if (dragListNodeKey != null)
			{
				ImageInfo info = Node.ImageInfoMap[dragListNodeKey];
				point.X -= info.size.Width / 2;
				point.Y -= info.size.Height / 2;
				g.DrawImage(info.image, point);
			}

			// draw drag node
// 			if (dragNode != null)
// 			{
// 				point.X -= dragNodePosDist.X;
// 				point.Y -= dragNodePosDist.Y;
// 				g.DrawImage(dragNode.PageNode.Image, point);
// 			}

			// draw line
			if (isConnectNode && selectedNode != null)
			{
				using (Pen pen = new Pen(Color.Black, 2))
				{
					g.DrawLine(pen, selectedNode.PageNode.OutPoint, point);
				}
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
			RefreshUI();
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


		private void tabPage1_DragEnter(object sender, DragEventArgs e)
		{
			//判断是否目前拖动的数据是字符串，如果是则拖动符串对目的组件进行拷贝
			dragListNodeKey = null;
			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				e.Effect = DragDropEffects.Move;
				string dummy = "temp";
				dragListNodeKey = (string)e.Data.GetData(dummy.GetType());
			}
			else
				e.Effect = DragDropEffects.None;
		}

		private void tabPage1_DragOver(object sender, DragEventArgs e)
		{
			if (movePoint.X == e.X && movePoint.Y == e.Y)
				return;

			RefreshUI();
			movePoint.X = e.X;
			movePoint.Y = e.Y;
		}

		private void tabPage1_DragDrop(object sender, DragEventArgs e)
		{
			Point pagePoint = tabPage1.PointToClient(new Point(e.X, e.Y));
			if (pagePoint.X < 0 || pagePoint.Y < 0 ||
				pagePoint.X > tabPage1.Size.Width || pagePoint.Y > tabPage1.Size.Height)
				return;

			// insert node
			AiTreeNode aiNode = treeView_BTree.AiNodes.AiAdd(dragListNodeKey);
			propertyGrid1.SelectedObject = aiNode.PageNode.Props;

			aiNode.PageNode.CenterPos = pagePoint;

			// refresh
			dragListNodeKey = null;
			RefreshUI();
		}

		private void treeView_BTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeView_BTree.Focused)
				selectedNode = (AiTreeNode)e.Node;
			RefreshUI();
		}

		private void tabPage1_MouseClick(object sender, MouseEventArgs e)
		{
			AiTreeNode clickNode = treeView_BTree.GetAiNodeAt(e.X, e.Y);

			if (e.Button == MouseButtons.Left)
			{
				// 左键选择
				if (isConnectNode)
				{
					isConnectNode = false;

					// 连接
					AiTreeNode selectedNode2 = treeView_BTree.GetAiNodeAt(e.X, e.Y);
					if (selectedNode == null || selectedNode2 == null)
						return;

					string path = selectedNode.AiFullPath;
					string path2 = selectedNode2.AiFullPath;
					if (path.Substring(0, Math.Min(path2.Length, path.Length)) == path2)
					{
						MessageBox.Show("循环连接！", "ERROR");
						return;
					}
					treeView_BTree.AiNodes.AiRemove(selectedNode2.Name, true);
					selectedNode.AiNodes.AiAdd(selectedNode2);
				}
				else
				{
					selectedNode = clickNode;
					treeView_BTree.SelectedNode = selectedNode;
					propertyGrid1.SelectedObject = null;
					if (clickNode == null)
					{
						RefreshUI();
						return;
					}

					propertyGrid1.SelectedObject = selectedNode.PageNode.Props;
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				// 右键选择
				if (isConnectNode)
				{
					// 取消
					isConnectNode = false;
				}
				else
				{
					selectedNode = clickNode;
					contextMenuStrip_Node.Show(tabPage1, e.X, e.Y);
				}
			}

			RefreshUI();
		}

		private void tabPage1_MouseDown(object sender, MouseEventArgs e)
		{
			dragNode = null;
			if (e.Button == MouseButtons.Left)
			{
				// 左键拖拽
				dragNode = treeView_BTree.GetAiNodeAt(e.X, e.Y);
				if (dragNode != null)
					dragNodePosDist = new Point(e.X - dragNode.PageNode.Pos.X, e.Y - dragNode.PageNode.Pos.Y);
			}
		}

		private void tabPage1_MouseUp(object sender, MouseEventArgs e)
		{
			if (dragNode != null)
			{
				if (e.X < 0 || e.Y < 0 ||
					e.X > tabPage1.Size.Width || e.Y > tabPage1.Size.Height)
				{
					treeView_BTree.AiNodes.AiRemove(dragNode.Name, true);
				}
				else
				{
					dragNodePosDist.X = e.X - dragNodePosDist.X;
					dragNodePosDist.Y = e.Y - dragNodePosDist.Y;
					dragNode.PageNode.Pos = dragNodePosDist;
				}
			}

			dragNode = null;
			isConnectNode = false;
			RefreshUI();
		}

		private void tabPage1_MouseMove(object sender, MouseEventArgs e)
		{
			if (movePoint.X == e.X && movePoint.Y == e.Y)
				return;

			movePoint.X = e.X;
			movePoint.Y = e.Y;

			if (dragNode != null)
			{
				selectedNode = null;
				Point point = movePoint;
				point.X -= dragNodePosDist.X;
				point.Y -= dragNodePosDist.Y;
				dragNode.PageNode.Pos = point;
				RefreshUI();
			}
			else if (isConnectNode)
			{
				RefreshUI();
			}
			
		}

		private void nodeConnectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (selectedNode == null)
				return;

			isConnectNode = true;
		}

		private void tabPage1_DragLeave(object sender, EventArgs e)
		{
			dragListNodeKey = null;
		}

	}


}