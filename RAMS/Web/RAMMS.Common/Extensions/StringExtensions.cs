using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsInt(this string stringValue)
        {
            return int.TryParse(stringValue, out _);
        }

        public static int AsInt(this string stringValue)
        {
            int.TryParse(stringValue, out int num);
            return num;
        }
        public static bool IsDouble(this string stringValue)
        {
            return double.TryParse(stringValue, out _);
        }

        public static double AsDouble(this string stringValue)
        {
            double.TryParse(stringValue, out double num);
            return num;
        }
    }
}
