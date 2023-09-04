using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Extensions
{
    public static class ListExtensions
    {
        public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        public static List<T> Populate<T>(this List<T> source, int count, Func<T> provider)
        {
            for (int i = 0; i < count; i++)
            {
                source.Add(provider());
            }
            return source;
        }
    }
}
