using Framework.States;
namespace OpenDictionary.Games.WordConformities.States;

internal class GameStartState : WordConformityState
{
    private IProperties properties;

    public GameStartState(IStateMachine<ConformityState> machine, IProperties properties) : base(machine)
    {
        this.properties = properties;
    }


    public override void Enter()
    {
        base.Enter();

        Next(ConformityState.NextAnswer);
    }
}