
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using OpenDictionary.Models;
using OpenDictionary.Observables.Games;
using OpenDictionary.Structures.Randoms;

namespace OpenDictionary.ViewModels.Games
{
    public class AnswerButtonCollection : InteractbleCollection<AnswerButtonObservable>
    {
        public AnswerButtonCollection(Func<AnswerButtonObservable, Task> tappedCommand)
            : base(tappedCommand) { }

        public void Fill(IReadOnlyList<Word> words, Word correct, Func<Word, string> textVisualize)
        {
            int buttonsCount = Collection.Count;
            int wordsCount = words.Count;

            int replaceIndex = RandomNumbers.Range(0, buttonsCount);

            for (int i = 0; i < buttonsCount; i++)
            {
                int ramdom = RandomNumbers.Range(i, wordsCount);

                bool isCorrect = i == replaceIndex;

                Word word = isCorrect ? correct : words[ramdom];

                AnswerButtonObservable info = Collection[i];

                SetButton(info, word, isCorrect, textVisualize);
            }
        }
        private static void SetButton(AnswerButtonObservable info, Word word, bool isCorrect, Func<Word, string> textVisualize)
        {
            info.Text = textVisualize.Invoke(word);

            info.IsCorrect = isCorrect;
        }
    }
}
