using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileSystem
{
    public partial class Form1 : Form
    {
        IOController controller;
        public Form1()
        {
            InitializeComponent();
            controller = new IOController(catalog);
            ImageList myimagelist = new ImageList();
            myimagelist.Images.Add(Image.FromFile("dir.png"));
            myimagelist.Images.Add(Image.FromFile("file.png"));
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
           
            if (controller.reDirectCatalog(selectIndex))
            {
                Form3 notePad = new Form3(controller);
                notePad.Show();
            }
        }

        private void noItemCreateFile_Click(object sender, EventArgs e)
        {
            if (!controller.createEntry("文件", null))
            {
                MessageBox.Show("inode不足无法创建");
            }
        }

        private void rename_Click(object sender, EventArgs e)
        {
            
            int selectIndex = catalog.FocusedItem.Index;
            Form2 dialig = new Form2(catalog,controller,selectIndex);
            dialig.Show();
        }

        private void remove_Click(object sender, EventArgs e)
        {
            int selectIndex = catalog.FocusedItem.Index;
            controller.removeEntry(catalog.Items[selectIndex].Text,selectIndex);
        }

        private void noItemCreateDir_Click(object sender, EventArgs e)
        {
            if (!controller.createEntry("文件夹", null))
            {
                MessageBox.Show("inode不足无法创建");
            }
        }

        private void hasItemCreateFile_Click(object sender, EventArgs e)
        {
            int selectIndex = catalog.FocusedItem.Index;
            if (!controller.createEntry("文件", catalog.Items[selectIndex].Text))
            {
                MessageBox.Show("inode不足无法创建");
            }
        }

        private void hasItemCreateDir_Click(object sender, EventArgs e)
        {
            int selectIndex = catalog.FocusedItem.Index;
            if (!controller.createEntry("文件夹", catalog.Items[selectIndex].Text))
            {
                MessageBox.Show("inode不足无法创建");
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            controller.back();
        }

        private void forward_Click(object sender, EventArgs e)
        {
            controller.forward();
        }

        

        private void undo_Click(object sender, EventArgs e)
        {

        }

        private void redo_Click(object sender, EventArgs e)
        {

        }
    }
}
