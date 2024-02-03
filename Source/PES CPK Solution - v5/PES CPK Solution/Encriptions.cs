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
    class Encriptions
    {

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

        public static byte[] Decompress(byte[] toDecompress)
        {
            byte[] tempDecompress = null;
            using (MemoryStream inputStream = new MemoryStream(toDecompress))
            {
                using (MemoryStream outputStream = new MemoryStream())
                {
                    using (DeflateStream decompressionStream = new DeflateStream(inputStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(outputStream);
                    }
                    tempDecompress = outputStream.ToArray();
                }
            }
            return tempDecompress;
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

        public static string encrypt(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6E, 0x20, 0x4D, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
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
