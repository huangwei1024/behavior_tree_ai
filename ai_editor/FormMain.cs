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

    }
}