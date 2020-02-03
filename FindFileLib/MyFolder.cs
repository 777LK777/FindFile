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

        public MyFolder(string path, string nameFileMask, ref List<MyFile> files, ref TreeView tree)
        {
            _directory = new DirectoryInfo(path);
            tree.Nodes.Add(_directory.Name);
            _files = MyFile.FileHarvester(_directory.GetFiles(), nameFileMask).ToList();

            for(int i =0; i < _files.Count; i++)
            {
                tree.Nodes.Add(_files[i].Name);
            }

            _folders = FoldersHarvester(_directory.GetDirectories(), nameFileMask, ref files, ref tree).ToList();
            files.AddRange(_files);
        }

        private MyFolder(DirectoryInfo directory, string nameFileMask, ref List<MyFile> files, ref TreeView tree)
        {
            _directory = directory;
            _files = MyFile.FileHarvester(_directory.GetFiles(), nameFileMask).ToList();

            for (int i = 0; i < _files.Count; i++)
            {
                tree.Nodes.Add(_files[i].Name);
            }

            _folders = FoldersHarvester(_directory.GetDirectories(), nameFileMask, ref files, ref tree).ToList();
            files.AddRange(_files);
        }
    }
}
