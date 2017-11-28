using System;
using System.CodeDom;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

class Des
{
    static byte[] BitsToBytes(string[] coded)
    {
        var result = new byte[8];
        for (int i = 0; i < coded.Length; i++)
        {
            result[i] += Reconvert(coded[i]);
        }
        return result;

    }

    private static byte Reconvert(string s)
    {
        string reverse = "";
        for (int i = s.Length-1; i >= 0; i-- )
        {
            reverse += s[i];

        }
        s = reverse;
        byte result = 0;
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i]=='1')
            {
                result +=(byte) Math.Pow(2, i);
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
        } while (b>=1);

        while (result.Length<8)
        {
            result += "0";
        }

        string reverse = "";
        for (int i = result.Length-1; i >=0; i--)
        {
            reverse += result[i];
        }
        return reverse;
    }

    static void Main(string[] args)
    {

        ////TEST
        //var e = new byte[] { 2, 4, 5, 60 ,200,254,0,7};
        //foreach (var item in ByteToBits(e))
        //{
        //    Console.WriteLine(item);
        //}
        //Console.ReadLine();

        //Console.WriteLine();
        //foreach (var item in BitsToBytes(ByteToBits(e))
        //)
        //{
        //    Console.Write($"{item} ");
        //}
        //Console.ReadLine();


        var des = new DESCryptoServiceProvider();
        byte[] key = new byte[des.Key.Length];
        Console.WriteLine("Podaj klucz:");
        string keyTest = Console.ReadLine();
        key = ConvertToByte(keyTest);

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

                    foreach (var item in ByteToBits(coded))
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine("+++ {0}", coded.Length);
                    break;

                case 2:
                    Console.WriteLine();
                    Console.WriteLine("Podaj słowo do odszyfrowania:");

                    byte[] toDecode = new byte[des.LegalBlockSizes.Length];
                    for (int i = 0; i < des.LegalBlockSizes.Length; i++)
                    {
                        toDecode[i] = byte.Parse(Console.ReadKey().KeyChar.ToString());
                    }

                    string decoded = Decode(toDecode, key, des.IV);

                    Console.WriteLine();
                    Console.WriteLine("Odszyfrowane");
                    Console.WriteLine(decoded);
                    break;

                default:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Zakończyć program? t/n");
            exit = char.Parse(Console.ReadLine());
        } while (exit != 't');
    }

    private static byte[] ConvertToByte(string keyTest)
    {
        int x = 63;
        int temp = 0;
        byte[] key = new byte[8];

        for (int i = 0; i < 8; i++)
        {
            temp = 0;
            for (int j = 0; j < 8; j++)
            {
                if (keyTest[x - j] == '1')
                {
                    temp += int.Parse(Math.Pow(2, j).ToString());
                }
            }
            x -= 8;
            key[i] = byte.Parse(temp.ToString());
        }

        return key;
    }


    static string Decode(byte[] cipherText, byte[] Key, byte[] IV)
    {
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("Key");

        string decoded = null;

        using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
        {
            des.Key = Key;
            des.IV = IV;

            ICryptoTransform decryptor = des.CreateDecryptor(des.Key, des.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        decoded = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return decoded;
    }

    public static byte[] DesEncryptOneBlock(byte[] plainText, byte[] key)
    {
        // Create a new DES key.
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        // Set the KeySize = 64 for 56-bit DES encryption.
        // The msb of each byte is a parity bit, so the key length is actually 56 bits.
        des.KeySize = 64;
        des.Key = key;
        des.Mode = CipherMode.ECB;
        des.Padding = PaddingMode.None;

        ICryptoTransform ic = des.CreateEncryptor();

        byte[] enc = ic.TransformFinalBlock(plainText, 0, 8);

        return enc;
    }
}