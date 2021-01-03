namespace RSA_Cipher
{
    public static class NumberGenerator
    {
        private static ulong NWD(ulong a, ulong b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }

            return a;
        }

        private static bool IsPrime(ulong n)
        {
            if (n < 2)
            {
                return false;
            }

            for (ulong i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static ulong GeneratePrimeNumber(ulong n)
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

        public static ulong GenerateRelativelyPrimeNumber(ulong phi)
        {
            ulong x = 2;

            while (true)
            {
                if (NWD(x, phi) == 1 && IsPrime(x))
                {
                    return x;
                }

                x++;
            }
        }

        public static ulong GenerateDNumber(ulong e, ulong phi)
        {
            ulong d = 2;

            while ((e * d - 1) % phi != 0)
            {        
                d++;
            }

            return d;
        }
    }
}
