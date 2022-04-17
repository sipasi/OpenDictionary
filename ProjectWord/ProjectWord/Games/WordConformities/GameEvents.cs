using System;

using OpenDictionary.Observables.Games;

namespace OpenDictionary.Games.WordConformities
{
    public class GameEvents : IGameEvents
    {
        public event Action<AnswerButtonObservable> Answered;

        public void InvokeAnswered(AnswerButtonObservable answer) => Answered?.Invoke(answer);
    }
}
