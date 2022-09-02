using Microsoft.Extensions.Options;
using RTLMaze.Core.Models;
using RTLMaze.Core.Scraper;
using RTLMaze.Models;

namespace RTLMaze.Core.Services;

public partial class MazeScraperService : IMazeScraperService
{
	# region Props and backing fields
	public const string JOB_TYPE = "scraping-service";
	
	protected IJobService _jobService;
	protected ScraperOptions _options;
	public long? SinceDate { get; set; }

	private FluentJob? _job;
	public FluentJob Job
	{
		get {  
			if( _job == null )
				_job = _jobService.NewJob( JOB_TYPE );

			return _job;
		}
	}
	# endregion

	public MazeScraperService( IJobService jobService, IOptions<ScraperOptions> options )
	{
		_jobService = jobService;
		_options = options.Value;
	}

	public bool IsAScraperRunning() => GetLastRunJob()!.Status == JobStatus.Running;

	# region Configuration methods
	public IMazeScraperService Since( DateTime? date )
	{
		if( date != null )
			SinceDate = new DateTimeOffset( (DateTime)date ).ToUnixTimeSeconds();
		else 
			SinceDate = null;

		return this;
	}
	# endregion

	# region Job interface

	public Job? GetLastSuccesfulRunJob() => _jobService.GetLastSuccessfulJob( JOB_TYPE );
	public Job? GetLastRunJob() => _jobService.GetLatestForType( JOB_TYPE );

	public FluentJob Start()
	{
		Job.Start().Save();
	
		return Job;
	}

	public FluentJob Finish()
	{
		Job.End().Save();
	
		return Job;
	}

	# endregion

	# region Processing methods 

	public virtual ICollection<int> FetchChangedTitles()
	{
		var result = new JsonStreamProcessor<Dictionary<string, int>>( _options )
						.Process( new HttpSource( _options.UpdateUrl ) );
						
		var query = result.AsQueryable();

		if( SinceDate != null )
			query = query.Where( kv => kv.Value >= SinceDate );

		return query
				.Select( kv => Int32.Parse( kv.Key ) )
				.ToList();
	}

	public virtual IEnumerable<Title> FetchTitleDetails( ICollection<int> ids )
	{
		var titleProcessor = new JsonStreamProcessor<Title>( _options );

		foreach( int id in ids )
		{
			var source = new HttpSource( _options.DetailUrl( id ) );
			var title = titleProcessor.Process( source );

			// Ensure we have valid output
			if( title.ID != default( int ) )
				yield return title;
		}
	}

	# endregion
}