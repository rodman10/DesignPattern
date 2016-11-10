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
        private IOController controller;
        private int index;
        private ListView view;
        public Form2(ListView listView,IOController controller,int index)
        {
            InitializeComponent();
            this.controller = controller;
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
            controller.reNameEntry(textBox1.Text, index);
            view.Items[index].Text = _name;
            this.Close();
        }

    }
}
