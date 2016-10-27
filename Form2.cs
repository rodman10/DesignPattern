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
    public partial class Form2 : Form
    {
        private MemoryManage memory;
        private int index;
        private ListView view;
        public Form2(ListView listView,MemoryManage memory,int index)
        {
            InitializeComponent();
            this.memory = memory;
            this.view = listView;
            this.index = index;
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            string _name=textBox1.Text;
            if (_name == null)
            {
                MessageBox.Show("文件名不能为空");
                return;
            }
            memory.reNameFile(textBox1.Text, index);
            view.Items[index].Text = _name;
            this.Close();
        }

    }
}
