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
using ShenmueDKSharp.Files.Images;
using ShenmueDKSharp.Files.Models;
using ShenmueDKSharp.Files.Containers;
using ShenmueDKSharp.Utils;
using ShenmueDKSharp.Files.Misc;

namespace wudecon
{
    class Program
    {
        static bool bTADExtract         = false;
        static bool bVerbose            = false;
        static int iNumFailedOperations = 0;
        static int iNumOperations       = 0;

        /// <summary>
        /// Exports a given MT7 file into an OBJ file at the requested directory
        /// </summary>
        /// <param name="path">Path to MT7</param>
        /// <param name="objFilepath">Path to output the resulting OBJ file</param>
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
                        string filename = Path.GetFileName(file);
                        string dest = currentChildDir + "\\_" + filename + "_\\" + Path.ChangeExtension(filename, ".obj");
                        string dir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

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

                    if(obj != null && mt7 != null)
                        obj.Write(objFilepath);                    

                    if(bVerbose)
                        Console.WriteLine("Converting {0} to {1}", path, objFilepath);
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

        /// <summary>
        /// Exports a given MT5 file into an OBJ file at the requested directory
        /// </summary>
        /// <param name="path">Path to MT5</param>
        /// <param name="objFilepath">Path to output the resulting OBJ file</param>
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
                        string filename = Path.GetFileName(file);
                        string dest = currentChildDir + "\\_" + filename + "_\\" + Path.ChangeExtension(filename, ".obj");
                        string dir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        try
                        {
                            ExportMT5(file, dest);

                            if (bVerbose)
                                Console.WriteLine("Converting {0} to {1}", file, dest);

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

                    if(obj != null && mt5 != null)
                        obj.Write(objFilepath);

                    if(bVerbose)
                        Console.WriteLine("Converted {0} to {1}", path, objFilepath);

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

        /// <summary>
        /// Extracts a given PKF file into the output directory provided
        /// </summary>
        /// <param name="path">Path to PKF</param>
        /// <param name="folder">Path to output the extracted PKF</param>
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
                        string filename = Path.GetFileName(file);
                        string dest = currentChildDir + "\\_" + filename + "_\\";
                        string dir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        try
                        {
                            PKF pkf = new PKF(file);

                            if(pkf != null)
                                pkf.Unpack(dest);

                            if (bVerbose)
                                Console.WriteLine("Unpacking {0} to {1}", file, dest);

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

                    if(pkf != null)
                        pkf.Unpack(folder);

                    if(bVerbose)
                        Console.WriteLine("Unpacking {0} to {1}", path, folder);
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

        /// <summary>
        /// Extracts a given PKS file into the output directory provided
        /// </summary>
        /// <param name="path">Path to PKS</param>
        /// <param name="folder">Path to output the extracted PKS</param>
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
                        string filename = Path.GetFileName(file);
                        string dest = currentChildDir + "\\_" + filename + "_\\";
                        string dir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        try
                        {
                            PKS pks = new PKS(file);

                            if(pks != null)
                                pks.Unpack(dest);

                            if (bVerbose)
                                Console.WriteLine("Unpacking {0} to {1}", file, dest);
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

                    if(pks != null)
                        pks.Unpack(folder);

                    if(bVerbose)
                        Console.WriteLine("Unpacked {0} to {1}", path, folder);

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

        /// <summary>
        /// Extracts a given SPR file into the output directory provided
        /// </summary>
        /// <param name="path">Path to SPR</param>
        /// <param name="folder">Path to output the extracted SPR</param>
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
                        string filename = Path.GetFileName(file);
                        string dest = currentChildDir + "\\_" + filename + "_\\";
                        string dir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        try
                        {
                            SPR spr = new SPR(file);

                            if (spr != null)
                                spr.Unpack(dest);

                            if (bVerbose)
                                Console.WriteLine("Unpacked {0} to {1}", file, dest);

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

                    if(spr != null)
                        spr.Unpack(folder);

                    if(bVerbose)
                        Console.WriteLine("Converted {0} to {1}", path, folder);
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

        /// <summary>
        /// Extracts a given IPAC file into the output directory provided
        /// </summary>
        /// <param name="path">Path to IPAC</param>
        /// <param name="folder">Path to output the extracted IPAC</param>
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
                        string filename = Path.GetFileName(file);
                        string dest = currentChildDir + "\\_" + filename + "_\\";
                        string dir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        try
                        {
                            IPAC ipac = new IPAC(file);

                            if(ipac != null)
                                ipac.Unpack(dest);

                            if (bVerbose)
                                Console.WriteLine("Converted {0} to {1}", file, dest);
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

                    if(ipac != null)
                        ipac.Unpack(folder);

                    if(bVerbose)
                        Console.WriteLine("Converted {0} to {1}", path, folder);
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
        /// <summary>
        /// Extracts a given GZ file into the output directory provided
        /// </summary>
        /// <param name="path">Path to GZ</param>
        /// <param name="folder">Path to output the extracted GZ</param>
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
                        string filename = Path.GetFileName(file);
                        string dest = currentChildDir + "\\_" + filename + "_\\";
                        string dir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        try
                        {
                            GZ gz = new GZ(file);

                            if (gz != null)
                                gz.Unpack(dest);

                            if (bVerbose)
                                Console.WriteLine("Converted {0} to {1}", file, dest);
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

                    if(gz != null)
                        gz.Unpack(folder);

                    if (bVerbose)
                        Console.WriteLine("Converted {0} to {1}", path, folder);
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

        /// <summary>
        /// Extracts a given AFS file into the output directory provided
        /// </summary>
        /// <param name="path">Path to AFS</param>
        /// <param name="folder">Path to output the extracted AFS</param>
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
                        string filename = Path.GetFileName(file);
                        string dest = currentChildDir + "\\_" + filename + "_\\";
                        string dir = Path.GetDirectoryName(dest);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);

                        try
                        {
                            AFS afs = new AFS(file);

                            if (afs != null)
                                afs.Unpack(dest);

                            if (bVerbose)
                                Console.WriteLine("Converted {0} to {1}", file, dest);
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
                    
                    if(afs != null)
                        afs.Unpack(folder);

                    if (bVerbose)
                        Console.WriteLine("Converted {0} to {1}", path, folder);
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

        /// <summary>
        /// Extracts a given TAC file into the output directory provided
        /// </summary>
        /// <param name="tadFilepath">Path to TAC</param>
        /// <param name="tacFilepath">Path to corresponding TAD file</param>
        /// <param name="folder">Path to output the extracted TAC</param>
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

        /// <summary>
        /// Replaces a file in a given TAC file.
        /// </summary>
        /// <param name="file">Path to replace inside TAC file</param>
        /// <param name="tacFilepath">Path to TAC file</param>
        /// <param name="destination">Path to file in the given TAC file to replace</param>
        /// <param name="tadFilepath">(Optional) Path to corresponding TAD file. Will auto-detect if empty.</param>
        static void ReplaceFileInTAC(string file, string tacFilepath, string destination, string tadFilepath = "")
        {
            // If tadFilepath is empty, search for it..
            if (String.IsNullOrEmpty(tadFilepath))
            {
                tadFilepath = Path.ChangeExtension(tacFilepath, ".tad");
                Console.WriteLine("{0}", tadFilepath);
                if (!File.Exists(tadFilepath))
                {
                    Console.WriteLine("ERROR: TAD file could not be found.");
                    ++iNumFailedOperations;
                    return;
                }
            }

            // Ensure the destination directory exists, if not, create it..
            if (!Directory.Exists(Path.GetDirectoryName(destination)))
            {
                Directory.CreateDirectory(Path.GetFullPath(destination));
                if (bVerbose)
                    Console.WriteLine("Creating directory \'{0}\'", Path.GetDirectoryName(destination));
            }

            // Read in the data and write it out..
            if (bVerbose)
                Console.WriteLine("Reading {0}..", tadFilepath);

            TAD tad = new TAD(tadFilepath);
            TAC tac = new TAC(tad, tacFilepath);

            if(tac != null)
            {
                tac.Unpack(bVerbose, false);
            }
        }

        /// <summary>
        /// Extracts a file from a given TAC file.
        /// </summary>
        /// <param name="file">Path to file to extract from TAC file.</param>
        /// <param name="tacFilepath">Path to TAC file to extract the file from.</param>
        /// <param name="destination">Path to output the extracted file from the TAC file.</param>
        /// <param name="tadFilepath">(Optional) Path to corresponding TAD file. Will auto-detect if empty.</param>
        static void ExtractFileFromTAC(string file, string tacFilepath, string destination, string tadFilepath = "")
        {
            // If tadFilepath is empty, search for it..
            if(String.IsNullOrEmpty(tadFilepath))            {
                tadFilepath = Path.ChangeExtension(tacFilepath, ".tad");
                Console.WriteLine("{0}", tadFilepath);
                if(!File.Exists(tadFilepath))                {
                    Console.WriteLine("ERROR: TAD file could not be found.");
                    ++iNumFailedOperations;
                    return;
                }
            }

            // Ensure the destination directory exists, if not, create it..
            if (!Directory.Exists(Path.GetDirectoryName(destination)))            {
                Directory.CreateDirectory(Path.GetFullPath(destination));
                if (bVerbose)
                    Console.WriteLine("Creating directory \'{0}\'", Path.GetDirectoryName(destination));
            }

            // Read in the data and write it out..
            if(bVerbose)
                Console.WriteLine("Reading {0}..", tadFilepath);

            TAD tad = new TAD(tadFilepath);
            TAC tac = new TAC(tad, tacFilepath);
            
            byte[] bytes = tac.GetFileBuffer(file);
            if (tac != null && bytes != null) 
            {
                try
                {
                    destination += "\\";

                    if (String.IsNullOrEmpty(Path.GetFileName(destination)))                    
                        destination += Path.GetFileName(file);
                    

                    FileStream fs = File.Open(destination, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    tac.Close();

                    Console.WriteLine("Finished writing {0} from {1} ({2})", Path.GetFileName(destination), Path.GetFileName(file), Path.GetFileName(tacFilepath));
                    ++iNumOperations;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Oops! {0} failed!\nException: {1}", tacFilepath, e.ToString());
                    ++iNumFailedOperations;
                }
            }
            else
            {
                Console.WriteLine("ERROR: Could not find {0} in TAC {1}", file, Path.GetFileName(tacFilepath));
                ++iNumFailedOperations;
            }
            return;
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
            Console.WriteLine("\twudecon --tacfile <file in tac to extract> <output dir>");

            Console.WriteLine("\n\tBatch conversion possible by replacing file argument for path");
            Console.WriteLine("\tWhen using --tacfile (or -tfile) the path to search for within the TAC needs to be in lowercase.");
            Console.WriteLine("\tFor verbose logging, add 'v' to the beginning or end of the mode, e.g. '--allv' or 'v--mt5'");
        }

        static void Main(string[] args)
        {
            //ShenmueDKSharp settings
            TextureDatabase.Automatic = true;
            MT7.SearchTexturesOneDirUp = false;
            MT5.SearchTexturesOneDirUp = false;
            PVRT.EnableBuffering = true;
            TEXN.WriteTextureBuffer = true;

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

            if ((args[0].Contains("--tfile") || args[0].Contains("-tf")))
            {
                ExtractFileFromTAC(args[1], args[2], args[3]);
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

                                    if (bVerbose)
                                        Console.WriteLine("Finished extracting {0} from {1} to {2}", tadFile.ToString(), tacFile.ToString(), destFolder);
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

                destFolder = args[2] + "\\IPAC";
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
                Console.WriteLine("Finished Processing MT5!");

                destFolder = args[2] + "\\MT7";
                Console.WriteLine("Processing MT7..");
                ExportMT7(src, destFolder);
                Console.WriteLine("Finished processing MT7!");

                timeStart.Stop();

                Console.WriteLine("Operations completed in {0} minutes ({1}ms)", timeStart.ElapsedMilliseconds/60000, timeStart.ElapsedMilliseconds);
            }

            Console.WriteLine("Completed {0}/{1} operations.", iNumOperations, (iNumOperations + iNumFailedOperations));
        }
    }
}
