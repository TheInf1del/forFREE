using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PES_CPK_Solution
{
    class Encryptions
    {
        public static string read_username;
        public static string read_serialNumber;
        public static string gistUser = "ec9uiUGjpFZMX+yw+QuxTLUuIKws2Pr/1bpf4KjCChNYuFkOiH1NbDn40hPIRwZB+dOsBmn1XM8nhWtlf2aTf5lnwua0BA3dopcxe/AmzEJdOVS0ngRwDgF7R50HQdVfW4dGOgfUVL3eYcuNRKbUUkQr1VuDU1KSVt/RMOTlMfyWogeVUjejtr+y+mLRGQQmEAEvH778WZ3BSSjkcT+WZz17FInAPTeGAplb8AvNCZI=";
        public static string gitHash = "+tevY4dt4JkFM5yFmaOfakx7ZI3PgV+zKvhkOATLq94=";
        public static string ms = "ueuZRm6z3iZi6dKRXzhyqpxS81qCwIoJqGWMvTD1vBobSqPc/DCd1stJ1MN36M20bUIWDRnr67FnhfCLnf5TkA==";
        public static string p = "Wd6jTeQeVC1599qFWDipn/glHJOsfZ6rbN3HdbYXe6TQQB2NAdNJY/SE1BmOJJGW9focA74iJoroDCpkTUDotXb4oFd4XEjQnghHsnUgSiUHU+uP+jfsBKrshNiXtY71K8+lX3uf4KbnOi02JrvqLjHc98et5IbUj3D9tOrU/fs=";
        public static string t = "9lCjnKBzny12rZLP65JlkTT0dDIZi7eDkXedgv72+do=";
        public static string rg = "sy9XILG4OEr4eoxWoD3Vye6Qu0uaocgnmnqWLgZ/8cWxvBwXXECHiWakt0Cvqb9ycucOMP/NVpFrspheQflf3WnEQyymD2n+fez4HLkIZ4a6Obl/bKNL36vf48ylhlql9JawGSbWsFxzAlMtIa05Z8GsuqWfpCwaU/gNQxVz/IhWy79UnOQH2x1ucZHdRbye2/O02QEL6X2+ymfzPnSk4g==";
        public static byte[] Compress(byte[] toCompress)
        {
            byte[] tempCompress = null;
            using (MemoryStream inputStream = new MemoryStream(toCompress))
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    using (DeflateStream compressionStream = new DeflateStream(outputStream, CompressionMode.Compress))
                    {
                        inputStream.CopyTo(compressionStream);
                    }
                    tempCompress = outputStream.ToArray();
                }
            }
            return tempCompress;
        }

        public static string GetFileSize(string TheFile)
        {
            double DoubleBytes;
            if (TheFile.Length == 0)
            {
                return "";
            }
            if (!File.Exists(TheFile))
            {
                return "";
            }
            ulong TheSize = (ulong)((new System.IO.FileInfo(TheFile)).Length);

            try
            {
                if (TheSize >= 1099511627776)
                {
                    DoubleBytes = (double)(TheSize / 1099511627776.0); //TB
                    return string.Format("{0:N2}", DoubleBytes) + " TB";
                }
                else if (TheSize >= 1073741824 && TheSize <= 1099511627775)
                {
                    DoubleBytes = (double)(TheSize / 1073741824.0); //GB
                    return string.Format("{0:N2}", DoubleBytes) + " GB";
                }
                else if (TheSize >= 1048576 && TheSize <= 1073741823)
                {
                    DoubleBytes = (double)(TheSize / 1048576.0); //MB
                    return string.Format("{0:N2}", DoubleBytes) + " MB";
                }
                else if (TheSize >= 1024 && TheSize <= 1048575)
                {
                    DoubleBytes = (double)(TheSize / 1024.0); //KB
                    return string.Format("{0:N2}", DoubleBytes) + " KB";
                }
                else if (TheSize >= 0 && TheSize <= 1023)
                {
                    DoubleBytes = TheSize; // bytes
                    return string.Format("{0:N2}", DoubleBytes) + " bytes";
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
        public static string FormatBytes(ulong BytesCaller)
        {
            double DoubleBytes2;

            try
            {
                if (BytesCaller >= 1099511627776)
                {
                    DoubleBytes2 = (double)(BytesCaller / 1099511627776.0); //TB
                    return string.Format("{0:N2}", DoubleBytes2) + " TB";
                }
                else if (BytesCaller >= 1073741824 && BytesCaller <= 1099511627775)
                {
                    DoubleBytes2 = (double)(BytesCaller / 1073741824.0); //GB
                    return string.Format("{0:N2}", DoubleBytes2) + " GB";
                }
                else if (BytesCaller >= 1048576 && BytesCaller <= 1073741823)
                {
                    DoubleBytes2 = (double)(BytesCaller / 1048576.0); //MB
                    return string.Format("{0:N2}", DoubleBytes2) + " MB";
                }
                else if (BytesCaller >= 1024 && BytesCaller <= 1048575)
                {
                    DoubleBytes2 = (double)(BytesCaller / 1024.0); //KB
                    return string.Format("{0:N2}", DoubleBytes2) + " KB";
                }
                else if (BytesCaller >= 0 && BytesCaller <= 1023)
                {
                    DoubleBytes2 = BytesCaller; // bytes
                    return string.Format("{0:N2}", DoubleBytes2) + " bytes";
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }

        }
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}