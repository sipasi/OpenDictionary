using System;

using OpenDictionary.Games.WordConformities.Observables;

namespace OpenDictionary.Games.WordConformities;

public interface IGameEvents
{
    event Action<AnswerButtonObservable> Answered;
}
