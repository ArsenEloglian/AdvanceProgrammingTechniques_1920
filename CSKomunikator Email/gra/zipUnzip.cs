using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gra
{
    static class zipUnzip
    {
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
                    foreach (string wybrany in args)
                    {
                        if (File.GetAttributes(wybrany).HasFlag(FileAttributes.Directory))
                        {
                            if (commonPath == wybrany) zip.AddDirectory(wybrany);
                            else zip.AddDirectory(wybrany, wybrany.Substring(commonPath.Length, wybrany.Length - commonPath.Length - 1));
                        }
                        else zip.AddFile(wybrany.Substring(commonPath.Length, wybrany.Length - 1 - commonPath.Length));                       
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
