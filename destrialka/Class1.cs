using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace lab6
{
    class Program
    {
        public static string EncryptECB(string decryptedString, string key)
        {
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            desProvider.Mode = CipherMode.ECB;
            desProvider.Padding = PaddingMode.PKCS7;
            desProvider.Key = Encoding.ASCII.GetBytes("e5d66cf8");
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] data = Encoding.Default.GetBytes(decryptedString);
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
        }
        static byte[] iv;
        public static string EncryptCBC(string decryptedString, string key)
        {
            iv = new byte[8];
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            DESCryptoServiceProvider tDES = new DESCryptoServiceProvider();
            provider.GetBytes(iv);
            tDES.IV = iv;
            tDES.Mode = CipherMode.CBC;
            tDES.Key = Encoding.ASCII.GetBytes(key.ToCharArray());
            using (MemoryStream stream = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(stream, tDES.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] data = Encoding.Default.GetBytes(decryptedString);
                    cs.Write(data, 0, data.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
        }
        public static string DecryptECB(string encryptedString, string key)
        {
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            desProvider.Mode = CipherMode.ECB;
            desProvider.Padding = PaddingMode.PKCS7;
            desProvider.Key = Encoding.ASCII.GetBytes("e5d66cf8");
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(encryptedString)))
            {
                using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs, Encoding.ASCII))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
        public static string DecryptCBC(string encryptedString, string key)
        {
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            desProvider.Mode = CipherMode.CBC;
            desProvider.Padding = PaddingMode.PKCS7;
            desProvider.Key = Encoding.ASCII.GetBytes(key);
            desProvider.IV = iv;
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(encryptedString)))
            {
                using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs, Encoding.ASCII))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Szyfrowanie \n2. Deszyfrowanie");
                var a = Console.ReadLine();
                if (a == "1")
                {
                    Console.WriteLine("1. Szyfrowanie ECB \n2. Szyfrowanie CBC");
                    {
                        var b = Console.ReadLine();
                        if (b == "1")
                        {
                            Console.Write("Podaj tekst do zaszyfrowania: ");
                            var plainText = Console.ReadLine();
                            Console.Write("Podaj klucz: ");
                            var code = Console.ReadLine();
                            var encryptedECB = EncryptECB(plainText, code);
                            Console.Write($"Zaszyfrowany tekst: {encryptedECB}");
                            Console.WriteLine();
                        }
                        else if (b == "2")
                        {
                            Console.Write("Podaj tekst do zaszyfrowania: ");
                            var plainText = Console.ReadLine();
                            Console.Write("Podaj klucz: ");
                            var code = Console.ReadLine();
                            var encryptedCBC = EncryptCBC(plainText, code);
                            Console.Write($"Zaszyfrowany tekst: {encryptedCBC}");
                            Console.WriteLine();
                        }
                    }
                }
                else if (a == "2")
                {
                    Console.WriteLine("1. Deszyfrowanie ECB \n2. Deszyfrowanie CBC");
                    {
                        var b = Console.ReadLine();
                        if (b == "1")
                        {
                            Console.Write("Podaj tekst do rozszyfrowania: ");
                            var plainText = Console.ReadLine();
                            Console.Write("Podaj klucz: ");
                            var code = Console.ReadLine();
                            var decryptedECB = DecryptECB(plainText, code);
                            Console.Write($"Zaszyfrowany tekst: {decryptedECB}");
                            Console.WriteLine();
                        }
                        else if (b == "2")
                        {
                            Console.Write("Podaj tekst do rozszyfrowania: ");
                            var plainText = Console.ReadLine();
                            Console.Write("Podaj klucz: ");
                            var code = Console.ReadLine();
                            var decryptedCBC = DecryptCBC(plainText, code);
                            Console.Write($"Zaszyfrowany tekst: {decryptedCBC}");
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wybrano niepoprawny numer!");
                }
            }
        }
    }
}