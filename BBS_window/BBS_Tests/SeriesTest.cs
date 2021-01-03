using System.Collections.Generic;
using System.Linq;

namespace BBS_Tests
{
    public static class SeriesTest
    {
        private static Dictionary<int, int> seriesSet = new Dictionary<int, int>();

        private static void FillSeries()
        {
            if (seriesSet.Any())
            {
                seriesSet.Clear();
            }

            for (int i = 1; i <= 6; i++)
            {
                seriesSet.Add(i, 0);
            }
        }

        private static bool IsInRange(this int value, int from, int to)
        {
            return value >= from && value <= to;
        }

        public static (bool result, Dictionary<int, int> value) IsSeriesTestPassed(string bbsString)
        {
            FillSeries();

            string series = string.Empty;

            foreach (var letter in bbsString)
            {
                if (letter != '0')
                {
                    series += letter;
                }
                else if (!string.IsNullOrEmpty(series))
                {
                    seriesSet[series.Length >= 6 ? 6 : series.Length]++;
                    series = string.Empty;
                }
            }

            bool[] summary = new bool[6];

            summary[0] = seriesSet[1].IsInRange(2315, 2685);
            summary[1] = seriesSet[2].IsInRange(1114, 1386);
            summary[2] = seriesSet[3].IsInRange(527, 723);
            summary[3] = seriesSet[4].IsInRange(240, 384);
            summary[4] = seriesSet[5].IsInRange(103, 209);
            summary[5] = seriesSet[6].IsInRange(103, 209);

            return (!summary.Any(s => !s), seriesSet);
        }
    }
}