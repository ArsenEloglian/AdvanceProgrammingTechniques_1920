using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace gra
{
    static partial class Program
    {
        static void twórzPamięćWspółdzieloną()
        {
            Semaphore semaphore = new Semaphore(1, 1, @"Global\graŻabkaSemaphore");
            semaphore.WaitOne();
            ServiceController sc = GetServiceInstalled(ring1ServiceName);
            if (sc == null || sc.Status != ServiceControllerStatus.Running) installAdminService();
            sc.ExecuteCommand(131);
            semaphore.WaitOne();
            MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("Global\\graŻabkaMMF");
            byte[] buffer = new byte[1024];
            mmf.CreateViewStream(0, 1024).Read(buffer, 0, 1024);
            string val1 = (string)new BinaryFormatter().Deserialize(new MemoryStream(buffer));
            MessageBox.Show(val1);
        }
        public static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0) throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0) throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0) throw new ArgumentNullException("IV");
            byte[] encrypted;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
        public static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0) throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0) throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0) throw new ArgumentNullException("IV");
            string plaintext = null;
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }

    }
}
