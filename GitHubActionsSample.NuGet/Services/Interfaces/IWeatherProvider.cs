using GitHubActionsSample.NuGet.Models;

namespace GitHubActionsSample.NuGet.Services.Interfaces;

public interface IWeatherProvider
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
}