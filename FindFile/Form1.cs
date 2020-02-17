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

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x02000000;
                return cp;
            }
        }


        private void FindStartStopBtn_Click(object sender, EventArgs e)
        {
            if (!_searchOver && keyWordTB.Text != "" && fileNameMaskTB.Text != "" && PathTB.Text != "Выберите папку...")
            {
                TimerPaint.Start();
                _searchOver = true;
                searcMenu.Visible = true;

                FindStartStopBtn.Text = "Стоп";

                Type treeType = typeof(TreeView);

                PropertyInfo doubleBuffered = treeType.GetProperty("DoubleBuffered", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);

                doubleBuffered.SetValue(treeFolders, true);

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
                TimerPaint.Stop();
                progressSearch.Value = 0;
                _search.StopFind();
                _searchOver = false;
                searcMenu.Visible = false;
                FindStartStopBtn.Text = "Поиск";
                treeFolders = new TreeView();
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
            TimerPaint.Stop();
            SetTimerLabel("00:00:00");
            SetFileNowLabel("Расположение файла");

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
            if(_search != null)
                timerLbl.Text = _search.SpentTime.Minutes.ToString() + ":"
                + _search.SpentTime.Seconds.ToString()
                + ":" + _search.SpentTime.Milliseconds.ToString();
        }

    }






    class BufferedTreeView : TreeView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }

        // Pinvoke:
        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }
}
