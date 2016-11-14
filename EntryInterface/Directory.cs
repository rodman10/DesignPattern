using System;
using System.Collections.Generic;
using System.Linq;
using FileSystem.UndoStructs;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileSystem.EntryInterface
{
    [Serializable]
    class Directory:Entry
    {

        public List<Entry> entries {get; }

        private void remove(ref CompoundCmd cmd, string name, int parent)
        {
            inode _node = MemoryInterface.getInstance().getInodeByIndex(parent);
            cmd.newOpe(new EditCmd(parent));
            int id = MemoryInterface.getInstance().getDataBlockByIndex(_node.getBlock(0)).removeInode(name);      //找到删除文件的inode
            _node = MemoryInterface.getInstance().getInodeByIndex(id);
            cmd.newOpe(new DeleteCmd(id));

            if (_node.getType().Equals("文件夹"))
            {
                for (int i = 0; ; i++)       //释放inodetable中信息
                {
                    string _name = MemoryInterface.getInstance().getDataBlockByIndex(_node.getBlock(0)).findInode(2);

                    if (_name != null)
                    {
                        remove(ref cmd, _name, id);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            List<int> b = _node.getBlockPtr().ToList<int>();        //获得该节点占用的全部块
            MemoryInterface.getInstance().releaseBlock(b);        //释放块位图
            MemoryInterface.getInstance().releaseInode(id);     //释放节点位图           
        }

        private string initDir(List<int> blocks, int parent, int current)
        {
            string name = "新建文件夹";
            name = MemoryInterface.getInstance().addNewInodeTableItem(MemoryInterface.getInstance().getInodeByIndex(parent).getBlock(0), name, current);

            MemoryInterface.getInstance().addNewInodeTableItem(blocks[0], "..", parent);
            MemoryInterface.getInstance().addNewInodeTableItem(blocks[0], ".", current);

            MemoryInterface.getInstance().getInodeByIndex(current).init(current, blocks, "文件夹", DateTime.Now);
            return name;
        }

        public override object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }

        public Directory(int nodeIndex,string name)
        {
            entries = new List<Entry>();
            this.name = name;
            this.node = nodeIndex;
            this.time = MemoryInterface.getInstance().getInodeByIndex(nodeIndex).getTime();
            int nodeTableBlock = MemoryInterface.getInstance().getInodeByIndex(nodeIndex).getBlock(0);
            for (int i = 2; ; i++)
            {
                string _name = MemoryInterface.getInstance().getDataBlockByIndex(nodeTableBlock).findInode(i);
                if (_name == null)
                {
                    break;
                }
                
                inode _node = MemoryInterface.getInstance().getInodeByIndex(MemoryInterface.getInstance().getInodeIndexByName(nodeIndex, _name));
           

                if (_node.getType().Equals("文件"))
                {
                    File file = new File(_node, _name);
                    entries.Add(file);                   
                }
                else
                {
                    Directory dir = new Directory(_node.id,_name);
                    entries.Add(dir);
                }            
            }
        }

        public override string getType()
        {
            return "文件夹";
        }

        public override int getSize()
        {
            return entries.Count;
        }

        public override object getContent()
        {
            return entries;
        }

        public override string createEntry(string _name ,string type)
        {
            int parent = -1;
            UndoableCmd cmd1= new EditCmd(node);
            if (_name != null)      //点击文件夹创建
            {
                parent = MemoryInterface.getInstance().getInodeIndexByName(node, _name);     //选中文件夹为父目录
            }
            else        //直接新建
            {
                parent = node;       //当前目录为父目录
            }

            List<int> nodeLoc = MemoryInterface.getInstance().getRequireInodes(1);        //找到未使用的inode节点
            List<int> blockLoc = MemoryInterface.getInstance().getRequireBlocks(1);       //找到空闲磁盘块


            if (nodeLoc == null || blockLoc == null)        //inodeMap或blockMap用尽  
            {
                return null;
            }
            string name=null;
            if (type.Equals("文件夹"))
            {
                name = initDir(blockLoc, parent, nodeLoc[0]);
                entries.Add(new Directory(nodeLoc[0], name));
            }
            else
            {
                name = "新建文件";
                name = MemoryInterface.getInstance().addNewInodeTableItem(MemoryInterface.getInstance().getInodeByIndex(parent).getBlock(0), name, nodeLoc[0]);
                inode temp = MemoryInterface.getInstance().getInodeByIndex(nodeLoc[0]);
                temp.init(nodeLoc[0], blockLoc, "文件", DateTime.Now);
                entries.Add(new File(temp, name));
            }
            UndoableCmd cmd2 = new AddCmd(nodeLoc[0]);
            CompoundCmd cmd3 = new CompoundCmd();
            cmd3.newOpe(cmd1);
            cmd3.newOpe(cmd2);
            UndoManager.getInstance().newOpe(cmd3);
            MemoryInterface.getInstance().write();
                      
            return name;
        }

        public override void reNameEntry(string newName,int _index)
        {
            inode _node = MemoryInterface.getInstance().getInodeByIndex(node);
            UndoManager.getInstance().newOpe(new EditCmd(node));
            string oldName=MemoryInterface.getInstance().getDataBlockByIndex(_node.getBlock(0)).reNameInode(newName, _index+2);      //在父目录的inodetable中进行修改
            entries[_index].name = newName;
            MemoryInterface.getInstance().write();
        }       

        public override void removeEntry(int selectedItem,string name)
        {            
            entries.RemoveAt(selectedItem);
            CompoundCmd cmd = new CompoundCmd();
            remove(ref cmd, name, node);
            UndoManager.getInstance().newOpe(cmd);
            MemoryInterface.getInstance().write();
        } 
    }
}
