using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace FindFileLib
{
    public class MyFile
    {
        public static IList<MyFile> FileHarvester(IEnumerable<FileInfo> files, string nameFileMask)
        {
            List<MyFile> _files = new List<MyFile>();

            IEnumerable<FileInfo> ff = files.Where(f => f.Name.Contains(nameFileMask));

            for(int i = 0; i < ff.Count(); i++)
            {
                _files.Add(new MyFile(ff.ElementAt(i)));
            }

            return _files;
        }

        private readonly FileInfo _file;
        private readonly TreeNode _node;
        private bool _wasRead;

        public event EventHandler ReadStart;
        public event EventHandler<ReadFileEventArgs> ReadFinish;

        private MyFile(FileInfo file)
        {
            _file = file;
            _node = new TreeNode(_file.Name);
            _wasRead = false;
        }

        public string Name
        {
            get => _file.Name;
        }

        public string FullName
        {
            get => _file.FullName;
        }

        public TreeNode Node
        {
            get => _node;
        }

        public Color BackgroundColor
        {
            get => _node.BackColor;
            set => _node.BackColor = value;
        }

        public bool WasRead
        {
            get
            {
                return _wasRead;
            }
        }

        void OnReadStart()
        {
            Volatile.Read(ref ReadStart)?.Invoke(this, null);
        }

        void OnReadFinish(ReadFileEventArgs e)
        {
            Volatile.Read(ref ReadFinish)?.Invoke(this, e);
        }

        public void ReadFile(string keySymbols)
        {
            OnReadStart();

            bool matchesFound = false;

            using (StreamReader sr = new StreamReader(_file.FullName, Encoding.Default))
            {
                string s = sr.ReadToEnd().ToLower();
                string ks = keySymbols.ToLower();

                matchesFound = s.Contains(ks);
                _wasRead = true;
            }

            OnReadFinish(new ReadFileEventArgs(matchesFound));
        }
    }
}
