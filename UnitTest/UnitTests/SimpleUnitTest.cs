using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System;

namespace MainUnitTest
{
    [TestClass]
    public class SimpleUnitTest
    {
        [TestMethod]
        public void NumberAddition()
        {
            double a = 1.0000020222;
            int b = 2;
            double expected = 3.0000020222;
            double actual = MathematicalCalculations.Addition(a, b);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NumberSubtraction()
        {
            double a = 5965181515.515611536;
            int b = 26354163;
            double expected = 5938827352.515611536;
            double actual = MathematicalCalculations.Subtraction(a, b);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void NumberMultiplication() 
        {
            double a = 1111111111111111;
            double b = 2;
            double expected = 2222222222222222;
            double actual = MathematicalCalculations.Multiplication(a, b);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void IntegerPartOfDivision()
        {
            int a = 55;
            int b = 3;
            int expected = 18;
            int actual = MathematicalCalculations.IntegerPart(a, b);
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void FractionalPartOfDivision()
        {
            int a = 55;
            int b = 3;
            int expected = 1;
            int actual = MathematicalCalculations.FractionalPart(a, b);
            Assert.AreEqual(expected, actual);
        }
    }
}
