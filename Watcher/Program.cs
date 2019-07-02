using System;
using System.IO;

namespace Watcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string caminho = "";
            if (args.Length == 0)
            {
                caminho = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "log.txt";
                var watcher = new Watcher(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "log.txt");
            }
            else
            {
                caminho = args[0].StartsWith('.') ? Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + args[0].Substring(2, args[0].Length - 2) : args[0];
                var watcher = new Watcher(Path.GetDirectoryName(caminho), Path.GetFileName(caminho));
            }
        }
    }
}
