using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing","Bracing","Chilly","Cool","Mild","Warm","Balmy","Hot","Sweltering","Scorching"
    };

    private readonly ILogger<WeatherController> _logger;

    public WeatherController(ILogger<WeatherController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Weather endpoint called");
        var rng = new Random();
        return Enumerable.Range(1,5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20,55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

public record WeatherForecast
{
    public DateTime Date { get; init; }
    public int TemperatureC { get; init; }
    public string? Summary { get; init; }
}
