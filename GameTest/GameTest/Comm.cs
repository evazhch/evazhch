using System;

namespace GameTest
{
    class Comm
    {
        public static long Time()
        {
            DateTime dt1 = new DateTime(1970, 1, 1);
            TimeSpan ts = DateTime.Now - dt1;
            return (long)ts.TotalMilliseconds;
        }
    }
}
