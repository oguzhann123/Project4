using System;
using System.Collections.Generic;

namespace ConsoleApp7
{
    internal class Program
    {
        public delegate double Function(double x);
        public static void Main()
        {
            
            double[] coeffs;

            Console.WriteLine("Please write the coefficients of polynomial function separated by spaces:");
            coeffs = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);
            Console.WriteLine("The function you've entered:");
            Console.Write("f(x) = ");
            for (int i = 0; i < coeffs.Length; i++)
            {
                Console.Write($"{coeffs[i]}x^{coeffs.Length - i - 1}");
                if (i < coeffs.Length - 1)
                {
                    Console.Write(" + ");
                }
            }
            Console.WriteLine();

            Console.WriteLine("Please write the boundaries of integration:");
            Console.Write("a = ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("b = ");
            double b = double.Parse(Console.ReadLine());

            Console.WriteLine($"Integration by trapezoidal method: {Trapezoidal(coeffs, a, b, 1000):F8}");
            Console.WriteLine($"Integration by Simpson's method: {Simpson(coeffs, a, b, 1000):F8}");


        }

        public static double EvalPolynomial(double[] coeffs, double x)
        {
            double result = 0;
            for (int i = 0; i < coeffs.Length; i++)
            {
                result += coeffs[i] * Math.Pow(x, coeffs.Length - i - 1);
            }
            return result;
        }

        public static double Trapezoidal(double[] coeffs, double a, double b, int n)
        {
            double h = (b - a) / n;
            double sum = 0;
            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += EvalPolynomial(coeffs, x);
            }
            return h * (0.5 * (EvalPolynomial(coeffs, a) + EvalPolynomial(coeffs, b)) + sum);
        }

        public static double Simpson(double[] coeffs, double a, double b, int n)
        {
            if (n % 2 != 0)
            {
                throw new ArgumentException("n must be even for Simpson's rule");
            }
            double h = (b - a) / n;
            double sum = 0;
            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                if (i % 2 == 0)
                {
                    sum += 2 * EvalPolynomial(coeffs, x);
                }
                else
                {
                    sum += 4 * EvalPolynomial(coeffs, x);
                }
            }
            return h / 3 * (EvalPolynomial(coeffs, a) + EvalPolynomial(coeffs, b) + sum);
        }
    }
}
