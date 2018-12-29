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
        static bool bVerbose = false;
        static int iNumFailedOperations = 0;
        static int iNumOperations = 0;

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
                        var currentChildDir = objFilepath + "\\" + Path.GetDirectoryName(file.Replace(path, ""));
                        if (!Directory.Exists(currentChildDir))
                            Directory.CreateDirectory(currentChildDir);

                        string dest = currentChildDir + "\\" + Path.GetFileName(file) + ".OBJ";
                                                
                        try
                        {    
                            if(bVerbose)
                                Console.WriteLine("Converting {0} to {1}", file, dest);

                            ExportMT7(file, dest);
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("Oops! {0} failed!\nException: {1}", file, e.ToString());

                            ++iNumFailedOperations;
                        }

                        ++iNumOperations;
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
                try
                {
                    MT7 mt7 = new MT7(path);
                    OBJ obj = new OBJ(mt7);

                    obj.Write(objFilepath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                    ++iNumFailedOperations;
                }

                ++iNumOperations;
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
                        var currentChildDir = objFilepath + "\\" + Path.GetDirectoryName(file.Replace(path, ""));
                        if (!Directory.Exists(currentChildDir))
                            Directory.CreateDirectory(currentChildDir);

                        string dest = currentChildDir + "\\" + Path.GetFileName(file) + ".OBJ";

                        try
                        {
                            if (bVerbose)
                                Console.WriteLine("Converting {0} to {1}", file, dest);

                            ExportMT5(file, dest);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oops! {0} failed!\nException: {1}", file, e.ToString());

                            ++iNumFailedOperations;
                        }

                        ++iNumOperations;
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
                try
                {
                    MT5 mt5 = new MT5(path);
                    OBJ obj = new OBJ(mt5);

                    obj.Write(objFilepath);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                    ++iNumFailedOperations;
                }

                ++iNumOperations;
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
                        var currentChildDir = folder + "\\" + Path.GetDirectoryName(file.Replace(path, ""));
                        if (!Directory.Exists(currentChildDir))
                            Directory.CreateDirectory(currentChildDir);

                        string dest = currentChildDir + "\\";

                        try
                        {
                            PKF pkf = new PKF(file);

                            if (bVerbose)
                                Console.WriteLine("Unpacking {0} to {1}", file, dest);

                            pkf.Unpack(dest);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oops! {0} failed!\nException: {1}", file, e.ToString());

                            ++iNumFailedOperations;
                        }

                        ++iNumOperations;
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
                try
                {
                    PKF pkf = new PKF(path);
                    pkf.Unpack(folder);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                    ++iNumFailedOperations;
                }

                ++iNumOperations;
            }
            return;
        }
        static void ExtractPKS(string path, string folder)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("{0} does not exist as a file, falling back to batch mode.", path);

                if(Directory.Exists(path))
                {
                    var ext = new List<string> { ".pks", ".PKS" };
                    var myFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string file in myFiles)
                    {
                        var currentChildDir = folder + "\\" + Path.GetDirectoryName(file.Replace(path, ""));
                        if (!Directory.Exists(currentChildDir))
                            Directory.CreateDirectory(currentChildDir);

                        string dest = currentChildDir + "\\";

                        try
                        {
                            PKS pks = new PKS(file);

                            if (bVerbose)
                                Console.WriteLine("Unpacking {0} to {1}", file, dest);

                            pks.Unpack(dest);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oops! {0} failed!\nException: {1}", file, e.ToString());

                            ++iNumFailedOperations;
                        }

                        ++iNumOperations;
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
                try
                {
                    PKS pks = new PKS(path);
                    pks.Unpack(folder);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                    ++iNumFailedOperations;
                }
                ++iNumOperations;
            }
            return;
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
                        var currentChildDir = folder + "\\" + Path.GetDirectoryName(file.Replace(path, ""));
                        if (!Directory.Exists(currentChildDir))
                            Directory.CreateDirectory(currentChildDir);

                        string dest = currentChildDir + "\\";

                        try
                        {
                            SPR spr = new SPR(file);

                            if (bVerbose)
                                Console.WriteLine("Unpacking {0} to {1}", file, dest);

                            spr.Unpack(dest);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                            ++iNumFailedOperations;
                        }
                        ++iNumOperations;
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
                try
                {
                    SPR spr = new SPR(path);

                    spr.Unpack(folder);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                    ++iNumFailedOperations;
                }
                ++iNumOperations;
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
                        var currentChildDir = folder + "\\" + Path.GetDirectoryName(file.Replace(path, ""));
                        if (!Directory.Exists(currentChildDir))
                            Directory.CreateDirectory(currentChildDir);

                        string dest = currentChildDir + "\\";

                        try
                        {
                            IPAC ipac = new IPAC(file);

                            if (bVerbose)
                                Console.WriteLine("Converting {0} to {1}", file, dest);

                            ipac.Unpack(dest);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                            ++iNumFailedOperations;
                        }
                        ++iNumOperations;
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
                try
                {
                    IPAC ipac = new IPAC(path);
                    ipac.Unpack(folder);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                    ++iNumFailedOperations;
                }
                ++iNumOperations;

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
                        var currentChildDir = folder + "\\" + Path.GetDirectoryName(file.Replace(path, ""));
                        if (!Directory.Exists(currentChildDir))
                            Directory.CreateDirectory(currentChildDir);

                        string dest = currentChildDir + "\\";

                        try
                        {
                            GZ gz = new GZ(file);

                            if (bVerbose)
                                Console.WriteLine("Converting {0} to {1}", file, dest);

                            gz.Unpack(dest);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                            ++iNumFailedOperations;
                        }
                        ++iNumOperations;

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
                try
                {
                    GZ gz = new GZ(path);

                    gz.Unpack(folder);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                    ++iNumFailedOperations;
                }
                ++iNumOperations;

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
                        var currentChildDir = folder + "\\" + Path.GetDirectoryName(file.Replace(path, ""));
                        if (!Directory.Exists(currentChildDir))
                            Directory.CreateDirectory(currentChildDir);

                        string dest = currentChildDir + "\\";

                        try
                        {
                            AFS afs = new AFS(file);

                            if (bVerbose)
                                Console.WriteLine("Converting {0} to {1}", file, dest);

                            afs.Unpack(dest);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                            ++iNumFailedOperations;
                        }
                        ++iNumOperations;
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
                try
                {
                    AFS afs = new AFS(path);

                    afs.Unpack(folder);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", path, e.ToString());

                    ++iNumFailedOperations;
                }
                ++iNumOperations;

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
            Console.WriteLine("\twudecon --all <input dir> <output dir>");
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

            if((args[0].Contains("v")))
                bVerbose = true;

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


            if((args[0].Contains("--all") || args[0].Contains("-a")))
            {
                Console.WriteLine("Processing all formats (except TAC):");

                Console.WriteLine("Processing PKF..");
                ExtractAFS(args[1], args[2]);
                Console.WriteLine("Finished processing PKF!");

                Console.WriteLine("Processing PKS..");
                ExtractPKS(args[1], args[2]);
                Console.WriteLine("Finished processing PKS!");

                Console.WriteLine("Processing SPR..");
                ExtractSPR(args[1], args[2]);
                Console.WriteLine("Finished processing SPR!");

                Console.WriteLine("Processing IPAC..");
                ExtractIPAC(args[1], args[2]);
                Console.WriteLine("Finished processing IPAC!");

                Console.WriteLine("Processing GZ..");
                ExtractGZ(args[1], args[2]);
                Console.WriteLine("Finished processing GZ!");

                Console.WriteLine("Processing AFS..");
                ExtractAFS(args[1], args[2]);
                Console.WriteLine("Finished processing AFS!");

                Console.WriteLine("Processing MT5..");
                ExportMT5(args[1], args[2]);
                Console.WriteLine("Finished Processing MT5!");

                Console.WriteLine("Processing MT7..");
                ExportMT7(args[1], args[2]);
                Console.WriteLine("Finished processing MT7!");
            }

            Console.WriteLine("Failed {1}/{0} operations.", iNumOperations, iNumFailedOperations);
        }
    }
}
