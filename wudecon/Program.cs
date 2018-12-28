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

namespace wudecon
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
            if (!batch)
            {
                PKF pkf = new PKF(pkfFilepath);
                pkf.Unpack(folder);
            }
            else
            {
                var ext = new List<string> { ".pkf", ".PKF" };
                var myFiles = Directory.GetFiles(pkfFilepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string file in myFiles)
                {
                    PKF pkf = new PKF(file);
                    pkf.Unpack(folder);
                }
            }
        }
        static void ExtractPKS(string pksFilepath, string folder, bool batch = false)
        {
            if (!batch)
            {
                PKS pks = new PKS(pksFilepath);
                pks.Unpack(folder);
            }
            else
            {
                var ext = new List<string> { ".pks", ".PKS" };
                var myFiles = Directory.GetFiles(pksFilepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string file in myFiles)
                {
                    PKS pks = new PKS(file);
                    pks.Unpack(folder);
                }
            }
        }
        static void ExtractSPR(string sprFilepath, string folder, bool batch = false)
        {
            if (!batch)
            {
                SPR spr = new SPR(sprFilepath);
                spr.Unpack(folder);
            }
            else
            {
                var ext = new List<string> { ".spr", ".SPR" };
                var myFiles = Directory.GetFiles(sprFilepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string file in myFiles)
                {
                    SPR spr = new SPR(file);
                    spr.Unpack(folder);
                }
            }
        }
        static void ExtractIPAC(string ipacFilepath, string folder, bool batch = false)
        {
            if (!batch)
            {
                IPAC ipac = new IPAC(ipacFilepath);
                ipac.Unpack(folder);
            }
            else
            {
                var ext = new List<string> { ".ipac", ".IPAC" };
                var myFiles = Directory.GetFiles(ipacFilepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string file in myFiles)
                {
                    IPAC ipac = new IPAC(file);
                    ipac.Unpack(folder);
                }
            }
        }
        static void ExtractGZ(string gzFilepath, string folder, bool batch = false)
        {
            if (!batch)
            {
                GZ gz = new GZ(gzFilepath);
                gz.Unpack(folder);
            }
            else
            {
                var ext = new List<string> { ".gz", ".GZ" };
                var myFiles = Directory.GetFiles(gzFilepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string file in myFiles)
                {
                    GZ gz = new GZ(file);
                    gz.Unpack(folder);
                }
            }
        }
        static void ExtractAFS(string afsFilepath, string folder, bool batch = false)
        {
            if (!batch)
            {
                AFS afs = new AFS(afsFilepath);
                afs.Unpack(folder);
            }
            else
            {
                var ext = new List<string> { ".afs", ".AFS" };
                var myFiles = Directory.GetFiles(afsFilepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string file in myFiles)
                {
                    AFS afs = new AFS(file);
                    afs.Unpack(folder);
                }
            }
        }
        static void ExtractTAC(string tadFilepath, string tacFilepath, string folder)
        {
            TAD tad = new TAD();
            tad.FileName = tadFilepath;
            tad.Unpack(tacFilepath, folder);
        }

        static void PrintUsage()
        {
            Console.WriteLine("\twudecon <mode> <input> <output>");
            Console.WriteLine("\twudecon --mt5 <mt5 file> <obj file>");
            Console.WriteLine("\twudecon --mt7 <mt7 file> <obj file>");
            Console.WriteLine("\twudecon [--pkf|--pks|--spr|--ipac|--gz|--afs] <source file> <output dir>");
            Console.WriteLine("\twudecon --tac <tad file> <tac file> <output dir>");
            Console.WriteLine("\twudecon --batch-mt5 <mt5 dir> <obj output dir>");

            Console.WriteLine("\nBatch flags:\n\t--batch-mt5\n\t--batch-mt7\n\t--batch-pkf\n\t--batch-pks\n\t--batch-spr\n\t--batch-ipac\n\t--batch-gz\n\t--batch-afs");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("wudecon v1.00\n");

            if (args.Count<string>() < 3 || args[0].Contains("-h") || args[0].Contains("--help") || args[0].Contains("/?"))
            {
                PrintUsage();
                return;
            }

            // Model Conversions
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

            // Container conversions
            if ((args[0].Contains("--pkf") || args[0].Contains("-pkf")))
            {
                ExtractPKF(args[1], args[2]);
            }
            else if ((args[0].Contains("--batch-pkf") || args[0].Contains("-bpkf")))
            {
                ExtractPKF(args[1], args[2], true);
            }
            if ((args[0].Contains("--pks") || args[0].Contains("-pks")))
            {
                ExtractPKS(args[1], args[2]);
            }
            else if ((args[0].Contains("--batch-pks") || args[0].Contains("-bpks")))
            {
                ExtractPKS(args[1], args[2], true);
            }

            if ((args[0].Contains("--spr") || args[0].Contains("-spr")))
            {
                ExtractSPR(args[1], args[2]);
            }
            else if ((args[0].Contains("--batch-spr") || args[0].Contains("-bspr")))
            {
                ExtractSPR(args[1], args[2], true);
            }


            if ((args[0].Contains("--ipac") || args[0].Contains("-ipac")))
            {
                ExtractIPAC(args[1], args[2]);
            }
            else if ((args[0].Contains("--batch-ipac") || args[0].Contains("-ipac")))
            {
                ExtractIPAC(args[1], args[2], true);
            }

            if ((args[0].Contains("--gz") || args[0].Contains("-gz")))
            {
                ExtractGZ(args[1], args[2]);
            }
            else if ((args[0].Contains("--batch-gz") || args[0].Contains("-bgz")))
            {
                ExtractGZ(args[1], args[2], true);
            }

            if ((args[0].Contains("--afs") || args[0].Contains("-afs")))
            {
                ExtractAFS(args[1], args[2]);
            }
            else if ((args[0].Contains("--batch-afs") || args[0].Contains("-bafs")))
            {
                ExtractAFS(args[1], args[2], true);

            }

            if ((args[0].Contains("--tac") || args[0].Contains("-tac")))
            {
                ExtractTAC(args[1], args[2], args[3]);
            }

            Console.WriteLine("Finished.");
        }
    }
}
