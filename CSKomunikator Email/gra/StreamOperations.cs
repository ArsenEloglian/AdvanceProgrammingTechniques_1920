using System;
using System.IO;
using System.Text;

namespace gra
{
    static partial class Program
    {
        static public void dopiszWybranyPlik(string wybranyPlik, string coDopisujemy)
        {
            if (!File.Exists(wybranyPlik))
            {
                using (StreamWriter sw = File.CreateText(wybranyPlik))
                {
                    sw.WriteLine(coDopisujemy);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(wybranyPlik))
                {
                    sw.WriteLine(coDopisujemy);
                }
            }
        }
        static public void Stream_WriteUInt(Stream ioStream, uint tenInt)
        {
            ioStream.WriteByte((byte)(tenInt / (256 * 256 * 256)));
            ioStream.WriteByte((byte)(tenInt / (256 * 256)));
            ioStream.WriteByte((byte)(tenInt / 256));
            ioStream.WriteByte((byte)(tenInt & 255));
            ioStream.Flush();
        }
        static public void Stream_WriteULng(Stream ioStream, ulong tenLng)
        {
            ulong halfLng = 256 * 256 * 256;
            halfLng *= 256;
            Stream_WriteUInt(ioStream, (uint)(tenLng / halfLng));
            Stream_WriteUInt(ioStream, (uint)(tenLng & (halfLng - 1)));
            ioStream.Flush();
        }
        static public void Stream_WriteString(Stream ioStream, string outString)
        {
            byte[] outBuffer = new UnicodeEncoding().GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue) len = (int)UInt16.MaxValue;
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();
        }
        static public void Stream_WriteStrings(Stream ioStream, string[] outStrings)
        {
            Stream_WriteUInt(ioStream, (uint)outStrings.Length);
            foreach (string outString in outStrings) Stream_WriteString(ioStream, outString);
        }
        static public uint Stream_ReadUInt(Stream ioStream)
        {
            return (uint)(ioStream.ReadByte() * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 + ioStream.ReadByte() * 256 + ioStream.ReadByte());
        }
        static public ulong Stream_ReadULng(Stream ioStream)
        {
            return (ulong)(ioStream.ReadByte() * 256 * 256 * 256 * 256 * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 * 256 * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 * 256 + ioStream.ReadByte() * 256 * 256 + ioStream.ReadByte() * 256 + ioStream.ReadByte());
        }
        static public string Stream_ReadString(Stream ioStream)
        {
            int len;
            len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            var inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);
            return new UnicodeEncoding().GetString(inBuffer);
        }
        static public string[] Stream_ReadStrings(Stream ioStream)
        {
            string[] outStrings = new string[Stream_ReadUInt(ioStream)];
            for (int i = 0; i < outStrings.Length; i++) outStrings[i] = Stream_ReadString(ioStream);
            return outStrings;
        }
        static public void Stream_WriteBytes(Stream ioStream, byte[] bytes)
        {
            Stream_WriteUInt(ioStream, (uint)bytes.Length);
            ioStream.Write(bytes, 0, bytes.Length);
            ioStream.Flush();
        }
        static public byte[] Stream_ReadBytes(Stream ioStream)
        {
            byte[] bytes = new byte[Stream_ReadUInt(ioStream)];
            ioStream.Read(bytes, 0, bytes.Length);
            return bytes;
        }
    }
}
