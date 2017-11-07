using System;
using System.Collections.Generic;
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

            throw new IndexOutOfRangeException();
        }

        static void Main(string[] args)
        {
            int keyLength = keyArray.Length;
            string toCode = Console.ReadLine();

            string codedMessage = Code(toCode);
            Console.WriteLine(codedMessage);
        }
    }
}