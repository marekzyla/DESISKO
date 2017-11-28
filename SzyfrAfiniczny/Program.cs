using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SzyfrAfiniczny
{
    class Program
    {
        //private static char[] helper = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPRSŚTUWYZŹŻ".ToCharArray();
        private static char[] helper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        static void Main(string[] args)
        {

            char choice;
            do
            {
                Console.WriteLine("\n1: Code \n2: Decode\nELSE: exit\n");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine("\n\n\n");
                switch (choice)
                {
                    case '1':
                        Code();
                        break;
                    case '2':
                        Decode();
                        break;
                }
            } while (choice == '1' || choice == '2');
        }

        static (int a, int b) EnterKeys()
        {
            Console.WriteLine("Enter KEYS");
            int a;
            Console.WriteLine("ENTER [a] ->");
            do
            {
                a = int.Parse(Console.ReadLine());
                if (!IsRelativelyPrime(a, helper.Length))
                {
                    Console.WriteLine(
                        "NOT AWAIABLE SINCE IT HAS MORE THAN ONE SHARED DIVIDERS WITH {0} (alphabet size)",
                        helper.Length);
                }
            } while (!IsRelativelyPrime(a, helper.Length));
            Console.WriteLine("ENTER [b] ->");
            int b = int.Parse(Console.ReadLine());
            return (a, b);
        }

        private static void Decode()
        {
            Console.WriteLine("Enter TExt to decode");
            string text = Console.ReadLine().ToUpper();
           
            var keys = EnterKeys();
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (helper.Contains(text[i]))
                {
                    int indexI = GetIndex(text[i]);
                    int letterIndex = GetIndexToDecode(keys.a);
                    if (indexI-keys.b<0)

                    {
                        indexI += helper.Length;
                    }
                    var d = ((letterIndex * (indexI- keys.b)) % helper.Length);
                    result += helper[d]; 
                }

                else
                {
                    result += text[i];
                }
            }
            Console.WriteLine(result);
        }

        private static void Code()
        {
            Console.WriteLine("Enter Text:");
            string text = Console.ReadLine().ToUpper();
            var keys = EnterKeys();

            string result = "";

            foreach (var textSign in text)
            {
                if (helper.Contains(textSign))
                {
                    int index = GetIndex(textSign) * keys.a + keys.b % helper.Length;
                    result += helper[index % helper.Length];
                }
                else
                {
                    result += textSign;
                }
            }
            Console.WriteLine(result);
        }

        static int GetIndex(char sign)
        {
            for (int i = 0; i < helper.Length; i++)
            {
                if (sign == helper[i])
                {
                    return i;
                }
            }
            throw new Exception("ENCODING ERROR, Try using only Polish letters");
        }

        static int GetIndexToDecode(int a)
        {
            for (int i = 0; i < helper.Length; i++)
            {
                if ((a * i) % helper.Length == 1)
                {
                    return i;
                }
            }
            throw new Exception("Decode ERROR");
        }

        static bool IsRelativelyPrime(int a, int b)
        {
            for (int i = 2; i <= Math.Min(a, b); i++)
            {
                if (a % i == 0 && b % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}