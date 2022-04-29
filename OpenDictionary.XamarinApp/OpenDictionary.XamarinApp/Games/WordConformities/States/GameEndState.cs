using Framework.States;
namespace OpenDictionary.Games.WordConformities.States
{
    public class GameEndState : WordConformityState
    {
        private readonly IProperties properties;

        public GameEndState(IStateMachine<ConformityState> machine, IProperties properties) : base(machine)
        {
            this.properties = properties;
        }


        public override void Enter()
        {
            base.Enter();

            properties.Question = "Game end";
        }
    }
}
