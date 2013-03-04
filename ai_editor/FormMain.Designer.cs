namespace ai_editor
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.treeView_BTree = new System.Windows.Forms.TreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.listView_Node = new System.Windows.Forms.ListView();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.tabControl_BTree = new System.Windows.Forms.TabControl();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.tabControl_BTree.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(674, 25);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 25);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
			this.splitContainer1.Size = new System.Drawing.Size(674, 351);
			this.splitContainer1.SplitterDistance = 200;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.treeView_BTree);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.listView_Node);
			this.splitContainer2.Size = new System.Drawing.Size(200, 351);
			this.splitContainer2.SplitterDistance = 179;
			this.splitContainer2.TabIndex = 0;
			// 
			// treeView_BTree
			// 
			this.treeView_BTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView_BTree.ImageIndex = 0;
			this.treeView_BTree.ImageList = this.imageList1;
			this.treeView_BTree.Location = new System.Drawing.Point(0, 0);
			this.treeView_BTree.Name = "treeView_BTree";
			this.treeView_BTree.SelectedImageIndex = 0;
			this.treeView_BTree.Size = new System.Drawing.Size(200, 179);
			this.treeView_BTree.TabIndex = 0;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "arrow_branch.png");
			this.imageList1.Images.SetKeyName(1, "arrow_divide.png");
			this.imageList1.Images.SetKeyName(2, "arrow_down.png");
			this.imageList1.Images.SetKeyName(3, "arrow_in.png");
			this.imageList1.Images.SetKeyName(4, "arrow_inout.png");
			this.imageList1.Images.SetKeyName(5, "arrow_join.png");
			this.imageList1.Images.SetKeyName(6, "arrow_left.png");
			this.imageList1.Images.SetKeyName(7, "arrow_merge.png");
			this.imageList1.Images.SetKeyName(8, "arrow_out.png");
			this.imageList1.Images.SetKeyName(9, "arrow_redo.png");
			this.imageList1.Images.SetKeyName(10, "arrow_refresh.png");
			this.imageList1.Images.SetKeyName(11, "arrow_refresh_small.png");
			this.imageList1.Images.SetKeyName(12, "arrow_right.png");
			this.imageList1.Images.SetKeyName(13, "arrow_rotate_anticlockwise.png");
			this.imageList1.Images.SetKeyName(14, "arrow_rotate_clockwise.png");
			this.imageList1.Images.SetKeyName(15, "arrow_switch.png");
			this.imageList1.Images.SetKeyName(16, "arrow_turn_left.png");
			this.imageList1.Images.SetKeyName(17, "arrow_turn_right.png");
			this.imageList1.Images.SetKeyName(18, "arrow_undo.png");
			this.imageList1.Images.SetKeyName(19, "arrow_up.png");
			this.imageList1.Images.SetKeyName(20, "asterisk_orange.png");
			this.imageList1.Images.SetKeyName(21, "asterisk_yellow.png");
			// 
			// listView_Node
			// 
			this.listView_Node.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView_Node.LargeImageList = this.imageList1;
			this.listView_Node.Location = new System.Drawing.Point(0, 0);
			this.listView_Node.Name = "listView_Node";
			this.listView_Node.Size = new System.Drawing.Size(200, 168);
			this.listView_Node.SmallImageList = this.imageList1;
			this.listView_Node.TabIndex = 0;
			this.listView_Node.UseCompatibleStateImageBehavior = false;
			this.listView_Node.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView_Node_ItemDrag);
			// 
			// splitContainer3
			// 
			this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer3.Location = new System.Drawing.Point(0, 0);
			this.splitContainer3.Name = "splitContainer3";
			// 
			// splitContainer3.Panel1
			// 
			this.splitContainer3.Panel1.Controls.Add(this.tabControl_BTree);
			this.splitContainer3.Size = new System.Drawing.Size(470, 351);
			this.splitContainer3.SplitterDistance = 300;
			this.splitContainer3.TabIndex = 0;
			// 
			// tabControl_BTree
			// 
			this.tabControl_BTree.AllowDrop = true;
			this.tabControl_BTree.Controls.Add(this.tabPage1);
			this.tabControl_BTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl_BTree.Location = new System.Drawing.Point(0, 0);
			this.tabControl_BTree.Name = "tabControl_BTree";
			this.tabControl_BTree.SelectedIndex = 0;
			this.tabControl_BTree.Size = new System.Drawing.Size(300, 351);
			this.tabControl_BTree.TabIndex = 1;
			this.tabControl_BTree.DragOver += new System.Windows.Forms.DragEventHandler(this.tabControl_BTree_DragOver);
			this.tabControl_BTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabControl_BTree_DragDrop);
			this.tabControl_BTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabControl_BTree_DragEnter);
			this.tabControl_BTree.SelectedIndexChanged += new System.EventHandler(this.tabControl_BTree_SelectedIndexChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 354);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(674, 22);
			this.statusStrip1.TabIndex = 2;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// tabPage1
			// 
			this.tabPage1.AllowDrop = true;
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(292, 325);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(674, 376);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FormMain";
			this.Text = "BT AI 编辑器";
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.Resize += new System.EventHandler(this.FormMain_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer3.Panel1.ResumeLayout(false);
			this.splitContainer3.ResumeLayout(false);
			this.tabControl_BTree.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView_BTree;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ListView listView_Node;
        private System.Windows.Forms.SplitContainer splitContainer3;
		private System.Windows.Forms.TabControl tabControl_BTree;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.TabPage tabPage1;

    }
}

