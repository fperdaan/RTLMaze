using Microsoft.AspNetCore.Mvc;

using RTLMaze.Models;
using RTLMaze.Core.Services;
using RTLMaze.Core.Models;

namespace RTLMaze.REST.Controllers.V1;

[ApiController, ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]"), Route("api/latest/[controller]")]
public partial class ManagementController : Controller
{
	[HttpGet, Route("content-update")]
	public async Task<IActionResult> ContentUpdate( [FromServices] IMazeScraperService scraper )
	{	
		// Only allow one job running at the time
		if( scraper.IsAScraperRunning() )
			return new Response<Job>( scraper.GetLastRunJob()! ).Convert();

		// Configure scraper ( we base it on the start date as this is the last time we scraped the content )
		scraper.Since( scraper.GetLastRunJob()?.DateStart );
		
		// Start script and bugger off
		await _ContentUpdateAsync( scraper );


		return new Response<FluentJob>( scraper.Job ).Convert();
	}

	protected async Task _ContentUpdateAsync( IMazeScraperService scraper )
	{
		Console.WriteLine("Start job");
		scraper.Start();

		var items = scraper.FetchChangedTitles();
			items = items.Take(10).ToList();

		foreach( Title title in scraper.FetchTitleDetails( items ) )
		{
			Console.WriteLine( $"{title.ID} / {title.Name}" );
		}


		// DbContext gets disposed in the meantime, this is not the way
		// Probabilly check ( https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=visual-studio )

		scraper.Finish();
		Console.WriteLine("Finish job");
/*

		var result = new JsonStreamProcessor<Dictionary<string, int>>()
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

			if( count++ >= 10 )
			{
				break; 
			}
		}
				
		repo.Process();*/
	}
}