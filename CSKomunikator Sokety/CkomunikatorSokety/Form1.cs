using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CkomunikatorSokety
{
    public partial class Form1 : Form
    {
        public NetworkStream stream;
        public Form1()
        {
            InitializeComponent();
            Text = Dns.GetHostName();
            IPHostEntry ihe = Dns.GetHostEntry("www.o2.pl");
            IPAddress ipAddr = ihe.AddressList[0];
            Text = ipAddr.ToString();
            IPHostEntry iphe = Dns.GetHostEntry("212.77.100.61");
            Text = iphe.HostName;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters RSAowszechny = rsa.ExportParameters(false); //false=owszechny pieczętowanie,true=oba odtworzenie
            RSAParameters RSAoba = rsa.ExportParameters(true);
            rsa.ImportParameters(RSAowszechny);
            byte[] encrypted_Bytes = rsa.Encrypt(Encoding.Unicode.GetBytes("zanakałowane dane"), false);
            rsa.ImportParameters(RSAoba);
            byte[] decrypted_Bytes = rsa.Decrypt(encrypted_Bytes, false);
            Text = Encoding.Unicode.GetString(decrypted_Bytes);
            string dataToHash = "widnieje liternictWo";
            string key = "ABCDEFGHIJKLMNOPQRSTUVWX";
            byte[] dataToHash_Bytes = Encoding.Unicode.GetBytes(dataToHash); //dawać małe macierze bo nie wydajny
            byte[] key_Bytes = Encoding.ASCII.GetBytes(key);
            MACTripleDES mac = new MACTripleDES(key_Bytes);
            byte[] result_Bytes = mac.ComputeHash(dataToHash_Bytes);
            Text = Encoding.ASCII.GetString(result_Bytes);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5_Bytes = md5.ComputeHash(dataToHash_Bytes);
            Text = Encoding.ASCII.GetString(md5_Bytes);
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
            server.Start();
            TcpClient client = server.AcceptTcpClient();
            stream = client.GetStream();
        }
        public byte[] bytes = new byte[100];
        public int i;
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            while (stream.DataAvailable)
            {
                int i = stream.Read(bytes, 0, bytes.Length);
                string str = new ASCIIEncoding().GetString(bytes, 0, i);
                textBox1.Text += str + "\r\n";
                textBox1.Refresh();
            }
            if (e.KeyCode == Keys.Enter)
            {
                byte[] bytes = new ASCIIEncoding().GetBytes(textBox2.Text);
                stream.Write(bytes, 0, bytes.Length);
                textBox2.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
