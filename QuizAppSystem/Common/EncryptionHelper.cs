//using System;
//using System.IO;
//using System.Security.Cryptography;
//using System.Text;


//namespace QuizAppSystem.Common;

//public static class EncryptionHelper
//{
//    private const string EncryptionKey = "YourEncryptionKey"; 
//    private const string EncryptionIV = "YourEncryptionIV"; 

//    public static string Encrypt(string plainText)
//    {
//        using (Aes aesAlg = Aes.Create())
//        {
//            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
//            aesAlg.IV = Encoding.UTF8.GetBytes(EncryptionIV);

//            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

//            using (MemoryStream msEncrypt = new MemoryStream())
//            {
//                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
//                {
//                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
//                    {
//                        swEncrypt.Write(plainText);
//                    }
//                }

//                byte[] encryptedBytes = msEncrypt.ToArray();
//                return Convert.ToBase64String(encryptedBytes);
//            }
//        }
//    }

//    public static string Decrypt(string cipherText)
//    {
//        byte[] cipherBytes = Convert.FromBase64String(cipherText);

//        using (Aes aesAlg = Aes.Create())
//        {
//            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
//            aesAlg.IV = Encoding.UTF8.GetBytes(EncryptionIV);

//            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

//            using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
//            {
//                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
//                {
//                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
//                    {
//                        return srDecrypt.ReadToEnd();
//                    }
//                }
//            }
//        }
//    }
//}
