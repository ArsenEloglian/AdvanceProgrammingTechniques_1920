using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceProcess;

namespace gra
{
    static class Program
    {
        static MemoryMappedFile mmfKernel;
        static MemoryMappedViewStream mmvStreamKernel;
        static void doFirst()
        {
            mmfKernel = MemoryMappedFile.CreateOrOpen("mmf1.data", 1024, MemoryMappedFileAccess.ReadWrite);
            mmvStreamKernel = mmfKernel.CreateViewStream(0, 1024);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(mmvStreamKernel, (int)34);
            mmvStreamKernel.Seek(0, SeekOrigin.Begin);
        }
        static void Main()
        {
            doFirst();
            ServiceBase[] ServicesToRun= new ServiceBase[] { new usługaŻabki() };
            ServiceBase.Run(ServicesToRun);
        }
        public static string gamePath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, Directory.GetCurrentDirectory().LastIndexOf('\\', Directory.GetCurrentDirectory().LastIndexOf('\\', Directory.GetCurrentDirectory().LastIndexOf('\\') - 1) - 1) + 1);
    }
}
