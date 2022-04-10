namespace ProjectWord.Models
{
    internal static class WordFiller
    {
        private static readonly Pair[] pairs = new Pair[]
        {
            new Pair(origin:"complain", translation: "скаржитися"),
            new Pair(origin:"advice", translation: "порада"),
            new Pair(origin:"insist", translation: "наполягати"),
            new Pair(origin:"suppose", translation: "припустимо"),
        };

        public static void FillGroup(WordGroup group, int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                foreach (var pair in pairs)
                {
                    Word word = new Word();

                    Set(word, pair);

                    group.Words.Add(word);
                }
            }
        }

        private static void Set(Word word, Pair pair)
        {
            word.Origin = pair.origin;
            word.Translation = pair.translation;
        }

        private readonly struct Pair
        {
            public readonly string origin;
            public readonly string translation;

            public Pair(string origin, string translation)
            {
                this.origin = origin;
                this.translation = translation;
            }
        }
    }
}
