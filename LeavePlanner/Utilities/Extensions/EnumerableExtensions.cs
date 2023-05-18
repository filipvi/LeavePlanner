using System.Diagnostics;

namespace LeavePlanner.Utilities.Extensions
{
    public static class EnumerableExtensions
    {
        [DebuggerStepThrough]
        public static bool HasItems<T>(this IEnumerable<T> source) => source != null && source.Any();

        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) => source == null || source.Any() == false;

        [DebuggerStepThrough]
        public static List<T> ToSafeList<T>(this IEnumerable<T> source) => new List<T>(source);

        public static IEnumerable<IEnumerable<TSource>> ChunkBy<TSource>
            (this IEnumerable<TSource> source, int chunkSize)
        {
            while (source.Any())                     // while there are elements left
            {   // still something to chunk:
                yield return source.Take(chunkSize); // return a chunk of chunkSize
                source = source.Skip(chunkSize);     // skip the returned chunk
            }
        }

    }
}
