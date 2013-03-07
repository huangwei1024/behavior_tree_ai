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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.treeView_BTree = new ai_editor.AiTreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.listView_Node = new System.Windows.Forms.ListView();
			this.splitContainer3 = new System.Windows.Forms.SplitContainer();
			this.tabControl_BTree = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.contextMenuStrip_Node = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.nodeConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.splitContainer3.Panel1.SuspendLayout();
			this.splitContainer3.Panel2.SuspendLayout();
			this.splitContainer3.SuspendLayout();
			this.tabControl_BTree.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.contextMenuStrip_Node.SuspendLayout();
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
			this.menuStrip1.Size = new System.Drawing.Size(827, 25);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.closeToolStripMenuItem});
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
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
			this.closeToolStripMenuItem.Text = "Close";
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
			this.splitContainer1.Size = new System.Drawing.Size(827, 449);
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
			this.splitContainer2.Size = new System.Drawing.Size(200, 449);
			this.splitContainer2.SplitterDistance = 307;
			this.splitContainer2.TabIndex = 0;
			// 
			// treeView_BTree
			// 
			this.treeView_BTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView_BTree.HideSelection = false;
			this.treeView_BTree.ImageIndex = 0;
			this.treeView_BTree.ImageList = this.imageList1;
			this.treeView_BTree.Location = new System.Drawing.Point(0, 0);
			this.treeView_BTree.Name = "treeView_BTree";
			this.treeView_BTree.SelectedImageIndex = 0;
			this.treeView_BTree.Size = new System.Drawing.Size(200, 307);
			this.treeView_BTree.TabIndex = 0;
			this.treeView_BTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_BTree_AfterSelect);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// listView_Node
			// 
			this.listView_Node.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView_Node.LargeImageList = this.imageList1;
			this.listView_Node.Location = new System.Drawing.Point(0, 0);
			this.listView_Node.Name = "listView_Node";
			this.listView_Node.Size = new System.Drawing.Size(200, 138);
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
			// 
			// splitContainer3.Panel2
			// 
			this.splitContainer3.Panel2.Controls.Add(this.propertyGrid1);
			this.splitContainer3.Size = new System.Drawing.Size(623, 449);
			this.splitContainer3.SplitterDistance = 453;
			this.splitContainer3.TabIndex = 0;
			// 
			// tabControl_BTree
			// 
			this.tabControl_BTree.Controls.Add(this.tabPage1);
			this.tabControl_BTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl_BTree.Location = new System.Drawing.Point(0, 0);
			this.tabControl_BTree.Name = "tabControl_BTree";
			this.tabControl_BTree.SelectedIndex = 0;
			this.tabControl_BTree.Size = new System.Drawing.Size(453, 449);
			this.tabControl_BTree.TabIndex = 1;
			this.tabControl_BTree.SelectedIndexChanged += new System.EventHandler(this.tabControl_BTree_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.AllowDrop = true;
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(445, 423);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "NewFile 1";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage1.Paint += new System.Windows.Forms.PaintEventHandler(this.tabPage1_Paint);
			this.tabPage1.DragOver += new System.Windows.Forms.DragEventHandler(this.tabPage1_DragOver);
			this.tabPage1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tabPage1_MouseMove);
			this.tabPage1.DragDrop += new System.Windows.Forms.DragEventHandler(this.tabPage1_DragDrop);
			this.tabPage1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabPage1_MouseClick);
			this.tabPage1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabPage1_MouseDown);
			this.tabPage1.DragLeave += new System.EventHandler(this.tabPage1_DragLeave);
			this.tabPage1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabPage1_MouseUp);
			this.tabPage1.DragEnter += new System.Windows.Forms.DragEventHandler(this.tabPage1_DragEnter);
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(166, 449);
			this.propertyGrid1.TabIndex = 0;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 452);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(827, 22);
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
			this.openFileDialog1.Filter = "BT文件|*.bt";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "BT文件|*.bt";
			// 
			// contextMenuStrip_Node
			// 
			this.contextMenuStrip_Node.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nodeConnectToolStripMenuItem});
			this.contextMenuStrip_Node.Name = "contextMenuStrip_Node";
			this.contextMenuStrip_Node.Size = new System.Drawing.Size(125, 26);
			// 
			// nodeConnectToolStripMenuItem
			// 
			this.nodeConnectToolStripMenuItem.Name = "nodeConnectToolStripMenuItem";
			this.nodeConnectToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.nodeConnectToolStripMenuItem.Text = "节点连接";
			this.nodeConnectToolStripMenuItem.Click += new System.EventHandler(this.nodeConnectToolStripMenuItem_Click);
			// 
			// FormMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(827, 474);
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
			this.splitContainer3.Panel2.ResumeLayout(false);
			this.splitContainer3.ResumeLayout(false);
			this.tabControl_BTree.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.contextMenuStrip_Node.ResumeLayout(false);
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
        private AiTreeView treeView_BTree;
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
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Node;
		private System.Windows.Forms.ToolStripMenuItem nodeConnectToolStripMenuItem;

    }
}

