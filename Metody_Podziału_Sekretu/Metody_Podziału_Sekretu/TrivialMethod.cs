using System;
using System.Collections.Generic;
using System.Linq;

namespace Metody_Podziału_Sekretu
{
    public class TrivialMethod
    {
        private static readonly Random random = new Random();

        private static int _n, _k, _s;

        public TrivialMethod(int n, int k)
        {
            _n = n;
            _k = k;
        }

        private static int mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        public List<int> DivideSecret()
        {
            List<int> sList = new List<int>();

            for (int i = 0; i < _n - 1; i++)
            {
                sList.Add(random.Next(0, _k - 1));
            }

            _s = random.Next(0, _k - 1);

            Console.WriteLine($"Wylosowane s: {_s}");

            for (int i = 0; i < _n - 1; i++)
            {
                _s -= sList[i];
            }

            _s = mod(_s, _k);

            sList.Add(_s);

            return sList;
        }

        public int RecoverSecret(List<int> sList)
        {
            return mod(sList.Sum(), _k);
        }
    }
}
