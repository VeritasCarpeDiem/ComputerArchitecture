using System;
using Xunit;
using ComputerArchitecture;
namespace TestProject1
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(64)]
        [InlineData(32)]
        [InlineData(1)]
        public void TestForPowerof2Work(int num)
        {
            bool myPowerof2 = Program.IsPowerof2(num);
            Assert.True(myPowerof2);

            bool expected= (int)Math.Log2(num) == Math.Log2(num);
        }

        [Theory]
        [InlineData(65)]
        [InlineData(33)]
        [InlineData(0)]
        public void TestForPowerof2ReturnsFalse(int num)
        {
            bool myPowerof2 = Program.IsPowerof2(num);
            Assert.False(myPowerof2);
        }

        [Theory]
        [InlineData(18,16)]
        [InlineData(34,32)]
        [InlineData(5,4)]
        public void TestForNearestPowerof2(int num, int expected)
        {
            int nearestPowerof2 = Program.NearestPowerof2(num);

            Assert.True(nearestPowerof2 == expected);
        }

        //[Theory]
        //[InlineData(0xDEADBEEF, 3, 0xBE)]
        //[InlineData(0x0BADCAFE, 2, 0xAD)]
        //[InlineData(0xCAFEBABE, 1, 0xCA)]
        //public void GetNthByteReturnsCorrectValue(uint num, int byteToGet, byte expected)
        //{
        //    int numFlipped = Program.FlipABit(num, byteToGet);

        //    Assert.True(numFlipped == expected);
        //}

        
    }
}
