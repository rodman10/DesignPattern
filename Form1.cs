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
    public partial class Form1 : Form
    {
        MemoryManage memory;
        public Form1()
        {
            InitializeComponent();
            memory = new MemoryManage(catalog);
            ImageList myimagelist = new ImageList();
            myimagelist.Images.Add(Image.FromFile("./dir.png"));
            myimagelist.Images.Add(Image.FromFile("./file.png"));
            catalog.SmallImageList = myimagelist;
            catalog.View = View.Details;
            catalog.Columns.Add("文件名");
            catalog.Columns.Add("类型");
            catalog.Columns.Add("大小");
            catalog.Columns.Add("修改时间");
            
        }

        private void catalog_MouseDown(object sender, MouseEventArgs e)
        {
            Point clickPoint = new Point(e.X, e.Y);
            if (catalog.GetItemAt(e.X, e.Y) == null)
            {
                if (e.Button == MouseButtons.Right)
                {
                    noItem.Show(MousePosition);
                }
                return;
            }
            ListViewItem currentItem = catalog.GetItemAt(e.X, e.Y);
            catalog.FocusedItem = currentItem;
            int selectIndex = catalog.FocusedItem.Index;
            if (e.Button == MouseButtons.Right)
            {
                if (catalog.Items[selectIndex].ImageIndex == 1)
                {
                    hasItem.Items[2].Enabled = false;
                }
                else
                {
                    hasItem.Items[2].Enabled = true;
                }
                hasItem.Show(MousePosition);
            }
        }

        private void catalog_DoubleClick(object sender, EventArgs e)
        {
            int selectIndex = catalog.FocusedItem.Index;
            if (memory.reDirectCatalog(catalog.Items[selectIndex].Text))
            {
                Form3 notePad = new Form3(memory);
                notePad.Show();
            }
        }

        private void noItemCreateFile_Click(object sender, EventArgs e)
        {
            if (!memory.createFile("文件", null))
            {
                MessageBox.Show("inode不足无法创建");
            }
        }

        private void rename_Click(object sender, EventArgs e)
        {
            
            int selectIndex = catalog.FocusedItem.Index;
            Form2 dialig = new Form2(catalog,memory,selectIndex);
            dialig.Show();
        }

        private void remove_Click(object sender, EventArgs e)
        {
            int selectIndex = catalog.FocusedItem.Index;
            memory.removeFile(catalog.Items[selectIndex].Text,null,selectIndex);
        }

        private void noItemCreateDir_Click(object sender, EventArgs e)
        {
            if (!memory.createFile("文件夹", null))
            {
                MessageBox.Show("inode不足无法创建");
            }
        }

        private void hasItemCreateFile_Click(object sender, EventArgs e)
        {
            int selectIndex = catalog.FocusedItem.Index;
            if (!memory.createFile("文件", catalog.Items[selectIndex].Text))
            {
                MessageBox.Show("inode不足无法创建");
            }
        }

        private void hasItemCreateDir_Click(object sender, EventArgs e)
        {
            int selectIndex = catalog.FocusedItem.Index;
            if (!memory.createFile("文件夹", catalog.Items[selectIndex].Text))
            {
                MessageBox.Show("inode不足无法创建");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            memory.back();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            memory.forward();
        }

      
       


    }
}
