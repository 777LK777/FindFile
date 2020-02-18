using FindFileLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace FindFile
{
    public class MyFolder
    {
        private static IList<MyFolder> FoldersHarvester(IEnumerable<DirectoryInfo> directories, string nameFileMask, ref List<MyFile> files, ref TreeView tree)
        {
            List<MyFolder> folders = new List<MyFolder>();

            for(int i = 0; i< directories.Count(); i++)
            {
                DirectoryInfo directory = directories.ElementAt(i);

                if (!directory.Attributes.HasFlag(FileAttributes.Hidden))
                    folders.Add(new MyFolder(directories.ElementAt(i), nameFileMask, ref files, ref tree));
            }

            return folders;
        }

        private readonly List<MyFolder> _folders;
        private readonly DirectoryInfo _directory;
        private readonly List<MyFile> _files;
        private readonly TreeNode _node;

        public MyFolder(string path, string nameFileMask, ref List<MyFile> files, ref TreeView tree)
        {
            _directory = new DirectoryInfo(path);
            _files = MyFile.FileHarvester(_directory.GetFiles(), nameFileMask).ToList();
            files.AddRange(_files);

            _folders = FoldersHarvester(_directory.GetDirectories(), nameFileMask, ref files, ref tree).ToList();

            tree.Nodes.Add(new TreeNode(Name,
                _files.Select(n => n.Node).
                Union(_folders.
                Select(n => n.Node)).ToArray()));
        }

        public string Name
        {
            get => _directory.Name;
        }

        public string FullName
        {
            get => _directory.FullName;
        }
        public TreeNode Node
        {
            get => _node;
        }

        private MyFolder(DirectoryInfo directory, string nameFileMask, ref List<MyFile> files, ref TreeView tree)
        {
            _directory = directory;
            _files = MyFile.FileHarvester(_directory.GetFiles(), nameFileMask).ToList();
            _folders = FoldersHarvester(_directory.GetDirectories(), nameFileMask, ref files, ref tree).ToList();
            files.AddRange(_files);

            _node = new TreeNode(
                Name,
                _files.Select(n => n.Node).
                Union(_folders.
                Select(n => n.Node)).ToArray());
        }
    }
}
