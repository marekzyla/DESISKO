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

        private static int flow = 3;

        static string Code(string message)
        {
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

        static string Decode(string message)
        {
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
            string toCode = Console.ReadLine();

            string codedMessage = Code(toCode);
            Console.WriteLine(codedMessage);
            string decoded = Decode(codedMessage);
            Console.WriteLine(decoded);
        }
    }
}