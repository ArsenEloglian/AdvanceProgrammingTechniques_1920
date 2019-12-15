using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceProcess;

namespace gra
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun= new ServiceBase[] { new usługaŻabki() };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
