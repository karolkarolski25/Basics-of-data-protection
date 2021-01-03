using System;
using System.Linq;

namespace BBS_Tests
{
    public static class SingleBitTest
    {
        public static (bool result, int value) IsSingleBitTestPassed(string bbsString)
        {
            int oneCount = bbsString.Where(c => c == '1').Count();

            return (oneCount > 9725 && oneCount < 10275, oneCount);
        }
    }
}
