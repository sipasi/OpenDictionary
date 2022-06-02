
using Framework.DependencyInjection;
using Framework.States;

namespace OpenDictionary.Games.WordConformities.States
{
    public struct StateMachineCreator
    {
        public IStateMachine<ConformityState> Create(IProperties properties, IGameEvents events, IUi ui)
        {
            StateMachine<ConformityState> machine = new StateMachine<ConformityState>();

            ServiceBuilder builder = new ServiceBuilder();

            builder.singleton
                .Add<IStateMachine<ConformityState>>(machine)

                .Add<GameStartState>()
                .Add<GameEndState>()
                .Add<NextAnswerState>()
                .Add<WaitAnswerState>()
                .Add<CorrectAnswerState>()
                .Add<UncorrectAnswerState>()

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
            machine.DefineState(container.Get<NextAnswerState>);
            machine.DefineState(container.Get<WaitAnswerState>);
            machine.DefineState(container.Get<CorrectAnswerState>);
            machine.DefineState(container.Get<UncorrectAnswerState>);

            machine.DefineStartTransition<GameStartState>(ConformityState.GameStart);

            machine.DefineTransition<GameStartState, NextAnswerState>(ConformityState.NextAnswer);

            machine.DefineTransition<NextAnswerState, WaitAnswerState>(ConformityState.WaitAnswer);
            machine.DefineTransition<NextAnswerState, GameEndState>(ConformityState.GameEnd);

            machine.DefineTransition<WaitAnswerState, CorrectAnswerState>(ConformityState.CorrectAnswer);
            machine.DefineTransition<WaitAnswerState, UncorrectAnswerState>(ConformityState.UncorrectAnswer);

            machine.DefineTransition<CorrectAnswerState, NextAnswerState>(ConformityState.NextAnswer);
            machine.DefineTransition<UncorrectAnswerState, NextAnswerState>(ConformityState.NextAnswer);
        }
    }
}
