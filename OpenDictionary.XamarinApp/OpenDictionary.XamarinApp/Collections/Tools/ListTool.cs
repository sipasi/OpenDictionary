using System.Collections.Generic;

using OpenDictionary.Structures.Randoms;

namespace OpenDictionary.Collections.Tools
{
    public static class ListTool
    {
        public static IList<T> Randomize<T>(IList<T> array, int times = 1) => Randomize(array, from: 0, to: array.Count, times);
        public static IList<T> Randomize<T>(IList<T> array, int from, int to, int times = 1)
        {
            for (int step = 0; step < times; step++)
            {
                for (int current = 0; current < to; current++)
                {
                    int next = RandomNumbers.Range(from, to);

                    T currentItem = array[current];
                    T nextItem = array[next];

                    array[current] = nextItem;
                    array[next] = currentItem;
                }
            }

            return array;
        }
    }
}
