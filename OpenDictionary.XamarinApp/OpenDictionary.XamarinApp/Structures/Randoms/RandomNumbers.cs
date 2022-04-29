using System;

namespace OpenDictionary.Structures.Randoms
{
    public static class RandomNumbers
    {
        private static readonly Random random = Create();

        public static int Range(int min, int max) => random.Next(min, max);

        private static Random Create()
        {
            DateTime now = DateTime.Now;

            int seed = now.Day + now.Hour + now.Minute + now.Second + now.Millisecond;

            Random random = new Random(seed);

            return random;
        }
    }
}
