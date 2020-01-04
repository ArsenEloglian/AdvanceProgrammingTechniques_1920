using System;
using System.Windows.Forms;

namespace wykazPlików
{
    static partial class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new wykazPlików());
        }
    }
}
