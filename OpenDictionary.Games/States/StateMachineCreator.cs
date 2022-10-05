
using Framework.DependencyInjection;
using Framework.States;

using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Games.WordConformities.States;

internal struct StateMachineCreator
{
    public IStateMachine<ConformityState> Create(IProperties properties, IGameEvents events, IUi ui, IDialogMessageService dialog, INavigationService navigation)
    {
        StateMachine<ConformityState> machine = new();

        ServiceBuilder builder = new();

        builder.singleton
            .Add<IStateMachine<ConformityState>>(machine)

            .Add<GameStartState>()
            .Add<GameEndState>()
            .Add<GameRestartState>()
            .Add<GameExitState>()
            .Add<NextAnswerState>()
            .Add<WaitAnswerState>()
            .Add<CorrectAnswerState>()
            .Add<UncorrectAnswerState>()

            .Add<INavigationService>(navigation)
            .Add<IDialogMessageService>(dialog)

            .Add<IProperties>(properties)
            .Add<IGameEvents>(events)
            .Add<IUi>(ui);

        IDiContainer container = builder.Build();

        Initialize(machine, container);

        return machine;
    }

    private void Initialize(IStateMachineDefinition<ConformityState> machine, IDiContainer container)
    {
        machine.DefineState(container.Get<GameStartState>);
        machine.DefineState(container.Get<GameEndState>);
        machine.DefineState(container.Get<GameRestartState>);
        machine.DefineState(container.Get<GameExitState>);
        machine.DefineState(container.Get<NextAnswerState>);
        machine.DefineState(container.Get<WaitAnswerState>);
        machine.DefineState(container.Get<CorrectAnswerState>);
        machine.DefineState(container.Get<UncorrectAnswerState>);

        machine.DefineStartTransition<GameStartState>(ConformityState.GameStart);

        machine.DefineTransition<GameStartState, NextAnswerState>(ConformityState.NextAnswer);

        machine.DefineTransition<NextAnswerState, WaitAnswerState>(ConformityState.WaitAnswer);
        machine.DefineTransition<NextAnswerState, GameEndState>(ConformityState.GameEnd);

        machine.DefineTransition<GameEndState, GameRestartState>(ConformityState.GameRestart);
        machine.DefineTransition<GameEndState, GameExitState>(ConformityState.GameExit);

        machine.DefineTransition<GameRestartState, GameStartState>(ConformityState.GameStart);

        machine.DefineTransition<WaitAnswerState, CorrectAnswerState>(ConformityState.CorrectAnswer);
        machine.DefineTransition<WaitAnswerState, UncorrectAnswerState>(ConformityState.UncorrectAnswer);

        machine.DefineTransition<CorrectAnswerState, NextAnswerState>(ConformityState.NextAnswer);
        machine.DefineTransition<UncorrectAnswerState, NextAnswerState>(ConformityState.NextAnswer);
    }
}