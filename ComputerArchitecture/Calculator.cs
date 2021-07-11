using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerArchitecture
{
    public class Calculator
    {
        public Stack<byte> stack = new Stack<byte>();
        
        public void Add()
        {
            byte num1= stack.Pop();
            byte num2 = stack.Pop();
            
            stack.Push((byte)(num1 + num2));
        }

        public void Subtract()//twos complements
        {
            byte num1 = stack.Pop();
            byte num2 = stack.Pop();

            stack.Push(num1);
            stack.Push((byte)Program.TwosComplement(num2));
            Add();
        }

        public void Multiply()
        {
            byte num1 = stack.Pop();
            byte num2 = stack.Pop();
            byte result = num1;
            for (int i = 0; i < num2-1; i++) //3*4
            {
                stack.Push((byte)(result + num1));
                result = stack.Pop();
            }   
        }

        public void Divide()
        {
            int num1 = stack.Pop();
            int num2 = stack.Pop();
            
            for (int i = 0; i < num2; i++)
            {
                
            }    
        }

    }
}
