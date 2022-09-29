using GitHubActionsSample.NuGet.Services;
using GitHubActionsSample.NuGet.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GitHubActionsSample.NuGet.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterWeatherProvider(this IServiceCollection services)
    {
        services.AddSingleton<IWeatherProvider, MockWeatherProvider>();

        return services;
    }
}