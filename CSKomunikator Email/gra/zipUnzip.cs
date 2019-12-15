using Ionic.Zip;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace gra
{
    static partial class Program
    {
        private static void prepareToZipAll(string[] args)
        {
            for (int i = 0; i < args.Length; i++) args[i] += "\\";
            if (zipAll(args) == "") MessageBox.Show("błądTUzipAllprogramCS");
        }

        private static void unzipAll(string[] args)
        {
            foreach (string arg in args) using (ZipFile zip = ZipFile.Read(arg)) zip.ExtractAll(arg.Substring(0, arg.Length - 4));
        }
        public static void zipProcessCommandLineArguments(string[] args)
        {
            if (args.Length == 0) return;
            if (args.Length != 1) MessageBox.Show(args.Length.ToString());
            bool areAllZipFiles = true;
            foreach (string arg in args)
            {
                if (Directory.Exists(arg) || (File.Exists(arg) && !arg.ToLower().EndsWith(".zip")))
                {
                    areAllZipFiles = false;
                    break;
                }
            }
            if (areAllZipFiles) unzipAll(args);
            else prepareToZipAll(args);
        }
        public static string baseDirectory(string fPath)
        {
            if (File.GetAttributes(fPath).HasFlag(FileAttributes.Directory)) return fPath;//skiva lub teczka
            string tmpPath = fPath.Substring(0, fPath.LastIndexOf("\\"));
            return tmpPath.Substring(0, tmpPath.LastIndexOf("\\") + 1);
        }
        public static string cuttedPathToDirectory(string cuttedPath)
        {
            int ostatniaTeczka = cuttedPath.LastIndexOf("\\");
            if (ostatniaTeczka == -1) return "";
            return cuttedPath.Substring(0, ostatniaTeczka + 1);
        }
        public static string getCommonPath(string[] allPaths)
        {
            if (allPaths.Length == 0) return "";
            string commonPath = baseDirectory(allPaths[0]);
            if (allPaths.Length == 1) return commonPath;
            for (int i = 1; i < allPaths.Length; i++)
            {
                int j;
                string tmpPath = baseDirectory(allPaths[i]);
                for (j = 0; j < commonPath.Length && j < tmpPath.Length && char.ToUpper(commonPath[j]) == char.ToUpper(tmpPath[j]); j++) ;
                if (j < commonPath.Length) commonPath = cuttedPathToDirectory(commonPath.Substring(0, j));
            }
            return commonPath;
        }
        public static string zipAll(string[] args)
        {
            string commonPath = getCommonPath(args);
            if (commonPath != "")
            {
                try
                {
                    string lastDirectory = Directory.GetCurrentDirectory();
                    Directory.SetCurrentDirectory(commonPath);
                    ZipFile zip = new ZipFile();
                    zip.AlternateEncoding = Encoding.UTF8;
                    zip.AlternateEncodingUsage = ZipOption.Always;
                    foreach (string wybrany in args)
                    {
                        ZipEntry zipEntry;
                        if (File.GetAttributes(wybrany).HasFlag(FileAttributes.Directory))
                        {
                            if (commonPath == wybrany) zipEntry=zip.AddDirectory(wybrany);
                            else zipEntry = zip.AddDirectory(wybrany, wybrany.Substring(commonPath.Length, wybrany.Length - commonPath.Length - 1));
                        }
                        else zipEntry = zip.AddFile(wybrany.Substring(commonPath.Length, wybrany.Length - 1 - commonPath.Length));
                    }
                    Directory.SetCurrentDirectory(Path.GetPathRoot(lastDirectory));
                    string[] zipFileNames = commonPath.Split(new char[] { '\\' });
                    string zipFileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + zipFileNames[zipFileNames.Length - 2].Replace(':', '_') + ".zip";
                    zip.Save(zipFileName);
                    return zipFileName;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            return "";
        }
    }
}
