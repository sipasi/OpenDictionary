
using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace Framework.States
{
    public class StateMachine<TTrigger> : IStateMachine<TTrigger>, IStateMachineDefinition<TTrigger>
    {
        private readonly Dictionary<Type, IStateDefinition> states
            = new Dictionary<Type, IStateDefinition>();
        private readonly Dictionary<(Type, TTrigger), IStateDefinition> transitions
            = new Dictionary<(Type, TTrigger), IStateDefinition>();

        private (IStateDefinition Definition, IState State) current;

        public void Fire(TTrigger trigger)
        {
            var transition = (current.Definition?.Type, trigger);

            AssertTransition(transition, trigger);

            var definition = transitions[transition];

            current.State?.Exit();
            current = (definition, definition?.GetState());
            current.State?.Enter();

            Assert.IsNotNull(current.State, $"State cant be null. Trigger Type[{typeof(TTrigger).Name}] Key[{trigger.ToString()}]");
        }

        public void DefineState<T>(StateFactory<T> factory) where T : IState
        {
            var type = typeof(T);
            var definition = new StateDefinition<T>(factory);

            states.Add(type, definition);
        }

        public void DefineTransition<T1, T2>(TTrigger trigger)
            where T1 : IState
            where T2 : IState
        {
            var type1 = typeof(T1);
            var type2 = typeof(T2);

            AssertState(type1);
            AssertState(type2);

            transitions.Add((type1, trigger), states[type2]);
        }

        public void DefineStartTransition<T>(TTrigger trigger) where T : IState
        {
            var type = typeof(T);

            AssertState(type);

            transitions.Add((null, trigger), states[type]);
        }

        private void AssertState(Type type)
        {
            string message = $"State of type {type.Name} not defined";

            Assert.IsTrue(states.ContainsKey(type), message);
        }
        private void AssertTransition((Type, TTrigger) transition, TTrigger trigger)
        {
            string message = $"Transition from state[{current.State?.GetType().Name ?? "ROOT"}] not found by trigger {trigger}";

            Assert.IsTrue(transitions.ContainsKey(transition), message);
        }
    }
}