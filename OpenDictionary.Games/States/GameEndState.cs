using System.Threading.Tasks;

using Framework.States;

using OpenDictionary.Services.Messages.Dialogs;

namespace OpenDictionary.Games.WordConformities.States;

internal class GameEndState : WordConformityState
{
    private readonly IProperties properties;
    private readonly IDialogMessageService dialog;

    public GameEndState(IStateMachine<ConformityState> machine, IProperties properties, IDialogMessageService dialog) : base(machine)
    {
        this.properties = properties;
        this.dialog = dialog;
    }


    public override async void Enter()
    {
        base.Enter();

        await Task.Delay(500);

        DialogResult result = await dialog.Show("Play again?", $"Correnct: {properties.Correct}\nUncorrect: {properties.Uncorrect}", ok: "Yes", cancel: "No");

        ConformityState state = result switch
        {
            DialogResult.Accept => ConformityState.GameRestart,
            DialogResult.Cancel => ConformityState.GameExit,

            _ => throw new System.NotImplementedException(),
        };

        Next(state);
    }
}