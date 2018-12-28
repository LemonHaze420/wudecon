using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShenmueDKSharp;
using ShenmueDKSharp.Files.Models;

namespace model2obj
{
    class Program
    {
        static void ExportMT7(string mt7Filepath, string objFilepath)
        {
            MT7 mt7 = new MT7(mt7Filepath);
            OBJ obj = new OBJ(mt7);
            obj.Write(objFilepath);
        }
        static void ExportMT5(string mt5Filepath, string objFilepath)
        {
            MT5 mt5 = new MT5(mt5Filepath);
            OBJ obj = new OBJ(mt5);
            obj.Write(objFilepath);
        }

        static void Main(string[] args)
        {
            if (args.Count<string>() < 3 || args[0].Contains("-h") || args[0].Contains("--help") || args[0].Contains("/?"))
            {
                Console.WriteLine("Correct usage:\n\tmodel2obj [--mt5|--mt7] <source> <destination>");
                return;
            }

            if((args[0].Contains("--mt5") || args[0].Contains("-mt5")))
                ExportMT5(args[1], args[2]);
            if ((args[0].Contains("--mt7") || args[0].Contains("-mt7")))
                ExportMT7(args[1], args[2]);
        }
    }
}
