using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class MathematicalCalculations
    {
        public static double Addition(double a, double b)
        {
            return a + b;
        }
        public static double Subtraction(double a, double b)
        {
            return a - b;
        }
        public static double Multiplication(double a, double b)
        {
            return a * b;
        }
        public static int IntegerPart(double a, double b)
        {
            return Convert.ToInt32(a / b);
        }
        public static int FractionalPart(double a, double b)
        {
            return Convert.ToInt32(a % b);
        }
    }
}
