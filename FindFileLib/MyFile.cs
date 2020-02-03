using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public event EventHandler OnReadStart;
        public event EventHandler<ReadFileEventArgs> OnReadFinish;

        private MyFile(FileInfo file)
        {
            _file = file;
        }

        public string Name
        {
            get => _file.Name;
        }

        public void ReadFile(string keySymbols)
        {
            OnReadStart?.Invoke(this, null);

            bool matchesFound = false;

            using (StreamReader sr = new StreamReader(_file.FullName, Encoding.Default))
            {
                string s = sr.ReadToEnd().ToLower();
                string ks = keySymbols.ToLower();

                matchesFound = s.Contains(ks);
            }

            OnReadFinish?.Invoke(this, new ReadFileEventArgs(matchesFound));
        }
    }
}
