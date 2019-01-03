using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ShenmueDKSharp;
using ShenmueDKSharp.Files;
using ShenmueDKSharp.Files.Models;
using ShenmueDKSharp.Files.Containers;
using ShenmueDKSharp.Utils;

namespace wudecon
{
    class Program
    {
        static bool bTADExtract         = false;
        static bool bVerbose            = false;
        static int iNumFailedOperations = 0;
        static int iNumOperations       = 0;

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
                var destFile = Path.GetFileName(objFilepath);
                objFilepath += "_\\";

                Directory.CreateDirectory(objFilepath); 
                objFilepath += destFile;
                
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
                var destFile = Path.GetFileName(objFilepath);
                objFilepath += "_\\";

                Directory.CreateDirectory(objFilepath);
                objFilepath += destFile;

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

                            dest+= "\\_" + Path.GetFileName(file) + "_\\";
                            Directory.CreateDirectory(dest);
                            
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
            try
            {
                TAD tad = new TAD(tadFilepath);
                TAC tac = new TAC(tad);

                tac.Unpack(bVerbose, false, folder);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops! {0} failed!\nException: {1}", tacFilepath, e.ToString());
            }
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

            Console.WriteLine("\n\tBatch conversion possible by replacing file argument for path");
            Console.WriteLine("\tFor verbose logging, add 'v' to the beginning or end of the mode, e.g. '--allv' or 'v--mt5'");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("{0} v{1}\n", Assembly.GetExecutingAssembly().GetName().Name, Assembly.GetExecutingAssembly().GetName().Version.ToString());

            if (args.Count<string>() < 3 || 
                args[0].Contains("-h") || 
                args[0].Contains("--help") || 
                args[0].Contains("/?") ||
                args[0].Contains("-v") ||
                args[0].Contains("--version"))
            {
                PrintUsage();
                return;
            }

            // Enable TAD/TAC batch conversion
            if ((args[0].Contains("t")))
                bTADExtract = true;

            // Enable verbose logging
            if ((args[0].Contains("v")))
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

            string src = args[1];
            string destFolder = args[2];

            if ((args[0].Contains("--all") || args[0].Contains("-a")))
            {
                var timeStart = System.Diagnostics.Stopwatch.StartNew();

                if (!bTADExtract)
                    Console.WriteLine("Processing all formats (except TAC):");
                else
                {
                    destFolder = args[2] + "\\TAC";

                    Console.WriteLine("Processing all formats:");

                    Console.WriteLine("Processing TAC..");

                    var ext = new List<string> { ".tac", ".TAC" };
                    var tacFiles = Directory.GetFiles(src, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    ext = new List<string> { ".tad", ".TAD" };
                    var tadFiles = Directory.GetFiles(src, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                    foreach (string tacFile in tacFiles)
                    {
                        foreach (string tadFile in tadFiles)
                        {
                            if (Path.GetFileNameWithoutExtension(tacFile).Equals(Path.GetFileNameWithoutExtension(tadFile)))
                            {
                                try
                                {
                                    ExtractTAC(tadFile, tacFile, destFolder);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Oops! {0} failed!\nException: {1}", tacFile, e.ToString());

                                    ++iNumFailedOperations;
                                }
                                ++iNumOperations;
                            }
                        }
                    }

                    Console.WriteLine("Finished processing TAC!");
                }

                destFolder = args[2] + "\\PKF";
                Console.WriteLine("Processing PKF..");
                ExtractPKF(src, destFolder);
                Console.WriteLine("Finished processing PKF!");

                destFolder = args[2] + "\\PKS";
                Console.WriteLine("Processing PKS..");
                ExtractPKS(src, destFolder);
                Console.WriteLine("Finished processing PKS!");

                destFolder = args[2] + "\\SPR";
                Console.WriteLine("Processing SPR..");
                ExtractSPR(src, destFolder);
                Console.WriteLine("Finished processing SPR!");

                /*destFolder = args[2] + "\\IPAC";
                Console.WriteLine("Processing IPAC..");
                ExtractIPAC(src, destFolder);
                Console.WriteLine("Finished processing IPAC!");

                destFolder = args[2] + "\\GZ";
                Console.WriteLine("Processing GZ..");
                ExtractGZ(src, destFolder);
                Console.WriteLine("Finished processing GZ!");

                destFolder = args[2] + "\\AFS";
                Console.WriteLine("Processing AFS..");
                ExtractAFS(src, destFolder);
                Console.WriteLine("Finished processing AFS!");

                destFolder = args[2] + "\\MT5";
                Console.WriteLine("Processing MT5..");
                ExportMT5(src, destFolder);
                Console.WriteLine("Finished Processing MT5!");*/

                destFolder = args[2] + "\\MT7";
                Console.WriteLine("Processing MT7..");
                ExportMT7(src, destFolder);
                Console.WriteLine("Finished processing MT7!");

                timeStart.Stop();

                Console.WriteLine("Operations completed in {0} minutes ({1}ms)", timeStart.ElapsedMilliseconds/60000, timeStart.ElapsedMilliseconds);
            }
            else if((args[0].Contains("--process")))
            {
                var texDir  = args[1];
                src         = args[2];
                destFolder  = args[2];

                Console.WriteLine("Initializing TextureDatabase..");
                TextureDatabase.SearchDirectory(texDir);

                Console.WriteLine("Converting models..");
                ExportMT7(src, destFolder);

                Console.WriteLine("Finished!");
                
                /*Console.WriteLine("Processing all TAD's");
                var ext = new List<string> { ".tac", ".TAC" };
                var tacFiles = Directory.GetFiles(src, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                ext = new List<string> { ".tad", ".TAD" };
                var tadFiles = Directory.GetFiles(src, "*.*", SearchOption.AllDirectories).Where(s => ext.Contains(Path.GetExtension(s)));

                foreach (string tacFile in tacFiles)
                {
                    foreach (string tadFile in tadFiles)
                    {
                        if (Path.GetFileNameWithoutExtension(tacFile).Equals(Path.GetFileNameWithoutExtension(tadFile)))
                        {
                            try
                            {
                                ExtractTAC(tadFile, tacFile, destFolder);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Oops! {0} failed!\nException: {1}", tacFile, e.ToString());

                                ++iNumFailedOperations;
                            }
                            ++iNumOperations;
                        }
                    }
                }
                Console.WriteLine("Finished processing");*/

            }

            if (iNumOperations > 0 || iNumFailedOperations > 0)
                Console.WriteLine("Failed {1}/{0} operations.", iNumOperations, iNumFailedOperations);
        }
    }
}
