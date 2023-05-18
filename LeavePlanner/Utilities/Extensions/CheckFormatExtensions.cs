using System.Globalization;

namespace LeavePlanner.Utilities.Extensions
{
    public static class CheckFormatExtensions
    {
        public static bool IsOib(this string oib)
        {
            if (oib.Length != 11)
                return false;

            // long b;
            // if (!long.TryParse(oib, out b)) return false;
            if (!long.TryParse(oib, out _))
                return false;

            int a = 10;
            for (int i = 0; i < 10; i++)
            {
                a += Convert.ToInt32(oib.Substring(i, 1));
                a %= 10;
                if (a == 0)
                    a = 10;
                a *= 2;
                a %= 11;
            }
            int kontrolni = 11 - a;
            if (kontrolni == 10)
                kontrolni = 0;

            return kontrolni == Convert.ToInt32(oib.Substring(10, 1));
        }

        public static bool IsInteger(this string textNumber)
        {
            bool isInteger = int.TryParse(textNumber, out _);

            return isInteger;
        }

        public static bool IsDecimal(this string textNumber)
        {
            bool isDecimal = decimal.TryParse(textNumber, out _);

            return isDecimal;
        }

        public static bool IsDate(this string text)
        {
            string[] formats = new[] { "dd.MM.yyyy", "dd.MM.yyyy." };
            var isDate = DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _);

            return isDate;
        }
    }
}
