using System;
using System.Collections.Generic;

using OpenDictionary.Games.WordConformities.Observables;
using OpenDictionary.Models;

namespace OpenDictionary.Games.WordConformities
{
    public class UiUpdater : IUi
    {
        private readonly IReadOnlyList<Word> words;
        private readonly GamePropertiesObservable properies;
        private readonly AnswerButtonCollection buttonCollection;
        private readonly Func<Word, string> originText;
        private readonly Func<Word, string> buttonText;

        public UiUpdater(IReadOnlyList<Word> words, GamePropertiesObservable properies, AnswerButtonCollection buttonCollection, Func<Word, string> originText, Func<Word, string> buttonText)
        {
            this.words = words;
            this.properies = properies;
            this.buttonCollection = buttonCollection;
            this.originText = originText;
            this.buttonText = buttonText;
        }

        public void UpdateQuestions()
        {
            Word correct = words[properies.Answered];

            properies.Question = originText.Invoke(correct);

            buttonCollection.Fill(words, correct, buttonText);
        }
    }
}