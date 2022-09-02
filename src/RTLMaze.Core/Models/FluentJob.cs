using RTLMaze.Core.Services;
using RTLMaze.Models;

namespace RTLMaze.Core.Models;

public partial class FluentJob
{
	private Job _job;
	private IJobService _context;

	# region Proxy props
	public int ID => _job.ID;
	public string Type => _job.Type;
	public JobStatus Status => _job.Status;

	public DateTime? DateStart => _job.DateStart;
	public DateTime? DateEnd => _job.DateEnd;

	# endregion


	// Create new job from specified type
	public FluentJob( IJobService context, string type ) : this( context, new Job( type ) ) {}

	// Continue from existing job
	public FluentJob( IJobService context, Job job )
	{
		_job = job;
		_context = context;
	}

	public virtual bool Is( JobStatus status ) => _job.Status == status;

	# region Fluid interface
	public virtual FluentJob End() => End( JobStatus.Processed );

	public virtual FluentJob End( JobStatus status )
	{
		_job.End();

		return SetStatus( status );
	}

	public virtual FluentJob SetStatus( JobStatus status )
	{
		_job.Status = status;

		return this;
	}

	public virtual FluentJob Start()
	{
		_job.Start();

		return SetStatus( JobStatus.Running );
	}
	# endregion	

	// End chain
	public virtual Task Save()
	{
		return _context.Save( _job );
	}
}