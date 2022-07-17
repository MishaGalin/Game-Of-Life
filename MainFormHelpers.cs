using System;

namespace Game_Of_Life
{
    internal static class MainFormHelpers
    {

        private static bool CloseEnough(double d1, double d2, double maxDifference = 0.001)
        {
            return Math.Abs(d1 - d2) < maxDifference;
        }
    }
}