
namespace LeavePlanner.Utilities.Extensions
{
    public static class ConvertToIntegerExtension
    {
        /// <summary>
        /// Used for converting string to integer 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Integer number</returns>
        public static int ConvertToInteger(this string data)
        {
            var integerNumber = int.Parse(data);
            return integerNumber;
        }

        /// <summary>
        /// Used for converting string to integer 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Nullable integer number</returns>
        public static int? ConvertToNullableInteger(this string data)
        {
            int? integerNumber = null;


            if (!string.IsNullOrWhiteSpace(data))
            {
                integerNumber = int.Parse(data);
            }

            return integerNumber;
        }
    }
}
