using System;
using System.CodeDom;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

class Des
{
    // 1001001010101110011000011111110101011101001100001001010111110101

    static byte[] BitsToBytes(string coded)
    {
        var result = new byte[8];
        int indexisko = 0;
        for (int i = 0; i < coded.Length; i += 8)
        {
            string g = "";
            for (int j = i; j < i + 8; j++)
            {
                g += coded[j];
            }
            result[indexisko] += ConvertStringToByte(g);
            indexisko++;
        }
        return result;
    }

    private static byte ConvertStringToByte(string s)
    {
        string reverse = "";
        for (int i = s.Length - 1; i >= 0; i--)
        {
            reverse += s[i];
        }
        s = reverse;
        byte result = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '1')
            {
                result += (byte) Math.Pow(2, i);
            }
        }

        return result;
    }

    static string[] ByteToBits(byte[] coded)
    {
        var result = new string[coded.Length];
        for (int i = 0; i < coded.Length; i++)
        {
            result[i] = CovertToBinary(coded[i]);
        }
        return result;
    }

    private static string CovertToBinary(byte b)
    {
        string result = "";
        do
        {
            if (b % 2 == 1)
            {
                result += "1";
            }
            else
            {
                result += "0";
            }

            b /= 2;
        } while (b >= 1);

        while (result.Length < 8)
        {
            result += "0";
        }

        string reverse = "";
        for (int i = result.Length - 1; i >= 0; i--)
        {
            reverse += result[i];
        }
        return reverse;
    }

    static void Main(string[] args)
    {
        var des = new DESCryptoServiceProvider();
        byte[] key = new byte[des.Key.Length];
        Console.WriteLine("Podaj klucz:");
        string keyTest = Console.ReadLine();
        key = BitsToBytes(keyTest);

        Console.WriteLine("=================");
        foreach (var item in key)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
        Console.WriteLine("=================");

        int choice = 0;
        char exit = 'n';
        string toCode = "";

        do
        {
            Console.WriteLine();
            Console.WriteLine("Wybierz:");
            Console.WriteLine("1 - zaszyfruj");
            Console.WriteLine("2 - odszyfruj");
            Console.Write("Twój wybór: ");

            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("Podaj słowo do zaszyfrowania:");

                    toCode = Console.ReadLine();

                    byte[] test = Encoding.ASCII.GetBytes(toCode);

                    //byte[] coded = Code(toCode, key, des.IV);
                    byte[] coded = DesEncryptOneBlock(test, key);

                    Console.WriteLine();
                    Console.WriteLine("Zaszyfrowane:");
                    temp = test;
                    foreach (var item in ByteToBits(coded))
                    {
                        Console.Write(item);
                    }
                    //Console.WriteLine(" {0}", coded.Length);
                    break;

                case 2:
                    Console.WriteLine();

                    Console.WriteLine("Podaj słowo do odszyfrowania:");

                    string test2 = Console.ReadLine();
                    byte[] toDecode = new byte[8];
                    toDecode = BitsToBytes(test2);


                    byte[] decoded = DesDecryptOneBlock(toDecode, key);

                    foreach (var item in temp)
                    {
                        Console.Write(item);
                    }
                    break;

                default:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Zakończyć program? t/n");
            exit = char.Parse(Console.ReadLine());
        } while (exit != 't');
    }

    public static byte[] DesDecryptOneBlock(byte[] plainText, byte[] key)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        des.KeySize = 64;
        des.Key = key;
        des.Padding = PaddingMode.None;

        ICryptoTransform ic = des.CreateDecryptor();

        byte[] enc = ic.TransformFinalBlock(plainText, 0, 8);

        return enc;
    }

    public static byte[] DesEncryptOneBlock(byte[] plainText, byte[] key)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        des.KeySize = 64;
        des.Key = key;
        des.Padding = PaddingMode.None;

        ICryptoTransform ic = des.CreateEncryptor();

        byte[] enc = ic.TransformFinalBlock(plainText, 0, 8);

        return enc;
    }
    static byte[] temp = new byte[8];
}