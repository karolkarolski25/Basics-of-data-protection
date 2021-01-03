using System;
using System.Collections.Generic;
using System.Linq;

namespace BBS_Tests
{
    public static class PokerTest
    {
        private static Dictionary<int, int> decimalSet = new Dictionary<int, int>();

        private static void FillSet()
        {
            for (int i = 0; i < 16; i++)
            {
                decimalSet.Add(i, 0);
            }
        }

        private static IEnumerable<string> Split(this string str, int n)
        {
            if (string.IsNullOrEmpty(str) || n < 1)
            {
                throw new ArgumentException();
            }

            return Enumerable.Range(0, str.Length / n).Select(i => str.Substring(i * n, n));
        }

        private static double CalculateX()
        {
            var sum = decimalSet.Sum(e => Math.Pow(e.Value, 2));

            return ((double)16 / 5000) * sum - 5000;
        }

        public static (bool result, double value) IsPokerTestPassed(string bbsString)
        {
            FillSet();

            IEnumerable<string> segments = bbsString.Split(4);

            foreach (var segment in segments)
            {
                decimalSet[Convert.ToInt32(segment, 2)]++;
            }

            double x = CalculateX();

            decimalSet.Clear();
    
            return (x > 2.16 && x < 46.17, x);
        }
    }
}
