using Framework.States;

namespace OpenDictionary.Games.WordConformities.States;

internal class NextAnswerState : WordConformityState
{
    private readonly IProperties properties;
    private readonly IUi ui;

    public NextAnswerState(IStateMachine<ConformityState> machine, IProperties properties, IUi ui)
        : base(machine)
    {
        this.properties = properties;
        this.ui = ui;
    }

    public override void Enter()
    {
        base.Enter();

        if (properties.Answered == properties.Total)
        {
            Next(ConformityState.GameEnd);

            return;
        }

        ui.UpdateQuestions();

        Next(ConformityState.WaitAnswer);
    }
}