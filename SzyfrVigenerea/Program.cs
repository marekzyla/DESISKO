using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SzyfrVigenerea
{
    public struct Index
    {
        public int X { get; set; }
        public int Y { get; set; }

    }

    class Program
    {
        static char[,] keys =
        {
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z'
            },
            {
                'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
                'W', 'X', 'Y', 'Z', 'A'
            },
            {
                'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W',
                'X', 'Y', 'Z', 'A', 'B'
            },
            {
                'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z', 'A', 'B', 'C'
            },
            {
                'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y',
                'Z', 'A', 'B', 'C', 'D'
            },
            {
                'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
                'A', 'B', 'C', 'D', 'E'
            },
            {
                'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A',
                'B', 'C', 'D', 'E', 'F'
            },
            {
                'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B',
                'C', 'D', 'E', 'F', 'G'
            },
            {
                'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C',
                'D', 'E', 'F', 'G', 'H'
            },
            {
                'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D',
                'E', 'F', 'G', 'H', 'I'
            },
            {
                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E',
                'F', 'G', 'H', 'I', 'J'
            },
            {
                'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F',
                'G', 'H', 'I', 'J', 'K'
            },
            {
                'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
                'H', 'I', 'J', 'K', 'L'
            },
            {
                'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H',
                'I', 'J', 'K', 'L', 'M'
            },
            {
                'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
                'J', 'K', 'L', 'M', 'N'
            },
            {
                'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                'K', 'L', 'M', 'N', 'O'
            },
            {
                'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K',
                'L', 'M', 'N', 'O', 'P'
            },
            {
                'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
                'M', 'N', 'O', 'P', 'Q'
            },
            {
                'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
                'N', 'O', 'P', 'Q', 'R'
            },
            {
                'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
                'O', 'P', 'Q', 'R', 'S'
            },
            {
                'U', 'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
                'P', 'Q', 'R', 'S', 'T'
            },
            {
                'V', 'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
                'Q', 'R', 'S', 'T', 'U'
            },
            {
                'W', 'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                'R', 'S', 'T', 'U', 'V'
            },
            {
                'X', 'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
                'S', 'T', 'U', 'V', 'W'
            },
            {
                'Y', 'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                'T', 'U', 'V', 'W', 'X'
            },
            {
                'Z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
                'U', 'V', 'W', 'X', 'Y'
            }
        };

        static char[] helperArray =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z'
        };

        static string Code(string message, string key)
        {
            string result = "";
            int keyIndex = 0;
            message = message.ToUpper();
            key = key.ToUpper();
            //todo Add++
            foreach (var msgSign in message)
            {
                if (helperArray.Contains(msgSign))
                {
                    var index = GetIndex(msgSign, key[keyIndex]);
                    result += keys[index.Y, index.X];

                    keyIndex++;

                    if (keyIndex >= key.Length)
                    {
                        keyIndex = 0;
                    }
                }
                else
                {
                    result += msgSign;
                }
            }
            return result;
        }

        static string Decode(string coded, string key)
        {
            string result = "";
            int keyIndex = 0;
            coded = coded.ToUpper();
           key= key.ToUpper();
            if (true)
            {

                foreach (var codedSign in coded)
                {
                    if (helperArray.Contains(codedSign))
                    {
                        var index = GetIndex(codedSign, key[keyIndex]);
                        result += helperArray[FindCodedIndex(codedSign, key[keyIndex])];

                        keyIndex++;

                        if (keyIndex >= key.Length)
                        {
                            keyIndex = 0;
                        } 
                    }
                    else
                    {
                        result += codedSign;
                    }
                } 
            }

            return result;
        }

        static int FindCodedIndex(char codedSign, char keySign)
        {

            int result = 0;
            int keyIndex = 0;
            for (int i = 0; i < helperArray.Length; i++)
            {
                if (helperArray[i] == keySign)
                {
                    keyIndex = i;
                    break;
                }
            }
            for (int i = 0; i < helperArray.Length; i++)
            {
                if (codedSign == keys[keyIndex, i])
                {
                    result = i;
                }
            }

            return result;

        }
        //TODO ZROB Z TYM COS 
        static Index GetIndex(char msgSign, char keySign)
        {

            var result = new Index();
            for (int i = 0; i < helperArray.Length; i++)
            {
                if (helperArray[i] == msgSign)
                {
                    result.X = i;
                }
            }
            for (int i = 0; i < helperArray.Length; i++)
            {
                if (helperArray[i] == keySign)
                {
                    result.Y = i;
                }
            }

            return result;



        }
        static void Main(string[] args)
        {
            int choice = 0;
            string message = "";
            string key = "";
            do
            {
                Console.WriteLine("1: code ,  2: decode , else EXIT");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter Text");
                        message = Console.ReadLine();
                        Console.WriteLine("Enter Key");
                        key = Console.ReadLine();
                        string coded = Code(message, key);
                        Console.WriteLine($"Coded {coded}");
                        break;
                    case 2:
                        Console.WriteLine("Enter Text");
                        message = Console.ReadLine();
                        Console.WriteLine("Enter Key");
                        key = Console.ReadLine();
                        string decoded = Decode(message, key);
                        Console.WriteLine($"Decoded {decoded}");
                        break;
                    
                }
            } while (choice != 3);



        }
    }
}



