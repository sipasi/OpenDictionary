
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MvvmHelpers.Commands;

using OpenDictionary.Models;
using OpenDictionary.Structures.Randoms;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Games.WordConformities.Observables
{
    public class AnswerButtonCollection : CollectionViewModel<AnswerButtonObservable>
    {
        private readonly HashSet<Word> hash;
        private readonly List<Word> words;

        public AnswerButtonCollection(Func<AnswerButtonObservable, Task> tappedCommand)
        {
            TappedCommand = new AsyncCommand<AnswerButtonObservable>(tappedCommand);

            int count = Collection.Count;

            int capacity = count == 0 ? 4 : count;

            hash = new HashSet<Word>(capacity);
            words = new List<Word>(capacity);
        }

        public void Fill(IReadOnlyList<Word> all, Word correct, Func<Word, string> questionText)
        {
            FillIncorrectWords(all, correct);

            int buttonsCount = Collection.Count;

            int replaceIndex = RandomNumbers.Range(0, buttonsCount);

            words.Insert(replaceIndex, correct);

            for (int i = 0; i < buttonsCount; i++)
            {
                AnswerButtonObservable info = Collection[i];

                Word word = words[i];

                SetButton(info, word, isCorrect: word == correct, questionText);
            }
        }

        private void FillIncorrectWords(IReadOnlyList<Word> all, Word correct)
        {
            int allCount = all.Count;
            int buttonsCount = Collection.Count;
            int countWithoutCorrect = buttonsCount - 1;

            hash.Clear();
            words.Clear();

            while (words.Count != countWithoutCorrect)
            {
                int random = RandomNumbers.Range(0, allCount);

                Word word = all[random];

                bool isCorrect = word.Id == correct.Id;

                if (isCorrect)
                {
                    continue;
                }

                if (hash.Contains(word))
                {
                    continue;
                }

                hash.Add(word);
                words.Add(word);
            }
        }

        private static void SetButton(AnswerButtonObservable info, Word word, bool isCorrect, Func<Word, string> questionText)
        {
            info.Text = questionText.Invoke(word);

            info.IsCorrect = isCorrect;
        }
    }
}