using System.Text.RegularExpressions;

namespace BBS_Tests
{
    public static class LongSeriesTest
    {
        public static (bool result, int value) IsLongSeriesTestPassed(string bbsString)
        {
            int longSeriesCount = Regex.Matches(bbsString, new string('1', 26)).Count + 
                Regex.Matches(bbsString, new string('0', 26)).Count;

            return (longSeriesCount < 1, longSeriesCount);
        }
    }
}