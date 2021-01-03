using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Diffie_Hellman
{
    public class UserB
    {
        private static Random random = new Random();

        private int _n;
        private int _g;

        private BigInteger k;

        private int y;
        private BigInteger x;

        public BigInteger X
        {
            get => x;
            set
            {
                x = value;

                k = BigInteger.Pow(value, y) % _n;
            }
        }
        public BigInteger Y { get; set; }

        public UserB(int n, int g)
        {
            _n = n;
            _g = g;

            GenerateY();

            Y = BigInteger.Pow(_g, y) % n;
        }

        private void GenerateY()
        {
            y = random.Next(500000, 750000);
        }

        public void PrintData()
        {
            Console.WriteLine();
            Console.WriteLine("User B:");
            Console.WriteLine($"Y: {Y}");
            Console.WriteLine($"k: {k}");
        }
    }
}
