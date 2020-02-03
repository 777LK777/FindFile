﻿using FindFileLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            string s = string.Empty;

            

            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                DialogResult res = dialog.ShowDialog();

                if (res == DialogResult.OK)
                {
                    s = dialog.SelectedPath;
                }
            }

            var search = new SearchResults(s, ".txt", null, SetColour);            

            search.FindMatches(@"ваиваи");

            for (int i = 0; i < search.Tree.Nodes[0].Nodes.Count; i++)
            {
                Console.WriteLine(search.Tree.Nodes[0].Nodes[i].Name);
            }

            Console.WriteLine($"\nCount: {search.Files.Count}");

            Console.WriteLine(search.Tree.Nodes.Count);


            Console.ReadKey();
        }


        static public void SetColour(object sender, ReadFileEventArgs e)
        {
            var file = sender as MyFile;
            if(e.MatchesFound)
                Console.WriteLine($"Найдено совпадение - {file.Name}");
        }
        
    }
}
