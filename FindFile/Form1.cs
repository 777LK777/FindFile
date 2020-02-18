using FindFileLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindFile
{
    public partial class Form1 : Form
    {
        private string _folderPath;
        private bool _searchOver;
        private SearchResults _search;
        private const string PATH_SAVE_DATA = "\\save.txt";

        public Form1() : base()
        {
            InitializeComponent();
            DoubleBuffered = true;

            InputData data = LoadDataManager.LoadData<InputData>(PATH_SAVE_DATA);
            if(data != null)
            {
                PathTB.Text = data.Path;
                _folderPath = data.Path;
                fileNameMaskTB.Text = data.FileName;
                keyWordTB.Text = data.KeyWord;
            }
            else
            {
                _folderPath = "Выберите папку...";
                PathTB.Text = _folderPath;
            }

            TimerPaint.Interval = 100;            

            _searchOver = false;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(PathTB.Text != null && fileNameMaskTB.Text != null && keyWordTB.Text != null)
                LoadDataManager.UploadData(new InputData(PathTB.Text, fileNameMaskTB.Text, keyWordTB.Text), PATH_SAVE_DATA);
        }





        private void SetProgressBar()
        {
            if (progressSearch.InvokeRequired)
            {
                progressSearch.Invoke((Action)(() =>
                {
                    if(progressSearch.Value < progressSearch.Maximum)
                        progressSearch.Value++;
                }));
            }
            else
                progressSearch.Value++;
        }
        private void SetProgressBar(int value)
        {
            if (progressSearch.InvokeRequired)
            {
                progressSearch.Invoke((Action)(() =>
                {
                    if (value < progressSearch.Maximum)
                        progressSearch.Value = value;
                }));
            }
            else
            {
                if (value < progressSearch.Maximum)
                    progressSearch.Value = value;
            }
        }

        private void ChangeFileNowName(string fullName)
        {
            if (fileNowLBL.InvokeRequired)
            {
                fileNowLBL.Invoke((Action)(() => fileNowLBL.Text = fullName));
            }
            else
                fileNowLBL.Text = fullName;
        }        

        private void SetTextStartBtn(string text)
        {
            if (FindStartStopBtn.InvokeRequired)
            {
                FindStartStopBtn.Invoke((Action)(() => FindStartStopBtn.Text = text));
            }
            else
                FindStartStopBtn.Text = text;
        }

        private void SearchMenuVisible(bool isVisible)
        {
            if (searcMenu.InvokeRequired)
            {
                searcMenu.Invoke((Action)(() => searcMenu.Visible = isVisible));
            }
            else
                searcMenu.Visible = isVisible;
        }

        private void SetFilesCounterLabel(string text)
        {
            if (filesCounterLbl.InvokeRequired)
            {
                filesCounterLbl.Invoke((Action)(() => filesCounterLbl.Text = text));
            }
            else
                filesCounterLbl.Text = text;
        }

        private void SetTimerLabel(string text)
        {
            if (timerLbl.InvokeRequired)
            {
                timerLbl.Invoke((Action)(() => timerLbl.Text = text));
            }
            else
                timerLbl.Text = text;
        }

        private void SetFileNowLabel(string text)
        {
            if (fileNowLBL.InvokeRequired)
            {
                fileNowLBL.Invoke((Action)(() => fileNowLBL.Text = text));
            }
            else
                fileNowLBL.Text = text;
        }




        private string FilesCount()
        {
            string allFiles = _search.Files.Count.ToString();
            string slash = "/";
            string checkedFiles = _search.FinishedReadFiles.Count.ToString();
            return checkedFiles + slash + allFiles;
        }

        private string GetNormalFileName(string fullPath)
        {
            if (fullPath.Length > 50)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append(fullPath.Substring(0, fullPath.IndexOf('\\')));
                sb.Append(" ... ");
                sb.Append(fullPath.Substring(fullPath.LastIndexOf('\\')));

                return sb.ToString();
            }
            else
                return fullPath;
        }

        private void FindStartStopBtn_Click(object sender, EventArgs e)
        {
            if (!_searchOver && keyWordTB.Text != "" && fileNameMaskTB.Text != "" && PathTB.Text != "Выберите папку...")
            {
                if (_search != null)
                {
                    treeFolders.Nodes.Clear();
                }

                TimerPaint.Start();
                _searchOver = true;
                searcMenu.Visible = true;

                FindStartStopBtn.Text = "Стоп";            

                _search = new SearchResults(_folderPath, fileNameMaskTB.Text, ref treeFolders,
                    (object _sender, EventArgs _e) =>
                    {
                        MyFile mf = _sender as MyFile;

                        SetFilesCounterLabel(FilesCount());                                       
                        mf.BackgroundColor = Color.Yellow;

                        ChangeFileNowName(GetNormalFileName(mf.FullName));

                        Thread.Sleep(50);
                    },
                    (object _sender, ReadFileEventArgs _e) =>
                    {
                        MyFile mf = _sender as MyFile;

                        if (_e.MatchWasFound)
                        {
                            Task.Run(() => 
                            {
                                MessageBox.Show(mf.FullName, "Найдено совпадение!", MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                            });                           

                            mf.BackgroundColor = Color.Green;
                        }
                        else
                            mf.BackgroundColor = Color.Red;

                        SetProgressBar();

                        Thread.Sleep(50);
                    });

                progressSearch.Maximum = _search.Files.Count;

                _search.FindComplete += OnFindComplete;

                _search.FindMatches(keyWordTB.Text.ToLower());
            }
            else if (keyWordTB.Text == "" || fileNameMaskTB.Text == "")
            {
                MessageBox.Show("Необходимо ввести имя файла и ключевое слово!!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (PathTB.Text == "Выберите папку...")
            {
                MessageBox.Show("Необходимо выбрать папку!!!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                TimerPaint.Stop();
                progressSearch.Value = 0;
                _search.StopFind();
                _searchOver = false;
                searcMenu.Visible = false;
                FindStartStopBtn.Text = "Поиск";
            }
        }

        private void PauseFindBtn_Click(object sender, EventArgs e)
        {
            if (_search.LookForNow)
            {
                _search.LookForNow = false;
                PauseFindBtn.Text = "Продолжить";
            }
            else
            {
                _search.LookForNow = true;
                PauseFindBtn.Text = "Пауза";
            }
                
        }

        private void OnFindComplete(object sender, EventArgs e)
        {
            _searchOver = false;
            SetProgressBar(0);
            ChangeFileNowName(string.Empty);
            SearchMenuVisible(false);
            TimerPaint.Stop();
            SetTimerLabel("00:00:00");
            SetFileNowLabel("Расположение файла");

            SetTextStartBtn("Поиск");
            if (_search.Files.Where(f => f.IsMatch).Count() == 0)
            {
                MessageBox.Show("В файлах совпадений не найдено", "Поиск закончен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show($"Найдено совпадений в файлах: {_search.Files.Where(f => f.IsMatch).Count()}", "Поиск закончен", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InFoolderBtn_Click(object sender, EventArgs e)
        {
            if (_searchOver == false)
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        _folderPath = dialog.SelectedPath;
                    }

                    PathTB.Text = _folderPath;
                }
            }
            else
                SystemSounds.Beep.Play();
        }

        private void timerPaint_Tick(object sender, EventArgs e)
        {
            if(_search != null)
                timerLbl.Text = _search.SpentTime.Minutes.ToString() + ":"
                + _search.SpentTime.Seconds.ToString()
                + ":" + _search.SpentTime.Milliseconds.ToString();
        }
    }
}
