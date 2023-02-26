using System;

namespace WeldScanApp
{
    internal class RandomHelper
    {
        private static Random _rand = new Random();
        public static int Roll(int min, int max)
        {
            return _rand.Next(min, max);
        }
    }
}
