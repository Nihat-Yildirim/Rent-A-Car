using Autofac.Extensions.DependencyInjection;
using Autofac;
using Business.DependencyResolvers.Autofac;

namespace WepAPI.Extensions
{
    public static class HostBuilderExtensions
    {
        public static void ConfigureAutofacProviderFactory(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new AutoFacBusinessModule());
                });
        }
    }
}
