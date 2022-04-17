namespace Framework.States
{
    public interface IStateMachineDefinition<TTrigger>
    {
        void DefineState<T>(StateFactory<T> factory) where T : IState;

        void DefineStartTransition<T>(TTrigger trigger) where T : IState;
        void DefineTransition<T1, T2>(TTrigger trigger) where T1 : IState where T2 : IState;
    }
}