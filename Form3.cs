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
        private MemoryManage memory;
        public Form3(MemoryManage memory)
        {
            InitializeComponent();
            this.memory = memory;
            richTextBox1.Text = memory.readFile();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!memory.saveFile(richTextBox1.Text))
            {
                MessageBox.Show("容量不足");
            }

        }
    }
}
