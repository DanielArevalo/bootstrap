using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Xpinn.Util
{
    public class CifradoBusiness
    {
        private Rijndael rijndael;
        private byte[] key;
        private byte[] iv;
        private int keySize;
        private int ivSize;

        public CifradoBusiness()
        {
            key = UTF8Encoding.UTF8.GetBytes("xpinn");
            iv = UTF8Encoding.UTF8.GetBytes("colombia");
            keySize = 32;
            ivSize = 16;
            Array.Resize(ref key, keySize);
            Array.Resize(ref iv, ivSize);
        }

        public string Encriptar(string pTexto)
        {
            try
            {
                rijndael = Rijndael.Create();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         rijndael.CreateEncryptor(key, iv),
                                                         CryptoStreamMode.Write);
                byte[] plainMessageBytes = UTF8Encoding.UTF8.GetBytes(pTexto);
                cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherMessageBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();

                return Convert.ToBase64String(cipherMessageBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("CifradoBusiness.Encriptar: " + ex.Message);
            }
        }

        public string Desencriptar(string pTexto)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(pTexto);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                Rijndael RijndaelAlg = Rijndael.Create();
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                             RijndaelAlg.CreateDecryptor(key, iv),
                                                             CryptoStreamMode.Read);
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();

                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                throw new Exception("CifradoBusiness.Desencriptar: " + ex.Message);
            }
        }

        public string Encrypt(string input, HashAlgorithm hashAlgorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = hashAlgorithm.ComputeHash(inputBytes);

            StringBuilder output = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)
                output.Append(hashedBytes[i].ToString("x2").ToLower());

            return output.ToString();
        }

        public string SHA256Encrypt(string input)
        {
            return Encrypt(input, new SHA256CryptoServiceProvider());
        }


        public string DesencriptarII(string pTexto)
        {
            try
            {    
                byte[] cipherTextBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(pTexto);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                Rijndael RijndaelAlg = Rijndael.Create();
                return System.Text.ASCIIEncoding.ASCII.GetString(plainTextBytes);
            }
            catch (Exception ex)
            {
                throw new Exception("CifradoBusiness.DesencriptarII: " + ex.Message);
            }
        }

    }
}
