using GitHubActionsSample.NuGet.Models;
using GitHubActionsSample.NuGet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GitHubActionsSample.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherProvider _weatherProvider;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherProvider weatherProvider, ILogger<WeatherForecastController> logger)
    {
        _weatherProvider = weatherProvider;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get() => await _weatherProvider.GetWeatherForecastAsync();
}