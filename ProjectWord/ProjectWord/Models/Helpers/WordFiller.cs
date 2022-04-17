namespace OpenDictionary.Models
{
    internal static class WordFiller
    {
        private static readonly Pair[] pairs = new Pair[]
        {
            new Pair(origin:"advice", translation: "порада"),
            new Pair(origin:"insist", translation: "наполягати"),
            new Pair(origin:"agree", translation: "погоджуватися"),
            new Pair(origin:"refuse", translation: "відмовитися"),
            new Pair(origin:"suggest", translation: "запропонувати"),
            new Pair(origin:"adjust", translation: "налаштувати"),
            new Pair(origin:"complain", translation: "скаржитися"),
            new Pair(origin:"prefer", translation: "віддати перевагу"),
            new Pair(origin:"realise", translation: "усвідомити"),
            new Pair(origin:"suppose", translation: "припускати"),
            new Pair(origin:"mean", translation: "означати"),
            new Pair(origin:"belong", translation: "належати"),
            new Pair(origin:"fit", translation: "здоровий, у формі"),
            new Pair(origin:"consist", translation: "складатися з чого"),
            new Pair(origin:"seem", translation: "здаватися"),
            new Pair(origin:"consider", translation: "обдумати"),
            new Pair(origin:"opinion", translation: "погляд"),
            new Pair(origin:"give", translation: "дати"),
            new Pair(origin:"slip", translation: "підсковзнутися"),
            new Pair(origin:"maintly", translation: "в основному"),
            new Pair(origin:"decide", translation: "вирішити"),
            new Pair(origin:"quiet", translation: "тихий"),
            new Pair(origin:"feasible", translation: "здійсненний"),
            new Pair(origin:"grant", translation: "задовольнити"),
            new Pair(origin:"strength", translation: "сила"),
            new Pair(origin:"eventually", translation: "нарешті"),
            new Pair(origin:"bother", translation: "турбувати"),
            new Pair(origin:"afford", translation: "дозволити собі"),
            new Pair(origin:"stairs", translation: "сходи"),
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