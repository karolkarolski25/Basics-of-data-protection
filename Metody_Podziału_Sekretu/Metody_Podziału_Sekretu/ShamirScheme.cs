using System;
using System.Collections.Generic;
using System.Linq;

namespace Metody_Podziału_Sekretu
{
    public class ShamirScheme
    {
        private static readonly Random random = new Random();

        int _s;
        int _p;
        int _t;
        int _n;

        private List<(int, int)> pary = new List<(int, int)>();

        List<int> aList = new List<int>();

        public ShamirScheme(int s, int t, int n, int p)
        {
            _s = s;
            _t = t;
            _n = n;
            _p = p;
        }

        public int RecoverSecret(List<int> podzial)
        {
            double wartosc = 0, licznik = 1, mianownik = 1, wynikDzielenia = 1;

            for (int i = 1; i <= _t; i++)
            {
                pary.Add((i + 1, podzial[i]));
            }

            for (int i = 0; i < _t; i++)
            {
                licznik = 1;
                mianownik = 1;
                wynikDzielenia = 1;

                for (int j = 0; j < _t; j++)
                {
                    if (i != j) 
                    {
                        licznik *= -(pary[j].Item1);
                        mianownik *= (pary[i].Item1 - pary[j].Item1);
                    }
                }

                wynikDzielenia = licznik / mianownik;
                wartosc += mod(wynikDzielenia * pary[i].Item2, _p);

                Console.WriteLine($"Pary: ({pary[i].Item1}, {pary[i].Item2})");
            }

            return (int)(mod(wartosc, _p));
        }

        public List<int> DivideSecret()
        {
            //_p = GeneratePrimeNumber(random.Next(10000000, 25000000));

            //Console.WriteLine($"Random p: {_p}");

            //_s = random.Next(0, _p - 1);

            for (int i = 0; i < _t - 1; i++)
            {
                aList.Add(random.Next(0, 1000));
            }

            List<int> podzial = new List<int>(Enumerable.Repeat(_s, _n));

            for (int i = 0; i < aList.Count; i++)
            {
                Console.WriteLine($"a{i + 1} = {aList.ElementAt(i)}");
            }

            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _t - 1; j++)
                {
                    podzial[i] += aList[j] * (int)Math.Pow(i + 1, j + 1);
                }
                
                podzial[i] = ((int)mod(podzial[i], _p));

                Console.WriteLine($"f({i + 1}) = {podzial[i]}");
            }

            return podzial;
        }

        static double mod(double x, double m)
        {
            double r = x - Math.Floor(x / m) * m;
            return r;
        }

        private static bool IsPrime(int n)
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

        private static int GeneratePrimeNumber(int n)
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
    }
}
