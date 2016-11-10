using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSystem
{
    public partial class Form3 : Form
    {
        private IOController controller;
        public Form3(IOController controller)
        {
            InitializeComponent();
            this.controller = controller;
            richTextBox1.Text = controller.readFile();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!controller.saveFile(richTextBox1.Text))
            {
                MessageBox.Show("容量不足");
            }

        }
    }
}
