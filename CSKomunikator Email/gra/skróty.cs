using System.IO;
using System.Windows.Forms;

namespace gra
{
    static partial class Program
    {
        static void createLink(string path, string name)
        {//c:\windows\system32\shell32.dll
            Shell32.Shell shl = new Shell32.Shell();
            StreamWriter sw = new StreamWriter(path + "\\" + name + ".lnk", false);
            sw.Close();
            Shell32.Folder dir = shl.NameSpace(path);
            Shell32.FolderItem itm = dir.Items().Item(name + ".lnk");
            Shell32.ShellLinkObject lnk = (Shell32.ShellLinkObject)itm.GetLink;
            lnk.Path = Application.ExecutablePath;
            lnk.Description = "pżyłożeńe";
            lnk.Arguments = "";
            lnk.WorkingDirectory = Application.ExecutablePath.ToString().Substring(0, Application.ExecutablePath.ToString().LastIndexOf('\\'));
            lnk.SetIconLocation(Application.ExecutablePath, 0);
            lnk.Save();
        }
    }
}
