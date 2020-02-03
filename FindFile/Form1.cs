using FindFileLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void InFoolderBtn_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _folderPath = dialog.SelectedPath;
                }

                PathTB.Text = _folderPath;
            }
        }

        private void FindStartStopBtn_Click(object sender, EventArgs e)
        {
            if (!_searchOver)
            {
                _searchOver = true;
                FindStartStopBtn.Text = "Стоп";

                _search = new SearchResults(_folderPath, fileNameMaskTB.Text, null, (object _sender, ReadFileEventArgs _e) =>
                {
                    if (_e.MatchesFound)
                    {
                        MyFile mf = _sender as MyFile;
                        MessageBox.Show(mf.FullName);
                    }
                });

                
                
                //MessageBox.Show(treeFolders.Nodes[0].Nodes.Count.ToString());
            }
            else
            {
                _searchOver = false;
                FindStartStopBtn.Text = "Поиск";
            }
        }
    }
}
