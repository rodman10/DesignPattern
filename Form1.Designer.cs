namespace FileSystem
{
    partial class Form1
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
            this.catalog = new System.Windows.Forms.ListView();
            this.noItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noItemCreateDir = new System.Windows.Forms.ToolStripMenuItem();
            this.noItemCreateFile = new System.Windows.Forms.ToolStripMenuItem();
            this.hasItem = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rename = new System.Windows.Forms.ToolStripMenuItem();
            this.remove = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.新建ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hasItemCreateFile = new System.Windows.Forms.ToolStripMenuItem();
            this.hasItemCreateDir = new System.Windows.Forms.ToolStripMenuItem();
            this.noItem.SuspendLayout();
            this.hasItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // catalog
            // 
            this.catalog.Location = new System.Drawing.Point(-2, 39);
            this.catalog.Name = "catalog";
            this.catalog.Size = new System.Drawing.Size(823, 491);
            this.catalog.TabIndex = 0;
            this.catalog.UseCompatibleStateImageBehavior = false;
            this.catalog.DoubleClick += new System.EventHandler(this.catalog_DoubleClick);
            this.catalog.MouseDown += new System.Windows.Forms.MouseEventHandler(this.catalog_MouseDown);
            // 
            // noItem
            // 
            this.noItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem});
            this.noItem.Name = "noItem";
            this.noItem.Size = new System.Drawing.Size(101, 26);
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noItemCreateDir,
            this.noItemCreateFile});
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            // 
            // noItemCreateDir
            // 
            this.noItemCreateDir.Name = "noItemCreateDir";
            this.noItemCreateDir.Size = new System.Drawing.Size(136, 22);
            this.noItemCreateDir.Text = "新建文件夹";
            this.noItemCreateDir.Click += new System.EventHandler(this.noItemCreateDir_Click);
            // 
            // noItemCreateFile
            // 
            this.noItemCreateFile.Name = "noItemCreateFile";
            this.noItemCreateFile.Size = new System.Drawing.Size(136, 22);
            this.noItemCreateFile.Text = "新建文件";
            this.noItemCreateFile.Click += new System.EventHandler(this.noItemCreateFile_Click);
            // 
            // hasItem
            // 
            this.hasItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rename,
            this.remove,
            this.新建ToolStripMenuItem1});
            this.hasItem.Name = "hasItem";
            this.hasItem.Size = new System.Drawing.Size(113, 70);
            // 
            // rename
            // 
            this.rename.Name = "rename";
            this.rename.Size = new System.Drawing.Size(152, 22);
            this.rename.Text = "重命名";
            this.rename.Click += new System.EventHandler(this.rename_Click);
            // 
            // remove
            // 
            this.remove.Name = "remove";
            this.remove.Size = new System.Drawing.Size(152, 22);
            this.remove.Text = "删除";
            this.remove.Click += new System.EventHandler(this.remove_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "←";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(111, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "→";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // 新建ToolStripMenuItem1
            // 
            this.新建ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hasItemCreateFile,
            this.hasItemCreateDir});
            this.新建ToolStripMenuItem1.Name = "新建ToolStripMenuItem1";
            this.新建ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.新建ToolStripMenuItem1.Text = "新建";
            // 
            // hasItemCreateFile
            // 
            this.hasItemCreateFile.Name = "hasItemCreateFile";
            this.hasItemCreateFile.Size = new System.Drawing.Size(152, 22);
            this.hasItemCreateFile.Text = "新建文件";
            this.hasItemCreateFile.Click += new System.EventHandler(this.hasItemCreateFile_Click);
            // 
            // hasItemCreateDir
            // 
            this.hasItemCreateDir.Name = "hasItemCreateDir";
            this.hasItemCreateDir.Size = new System.Drawing.Size(152, 22);
            this.hasItemCreateDir.Text = "新建文件夹";
            this.hasItemCreateDir.Click += new System.EventHandler(this.hasItemCreateDir_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 527);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.catalog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.noItem.ResumeLayout(false);
            this.hasItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView catalog;
        private System.Windows.Forms.ContextMenuStrip noItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noItemCreateDir;
        private System.Windows.Forms.ToolStripMenuItem noItemCreateFile;
        private System.Windows.Forms.ContextMenuStrip hasItem;
        private System.Windows.Forms.ToolStripMenuItem rename;
        private System.Windows.Forms.ToolStripMenuItem remove;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hasItemCreateFile;
        private System.Windows.Forms.ToolStripMenuItem hasItemCreateDir;

    }
}

