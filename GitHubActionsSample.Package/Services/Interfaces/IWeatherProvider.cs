using GitHubActionsSample.Package.Models;

namespace GitHubActionsSample.Package.Services.Interfaces;

public interface IWeatherProvider
{
    Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync();
}