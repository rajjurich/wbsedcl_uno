using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;

namespace UNO
{
    public class TripleDESImplementation
    {
        //Encryption Key 
        private byte[] EncryptionKey { get; set; }
        // The Initialization Vector for the DES encryption routine 
        private byte[] IV { get; set; }

        /// <summary> 
        /// Constructor for TripleDESImplementation class 
        /// </summary> 
        /// <param name="encryptionKey">The 24-byte encryption key (24 character ASCII)</param> 
        /// <param name="IV">The 8-byte DES encryption initialization vector (8 characters ASCII)</param> 
        public TripleDESImplementation(string encryptionKey, string IV)
        {
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentNullException("'encryptionKey' parameter cannot be null.", "encryptionKey");
            }
            if (string.IsNullOrEmpty(IV))
            {
                throw new ArgumentException("'IV' parameter cannot be null or empty.", "IV");
            }

            EncryptionKey = Encoding.ASCII.GetBytes(encryptionKey);
            // Ensures length of 24 for encryption key 
            Trace.Assert(EncryptionKey.Length == 24, "Encryption key must be exactly 24 characters of ASCII text (24 bytes)");

            this.IV = Encoding.ASCII.GetBytes(IV);
            // Ensures length of 8 for init. vector 
            Trace.Assert(IV.Length == 8, "Init. vector must be exactly 8 characters of ASCII text (8 bytes)");
        }
        private static byte[] HEXToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        public TripleDESImplementation(string encryptionKey, byte[] IV)
        {
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentNullException("'encryptionKey' parameter cannot be null.", "encryptionKey");
            }
            if (IV == null)
            {
                throw new ArgumentException("'IV' parameter cannot be null or empty.", "IV");
            }
            if (IV.Length < 8)
            {
                byte[] tmp = new byte[8];
                IV.CopyTo(tmp, 8 - IV.Length);
                IV = tmp;
            }
            EncryptionKey = Encoding.ASCII.GetBytes(encryptionKey);
            // Ensures length of 24 for encryption key 
            Trace.Assert(EncryptionKey.Length == 24, "Encryption key must be exactly 24 characters of ASCII text (24 bytes)");

            this.IV = IV;
            // Ensures length of 8 for init. vector 
            Trace.Assert(IV.Length == 8, "Init. vector must be exactly 8 characters of ASCII text (8 bytes)");
        }

        /// <summary> 
        /// Encrypts a text block 
        /// </summary> 
        /// 
        public static string EncryptKey(string Key, string CSN)
        {

            byte[] csnbytes = HEXToByteArray(CSN);
            string binKey = HexToBinary(Key.PadLeft(12, '0')).PadLeft(48, '0');
            binKey = binKey.Replace("0", ".");
            binKey = binKey.Replace("1", "0");
            binKey = binKey.Replace(".", "1");
            binKey = (Convert.ToUInt64(binKey, 2)).ToString("X").PadLeft(12, '0');
            //string InverseKey = ReverseKey(Key, CSN); // Convert.ToUInt64(binKey, 2).ToString("X").PadLeft(12, '0');

            TripleDESImplementation tep = new TripleDESImplementation(Key + binKey, csnbytes);
            string strTmp = tep.EncryptECB(CSN);
            return strTmp.Substring(strTmp.Length - 12);
            //return Strings.Right(strTmp, 12);
        }

        private string EncryptECB(string textToEncrypt)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = EncryptionKey;
            tdes.Padding = PaddingMode.None;
            //  tdes.IV = IV;
            tdes.Mode = CipherMode.ECB;
            byte[] buffer = Encoding.ASCII.GetBytes(textToEncrypt);
            buffer = tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length);
            return byteArrayToHexString(buffer);
            //  return Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }
        private string Encrypt(string textToEncrypt)
        {
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = EncryptionKey;
            tdes.IV = IV;

            byte[] buffer = Encoding.ASCII.GetBytes(textToEncrypt);
            buffer = tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length);
            return byteArrayToHexString(buffer);
            return Convert.ToBase64String(tdes.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }
        private static string byteArrayToHexString(byte[] tmpbytes)
        {
            if (tmpbytes == null) return "";
            if (tmpbytes.Length <= 0)
                return "";

            string strOut = "";
            for (int i = 0; i < tmpbytes.Length; i++)
            {
                strOut = strOut + tmpbytes[i].ToString("X").PadLeft(2, '0');
                //strOut = strOut + Conversion.Hex(tmpbytes[i]).ToString().PadLeft(2, '0') + "";
            }

            return strOut;

        }
        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
        private static string HexToBinary(string hexstring)
        {
            string binarystring = String.Join(String.Empty,
                hexstring.Select(
                            c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            return binarystring;
        }
        /// <summary> 
        /// Decrypts an encrypted text block 
        /// </summary> 
        private string Decrypt(string textToDecrypt)
        {
            byte[] buffer = StringToByteArray(textToDecrypt);// Convert.FromBase64String(textToDecrypt);

            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = EncryptionKey;
            des.IV = IV;

            return Encoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
        }

    }
}