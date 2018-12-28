using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShenmueDKSharp.Files.Models;

namespace model2obj
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count<string>() < 3 || args[0].Contains("-h") || args[0].Contains("--help") || args[0].Contains("/?"))
            {
                Console.WriteLine("Correct usage:\n\tmodel2obj [--mt5|--mt7] <source> <destination>");
                return;
            }

            bool MT5convert = false, MT7convert = false;

            string modelSource;
            string modelDestination;

            modelSource         = args[1];
            modelDestination    = args[2];
            Console.WriteLine("Source: {0}\nDestination: {1}", modelSource, modelDestination);

            if((args[0].Contains("--mt5") || args[0].Contains("-mt5")))
               MT5convert = true;
            if ((args[0].Contains("--mt7") || args[0].Contains("-mt7")))
               MT7convert = true;
            
            if(MT5convert)
            {
                MT5 tmpMT5 = new MT5(modelSource);
                OBJ tmpOBJ = new OBJ(tmpMT5);

                tmpOBJ.Write(modelDestination);
            }
            else if (MT7convert)
            {
                MT7 tmpMT7 = new MT7(modelSource);
                OBJ tmpOBJ = new OBJ(tmpMT7);

                tmpOBJ.Write(modelDestination);
            }
        }
    }
}
