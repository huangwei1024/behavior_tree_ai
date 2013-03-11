﻿namespace ai_editor
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
			this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.treeView_BTree = new ai_editor.AiTreeView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.contextMenuStrip_Node = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.新建节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.sequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.parallelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.conditionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.actionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.删除节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip_Tree = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.新建根节点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.contextMenuStrip_Node.SuspendLayout();
			this.contextMenuStrip_Tree.SuspendLayout();
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
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.newToolStripMenuItem.Text = "New";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveToolStripMenuItem
			// 
			this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
			this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveToolStripMenuItem.Text = "Save";
			this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer1.Location = new System.Drawing.Point(0, 25);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView_BTree);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
			this.splitContainer1.Size = new System.Drawing.Size(827, 449);
			this.splitContainer1.SplitterDistance = 558;
			this.splitContainer1.TabIndex = 1;
			// 
			// treeView_BTree
			// 
			this.treeView_BTree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView_BTree.HideSelection = false;
			this.treeView_BTree.ImageKey = "Action.png";
			this.treeView_BTree.ImageList = this.imageList1;
			this.treeView_BTree.Indent = 20;
			this.treeView_BTree.Location = new System.Drawing.Point(0, 0);
			this.treeView_BTree.Name = "treeView_BTree";
			this.treeView_BTree.SelectedImageIndex = 0;
			this.treeView_BTree.ShowNodeToolTips = true;
			this.treeView_BTree.Size = new System.Drawing.Size(558, 449);
			this.treeView_BTree.TabIndex = 0;
			this.treeView_BTree.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView_BTree_MouseClick);
			this.treeView_BTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_BTree_AfterSelect);
			this.treeView_BTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_BTree_MouseDown);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			this.imageList1.Images.SetKeyName(0, "Action12");
			this.imageList1.Images.SetKeyName(1, "Condition12");
			this.imageList1.Images.SetKeyName(2, "pointer_down.png");
			this.imageList1.Images.SetKeyName(3, "randomselector.png");
			this.imageList1.Images.SetKeyName(4, "Selector12");
			this.imageList1.Images.SetKeyName(5, "Sequence12");
			this.imageList1.Images.SetKeyName(6, "Action");
			this.imageList1.Images.SetKeyName(7, "Sequence");
			this.imageList1.Images.SetKeyName(8, "Selector");
			this.imageList1.Images.SetKeyName(9, "Condition");
			this.imageList1.Images.SetKeyName(10, "Parallel");
			// 
			// propertyGrid1
			// 
			this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new System.Drawing.Size(265, 449);
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
            this.新建节点ToolStripMenuItem,
            this.删除节点ToolStripMenuItem});
			this.contextMenuStrip_Node.Name = "contextMenuStrip_Node";
			this.contextMenuStrip_Node.Size = new System.Drawing.Size(125, 48);
			// 
			// 新建节点ToolStripMenuItem
			// 
			this.新建节点ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectorToolStripMenuItem,
            this.sequenceToolStripMenuItem,
            this.parallelToolStripMenuItem,
            this.conditionToolStripMenuItem,
            this.actionToolStripMenuItem});
			this.新建节点ToolStripMenuItem.Name = "新建节点ToolStripMenuItem";
			this.新建节点ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.新建节点ToolStripMenuItem.Text = "新建节点";
			// 
			// selectorToolStripMenuItem
			// 
			this.selectorToolStripMenuItem.Name = "selectorToolStripMenuItem";
			this.selectorToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.selectorToolStripMenuItem.Text = "Selector";
			this.selectorToolStripMenuItem.Click += new System.EventHandler(this.selectorToolStripMenuItem_Click);
			// 
			// sequenceToolStripMenuItem
			// 
			this.sequenceToolStripMenuItem.Name = "sequenceToolStripMenuItem";
			this.sequenceToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.sequenceToolStripMenuItem.Text = "Sequence";
			this.sequenceToolStripMenuItem.Click += new System.EventHandler(this.sequenceToolStripMenuItem_Click);
			// 
			// parallelToolStripMenuItem
			// 
			this.parallelToolStripMenuItem.Name = "parallelToolStripMenuItem";
			this.parallelToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.parallelToolStripMenuItem.Text = "Parallel";
			this.parallelToolStripMenuItem.Click += new System.EventHandler(this.parallelToolStripMenuItem_Click);
			// 
			// conditionToolStripMenuItem
			// 
			this.conditionToolStripMenuItem.Name = "conditionToolStripMenuItem";
			this.conditionToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.conditionToolStripMenuItem.Text = "Condition";
			this.conditionToolStripMenuItem.Click += new System.EventHandler(this.conditionToolStripMenuItem_Click);
			// 
			// actionToolStripMenuItem
			// 
			this.actionToolStripMenuItem.Name = "actionToolStripMenuItem";
			this.actionToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
			this.actionToolStripMenuItem.Text = "Action";
			this.actionToolStripMenuItem.Click += new System.EventHandler(this.actionToolStripMenuItem_Click);
			// 
			// 删除节点ToolStripMenuItem
			// 
			this.删除节点ToolStripMenuItem.Name = "删除节点ToolStripMenuItem";
			this.删除节点ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.删除节点ToolStripMenuItem.Text = "删除节点";
			// 
			// contextMenuStrip_Tree
			// 
			this.contextMenuStrip_Tree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建根节点ToolStripMenuItem});
			this.contextMenuStrip_Tree.Name = "contextMenuStrip_Tree";
			this.contextMenuStrip_Tree.Size = new System.Drawing.Size(137, 26);
			// 
			// 新建根节点ToolStripMenuItem
			// 
			this.新建根节点ToolStripMenuItem.Name = "新建根节点ToolStripMenuItem";
			this.新建根节点ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.新建根节点ToolStripMenuItem.Text = "新建根节点";
			this.新建根节点ToolStripMenuItem.Click += new System.EventHandler(this.新建根节点ToolStripMenuItem_Click);
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveAsToolStripMenuItem.Text = "SaveAs";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
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
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.contextMenuStrip_Node.ResumeLayout(false);
			this.contextMenuStrip_Tree.ResumeLayout(false);
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
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Node;
		private System.Windows.Forms.PropertyGrid propertyGrid1;
		private System.Windows.Forms.ToolStripMenuItem 新建节点ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem sequenceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem parallelToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem conditionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem actionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 删除节点ToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Tree;
		private System.Windows.Forms.ToolStripMenuItem 新建根节点ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;

    }
}

