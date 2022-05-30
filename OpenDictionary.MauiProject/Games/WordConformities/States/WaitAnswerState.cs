using Framework.States;

using OpenDictionary.Observables.Games;
namespace OpenDictionary.Games.WordConformities.States;

public class WaitAnswerState : WordConformityState
{
    private readonly IProperties properties;
    private readonly IGameEvents events;

    public WaitAnswerState(IStateMachine<ConformityState> machine, IProperties properties, IGameEvents events)
        : base(machine) => (this.events, this.properties) = (events, properties);

    public override void Enter()
    {
        base.Enter();

        events.Answered += Answered;
    }
    public override void Exit()
    {
        base.Exit();

        events.Answered -= Answered;
    }

    private void Answered(AnswerButtonObservable answer)
    {
        ConformityState state = answer.IsCorrect ? ConformityState.CorrectAnswer : ConformityState.UncorrectAnswer;

        properties.Answered++;

        NextState(state);
    }
}
