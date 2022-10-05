using Framework.States;
namespace OpenDictionary.Games.WordConformities.States;

internal abstract class WordConformityState : IState
{
    private readonly IStateMachine<ConformityState> machine;

    protected WordConformityState(IStateMachine<ConformityState> machine) => this.machine = machine;

    public virtual void Enter() { }

    public virtual void Exit() { }

    public void Next(ConformityState state) => machine.Fire(state);
}