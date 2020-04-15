using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Security.Cryptography;
using System.Data;
using System.Xml;
using System.Text.RegularExpressions;

namespace CMS.Framework.Common
{

    public class clsUtility
    {
        public const string strDelimeter = "||";

       
        public static string ConnectionStringName()
        {
            return "connection_string";
        }

        public static string EncryptString(string strPlainText)
        {

            Byte[] bytInitVectorBytes = Encoding.ASCII.GetBytes("@1B2c3D4e5F6g7H8");
            Byte[] bytSaltValueBytes = Encoding.ASCII.GetBytes("s@1tValue");
            Byte[] bytPlainTextBytes = Encoding.UTF8.GetBytes(strPlainText);

            PasswordDeriveBytes strPassword = new PasswordDeriveBytes("Pas5pr@se", bytSaltValueBytes, "SHA1", 2);
            Byte[] bytkeyBytes = strPassword.GetBytes(256 / 8);

            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(bytkeyBytes, bytInitVectorBytes);
            MemoryStream MemoryStream = new MemoryStream();
            CryptoStream CryptoStream = new CryptoStream(MemoryStream, Encryptor, CryptoStreamMode.Write);
            CryptoStream.Write(bytPlainTextBytes, 0, bytPlainTextBytes.Length);
            CryptoStream.FlushFinalBlock();
            Byte[] cipherTextBytes = MemoryStream.ToArray();

            MemoryStream.Close();
            CryptoStream.Close();
            String strcipherText = Convert.ToBase64String(cipherTextBytes);
            return strcipherText;

        }

        public static string DecryptString(string strcipherText)
        {
            Byte[] bytInitVectorBytes = Encoding.ASCII.GetBytes("@1B2c3D4e5F6g7H8");
            Byte[] bytSaltValueBytes = Encoding.ASCII.GetBytes("s@1tValue");
            Byte[] cipherTextBytes = Convert.FromBase64String(strcipherText);
            PasswordDeriveBytes strPassword = new PasswordDeriveBytes("Pas5pr@se", bytSaltValueBytes, "SHA1", 2);

            Byte[] bytkeyBytes = strPassword.GetBytes(256 / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(bytkeyBytes, bytInitVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

            Byte[] bytPlainTextBytes = new Byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(bytPlainTextBytes, 0, bytPlainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return (Encoding.UTF8.GetString(bytPlainTextBytes, 0, decryptedByteCount));
        }

     
    }
}