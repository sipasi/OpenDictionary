using Framework.States;
namespace OpenDictionary.Games.WordConformities.States;

public class UncorrectAnswerState : WordConformityState
{
    private readonly IProperties properties;

    public UncorrectAnswerState(IStateMachine<ConformityState> machine, IProperties properties)
        : base(machine) => this.properties = properties;

    public override void Enter()
    {
        base.Enter();

        properties.Uncorrect++;

        NextState(ConformityState.NextAnswer);
    }
}
