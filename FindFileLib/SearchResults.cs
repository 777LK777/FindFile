using FindFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace FindFileLib
{
    public class SearchResults
    {
        private List<MyFile> _files;
        private MyFolder _root;
        private Stopwatch _findTime;

        public event EventHandler FindComplete;

        /// <summary>
        /// Время прошедшее с начала поиска файлов
        /// </summary>
        public TimeSpan SpentTime
        {
            get
            {
                return _findTime.Elapsed;
            }
        }

        /// <summary>
        /// Ищи!
        /// </summary>
        private bool _lookFor;

        /// <summary>
        /// Для остановки поиска
        /// </summary>
        private bool _stopFind;

        public SearchResults(string rootPath, string nameMask, ref TreeView tree, EventHandler startSearch, EventHandler<ReadFileEventArgs> finishSearch)
        {
            _findTime = new Stopwatch();
            _findTime.Start();

            _files = new List<MyFile>();            
            _root = new MyFolder(rootPath, nameMask, ref _files, ref tree);
            _lookFor = true;
            _stopFind = false;
            

            foreach (var i in _files)
            {
                i.ReadStart += startSearch;
                i.ReadFinish += finishSearch;
            }
        }

        /// <summary>
        /// Найти файлы, включающие ключевое слово (фразу)
        /// </summary>
        /// <param name="keyWord">Ключевое слово(фраза)</param>
        async public void FindMatches(string keyWord)
        {            
            await Task.Run(() =>
            {
                for (int i = 0; i < _files.Count; i++)
                {
                    // Петля
                    while (!_lookFor)
                    {
                        if (_findTime.IsRunning)
                            _findTime.Stop();

                        int spin = 1;
                        Thread.SpinWait(spin);
                    }

                    if (!_findTime.IsRunning)
                        _findTime.Start();

                    if (_stopFind)
                    {
                        if (_findTime.IsRunning)
                            _findTime.Stop();
                        break;
                    }                        

                    _files[i].ReadFile(keyWord);
                }
                _findTime.Stop();
                OnFindComplete();
            });            
        }

        private void OnFindComplete()
        {
            Volatile.Read(ref FindComplete)?.Invoke(this, null);
        }

        /// <summary>
        /// Список найденных файлов
        /// </summary>
        public IList<MyFile> Files
        {
            get => _files;
        }

        /// <summary>
        /// Список прочтенных файлов
        /// </summary>
        public IList<MyFile> FinishedReadFiles
        {
            get
            {
                return _files.Where(f => f.WasRead).ToList();
            }
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

        public void StopFind()
        {
            _stopFind = true;
            _findTime.Reset();            
        }
    }
}
