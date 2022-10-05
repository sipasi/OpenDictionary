using Framework.States;

namespace OpenDictionary.Games.WordConformities.States;

internal class GameRestartState : WordConformityState
{
    private readonly IProperties properties;
    private readonly IUi ui;

    public GameRestartState(IStateMachine<ConformityState> machine, IProperties properties, IUi ui)
        : base(machine)
    {
        this.properties = properties;
        this.ui = ui;
    }

    public override void Enter()
    {
        properties.Reset();

        ui.UpdateQuestions();

        Next(ConformityState.GameStart);
    }
}