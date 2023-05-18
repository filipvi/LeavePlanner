using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LeavePlanner.Utilities.Extensions
{
    public static class RemoveFromModelStateExtension
    {
        /// <summary>
        /// Remove all entries where a key starts with a given value
        /// This will remove list entries
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="startsWith"></param>
        public static void Remove_StartsWith(this ModelStateDictionary dict, string startsWith)
        {
            foreach (string key in dict.Keys.Where(k => k.StartsWith(startsWith)).ToList())
            {
                dict.Remove(key);
            }
        }
    }
}
