using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzyfrCezara
{
    class Program
    {
        static char[] keyArray = "AĄBCĆDEĘFGHIJKLŁMNŃOÓPRSŚTUWYZŹŻ".ToCharArray();
        

        static string Code(string message, int flow=3)
        {
            if (flow>=keyArray.Length)
            {
            flow = flow / keyArray.Length;

            }
            string result = "";
            foreach (var sign in message.ToUpper())
            {
                if (keyArray.Contains(sign))
                {

                    int index = GetIndex(sign) + flow;
                    if (index < keyArray.Length)
                    {
                        result += keyArray[index];
                    }
                    else
                    {
                        result += keyArray[index - keyArray.Length];
                    } 
                }
                else
                {
                    result += sign;
                }
            }
            return result;
        }

        static string Decode(string message,int flow=3)
        {
            if (flow >= keyArray.Length)
            {
                flow = flow / keyArray.Length;

            }

            string result = "";
            foreach (var sign in message.ToUpper())
            {
                if (keyArray.Contains(sign))
                {
                    int index = GetIndex(sign) - flow;

                    if (index < 0)

                    {
                        index = keyArray.Length + index;
                    }
                    if (index < keyArray.Length)
                    {
                        result += keyArray[index];
                    }
                    else
                    {
                        result += keyArray[index - keyArray.Length];
                    } 
                }
                else
                {
                    result += sign;
                }
            }
            return result;
        }


        private static int GetIndex(char sign)
        {
            for (int i = 0; i < keyArray.Length; i++)
            {
                if (keyArray[i] == sign)
                {
                    return i;
                }
            }

            throw new IndexOutOfRangeException("EEEE");
        }

        static void Main(string[] args)
        {
            int keyLength = keyArray.Length;
            Console.WriteLine("Wprowadź tekst");
            string toCode = Console.ReadLine();
            Console.WriteLine("Zaszyfrowane");
            string codedMessage = Code(toCode,3);
            Console.WriteLine(codedMessage);
            Console.WriteLine("Odszyfrowany ");
            string decoded = Decode(codedMessage,3);
            Console.WriteLine(decoded);
        }
    }
}