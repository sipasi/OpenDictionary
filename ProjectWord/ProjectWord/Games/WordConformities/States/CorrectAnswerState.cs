﻿using Framework.States;
namespace OpenDictionary.Games.WordConformities.States
{
    public class CorrectAnswerState : WordConformityState
    {
        private readonly IProperties properties;

        public CorrectAnswerState(IStateMachine<ConformityState> machine, IProperties properties)
            : base(machine) => this.properties = properties;

        public override void Enter()
        {
            base.Enter();

            properties.Correct++;

            NextState(ConformityState.NextAnswer);
        }
    }
}
