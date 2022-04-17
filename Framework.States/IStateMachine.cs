namespace Framework.States
{
    public interface IStateMachine<TTrigger>
    {
        void Fire(TTrigger trigger);
    }
}