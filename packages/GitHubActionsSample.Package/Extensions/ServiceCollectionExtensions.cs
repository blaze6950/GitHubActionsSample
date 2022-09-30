using GitHubActionsSample.Package.Services;
using GitHubActionsSample.Package.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubActionsSample.Package.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterWeatherProvider(this IServiceCollection services)
    {
        services.AddSingleton<IWeatherProvider, MockWeatherProvider>();

        return services;
    }
}