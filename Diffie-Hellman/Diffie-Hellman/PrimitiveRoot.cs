using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diffie_Hellman
{
    public static class PrimitiveRoot
    {
        static int Power(int x, int y, int p)
        {
            int res = 1;

            x = x % p;

            while (y > 0)
            {
                if (y % 2 == 1)
                {
                    res = (res * x) % p;
                }

                y = y >> 1;
                x = (x * x) % p;
            }
            return res;
        }

        static void FindPrimefactors(HashSet<int> s, int n)
        {
            while (n % 2 == 0)
            {
                s.Add(2);
                n = n / 2;
            }

            for (int i = 3; i <= Math.Sqrt(n); i = i + 2)
            {
                while (n % i == 0)
                {
                    s.Add(i);
                    n = n / i;
                }
            }


            if (n > 2)
            {
                s.Add(n);
            }

        }

        public static int FindPrimitive(int n)
        {
            HashSet<int> s = new HashSet<int>();

            int phi = n - 1;

            FindPrimefactors(s, phi);

            var d = s.OrderByDescending(prim => prim);

            for (int r = phi - 1; r >= 2; r--)
            {
                bool flag = false;

                foreach (int a in d)
                {
                    if (Power(r, phi / (a), n) == 1)
                    {
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    return r;
                }
            }
            return -1;
        }
    }
}
