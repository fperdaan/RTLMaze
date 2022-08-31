using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RTLMaze.DAL;
using RTLMaze.Models;
using RTLMaze.REST.Models;

namespace RTLMaze.REST.Controllers.V1;

[ApiController, ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]"), Route("api/latest/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet, Route("")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

	[HttpGet, Route("test")]
    public IAsyncEnumerable<Title> Test( [FromServices] IRepository<Title> repo )
    {
        return repo.Query().AsAsyncEnumerable();
    }
}
