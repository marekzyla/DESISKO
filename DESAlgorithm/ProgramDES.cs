//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Threading;

//namespace DESAlgorithm
//{
//    class ProgramDES
//    {
//        static void Main(string[] args)
//        {
//            var desProvider = new DESCryptoServiceProvider();
//            char choice = ' ';
//            do
//            {

//                Console.WriteLine("\n-------\n");
//                Console.WriteLine("Choose action to perform");
//                Console.WriteLine("C-Code using DES\nD-Dedocode using DES");
//                Console.WriteLine("Press key, [[other to exit]]");
//                choice = Console.ReadKey().KeyChar.ToString().ToUpper()[0];
               
              
//                byte[] keyBytes = new byte[8];
//                if (choice == 'C' || choice == 'D')
//                {
//                    Console.WriteLine();
//                    Console.WriteLine("Enter 8-bit key");
                   
//                    for (int i = 0; i < 8; i++)
//                    {
//                        keyBytes[i] = byte.Parse(Console.ReadKey().KeyChar.ToString());
//                    }
//                    Console.WriteLine();
//                }
//                switch (choice)
//                {
//                    case 'C':
//                        Console.WriteLine("Enter text");
//                        string text = Console.ReadLine();
//                        Code(text, desProvider, keyBytes);
//                        break;
//                    case 'D':
//                        Console.WriteLine("Enter coded information separated by white space");
//                        var codedBytes = new List<byte>();
//                        var coded = Console.ReadLine().Split(' ');
//                        foreach (var numbers in coded)
//                        {
//                            byte sByte;
//                            if (byte.TryParse(numbers, out sByte))
//                            {
//                                codedBytes.Add(byte.Parse(numbers));
//                            }
                          
                          
//                        }
//                        Decode(codedBytes.ToArray(), desProvider, keyBytes);
//                        break;
//                }

//                Thread.Sleep(1000);
//                Console.WriteLine("Press any key to continue");
//                Console.ReadLine();
              
//            } while (choice == 'C' || choice == 'D');
//        }

//        static void Code(string text, DESCryptoServiceProvider provider, byte[] key)
//        {
//            var encryptor = provider.CreateEncryptor(key, provider.IV);
//            var memoryStream = new MemoryStream();
//            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
//            using (var streamWriter = new StreamWriter(cryptoStream))
//            {
//                streamWriter.Write(text);
//            }
//            byte[] encrypted = memoryStream.ToArray();
//            Console.WriteLine("Coded");
//            foreach (var bit in encrypted)
//            {
//                Console.Write("{0} ", bit);
//            }
//            Console.WriteLine();
//        }

//        static void Decode(byte[] coded, DESCryptoServiceProvider provider, byte[] key)
//        {
//            var decryptor = provider.CreateDecryptor(key, provider.IV);
//            var memoryStream = new MemoryStream(coded);
//            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
//            var text = "";
//            using (var streamReader = new StreamReader(cryptoStream))
//            {
//                text = streamReader.ReadToEnd();
//            }
//            Console.WriteLine("Decoded");
//            Console.WriteLine(text);
//            Console.WriteLine();
//        }
//    }
//}