using System.Collections.Generic;

namespace ProjectWord.Models
{
    internal static class WordGroupCreator
    {
        public static WordGroup Create(string name)
        {
            WordGroup group = new WordGroup
            {
                Name = name,
                Words = new List<Word>()
            };

            WordFiller.FillGroup(group);

            return group;
        }
        public static WordGroup[] CreateRange(int count)
        {
            WordGroup[] groups = new WordGroup[count];

            for (int i = 0; i < count; i++)
            {
                groups[i] = Create("English Grammar in Use");
            }

            return groups;
        }
    }
}
