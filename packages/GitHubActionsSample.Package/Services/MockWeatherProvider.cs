using GitHubActionsSample.Package.Models;
using GitHubActionsSample.Package.Services.Interfaces;

namespace GitHubActionsSample.Package.Services;

internal class MockWeatherProvider : IWeatherProvider
{
    private static readonly string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<IEnumerable<WeatherForecast>> GetWeatherForecastAsync() =>
        Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }));
}