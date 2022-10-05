using Framework.States;

using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Games.WordConformities.States;

internal class GameExitState : WordConformityState
{
    private readonly INavigationService navigation;

    public GameExitState(IStateMachine<ConformityState> machine, INavigationService navigation) : base(machine)
    {
        this.navigation = navigation;
    }

    public override async void Enter()
    {
        await navigation.GoBackAsync();
    }
}