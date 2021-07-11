using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace ComputerArchitecture
{
    public class Program
    {
        public static sbyte TwosComplement(byte b)
        {
            if (b < 128)
            {
                return Convert.ToSByte(b);
            }
            else
            {
                int x = Convert.ToInt32(b);
                return Convert.ToSByte(x - 256);
            }
        }

        public unsafe static void Loop(uint data)
        {

            byte* start = (byte*)&data;
            byte* end = start + sizeof(uint);

            while (start < end)
            {
                byte currentByte = *start;
                Console.WriteLine(Convert.ToString(currentByte, 16));
                start++;
            }
        }

        public unsafe static byte SumOfArray(byte* start, byte length) //start pointer is copy of fixed pointer so we can change it
        {
            byte sum = 0;
            byte* end = start + length;

            while (start < end)
            {
                sum += *start;
                start++;
            }
            return sum;
        }
        public unsafe static void LoopThruArray(int[] data)
        {
            //int* meow = data;   
            fixed (int* begin = data)
            {
                int* current = begin;
                int* end = current + data.Length;

                while (current < end)
                {
                    Console.WriteLine(*current);
                    current++;
                }
            }
        }

        public unsafe static bool IsPalindrome(string s) //O(1) solution
        {
            string reversedString = "";

            fixed (char* begin = s)
            {
                char* current = begin;
                char* end = current + s.Length - 1;
                while (end > current)
                {
                    reversedString += *end;
                    end--;
                    if (reversedString == s)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public unsafe static bool IsPalimdromeFast(string s) //O(n/2) solution => assume it's a palindrome
        {
            string reversedString = ""; //example: mom
            fixed (char* begin = s)
            {
                char* start = begin;
                char* right = begin + s.Length - 1;
                while (start < right)
                {
                    if (*start != *right)
                    {
                        return false;
                    }
                    //reversedString += *right;
                    right--;
                    start++;
                }

            }
            return true;
        }
        enum Instructions
        {
            ADD = 1,
            SUB,
            MUL,
            DIV
        }
        public static int DoMath(uint data)
        {
            int result;
            //Instructions instruction = Instructions.ADD; 
            //1st byte is opcode
            //2nd byte is pad
            //3rd byte is R1
            //4th byte is R2

            Instructions instruction = (Instructions)GetNthByte(data, 0);
            switch (instruction)
            {
                case Instructions.ADD:
                    {
                        result = GetNthByte(data, 3) + GetNthByte(data, 4);
                        break;
                    }
                case Instructions.SUB:
                    {
                        result = GetNthByte(data, 4) - GetNthByte(data, 3);
                        break;
                    }
                case Instructions.MUL:
                    {
                        result = GetNthByte(data, 3) * GetNthByte(data, 4);
                        break;
                    }
                case Instructions.DIV:
                    {
                        result = GetNthByte(data, 4) / GetNthByte(data, 3);
                        break;
                    }
                default:
                    throw new InvalidOperationException($"No such instruction: {instruction}");
                    break;
            }

            return result;
        }
        //Bonus: Define sub, multiply, divide, mod in terms of ADD
        public static int Subtract(int num1, int num2) => num1 + (-num2);


        public static int Multiply(int num1, int num2)
        {
            int result = 0;
            for (int i = 0; i < num2; i++)
            {
                result += num1;
            }
            return result;
        }

        public static int Divide(int num1, int num2)
        {
            int modulus = 0;
            int quotient = 0; //this is the counter
            do
            {
                num1 = num1 - num2;
                quotient++;
            } while (num2 < num1);
            modulus = num1;
            if (modulus != 0)
            {
                Console.WriteLine($"Remainder is: {modulus}");
            }
            return quotient;
        }

        public static void CountToTen()
        {
            int counter = 0;
            int amountofTimesToLoop = 10;
        loop:

            if (counter <= amountofTimesToLoop)
            {
                goto end;
            }
            counter++;

            goto loop;

        end:
            return;
        }
        public static byte[] GetAllBytes(uint num)
        {
            byte[] byteArray = new byte[sizeof(int)];

            for (uint i = 0; i < byteArray.Length; i++)
            {

                byteArray[i] = GetNthByte(i, i);
            }
            return byteArray;

        }

        public static byte GetNthByte(uint num, uint byteIndex) //num = 0000 0010 (2) 
                                                                // mask = 0xFF = 255 = 1111 1111 
        {
            int mask = 0xFF;
            int shiftedMask = mask << (int)(byteIndex * 8);
            int AND = (int)num & shiftedMask;

            return (byte)AND;

        }

        public static int FlipABit(uint num, int bitIndex)//num = 1010 (10) bitToFlip: 1 new num = 1000 (8)
        {
            int mask = 1 << bitIndex;

            return mask;
        }

        public static int ReturnBitsFlipped(int num, int bitIndex, bool trueOrFalse)
        {
            //AND -> & 
            //OR -> |
            //XOR -> ^
            //NOT -> ~

            int mask = 1 << bitIndex;
            int or = num | mask;

            if (or == num)
            {
                if (trueOrFalse == true)//this must mean 1 is at this location
                {
                    return num;
                }
                num -= mask;
                return num;
            }

            if (trueOrFalse)
            {
                num |= mask;
            }
            return or;
        }
        public static int NearestPowerof2(int num)
        {

            if (num % 4 == 0) //case where num is a power of 2
            {
                return num;
            }
            int shift = 1;
            while (shift < num)
            {
                if (shift > num)
                {
                    shift >>= 1;
                    return shift;
                }
                shift = shift << 1;
            }
            return shift >> 1;

            //ternary operator example: 
            int a = 5;
            int b = 2;

            bool c = a > b ? true : false;
        }
        public static bool IsPowerof2(int a)//32 bit 
        {
            int b = 1;

            while (b <= a)
            {
                if (a == b)
                {
                    return true;
                }
                b <<= 1; //a = a << 1;
            }
            return false;
        }
        static unsafe void Main(string[] args)
        {
            int a = 5;
            var binaryofA = Convert.ToString(a, 2);
            binaryofA = binaryofA.PadLeft(8, '0');
            Console.WriteLine($"Binary of A {binaryofA}");

            Console.WriteLine(IsPowerof2(4));

            Console.WriteLine(NearestPowerof2(18));

            Console.WriteLine(ReturnBitsFlipped(0x05, 1, true));

            Console.WriteLine(GetNthByte(2, 1));

            Console.WriteLine(Multiply(3, 4));

            Console.WriteLine(Divide(9, 3));

            Console.Write(Divide(5, 2));

            Loop(0xDEADBEEF);

            Console.WriteLine(IsPalimdromeFast("mom"));
            byte[] array = new byte[] { 1, 2, 3, 4 };
            int sumOfArray = 0;
            fixed (byte* startOfArray = array)
            {
                sumOfArray = SumOfArray(startOfArray, (byte)array.Length);
            }

            Console.WriteLine(sumOfArray);

            Regex re = new Regex(@"^ADD +(R\d{1,2}) +(R\d{1,2}) +(R\d{1,2})$");
            var match = re.Match("ADD R1 R2 R3");

            for (int i = 0; i < match.Groups.Count; i++)
            {
                Console.WriteLine(match.Groups[i].ToString());
            }
        }
        #region WaysOfPassingStuff

        struct Cat
        {
            public string Name;
        }

        class Dog
        {
            public string Name;
        }

        static void ValueByValue(Cat cat) { }

        static void ValueByRef(ref Cat cat)//This will change Cat
        {
            cat = new Cat()
            {
                Name = "Goodbye"
            };
        }

        static void RefByValue(Dog dog)
        {
            dog.Name = "Hello";
            dog = new Dog(); //This does nothing
        }

        static void RefByRef(ref Dog dog) { }

        #endregion
    }
}
