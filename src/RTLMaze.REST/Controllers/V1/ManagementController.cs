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
	public async Task<IActionResult> ContentUpdate( [FromServices] IRepositoryImporter<Title> repo, [FromServices] IRepository<Job> jobRepo, [FromServices] IOptions<ScraperOptions> options )
	{	
		// var result = new Title("Change this title"){
		// 	ID = 25
		// };

		// result.Cast.Add(new Cast {
		// 	Name = "Foo",
		// 	Title = result,
		// 	Person = new Person("Bar"){
		// 		ID = 15
		// 	}
		// });

		// repo.Import( result );
		// repo.Process();

		var titleProcessor = new JsonStreamProcessor<Title>( options );
		var url = $"https://api.tvmaze.com/shows/41?embed=cast";
			var source = new HttpSource( url );

			var title = titleProcessor.Process( new HttpSource( url ) );
		
			repo.Import( title );
			repo.Process();

		Console.WriteLine("Continue request" );


		return new Response<Title>( title ).Convert();

		/*var result = new JsonStreamProcessor<Dictionary<string, int>>()
						.SetJsonOptions( options.Value.JsonSerializerOptions )
						.Process( new HttpSource( "https://api.tvmaze.com/updates/shows" ) );

		
		var timestamp = new DateTimeOffset( DateTime.UtcNow ).AddDays( -7 ).ToUnixTimeSeconds();

		var updated = result
						.Where( kv => kv.Value >= timestamp )
						.Select( kv => Int32.Parse( kv.Key ) )
						.ToList();


		var titleProcessor = new JsonStreamProcessor<Title>( options );

		var count = 0;

		foreach( int titleId in updated )
		{
			var url = $"https://api.tvmaze.com/shows/{titleId}?embed=cast";
			var source = new HttpSource( url );

			var title = titleProcessor.Process( new HttpSource( url ) );
		
			repo.Import( title );

			if( count++ >= 2 )
			{
				repo.Process();
				return new Response<object>( title ).Convert();
			}
		}

		// Job job = new Job("content-update");

		// job.Start();

		// jobRepo.Save( job );

		return Ok();*/
	}
}