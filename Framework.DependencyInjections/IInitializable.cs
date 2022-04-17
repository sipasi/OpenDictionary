namespace Framework.DependencyInjection
{
    public interface IInitializable
    {
        void Initialize(IDiContainer container);
    }
}