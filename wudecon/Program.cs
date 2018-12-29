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
using ShenmueDKSharp.Utils;

namespace wudecon
{
    class Program
    {
        static void ExportMT7(string path, string objFilepath)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", path);

                if (Directory.Exists(path))
                {
                    var ext = new List<string> { ".mt7", ".MT7" };
                    var myFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        string dest = file;
                        dest += ".OBJ";

                        ExportMT7(file, dest);
                    }
                }
                else
                {
                    Console.WriteLine("{0} does not exist. Cancelling operation.", path);
                    return;
                }
            }
            else
            {
                MT7 mt7 = new MT7(path);
                OBJ obj = new OBJ(mt7);

                obj.Write(objFilepath);
            }

            return;
        }
        static void ExportMT5(string path, string objFilepath)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", path);

                if (Directory.Exists(path))
                {
                    var ext = new List<string> { ".mt5", ".MT5" };
                    var myFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        string dest = file;
                        dest += ".OBJ";

                        ExportMT5(file, dest);
                    }
                }
                else
                {
                    Console.WriteLine("{0} does not exist. Cancelling operation.", path);
                    return;
                }
            }
            else
            {
                MT5 mt5 = new MT5(path);
                OBJ obj = new OBJ(mt5);

                obj.Write(objFilepath);
            }
            return;
        }
        static void ExtractPKF(string path, string folder)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", path);

                if (Directory.Exists(path))
                {
                    var ext = new List<string> { ".pkf", ".PKF" };
                    var myFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        PKF pkf = new PKF(file);
                        pkf.Unpack(folder);
                    }
                }
                else
                {
                    Console.WriteLine("{0} does not exist. Cancelling operation.", path);
                    return;
                }
            }
            else
            {
                PKF pkf = new PKF(path);
                pkf.Unpack(folder);
            }
        }
        static void ExtractPKS(string pksFilepath, string folder)
        {
            if (!File.Exists(pksFilepath))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", pksFilepath);

                if(Directory.Exists(pksFilepath))
                {
                    var ext = new List<string> { ".pks", ".PKS" };
                    var myFiles = Directory.GetFiles(pksFilepath, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        PKS pks = new PKS(file);
                        pks.Unpack(folder);
                    }
                }
                else
                {
                    Console.WriteLine("{0} does not exist. Cancelling operation.", pksFilepath);
                    return;
                }
            }
            else
            {
                PKS pks = new PKS(pksFilepath);
                pks.Unpack(folder);
                return;
            }
        }
        static void ExtractSPR(string path, string folder)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", path);

                if (Directory.Exists(path))
                {
                    var ext = new List<string> { ".spr", ".SPR" };
                    var myFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        SPR spr = new SPR(file);

                        spr.Unpack(folder);
                    }
                }
                else
                {
                    Console.WriteLine("{0} does not exist. Cancelling operation.", path);
                    return;
                }
            }
            else
            {
                SPR spr = new SPR(path);

                spr.Unpack(folder);    
            }

            return;
        }
        static void ExtractIPAC(string path, string folder)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", path);

                if (Directory.Exists(path))
                {
                    var ext = new List<string> { ".ipac", ".IPAC" };
                    var myFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        IPAC ipac = new IPAC(file);
                        ipac.Unpack(folder);
                    }
                }
                else
                {
                    Console.WriteLine("{0} does not exist. Cancelling operation.", path);
                    return;
                }
            }
            else
            {
                IPAC ipac = new IPAC(path);
                ipac.Unpack(folder);
            }
            return;
        }
        static void ExtractGZ(string path, string folder)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", path);

                if (Directory.Exists(path))
                {
                    var ext = new List<string> { ".gz", ".GZ" };
                    var myFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        GZ gz = new GZ(file);

                        gz.Unpack(folder);
                    }
                }
                else
                {
                    Console.WriteLine("{0} does not exist. Cancelling operation.", path);
                    return;
                }
            }
            else
            {
                GZ gz = new GZ(path);

                gz.Unpack(folder);
            }
            return;
        }
        static void ExtractAFS(string path, string folder)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", path);

                if (Directory.Exists(path))
                {
                    var ext = new List<string> { ".afs", ".AFS" };
                    var myFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        AFS afs = new AFS(file);

                        afs.Unpack(folder);
                    }
                }
                else
                {
                    Console.WriteLine("{0} does not exist. Cancelling operation.", path);
                    return;
                }
            }
            else
            {
                AFS afs = new AFS(path);

                afs.Unpack(folder);
            }

            return;
        }
        static void ExtractTAC(string tadFilepath, string tacFilepath, string folder)
        {
            TAD tad = new TAD(tadFilepath);
            TAC tac = new TAC(tad);

            tac.Unpack(true, false, folder);
        }

        static void PrintUsage()
        {
            Console.WriteLine("\twudecon <mode> <input> <output>");
            Console.WriteLine("\twudecon --mt5 <mt5 file> <obj file>");
            Console.WriteLine("\twudecon --mt5 <dir with mt5's> <output dir>");
            Console.WriteLine("\twudecon --mt7 <mt7 file> <obj file>");
            Console.WriteLine("\twudecon [--pkf|--pks|--spr|--ipac|--gz|--afs] <source file> <output dir>");
            Console.WriteLine("\twudecon --tac <tad file> <tac file> <output dir>");

            Console.WriteLine("Batch conversion possible by replacing file argument for path");

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

            if ((args[0].Contains("--mt7") || args[0].Contains("-mt7")))
            {
                ExportMT7(args[1], args[2]);
            }

            // Container conversions
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

            Console.WriteLine("Finished.");
        }
    }
}
