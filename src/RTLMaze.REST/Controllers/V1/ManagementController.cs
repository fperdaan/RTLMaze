using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using RTLMaze.Core;
using RTLMaze.Core.Scraper;
using RTLMaze.DAL;
using RTLMaze.Models;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Options;

namespace RTLMaze.REST.Controllers.V1;

[ApiController, ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]"), Route("api/latest/[controller]")]
public partial class ManagementController : Controller
{
	[HttpGet, Route("content-update")]
	public IActionResult ContentUpdate( [FromServices] IRepository<Title> repo, [FromServices] IOptions<ScraperOptions> options )
	{	
		// var result = new JsonStreamProcessor<List<Title>>()
		// 				.SetJsonOptions( options.Value )
		// 				.Process( new HttpSource( "https://api.tvmaze.com/shows?page=2" ) );

		// repo.SaveAllLazy( result );

		var id = 1;

		var url = $"https://api.tvmaze.com/shows/{id}?embed=cast";
		var source = new HttpSource( url );

		var result = new JsonStreamProcessor<Title>()
					.SetJsonOptions( options.Value.JsonSerializerOptions )
					.Process( source );

		repo.Add( result );

		return new Response<Title>( result ).Convert();
	}
}