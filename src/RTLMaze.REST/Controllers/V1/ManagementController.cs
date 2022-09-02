using Microsoft.AspNetCore.Mvc;

using RTLMaze.Models;
using RTLMaze.Core.Services;
using RTLMaze.Core.Models;
using RTLMaze.DAL;

namespace RTLMaze.REST.Controllers.V1;

[ApiController, ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]"), Route("api/latest/[controller]")]
public partial class ManagementController : Controller
{
	[HttpGet, Route("content-update")]
	public async Task<IActionResult> ContentUpdate( [FromServices] IMazeScraperService scraper, [FromServices] IRepositoryImporter<Title> importer )
	{	
		// Only allow one job running at the time
		//if( scraper.IsAScraperRunning() )
		//	return new Response<Job>( scraper.GetLastRunJob()! ).Convert();

		// Configure scraper ( we base it on the start date as this is the last time we scraped the content )
		//scraper.Since( scraper.GetLastRunJob()?.DateStart );
		
		// Start script and bugger off
		await _ContentUpdateAsync( scraper, importer );


		return new Response<FluentJob>( scraper.Job ).Convert();
	}

	protected Task _ContentUpdateAsync( IMazeScraperService scraper, IRepositoryImporter<Title> importer )
	{
		scraper.Start();

		try 
		{
			var items = scraper.FetchChangedTitles();
				items = items.Skip(20).Take(20).ToList();

			scraper.FetchTitleDetails( items )
				.AsParallel()
				.ForAll( title => {
					importer.Import( title );
				});

			importer.Process();
			scraper.Finish();
		}
		catch( Exception e )
		{
			// We should probably also log / handle the exception here

			scraper.Finish( JobStatus.Failed );

			throw e;
		}


		// DbContext gets disposed in the meantime, starting this async and quiting the request is not the way
		// Probabilly check ( https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-6.0&tabs=visual-studio )

		return Task.CompletedTask;
	}
}