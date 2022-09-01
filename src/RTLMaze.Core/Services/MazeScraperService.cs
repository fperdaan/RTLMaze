using RTLMaze.Core.Models;
using RTLMaze.Models;

namespace RTLMaze.Core.Services;

public partial class MazeScraperService : IMazeScraperService
{
	public const string JOB_TYPE = "scraping-service";

	private IJobService _jobService;
	private long? _sinceDate;

	private FluentJob? _job;
	public FluentJob Job
	{
		get {  
			if( _job == null )
				_job = _jobService.NewJob( JOB_TYPE );

			return _job;
		}
	}

	public MazeScraperService( IJobService jobService )
	{
		_jobService = jobService;
	}

	public DateTime? GetLastRunTime() => _jobService.GetTimeWhenLastSuccessful( JOB_TYPE );
	public Job? GetLastRunJob() => _jobService.GetLatestForType( JOB_TYPE );
	public bool IsAScraperRunning() => GetLastRunJob()!.Status == JobStatus.Running;

	public IMazeScraperService Since( DateTime? date )
	{
		if( date != null )
			_sinceDate = new DateTimeOffset( (DateTime)date ).ToUnixTimeSeconds();
		else 
			_sinceDate = null;

		return this;
	}

	# region Job interface
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
}