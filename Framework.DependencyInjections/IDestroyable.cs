namespace Framework.DependencyInjection
{
    public interface IDestroyable
    {
        void Destroy(IDiContainer container);
    }
}