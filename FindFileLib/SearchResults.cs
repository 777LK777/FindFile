using FindFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindFileLib
{
    public class SearchResults
    {
        private List<MyFile> _files;
        private MyFolder _root;
        private TreeView _tree;

        /// <summary>
        /// Ищи!
        /// </summary>
        private bool _lookFor;
        /// <summary>
        /// Для остановки поиска
        /// </summary>
        private bool _stopFind;

        public SearchResults(string rootPath, string nameMask, EventHandler startSearch, EventHandler<ReadFileEventArgs> finishSearch)
        {
            _files = new List<MyFile>();
            _tree = new TreeView();
            _root = new MyFolder(rootPath, nameMask, ref _files, ref _tree);
            _lookFor = true;
            _stopFind = false;

            foreach(var i in _files)
            {
                i.OnReadStart += startSearch;
                i.OnReadFinish += finishSearch;
            }
        }

        public void FindMatches(string keyWord)
        {
            for (int i = 0; i < _files.Count; i++)
            {
                // Петля
                while (!_lookFor)
                {
                    int spin = 1;
                    Thread.SpinWait(spin);
                }

                if (_stopFind)
                    break;

                _files[i].ReadFile(keyWord);
            }
        }

        public IList<MyFile> Files
        {
            get => _files;
        }

        /// <summary>
        /// Приостановить или возобновить поиск
        /// </summary>
        public bool LookForNow
        {
            get => _lookFor;
            set
            {
                _lookFor = value;
            }
        }

        /// <summary>
        /// Прекратить поиск
        /// </summary>
        public bool StopFind
        {
            set
            {
                _stopFind = true;
            }
        }

        public TreeView Tree
        {
            get
            {
                return _tree;
            }
        }
    }
}
