using Microsoft.Extensions.Options;

namespace ChoreographyKata.Tests;

internal static class ConfigurationOptionsFactory
{
    internal static IOptions<TConfiguration> CreateOptions<TConfiguration>(this TConfiguration configuration)
        where TConfiguration : class =>
        new OptionsWrapper<TConfiguration>(configuration);
}