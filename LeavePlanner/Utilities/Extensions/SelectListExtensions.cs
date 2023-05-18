using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeavePlanner.Utilities.Extensions
{
    public static class SelectListExtensions
    {
        /// <summary>
        /// Create select list from List of items
        /// </summary>
        /// <param name="items">list of items for select list</param>
        /// <param name="funcToGetValue"> value for each item of select list</param>
        /// <param name="funcToGetText"> text for each item</param>
        /// <returns>Select list</returns>
        public static List<SelectListItem> ToSelectList<T>(this List<T> items,
            Func<T, object> funcToGetValue, Func<T, object> funcToGetText)
        {
            return items
                .Select(x => new SelectListItem
                {
                    Value = funcToGetValue(x).ToString(),
                    Text = funcToGetText(x).ToString()
                }).ToList();
        }

        /// <summary>
        /// Create select list from List of items
        /// </summary>
        /// <param name="items">list of items for select list</param>
        /// <param name="funcToGetValue"> value for each item of select list</param>
        /// <param name="funcToGetText"> text for each item</param>
        /// <param name="whereClause">Filter select list</param>
        /// <returns>Select list</returns>
        public static List<SelectListItem> ToSelectList<T>(this List<T> items, Func<T, object> funcToGetValue,
            Func<T, object> funcToGetText, Func<T, bool> whereClause)
        {
            return items
                .Where(whereClause)
                .Select(x => new SelectListItem
                {
                    Value = funcToGetValue(x).ToString(),
                    Text = funcToGetText(x).ToString()
                }).ToList();
        }

        /// <summary>
        /// Create select list from List of items
        /// </summary>
        /// <param name="items">list of items for select list</param>
        /// <param name="funcToGetValue"> value for each item of select list</param>
        /// <param name="funcToGetText"> text for each item</param>
        /// <param name="whereClause">Filter select list</param>
        /// <param name="selectedValue">Preselect already selected value</param>
        /// <returns>Select list</returns>
        public static List<SelectListItem> ToSelectList<T>(this List<T> items, Func<T, object> funcToGetValue,
            Func<T, object> funcToGetText, Func<T, bool> whereClause, object selectedValue)
        {
            return items
                .Where(whereClause)
                .Select(x => new SelectListItem
                {
                    Value = funcToGetValue(x).ToString(),
                    Text = funcToGetText(x).ToString(),
                    Selected = selectedValue != null && funcToGetValue(x).ToString() == selectedValue.ToString()
                }).ToList();
        }
    }
}
