using Microsoft.Extensions.Options;
using Polly.RateLimit;
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

	private RateLimitPolicy _rateLimitPolicy;
	
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

		_rateLimitPolicy = _options.HttpRateLimiterPolicy;
	}

	public bool IsAScraperRunning() => GetLastRunJob()!.Status == JobStatus.Running;

	# region Configuration methods
	public virtual IMazeScraperService Since( DateTime? date )
	{
		if( date != null )
			SinceDate = new DateTimeOffset( (DateTime)date ).ToUnixTimeSeconds();
		else 
			SinceDate = null;

		return this;
	}

	public virtual IMazeScraperService SetRateLimitPolicy( RateLimitPolicy policy )
	{
		_rateLimitPolicy = policy;

		return this;
	}

	# endregion

	# region Job interface

	public virtual Job? GetLastSuccesfulRunJob() => _jobService.GetLastSuccessfulJob( JOB_TYPE );
	public virtual Job? GetLastRunJob() => _jobService.GetLatestForType( JOB_TYPE );

	public virtual FluentJob Start()
	{
		Job.Start().Save();
	
		return Job;
	}

	public virtual FluentJob Finish() => Finish( JobStatus.Processed );

	public virtual FluentJob Finish( JobStatus status )
	{
		Job.End( status ).Save();
	
		return Job;
	}

	# endregion

	# region Processing methods 

	public virtual ICollection<int> FetchChangedTitles()
	{
		var source = new ChangedTitleSource( _options );
			source.SinceDate = SinceDate;

		return source.GetSource();
	}

	public virtual IEnumerable<Title> FetchTitleDetails( ICollection<int> ids )
	{
		var policy = _rateLimitPolicy;

		var titleProcessor = new JsonStreamProcessor<Title>( _options );
		var source = new HttpSource( _options );
		
		foreach( int id in ids )
		{
			Title? title = _Throttle( 0, 3, policy, () => titleProcessor.Process( source.FromUrl( _options.DetailUrl( id ) ) ) );

			// Ensure we have valid output
			if( title != null && title.ID != default( int ) )
				yield return title;
		}
	}

	protected virtual Title? _Throttle( int attempt, int maxAttempts, RateLimitPolicy policy, Func<Title> request )
	{
		try
		{
			return policy.Execute( request );
		}
		catch( RateLimitRejectedException )
		{
			if( attempt + 1 >= maxAttempts )
				return null;

			Console.WriteLine( $"{Thread.CurrentThread.ManagedThreadId} hit limit, delaying output" );
			Thread.Sleep( 10000 );

			return _Throttle( attempt, maxAttempts, policy, request );
		}
	}

	# endregion
}