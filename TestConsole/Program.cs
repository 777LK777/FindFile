using FindFileLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestConsole
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //string s = string.Empty;           

            //using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            //{
            //    DialogResult res = dialog.ShowDialog();

            //    if (res == DialogResult.OK)
            //    {
            //        s = dialog.SelectedPath;
            //    }
            //}

            Stopwatch sw = new Stopwatch();

            sw.Start();
            Thread.Sleep(5000);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine();

            sw.Start();
            Thread.Sleep(10000);
            sw.Stop();

            

            Console.WriteLine(sw.Elapsed);

            TimeSpan ts = sw.Elapsed;
            Console.WriteLine(ts.Seconds);






            Console.ReadKey();
        }


        static public void SetColour(object sender, ReadFileEventArgs e)
        {
            var file = sender as MyFile;
            if(e.MatchWasFound)
                Console.WriteLine($"Найдено совпадение - {file.Name}");
        }
        
    }
}
