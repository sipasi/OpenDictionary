using System;

using OpenDictionary.Observables.Games;

namespace OpenDictionary.Games.WordConformities;

public interface IGameEvents
{
    event Action<AnswerButtonObservable> Answered;
}
