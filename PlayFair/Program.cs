using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PlayFair
{
    class Key
    {
        private string _key;

        public Key()
        {
            _key = GetFixedKey();
        }

        private string DeleteRepetitiveLetters(string input, char letter)
        {
            var result = "";
            if (input.Count(x => x == letter) > 1)
            {
                bool isFirstTime = true;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] != letter)
                    {
                        result += input[i];
                    }
                    else if (isFirstTime)
                    {
                        isFirstTime = false;
                        result += input[i];
                    }
                }
                return result;
            }
            return input;
        }

        public string GetFixedKey()
        {
            Console.WriteLine("Podaj klucz");
            string key = Console.ReadLine().ToUpper().Replace(" ", "");
            foreach (var c in key)
            {
                key = DeleteRepetitiveLetters(key, c);
            }
            return key;
        }

        public override string ToString()
        {
            return _key;
        }
    }

    class CodeArray
    {
        public Key Key { get; private set; }

        public CodeArray(Key key)
        {
            Key = key;
        }


        private bool Contains(char[,] array, char letter)
        {
            if (letter == 'A')
            {
            }
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == letter)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private char[,] GetKeyArray(string key)
        {
            var helper = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToList();
            var result = new char[5, 5];
            int x = 0;
            int y = 0;

            foreach (var c in key)
            {
                if (x >= 5)
                {
                    x = 0;
                    y++;
                }
                result[y, x] = c;
                if (x < 5)
                {
                    x++;
                }
            }

            int index = 0;

            for (int i = y; i < 5; i++)
            {
                for (int j = x; j < 5; j++)
                {
                    bool insertedLetter = false;

                    do
                    {
                        if (!Contains(result, helper[index]))
                        {
                            result[i, j] = helper[index];
                            insertedLetter = true;
                        }
                        else
                        {
                            index++;
                        }
                    } while (!insertedLetter);
                    //result[i, j] = helper[index];


                    index++;
                }
                x = 0;
            }

            return result;
        }

        public void PrintArray(char[,] array)
        {
            Console.WriteLine("\n\n------------------");
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(" {0} ", array[i, j]);
                }
                Console.WriteLine();
            }
        }

        public char[,] GetCodeArray()
        {
            string key = Key.ToString();
            return GetKeyArray(key);
        }
    }

    enum Direction
    {
        Left,
        Right,
        Down,
        Up
    }

    class Coder
    {
        private string _key;
        private char[,] _codeArray;
        private string _text;
        private char[] _helper;
        private List<string> _splitted;
        Random rnd = new Random();

        public void Generate(Key key)
        {
            var codeArray = new CodeArray(key);
            _helper = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToCharArray();
            Console.WriteLine("Wprowadź tekst jawny, bądź zaszyfrowany");
            _text = Console.ReadLine().ToUpper().Replace(" ", "");
            _key = key.ToString();
            _codeArray = codeArray.GetCodeArray();
            _splitted = SplitText();
        }

        private char GetRandomLetter(char existing)
        {
            char proposal;
            do
            {
                proposal = _helper[rnd.Next(_helper.Length)];
            } while (proposal == existing);
            return proposal;
        }

        private List<string> SplitText()
        {
            var splitted = new List<string>();
            int i = 0;
            do
            {
                if (i == _text.Length - 1)
                {
                    splitted.Add($"{_text[i]}{GetRandomLetter(_text[i])}");
                    break;
                }

                if (_text[i] == _text[i + 1])
                {
                    splitted.Add($"{_text[i]}{GetRandomLetter(_text[i])}");
                    i++;
                }
                else
                {
                    splitted.Add($"{_text[i]}{_text[i + 1]}");
                    i += 2;
                }
            } while (i < _text.Length);
            foreach (var item in splitted)
            {
                Console.Write("{0}", item);
            }

            Console.WriteLine();
            return splitted;
        }

        private (int x, int y) GetIndex(char c)
        {
            for (int y = 0; y < _codeArray.GetLength(0); y++)
            {
                for (int x = 0; x < _codeArray.GetLength(1); x++)
                {
                    if (c == _codeArray[y, x])
                    {
                        return (x, y);
                    }
                }
            }
            throw new Exception("Cannot find that sign. Probably using wrong alphabet");
        }


        public int MoveTo(int current, Direction direction)
        {
            if (direction == Direction.Down || direction == Direction.Right)
            {
                if (current == _codeArray.GetLength(0) - 1)
                {
                    return 0;
                }
                return current + 1;
            }
            else if (current == 0)
            {
                return _codeArray.GetLength(0) - 1;
            }
            return current - 1;
        }


        public string Coded()
        {
            string result = "";
            foreach (var element in _splitted)
            {
                var a = GetIndex(element[0]);
                var b = GetIndex(element[1]);

                if (a.x == b.x)
                {
                    a.y = MoveTo(a.y, Direction.Down);
                    b.y = MoveTo(b.y, Direction.Down);
                }
                else if (a.y == b.y)
                {
                    a.x = MoveTo(a.x, Direction.Right);
                    b.x = MoveTo(b.x, Direction.Right);
                }
                else
                {
                    int tempX = a.x;
                    a.x = b.x;
                    b.x = tempX;
                }
                result += $"{_codeArray[a.y, a.x]}{_codeArray[b.y, b.x]}";
            }
            return result;
        }

        public string Decoded()
        {
            string result = "";
            foreach (var element in _splitted)
            {
                var a = GetIndex(element[0]);
                var b = GetIndex(element[1]);

                if (a.x == b.x)
                {
                    a.y = MoveTo(a.y, Direction.Up);
                    b.y = MoveTo(b.y, Direction.Up);
                }
                else if (a.y == b.y)
                {
                    a.x = MoveTo(a.x, Direction.Left);
                    b.x = MoveTo(b.x, Direction.Left);
                }
                else
                {
                    int tempX = a.x;
                    a.x = b.x;
                    b.x = tempX;
                }
                result += $"{_codeArray[a.y, a.x]}{_codeArray[b.y, b.x]}";
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var coder = new Coder();
            char choice = ' ';
            do
            {
                Console.WriteLine("C-Code \\ D-Decode \\ etc-EXIT");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (choice)
                {
                    case 'c':
                        coder.Generate(new Key());
                        Console.WriteLine(coder.Coded());
                        break;
                    case 'd':
                        coder.Generate(new Key());
                        Console.WriteLine(coder.Decoded());
                        break;
                }
            } while (choice == 'c' || choice == 'd');
        }
    }
}