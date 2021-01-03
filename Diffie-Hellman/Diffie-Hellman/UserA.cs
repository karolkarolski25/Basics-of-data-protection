using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Diffie_Hellman
{
    public class UserA
    {
        private static Random random = new Random();

        private BigInteger _n;
        private int _g;

        private BigInteger k;

        private int x;
        private BigInteger y;

        public BigInteger Y 
        { 
            get => y;
            set
            {
                y = value;

                k = BigInteger.Pow(value, x) % _n;
            }
        }
        public BigInteger X { get; set; }

        public UserA(int n, int g)
        {
            _n = n;
            _g = g;

            GenerateX();

            X = BigInteger.Pow(_g, x) % _n;
        }

        private void GenerateX()
        {
            x = random.Next(500000, 750000);
        }

        public void PrintData()
        {
            Console.WriteLine();
            Console.WriteLine("User A:");
            Console.WriteLine($"X: {X}");
            Console.WriteLine($"k: {k}");
        }
    }
}
