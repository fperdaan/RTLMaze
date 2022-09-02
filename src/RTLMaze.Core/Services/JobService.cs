using RTLMaze.Core.Models;
using RTLMaze.DAL;
using RTLMaze.Models;

namespace RTLMaze.Core.Services;

public partial class JobService : IJobService
{
	private IRepository<Job> _repo;

	public JobService( IRepository<Job> repo )
	{
		_repo = repo;
	}

	public virtual Job GetLatestForType( string type )
	{	
		Job? result = _repo.Query()
							.Where( j => j.Type == type )
							.OrderByDescending( j => j.DateStart )
							.FirstOrDefault();

		if( result == null )
			result = new Job( type );

		return result;
	}

	public virtual Job? GetLastSuccessfulJob( string type )
	{	
		return _repo.Query()
					.Where( j => j.Type == type && j.Status == JobStatus.Processed )
					.OrderByDescending( j => j.DateEnd )
					.FirstOrDefault();
	}

	// convience while managing the job from the repo
	public virtual Task Save( Job job )
	{
		return _repo.Save( job );
	}

	public FluentJob NewJob( string type )
	{
		return new FluentJob( this, type );
	}

	public FluentJob ModifyJob( Job job )
	{
		return new FluentJob( this, job );
	}
}