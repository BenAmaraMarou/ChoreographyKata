using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChoreographyKata.Functions.Registration;

internal static class ConfigurationRegistration
{
    internal static void AddConfiguration<TConfiguration>(this IServiceCollection services, string sectionKey)
        where TConfiguration : class =>
        services
            .AddOptions<TConfiguration>()
            .Configure<IConfiguration>((configuration, config) =>
            {
                config.GetSection(sectionKey).Bind(configuration);
            });
}