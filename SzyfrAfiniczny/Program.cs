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

        private static void Decode()
        {
            Console.WriteLine("Enter TExt to decode");
            string text = Console.ReadLine().ToUpper();
            //string text = "XZI";
            Console.WriteLine("Enter KEYS");
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            //int a = 7;
            //int b = 5;
            string result = "";

            foreach (var textSign in text)
            {
                if (helper.Contains(textSign))
                {
                    int letterIndex = GetIndex(textSign);
                    var d = ((15 * (letterIndex - b)) % helper.Length);
                    result += helper[(int) d];
                }

                else
                {
                    result += textSign;
                }
            }
            Console.WriteLine(result);
        }

        private static void Code()
        {
            Console.WriteLine("Enter Text:");
            string text = Console.ReadLine().ToUpper();
            Console.WriteLine("Enter KEYS:");

            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());

            string result = "";

            foreach (var textSign in text)
            {
                if (helper.Contains(textSign))
                {
                    int index = GetIndex(textSign) * a + b % helper.Length;
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
    }
}