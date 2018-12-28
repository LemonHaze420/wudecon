using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShenmueDKSharp;
using ShenmueDKSharp.Files.Models;
using ShenmueDKSharp.Files.Containers;

namespace model2obj
{
    class Program
    {
        static void ExportMT7(string mt7Filepath, string objFilepath, bool batch = false)
        {
            if (!batch)
            {
                MT7 mt7 = new MT7(mt7Filepath);
                OBJ obj = new OBJ(mt7);

                obj.Write(objFilepath);
            }
            else
            {
                var ext = new List<string> { ".mt7", ".MT7" };
                var myFiles = Directory.GetFiles(mt7Filepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string file in myFiles)
                {
                    string dest = file;
                    dest += ".OBJ";

                    ExportMT7(file, dest);
                }
            }
        }
        static void ExportMT5(string mt5Filepath, string objFilepath, bool batch = false)
        {
            if (!batch)
            {
                MT5 mt5 = new MT5(mt5Filepath);
                OBJ obj = new OBJ(mt5);
                obj.Write(objFilepath);
            }
            else
            {
                var ext = new List<string> { ".mt5", ".MT5" };
                var myFiles = Directory.GetFiles(mt5Filepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string file in myFiles)                {
                    string dest = file;
                    dest += ".OBJ";

                    ExportMT5(file, dest);
                }
            }
        }
        static void ExtractPKF(string pkfFilepath, string folder, bool batch = false)
        {
            PKF pkf = new PKF(pkfFilepath);
            pkf.Unpack(folder);
        }
        static void ExtractPKS(string pksFilepath, string folder, bool batch = false)
        {
            PKS pks = new PKS(pksFilepath);
            pks.Unpack(folder);
        }
        static void ExtractSPR(string sprFilepath, string folder, bool batch = false)
        {
            SPR spr = new SPR(sprFilepath);
            spr.Unpack(folder);
        }
        static void ExtractIPAC(string ipacFilepath, string folder, bool batch = false)
        {
            IPAC ipac = new IPAC(ipacFilepath);
            ipac.Unpack(folder);
        }
        static void ExtractTAC(string tadFilepath, string tacFilepath, string folder, bool batch = false)
        {
            TAD tad = new TAD();
            tad.FileName = tadFilepath;
            tad.Unpack(tacFilepath, folder);
        }
        static void ExtractGZ(string gzFilepath, string folder, bool batch = false)
        {
            GZ gz = new GZ(gzFilepath);
            gz.Unpack(folder);
        }
        static void ExtractAFS(string afsFilepath, string folder, bool batch = false)
        {
            AFS afs = new AFS(afsFilepath);
            afs.Unpack(folder);
        }

        static void Main(string[] args)
        {
            if (args.Count<string>() < 3 || args[0].Contains("-h") || args[0].Contains("--help") || args[0].Contains("/?"))
            {
                Console.WriteLine("Correct usage:\n\tmodel2obj [--mt5|--mt7] <source model> <destination obj>\n\tmodel2obj [--pkf|--pks|--spr|--ipac|--gz|--afs] <source file> <output dir>\n\tmodel2obj --tac <tad file> <tac file> <output dir>");
                return;
            }

            if ((args[0].Contains("--mt5") || args[0].Contains("-mt5")))
            {
                ExportMT5(args[1], args[2]);
            }
            else if ((args[0].Contains("--batch-mt5") || args[0].Contains("-bmt5")))
            {
                ExportMT5(args[1], args[2], true);
            }

            if ((args[0].Contains("--mt7") || args[0].Contains("-mt7")))
            {
                ExportMT7(args[1], args[2]);
            }
            else if ((args[0].Contains("--batch-mt7") || args[0].Contains("-bmt7")))
            {
                ExportMT7(args[1], args[2], true);
            }

            if ((args[0].Contains("--pkf") || args[0].Contains("-pkf")))
            {
                ExtractPKF(args[1], args[2]);
            }
            if ((args[0].Contains("--pks") || args[0].Contains("-pks")))
            {
                ExtractPKS(args[1], args[2]);
            }
            if ((args[0].Contains("--spr") || args[0].Contains("-spr")))
            {
                ExtractSPR(args[1], args[2]);
            }
            if ((args[0].Contains("--ipac") || args[0].Contains("-ipac")))
            {
                ExtractIPAC(args[1], args[2]);
            }
            if ((args[0].Contains("--gz") || args[0].Contains("-gz")))
            {
                ExtractGZ(args[1], args[2]);
            }
            if ((args[0].Contains("--afs") || args[0].Contains("-afs")))
            {
                ExtractAFS(args[1], args[2]);
            }
            if ((args[0].Contains("--tac") || args[0].Contains("-tac")))
            {
                ExtractTAC(args[1], args[2], args[3]);
            }


        }
    }
}
