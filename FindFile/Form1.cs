using FindFileLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
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

        public Form1()
        {
            InitializeComponent();

            _folderPath = "Выберите папку...";
            PathTB.Text = _folderPath;

            _searchOver = false;
        }

        

        

        private void ProgressBarUp()
        {
            if (progressSearch.InvokeRequired)
            {
                progressSearch.Invoke((Action)(() => { progressSearch.Value++; }));
            }
            else
                progressSearch.Value++;
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

                        Thread.Sleep(100);
                    },
                    (object _sender, ReadFileEventArgs _e) =>
                    {
                        MyFile mf = _sender as MyFile;

                        if (_e.MatchWasFound)
                        {
                            MessageBox.Show(mf.FullName);
                            mf.BackgroundColor = Color.Green;
                        }
                        else
                            mf.BackgroundColor = Color.Red;

                        ProgressBarUp();

                        Thread.Sleep(100);
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
                _search.StopFind();
                _searchOver = false;
                searcMenu.Visible = true;
                FindStartStopBtn.Text = "Поиск";
                fileNameMaskTB.Text = "";
                keyWordTB.Text = "";
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
            ChangeFileNowName(string.Empty);
            SearchMenuVisible(false);
            SetTextStartBtn("Поиск");
            MessageBox.Show("GOTOVO!");
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

        }
    }
}
