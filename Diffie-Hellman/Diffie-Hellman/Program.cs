using System;
using System.Collections.Generic;
using System.Linq;

namespace Diffie_Hellman
{
    class Program
    {
        private static Random random = new Random();

        static bool IsPrime(int n)
        {
            if (n < 2)
            {
                return false;
            }

            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        static int GeneratePrimeNumber(int n)
        {
            if (IsPrime(n))
            {
                return n;
            }

            while (!IsPrime(n))
            {
                n++;
            }

            return n;
        }

        static void Main(string[] args)
        {
            int n = GeneratePrimeNumber(random.Next(1000000, 2500000));

            int g = PrimitiveRoot.FindPrimitive(n);

            Console.WriteLine($"n: {n}");
            Console.WriteLine($"g: {g}");

            var userA = new UserA(n, g);
            var userB = new UserB(n, g);

            userA.Y = userB.Y;
            userB.X = userA.X;

            userA.PrintData();

            userB.PrintData();
        }
    }
}
