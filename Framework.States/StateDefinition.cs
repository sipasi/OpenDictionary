using System;

namespace Framework.States
{
    public delegate T StateFactory<out T>();

    internal class StateDefinition<T> : IStateDefinition where T : IState
    {
        private readonly StateFactory<T> factory;
        private readonly Type type;

        public Type Type => type;


        public StateDefinition(StateFactory<T> factory)
        {
            this.factory = factory;
            type = typeof(T);
        }


        public IState GetState() => factory.Invoke();
    }
}