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

        public Form1()
        {
            InitializeComponent();

            _folderPath = "Выберите папку...";
            PathTB.Text = _folderPath;

            _searchOver = false;

            //treeFolders;
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
            }
            else
            {
                _searchOver = false;
                FindStartStopBtn.Text = "Поиск";
            }
        }
    }
}
