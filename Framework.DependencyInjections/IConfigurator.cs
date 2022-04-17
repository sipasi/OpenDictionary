
namespace Framework.DependencyInjection
{
    public interface IConfigurator
    {
        void Configure(ServiceBuilder builder);
    }
}